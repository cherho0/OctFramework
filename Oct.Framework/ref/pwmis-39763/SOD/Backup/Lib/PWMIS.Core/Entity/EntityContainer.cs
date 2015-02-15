﻿/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * PDF.NET 数据开发框架
 * http://www.pwmis.com/sqlmap
 * 
 * 详细内容，请参看“打造轻量级的实体类数据容器”
 * （ http://www.cnblogs.com/bluedoctor/archive/2011/05/23/2054541.html）
 * 
 * 作者：邓太华     时间：2008-10-12
 * 版本：V4.6.2
 * 
 * 修改者：         时间：2012-11-2                
 * 修改说明：修复一个复杂查询时候的分页错误 
 * 
 * 修改者：         时间：2013-4-7                
 * 修改说明：新增 MapToList 方法，可以将多实体类查询结果映射到匿名列表结果类型
 * 
 * 修改者：         时间：2013-4-16                
 * 修改说明：新增 MapToDataTable 方法，可以将多实体类查询结果映射到数据表
 * 
 * 
 * 修改者：         时间：2014-4-14                
 * 修改说明：如果OQL未设置记录数量（等于0），那么查询会先进行一次记录数量查询。感谢网友 成都-小兵 发现此问题。

 * 修改者：         时间：2015-1-29                
 * 修改说明：如果查询记录为0条记录，转换成DataTable 的时候会抱错，感谢网友 成都-小兵 发现此问题。
 * 
 * ========================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PWMIS.DataMap.Entity;
using PWMIS.DataProvider.Data;
using PWMIS.Core;
using PWMIS.Common;

namespace PWMIS.DataMap.Entity
{

    public delegate TResult ECResultFunc<TResult>(EntityContainer ec);

    /// <summary>
    /// 实体数据容器
    /// </summary>
    public class EntityContainer
    {
        private OQL oql;
        private AdoHelper db;
        private string[] fieldNames;
        private Type[] fieldTypes;
        private List<object[]> Values;
        private object[] currValue;

        /// <summary>
        /// 以 TResult为输入参数，并返回此类型的函数的委托定义
        /// </summary>
        /// <typeparam name="TResult">输入类型</typeparam>
        /// <param name="arg">参数</param>
        /// <returns></returns>
        public delegate TResult Func<TResult>(TResult arg);
        /// <summary>
        /// 返回一个结果类型的泛型委托函数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public delegate TResult MyFunc<TResult>();

        

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EntityContainer()
        {

        }

        /// <summary>
        /// 使用OQL表达式和数据访问对象实例初始化。如果OQL未设置记录数量（等于0），那么查询会先进行一次记录数量查询。
        /// </summary>
        /// <param name="oql">OQL表达式</param>
        /// <param name="db">数据访问对象实例</param>
        public EntityContainer(OQL oql, AdoHelper db)
        {
            this.OQL = oql;
            this.DataBase = db;
        }

        /// <summary>
        /// 使用查询表达式初始化。如果OQL未设置记录数量（等于0），那么查询会先进行一次记录数量查询。
        /// </summary>
        /// <param name="oql"></param>
        public EntityContainer(OQL oql)
        {
            this.OQL = oql;
        }

        /// <summary>
        /// 查询表达式
        /// </summary>
        public OQL OQL
        {
            get { return this.oql; }
            set { this.oql = value; }
        }

        /// <summary>
        /// 数据访问对象
        /// </summary>
        public AdoHelper DataBase
        {
            get
            {
                if (db == null)
                    db = PWMIS.DataProvider.Adapter.MyDB.Instance;
                return db;
            }
            set { db = value; }
        }

        private IDataReader ExecuteDataReader(OQL oql, AdoHelper db)
        {
            string sql = "";
            sql = oql.ToString();

            //处理实体类分页 2010.6.20
            if (oql.PageEnable)
            {
                //处理分页统前的记录数量统计问题 感谢网友 @成都-小兵 发现此问题
                if (oql.PageWithAllRecordCount == 0)
                {
                    object oValue = EntityQueryAnonymous.ExecuteOQLCount(oql, db);
                    oql.PageWithAllRecordCount = CommonUtil.ChangeType<int>(oValue);
                }

                switch (db.CurrentDBMSType)
                {
                    case PWMIS.Common.DBMSType.Access:
                    case PWMIS.Common.DBMSType.SqlServer:
                    case PWMIS.Common.DBMSType.SqlServerCe:
                        if (oql.haveJoinOpt)
                        {
                            if (oql.PageNumber <= 1) //仅限定记录条数
                            {
                                sql = "Select Top " + oql.PageSize + " " + sql.Trim().Substring("SELECT ".Length);

                            }
                            else //必须采用复杂分页方案
                            {
                                //edit at 2012.10.2 oql.PageWithAllRecordCount
                                sql = PWMIS.Common.SQLPage.MakeSQLStringByPage(PWMIS.Common.DBMSType.SqlServer, sql, "", oql.PageSize, oql.PageNumber, oql.PageWithAllRecordCount);

                            }
                        }
                        else
                        {
                            //单表查询的情况
                            if (oql.PageOrderDesc)
                                sql = PWMIS.Common.SQLPage.GetDescPageSQLbyPrimaryKey(oql.PageNumber, oql.PageSize, oql.sql_fields, oql.sql_table, oql.PageField, oql.sql_condition);
                            else
                                sql = PWMIS.Common.SQLPage.GetAscPageSQLbyPrimaryKey(oql.PageNumber, oql.PageSize, oql.sql_fields, oql.sql_table, oql.PageField, oql.sql_condition);
                        }

                        break;
                    case PWMIS.Common.DBMSType.Oracle:
                        sql = PWMIS.Common.SQLPage.MakeSQLStringByPage(PWMIS.Common.DBMSType.Oracle, sql, "", oql.PageSize, oql.PageNumber, 999);
                        break;
                    case PWMIS.Common.DBMSType.MySql:
                        sql = PWMIS.Common.SQLPage.MakeSQLStringByPage(PWMIS.Common.DBMSType.MySql, sql, "", oql.PageSize, oql.PageNumber, 999);
                        break;
                    case PWMIS.Common.DBMSType.PostgreSQL:
                        sql = PWMIS.Common.SQLPage.MakeSQLStringByPage(PWMIS.Common.DBMSType.PostgreSQL, sql, "", oql.PageSize, oql.PageNumber, 999);
                        break;
                    default:
                        throw new Exception("实体类分页错误：不支持此种类型的数据库分页。");

                }

            }

            IDataReader reader = null;
            if (oql.Parameters != null && oql.Parameters.Count > 0)
            {
                int fieldCount = oql.Parameters.Count;
                IDataParameter[] paras = EntityQueryAnonymous.GetParameters(oql.Parameters, db);
                //int index = 0;

                //foreach (string name in oql.Parameters.Keys)
                //{
                //    paras[index] = db.GetParameter(name, oql.Parameters[name]);
                //    index++;
                //}
                reader = db.ExecuteDataReader(sql, CommandType.Text, paras);
            }
            else
            {
                reader = db.ExecuteDataReader(sql);
            }
            return reader;
        }

        /// <summary>
        /// 执行OQL查询，并将查询结果缓存。如果未设置记录数量，那么查询会先进行一次记录数量查询。
        /// </summary>
        /// <returns>结果的行数</returns>
        public int Execute()
        {
            IDataReader reader = ExecuteDataReader(this.OQL, this.DataBase);
            return Execute(reader);
        }

        /// <summary>
        /// 执行DataReader查询，并将查询结果缓存
        /// </summary>
        /// <param name="reader">数据阅读器</param>
        /// <returns>结果行数</returns>
        public int Execute(IDataReader reader)
        {
            List<object[]> list = new List<object[]>();
            using (reader)
            {
                int fcount = reader.FieldCount;
                fieldNames = new string[fcount];
                fieldTypes=new Type[fcount];
                if (reader.Read())
                {

                    object[] values = null;

                    for (int i = 0; i < fcount; i++)
                    {
                        fieldNames[i] = reader.GetName(i);
                        fieldTypes[i] = reader.GetFieldType(i);
                    }

                    do
                    {
                        values = new object[fcount];
                        reader.GetValues(values);
                        list.Add(values);
                    } while (reader.Read());

                }
            }
            this.Values = list;
            return list.Count;
        }

        /// <summary>
        /// 获取容器数据中的字段名数组
        /// </summary>
        public string[] FieldNames
        {
            get { return this.fieldNames; }
        }
        /// <summary>
        /// 获取容器数据中的每一行的值
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object[]> GetItemValues()
        {
            if (this.Values != null && this.fieldNames != null)
            {
                foreach (object[] itemValues in this.Values)
                {
                    yield return itemValues;
                }
            }
        }

        /// <summary>
        /// 将数据从容器中映射到实体中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Map<T>() where T : EntityBase
        {
            if (this.Values == null)
            {
                int rowsCount = this.Execute();
                if (rowsCount <= 0)
                    yield break;

            }
            if (this.Values != null && this.fieldNames != null)
            {
                if (this.Values.Count == 0)
                    yield break;

                Dictionary<string, int> dictNameIndex = new Dictionary<string, int>();
                T entity = Activator.CreateInstance<T>();
                string tabeName = entity.TableName;
                //查找字段匹配情况
                //entity.PropertyNames 存储的仅仅是查询出来的列名称，由于有连表查询，
                //如果要映射到指定的实体，还得检查当前列对应的表名称
                if (this.OQL.sql_fields.Contains("[" + tabeName + "]"))
                {
                    //是连表查询
                    for (int i = 0; i < this.fieldNames.Length; i++)
                    {
                        for (int j = 0; j < entity.PropertyNames.Length; j++)
                        {
                            string cmpString = "[" + tabeName + "].[" + entity.PropertyNames[j] + "]";
                            if (this.OQL.sql_fields.Contains(cmpString))
                            {
                                dictNameIndex[this.fieldNames[i]] = i;
                            }
                        }


                    }
                }
                else
                {
                    for (int i = 0; i < this.fieldNames.Length; i++)
                    {
                        for (int j = 0; j < entity.PropertyNames.Length; j++)
                        {
                            if (this.fieldNames[i] == entity.PropertyNames[j])
                            {
                                dictNameIndex[this.fieldNames[i]] = i;
                            }
                        }
                    }
                }

                //没有匹配的，提前结束
                if (dictNameIndex.Count == 0)
                    yield break;

                int length = entity.PropertyValues.Length;
                foreach (object[] itemValues in this.Values)
                {
                    for (int m = 0; m < length; m++)
                    {
                        //将容器的值赋值给实体的值元素
                        string key = entity.PropertyNames[m];
                        if (dictNameIndex.ContainsKey(key))
                            entity.PropertyValues[m] = itemValues[dictNameIndex[key]];
                    }
                    yield return entity;
                    //创建一个新实例
                    entity = Activator.CreateInstance<T>();
                }
            }
            else
            {
                throw new Exception("EntityContainer 错误，执行查询没有返回任何行。");
            }
        }

        private object propertyList(string propertyName, object[] itemValues)
        {
            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (string.Compare(fieldNames[i], propertyName, true) == 0)
                {
                    return itemValues[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 根据字段名，从当前行获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public T GetItemValue<T>(string fieldName)
        {
            if (this.currValue != null)
                return CommonUtil.ChangeType<T>(propertyList(fieldName, this.currValue));
            else
                return default(T);
        }

        /// <summary>
        /// 根据字段索引，从当前行获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldIndex"></param>
        /// <returns></returns>
        public T GetItemValue<T>(int fieldIndex)
        {
            if (this.currValue != null)
                return CommonUtil.ChangeType<T>(this.currValue[fieldIndex]);
            else
                return default(T);
        }

        /// <summary>
        /// 采用自定义的映射方式，将数据容器中的数据映射到指定的类中 
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="fun">处理数据的方法</param>
        /// <returns></returns>
        public IEnumerable<TResult> Map<TResult>(Func<TResult> fun) where TResult : class, new()
        {
            if (this.Values == null)
                this.Execute();
            if (this.Values != null && this.fieldNames != null)
            {
                foreach (object[] itemValues in this.Values)
                {
                    TResult t = new TResult();
                    this.currValue = itemValues;
                    fun(t);
                    yield return t;
                }
            }
            else
            {
                throw new Exception("EntityContainer 错误，执行查询没有返回任何行。");
            }
        }

        /// <summary>
        /// 将结果映射到相应类型的列表（可以使匿名类型）
        /// <example>
        /// <code>
        /// <![CDATA[
        /// OQL q=OQL.From(entity1)
        ///          .Join(entity2).On(entity1.PK,entity2.FK)
        ///          .Select(entity1.Field1,entity2.Field2)
        ///       .End;
        /// EntityContainer ec=new EntityContainer(q);
        /// var list=ec.MapToList(()=>
        ///          {
        ///             return new {
        ///                          Property1=ec.GetItemValue<int>(0), 
        ///                          Property2=ec.GetItemValue<string>(1) 
        ///                        };
        ///          });
        /// 
        /// foreache(var item in list)
        /// {
        ///     Console.WriteLine("Property1={0},Property2={1}",item.Property1,item.Property2);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TResult">要映射的结果类型</typeparam>
        /// <param name="fun">自定义的返回结果类型的函数</param>
        /// <returns>结果列表</returns>
        public IList<TResult> MapToList<TResult>(MyFunc<TResult> fun) where TResult : class
        {
            if (this.Values == null)
                this.Execute();
            List<TResult> resultList = new List<TResult>();
            if (this.Values != null && this.fieldNames != null)
            {
                foreach (object[] itemValues in this.Values)
                {
                    
                    this.currValue = itemValues;
                   TResult t = fun();
                   resultList.Add(t);
                }
                return resultList;
            }
            else
            {
                throw new Exception("EntityContainer 错误，执行查询没有返回任何行。");
            }
        }

        /// <summary>
        /// 将实体类容器转换为对象列表
        /// <example>
        /// <code>
        /// <![CDATA[
        /// OQL q=OQL.From(entity1)
        ///          .Join(entity2).On(entity1.PK,entity2.FK)
        ///          .Select(entity1.Field1,entity2.Field2)
        ///       .End;
        /// EntityContainer ec=new EntityContainer(q);
        /// var list=ec.ToObjectList( e =>
        ///          {
        ///             return new {
        ///                          Property1=e.GetItemValue<int>(0), 
        ///                          Property2=e.GetItemValue<string>(1) 
        ///                        };
        ///          });
        /// 
        /// foreache(var item in list)
        /// {
        ///     Console.WriteLine("Property1={0},Property2={1}",item.Property1,item.Property2);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="TResult">结果的列表元素类型</typeparam>
        /// <param name="fun">容器结果委托函数</param>
        /// <returns>对象列表</returns>
        public IList<TResult> ToObjectList<TResult>(ECResultFunc<TResult> fun) where TResult : class
        {
            if (this.Values == null)
                this.Execute();
            List<TResult> resultList = new List<TResult>();
            if (this.Values != null && this.fieldNames != null)
            {
                foreach (object[] itemValues in this.Values)
                {

                    this.currValue = itemValues;
                    TResult t = fun(this);
                    resultList.Add(t);
                }
                return resultList;
            }
            else
            {
                throw new Exception("EntityContainer 错误，执行查询没有返回任何行。");
            }
        }

        /// <summary>
        /// 将容器的结果数据映射到DataTable
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>DataTable</returns>
        public DataTable MapToDataTable(string tableName)
        {
            if (this.Values == null)
                this.Execute();
            if (this.Values != null && this.fieldNames != null)
            {
                DataTable dt = new DataTable(tableName);
                for (int i = 0; i < this.fieldNames.Length; i++)
                {
                    DataColumn col = new DataColumn(this.fieldNames[i]);
                    //下面的方式当数据为空的时候,会报错,感谢网友 @成都-小兵 发现此问题
                    //object V = this.Values[0][i];
                    //col.DataType = V == null || V == DBNull.Value ? typeof(string) : V.GetType();
                    col.DataType = this.fieldTypes[i];
                    dt.Columns.Add(col);
                }

                foreach (object[] itemValues in this.Values)
                {
                    dt.Rows.Add(itemValues);
                }
                return dt;
            }
            else
            {
                throw new Exception("EntityContainer 错误，执行查询没有返回任何行。");
            }
        }
    }
}

using Oct.Tools.Core.T4;
using Oct.Tools.Core.Unity;
using Oct.Tools.Plugin.CodeGenerator.Dal;
using Oct.Tools.Plugin.CodeGenerator.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;

namespace Oct.Tools.Plugin.CodeGenerator.Bll
{
    public class TableBll
    {
        #region 属性

        /// <summary>
        /// 默认命名空间
        /// </summary>
        public static string DefaultNameSpace
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultNameSpace"];
            }
        }

        /// <summary>
        /// 模板文件的存放文件夹地址
        /// </summary>
        public static string TempDirectoryPath
        {
            get
            {
                return string.Format(@"{0}\Temp", Environment.CurrentDirectory);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TextConnection(string connectionString)
        {
            return DBHelper.TextConnection(connectionString);
        }

        /// <summary>
        /// 批量执行Sql（带事务）
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static bool ExecuteSqls(string connectionString, string[] sqls)
        {
            return DBHelper.ExecuteSqls(connectionString, sqls);
        }

        /// <summary>
        /// 根据查询语句获取数据
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataByQuery(string connectionString, string sql)
        {
            var dt = DBHelper.GetDataTable(connectionString, sql);

            return dt;
        }

        /// <summary>
        /// 获取数据库表的名称集合
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<string> GetDBTableNameList(string connectionString)
        {
            return TableDal.GetDBTableNameList(connectionString);
        }

        /// <summary>
        /// 获取数据库表的信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDBTableInfo(string connectionString, string tableName)
        {
            return TableDal.GetDBTableInfo(connectionString, tableName);
        }

        /// <summary>
        /// 获取代码生成所需的信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCompositeQuery"></param>
        /// <returns></returns>
        public static CodeBaseInfo GetCodeBaseInfoByDBTable(DataTable dt, bool isCompositeQuery = false)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;

            var codeBase = new CodeBaseInfo() { TableName = dt.TableName };

            foreach (DataRow r in dt.Rows)
            {
                var filed = new FiledInfo()
                {
                    No = 1,
                    Name = string.Empty,
                    CSharpType = string.Empty,
                    CommonType = typeof(object),
                    Length = 0,
                    DecimalPlace = 0,
                    IsIdentify = false,
                    IsPk = false,
                    IsAllowNull = false,
                    Description = string.Empty
                };

                if (dt.Columns.Contains("序号"))
                    filed.No = int.Parse(r["序号"].ToString());

                if (dt.Columns.Contains("列名"))
                    filed.Name = r["列名"].ToString();

                if (dt.Columns.Contains("数据类型"))
                {
                    if (isCompositeQuery)
                        filed.CSharpType = r["数据类型"].ToString();
                    else
                    {
                        filed.CSharpType = ConvertUnity.MapCsharpType(r["数据类型"].ToString());

                        filed.CommonType = ConvertUnity.MapCommonType(r["数据类型"].ToString());
                    }
                }

                if (dt.Columns.Contains("长度"))
                    filed.Length = int.Parse(r["长度"].ToString());

                if (dt.Columns.Contains("小数"))
                    filed.DecimalPlace = int.Parse(r["小数"].ToString());

                if (dt.Columns.Contains("标识"))
                    filed.IsIdentify = bool.Parse(r["标识"].ToString());

                if (dt.Columns.Contains("主键"))
                    filed.IsPk = bool.Parse(r["主键"].ToString());

                if (dt.Columns.Contains("允许空"))
                    filed.IsAllowNull = bool.Parse(r["允许空"].ToString());

                if (dt.Columns.Contains("说明"))
                    filed.Description = r["说明"].ToString();

                codeBase.FiledList.Add(filed);
            }

            return codeBase;
        }

        /// <summary>
        /// 获取类名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetClassName(string tableName)
        {
            return tableName.Replace("-", string.Empty).Replace("_", string.Empty);
        }

        /// <summary>
        /// 代码生成
        /// </summary>
        /// <param name="codeBaseInfo"></param>
        /// <param name="tempPath"></param>
        /// <returns></returns>
        public static string CodeGenerator(CodeBaseInfo codeBaseInfo, string tempPath)
        {
            var temp = new FileInfo(tempPath);

            if (!temp.Exists)
                throw new Exception(string.Format("模板：“{0}“不存在！", temp.Name));

            var output = TemplateMgr.ProcessTemplate("dt", codeBaseInfo, temp, new string[] { typeof(CodeBaseInfo).Namespace });

            return output;
        }

        #endregion
    }
}

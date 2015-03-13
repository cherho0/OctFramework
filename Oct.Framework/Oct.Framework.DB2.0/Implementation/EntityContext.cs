using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Interface;
using Oct.Framework.DB.LightDataAccess;

namespace Oct.Framework.DB.Implementation
{
    public class EntityContext<T> : IDBContext<T> where T : BaseEntity<T>, new()
    {
        public ISession Session { get; private set; }

        public EntityContext(ISession session)
        {
            Session = session;
        }

        /// <summary>
        ///     新增一个对象
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            DbCommand cmd = CreateSqlCommand(entity.GetInsertCmd());
            var idp = EntitiesProxyHelper.GetProxyInfo<T>().IdentitesProp;
            if (!string.IsNullOrEmpty(idp))
            {
                object v = null;
                cmd.CommandText += "; SELECT @@IDENTITY ";
                var ds = Session.ExecCmd(cmd);
                if (ds.Tables.Count == 0)
                    return;

                if (ds.Tables[0].Rows.Count == 0)
                    return;

                v = ds.Tables[0].Rows[0][0];
                EntitiesProxyHelper.GetDynamicMethod<T>().SetValue(entity, idp, v.ToString().ToInt());
            }
            else
            {
                Session.AddCommands(cmd);
            }
        }

        /// <summary>
        ///     更新一个对象
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            DbCommand cmd = CreateSqlCommand(entity.GetUpdateCmd());
            Session.AddCommands(cmd);
        }

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            string cmdtext = entity.GetDelSql();
            var cmd = new SqlCommand(cmdtext);
            Session.AddCommands(cmd);
        }

        public void Delete(object pk)
        {
            var entity = new T();
            string cmdtext = entity.GetDelSql(pk);
            var cmd = new SqlCommand(cmdtext);
            Session.AddCommands(cmd);
        }

        public void Delete()
        {
            var entity = new T();
            string cmdtext = entity.GetDelSql();
            var cmd = new SqlCommand(cmdtext);
            Session.AddCommands(cmd);
        }

        /// <summary>
        ///     获取一个实体对象
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T GetModel(object pk)
        {
            var entity = new T();
            string sql = entity.GetModelSql(pk);
            ISQLContext sqlContext = new SQLContext(Session);
            var reader = sqlContext.ExecuteQueryReader(sql);
            if (!reader.Read())
            {
                reader.Close();
                reader.Dispose();
                return null;
            }
            var r = ((DbDataReader)reader).ToObject<T>();
            reader.Close();
            reader.Dispose();
            r.EntryUpdateStack();
            return r;
        }

        /// <summary>
        ///     查询一系列实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> Query(string @where, IDictionary<string, object> paras, string order = "")
        {
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }

            var entity = new T();
            string sql = entity.GetQuerySql(@where);
            if (!order.IsNullOrEmpty())
            {
                sql += " order by " + order;
            }
            ISQLContext sqlContext = new SQLContext(Session);
            var paramters = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    paramters.Add(new SqlParameter(para.Key, para.Value));
                }
            }
            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var listdata = ((DbDataReader)reader).ToObjects<T>().ToList();
            reader.Close();
            reader.Dispose();
            /*foreach (var data in listdata)
            {
                data.ClearStack();
            }*/
            return listdata;
        }

        /// <summary>
        ///     查询一系列实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> Query(string @where, string order = "")
        {
            return Query(@where, null, "");
        }

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public PageResult<T> QueryPage(string @where, IDictionary<string, object> paras, string order, int pageIndex,
            int pageSize)
        {
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var parasList = new List<SqlParameter>();
            var parasListData = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    parasList.Add(new SqlParameter(para.Key, para.Value));
                    parasListData.Add(new SqlParameter(para.Key, para.Value));
                }
            }


            ISQLContext sqlContext = new SQLContext(Session);
            var entity = new T();
            string sql = entity.GetQuerySql(@where);

            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);


            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";

            var reader = (DbDataReader)sqlContext.ExecuteQueryReader(sql, parasListData.ToArray());
            var entities = reader.ToObjects<T>().ToList();
            /*foreach (var data in entities)
            {
                data.ClearStack();
            }*/
            return new PageResult<T>(entities, total);
        }

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public PageResult<T> QueryPage(string @where, string order, int pageIndex, int pageSize)
        {
            return QueryPage(@where, null, order, pageIndex, pageSize);
        }

        public List<T> Query(string where, string order = "", params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(where, paras);
            return Query(scalarBuiler.Where, scalarBuiler.Paras, order);
        }

        public PageResult<T> QueryPage(string where, string order, int pageIndex, int pageSize, params object[] paras)
        {
            var scalarBuiler = WhereHelper.CreateScalarWhere(where, paras);
            return QueryPage(scalarBuiler.Where, scalarBuiler.Paras, order, pageIndex, pageSize);
        }

        public static DbCommand CreateSqlCommand(IOctDbCommand cmd)
        {
            DbCommand execCmd = cmd.GetDbCommand(x => x, () => new SqlCommand(),
                x =>
                {
                    if (x == null)
                    {
                        return new IDbDataParameter[0];
                    }
                    return x.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
                }
                    );
            return execCmd;
        }
    }
}
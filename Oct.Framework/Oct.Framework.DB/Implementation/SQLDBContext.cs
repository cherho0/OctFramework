using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Oct.Framework.DB.Implementation
{
    public class SQLDBContext<T> : IDBContext<T> where T : BaseEntity<T>, new()
    {
        public ISession Session { get; private set; }

        public SQLDBContext(ISession session)
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
            if (entity.IsIdentityPk)
            {
                object v = null;
                cmd.CommandText += "; SELECT @@IDENTITY ";
                var ds = Session.ExecCmd(cmd);
                if (ds.Tables.Count == 0)
                    return;

                if (ds.Tables[0].Rows.Count == 0)
                    return;

                v = ds.Tables[0].Rows[0][0];
                entity.SetIdentity(v);
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
        public void Update(T entity, string where = "", IDictionary<string, object> paras = null)
        {
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            DbCommand cmd = CreateSqlCommand(entity.GetUpdateCmd(where, paras));
            Session.AddCommands(cmd, entity);
        }

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            string cmdtext = entity.GetDelSQL();
            var cmd = new SqlCommand(cmdtext);
            Session.AddCommands(cmd);
        }

        public void Delete(object pk)
        {
            var entity = new T();
            string cmdtext = entity.GetDelSQL(pk);
            var cmd = new SqlCommand(cmdtext);
            Session.AddCommands(cmd);
        }

        public void Delete(string @where = "", IDictionary<string, object> paras = null)
        {
            if (SQLWordFilte.CheckKeyWord(where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var entity = new T();
            string cmdtext = entity.GetDelSQL(null, where);
            var cmd = new SqlCommand(cmdtext);
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    cmd.Parameters.Add(new SqlParameter(para.Key, para.Value));
                }
            }
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
            string sql = entity.GetModelSQL(pk);
            ISQLContext sqlContext = new SQLContext(Session);
            DataSet ds = sqlContext.ExecuteQuery(sql);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return entity.GetEntityFromDataRow(ds.Tables[0].Rows[0]);
        }

        /// <summary>
        /// 针对个别字段查询
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <param name="expression"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T GetModel<TP>(Expression<Func<T, TP>> expression, object pk)
        {
            List<string> props = new List<string>();
            MemberExpression body = expression.Body as MemberExpression;

            if (body == null)
            {
                NewExpression ubody = (NewExpression)expression.Body;
                var mn = ubody.Members;
                foreach (var m in mn)
                {
                    props.Add(m.Name);
                }
            }
            else
            {
                props.Add(body.Member.Name);
            }
            var entity = new T();
            string sql = entity.GetModelSQL(pk);
            var length = sql.IndexOf("from");
            var cols = string.Join(",", props);
            sql = string.Format("select {0} {1}", cols, sql.Remove(0, length));
            ISQLContext sqlContext = new SQLContext(Session);
            DataSet ds = sqlContext.ExecuteQuery(sql);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return entity.GetEntityFromDataRow(ds.Tables[0].Rows[0]);
        }

        /// <summary>
        ///     查询一系列实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> Query(string @where, IDictionary<string, object> paras, string order = "")
        {
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var entities = new List<T>();
            var entity = new T();
            string sql = entity.GetQuerySQL(@where);
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

            DataSet ds = sqlContext.ExecuteQuery(sql, paramters.ToArray());
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
            return entities;
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
        ///     查询一系列实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> Query<TP>(Expression<Func<T, TP>> expression, string @where, IDictionary<string, object> paras = null, string order = "")
        {
            List<string> props = new List<string>();
            MemberExpression body = expression.Body as MemberExpression;

            if (body == null)
            {
                NewExpression ubody = (NewExpression)expression.Body;
                var mn = ubody.Members;
                foreach (var m in mn)
                {
                    props.Add(m.Name);
                }
            }
            else
            {
                props.Add(body.Member.Name);
            }

            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var entities = new List<T>();
            var entity = new T();
            string sql = entity.GetQuerySQL(@where);
            var length = sql.IndexOf("from");
            var cols = string.Join(",", props);
            sql = string.Format("select {0} {1}", cols, sql.Remove(0, length));
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

            DataSet ds = sqlContext.ExecuteQuery(sql, paramters.ToArray());
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
            return entities;
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

            var entities = new List<T>();
            ISQLContext sqlContext = new SQLContext(Session);
            var entity = new T();
            string sql = entity.GetQuerySQL(@where);

            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            string rownumStr = ", ROW_NUMBER() OVER(ORDER BY " + order + ") rn";

            sql = sql.ToUpper().Replace("FROM", " {0} FROM ");
            sql = string.Format(sql, rownumStr);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";
            DataSet ds = sqlContext.ExecuteQuery(sql, parasListData.ToArray());

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
            return new PageResult<T>(entities, total);
        }

        public PageResult<T> QueryPage<TP>(Expression<Func<T, TP>> expression, string @where, IDictionary<string, object> paras, string order, int pageIndex, int pageSize)
        {

            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            List<string> props = new List<string>();
            MemberExpression body = expression.Body as MemberExpression;

            if (body == null)
            {
                NewExpression ubody = (NewExpression)expression.Body;
                var mn = ubody.Members;
                foreach (var m in mn)
                {
                    props.Add(m.Name);
                }
            }
            else
            {
                props.Add(body.Member.Name);
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

            var entities = new List<T>();
            ISQLContext sqlContext = new SQLContext(Session);
            var entity = new T();
            string sql = entity.GetQuerySQL(@where);
            var length = sql.IndexOf("from");
            var cols = string.Join(",", props);
            sql = string.Format("select {0} {1}", cols, sql.Remove(0, length));
            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            string rownumStr = ", ROW_NUMBER() OVER(ORDER BY " + order + ") rn";

            sql = sql.ToUpper().Replace("FROM", " {0} FROM ");
            sql = string.Format(sql, rownumStr);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";
            DataSet ds = sqlContext.ExecuteQuery(sql, parasListData.ToArray());

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
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

        private DbCommand CreateSqlCommand(IOctDbCommand cmd)
        {
            DbCommand execCmd = cmd.GetDbCommand(x => x, () => new SqlCommand(),
                x => x.Select(p => new SqlParameter(p.Key, p.Value)).ToArray());
            return execCmd;
        }

        public List<T> Query(Expression<Func<T, bool>> func)
        {
            var entity = new T();
            var entities = new List<T>();
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);

            var sql = entity.GetQuerySQL(h.SqlWhere);
            var paramters = h.Paras;

            ISQLContext sqlContext = new SQLContext(Session);
            var ds = sqlContext.ExecuteQuery(sql, paramters.ToArray());

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();

                entities.Add(newt.GetEntityFromDataRow(row));
            }

            return entities;

            return null;
        }

        public List<T> Query(Expression<Func<T, bool>> func, string order, int pageIndex, int pageSize, out int total)
        {
            var entity = new T();
            var entities = new List<T>();
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var paramters = h.Paras;
            var sql = entity.GetQuerySQL(h.SqlWhere);
            ISQLContext sqlContext = new SQLContext(Session);
            total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), h.GetParameters());

            int start = (pageIndex - 1) * pageSize;
            string rownumStr = ", ROW_NUMBER() OVER(ORDER BY " + order + ") rn";

            sql = sql.ToUpper().Replace("FROM", " {0} FROM ");
            sql = string.Format(sql, rownumStr);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";
            DataSet ds = sqlContext.ExecuteQuery(sql, paramters);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
            return entities;
        }
    }
}
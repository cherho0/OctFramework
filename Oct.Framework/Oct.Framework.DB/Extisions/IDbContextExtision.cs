using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Extisions
{
    public static class DbContextExtision
    {
        /// <summary>
        ///   查询部分字段   根据主键获取一个实体对象
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static T GetModel<T, TP>(this IDBContext<T> context, Expression<Func<T, TP>> expression, object pk) where T : BaseEntity<T>, new()
        {
            var props = ExpressionHelper.GetProps(expression);
            var entity = new T();
            string sql = entity.GetModelSQL(pk);
            var cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            ISQLContext sqlContext = new SQLContext(context.Session);

            var reader = sqlContext.ExecuteQueryReader(sql);
            reader.Read();
            entity = entity.GetEntityFromDataReader(reader);
            reader.Close();
            reader.Dispose();
            return entity;
        }

        public static List<T> QueryOrder<T, TP, TO>(this IDBContext<T> context, Expression<Func<T, TP>> expression, string where,
            IDictionary<string, object> paras, Expression<Func<TP, TO>> orderexpression) where T : BaseEntity<T>, new()
        {
            var props = ExpressionHelper.GetProps(expression);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }

            var entity = new T();
            string sql = entity.GetQuerySQL(@where);
            var cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            if (!order.IsNullOrEmpty())
            {
                sql += " order by " + order;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);
            var paramters = new List<SqlParameter>();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    paramters.Add(new SqlParameter(para.Key, para.Value));
                }
            }

            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            return DataReaderHelper.ReaderToList<T>(reader);
        }

        public static List<T> QueryOrder<T, TP, TO>(this IDBContext<T> context, Expression<Func<T, bool>> func, Expression<Func<T, TP>> expression, 
             Expression<Func<TP, TO>> orderexpression) where T : BaseEntity<T>, new()
        {
            var props = ExpressionHelper.GetProps(expression);
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            var paramters = h.Paras;
            var entity = new T();
            string sql = entity.GetQuerySQL("");
            var cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);

            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            if (!order.IsNullOrEmpty())
            {
                sql += " order by " + order;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);
            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            return DataReaderHelper.ReaderToList<T>(reader);
        }


        /// <summary>
        /// linq查询 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<T> QueryOrder<T, TO>(this IDBContext<T> context, Expression<Func<T, bool>> func,
            Expression<Func<T, TO>> orderexpression) where T : BaseEntity<T>, new()
        {
            var entity = new T();

            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            var sql = entity.GetQuerySQL("");
            sql = string.Format("select * from ( {0} ) Tab ", sql);
            var paramters = h.Paras;
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            if (!order.IsNullOrEmpty())
            {
                sql += " order by " + order;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);

            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return entities;

        }

        /// <summary>
        /// linq查询 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<T> Query<T>(this IDBContext<T> context, Expression<Func<T, bool>> func) where T : BaseEntity<T>, new()
        {
            var entity = new T();

            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var sql = entity.GetQuerySQL("");
            sql = string.Format("select * from ( {0} ) Tab ", sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            var paramters = h.Paras;
            ISQLContext sqlContext = new SQLContext(context.Session);
            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return entities;
        }

       

        /// <summary>
        /// linq查询 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<T> Query<T, TP>(this IDBContext<T> context, Expression<Func<T, TP>> expression,
            Expression<Func<T, bool>> func) where T : BaseEntity<T>, new()
        {
            var entity = new T();

            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var props = ExpressionHelper.GetProps(expression);
            var sql = entity.GetQuerySQL("");
            string cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            var paramters = h.Paras;
            ISQLContext sqlContext = new SQLContext(context.Session);

            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return entities;
        }

        /// <summary>
        /// linq查询 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<T> Query<T, TP, TO>(this IDBContext<T> context, Expression<Func<T, TP>> expression,
            Expression<Func<T, bool>> func, Expression<Func<TP, TO>> orderexpression) where T : BaseEntity<T>, new()
        {
            var entity = new T();

            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var props = ExpressionHelper.GetProps(expression);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            var sql = entity.GetQuerySQL("");
            string cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            var paramters = h.Paras;
            if (!order.IsNullOrEmpty())
            {
                sql += " order by " + order;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);
            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);

            return entities;
        }

        /// <summary>
        /// linq分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static PageResult<T> QueryPage<T, TO>(this IDBContext<T> context,
            Expression<Func<T, bool>> func, Expression<Func<T, TO>> orderexpression,
            int pageIndex, int pageSize) where T : BaseEntity<T>, new()
        {
            var entity = new T();
            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var paramters = h.Paras;
            var sql = entity.GetQuerySQL("");
            sql = string.Format("select * from ( {0} ) Tab ", sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);
            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), h.GetParameters());
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";

            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return new PageResult<T>(entities, total);

        }

        /// <summary>
        ///     查询分页数据-部分字段
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static PageResult<T> QueryPage<T, TP, TO>(this IDBContext<T> context, Expression<Func<T, TP>> expression, string where,
            IDictionary<string, object> paras,
            Expression<Func<TP, TO>> orderexpression, int pageIndex, int pageSize) where T : BaseEntity<T>, new()
        {
            if (SQLWordFilte.CheckKeyWord(@where))
            {
                throw new Exception("您提供的关键字有可能危害数据库，已阻止执行");
            }
            var props = ExpressionHelper.GetProps(expression);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
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
            ISQLContext sqlContext = new SQLContext(context.Session);
            var entity = new T();
            string sql = entity.GetQuerySQL(@where);
            var cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), parasList.ToArray());

            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";

            var reader = sqlContext.ExecuteQueryReader(sql, parasListData.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return new PageResult<T>(entities, total);
        }

        /// <summary>
        /// linq分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static PageResult<T> QueryPage<T, TP, TO>(this IDBContext<T> context, Expression<Func<T, TP>> expression,
            Expression<Func<T, bool>> func, Expression<Func<TP, TO>> orderexpression, int pageIndex, int pageSize)
            where T : BaseEntity<T>, new()
        {
            var entity = new T();

            var h = new ExpressionHelper();
            //解析表达式
            h.ResolveExpression(func);
            var paramters = h.Paras;
            var props = ExpressionHelper.GetProps(expression);
            var order = orderexpression == null ? "" : ExpressionHelper.GetProps(orderexpression)[0];
            var sql = entity.GetQuerySQL("");
            string cols = string.Join(",", props);
            sql = string.Format("select {0} from ( {1} ) Tab ", cols, sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            ISQLContext sqlContext = new SQLContext(context.Session);
            var total = sqlContext.GetResult<int>(string.Format("SELECT COUNT(1) FROM ({0}) a", sql), h.GetParameters());

            int start = (pageIndex - 1) * pageSize;
            var tempsql = "select *,ROW_NUMBER() OVER(ORDER BY " + order + ") rn from ({0}) a ";
            sql = string.Format(tempsql, sql);
            sql = "SELECT TOP " + pageSize + " * FROM (" + sql + ") query WHERE rn > " + start + " ORDER BY rn";
            var reader = sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities = DataReaderHelper.ReaderToList<T>(reader);
            return new PageResult<T>(entities, total);

        }

    }
}

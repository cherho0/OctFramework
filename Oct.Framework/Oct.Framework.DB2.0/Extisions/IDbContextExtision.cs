using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Composite;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;
using Oct.Framework.DB.LightDataAccess;

namespace Oct.Framework.DB.Extisions
{
    public static class DbContextExtision
    {

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
            var sql = entity.GetQuerySql("");
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
            var reader = (DbDataReader)sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            return reader.ToObjects<T>().ToList();

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
            var sql = entity.GetQuerySql("");
            sql = string.Format("select * from ( {0} ) Tab ", sql);
            if (!h.SqlWhere.IsNullOrEmpty())
            {
                sql += " where  " + h.SqlWhere;
            }
            var paramters = h.Paras;
            ISQLContext sqlContext = new SQLContext(context.Session);
            var reader = (DbDataReader)sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            return reader.ToObjects<T>().ToList();
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
            var sql = entity.GetQuerySql("");
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

            var reader = (DbDataReader)sqlContext.ExecuteQueryReader(sql, paramters.ToArray());
            var entities= reader.ToObjects<T>().ToList();
            return new PageResult<T>(entities, total);

        }
      
        public static ICompositeQuery AsCompositeQuery(this ISQLContext context)
        {
            ICompositeQuery query = new CompositeQuery(context);
            return query;
        }
    }
}

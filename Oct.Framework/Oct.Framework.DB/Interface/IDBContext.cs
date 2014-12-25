using Oct.Framework.DB.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Oct.Framework.DB.Core;

namespace Oct.Framework.DB.Interface
{
    public interface IDBContext<T> where T : BaseEntity<T>, new()
    {
        ISession Session { get; }

        /// <summary>
        ///     新增一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(T entity);

        /// <summary>
        ///     更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity, string where = "", IDictionary<string, object> paras = null);

        /// <summary>
        ///     删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        ///     根据主键删除一条数据  pk 可为null
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        void Delete(object pk);

        /// <summary>
        ///     根据主键删除一条数据  pk 可为null
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        void Delete(string where = "", IDictionary<string, object> paras = null);

        /// <summary>
        ///     根据主键获取一个实体对象
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        T GetModel(object pk);

        /// <summary>
        ///   查询部分字段   根据主键获取一个实体对象
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        T GetModel<TP>(Expression<Func<T, TP>> func, object pk);

        /// <summary>
        ///     通过where 查询一批数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> Query(string where, IDictionary<string, object> para, string order = "");

        /// <summary>
        ///     通过where 查询一批数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> Query(string where, string order = "");

        /// <summary>
        /// 查询部分字段并返回集合
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <param name="expression"></param>
        /// <param name="where"></param>
        /// <param name="paras"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        List<T> Query<TP>(Expression<Func<T, TP>> expression, string @where, IDictionary<string, object> paras,
            string order = "");

        /// <summary>
        ///     查询分页数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<T> QueryPage(string where, IDictionary<string, object> paras, string order, int pageIndex, int pageSize);

        /// <summary>
        ///     查询分页数据-部分字段
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<T> QueryPage<TP>(Expression<Func<T, TP>> expression, string where, IDictionary<string, object> paras, string order, int pageIndex, int pageSize);

        /// <summary>
        ///     查询分页数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<T> QueryPage(string where, string order, int pageIndex, int pageSize);

        /// <summary>
        /// linq查询 -- 只支持单表
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        [Obsolete]
        List<T> Query(Expression<Func<T, bool>> func);

        /// <summary>
        /// linq分页查询 -- 只支持单表
        /// </summary>
        /// <param name="func"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
         [Obsolete]
        List<T> Query(Expression<Func<T, bool>> func, string orderby, int pageIndex, int pageSize, out int total);
    }
}
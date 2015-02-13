using System.Collections.Generic;
using Oct.Framework.DB.Base;
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
        void Update(T entity);

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
        void Delete();

        /// <summary>
        ///     根据主键获取一个实体对象
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        T GetModel(object pk);

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
        ///     查询分页数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<T> QueryPage(string where, IDictionary<string, object> paras, string order, int pageIndex, int pageSize);

        /// <summary>
        ///     查询分页数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<T> QueryPage(string where, string order, int pageIndex, int pageSize);

        //匿名查询
      
        /// <summary>
        ///     通过where 匿名查询 where 条件使用 condition1=? and condition2=?
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> Query(string where, string order = "", params object[] paras);

        /// <summary>
        ///     查询分页数据  匿名查询 where 条件使用   condition1=? and condition2=?  1,2,3,4,5,
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        PageResult<T> QueryPage(string where, string order, int pageIndex, int pageSize,params object[] paras);
        
        }
}
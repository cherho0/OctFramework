using Oct.Framework.Entities;
using System;
using System.Collections.Generic;
using Oct.Framework.DB.Core;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Services
{
    public interface ICommonRoleActionService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(CommonRoleAction entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Modify(CommonRoleAction entity);

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(CommonRoleAction entity);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);

        /// <summary>
        /// 根据条件删除 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        bool Delete(string where, IDictionary<string, object> paras);

        /// <summary>
        /// 通过主键获取一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommonRoleAction GetModel(Guid id);

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        List<CommonRoleAction> GetModels(string where = "", IDictionary<string, object> paras = null, string order = "");

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="paras"> 参数键为@拼接的参数，值为参数值</param>
        /// <param name="total"></param>
        /// <returns></returns>
        PageResult<CommonRoleAction> GetModels(int pageIndex, int pageSize, string where, string order, IDictionary<string, object> paras);

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        List<CommonMenuActions> GetMenuActions(string where = "", IDictionary<string, object> paras = null, string order = "");

        bool Authorization(Guid roleId, Guid[] actions);

    }

    public partial class CommonRoleActionService : ICommonRoleActionService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(CommonRoleAction entity)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Add(entity);

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(CommonRoleAction entity)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Update(entity);

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(CommonRoleAction entity)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Delete(entity);

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Delete(id);

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据条件删除 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        public bool Delete(string @where, IDictionary<string, object> paras)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Delete(@where, paras);

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过主键获取一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonRoleAction GetModel(Guid id)
        {
            using (var context = new DbContext())
            {
                return context.CommonRoleActionContext.GetModel(id);
            }
        }

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        public List<CommonRoleAction> GetModels(string where = "", IDictionary<string, object> paras = null, string order = "")
        {
            using (var context = new DbContext())
            {
                return context.CommonRoleActionContext.Query(@where, paras);
            }
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="paras"> 参数键为@拼接的参数，值为参数值</param>
        /// <param name="total"></param>
        /// <returns></returns>
        public PageResult<CommonRoleAction> GetModels(int pageIndex, int pageSize, string @where, string order, IDictionary<string, object> paras)
        {
            using (var context = new DbContext())
            {
                return context.CommonRoleActionContext.QueryPage(where, paras, order, pageIndex, pageSize);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Oct.Framework.Entities;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Services
{
	public interface ICommonUserRoleService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(CommonUserRole entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Modify(CommonUserRole entity);

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(CommonUserRole entity);

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
        CommonUserRole GetModel(Guid id);

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        List<CommonUserRole> GetModels(string where = "", IDictionary<string, object> paras = null);

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
        List<CommonUserRole> GetModels(int pageIndex, int pageSize, string where, string order, IDictionary<string, object> paras, out int total);
    }

    public class CommonUserRoleService : ICommonUserRoleService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(CommonUserRole entity)
        {
            using (var context = new DbContext())
            {
		context.CommonUserRoleContext.Add(entity);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(CommonUserRole entity)
        {
            using (var context = new DbContext())
            {
		context.CommonUserRoleContext.Update(entity);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(CommonUserRole entity)
        {
            using (var context = new DbContext())
            {
		context.CommonUserRoleContext.Delete(entity);
                
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
		context.CommonUserRoleContext.Delete(id);
                
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
		context.CommonUserRoleContext.Delete(@where, paras);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过主键获取一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonUserRole GetModel(Guid id)
        {
            using (var context = new DbContext())
            {
                return context.CommonUserRoleContext.GetModel(id);
            }
        }

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        public List<CommonUserRole> GetModels(string @where, IDictionary<string, object> paras)
        {
            using (var context = new DbContext())
            {
                return context.CommonUserRoleContext.Query(@where, paras);
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
        public List<CommonUserRole> GetModels(int pageIndex, int pageSize, string @where, string order, IDictionary<string, object> paras, out int total)
        {
            using (var context = new DbContext())
            {
                return context.CommonUserRoleContext.QueryPage(where, paras, order, pageIndex, pageSize, out total);
            }
        }
    }
}

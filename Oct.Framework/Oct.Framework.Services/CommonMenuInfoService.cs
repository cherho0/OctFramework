using System;
using System.Collections.Generic;
using Oct.Framework.Entities;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Services
{
	public interface ICommonMenuInfoService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(CommonMenuInfo entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Modify(CommonMenuInfo entity);

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(CommonMenuInfo entity);

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
        CommonMenuInfo GetModel(Guid id);

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        List<CommonMenuInfo> GetModels(string where, IDictionary<string, object> paras);

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
        List<CommonMenuInfo> GetModels(int pageIndex, int pageSize, string where, string order, IDictionary<string, object> paras, out int total);
    }

    public class CommonMenuInfoService : ICommonMenuInfoService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(CommonMenuInfo entity)
        {
            using (var context = new DbContext())
            {
		context.CommonMenuInfoContext.Add(entity);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(CommonMenuInfo entity)
        {
            using (var context = new DbContext())
            {
		context.CommonMenuInfoContext.Update(entity);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据一个实体对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(CommonMenuInfo entity)
        {
            using (var context = new DbContext())
            {
		context.CommonMenuInfoContext.Delete(entity);
                
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
		context.CommonMenuInfoContext.Delete(id);
                
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
		context.CommonMenuInfoContext.Delete(@where, paras);
                
		return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过主键获取一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonMenuInfo GetModel(Guid id)
        {
            using (var context = new DbContext())
            {
                return context.CommonMenuInfoContext.GetModel(id);
            }
        }

        /// <summary>
        /// 通过条件获取对象 参数键为@拼接的参数，值为参数值
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paras">参数键为@拼接的参数，值为参数值</param>
        /// <returns></returns>
        public List<CommonMenuInfo> GetModels(string @where, IDictionary<string, object> paras)
        {
            using (var context = new DbContext())
            {
                return context.CommonMenuInfoContext.Query(@where, paras);
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
        public List<CommonMenuInfo> GetModels(int pageIndex, int pageSize, string @where, string order, IDictionary<string, object> paras, out int total)
        {
            using (var context = new DbContext())
            {
                return context.CommonMenuInfoContext.QueryPage(where, paras, order, pageIndex, pageSize, out total);
            }
        }
    }
}

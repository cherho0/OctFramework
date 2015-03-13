using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.DynamicObj;
using Oct.Framework.DB.Implementation;
using Oct.Framework.DB.Interface;

namespace Oct.Framework.DB.Extisions
{
    public static class IEntityExtision
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void Insert<T>(this T entity) where T : BaseEntity<T>, new()
        {
           var entitycontext = new EntityContext<T>(CurrentSessionFactory.GetCurrentSession());
            entitycontext.Add(entity);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void Update<T>(this T entity) where T : BaseEntity<T>, new()
        {
            var entitycontext = new EntityContext<T>(CurrentSessionFactory.GetCurrentSession());
            entitycontext.Update(entity);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void Delete<T>(this T entity) where T : BaseEntity<T>, new()
        {
            var entitycontext = new EntityContext<T>(CurrentSessionFactory.GetCurrentSession());
            entitycontext.Delete(entity);
        }
    }
}

using Oct.Framework.Entities;
using Oct.Framework.Entities.Entities;
using System;
using System.Collections.Generic;

namespace Oct.Framework.Services
{
    

    public partial class CommonUserService : BaseService, ICommonUserService
    {
        public void Authorization(Guid userId, Guid[] roles)
        {
            using (var context = new DbContext())
            {
                context.CommonUserRoleContext.Delete("userid=@userid",
                    new Dictionary<string, object>() { { "@userid", userId } });
                if (roles != null)
                {
                    foreach (var chkRole in roles)
                    {
                        context.CommonUserRoleContext.Add(new CommonUserRole()
                        {
                            CreateDate = DateTime.Now,
                            Id = Guid.NewGuid(),
                            ModifyDate = null,
                            RoleId = chkRole,
                            UserId = userId
                        });
                    }
                }
                context.SaveChanges();
            }

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="where"></param>
        /// <param name="para"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<CommonUserAcrions> GetUserActions(string where, Dictionary<string, object> para, string order)
        {
            using (var context = new DbContext())
            {
                return context.CommonUserAcrionsContext.Query(where, para, order);
            }
        }
    }
}

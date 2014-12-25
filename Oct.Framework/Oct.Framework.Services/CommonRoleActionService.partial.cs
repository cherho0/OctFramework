using System;
using System.Collections.Generic;
using Oct.Framework.Entities;
using Oct.Framework.Entities.Entities;

namespace Oct.Framework.Services
{
    public partial class CommonRoleActionService : ICommonRoleActionService
    {
        public List<CommonMenuActions> GetMenuActions(string @where = "", IDictionary<string, object> paras = null, string order = "")
        {
            using (var context = new DbContext())
            {
                return context.CommonMenuActionsContext.Query(@where, paras, order);
            }
        }

        public bool Authorization(Guid roleId, Guid[] actions)
        {
            using (var context = new DbContext())
            {
                context.CommonRoleActionContext.Delete("roleId=@roleID", new Dictionary<string, object>()
                {
                   { "@roleID",roleId}
                });
                if (actions != null)
                {
                    foreach (var action in actions)
                    {
                        context.CommonRoleActionContext.Add(new CommonRoleAction()
                        {
                            ActionId = action,
                            CreateDate = DateTime.Now,
                            Id = Guid.NewGuid(),
                            ModifyDate = null,
                            RoleId = roleId
                        });
                    }
                }

                return context.SaveChanges() > 0;
            }
        }
    }
}

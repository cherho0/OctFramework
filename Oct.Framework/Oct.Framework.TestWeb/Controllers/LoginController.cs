using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.MvcExt.User;
using Oct.Framework.Services;
using Oct.Framework.TestWeb.Areas.Premission.Models;

namespace Oct.Framework.TestWeb.Controllers
{
    public class LoginController : BaseController
    {
        [Dependency]
        public ICommonUserService CommonUserService { get; set; }
        //
        // GET: /Login/

        public ActionResult Login()
        {
            int toal;
          var user =  DbContext.CommonUserContext
              .QueryPage(p => new {p.UserName,p.Account},"",null,"id",1,1,out toal);

            return View();
        }

        [HttpPost]
        public ActionResult Login(string acc)
        {
            var users = CommonUserService.GetModels("Account=@Account", new Dictionary<string, object>()
            {
                {"@Account",acc}
            });
            if (users.Count > 0)
            {
                var user = users[0];
                LoginHelper.Instance.Login(() =>
                {
                    return new UserBase()
                    {
                        Account = user.Account,
                        Avatar = user.Avatar,
                        IpAddress = Kernel.GetIP(),
                        Sex = user.Gander ?? 0,
                        UserName = user.UserName
                    };
                });
                //添加权限
                var userActions = CommonUserService.GetUserActions("ur.userId=@userId", new Dictionary<string, object>()
            {
                {"@userId",user.Id}
            }, " m.Sort,a.sort");

                LoginHelper.Instance.CacheRoles(() =>
                {
                    var userroles = new UserRoles();
                    userActions.ForEach(p =>
                        userroles.AddRole(p.Url, p.Name, p.Name));
                    return userroles;
                });

                LoginHelper.Instance.CacheMeuns(() =>
                {
                    var usermenu = new UserMenus();
                    userActions.ForEach(p =>
                    {
                        if (p.IsVisible)
                        usermenu.Add(p.MenuName, p.Name, p.Url);
                    }
                        
                       );
                    return usermenu;
                });

                return RedirectToAction("index", "home");
            }
            return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            LoginHelper.Instance.LogOut();
            return RedirectToAction("Login");
        }
    }
}

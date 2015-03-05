using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Session;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Extisions;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.MvcExt.User;
using Oct.Framework.Services;
using Oct.Framework.TestWeb.Code;

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
            Test.ax = 0;
            Test.ay = 0;
            Test.la = 2.1;
            Test.bx = 3.1;
            Test.by = 0;
            Test.lb = 3.4;
            Test.cx = 2.2;
            Test.cy = 5.4;
            Test.lc = 3.1;
          var p =  Test.Get();
            var ss11 = CacheHelper.GetAll<string>("priceModels");
            //var models = Kernel.GetEnumModels<OperationEnum>(1,2);

            //拼参数demo
            string where = "";
            var paras = new List<object>();
            if (false)
            {
                where += " id=?";
                paras.Add(1);
            }
            if (true)
            {
                where += " dd like ?";
                paras.Add("%的%");
            }

            // var ret = DbContext.TestTsContext.Query(where, "", paras.ToArray());
            IDictionary<string, object> dic = new Dictionary<string, object>() { { "@dd", 1 } };

            var vss = DbContext.SQLContext.ExecuteQuery("select * from testts where id=@dd  ", dic);

            //var json = Kernel.SerializeObject(vss.Models);

            // var ok11 = CacheHelper.Set("test", vss.Models);
            //var v = CacheHelper.Get<IEnumerable<ExpandoObject>>("test");
            // var ds = Kernel.DeserializeObject<DataSet>(v);
            // var allkeys = CacheHelper.GetAll<string>("test");
            // CacheHelper.RemoveAll("test");
            // var v1 = CacheHelper.Get("test");
            /*Stopwatch sw = new Stopwatch();
            sw.Start();
            //var models = DbContext.TestTsContext.Query("");
            //10w 数据 417毫秒。0.4 秒
            sw.Stop();

            //普通linq 查询
            var temp1 = DbContext.CommonActionInfoContext.Query(p => p.IsVisible);

            //普通分页查询
            var ss = DbContext.CommonUserContext.QueryPage("", "id", 1, 10);

            //查询某对象的某些字段
            var user = DbContext.CommonUserContext.GetModel(p => p.UserName, "57D5A932-584F-4632-929F-284D0E4165F4");

            //符合查询某些字段
            var us5 = DbContext.CommonMenuActionsContext.Query(p => p.MenuName, p => p.IsVisible == true);

            //排序查询某些字段
            var us = DbContext.CommonUserContext.QueryOrder(p => new { p.UserName, p.Account }, "", null, p => p.UserName);

            //分页查询某些字段
            var us1 = DbContext.CommonUserContext.QueryPage(p => new { p.UserName, p.Account }, "", null, p => p.UserName, 1, 10);

            //linq 拼where
            var us2 = DbContext.CommonUserContext.Query(p => p.Account == "czy");

            //linq 复合查询
            var us3 = DbContext.CommonUserAcrionsContext.Query(p => p.IsVisible == true);

            //linq 排序查询
            var us4 = DbContext.CommonUserAcrionsContext.QueryOrder(p => p.IsVisible == true, p => p.Sort);

            //linq 排序查询某些字段
            var us7 = DbContext.CommonUserAcrionsContext.QueryOrder(p => p.IsVisible == true, p => new { p.Name }, p => p.Name);

            //linq 分页查询
            var us6 = DbContext.CommonMenuActionsContext.QueryPage(p => p.IsVisible == true, p => p.Name, 1, 10);

            //linq 分页查询某些字段
            var us8 = DbContext.CommonMenuActionsContext.QueryPage(p => new { p.Name }, p => p.IsVisible == true, p => p.Name, 1, 10);

            //linq 分页查询 条件判断
            var us9 = DbContext.CommonUserAcrionsContext.QueryPage(p => p.Name != "", p => p.Name, 1, 10);
            */
            //var pr = DbContext.SQLContext.ExecutePageExpandoObjects("select * from testts", null, "id", 1, 10);

            /*foreach (dynamic model in pr.Models)
            {
                var id = model.Id;
                var name = model.DD;

                var sss = "22222";
            }
            */
            var ok1 = CacheHelper.Set("test", "test", 1);
            var bv = CacheHelper.Get<string>("test");
            var sessionprovide = Bootstrapper.GetRepository<ISessionProvider>();
            var get = sessionprovide.GetSession<string>("ceshi");
            var ss = false;
            if (get == null)
            {
                var ok = sessionprovide.AddSession("ceshi", DateTime.Now.ToString());
                ss = ok;
                get = sessionprovide.GetSession<string>("ceshi");
            }

            DbContext.SaveChanges();
            Console.WriteLine(ss);
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

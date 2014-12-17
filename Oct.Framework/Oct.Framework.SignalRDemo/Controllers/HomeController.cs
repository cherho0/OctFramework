using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Json;
using Oct.Framework.SignalRDemo.Common;
using Oct.Framework.SignalRDemo.Models;
using Oct.Framework.SignalRDemo.signalr;

namespace Oct.Framework.SignalRDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public static List<LoginModel> AllUser = new List<LoginModel>(); 

        public ActionResult Index()
        {
            var model = CookieHelper.GetCookieValue("user");
            if (string.IsNullOrEmpty(model))
            {
                return RedirectToAction("Login");
            }
            ViewBag.cur = model;
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            ViewBag.AllUsers = cache.Get("user");//
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //  用户存入cookie
            CookieHelper.SetCookie("user", model.Account);
            //  用户存在session
            //SessionMgr.Instance.SetSession(SessionMgr.UserKey, model);
            //  用户存在缓存里
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            AllUser.Add(model);
            var flag = cache.Set("user", AllUser);
            //  推送给客户端
            var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
            var allUser = JsonHelper.SerializeObject(AllUser);
            context.Clients.All.allUsers(allUser);
            return RedirectToAction("Index");
        }



        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit(string account)
        {
            var model = new LoginModel();
            model.Account = account;
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            var allDic = (List<LoginModel>)cache.Get("user");//   获取缓存中的大集合
            foreach (var item in allDic)
            {
                if (item.Account == account)
                {
                    allDic.Remove(item);
                    break;
                }
            }
            cache.Set("user", allDic);
            //  推送
            var context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
            var allUser = JsonHelper.SerializeObject(AllUser);
            context.Clients.All.allUsers(allUser);
            //  清除cookie
            CookieHelper.ClearCookie("user");

            //SessionMgr.Instance.Remove(SessionMgr.UserKey);
            return Content("<script type='text/javascript'>alert('退出成功');window.location.href='/Home/Login'</script>");
        }
    }
}

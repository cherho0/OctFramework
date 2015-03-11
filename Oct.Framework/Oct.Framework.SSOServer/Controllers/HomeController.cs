using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.SSO.Entity;

namespace Oct.Framework.SSOServer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ICacheHelper _cacheHelper = new RedisHelper();
            var token = CookieHelper.GetCookieValue("ssotoken");
            if (!token.IsNullOrEmpty())
            {
                var ssoUser = _cacheHelper.Get<SsoUser>("sso_" + token);
                ViewBag.ssouser = ssoUser;
            }
            return View();
        }

    }
}

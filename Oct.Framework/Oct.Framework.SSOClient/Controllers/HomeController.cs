using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.SSO.Client;
using Oct.Framework.SSO.Filter;

namespace Oct.Framework.SSOClient.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [SsoAuthFilter]
        public ActionResult Index()
        {
            var user = CookieHelper.GetCookieValue(SsoClient.UserName);
            ViewBag.user = user;
            return View();
        }
    }
}

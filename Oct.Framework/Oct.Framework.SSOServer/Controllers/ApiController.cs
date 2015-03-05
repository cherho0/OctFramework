using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oct.Framework.Core.Cache;
using Oct.Framework.SSO.Entity;

namespace Oct.Framework.SSOServer.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public JsonResult GetUser(string token)
        {
            ICacheHelper _cacheHelper = new RedisHelper();
            var ssoUser = _cacheHelper.Get<SsoUser>("sso_" + token);
            return Json(ssoUser, JsonRequestBehavior.AllowGet);
        }

    }
}


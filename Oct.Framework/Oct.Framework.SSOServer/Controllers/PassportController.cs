using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.SSO.Entity;
using Oct.Framework.SSOServer.Models;

namespace Oct.Framework.SSOServer.Controllers
{
    public class PassportController : Controller
    {
        ICacheHelper _cacheHelper = new RedisHelper();

        //
        // GET: /Passport/
        public ActionResult Login(string returnurl)
        {
            var token = CookieHelper.GetCookieValue("ssotoken");
            if (!token.IsNullOrEmpty())
            {
                var ssoUser = _cacheHelper.Get<SsoUser>("sso_" + token);
                if (ssoUser != null)
                {
                    var ret = BuildUrl(returnurl, "token", token);
                    return Redirect(ret);
                }
            }
            ViewBag.returnurl = returnurl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnurl)
        {
            ViewBag.returnurl = returnurl;
            //todo 验证


            var token = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            var ssouser = new SsoUser() { token = token, username = model.Acc };
            _cacheHelper.Set("sso_" + token, ssouser);
            CookieHelper.SetCookie("ssotoken", token);
            if (!returnurl.IsNullOrEmpty())
            {
                var ret = BuildUrl(returnurl, "token", token);
                return Redirect(ret);
            }
            return RedirectToAction("Index", "Home");
        }

        public string BuildUrl(string url, string paramText, string paramValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", paramText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            //_url = reg1.Replace(_url, "");
            if (_url.IndexOf("?") == -1)
                _url += string.Format("?{0}={1}", paramText, paramValue);//?
            else
                _url += string.Format("&{0}={1}", paramText, paramValue);//&
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }
    }
}

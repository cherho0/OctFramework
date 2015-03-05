using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Oct.Framework.Core.ApiData;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.Json;
using Oct.Framework.SSO.Client;
using Oct.Framework.SSO.Entity;

namespace Oct.Framework.SSO.Filter
{
    public class SsoAuthFilter : AuthorizeAttribute
    {
        private const string loginurl = "Passport/Login";
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //todo 获取用户信息,获取到了直接返回,这里可以替换使用应用程序中的数据
            var userName = CookieHelper.GetCookieValue(SsoClient.UserName);
            if (!userName.IsNullOrEmpty())
            {
                return;
            }

            var user = SsoClient.Instance.GetUser();
            if (user == null)
            {
                //跳转到sso登录页面
                var ssoserverLogin = ConfigSettingHelper.GetAppStr("ssoserver") + loginurl + "?returnurl=" + filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri;
                filterContext.Result = new RedirectResult(ssoserverLogin);
                return;
            }

            CookieHelper.SetCookie(SsoClient.Token, user.token);
            CookieHelper.SetCookie(SsoClient.UserName, user.username);
            FormsAuthentication.SetAuthCookie(user.username, true);
            filterContext.Result = new RedirectResult(filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
        }
    }
}

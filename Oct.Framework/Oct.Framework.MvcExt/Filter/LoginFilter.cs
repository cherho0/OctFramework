using System;
using System.Web.Mvc;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Filter
{
    /// <summary>
    /// 登录验证过滤器
    /// </summary>
    public class LoginFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");

            var hasuser = LoginHelper.Instance.IsLogin();

            if (!hasuser)   
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
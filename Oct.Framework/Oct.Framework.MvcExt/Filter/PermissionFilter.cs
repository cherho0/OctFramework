using System;
using System.Web.Mvc;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Filter
{
    public class PermissionFilter : AuthorizeAttribute
    {
        /// <summary>
        /// 权限验证过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");

            var roles = LoginHelper.Instance.GetLoginUserRoles();

            if (roles == null)
            {
                throw new Exception("没有权限，限制登录！！");
            }

            //验证action 权限

            //权限验证通过了，记录view日志

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string area = filterContext.RouteData.DataTokens["area"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            var ok = roles.CheckRole(controllerName, actionName,area);
            if (!ok)
            {
                throw new Exception("没有权限，限制登录！！");
            }
        }

    }
}

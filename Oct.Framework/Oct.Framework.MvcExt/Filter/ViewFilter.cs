using System;
using System.Web.Mvc;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.Core.Session;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Filter
{
    /// <summary>
    /// 浏览追踪过滤器
    /// </summary>
    public class ViewFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");
            var sessionProvider = Bootstrapper.GetRepository<ISessionProvider>();
            var currentUser = LoginHelper.Instance.GetLoginUser<UserBase>();

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            var url = "/" + controllerName.ToLower() + "/" + actionName.ToLower();
            var ip = GetIP();
            var vLog = new ViewLog();
            vLog.ViewType = "Web";
            vLog.FromIp = ip;
            vLog.Action = actionName;
            vLog.Controller = controllerName;
            if (currentUser != null)
            {
                vLog.OpertingUser = currentUser.Account;
            }
            else
            {
                vLog.OpertingUser = ip;
            }
            
            vLog.CreateDate = DateTime.Now;
            vLog.Name = controllerName + "-" + actionName;
            vLog.Operation = filterContext.RequestContext.HttpContext.Request.HttpMethod;
            vLog.Para = filterContext.RequestContext.HttpContext.Request.Url.ToString();
            var idbLog = Bootstrapper.GetRepository<IDbLog>();
            if (idbLog != null)
            {
                idbLog.AddViewLog(vLog);
            }
        }

        private string GetIP()
        {
            return IpHelper.GetIP();
        }
    }
}



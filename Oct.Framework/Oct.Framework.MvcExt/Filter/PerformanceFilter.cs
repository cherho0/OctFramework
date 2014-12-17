using System;
using System.Diagnostics;
using System.Web.Mvc;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.Core.Session;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Filter
{
    /// <summary>
    /// 性能追踪过滤器
    /// </summary>
    public class PerformanceFilterAttribute : ActionFilterAttribute
    {
        private PerformanceLog log;
        private Stopwatch sw;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            sw = new Stopwatch();
            sw.Start();
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            log = new PerformanceLog();
            log.ActionName = actionName;
            log.ControllerName = controllerName;
            log.IP = IpHelper.GetIP();

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            sw.Stop();
            log.MillionSeconds = sw.ElapsedMilliseconds;

            var idbLog = Bootstrapper.GetRepository<IDbLog>();
            if (idbLog != null)
            {
                idbLog.AddPerformanceLog(log);
            }
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.Core.Session;
using Oct.Framework.Entities;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.MvcExt.Result;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Base
{
    public class BaseController : Controller
    {
        [Dependency]
        protected ICacheHelper CacheHelper { get; set; }

        [Dependency]
        protected ISessionProvider SessionProvider { get; set; }

        private UserBase _currentUser;

        /// <summary>
        /// 获取当前用户
        /// </summary>
        protected UserBase CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = LoginHelper.Instance.GetLoginUser<UserBase>();
                }

                return _currentUser;
            }
        }

        protected IKernel Kernel { get; private set; }

        public BaseController()
        {
            Kernel = new Kernel(this);
        }

        protected DbContext DbContext;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            DbContext = new DbContext();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            DbContext.Dispose();
        }

        /// <summary>
        /// 返回xml对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        protected XmlResult Xml(string xml)
        {
            return new XmlResult(xml);
        }

        /// <summary>
        /// 返回xml对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        protected XmlResult Xml(object xml)
        {
            return new XmlResult(xml);
        }
    }
}

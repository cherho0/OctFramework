using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.Core.Session;
using Oct.Framework.Entities;
using Oct.Framework.MvcExt.Filter;
using Oct.Framework.MvcExt.Result;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Base
{
    public class BaseApiController : ApiController
    {
        [Dependency]
        protected ICacheHelper CacheHelper { get; set; }

        [Dependency]
        protected ISessionProvider SessionProvider { get; set; }

        protected DbContext DbContext;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DbContext = new DbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MongoDB.Bson.Serialization;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.Services;
using Oct.Framework.Services.TestTsService;
using Unity.Mvc4;

namespace Oct.Framework.MvcExt
{
    public static class OctGlobal
    {
        public static void InitIOC()
        {
            BsonClassMap.RegisterClassMap<DataChangeLog>();
            BsonClassMap.RegisterClassMap<ViewLog>();
            BsonClassMap.RegisterClassMap<PerformanceLog>();
            IUnityContainer container = Bootstrapper.Initialise();

            container.RegisterType<ITestTsService, TestTsService>();
            container.RegisterType<ITest, Test>();

            container.RegisterType<ICommonActionInfoService, CommonActionInfoService>();
            container.RegisterType<ICommonMenuInfoService, CommonMenuInfoService>();
            container.RegisterType<ICommonRoleActionService, CommonRoleActionService>();
            container.RegisterType<ICommonUserRoleService, CommonUserRoleService>();
            container.RegisterType<ICommonUserService, CommonUserService>();
            container.RegisterType<ICommonRoleInfoService, CommonRoleInfoService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
           // GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

    }
}

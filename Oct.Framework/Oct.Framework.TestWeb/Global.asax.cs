using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Lucene.Net.Documents;
using Oct.Framework.Core;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Log;
using Oct.Framework.DB;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt;
using Oct.Framework.MvcExt.User;
using Oct.Framework.SearchEngine;
using Oct.Framework.TestWeb.Areas.Premission.Models;
using Oct.Framework.TestWeb.Models;

namespace Oct.Framework.TestWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static string AppID { get; set; }

        protected void Application_Start()
        {
            AppID = Guid.NewGuid().ToString();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            OctGlobal.InitIOC(); //Bootstrapper.Initialise();
            CreateMapper();
            CreateSreachIndex();
        }

        public void Session_Start()
        {
            
        }

        public void Session_End()
        {
            LoginHelper.Instance.LogOut();
        }

        public static SearchEngineTasks SreachTask;

        private void CreateSreachIndex()
        {
            SreachTask = new SearchEngineTasks();
            SreachTask.AddWork<TestTs>("TestIndex", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestIndex"), DoWorkStyle.PerHour, (t, d) =>
            {
                d.Add(new Field("Id", t.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                d.Add(new Field("DD", t.DD, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
            });
            //SreachTask.Start();
            //SreachTask.Do("TestIndex");
            //SreachTask.DoUnitUpdate("TestIndex", "Id", "1");
        }

        private void CreateMapper()
        {
            Mapper.CreateMap<TestTs, TestTsDTO>();
            Mapper.CreateMap<TestTsDTO, TestTs>();

            Mapper.CreateMap<CommonActionInfo, CommonActionInfoDTO>();
            Mapper.CreateMap<CommonActionInfoDTO, CommonActionInfo>();

            Mapper.CreateMap<CommonMenuInfoDTO, CommonMenuInfo>();
            Mapper.CreateMap<CommonMenuInfo, CommonMenuInfoDTO>();

            Mapper.CreateMap<CommonRoleActionDTO, CommonRoleAction>();
            Mapper.CreateMap<CommonRoleAction, CommonRoleActionDTO>();

            Mapper.CreateMap<CommonRoleInfoDTO, CommonRoleInfo>();
            Mapper.CreateMap<CommonRoleInfo, CommonRoleInfoDTO>();

            Mapper.CreateMap<CommonUserDTO, CommonUser>();
            Mapper.CreateMap<CommonUser, CommonUserDTO>();

            Mapper.CreateMap<CommonUserRoleDTO, CommonUserRole>();
            Mapper.CreateMap<CommonUserRole, CommonUserRoleDTO>();
        }

        
    }
}
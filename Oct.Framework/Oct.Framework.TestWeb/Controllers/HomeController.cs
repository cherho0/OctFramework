using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Entities;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.MvcExt.Filter;
using Oct.Framework.MvcExt.User;
using Oct.Framework.SearchEngine;
using Oct.Framework.TestWeb.Models;

namespace Oct.Framework.TestWeb.Controllers
{
    [LoginFilter]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            var u = LoginHelper.Instance.GetLoginUser<UserBase>();

            /*var user = new CurrentUser
            {
                Account = "Ny",
                Address = "sz",
                Avatar = "jpg",
                IpAddress = IpHelper.GetIP(),
                Sex = 1,
                UserName = "Ny"
            };
            LoginHelper.Instance.Login(() => user);*/


            /*var sw = new Stopwatch();
            sw.Start();
            var test1 = Bootstrapper.GetRepository<ITest>();
            sw.Stop();
           var t1= sw.ElapsedMilliseconds;
            sw.Restart();
            var test2 = Bootstrapper.GetRepository<ITest>();
            sw.Stop();
            var t2 = sw.ElapsedMilliseconds;
            test1.SetID(100);

            var id = test2.Id;

            var umsg = new UcUserMsg()
            {
                Id = Guid.NewGuid(),
                UserId = "admin",
                Title = "我我哦我我我我1212",
                Message = "我我哦我我我我1212",
                CreateDate = DateTime.Now,
                IsRead = false,
                FromUser = "admin"
            };
            var sessionProvider = Bootstrapper.GetRepository<ISessionProvider>();
            sessionProvider.AddSession("cao", umsg);

            var cao = sessionProvider.GetSession<UcUserMsg>("cao");

           /* string testint = "1";
            bool ss = testint.IsNullOrEmpty();



           // ICacheHelper cache = Bootstrapper.GetRepository<ICacheHelper>();
            var ret = cache.Set("11", "ssss");
            var sss = cache.Get<string>("11");*/

            /*var dbcontext = new DbContext();
            var e = new TestTs() {DD ="ssss"};
            dbcontext.TestTsContext.Add(e);
           //  dbcontext.TestTsContext.Delete();
            var id = e.Id;
            dbcontext.SaveChanges();
            dbcontext.Dispose();*/

            /*  var ds = dbcontext.SQLContext.ExecuteQuery("select * from testts");
               var tt = Encoding.Default.GetString((byte[]) ds.Tables[0].Rows[0]["ts"]
                   );
               var ss =long.Parse(tt);*/
            /*
           
             LogsHelper.Debug("tetetetet");
           
            

            var member = Bootstrapper.GetRepository<ICacheHelper>();
            var a = member.Set("aaa", umsg);
            var b = member.Get<UcUserMsg>("aaa");
            ICacheHelper cache = Bootstrapper.GetRepository<ICacheHelper>();    
            var ret = cache.Set("11", "ssss");
            var sss = cache.Get<string>("11");*/



            using (DbContext context = new DbContext())
            {
                //context.UserMsgContext.Add(umsg);
                //context.UserMsgContext.Update(umsg);
                var model = new UcUserMsg();
                model.ActiveLogDataChange("admin");
                model.Id = Guid.NewGuid();
                model.Message = "我是杀死后四十岁后四十1111";
                context.UserMsgContext.Update(model);
                // context.UserMsgContext.Delete("26E57601-DAFA-4206-A72E-CD1B3E7EB364");
                // context.UserMsgContext.Query("1=1");
                int total;
                //context.UserMsgContext.QueryPage("1=1", "id", 1, 20, out total);

                /*  using (DbContext context = new DbContext())
                  {
                       //context.UserMsgContext.Add(umsg);
                      //context.UserMsgContext.Update(umsg);
                      var model = new UcUserMsg();
                      model.ActiveLogDataChange("admin");
                      model.Id = Guid.Parse("C155FF8A-90CE-4E26-89CF-D79048CB37DB");
                      model.Message = "我是杀死后四十岁后四十1111";
                      context.UserMsgContext.Update(model);
                      // context.UserMsgContext.Delete("26E57601-DAFA-4206-A72E-CD1B3E7EB364");
                      // context.UserMsgContext.Query("1=1");
                      int total;
                      //context.UserMsgContext.QueryPage("1=1", "id", 1, 20, out total);


                      //所有操作执行完成，调用一个保存，全部工作单元保存
                      // context.SaveChanges();
                      //var models = context.UserMsgContext.QueryPage("", "id", 1, 5,out total );
                      //dataGridView1.DataSource = models;

                      //context.UserMsgContext.Add(umsg);
                     // context.UserMsgContext.Add(umsg);


                     context.SaveChanges();
                 }

                      context.SaveChanges();*/
            }
            ViewBag.s = "我是八爪鱼快乐的一员测试分词";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string segment,int? p)
        {

            if (segment.IsNullOrEmpty())
            {
                return Index();
            }
            var page = p ?? 1;
            var wapper = new SearchEngineWapper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestIndex"));
            wapper.AddKey("DD");
            wapper.AddPreviewKey("DD");
            wapper.Search(segment,(page-1)*10,10 );
            var sret = wapper.Result;
            var rets = segment.Segment();
            var ret = string.Join(";", rets);
            ViewBag.s = segment;
            ViewBag.sr = ret;
            ViewBag.sret = sret;
            ViewBag.Cost = wapper.Cost.ToString();
           
            var pm = Kernel.CreatePageModel(10,page,wapper.Total);
            ViewBag.Pm = pm;
            return View();
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public ActionResult Demo()
        {
            var model = new DemoDTO() {EnumDDL = "721c3846-e4bd-4cc9-8f75-00080f30918a",DemoTime = DateTime.Now.ToYYYYMMDDString()};
            return View(model);
        }
    }
}
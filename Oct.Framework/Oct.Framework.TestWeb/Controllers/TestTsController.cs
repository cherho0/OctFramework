using AutoMapper;
using Microsoft.Practices.Unity;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.Services.TestTsService;
using Oct.Framework.TestWeb.Models;
using System.Web.Mvc;

namespace Oct.Framework.TestWeb.Controllers
{
    public class TestTsController : BaseController
    {
        [Dependency]
        public ITestTsService TestTsService { get; set; }

        public ActionResult Index(int? page)
        {
            var p = page ?? 1;
            var total = 0;
            var models = this.TestTsService.GetModels(p, 5, string.Empty, "id", null, out total);
            var pm = Kernel.CreatePageModel(5, p, total);
            this.ViewBag.PM = pm;

            return this.View(models);
        }

        public ActionResult Details(int id)
        {
            var entity = this.TestTsService.GetModel(id);
            var model = Mapper.Map<TestTsDTO>(entity);

            return this.View(model);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(TestTsDTO model)
        {
            try
            {
                var entity = Mapper.Map<TestTs>(model);

                this.TestTsService.Add(entity);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Edit(int id)
        {
            var entity = this.TestTsService.GetModel(id);
            var model = Mapper.Map<TestTsDTO>(entity);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(TestTsDTO model)
        {
            try
            {
                var entity = Mapper.Map<TestTs>(model);

                this.TestTsService.Modify(entity);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Delete(int id)
        {
            this.TestTsService.Delete(id);

            return this.RedirectToAction("Index");
        }
    }
}

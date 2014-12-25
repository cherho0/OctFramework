using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.Services;
using Oct.Framework.TestWeb.Areas.Premission.Models;
using Oct.Framework.TestWeb.Models;

namespace Oct.Framework.TestWeb.Areas.Premission.Controllers
{
	public class CommonUserRoleController : BaseController
    {
        [Dependency]
        public ICommonUserRoleService CommonUserRoleService { get; set; }

        public ActionResult Index(int? page)
        {
            var p = page ?? 1;
            var total = 0;
            var models = this.CommonUserRoleService.GetModels(p, 5, string.Empty, "CreateDate", null);
            var pm = this.Kernel.CreatePageModel(5, p, models.TotalCount);
            var dtos = Mapper.Map<List<CommonUserRoleDTO>>(models.Models);
            this.ViewBag.PM = pm;
            if (dtos == null)
            {dtos = new List<CommonUserRoleDTO>();
                
            }
            return this.View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            var entity = this.CommonUserRoleService.GetModel(id);
            var model = Mapper.Map<CommonUserRoleDTO>(entity);

            return this.View(model);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(CommonUserRoleDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonUserRole>(model);

                this.CommonUserRoleService.Add(entity);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var entity = this.CommonUserRoleService.GetModel(id);
            var model = Mapper.Map<CommonUserRoleDTO>(entity);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonUserRoleDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonUserRole>(model);

                this.CommonUserRoleService.Modify(entity);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            this.CommonUserRoleService.Delete(id);

            return this.RedirectToAction("Index");
        }
    }
}
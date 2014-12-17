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
    public class CommonRoleActionController : BaseController
    {
        [Dependency]
        public ICommonRoleActionService CommonRoleActionService { get; set; }

        public ActionResult Index(int? page)
        {
            int p = page ?? 1;
            int total = 0;
            List<CommonRoleAction> models = CommonRoleActionService.GetModels(p, 5, string.Empty, "CreateDate", null,
                out total);
            PageModel pm = Kernel.CreatePageModel(5, p, total);
            var dtos = Mapper.Map<List<CommonRoleActionDTO>>(models);
            ViewBag.PM = pm;
            if (dtos == null)
            {
                dtos = new List<CommonRoleActionDTO>();

            }
            return View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            CommonRoleAction entity = CommonRoleActionService.GetModel(id);
            var model = Mapper.Map<CommonRoleActionDTO>(entity);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CommonRoleActionDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonRoleAction>(model);

                CommonRoleActionService.Add(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("jump");
            }
        }

        public ActionResult Edit(Guid id)
        {
            CommonRoleAction entity = CommonRoleActionService.GetModel(id);
            var model = Mapper.Map<CommonRoleActionDTO>(entity);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonRoleActionDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonRoleAction>(model);

                CommonRoleActionService.Modify(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("jump");
            }
        }

        public ActionResult Delete(Guid id)
        {
            CommonRoleActionService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
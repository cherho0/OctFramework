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
    public class CommonMenuInfoController : BaseController
    {
        [Dependency]
        public ICommonMenuInfoService CommonMenuInfoService { get; set; }

        public ActionResult Index(int? page)
        {
            int p = page ?? 1;
            var models = CommonMenuInfoService.GetModels(p, 5, string.Empty, "CreateDate", null);
            PageModel pm = Kernel.CreatePageModel(5, p, models.TotalCount);
            var dtos = Mapper.Map<List<CommonMenuInfoDTO>>(models.Models);
            ViewBag.PM = pm;
            if (dtos == null)
            {
                dtos = new List<CommonMenuInfoDTO>();

            }
            return View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            CommonMenuInfo entity = CommonMenuInfoService.GetModel(id);
            var model = Mapper.Map<CommonMenuInfoDTO>(entity);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CommonMenuInfoDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonMenuInfo>(model);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = null;
                CommonMenuInfoService.Add(entity);

                return View("jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            CommonMenuInfo entity = CommonMenuInfoService.GetModel(id);
            var model = Mapper.Map<CommonMenuInfoDTO>(entity);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonMenuInfoDTO model)
        {
            try
            {
                var entity = CommonMenuInfoService.GetModel(model.Id);
                entity.Name = model.Name;
                entity.IsAllowAnonymousAccess = model.IsAllowAnonymousAccess;
                entity.IsEnable = model.IsEnable;
                entity.Sort = model.Sort;
                entity.ModifyDate = DateTime.Now;
                CommonMenuInfoService.Modify(entity);

                return View("jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            CommonMenuInfoService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
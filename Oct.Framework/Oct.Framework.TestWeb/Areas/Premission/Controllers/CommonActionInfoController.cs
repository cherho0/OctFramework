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
    public class CommonActionInfoController : BaseController
    {
        [Dependency]
        public ICommonActionInfoService CommonActionInfoService { get; set; }

        public ActionResult Index(int? page, Guid modelId)
        {
            int p = page ?? 1;
            int total = 0;
            List<CommonActionInfo> models = CommonActionInfoService.GetModels(p, 5, " MenuId=@MenuId ", "CreateDate", new Dictionary<string, object>()
            {
                 {"@MenuId",modelId}
            },
                out total);

            var dtos = Mapper.Map<List<CommonActionInfoDTO>>(models);
            if (dtos == null)
            {
                dtos = new List<CommonActionInfoDTO>();
            }
            PageModel pm = Kernel.CreatePageModel(5, p, total);
            ViewBag.modelId = modelId;
            ViewBag.PM = pm;
            return View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            CommonActionInfo entity = CommonActionInfoService.GetModel(id);
            var model = Mapper.Map<CommonActionInfoDTO>(entity);

            return View(model);
        }

        public ActionResult Create(Guid modelId)
        {
            var model = new CommonActionInfoDTO();

            model.MenuId = modelId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CommonActionInfoDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonActionInfo>(model);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = null;
                CommonActionInfoService.Add(entity);

                return View("jump");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(Guid id)
        {
            CommonActionInfo entity = CommonActionInfoService.GetModel(id);
            var model = Mapper.Map<CommonActionInfoDTO>(entity);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonActionInfoDTO model)
        {
            try
            {
                var entity = CommonActionInfoService.GetModel(model.Id);
                entity.ModifyDate = DateTime.Now;
                entity.CategoryName = model.CategoryName;
                entity.IsEnable = model.IsEnable;
                entity.IsLog = model.IsLog;
                entity.IsVisible = model.IsVisible;
                entity.Name = model.Name;
                entity.Operation =(int) model.Operation;
                entity.Sort = model.Sort;
                entity.Url = model.Url;

                CommonActionInfoService.Modify(entity);

                return View("jump");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(Guid id, Guid modelId)
        {
            CommonActionInfoService.Delete(id);

            return RedirectToAction("Index", new { modelId });
        }
    }
}
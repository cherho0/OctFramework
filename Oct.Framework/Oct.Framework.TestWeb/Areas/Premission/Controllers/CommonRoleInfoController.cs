using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CommonRoleInfoController : BaseController
    {
        [Dependency]
        public ICommonRoleInfoService CommonRoleInfoService { get; set; }

        [Dependency]
        public ICommonActionInfoService CommonActionInfoService { get; set; }

        [Dependency]
        public ICommonMenuInfoService CommonMenuInfoService { get; set; }

        [Dependency]
        public ICommonRoleActionService CommonRoleActionService { get; set; }

        public ActionResult Index(int? page)
        {
            int p = page ?? 1;
            int total = 0;
            var models = CommonRoleInfoService.GetModels(p, 5, string.Empty, "CreateDate", null);
            PageModel pm = Kernel.CreatePageModel(5, p, models.TotalCount);
            var dtos = Mapper.Map<List<CommonRoleInfoDTO>>(models.Models);
            ViewBag.PM = pm;
            if (dtos == null)
            {
                dtos = new List<CommonRoleInfoDTO>();

            }
            return View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            CommonRoleInfo entity = CommonRoleInfoService.GetModel(id);
            var model = Mapper.Map<CommonRoleInfoDTO>(entity);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CommonRoleInfoDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonRoleInfo>(model);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = null;
                CommonRoleInfoService.Add(entity);

                return View("jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            CommonRoleInfo entity = CommonRoleInfoService.GetModel(id);
            var model = Mapper.Map<CommonRoleInfoDTO>(entity);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonRoleInfoDTO model)
        {
            try
            {
                var entity = CommonRoleInfoService.GetModel(model.Id);
                entity.Code = model.Code;
                entity.IsEnable = model.IsEnable;
                entity.IsSysDefault = model.IsSysDefault;
                entity.ModifyDate = DateTime.Now;
                entity.Name = model.Name;
                CommonRoleInfoService.Modify(entity);

                return View("jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            CommonRoleInfoService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Authorization(Guid roleid)
        {
            //CommonRoleInfo entity = CommonRoleInfoService.GetModel(roleid);
           // var model = Mapper.Map<CommonRoleInfoDTO>(entity);
            var actionRoles = CommonRoleActionService.GetMenuActions("", null, " a.Menuid,a.CategoryName,a.Sort");
            var roles = CommonRoleActionService.GetModels("RoleId=@RoleId", new Dictionary<string, object>()
            {
                {
                    "@RoleId",roleid
                }
            });
            ViewBag.roleId = roleid;
            ViewBag.actionRoles = actionRoles;
            ViewBag.roles = roles;
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(Guid roleId, Guid[] actions)
        {
            CommonRoleActionService.Authorization(roleId,actions);
            return RedirectToAction("Authorization", new { roleid  = roleId});
        }
    }
}
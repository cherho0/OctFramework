using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;
using Oct.Framework.Entities.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.Extisions;
using Oct.Framework.MvcExt.Filter;
using Oct.Framework.Services;
using Oct.Framework.TestWeb.Areas.Premission.Models;
using Oct.Framework.TestWeb.Models;

namespace Oct.Framework.TestWeb.Areas.Premission.Controllers
{
    public class CommonUserController : BaseController
    {
        [Dependency]
        public ICommonUserService CommonUserService { get; set; }

        [Dependency]
        public ICommonRoleInfoService CommonRoleInfoService { get; set; }

        [Dependency]
        public ICommonUserRoleService CommonUserRoleService { get; set; }

        public ActionResult Index(int? page)
        {
            int p = page ?? 1;
            int total = 0;
            List<CommonUser> models = CommonUserService.GetModels(p, 5, string.Empty, "CreateDate", null, out total);
            PageModel pm = Kernel.CreatePageModel(5, p, total);
            var dtos = Mapper.Map<List<CommonUserDTO>>(models);
            if (dtos == null)
            {
                dtos = new List<CommonUserDTO>();
            }
            ViewBag.PM = pm;

            return View(dtos);
        }

        public ActionResult Details(Guid id)
        {
            CommonUser entity = CommonUserService.GetModel(id);
            var model = Mapper.Map<CommonUserDTO>(entity);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CommonUserDTO model)
        {
            try
            {
                var entity = Mapper.Map<CommonUser>(model);
                var id = Guid.NewGuid();
                entity.Id = id;
                entity.CreateUserId = id;
                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = null;
                entity.LastLoginDate = null;
                CommonUserService.Add(entity);

                return View("Jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            CommonUser entity = CommonUserService.GetModel(id);
            var model = Mapper.Map<CommonUserDTO>(entity);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CommonUserDTO model)
        {
            try
            {
                var entity = CommonUserService.GetModel(model.Id);
                entity.Status = model.Status;
                entity.Address = model.Address;
                entity.Email = model.Email;
                entity.Fax = model.Fax;
                entity.Gander = model.Gander;
                entity.IDNumber = model.IDNumber;
                entity.Mobile = model.Mobile;
                entity.Phone = model.Phone;
                entity.QQ = model.QQ;
                entity.UserName = model.UserName;
                entity.ModifyUserId = model.Id;
                entity.ModifyDate = DateTime.Now;
                CommonUserService.Modify(entity);

                return View("Jump");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            CommonUserService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Authorization(Guid userId)
        {
            ViewBag.userId = userId;
            var roles = CommonRoleInfoService.GetModels();
            var urs = CommonUserRoleService.GetModels("userid=@userid",new Dictionary<string, object>()
            {
                {"@userid" , userId}
            });
            ViewBag.roles = roles;
            ViewBag.urs = urs;
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(Guid userId, Guid[] roles)
        {
            CommonUserService.Authorization(userId, roles);
            return RedirectToAction("Authorization", new { userId });
        }
    }
}
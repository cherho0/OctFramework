using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Oct.Framework.Core.Common;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Core;

namespace Oct.Framework.TestWeb.Areas.CodeGen.Controllers
{
    public class GenController : Controller
    {
        private const string dllName = "Oct.Framework.Entities";
        //
        // GET: /CodeGen/Gen/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChoseEntity(string type)
        {
            var dll = Assembly.Load(dllName);
            var clses = dll.DefinedTypes;
            ViewBag.Ens = clses.Where(p => !p.IsSubclassOf(typeof(DBContextBase))).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Name
            });
            return View();
        }

        [HttpPost]
        public ActionResult BindProp(string[] entity)
        {
            var dll = Assembly.Load(dllName);
            var clses = dll.DefinedTypes;
            var cls = clses.Where(p => entity.Contains(p.Name));
            var props = cls.SelectMany(p => p.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance));
            var showprop =
                props.Where(p => !IgonProps.Contains(p.Name)).Select(p => new { p.Name, Type = p.PropertyType.ToString() });
            ViewBag.props = showprop.ToExpando();
            ViewBag.Ens = clses.Where(p => !p.IsSubclassOf(typeof(DBContextBase))).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Name
            });
            return View();
        }

        private List<string> IgonProps = new List<string>()
        {
            "PkValue","ChangedProps","PkName","IsIdentityPk","Props","TableName"
        };

        public JsonResult TreeJson()
        {
            var json =
                new[]
                {
                    new
                    {
                        text = "控件",
                        value="root",
                        children = new []{ 
                            new
                            {
                             text = "列表",
                            value="table",
                            },
                            new
                            {
                             text = "表单",
                            value="form",
                            },
                            new
                            {
                             text = "下拉列表",
                            value="ddl",
                            }
                        }
                    },
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}

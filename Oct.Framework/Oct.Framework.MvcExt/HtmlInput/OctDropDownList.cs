using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oct.Framework.Core.IOC;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Interface;
using Oct.Framework.Entities;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class OctDropDownList
    {
        private static Dictionary<string, IList<SelectListItem>> enumCache = new Dictionary<string, IList<SelectListItem>>();

        public static HtmlString DropDownListEnumFor<TM, TP, TK>(this HtmlHelper<TM> helper, Expression<Func<TM, TP>> expression,
           object htmlattributes = null, bool addall = false, string val = "") where TK : struct
        {
            return helper.DropDownListFor(expression, GetEnumValuesAndDsc<TK>(addall, val), htmlattributes);
        }

        public static HtmlString DropDownListEnum<K>(this HtmlHelper helper, string name,
          object htmlattributes = null, bool addall = false, string val = "") where K : struct
        {
            return helper.DropDownList(name, GetEnumValuesAndDsc<K>(addall, val), htmlattributes);
        }

        public static IList<SelectListItem> GetEnumValuesAndDsc<T>(bool addall = true, string val = "") where T : struct
        {
            var typename = typeof(T).ToString();
            IList<SelectListItem> items = new List<SelectListItem>();
            if (!enumCache.ContainsKey(typename))
            {
                var types = typeof(T);
                var ms = types.GetFields();
                foreach (FieldInfo fieldInfo in ms)
                {
                    try
                    {
                        DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                        if (attr == null)
                        {
                            continue;
                        }
                        var txt = attr.Description;
                        var v = ((int)Enum.Parse(types, fieldInfo.Name)).ToString();
                        var item = new SelectListItem()
                        {
                            Text = txt,
                            Value = v,
                            Selected = val.Equals(v)
                        };
                        items.Add(item);


                    }
                    catch (Exception)
                    {
                    }
                }
                enumCache.Add(typename, items);
            }

            items = enumCache[typename];

            if (addall && !items.Any(p => p.Value == "-999"))
            {
                var defitem = new SelectListItem()
                {
                    Text = "全部",
                    Value = "-999"
                };
                items.Insert(0, defitem);
            }
            return items;
        }

        public static IEnumerable<SelectListItem> GetSelectListItemsFromEnum<T>(this HtmlHelper helper, bool addall = false, string val = "") where T : struct
        {
            return GetEnumValuesAndDsc<T>(addall, val);
        }

        /// <summary>
        /// 通过类型获取全部结果集生成的listitem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="where"></param>
        /// <param name="select"></param>
        /// <param name="addall"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetSelectListItems<T>(this HtmlHelper helper,
          string where,
            Func<T, SelectListItem> select, bool addall = false) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where).Select(select).ToList();
            if (addall)
            {
                data.Insert(0, new SelectListItem { Text = "-全部-", Value = string.Empty });
            }
            return data;
        }

        public static HtmlString DropDownListModel<T>(this HtmlHelper helper, string name,
         string where,
            Func<T, SelectListItem> select, object htmlattributes = null, bool addall = false) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where).Select(select).ToList();
            if (addall)
            {
                data.Insert(0, new SelectListItem { Text = "-全部-", Value = string.Empty });
            }
            return helper.DropDownList(name, data, htmlattributes);
        }

        public static HtmlString DropDownListModelFor<T1, T2, TM>(this HtmlHelper<T1> helper, Expression<Func<T1, T2>> expression,
        string where,
           Func<TM, SelectListItem> select, object htmlattributes = null, bool addall = false) where TM : BaseEntity<TM>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<TM>();
            var data = repo.Query(where).Select(select).ToList();
            if (addall)
            {
                data.Insert(0, new SelectListItem { Text = "-全部-", Value = string.Empty });
            }
            return helper.DropDownListFor(expression, data, htmlattributes);
        }
    }
}

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

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class EnumDropDownList
    {
        private static Dictionary<string, IList<SelectListItem>> enumCache = new Dictionary<string, IList<SelectListItem>>();

        public static HtmlString DropDownListForEnum<TM, TP, TK>(this HtmlHelper<TM> helper, Expression<Func<TM, TP>> expression,
           object htmlattributes = null, bool addall = false, string val = "") where TK : struct
        {
            return helper.DropDownListFor(expression, GetEnumValuesAndDsc<TK>(addall, val), htmlattributes);
        }

        public static HtmlString DropDownListForEnum<K>(this HtmlHelper helper, string name,
          object htmlattributes = null, bool addall = false, string val = "") where K : struct
        {
            return helper.DropDownList(name, GetEnumValuesAndDsc<K>(addall, val), htmlattributes);
        }

        public static IList<SelectListItem> GetEnumValuesAndDsc<T>(bool addall = true, string val = "") where T : struct
        {
            var typename = typeof (T).ToString();
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

            if (addall && !items.Any(p=>p.Value == "-999"))
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
    }
}

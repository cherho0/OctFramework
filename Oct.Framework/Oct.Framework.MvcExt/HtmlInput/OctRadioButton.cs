using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Oct.Framework.DB.Base;
using Oct.Framework.Entities;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class OctRadioButton
    {
        public static MvcHtmlString RadioButtonList<T>(this HtmlHelper helper, string id, string name,
           Func<T, SelectListItem> select, object defaultValue = null, string where = "", string order = "", object htmlAttributes = null, bool isHorizon = true) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where, order).Select(select).ToList();
            if (htmlAttributes == null)
            {
                htmlAttributes = new { };
            }
            return RadioButtonList(helper, id, name, data, defaultValue, htmlAttributes, isHorizon);
        }

        public static MvcHtmlString RadioButtonListFor<TM, TY, T>(this HtmlHelper helper, Expression<Func<TM, TY>> expression,
           Func<T, SelectListItem> select, string where = "", string order = "", object htmlAttributes = null, bool isHorizon = true) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where, order).Select(select).ToList();
            if (htmlAttributes == null)
            {
                htmlAttributes = new { };
            }
            string id = "";
            string name = "";
            object value = null;
            var body = expression.Body as MemberExpression;
            if (body != null)
            {
                var me = body;
                name = me.Member.Name;
                id = name;
                value = helper.ViewData.ModelMetadata.Properties.FirstOrDefault(p => p.PropertyName == name).Model;
            }

            return RadioButtonList(helper, id, name, data, value, htmlAttributes, isHorizon);
        }


        /// <summary>
        /// 生成复选框列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <param name="defaultValue"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isHorizon">是否水平布局</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string id, string name,
            IEnumerable<SelectListItem> selectList, object defaultValue = null, object htmlAttributes = null, bool isHorizon = true)
        {
            if (htmlAttributes == null)
            {
                htmlAttributes = new { };
            }
            var htmlAttributesDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var list = new List<SelectListItem>();
            var checkBoxListHtml = new StringBuilder();

            htmlAttributesDict.Add("type", "radio");
            htmlAttributesDict.Add("name", name);
            htmlAttributesDict.Add("style", "border:none;");

            foreach (var item in selectList)
            {
                item.Selected = item.Value.Equals(defaultValue);
                list.Add(item);
            }

            foreach (var item in list)
            {
                var newHtmlAttributes = htmlAttributesDict.DeepCopy();
                newHtmlAttributes.Add("value", item.Value);
                newHtmlAttributes.Add("id", id + item.Text);
                if (item.Selected)
                    newHtmlAttributes.Add("checked", "checked");

                var tagBuilder = new TagBuilder("input");
                tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);

                var inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                var containerFormat = isHorizon ? @"<label> {0}  {1}</label>" : @"<p><label> {0}  {1}</label></p>";

                checkBoxListHtml.AppendFormat(containerFormat, inputAllHtml, item.Text);
            }
            return MvcHtmlString.Create(checkBoxListHtml.ToString());
        }
    }
}

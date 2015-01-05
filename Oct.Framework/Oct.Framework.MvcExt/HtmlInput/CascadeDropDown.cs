using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oct.Framework.Core.Common;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public class CascadeDropDown
    {
        /// <summary>
        /// 控件id 和name
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 获取json
        /// </summary>
        public string Data_jsonurl { get; set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// json中text对应的字段
        /// </summary>
        public string data_textName { get; set; }
        /// <summary>
        /// json中value对应的字段
        /// </summary>
        public string data_valueName { get; set; }
        /// <summary>
        /// select默认显示的内容
        /// </summary>

        public string data_defaulttext { get; set; }
        /// <summary>
        /// select默认显示内容时候的值
        /// </summary>
        public string data_defaultvalue { get; set; }
        /// <summary>
        /// 请求json参数key
        /// </summary>
        public string data_paramKey { get; set; }
        /// <summary>
        /// 关联控件id
        /// </summary>
        public string data_accordingto { get; set; }
    }

    public static class CascadeDropDowns
    {
        public static MvcHtmlString CascadeDropDownLists(this HtmlHelper helper, string firstName, IEnumerable<SelectListItem> items, params CascadeDropDown[] drops)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"J_MultiLevelSelect\">");
            sb.Append(helper.DropDownList(firstName, items, new { @class = "input-sm" }));
            foreach (var cascadeDropDown in drops)
            {
                sb.Append(" <select class=\"input-sm " + (cascadeDropDown.Required ? "required" : "") + "\" ");
                sb.Append("  id=\"" + cascadeDropDown.Id + "\" ");
                sb.Append("  name=\"" + cascadeDropDown.Id + "\"");
                sb.Append("  data-jsonurl = \"" + cascadeDropDown.Data_jsonurl + "\"");
                sb.Append("  data-textName=\"" + cascadeDropDown.data_textName + "\"");
                sb.Append("  data-valueName=\"" + cascadeDropDown.data_valueName + "\"");
                if (!cascadeDropDown.data_defaulttext.IsNullOrEmpty())
                {
                    sb.Append("  data-defaulttext=\"" + cascadeDropDown.data_defaulttext + "\"");                    
                }
                if (!cascadeDropDown.data_defaultvalue.IsNullOrEmpty())
                {
                    sb.Append("  data-defaultvalue=\"" + cascadeDropDown.data_defaultvalue + "\"");
                }
                sb.Append("  data-paramKey=\"" + cascadeDropDown.data_paramKey + "\"");
                sb.Append("  data-accordingto=\"#" + cascadeDropDown.data_accordingto + "\">");
                sb.Append(" </select>");
            }
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}

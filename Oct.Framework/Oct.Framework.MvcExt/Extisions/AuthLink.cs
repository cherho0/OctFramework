using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Oct.Framework.Core.Common;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Extisions
{
    public static class AuthLink
    {
        public static HtmlString AuthedLink(this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName,
             string para = "",
            string @class = "",
            string attr = ""
            )
        {
            var area = helper.ViewContext.RouteData.DataTokens["area"].ToString();
            var enablePermission = ConfigSettingHelper.GetAppStr<bool>("EnablePermission");
            if (enablePermission)
            {
                var roles = LoginHelper.Instance.GetLoginUserRoles();
                if (roles == null)
                {
                    return new HtmlString("");
                }
                if (!roles.CheckRole(controllerName, actionName, area))
                {
                    return new HtmlString("");
                }
            }
            var d = new RouteValueDictionary();
            d.Add("area", area);
           

            UrlHelper urlhelp = new UrlHelper(helper.ViewContext.RequestContext);
            var rurl = urlhelp.Action(actionName, controllerName,d) + "?" + para;
            return new HtmlString("<a class='" + @class + "' " + attr + " href='" + rurl + "'>" + linkText + "</a>");
        }

        /// <summary>
        /// 根据权限生成超链接，当没有权限返回空
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="class"></param>
        /// <param name="htmlAttrbuites"></param>
        /// <param name="routeAttributes"></param>
        /// <returns></returns>
        public static HtmlString AuthedLink(this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName,
            string @class,
            object htmlAttrbuites,
            object routeAttributes)
        {
            var enablePermission = ConfigSettingHelper.GetAppStr<bool>("EnablePermission");
            var area = helper.ViewContext.RouteData.DataTokens["area"].ToString();
            if (enablePermission)
            {
                var roles = LoginHelper.Instance.GetLoginUserRoles();
                if (roles == null)
                {
                    return new HtmlString("");
                }
               
                if (!roles.CheckRole(controllerName, actionName, area))
                {
                    return new HtmlString("");
                }
            }
            TagBuilder tag = new TagBuilder("a");

            tag.AddCssClass(@class);
            if (htmlAttrbuites != null)
            {
                var ats = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttrbuites);
                foreach (var at in ats)
                {
                    tag.Attributes.Add(at.Key, at.Value.ToString());
                }

            }
            var d = new RouteValueDictionary();
            d.Add("area", area);
            if (routeAttributes != null)
            {
                var atts = HtmlHelper.AnonymousObjectToHtmlAttributes(routeAttributes);
                foreach (var att in atts)
                {
                    d.Add(att.Key, att.Value);
                }
            }
            UrlHelper urlhelp = new UrlHelper(helper.ViewContext.RequestContext);
            tag.Attributes.Add("href", urlhelp.Action(actionName, controllerName, d));
            tag.InnerHtml = linkText;
            return new HtmlString(tag.ToString());
        }


        public static HtmlString AuthedLink(this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName
            )
        {
            return AuthedLink(helper, linkText, actionName, controllerName, "", new { }, new { });
        }

        public static HtmlString AuthedLink(this HtmlHelper helper,
           string linkText,
           string actionName,
           string controllerName,
            string @class
           )
        {
            return AuthedLink(helper, linkText, actionName, controllerName, @class, new { }, new { });
        }


        public static HtmlString AuthedLink(this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName,
            string @class,
            object routeAttributes
            )
        {
            return AuthedLink(helper, linkText, actionName, controllerName, @class, new { }, routeAttributes);
        }

        public static HtmlString AuthedLink(this HtmlHelper helper,
            string linkText,
            string actionName,
            string controllerName,
            object routeAttributes
            )
        {
            return AuthedLink(helper, linkText, actionName, controllerName, "", new { }, routeAttributes);
        }

        public static HtmlString GenRouteUrl(this HtmlHelper helper, string url, object routeAttributes = null)
        {
            var strs = url.Split(new char[]{'/'},StringSplitOptions.RemoveEmptyEntries);
            var a = strs[strs.Length - 1];
            var c = strs[strs.Length - 2];
            var area = "";
            if (strs.Length == 3)
            {
                area = strs[0];
            }
            var d = new RouteValueDictionary();
            d.Add("area", area);
            if (routeAttributes != null)
            {
                var atts = HtmlHelper.AnonymousObjectToHtmlAttributes(routeAttributes);
                foreach (var att in atts)
                {
                    d.Add(att.Key, att.Value);
                }
            }
            UrlHelper urlhelp = new UrlHelper(helper.ViewContext.RequestContext);
            return new HtmlString(urlhelp.Action(a, c, d));
        }
    }
}

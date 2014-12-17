using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Oct.Framework.MvcExt.Extisions
{
    public static class PagerHelper
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="htmlAttributes">分页头标签属性</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static HtmlString Pager(this HtmlHelper helper, string pageName, int currentPageIndex, int pageSize, int recordCount, object htmlAttributes, string className, PageMode mode)
        {
            //TagBuilder builder = new TagBuilder("table");
            //builder.IdAttributeDotReplacement = "_";
            //builder.GenerateId(id);
            //builder.AddCssClass(className);
            //builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //builder.InnerHtml = GetNormalPage(currentPageIndex, pageSize, recordCount, mode);
            return new HtmlString(GetNormalPage(pageName, currentPageIndex, pageSize, recordCount, mode));
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="className">分页样式</param>
        /// <returns></returns>
        public static HtmlString Pager(this HtmlHelper helper, string pageName, int currentPageIndex, int pageSize, int recordCount, string className)
        {
            return Pager(helper, pageName, currentPageIndex, pageSize, recordCount, null, className, PageMode.Normal);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public static HtmlString Pager(this HtmlHelper helper, string pageName, int currentPageIndex, int pageSize, int recordCount)
        {
            return Pager(helper, pageName, currentPageIndex, pageSize, recordCount, null);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static HtmlString Pager(this HtmlHelper helper, string pageName, int currentPageIndex, int pageSize, int recordCount, PageMode mode)
        {
            return Pager(helper, pageName, currentPageIndex, pageSize, recordCount, null, mode);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static HtmlString Pager(this HtmlHelper helper, string pageName, int currentPageIndex, int pageSize, int recordCount, string className, PageMode mode)
        {
            return Pager(helper, pageName, currentPageIndex, pageSize, recordCount, null, className, mode);
        }

        /// <summary>
        /// 获取普通分页
        /// </summary>
        /// <param name="pageName">分页参数名</param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        private static string GetNormalPage(string pageName, int currentPageIndex, int pageSize, int recordCount, PageMode mode)
        {
            int pageCount = (recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1);
            StringBuilder url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.AbsolutePath + "?" + pageName + "={0}");
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != pageName)
                    url.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"row\">");
            sb.AppendFormat("<div class=\"col-sm-3\"><span class=\"btn blue disabled\">总共{0}条记录,共{1}页,当前第{2}页</span></div>", recordCount, pageCount, currentPageIndex);
            sb.Append("<div class=\"col-sm-9\"><ul class=\"pagination\">");
            if (currentPageIndex == 1)
                sb.Append("<li class=\"disabled\"><a href=\"javascript:return false;\">首页</a></li>");
            else
            {
                string url1 = string.Format(url.ToString(), 1);
                sb.AppendFormat("<li><a href={0}>首页</a></li>", url1);
            }
            if (currentPageIndex > 1)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex - 1);
                sb.AppendFormat("<li class=\"prev\"><a href={0}>上一页</a></li>", url1);
            }
            else
                sb.Append("<li class=\"prev disabled\"><a href=\"javascript:return false;\">上一页</a></li>");
            if (mode == PageMode.Numeric)
                sb.Append(GetNumericPage(currentPageIndex, pageSize, recordCount, pageCount, url.ToString()));
            if (currentPageIndex < pageCount)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex + 1);
                sb.AppendFormat("<li class=\"next\"><a href={0}>下一页</a></li>", url1);
            }
            else
                sb.Append("<li class=\"next disabled\"><a href=\"javascript:return false;\">下一页</a></li>");

            if (currentPageIndex == pageCount || pageCount == 0)
                sb.Append("<li class=\"disabled\"><a href=\"javascript:return false;\">末页</a></li>");
            else
            {
                string url1 = string.Format(url.ToString(), pageCount);
                sb.AppendFormat("<li><a href={0}>末页</a></li>", url1);
            }
            sb.Append("</ul></div>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取数字分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetNumericPage(int currentPageIndex, int pageSize, int recordCount, int pageCount, string url)
        {
            int k = currentPageIndex / 10;
            int m = currentPageIndex % 10;
            StringBuilder sb = new StringBuilder();
            if (currentPageIndex / 10 == pageCount / 10)
            {
                if (m == 0)
                {
                    k--;
                    m = 10;
                }
                else
                    m = pageCount % 10;
            }
            else
                m = 10;
            for (int i = k * 10 + 1; i <= k * 10 + m; i++)
            {
                if (i == currentPageIndex)
                    sb.AppendFormat("<li class=\"disabled\"><a href=\"javascript:return false;\">{0}</a></li>", i);
                else
                {
                    string url1 = string.Format(url.ToString(), i);
                    sb.AppendFormat("<li><a href={0}>{1}</a></li>", url1, i);
                }
            }

            return sb.ToString();
        }
    }
    /// <summary>
    /// 分页模式
    /// </summary>
    public enum PageMode
    {
        /// <summary>
        /// 普通分页模式
        /// </summary>
        Normal,
        /// <summary>
        /// 普通分页加数字分页
        /// </summary>
        Numeric
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Oct.Framework.Core.Common;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class BootstrapTemplate
    {

        public static MvcHtmlString Submit(this HtmlHelper helper, string text)
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn blue input-sm\" id=\"SubmitBtn\">&nbsp;" + text + "</button>");
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string text, string onclick = "")
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn blue input-sm\" id=\"SubmitBtn\" " +
                (onclick.IsNullOrEmpty() ? "" : "onclick=\"" + onclick + "\"")
                + ">&nbsp" + text + "</button>");
        }

        public static SreachDiv BeginBootstrapSreachDiv(this HtmlHelper helper)
        {
            var sreachdiv = new SreachDiv(helper.ViewContext);
            return sreachdiv;
        }
    }

    public class SreachDiv : IDisposable
    {
        private ViewContext _context;



        public SreachDiv(ViewContext context)
        {
            _context = context;
            context.Writer.Write("<div class=\"row\">");
            context.Writer.Write("<div class=\"col-xs-12\">");
            context.Writer.Write(" <div class=\"well well-sm form\">");
            context.Writer.Write(" <div class=\"clearfix form-body search-wrapper\">");


        }

        public void Dispose()
        {
            _context.Writer.Write("</div></div></div></div>");
        }
    }
}

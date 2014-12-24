using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class BootstrapTemplate
    {
        public static MvcHtmlString Submit(this HtmlHelper helper)
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn blue input-sm\" id=\"SubmitBtn\">&nbsp;搜  索</button>");
        }

        public static MvcHtmlString Button(this HtmlHelper helper)
        {
            return MvcHtmlString.Create("<button type=\"submit\" class=\"btn blue input-sm\" id=\"SubmitBtn\">&nbsp;搜  索</button>");
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
            var sb = new StringBuilder();
            sb.Append("");
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

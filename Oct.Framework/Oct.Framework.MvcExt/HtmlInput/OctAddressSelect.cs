using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class OctAddressSelect
    {
        public static MvcHtmlString AddressSelect(this HtmlHelper helper, string proId = "proId", string cityId = "cityId", string areaId = "areaId", string pro = "", string city = "", string area="")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<select name='" + proId + "' id='" + proId + "' class='input-sm'></select>");
            sb.Append("<select name='" + cityId + "' id='" + cityId + "' class='input-sm'></select>");
            sb.Append("<select name='" + areaId + "' id='" + areaId + "' class='input-sm'></select>");
            sb.Append("  <script type='text/javascript' src='/assets/octopus/js/address.js'> </script> ");
            sb.Append("  <script type='text/javascript'>  ");
            sb.Append(" addressInit('" + proId + "', '" + cityId + "', '" + areaId + "');  ");
            sb.Append(" </script>  ");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}

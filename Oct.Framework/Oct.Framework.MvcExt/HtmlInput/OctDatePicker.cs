using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oct.Framework.DB.Base;
using Oct.Framework.Entities;

namespace Oct.Framework.MvcExt.HtmlInput
{
    public static class OctDatePicker
    {
        public static MvcHtmlString DatePickerFor<TM, Tp>(this HtmlHelper<TM> helper, Expression<Func<TM, Tp>> expression
          )
        {
            return helper.TextBoxFor(expression, new { @class = "date-picker input-sm", data_date_format = "yyyy-mm-dd"});
        }

      /*  public static MvcHtmlString DateTimePickerFor<TM, Tp>(this HtmlHelper<TM> helper, Expression<Func<TM, Tp>> expression
         )
        {
            return helper.TextBoxFor(expression, new { @class = "date-picker input-sm", data_date_format = "yyyy-mm-dd HH:mm:ss" });
        }*/
    }
}

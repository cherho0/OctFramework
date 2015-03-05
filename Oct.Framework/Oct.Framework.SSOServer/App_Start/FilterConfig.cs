using System.Web;
using System.Web.Mvc;

namespace Oct.Framework.SSOServer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
using System.Web.Mvc;

namespace Oct.Framework.TestWeb.Areas.CodeGen
{
    public class CodeGenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CodeGen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CodeGen_default",
                "CodeGen/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

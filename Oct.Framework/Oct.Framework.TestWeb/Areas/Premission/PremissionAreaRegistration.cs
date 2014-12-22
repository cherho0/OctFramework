using System.Web.Mvc;

namespace Oct.Framework.TestWeb.Areas.Premission
{
    public class PremissionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Premission";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
               "Premission_default2",
               "Premission/{controller}/{action}/{id}.action",
               new { action = "Index", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "Premission_default1",
                "Premission/{controller}/{action}.action",
                new { action = "Index" }
            );

            context.MapRoute(
                "Premission_default",
                "Premission/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

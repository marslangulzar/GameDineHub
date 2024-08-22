using System.Web.Mvc;
using System.Web.Routing;

namespace FireStreetPizza
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Admin",
               url: "admin",
               defaults: new { controller = "General", action = "manage", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Display",
              url: "display",
              defaults: new { controller = "General", action = "Display", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "Play",
             url: "play",
             defaults: new { controller = "Team", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "General", action = "Display", id = UrlParameter.Optional }
            );
        }
    }
}

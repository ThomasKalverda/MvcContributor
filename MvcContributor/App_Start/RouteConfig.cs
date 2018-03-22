using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcContributor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Table",
                url: "Contributor/Details/{id}",
                defaults: new { controller = "Contributors", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Upload",
                url: "Contributor/Edit/{id}",
                defaults: new { controller = "Contributors", action = "Upload", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Details",
                url: "Contributor/Details/{id}",
                defaults: new { controller = "Contributors", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contributors", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

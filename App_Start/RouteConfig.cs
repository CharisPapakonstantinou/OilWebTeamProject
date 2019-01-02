using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OilTeamProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Cart", "Cart/{action}/{id}",
                new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                new[] { "CmsShoppingCart.Controllers" });

            routes.MapRoute(
               name: "Specific Category",
               url: "Home/category/{slug}",
               defaults: new { controller = "Home", action = "SingleCategory" }
            );

            routes.MapRoute(
               name: "Specific Product",
               url: "Home/product/{slug}",
               defaults: new { controller = "Home", action = "SingleProduct" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mockingbird
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "portal/{controller}/{action}/{id}",//Prefix route with 'portal' to route all portal requests for manage mock endpoints.
                defaults: new { controller = "mockingbird", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}

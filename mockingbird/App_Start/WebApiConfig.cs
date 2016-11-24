using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using mockingbird.Handlers;

namespace mockingbird
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new MockRequestsInterceptor());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*url}" //Lets allow all the requests to this website to be intercepted.
            );
        }
    }
}

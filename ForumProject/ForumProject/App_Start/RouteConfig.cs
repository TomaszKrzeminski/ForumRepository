using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace ForumProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            routes.MapRoute("", "Home/Show_IntermediateCategory_List/{id}", new { controller = "Home", action = "Show_IntermediateCategory_List", id = UrlParameter.Optional },
                          
             
              new{
                  controller="Home",action= "Show_IntermediateCategory_List",
                  id =new RangeRouteConstraint(1,20) }
          );



            routes.MapRoute("", "Forum/{controller}/{action}/{id}", new { controller = "Admin", action = "Index",id=UrlParameter.Optional });

            routes.MapRoute(
               name: "",
               url: "{controller}/{action}/{id}/{*text}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );


          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

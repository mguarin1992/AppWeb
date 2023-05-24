using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EliminarUsuario",
                url: "Usuarios/Eliminar/{id}",
                defaults: new { controller = "Usuarios", action = "Eliminar", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "RecuperarClave",
                url: "Acceso/RecuperarClave",
                defaults: new { controller = "Acceso", action = "RecuperarClave" }
            );
        }
    }
}

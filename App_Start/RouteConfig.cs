using System.Web.Mvc;
using System.Web.Routing;

namespace API_desafio
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "usuario", action = "ExibirTodos", id = UrlParameter.Optional }
            );
        }
    }
}

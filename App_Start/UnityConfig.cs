using API_desafivo_v2.Controllers;
using API_desafivo_v2.Data;
using System.Data.Entity;
using System.Text;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API_desafivo_v2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
                 container.RegisterType< DbContext, API_desafivo_v2Context > (); 
                 container.RegisterType<StringBuilder, StringBuilder>();

            //this

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using MvcApplication1;
using Ninject;
using Ninject.Web.Mvc;
using ZeroDay.API.App_Start;
using ZeroDay.DAL.Interfaces;
using ZeroDay.DAL.Repositories;

namespace ZeroDay.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public class MyNinjectModule : Ninject.Modules.NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<IImageRepository>().To<ImageRepository>();
            }
        }
    }
}
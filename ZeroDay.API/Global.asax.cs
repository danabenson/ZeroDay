using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZeroDay.API.App_Start;

namespace ZeroDay.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
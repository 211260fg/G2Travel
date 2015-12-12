using System.Web.Http;
using System.Web.Routing;

namespace G10TravelService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}
using System.Web.Http;
using GeneradorClimas.WebApi.Models;

namespace GeneradorClimas.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var formatters = GlobalConfiguration.Configuration.Formatters;

            formatters.Remove(formatters.XmlFormatter);

            Climas.Init();
        }
    }
}

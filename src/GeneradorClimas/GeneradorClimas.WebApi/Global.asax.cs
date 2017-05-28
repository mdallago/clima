using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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

    public enum Clima:byte
    {
        Sequia, Lluvia, Optimo, NoDefinido
    }

    public static class Climas
    {
        static Dictionary<int,Clima> climas;

        public static void Init()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/climas.txt");
            var lines = System.IO.File.ReadAllLines(path);
            climas = lines.Select((x,i) => new { Dia = i, Clima = (Clima)byte.Parse(x) } ).ToDictionary( x=> x.Dia,j=> j.Clima  );
        }

        public static Clima? ObtenerClima (int dia)
        {
            if (climas.ContainsKey(dia))
                return climas[dia];
            return null;
        }
    }
}

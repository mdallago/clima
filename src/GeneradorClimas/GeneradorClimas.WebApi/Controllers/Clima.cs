using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneradorClimas.WebApi.Controllers
{
    public class ClimaController : ApiController
    {
        public HttpResponseMessage Get(int dia)
        {
            var clima = Climas.ObtenerClima(dia);

            if (clima != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK , new Resultado()
                {
                    Dia = dia,
                    Clima = clima.Value.ToString()
                });
            }
            else
            {
                HttpError err = new HttpError("El dia solicitado no existe");
                return Request.CreateErrorResponse( HttpStatusCode.NotFound,err);
            }
        }
    }

    public class Resultado
    {
        public int Dia { get; set; }
        public string Clima { get; set; }
    }

}
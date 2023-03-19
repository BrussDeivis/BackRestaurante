using System;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.WebApplication.Controllers;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.Areas.Administracion.Controllers
{
    public class ActorBaseController : BaseController
    {

        public DataActorApi ObtenerActorRucApi(string numeroDocumento)
        {
            DataActorApi dataActorApi = new DataActorApi();
            try
            {
                string url = AplicacionSettings.Default.UrlApiConsultaActorRuc + numeroDocumento;
                var t = Task.Run(() => GetURI(new Uri(url)));
                t.Wait();
                string result = t.Result;
                dataActorApi = Newtonsoft.Json.JsonConvert.DeserializeObject<DataActorApi>(result);
                if (dataActorApi.Estado == false)
                {
                    throw new ControllerException("No se puede obtener el actor desde el api");
                }
                return dataActorApi;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener el actor", e);
            }
        }

        public DataActorApi ObtenerActorDniApi(string numeroDocumento)
        {
            var dataActorApi = new DataActorApi();
            try
            {
                string url = AplicacionSettings.Default.UrlApiConsultaActorDni + numeroDocumento;
                var t = Task.Run(() => GetURI(new Uri(url)));
                t.Wait();
                string result = t.Result;
                dataActorApi = Newtonsoft.Json.JsonConvert.DeserializeObject<DataActorApi>(result);
                if (dataActorApi.Estado == false)
                {
                    throw new ControllerException("No se puede obtener el actor desde el api");
                }
                return dataActorApi;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener el actor", e);
            }
        }
    }
}
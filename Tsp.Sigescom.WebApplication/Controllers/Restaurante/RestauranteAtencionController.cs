using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class RestauranteAtencionController : BaseController
    {
        private readonly IAtencion_Logica _atencionLogica;
        protected readonly IPdfUtil _pdfUtil;


        protected readonly IBarCodeUtil barCodeUtil;

        public RestauranteAtencionController(): base()
        {
            _atencionLogica = Dependencia.Resolve<IAtencion_Logica>();
            _pdfUtil = Dependencia.Resolve<IPdfUtil>();
        }
        SesionRestaurante SesionRestaurante
        {
            get
            {
                return (SesionRestaurante)this.Session["SesionRestaurante"];
            }
            set
            {
                this.Session["SesionRestaurante"] = value;
            }
        }


        public JsonResult CambiarMesa(AtencionRestaurante atencion, int idNuevaMesa)
        {
            try
            {
                OperationResult result = _atencionLogica.CambiarDeMesa(atencion, idNuevaMesa, SesionRestaurante);
                return Json(new { result.code_result, data = result.information, result_description = result.title, result.objeto });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

    }
}
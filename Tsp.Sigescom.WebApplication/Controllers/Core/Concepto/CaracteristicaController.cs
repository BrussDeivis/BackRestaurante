using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Utilitarios;
using System.Threading.Tasks;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CaracteristicaController : BaseController
    {
        private readonly ICaracteristica_Logica _logicaCaracteristica;

        public CaracteristicaController()
        {
            _logicaCaracteristica = Dependencia.Resolve<ICaracteristica_Logica>();
        }

        public JsonResult ObtenerCaracteristicasYValoresDeCaracteristicasDeConceptoNegocio(int idConcepto)
        {
            try
            {
                var concepto = _logicaCaracteristica.ObtenerConceptoNegocioConSusCaracteristicasYSusValores(idConcepto);
                return Json(concepto);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

    }
}
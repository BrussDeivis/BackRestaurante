using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Venta;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class VentaReportesController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IVentaReporte_Logica ventaReportingLogica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;

        public VentaReportesController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            ventaReportingLogica = Dependencia.Resolve<IVentaReporte_Logica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
        }

        [Authorize(Roles = "JefeVenta,AdministradorNegocio,Gerente")]
        public ActionResult Principal()
        {
            ViewBag.Data = ventaReportingLogica.ObtenerDatosParaReportePrincipal(ProfileData());
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CorreccionInventarioController : BaseController
    {

        protected readonly IAlmacenReporte_Logica  almacenReportingLogica;
        protected readonly IInventarioHistorico_Logica inventarioHistoricoLogica;
        protected readonly IInventarioActual_Logica inventarioActualLogica;


        public CorreccionInventarioController() : base()
        {
            inventarioActualLogica = Dependencia.Resolve<IInventarioActual_Logica>();
            inventarioHistoricoLogica = Dependencia.Resolve<IInventarioHistorico_Logica>();
            almacenReportingLogica = Dependencia.Resolve<IAlmacenReporte_Logica>();


    }
    // GET: CorreccionInventario
    public ActionResult Corregir()
        {
            return View();
        }
    }
}
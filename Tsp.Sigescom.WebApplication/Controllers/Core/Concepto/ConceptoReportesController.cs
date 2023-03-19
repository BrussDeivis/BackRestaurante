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
    public class ConceptoReportesController : BaseController
    {
        private readonly IConceptoLogica _logicaConcepto;

        public ConceptoReportesController()
        {
            _logicaConcepto = Dependencia.Resolve<IConceptoLogica>();

        }

        [Authorize(Roles = "Vendedor,Almacenero,AdministradorNegocio")]
        public ActionResult ReportesAdministrador()
        {
            return View();
        }
       
        public ActionResult ReporteDeProductoConCodigoDeBarra()// 
        {
            List<ConceptoDeNegocio> resultado = _logicaConcepto.obtenerMercaderias();
            List<ReporteProductoViewModel> reporteProductoViewModel = ReporteProductoViewModel.Convert(resultado);
            ReportParameter entidadInterna = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
            ReportParameter fecha = new ReportParameter("FechaActual",DateTimeUtil.FechaActual().ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActualConHora", fechaActual.ToString());
            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Conceptos/Conceptos.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { entidadInterna, nombreEmpresa, fecha, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetReporteDeProducto", reporteProductoViewModel);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
    }
}
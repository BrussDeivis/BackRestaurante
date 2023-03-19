using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class EjemploController : BaseController
    {
        private readonly ICocheraLogica cocheraLogica;
        private new readonly IActorNegocioLogica actorNegocioLogica;
        public EjemploController()
        {
            cocheraLogica = Dependencia.Resolve<ICocheraLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
        }

        // GET: Ejemplo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reportes()
        {
            ViewBag.fechaHoraInicio = DateTime.Now.AddDays(-30);
            ViewBag.fechaHoraFin = DateTime.Now;
            ViewBag.cantidadMaximaDias = 30;//todo: parametrizar
            return View();
        }

        public ActionResult TemplateVertical()
        {
            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            var cochera = new ItemGenerico { Id = 3, Nombre = "BOTICA_MARCOS_PUNTO DE VENTA 1" };
            bool fromRequest = true;
            List<EntradaSalidaUsuario> vehiculosIngresados = cocheraLogica.ObtenerVehiculosIngresados(cochera.Id);
            ReportParameter nombreSede = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            //rptviewer.LocalReport.SubreportProcessing();
            string path = @"/Content/reports/Ejemplos/EjemploPruebaVertical.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion,logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("vehiculosIngresadosDataset", vehiculosIngresados);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }


        public ActionResult TemplateHorizontal()
        {

            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            var cochera = new ItemGenerico { Id = 3, Nombre = "BOTICA_MARCOS_PUNTO DE VENTA 1" };
            bool fromRequest = true;
            List<EntradaSalidaUsuario> vehiculosIngresados = cocheraLogica.ObtenerVehiculosIngresados(cochera.Id);
            ReportParameter nombreSede = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            ReportParameter usuario = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechayHoraActual = new ReportParameter("FechaActual", fechaActual.ToString());
      
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Ejemplos/EjemploPruebaHorizontal.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion, logoSede, usuario, fechayHoraActual });
            ReportDataSource rptdatasource = new ReportDataSource("vehiculosIngresadosDataset", vehiculosIngresados);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult ReporteVehiculosIngresados(int idCochera, string nombreCochera)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };
            var rptviewer = GenerarReporteVehiculosIngresados(cochera, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteVehiculosIngresados(ItemGenerico cochera, bool fromRequest)
        {
            List<EntradaSalidaUsuario> vehiculosIngresados = cocheraLogica.ObtenerVehiculosIngresados(cochera.Id);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Cochera/VehiculosIngresados.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion });
            ReportDataSource rptdatasource = new ReportDataSource("vehiculosIngresadosDataset", vehiculosIngresados);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }

        public ActionResult ReporteIngresosYSalidas(int idCochera, string nombreCochera, DateTime desde, DateTime hasta)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };

            var rptviewer = GenerarReporteIngresosYSalidas(cochera, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteIngresosYSalidas(ItemGenerico cochera, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<EntradaSalida> ingresosYSalidas = cocheraLogica.ObtenerEntradasYSalidas(cochera.Id, desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            //ReportParameter logoSede = new ReportParameter("LogoSede", ObtenerSede().Logo());
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            string path = @"/Content/reports/Cochera/IngresosYSalidas.rdlc";
            ReportDataSource rptdatasource = new ReportDataSource("entradasYSalidasDataset", ingresosYSalidas);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion });
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }

        public ActionResult ReporteExoneraciones(int idCochera, string nombreCochera)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };
            var rptviewer = GenerarReporteExoneraciones(cochera, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteExoneraciones(ItemGenerico cochera, bool fromRequest)
        {
            List<ExoneracionDeVehiculo_Flat> ingresosYSalidas = ExoneracionDeVehiculo_Flat.Convert(cocheraLogica.ObtenerVehiculosExonerados(cochera.Id));
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            string path = @"/Content/reports/Cochera/VehiculosExonerados.rdlc";
            ReportDataSource rptdatasource = new ReportDataSource("exoneracionesDataset", ingresosYSalidas);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion });
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }


        public ActionResult Botones() {
            return View();

        }

        public ActionResult BandejaResponsive()
        {
            return View();

        }

        public ActionResult Prueba()
        {
            return View();

        }


        
    }

   


}
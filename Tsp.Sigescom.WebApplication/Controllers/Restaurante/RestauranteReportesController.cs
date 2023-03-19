using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class RestauranteReportesController : BaseController
    {
        private readonly IRestauranteLogica restauranteLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IEstablecimiento_Logica establecimientoLogica;


        public RestauranteReportesController():base()
        {
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            restauranteLogica = Dependencia.Resolve<IRestauranteLogica>();
            establecimientoLogica = Dependencia.Resolve<IEstablecimiento_Logica>();

        }

        public async Task<ActionResult> Reportes()
        {
            SesionRestaurante = await restauranteLogica.ObtenerSesion(ProfileData());
            ViewBag.fechaHoraInicio = DateTime.Now.AddDays(-30).Date;
            ViewBag.fechaHoraFin = DateTime.Now.Date.AddDays(1).AddTicks(-1);
            ViewBag.parametros = ConfiguracionRestauranteReportes.Default;
            ViewBag.establecimientos = establecimientoLogica.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos();
            return View("~/Views/Restaurante/Reportes.cshtml");
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
        public ActionResult ReporteOrdenesPorMozo(DateTime desde, DateTime hasta)
        {
            var restaurante = new ItemGenerico { Id = SesionRestaurante.SesionDeUsuario.IdCentroDeAtencionSeleccionado, Nombre = SesionRestaurante.SesionDeUsuario.NombreCentroDeAtencionSeleccionado };
            var rptviewer = GenerarReporteDeOrdenesPorMozo(restaurante, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReporteDeOrdenesPorMozo(ItemGenerico establecimiento, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<ResumenOrden_Consulta> detalles = restauranteLogica.ObtenerReporteDeResumenDeOrdenes(desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", establecimiento.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/OrdenesPorMozo.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion, inicioReporte, finalReporte, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("itemsOrdenesResumidasDataSet", detalles);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }

        public ActionResult ReporteOrdenesDetallado(DateTime desde, DateTime hasta)
        {
            var restaurante = new ItemGenerico { Id = SesionRestaurante.SesionDeUsuario.IdCentroDeAtencionSeleccionado, Nombre = SesionRestaurante.SesionDeUsuario.NombreCentroDeAtencionSeleccionado };
            var rptviewer = GenerarReporteOrdenesDetallado(restaurante, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReporteOrdenesDetallado(ItemGenerico restaurante, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<DetalleOrden_Consulta> detalles = restauranteLogica.ObtenerReporteDetallesAtendidosEnOrdenes(desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", restaurante.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/OrdenesDetalladas.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion, inicioReporte, finalReporte, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("itemsOrdenesDetalladasDataSet", detalles);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }


        public ActionResult ReporteDevolucionesEnOrdenes(DateTime desde, DateTime hasta)
        {
            var restaurante = new ItemGenerico { Id = SesionRestaurante.SesionDeUsuario.IdCentroDeAtencionSeleccionado, Nombre = SesionRestaurante.SesionDeUsuario.NombreCentroDeAtencionSeleccionado };
            var rptviewer = GenerarReporteDevolucionesEnOrdenes(restaurante, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReporteDevolucionesEnOrdenes(ItemGenerico restaurante, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<DetalleOrden_Consulta> detalles = restauranteLogica.ObtenerReporteDetallesDevueltosEnOrdenes(desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", restaurante.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/DevolucionesEnOrdenes.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion, inicioReporte, finalReporte, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("detallesDataSet", detalles);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }
        public ActionResult ReporteDeOrdenesPorConcepto(DateTime desde, DateTime hasta)
        {
            var restaurante = new ItemGenerico { Id = SesionRestaurante.SesionDeUsuario.IdCentroDeAtencionSeleccionado, Nombre = SesionRestaurante.SesionDeUsuario.NombreCentroDeAtencionSeleccionado };
            var rptviewer = GenerarReporteDeOrdenesPorConcepto(restaurante, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReporteDeOrdenesPorConcepto(ItemGenerico restaurante, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<ItemRestaurante_Consulta> detalles = restauranteLogica.ObtenerReporteDeItemsDeRestaurante(desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", restaurante.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/OrdenesPorConcepto.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion, inicioReporte, finalReporte, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("itemsRestauranteDataSet", detalles);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }


        public ActionResult ReporteDeAtenciones(DateTime desde, DateTime hasta, int idEstablecimiento, string nombreEstablecimiento)
        {
            var establecimiento = new ItemGenerico { Id = idEstablecimiento, Nombre = nombreEstablecimiento };
            var rptviewer = GenerarReporteDeAtenciones(establecimiento, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReporteDeAtenciones(ItemGenerico establecimiento, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<OrdenAtencion_Consulta> ordenesAtencion = restauranteLogica.ObtenerOrdenesAtencion(desde, hasta, establecimiento.Id);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter nombreEstablecimiento = new ReportParameter("NombreEstablecimiento", establecimiento.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/OrdenesAtenciones.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametrosBasicos.NombreSede, nombreEstablecimiento, inicioReporte, finalReporte, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametrosBasicos.FechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("ordenesAtencionDataSet", ordenesAtencion);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }

        public ActionResult ReportePorModoAtenciones(DateTime desde, DateTime hasta, int idEstablecimiento, string nombreEstablecimiento)
        {
            var establecimiento = new ItemGenerico { Id = idEstablecimiento, Nombre = nombreEstablecimiento };
            var rptviewer = GenerarReportePorModoAtenciones(establecimiento, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReportePorModoAtenciones(ItemGenerico establecimiento, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<OrdenPorModoAtencion_Consulta> ordenesAtencion = restauranteLogica.ObtenerOrdenesPorModoAtencion(desde, hasta, establecimiento.Id);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter nombreEstablecimiento = new ReportParameter("NombreEstablecimiento", establecimiento.Nombre);
            ReportParameter inicioReporte = new ReportParameter("Desde", desde.ToString());
            ReportParameter finalReporte = new ReportParameter("Hasta", hasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Restaurante/OrdenesPorModoAtencion.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametrosBasicos.NombreSede, nombreEstablecimiento, inicioReporte, finalReporte, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametrosBasicos.FechaActualSistema });
            ReportDataSource rptdatasource = new ReportDataSource("ordenesAtencionDataSet", ordenesAtencion);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }
    }
}
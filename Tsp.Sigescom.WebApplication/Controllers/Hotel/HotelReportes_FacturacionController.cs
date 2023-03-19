using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Logica.SigesHotel;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class HotelReportes_FacturacionController : BaseController
    {
        private readonly IHotelReporte_Logica hotelReporte_Logica;

        public HotelReportes_FacturacionController() : base()
        {
            hotelReporte_Logica = Dependencia.Resolve<IHotelReporte_Logica>();
        }
        public ActionResult Facturacion_Facturadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarFacturacion_Facturadas(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarFacturacion_Facturadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<Facturada> facturadas = hotelReporte_Logica.ObtenerFacturadas(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Facturacion_Facturadas.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreEstablecimiento,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetFacturadas", facturadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }
        public ActionResult Facturacion_NoFacturadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarFacturacion_NoFacturadas(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarFacturacion_NoFacturadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<NoFacturada> noFacturadas = hotelReporte_Logica.ObtenerNoFacturadas(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Facturacion_NoFacturadas.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreEstablecimiento,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceNoFacturadas = new ReportDataSource("DataSetNoFacturadas", noFacturadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceNoFacturadas);
            return rptviewer;
        }
        public ActionResult Facturacion_Incidentes(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarFacturacion_Incidentes(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarFacturacion_Incidentes(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<Incidente> facturadas = hotelReporte_Logica.ObtenerIncidentes(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Facturacion_Incidentes.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreEstablecimiento,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetIncidentes", facturadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }
    }
}
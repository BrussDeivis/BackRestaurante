using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
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
    public class HotelReportes_AtencionesController : BaseController
    {
        private readonly IHotelReporte_Logica hotelReporte_Logica;

        public HotelReportes_AtencionesController() : base()
        {
            hotelReporte_Logica = Dependencia.Resolve<IHotelReporte_Logica>();
        }
        public ActionResult Atenciones_Ingresos(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarAtenciones_Ingresos(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarAtenciones_Ingresos(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<Ingreso> ingresos = hotelReporte_Logica.ObtenerIngresos(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Atenciones_Ingresos.rdlc";
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
            ReportDataSource rptdatasourceIngresos = new ReportDataSource("DataSetIngresos", ingresos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceIngresos);
            return rptviewer;
        }
        public ActionResult Atenciones_Salidas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarAtenciones_Salidas(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarAtenciones_Salidas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<Salida> salidas = hotelReporte_Logica.ObtenerSalidas(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Atenciones_Salidas.rdlc";
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
            ReportDataSource rptdatasourceSalidas = new ReportDataSource("DataSetSalidas", salidas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSalidas);
            return rptviewer;
        }
        public ActionResult Atenciones_Anuladas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarAtenciones_Anuladas(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarAtenciones_Anuladas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<Anulada> anuladas = hotelReporte_Logica.ObtenerAnuladas(idEstablecimiento, fechaDesde, fechaHasta);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Atenciones_Anuladas.rdlc";
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
            ReportDataSource rptdatasourceAnuladas = new ReportDataSource("DataSetAnuladas", anuladas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceAnuladas);
            return rptviewer;
        }
        public ActionResult Atenciones_FormularioT1(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            var rptviewer = GenerarAtenciones_FormularioT1(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarAtenciones_FormularioT1(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            FormularioT1 formularioT1 = hotelReporte_Logica.ObtenerFormularioT1(idEstablecimiento, fechaDesde, fechaHasta);
            var sede = ObtenerSede();
            ReportParameter parametroRazonSocial = new ReportParameter("RazonSocial", sede.Nombre);
            ReportParameter parametroRuc = new ReportParameter("Ruc", sede.DocumentoIdentidad);
            ReportParameter parametroNombreComercial = new ReportParameter("NombreComercial", sede.NombreComercial);
            ReportParameter parametroClase = new ReportParameter("Clase", HotelSettings.Default.ClaseHotelMincetur);
            ReportParameter parametroCategoria = new ReportParameter("Categoria", HotelSettings.Default.CategoriaHotelMincetur);
            ReportParameter parametroNumeroCertificado = new ReportParameter("NumeroCertificado", HotelSettings.Default.NumeroCertificadoHotelMincetur);
            ReportParameter parametroDireccion = new ReportParameter("Direccion", sede.DomicilioFiscal.Detalle);
            ReportParameter parametroTelefonoFijo = new ReportParameter("TelefonoFijo", sede.Telefono);
            ReportParameter parametroSistemaCoordenadas = new ReportParameter("SistemaCoordenadas", HotelSettings.Default.SistemaCoordenadasHotelMincetur);
            ReportParameter parametroRegion = new ReportParameter("Region", sede.DomicilioFiscal.Ubigeo.Nombre.Split('-')[0]);
            ReportParameter parametroProvincia = new ReportParameter("Provincia", sede.DomicilioFiscal.Ubigeo.Nombre.Split('-')[1]);
            ReportParameter parametroDistrito = new ReportParameter("Distrito", sede.DomicilioFiscal.Ubigeo.Nombre.Split('-')[2]);
            ReportParameter parametroPaginaWeb = new ReportParameter("PaginaWeb", HotelSettings.Default.PaginaWebHotelMincetur);
            ReportParameter parametroEmailReserva = new ReportParameter("EmailReserva", HotelSettings.Default.EmailReservaHotelMincetur);
            ReportParameter parametroAnio = new ReportParameter("Anio", fechaDesde.Year.ToString());
            ReportParameter parametroMes = new ReportParameter("Mes", fechaDesde.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper());
            ReportParameter parametroResolucionJefatura = new ReportParameter("ResolucionJefatura", HotelSettings.Default.ResolucionJefaturaReporteMincetur);

            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Atenciones_FormatoT1.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametroRazonSocial,
                parametroRuc,
                parametroNombreComercial,
                parametroClase,
                parametroCategoria,
                parametroNumeroCertificado,
                parametroDireccion,
                parametroTelefonoFijo,
                parametroSistemaCoordenadas,
                parametroRegion,
                parametroProvincia,
                parametroDistrito,
                parametroPaginaWeb,
                parametroEmailReserva,
                parametroAnio,
                parametroMes,
                parametroResolucionJefatura
            });
            ReportDataSource rptdatasourceArribo1_8 = new ReportDataSource("DataSetDiaArribo1_8", formularioT1.Dias1_8Arribos);
            ReportDataSource rptdatasourceArribo9_16 = new ReportDataSource("DataSetDiaArribo9_16", formularioT1.Dias9_16Arribos);
            ReportDataSource rptdatasourceArribo17_24 = new ReportDataSource("DataSetDiaArribo17_24", formularioT1.Dias17_24Arribos);
            ReportDataSource rptdatasourceArribo25_Total = new ReportDataSource("DataSetDiaArribo25_Total", formularioT1.Dias25_TotalArribos);
            ReportDataSource rptdatasourceiboPernoctacionExtranjero = new ReportDataSource("DataSetArriboPernoctacionExtranjero", formularioT1.ArribosPernoctacionesExtranjeros);
            ReportDataSource rptdatasourceiboPernoctacionNacional = new ReportDataSource("DataSetArriboPernoctacionNacional", formularioT1.ArribosPernoctacionesNacionales);
            ReportDataSource rptdatasourceivoViaje = new ReportDataSource("DataSetMotivoViaje", formularioT1.MotivoViajes);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceArribo1_8);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceArribo9_16);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceArribo17_24);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceArribo25_Total);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceiboPernoctacionExtranjero);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceiboPernoctacionNacional);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceivoViaje);
            return rptviewer;
        }
    }
}
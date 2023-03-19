using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Venta;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.WebApplication.Controllers.reportGeneration;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ReporteController :BaseController
    {
        private new readonly IOperacionLogica operacionLogica;
        private new readonly IConceptoLogica conceptoLogica;
        private new readonly ILibrosElectronicosLogica librosElectronicosLogica;
        private new readonly ILibrosElectronicosAdsoftLogica librosElectronicosAdsoftLogica;
        private new readonly ILibrosElectronicosFoxcontLogica librosElectronicosFoxcontLogica;
        private new readonly IConfiguracionLogica configuracionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        LibrosElectronicosGeneradorReportes generador = new LibrosElectronicosGeneradorReportes();
        protected readonly IMailer mailer;
        protected readonly ICentroDeAtencion_Logica centroDeAtencionLogica;

        protected readonly ISede_Logica sedeLogica;
        protected readonly IConsultaMasivaVentaLogica consultaMasivaVentaLogica;



        public ReporteController()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            librosElectronicosAdsoftLogica = Dependencia.Resolve<ILibrosElectronicosAdsoftLogica>();
            librosElectronicosFoxcontLogica = Dependencia.Resolve<ILibrosElectronicosFoxcontLogica>();
            configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();
            consultaMasivaVentaLogica = Dependencia.Resolve<IConsultaMasivaVentaLogica>();
        }


        public void DefinirReportViewer(ReportViewer rptviewer, List<ReportDataSource> datasources)
        {
            rptviewer.ProcessingMode = ProcessingMode.Local;
            datasources.ForEach(ds => rptviewer.LocalReport.DataSources.Add(ds));
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
        }

        #region GETS
        public ActionResult ConsolidadoReportesAdministrador()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }

        public ActionResult GastosReportesAdministrador()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }

        public ActionResult DeudasPorPagarYDeudasPorCobrarsAdministrador()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        [Authorize(Roles = "Gerente,AdministradorNegocio")]
        public ActionResult DeudasYPagos()
        {

            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        [Authorize(Roles = "Gerente,AdministradorNegocio")]
        public ActionResult ReportesAdministradorDeCompras()
        {
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteCompra();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        [Authorize(Roles = "Gerente,AdministradorNegocio")]
        public ActionResult ReporteDeDeudaPorCliente()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        public ActionResult ReporteUtilidadDeVentas()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }

        public ActionResult ReporteFinanzas()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio,Gerente")]
        public ActionResult ReportePuntos()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            return View();
        }
        #endregion


        #region REPORTES DEL ADMINISTRADOR

        //REPORTE POR SERIE DE COMPROBANTE
        public ActionResult ReporteIntervaloFechasPorComprobante(string fechaInicio, string fechaFin, int idSerie)
        {
            var sede = ObtenerSede();
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            var rptviewer = GenerarReporteIntervaloFechasPorComprobante(ProfileData().NombreCentroDeAtencionSeleccionado, sede, fechaDesde, fechaHasta, idSerie, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        /// <summary>
        /// Genera el reporte intervalo de fechas por comprobante
        /// </summary>
        /// <param name="nombreCentroDeAtencion"></param>
        /// <param name="sede"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idSerie"></param>
        /// <param name="fromRequest">Sirve para diferenciar si se esta haciendo una peticion interna (vista) o si se esta haciendo una peticion externa (hangfire)</param>
        /// <returns></returns>
        public ReportViewer GenerarReporteIntervaloFechasPorComprobante(string nombreCentroDeAtencion, EstablecimientoComercialExtendidoConLogo sede, DateTime fechaDesde, DateTime fechaHasta, int idSerie, bool fromRequest)
        {
            CentroDeAtencionExtendido centroAtencionDeLaSerie = centroDeAtencionLogica.ObtenerCentroDeAtencionSegunSerieComprobante(idSerie);
            string nombreCentroAtencionDeLaSerie = centroAtencionDeLaSerie.Nombre;
            List<ResumenDeTransaccionVenta> ordenes = operacionLogica.ObtenerResumenDeOperacionesDeVenta(fechaDesde, fechaHasta, idSerie);

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombreCentroDeAtencion);
            ReportParameter parametroNombreCentroAtencionDeLaSerie = new ReportParameter("NombreCentroDeAtencionDeLaSerie", nombreCentroAtencionDeLaSerie);

            //Sede sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", fromRequest ? ProfileData().Empleado.NombresYApellidos : "-");
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
            var rptviewer = new ReportViewer();
            string path = @"/Content/reports/Core/Ventas/VentasPorComprobante.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, parametroNombreCentroAtencionDeLaSerie, logoSede, fechaActualSistema, empleadoSede, nombreCentroAtencion });

            ReportDataSource rptdatasourceordenes = new ReportDataSource("ordenesDataset", ordenes);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceordenes);
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            return rptviewer;
        }

        //REPORTE DE PUNTOS DE VENTA POR CONCEPTO - CAMBIADO A VENTAS POR CONCEPTO
        public ActionResult ReporteIntervaloFechasPorConcepto(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            //List<OrdenDeVenta> detalleDeVentasPorPuntosDeVenta = operacionLogica.ObtenerOrdenesDeVentaConfirmadasOTransferidas(idsPuntosDeVentas, fechaDesde, fechaHasta); //y22
            //List<DetalleDeVentaConPuntoVentaViewModel> reporteVentaViewModelDetalles = DetalleDeVentaConPuntoVentaViewModel.Convert(detalleDeVentasPorPuntosDeVenta);

            List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ventasConfirmadasYTransmididas = operacionLogica.ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadas(idsPuntosDeVentas, fechaDesde, fechaHasta);
            List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ventasInvalidadas = operacionLogica.ObtenerOrdenesDeVentaPorConceptoInvalidadas(idsPuntosDeVentas, fechaDesde, fechaHasta);
            //List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ventasAnuladas = operacionLogica.ObtenerOrdenesDeVentaPorConceptoAnuldada(idsPuntosDeVentas, fechaDesde, fechaHasta); 

            string _nombresCentrosAtencion = "";

            foreach (var item in idsPuntosDeVentas)
            {
                string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(item);
                _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
            }

            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreDeSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorConcepto.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreDeSede, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptVentasConfirmadasTransmitida = new ReportDataSource("dataSetVentaConfirmadas", ventasConfirmadasYTransmididas);
            ReportDataSource rptVentasInvalidadas = new ReportDataSource("dataSetVentaInvalidadas", ventasInvalidadas);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptVentasConfirmadasTransmitida);
            rptviewer.LocalReport.DataSources.Add(rptVentasInvalidadas);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        #endregion

        #region REPORTES DE VENTAS - ADMINISTRADOR

        //REPORTE DE PUNTOS DE VENTA POR COMPROBANTE cambio VENTAS POR CENTRO DE ATENCIÓN Y SERIE
        public ActionResult ReporteDeVentaDeLosPuntosDeVentaPorComprobante(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            var rptviewer = GenerarReporteDeVentaDeLosPuntosDeVentaPorComprobante(sede, fechaDesde, fechaHasta, idsPuntosDeVentas, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        /// <summary>
        /// Genera el reporte de venta de los puntos de venta por comprobantes
        /// </summary>
        /// <param name="sede"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idsPuntosDeVentas"></param>
        /// <param name="fromRequest">Sirve para diferenciar si se esta haciendo una peticion interna (vista) o si se esta haciendo una peticion externa (hangfire)</param>
        /// <returns></returns>
        public ReportViewer GenerarReporteDeVentaDeLosPuntosDeVentaPorComprobante(EstablecimientoComercialExtendidoConLogo sede, DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosDeVentas, bool fromRequest)
        {
            List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ventasConfirmada = operacionLogica.ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSerie(idsPuntosDeVentas, fechaDesde, fechaHasta);
            List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ventasInvalidada = operacionLogica.ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSerie(idsPuntosDeVentas, fechaDesde, fechaHasta);

            string _nombresCentrosAtencion = "";

            foreach (var item in idsPuntosDeVentas)
            {
                string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(item);
                _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
            }

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", fromRequest ? ProfileData().Empleado.NombresYApellidos : "-");

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            var rptviewer = new ReportViewer();
            string path = @"/Content/reports/Core/Ventas/VentasPorCentroDeAtencionYSerie.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, nombreCentroAtencion, parametroFechaDesde, parametroFechaHasta, empleadoSede, fechaActualSistema, logoSede });

            ReportDataSource rptdatasourceConfirmada = new ReportDataSource("DataSetConfirmada", ventasConfirmada);
            ReportDataSource rptdatasourceInvalidada = new ReportDataSource("DataSetInvalidada", ventasInvalidada);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidada);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            return rptviewer;
        }

        //REPORTE DE PUNTOS DE VENTA POR CONCEPTO-------------------cambio a ventas por concepto y vendedor
        public ActionResult ReporteDeVentaDeVendedoresPorConcepto(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsVendedor)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            //RESUMEN
            List<ResumenTransaccionPorVendedor> resumenVentasConfirmadas = operacionLogica.ObtenerResumenDeVentasPorVendedorConfirmadas(idsVendedor, fechaDesde, fechaHasta);
            List<ResumenTransaccionPorVendedor> resumenVentasInvalidadas = operacionLogica.ObtenerResumenDeVentasPorVendedorInvalidadas(idsVendedor, fechaDesde, fechaHasta);
            //DETALLADO
            List<DetalleTransaccionPorVendedor> detalleConfirmado = operacionLogica.ObtenerDetalleDeVentasPorVendedorConfirmadas(idsVendedor, fechaDesde, fechaHasta);
            List<DetalleTransaccionPorVendedor> detalleInvalidado = operacionLogica.ObtenerDetallaDeVentasPorVendedorInvalidadas(idsVendedor, fechaDesde, fechaHasta);

            //parámetros
            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorConceptoYVendedorAdministrador.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreSede, nombreCentroAtencion, logoSede, fechaActualSistema, empleadoSede });

            ReportDataSource rptdatasourceVentasResumenConfirmadas = new ReportDataSource("DataSetResumenConfirmadas", resumenVentasConfirmadas);
            ReportDataSource rptdatasourceVentasResumenInvalidadas = new ReportDataSource("DataSetResumenInvalidadas", resumenVentasInvalidadas);
     

            ReportDataSource rptdatasourceVentasDetalleConfirmadas = new ReportDataSource("DataSetDetalleConfirmadas", detalleConfirmado);
            ReportDataSource rptdatasourceVentasDetalleInvalidadas = new ReportDataSource("DataSetDetalleInvalidadas", detalleInvalidado);
    

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasResumenConfirmadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasResumenInvalidadas);
       
            rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasDetalleConfirmadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasDetalleInvalidadas);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);

            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");

        }

        //REPORTE DE VENTA POR SERIE Y FAMILIA
        public ActionResult ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYFamilia(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<TransaccionPorSerieDeComprobanteYConceptoBasico> ventasConfirmada = operacionLogica.ObtenerComprobanteVentaPorSerieYConceptoBasicoConfirmado(idsPuntosDeVentas, fechaDesde, fechaHasta); //a13.1
            List<TransaccionPorSerieDeComprobanteYConceptoBasico> ventasInvalidada = operacionLogica.ObtenerComprobantePorSerieYConceptoBasicoInvalidado(idsPuntosDeVentas, fechaDesde, fechaHasta); //a13.1

            string _nombresCentrosAtencion = "";

            foreach (var item in idsPuntosDeVentas)
            {
                string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(item);
                _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
            }
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
            ReportParameter FechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter FechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorFamiliaYSerieAdministrador.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, nombreCentroAtencion, FechaDesde, FechaHasta, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceConfirmada = new ReportDataSource("DataSetConfirmada", ventasConfirmada);
            ReportDataSource rptdatasourceInvalidada = new ReportDataSource("DataSetInvalidada", ventasInvalidada);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidada);

            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DE PUNTOS DE VENTA POR SERIE Y CONCEPTO
        public ActionResult ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYConcepto(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int idPuntoDeVenta, string nombrePuntoDeVenta) //a13
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);


            List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ResumenPorSerieYConceptoTransmitidoYConfirmado = operacionLogica.ObtenerResumenVentasConSerieYConceptoNegocioConfirmada(idPuntoDeVenta, fechaDesde, fechaHasta); //XY5.1
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoTransmitidoYConfirmado = operacionLogica.ObtenerResumenVentasConSerieYConceptoBasicoConfirmadaYTransmitida(idPuntoDeVenta, fechaDesde, fechaHasta); // XY5.2
            List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ResumenPorSerieYConceptoInvalidos = operacionLogica.ObtenerResumenVentasConSerieYConceptoNegocioInvalidadas(idPuntoDeVenta, fechaDesde, fechaHasta); //XY5.3
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoInvalidos = operacionLogica.ObtenerResumenVentasConSerieYConceptoBasicoInvalidadas(idPuntoDeVenta, fechaDesde, fechaHasta); // XY5.4
            List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ResumenPorSerieYConceptoAnulados = operacionLogica.ObtenerResumenVentasConSerieYConceptoNegocioAnuladas(idPuntoDeVenta, fechaDesde, fechaHasta); //XY5.5
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoAnulados = operacionLogica.ObtenerResumenVentasConSerieYConceptoBasicoAnuladas(idPuntoDeVenta, fechaDesde, fechaHasta); // XY5.6

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion
            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombrePuntoDeVenta);

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorConceptoYSerie.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, empleadoSede, fechaActualSistema, logoSede });

            ReportDataSource rptdatasourceSerieConceptoTransmitidoYConfirmado = new ReportDataSource("DataSetPorSerieYConceptoNegocioTransmitidaYConfirmada", ResumenPorSerieYConceptoTransmitidoYConfirmado);
            ReportDataSource rptdatasourceSerieConceptoBasicoTransmitidoYConfirmado = new ReportDataSource("DataSetPorSerieYConceptoBasicoTransmitidaYConfirmada", ResumenPorSerieYConeptoBasicoTransmitidoYConfirmado);//
            ReportDataSource rptdatasourceSerieConceptoInvalidado = new ReportDataSource("DataSetPorSerieYConceptoNegocioInvalidado", ResumenPorSerieYConceptoInvalidos);
            ReportDataSource rptdatasourceSerieConceptoBasicoInvalidado = new ReportDataSource("DataSetPorSerieYConceptoBasicoInvalidado", ResumenPorSerieYConeptoBasicoInvalidos);//
            ReportDataSource rptdatasourceSerieConceptoAnulado = new ReportDataSource("DataSetPorSerieYConceptoNegocioAnulado", ResumenPorSerieYConceptoAnulados);
            ReportDataSource rptdatasourceSerieConceptoBasicoAnulado = new ReportDataSource("DataSetPorSerieYConceptoBasicoAnulado", ResumenPorSerieYConeptoBasicoAnulados);//

            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoTransmitidoYConfirmado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasicoTransmitidoYConfirmado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoInvalidado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasicoInvalidado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoAnulado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasicoAnulado);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ActionResult ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYCategoria(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<TransaccionPorSerieDeComprobanteYCategoria> ventasConfirmada = consultaMasivaVentaLogica.ObtenerComprobanteVentaPorSerieYCategoriaConfirmado(idsPuntosDeVentas, fechaDesde, fechaHasta);  
            List<TransaccionPorSerieDeComprobanteYCategoria> ventasInvalidada = consultaMasivaVentaLogica.ObtenerComprobanteVentaPorSerieYCategoriaInvalidado(idsPuntosDeVentas, fechaDesde, fechaHasta);

            string _nombresCentrosAtencion = "";

            foreach (var item in idsPuntosDeVentas)
            {
                string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(item);
                _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
            }
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
            ReportParameter FechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter FechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorCategoriaYSerieAdministrador.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, nombreCentroAtencion, FechaDesde, FechaHasta, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceConfirmada = new ReportDataSource("DataSetConfirmada", ventasConfirmada);
            ReportDataSource rptdatasourceInvalidada = new ReportDataSource("DataSetInvalidada", ventasInvalidada);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidada);

            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ActionResult ReporteDeVentaPorSerieDeComprobanteYConcepto(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsSeries, string nombresSeries)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<TransaccionPorSerieDeComprobanteYConcepto> ventasConfirmada = consultaMasivaVentaLogica.ObtenerComprobanteVentaPorSerieYConceptoConfirmado(idsSeries, fechaDesde, fechaHasta);
            List<TransaccionPorSerieDeComprobanteYConcepto> ventasInvalidada = consultaMasivaVentaLogica.ObtenerComprobanteVentaPorSerieYConceptoInvalidado(idsSeries, fechaDesde, fechaHasta);

           
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter FechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter FechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreSeries = new ReportParameter("NombreSeries", nombresSeries);

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorSerieYConcepto.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, FechaDesde, FechaHasta, logoSede, empleadoSede, fechaActualSistema, nombreSeries });

            ReportDataSource rptdatasourceConfirmada = new ReportDataSource("DataSetConfirmada", ventasConfirmada);
            ReportDataSource rptdatasourceInvalidada = new ReportDataSource("DataSetInvalidada", ventasInvalidada);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidada);

            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DE VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO - CAMBIADO A VENTAS POR MODALIDAD Y CONCEPTO
        public ActionResult VentasPorModalidadConsolidadoPorConcepto(string fechaInicio, string fechaFin, int idVendedor, string nombreVendedor, [System.Web.Http.FromUri] string[] modalidades)
        {
            try
            {

                //DECLARACION DE VARIABLES
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
                int idEmpleado = idVendedor == 0 ? ProfileData().Empleado.Id : idVendedor;

                //RESUMEN
                List<Resumen_Transaccion_Por_Modalidad> resumenVentasConfirmadas = operacionLogica.ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                List<Resumen_Transaccion_Por_Modalidad> resumenVentasInvalidadas = operacionLogica.ObtenerResumenesDeVentasPorModalidadYPorVendedorInvalidadas(modalidades, idEmpleado, fechaDesde, fechaHasta);

                //DETALLADO
                List<Detalle_Transaccion_Por_Modalidad> detalleVentasConfirmadas = operacionLogica.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaConfirmadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                List<Detalle_Transaccion_Por_Modalidad> detalleVentasInvalidadas = operacionLogica.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaInvalidadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

                //PARAMETROS
                ReportParameter nombreSede = new ReportParameter("NombreSede", sede.Nombre);
                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter parametroEmpleado = new ReportParameter("Empleado", idVendedor == 0 ? ProfileData().Empleado.NombresYApellidos.ToString() : nombreVendedor);

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                var rptviewer = new ReportViewer();
                //NOMBRE DEL REPORTE
                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorModalidadYConcepto.rdlc";
                //PARAMETROS DEL REPORTE
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreSede, parametroFechaDesde, parametroFechaHasta, parametroEmpleado, logoSede, fechaActualSistema, empleadoSede });
                //DATA SOURCE - RESUMEN
                ReportDataSource rptdatasourceResumenVentasConfirmadas = new ReportDataSource("DataSetResumenConfirmadas", resumenVentasConfirmadas);
                ReportDataSource rptdatasourceResumenVentasInvalidas = new ReportDataSource("DataSetResumenInvalidadas", resumenVentasInvalidadas);
                //DATA SOURCE - DETALLADO
                ReportDataSource rptdatasourceDetalleVentasConfirmadas = new ReportDataSource("DataSetDetalleConfirmadas", detalleVentasConfirmadas);
                ReportDataSource rptdatasourceDetalleVentasInvalidadas = new ReportDataSource("DataSetDetalleInvalidadas", detalleVentasInvalidadas);

                //LOCAL
                rptviewer.ProcessingMode = ProcessingMode.Local;
                //AGREGAR DATA SOURCE - RESUMEN
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasInvalidas);

                //AGREGAR DATA SOURCE - DETALLADO
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasInvalidadas);

                //TAMAÑOS
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de ventas por modalidad consolidado por concepto", e);
            }
        }

        //REPORTE DE VENTAS POR MODALIDAD CONSOLIDADO POR PRECIO UNITARIO - CAMBIADO A VENTAS POR MODALIDAD, CONCEPTO Y PRECIO UNITARIO
        public ActionResult VentasPorModalidadConsolidadoPorPrecioUnitario(string fechaInicio, string fechaFin, int idVendedor, string nombreVendedor, [System.Web.Http.FromUri] string[] modalidades)
        {
            try
            {

                //DECLARACION DE VARIABLES
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
                int idEmpleado = idVendedor == 0 ? ProfileData().Empleado.Id : idVendedor;

                //RESUMEN
                List<Resumen_Transaccion_Por_Modalidad> resumenVentasConfirmadas = operacionLogica.ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                List<Resumen_Transaccion_Por_Modalidad> resumenVentasInvalidadas = operacionLogica.ObtenerResumenesDeVentasPorModalidadYPorVendedorInvalidadas(modalidades, idEmpleado, fechaDesde, fechaHasta);

                //DETALLADO
                List<Detalle_Transaccion_Por_Modalidad> detalleVentasConfirmadas = operacionLogica.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioConfirmadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                List<Detalle_Transaccion_Por_Modalidad> detalleVentasInvalidadas = operacionLogica.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioInvalidadas(modalidades, idEmpleado, fechaDesde, fechaHasta);
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

                //PARAMETROS
                ReportParameter nombreDeSede = new ReportParameter("NombreSede", sede.Nombre);
                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter parametroEmpleado = new ReportParameter("Empleado", idVendedor == 0 ? ProfileData().Empleado.NombresYApellidos.ToString() : nombreVendedor);

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                var rptviewer = new ReportViewer();
                //NOMBRE DEL REPORTE
                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorModalidadConceptoYPrecioUnitario.rdlc";
                //PARAMETROS DEL REPORTE
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreDeSede, parametroFechaDesde, parametroFechaHasta, parametroEmpleado, logoSede, fechaActualSistema, empleadoSede });
                //DATA SOURCE - RESUMEN
                ReportDataSource rptdatasourceResumenVentasConfirmadas = new ReportDataSource("DataSetResumenConfirmadas", resumenVentasConfirmadas);
                ReportDataSource rptdatasourceResumenVentasInvalidas = new ReportDataSource("DataSetResumenInvalidadas", resumenVentasInvalidadas);
                //DATA SOURCE - DETALLADO
                ReportDataSource rptdatasourceDetalleVentasConfirmadas = new ReportDataSource("DataSetDetalleConfirmadas", detalleVentasConfirmadas);
                ReportDataSource rptdatasourceDetalleVentasInvalidadas = new ReportDataSource("DataSetDetalleInvalidadas", detalleVentasInvalidadas);

                //LOCAL
                rptviewer.ProcessingMode = ProcessingMode.Local;
                //AGREGAR DATA SOURCE - RESUMEN
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasInvalidas);

                //AGREGAR DATA SOURCE - DETALLADO
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasInvalidadas);

                //TAMAÑOS
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de ventas por modalidad consolidado por precio unitario", e);
            }
        }

        //REPORTE POR NUMERO DE COMPROBANTE CON ICBPER
        public ActionResult ReportePorNumeroDeComprobanteConIcbper(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            var rptviewer = GenerarReportePorNumeroDeComprobanteConIcbper(sede, fechaDesde, fechaHasta, idsPuntosDeVentas, true);

            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReportePorNumeroDeComprobanteConIcbper(EstablecimientoComercialExtendidoConLogo sede, DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosDeVentas, bool fromRequest)
        {
            try
            {
                string _nombresCentrosAtencion = "";

                foreach (var item in idsPuntosDeVentas)
                {
                    string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(item);
                    _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
                }

                //RESUMEN
                List<Venta_Cliente> ventasClientesConfirmadas = operacionLogica.ObtenerVentasClientesConfirmadasConIcbper(idsPuntosDeVentas, fechaDesde, fechaHasta);
                List<Venta_Cliente> ventasClientesInvalidadas = operacionLogica.ObtenerVentasClientesInvalidadasConIcbper(idsPuntosDeVentas, fechaDesde, fechaHasta);

                //PARAMETROS
                ReportParameter nombreDeSede = new ReportParameter("NombreSede", sede.Nombre);
                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
                ReportParameter empleadoSede = new ReportParameter("Usuario", fromRequest ? ProfileData().Empleado.NombresYApellidos : "-");

                var rptviewer = new ReportViewer();
                //NOMBRE DEL REPORTE
                string path = @"/Content/reports/Core/Ventas/VentasPorSerieDeComprobante.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                //PARAMETROS DEL REPORTE
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreDeSede, parametroFechaDesde, parametroFechaHasta, logoSede, fechaActualSistema, empleadoSede });
                //DATA SOURCE 
                ReportDataSource rptdatasourceVentasClientesonfirmadas = new ReportDataSource("DataSetConfirmadas", ventasClientesConfirmadas);
                ReportDataSource rptdatasourceVentasClientesInvalidas = new ReportDataSource("DataSetInvalidadas", ventasClientesInvalidadas);

                //LOCAL
                rptviewer.ProcessingMode = ProcessingMode.Local;
                //AGREGAR DATA SOURCE - RESUMEN
                rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasClientesonfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceVentasClientesInvalidas);

                //TAMAÑOS
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte por número de comprobante con icbper", e);
            }
        }

        #endregion

        #region REPORTES DE VENTAS - VENDEDOR

        //REPORTE DEL VENDEDOR POR CONCEPTO
        public ActionResult ReporteDeVentaDelVendedorPorConcepto(string fechaInicio, string fechaFin)//usas vendedor   c1
        {
            try
            {
                //DECLARACION DE VARIABLES
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

                //RESUMEN
                List<ResumenTransaccionPorVendedor> resumenVentasConfirmadas = operacionLogica.ObtenerResumenDeVentasPorVendedorConfirmadas(new int[] { ProfileData().Empleado.Id }, fechaDesde, fechaHasta);
                List<ResumenTransaccionPorVendedor> resumenVentasInvalidadas = operacionLogica.ObtenerResumenDeVentasPorVendedorInvalidadas(new int[] { ProfileData().Empleado.Id }, fechaDesde, fechaHasta);
                //DETALLADO
                List<Resumen_transaccion_Venta_PorConcepto> detalleVentasConfirmadas = operacionLogica.ObtenerOrdenesDeVentaPorConceptConfirmadas(ProfileData().Empleado.Id, fechaDesde, fechaHasta);
                List<Resumen_transaccion_Venta_PorConcepto> detalleVentasInvalidadas = operacionLogica.ObtenerOrdenesDeVentaPorConceptoInvalidadas(ProfileData().Empleado.Id, fechaDesde, fechaHasta);
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

                //PARAMETROS
                ReportParameter nombreDeEmpresa = new ReportParameter("NombreSede", sede.Nombre);

                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter parametroEmpleado = new ReportParameter("Empleado", ProfileData().Empleado.NombresYApellidos.ToString());

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                var rptviewer = new ReportViewer();
                //NOMBRE DEL REPORTE
                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentaPorConceptoYVendedor.rdlc";
                //PARAMETROS DEL REPORTE
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreDeEmpresa, parametroFechaDesde, parametroFechaHasta, parametroEmpleado, logoSede, empleadoSede, fechaActualSistema });
                //DATA SOURCE - RESUMEN
                ReportDataSource rptdatasourceResumenVentasConfirmadas = new ReportDataSource("DataSetResumenConfirmadas", resumenVentasConfirmadas);
                ReportDataSource rptdatasourceResumenVentasInvalidas = new ReportDataSource("DataSetResumenInvalidadas", resumenVentasInvalidadas);
                //DATA SOURCE - DETALLADO
                ReportDataSource rptdatasourceDetalleVentasConfirmadas = new ReportDataSource("DataSetDetalleConfirmadas", detalleVentasConfirmadas);
                ReportDataSource rptdatasourceDetalleVentasInvalidas = new ReportDataSource("DataSetDetalleInvalidadas", detalleVentasInvalidadas);

                //LOCAL
                rptviewer.ProcessingMode = ProcessingMode.Local;
                //AGREGAR DATA SOURCE - RESUMEN
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenVentasInvalidas);

                //AGREGAR DATA SOURCE - DETALLADO
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasConfirmadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleVentasInvalidas);

                //TAMAÑOS
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                return View("ReportesAdministradorDeCompras");
            }
        }

        #endregion

        #region REPORTE DE COMPRAS  
        public ActionResult ReporteDecompraPorTipo(string fechaInicio, string fechaFin, int tipoComprobante)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(fechaFin);

                List<Resumen_Compra> comprasNoGravadas = operacionLogica.ObtenerResumenesCompraDeTipoNoGravadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, tipoComprobante);
                List<Resumen_Compra> comprasGravadasDestinadasAVentasGravadas = operacionLogica.ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, tipoComprobante);
                List<Resumen_Compra> comprasGravadasDestinadasAVentasNoGravadas = operacionLogica.ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasNoGravadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, tipoComprobante);
                List<Resumen_Compra> comprasGravadasDestinadasAVentasGravadasYNoGravadas = operacionLogica.ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadasYNoGravadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, tipoComprobante);

                List<ResumenDeTipoCompra> resumenesTipoCompra = ResumenDeTipoCompra.Convertir(comprasNoGravadas, comprasGravadasDestinadasAVentasGravadas, comprasGravadasDestinadasAVentasNoGravadas, comprasGravadasDestinadasAVentasGravadasYNoGravadas);

                var rptviewer = new ReportViewer();
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
                ReportParameter nombreSede = new ReportParameter("NombreEmpresa", sede.Nombre.ToString());
                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado.ToString());
                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos.ToString());

                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Compras/ComprasPorTipo.rdlc";
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreSede, logoSede, fechaActualSistema, empleadoSede, nombreCentroAtencion });

                ReportDataSource rptdatasourceComprasNoGravadas = new ReportDataSource("ComprasNoGravadasDataSet", comprasNoGravadas);
                ReportDataSource rptdatasourceComprasDestinadasAVentasGravadas = new ReportDataSource("ComprasGravadasDestinadasAVentasGravadasDataSet", comprasGravadasDestinadasAVentasGravadas);
                ReportDataSource rptdatasourceComprasDestinadasAVentasNoGravadas = new ReportDataSource("ComprasGravadasDestinadasAVentasNoGravadasDataSet", comprasGravadasDestinadasAVentasNoGravadas);
                ReportDataSource rptdatasourceComprasDestinadasAVentasGravadasYNoGravadas = new ReportDataSource("ComprasGravadasDestinadasAVentasGravadasYNoGravadasDataSet", comprasGravadasDestinadasAVentasGravadasYNoGravadas);
                ReportDataSource rptdatasourceResumenesTipoCompra = new ReportDataSource("ResumenesTipoCompraDataSet", resumenesTipoCompra);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourceComprasNoGravadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceComprasDestinadasAVentasGravadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceComprasDestinadasAVentasNoGravadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceComprasDestinadasAVentasGravadasYNoGravadas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenesTipoCompra);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return View("ReportesAdministradorDeCompras");
            }
        }
        public ActionResult ReporteDecompraPorComprobante(string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(fechaFin);

                List<OrdenDeCompra> ordenesDeCompra = operacionLogica.ObtenerOrdenesDeCompraConfirmadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);

                List<ReporteCompraComprobante> reporteCompraComproanteViewModel = ReporteCompraComprobante.Convert(ordenesDeCompra);

                var rptviewer = new ReportViewer();
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", sede.Nombre);
                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Content/reports/Core/Compras/ComprasPorComprobante.rdlc";
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, logoSede, fechaActualSistema, empleadoSede });
                ReportDataSource rptdatasourceVentaPorComprobante = new ReportDataSource("CompraPorComprobanteDataSet", reporteCompraComproanteViewModel);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourceVentaPorComprobante);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return View("ReportesAdministradorDeCompras");
            }
        }

        public ActionResult ReporteDeCompraPorConcepto(string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

                List<OrdenDeCompra> detalleDeCompraPorConcepto = operacionLogica.ObtenerOrdenesDeCompraConfirmadas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                List<ReporteCompraConceptoViewModel> reporteDetalleDeCompraViewModel = ReporteCompraConceptoViewModel.Convert(detalleDeCompraPorConcepto);

                var rptviewer = new ReportViewer();
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede(); //trae de la variable de seccion

                ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
                ReportParameter nombreDeEmpresa = new ReportParameter("NombreEmpresa", sede.Nombre);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Content/reports/Core/Compras/ComprasPorConcepto.rdlc";

                rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreDeEmpresa, nombreCentroAtencion, empleadoSede, fechaActualSistema, logoSede });
                ReportDataSource rptdatasourcePorConcepto = new ReportDataSource("DetalleDeCompraDataSet", reporteDetalleDeCompraViewModel);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcePorConcepto);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return View("ReportesAdministradorDeCompras");
            }
        }
        #endregion

        #region REPORTE DE GASTOS

        public ActionResult ReporteDeGastosPorConcepto(string fechaInicio, string fechaFin, bool esReporteGlobal, string nombreCentroDeAtencion, [System.Web.Http.FromUri] int[] idsCentrosAtencion)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(fechaFin);

            List<Resumen_Transaccion_Gasto_Por_Concepto> reporteGastos = operacionLogica.ObtenerReporteGastoPorConcepto(fechaDesde, fechaHasta, esReporteGlobal, idsCentrosAtencion);
            List<Resumen_Transaccion_Gasto_Por_Concepto> resumenGastos = Resumen_Transaccion_Gasto_Por_Concepto.Resumen(reporteGastos);


            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", esReporteGlobal ? ObtenerSede().Nombre : nombreCentroDeAtencion);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Gastos/ReporteDeGastos.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, fechaActualSistema, logoSede, empleadoSede });

            ReportDataSource rptdatasourceGastosResumen = new ReportDataSource("DataSetResumen", resumenGastos);
            ReportDataSource rptdatasourceGastosDetalle = new ReportDataSource("DataSetDetalle", reporteGastos);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceGastosDetalle);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceGastosResumen);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }

        #endregion

        #region REPORTE CONSOLIDADO

        public ActionResult ReporteConsolidado(string fechaInicio, string fechaFin) // Codi.XY7 en proceso
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            //gastopersonal no esta todabia inplementado TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
            List<Resumen_Transaccion_Consolidado> ResumenConsolidado = operacionLogica.ObtenerReporteVentaCompraPagoCobroYGastos(fechaDesde, fechaHasta);
            List<ConsolidadoViewModel> reporteVentaConsolidadoViewModel = ConsolidadoViewModel.Convert(ResumenConsolidado);


            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/Consolidado.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });
            ReportDataSource rptdatasourceSerieConceptoBasico = new ReportDataSource("DataSetConsolidado", reporteVentaConsolidadoViewModel);//

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasico);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");
        }

        #endregion



        #region   REPORTE ESTADO DE CUENTA CLIENTE

        //REPORTE DE ESTADO DE CUENTA DE CARTERA DE CLIENTE
        public ActionResult ReporteDeEstadoDeCuentaClienteVentaCobro(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int idCliente, string nombreCliente, [System.Web.Http.FromUri] int idPuntoDeVenta, string nombrePuntoDeVenta)
        {
            EstadoCuentaCliente_VentaCobro estadoCuentaClienteVentaCobro;

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            estadoCuentaClienteVentaCobro = operacionLogica.ObtenerDeudasMasivasDeCarteraDeClientes(idPuntoDeVenta, idCliente, fechaDesde, fechaHasta);
            estadoCuentaClienteVentaCobro.Cliente = nombreCliente;
            var htmlReporte =HtmlStringBuilder.RenderRazorViewToString("../Reporte/EstadoDeCuentaClienteCarteraCliente", estadoCuentaClienteVentaCobro, this);

            var Renderer = new SelectPdf.HtmlToPdf();
            Renderer.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 0;
            Renderer.Options.MarginLeft = 0;
            Renderer.Options.MarginRight = 0;
            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;
            var documentoPdf = Renderer.ConvertHtmlString(htmlReporte);

            byte[] fileBytes = documentoPdf.Save();
            string fileName = "Estado De Cuenta Cliente Cartera Cliente.pdf";
            return File(fileBytes, "application/pdf", fileName);




        }



        #endregion
 

        #region REPORTE DE UTILIDAD DE VENTAS

        public ActionResult ReporteDeUtilidadDeVentaPorFamilia(string fechaInicio, string fechaFin, bool esReporteGlobal, string nombreCentroDeAtencion, [System.Web.Http.FromUri] int[] idsCentrosAtencion)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<CostoUtilidadPorConcepto> reporteDeUtilidadDeVentas = operacionLogica.ObtenerReporteDeUtilidadDeVentasPorFamilia(fechaDesde, fechaHasta, esReporteGlobal, idsCentrosAtencion);
            List<CostoUtilidadPorConcepto> mejorUtilidadDeVentas = reporteDeUtilidadDeVentas.OrderByDescending(r => r.Utilidad).Take(20).ToList();
            List<CostoUtilidadPorConcepto> peorUtilidadDeVentas = reporteDeUtilidadDeVentas.OrderBy(r => r.Utilidad).Take(20).ToList();

            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreReporte = new ReportParameter("NombreReporte", "REPORTE DE UTILIDAD POR FAMILIA");
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", esReporteGlobal ? ObtenerSede().Nombre : nombreCentroDeAtencion);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/ReporteUtilidadVentaPorFamilia.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreReporte, nombreCentroAtencion, empleadoSede, logoSede, fechaActualSistema });

            ReportDataSource rptdatasourceReporteUtilidad = new ReportDataSource("DataSetReporteUtilidad", reporteDeUtilidadDeVentas);
            ReportDataSource rptdatasourceMejorUtilidad = new ReportDataSource("DataSetMejorUtilidad", mejorUtilidadDeVentas);
            ReportDataSource rptdatasourcePeorUtilidad = new ReportDataSource("DataSetPeorUtilidad", peorUtilidadDeVentas);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReporteUtilidad);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceMejorUtilidad);
            rptviewer.LocalReport.DataSources.Add(rptdatasourcePeorUtilidad);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }

        public ActionResult ReporteDeUtilidadDeVentaPorConcepto(string fechaInicio, string fechaFin, bool esReporteGlobal, string nombreCentroDeAtencion, [System.Web.Http.FromUri] int[] idsCentrosAtencion, [System.Web.Http.FromUri] int[] idsFamilia)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<CostoUtilidadPorConcepto> reporteDeUtilidadDeVentas = operacionLogica.ObtenerReporteDeUtilidadDeVentasPorConcepto(fechaDesde, fechaHasta, esReporteGlobal, idsCentrosAtencion, idsFamilia);

            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreReporte = new ReportParameter("NombreReporte", "REPORTE DE UTILIDAD POR CONCEPTO");
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", esReporteGlobal ? ObtenerSede().Nombre : nombreCentroDeAtencion);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/ReporteUtilidadVentaPorConcepto.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreReporte, nombreCentroAtencion, empleadoSede, logoSede, fechaActualSistema });

            ReportDataSource rptdatasourceReporteUtilidad = new ReportDataSource("DataSetReporteUtilidad", reporteDeUtilidadDeVentas);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReporteUtilidad);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }

        #endregion

        #region REPORTES DE FINANZAS
        public ActionResult ReporteDeCaja(string fechaInicio, string fechaFin, bool esReporteGlobal, string nombreCentroDeAtencion, [System.Web.Http.FromUri] int[] idsCentrosAtencion)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<Resumen_Movimiento_Caja> resumenReporteDeCaja = operacionLogica.ObtenerSaldosIniciales(fechaDesde, esReporteGlobal, idsCentrosAtencion);
            List<Movimiento_Caja> movimientosDeCaja = operacionLogica.ObtenerReporteDeCaja(fechaDesde, fechaHasta, esReporteGlobal, idsCentrosAtencion);
            var cajasVigentes = centroDeAtencionLogica.ObtenerCajasVigentes();
            List<Movimiento_Caja> reporteDeCaja = Movimiento_Caja.Convert(resumenReporteDeCaja, movimientosDeCaja, cajasVigentes);

            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", esReporteGlobal ? ObtenerSede().Nombre : nombreCentroDeAtencion);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/ReporteDeCaja.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, empleadoSede, logoSede, fechaActualSistema });

            ReportDataSource rptdatasourceReporteCaja = new ReportDataSource("DataSetReporteCaja", reporteDeCaja);
            ReportDataSource rptdatasourceResumenReporteCaja = new ReportDataSource("DataSetResumenReporteCaja", resumenReporteDeCaja);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReporteCaja);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenReporteCaja);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }

        #endregion
        #region REPORTE DE PUNTOS
        public ActionResult ReporteDePuntosCanjeados(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCentrosAtencion)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<Reporte_Puntos_Canjeados> reportePuntosCanjeados = operacionLogica.ObtenerReporteDePuntosCanjeados(fechaDesde, fechaHasta, idsCentrosAtencion);
            List<Reporte_Puntos_Canjeados> reporteIngresoDineroConPuntos = reportePuntosCanjeados.Where(r => r.EsIngreso).ToList();
            List<Reporte_Puntos_Canjeados> reporteSalidaDineroConPuntos = reportePuntosCanjeados.Where(r => !r.EsIngreso).ToList();
            List<Resumen_Puntos_Canjeados> resumenPuntosCanjeados = Resumen_Puntos_Canjeados.Convert(reportePuntosCanjeados);

            var rptviewer = new ReportViewer();

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);

            var sede = ObtenerSede();
            DateTime fechaActual =DateTimeUtil.FechaActual();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/ReportePuntosCanjeados.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, empleadoSede, logoSede, fechaActualSistema });

            ReportDataSource rptdatasourceResumenPuntosCanjeados = new ReportDataSource("DataSetResumenPuntosCanjeados", resumenPuntosCanjeados);
            ReportDataSource rptdatasourceIngresoDineroConPuntos = new ReportDataSource("DataSetIngresoDineroConPuntos", reporteIngresoDineroConPuntos);
            ReportDataSource rptdatasourceSalidaDineroConPuntos = new ReportDataSource("DataSetSalidaDineroConPuntos", reporteSalidaDineroConPuntos);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenPuntosCanjeados);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceIngresoDineroConPuntos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSalidaDineroConPuntos);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }

        public ActionResult ReporteDePuntosPendientes()
        {

            List<Reporte_Puntos_Pendientes> reportePuntosPendientes = operacionLogica.ObtenerReporteDePuntosPendientes();

            var rptviewer = new ReportViewer();
            var sede = ObtenerSede();

            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", sede.Nombre);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/ReportePuntosPendientes.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, empleadoSede, logoSede, fechaActualSistema });

            ReportDataSource rptdatasourceReportePuntosPendientes = new ReportDataSource("DataSetReportePuntosPendientes", reportePuntosPendientes);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReportePuntosPendientes);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");
        }
        #endregion

        public async Task EnviarCorreoElectronicoConReportesAutomaticamente()
        {
            EstablecimientoComercialExtendidoConLogo sede = sedeLogica.ObtenerSedeConLogo();

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-PE");
                var ordenMascara = AplicacionSettings.Default.MascaraDeReporteMensualDeEnvioAutomatico;
                var mascaraList = ordenMascara.ToCharArray();
                string asunto = "Reporte mensual de " + sede.DocumentoIdentidad + " \" " + sede.Nombre + " \" ";
                string cuerpo = @"<html>
                      <body>
                      <p>Reportes mensuales:</p> 
                      ";
                for (int i = 0; i < mascaraList.Count(); i++)
                {
                    if (mascaraList[i] == '1')
                    {
                        cuerpo += @"<p> - " + Diccionario.ValoresMascaraReporteMensualDeEnvioAutomatico.Single(v => v.Key == (i + 1)).Value + "</p> \n ";
                    }
                }
                cuerpo += @"<p>Reportes adjuntados,</p>
                      <p>Saludos.</p>
                      <p>SIGES (Sistema de Gestion Comercial), Un producto de Tech Solutions Perú<br>www.siges.tsolperu.com</br></p>
                      </body>
                      </html>";
                List<string> correosDestino = AplicacionSettings.Default.CorreosDestinoDelEnvioAutomaticoDeReportes.Split(';').ToList();
                List<Attachment> adjuntos = await GenerarReportesAdjuntos(sede, mascaraList);

                //Envio del correo electronico
                mailer.Send(asunto, cuerpo, correosDestino, AplicacionSettings.Default.ToMailDefault, adjuntos);
                //Envio de correo de envio de correo electronicos
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionHangfireReportes, "Reportes de " + sede.Nombre + " enviados correctamente a los correos destino.", cuerpo);
            }
            catch (Exception e)
            {
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionHangfireReportes, "ERROR AL ENVIAR LOS REPORTES DE " + sede.Nombre, "Enviar los reportes de: " + sede.Nombre + "\r\n" + "El error es el siguiente: " + e);
            }
        }

        public async Task<List<Attachment>> GenerarReportesAdjuntos(EstablecimientoComercialExtendidoConLogo sede, char[] mascaraList)
        {
            try
            {
                List<Attachment> attachments = new List<Attachment>();
                DateTime fechaActual = operacionLogica.FechaActual().Date;
                DateTime fechaDeReporte = fechaActual.AddMonths(-1);
                DateTime fechaDesde = new DateTime(fechaDeReporte.Year, fechaDeReporte.Month, 1);
                DateTime fechaHasta = fechaDesde.AddMonths(1).AddMilliseconds(-1);
                string nombreDePeriodo = fechaDeReporte.Year.ToString() + fechaDeReporte.Month.ToString("D2");
                Periodo periodo = librosElectronicosLogica.ObtenerPeriodo(nombreDePeriodo);

                if (mascaraList[0] == '1')
                {
                    //Obtener reporte de todos los puntos de venta por comprobante
                    List<CentroDeAtencionExtendido> listaDePuntosDeVenta = centroDeAtencionLogica.ObtenerPuntosDeVentaVigentes();
                    int[] idsPuntosDeVentas = listaDePuntosDeVenta.Select(l => l.Id).ToArray();
                    var rptviewer = GenerarReporteDeVentaDeLosPuntosDeVentaPorComprobante(sede, fechaDesde, fechaHasta, idsPuntosDeVentas, false);
                    string fileNameContent = "PuntosDeVentaPorComprobante_" + sede.DocumentoIdentidad;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                if (mascaraList[1] == '1')
                {
                    List<ReportViewerComprimir> reportesAcomprimir = new List<ReportViewerComprimir>();
                    //Obtener reporte de serie por comprobante
                    List<int> idsComprobante = new List<int> { MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito, MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito };
                    var tiposDeComprobanteParaVenta = await configuracionLogica.ObtenerTiposDeComprobanteParaVenta();
                    List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(tiposDeComprobanteParaVenta);

                    foreach (var id in idsComprobante)
                    {
                        var comprobante = comprobantes.SingleOrDefault(c => c.TipoComprobante.Id == id);
                        if (comprobante != null)
                        {
                            var seriesDeComprobante = comprobante.Series;
                            foreach (var serie in seriesDeComprobante)
                            {
                                reportesAcomprimir.Add(new ReportViewerComprimir(GenerarReporteIntervaloFechasPorComprobante(sede.Nombre, sede, fechaDesde, fechaHasta, serie.Id, false), "SeriePorComprobante_" + serie.Nombre + "_" + sede.DocumentoIdentidad));
                            }
                        }
                    }
                    attachments.Add(ComprimirAdjuntosExcel(reportesAcomprimir, "SeriePorComprobante_" + periodo.anio + "-" + periodo.mes));
                }
                //if (mascaraList[2] == '1' || mascaraList[3] == '1')
                //{
                //    //Eliminar y generar los libros electronicos
                //    EliminarYGenerarLibrosElectronicos(7, periodo.id);
                //}
                if (mascaraList[2] == '1')
                {
                    var librosElectronicos = maestroLogica.ObtenerDetalleMaestroLibroElectronico();
                    var idsLibrosElectronicosSeleccionados = new List<int> { MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas };
                    var librosElectronicosSeleccionados = librosElectronicos.Where(le => idsLibrosElectronicosSeleccionados.Contains(le.Id)).ToList();
                    string fileNameZip = sede.DocumentoIdentidad + "_LE" + periodo.nombre + ".zip";
                    //Generamos el reporte en PLE TXT comprimido
                    var compressedBytes = generador.GenerarLibrosElectronicosCSVComprimido(7, sede, periodo, librosElectronicosSeleccionados);
                    attachments.Add(new Attachment(new MemoryStream(compressedBytes), fileNameZip, "application/zip"));
                }
                if (mascaraList[3] == '1')
                {
                    var librosElectronicos = maestroLogica.ObtenerDetalleMaestroLibroElectronico();
                    var idsLibrosElectronicosSeleccionados = new List<int> { MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas };
                    var librosElectronicosSeleccionados = librosElectronicos.Where(le => idsLibrosElectronicosSeleccionados.Contains(le.Id)).ToList();
                    string fileNameZip = sede.DocumentoIdentidad + "_LE" + periodo.nombre + ".zip";
                    //Generamos el reporte en PLE TXT comprimido
                    var compressedBytes = generador.GenerarLibrosElectronicosTXTComprimido(7, sede, periodo, librosElectronicosSeleccionados);
                    attachments.Add(new Attachment(new MemoryStream(compressedBytes), fileNameZip, "application/zip"));
                }
                if (mascaraList[4] == '1')
                {
                    //Obtener reporte en formato adsoft
                    var rptviewer = generador.GenerarReporteDeVentaFormatoAdsoft(7, sede, periodo, false, librosElectronicosAdsoftLogica, "");
                    string fileNameContent = "Adsoft_" + sede.DocumentoIdentidad + "_" + sede.Nombre + "_" + periodo.anio + "-" + periodo.mes;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                if (mascaraList[5] == '1')
                {
                    List<ReportViewerComprimir> reportesAcomprimir = new List<ReportViewerComprimir>();
                    //Obtener reporte en formato foxcom
                    reportesAcomprimir.Add(new ReportViewerComprimir(generador.GenerarReporteDeVentaFormatoFoxcontBoletaVentaFactura(7, sede, periodo, false, librosElectronicosFoxcontLogica, ""), periodo.anio + "-" + periodo.mes));
                    //Obtener reporte en formato foxcom de notas de debito y credito
                    reportesAcomprimir.Add(new ReportViewerComprimir(generador.GenerarReporteDeVentaFormatoFoxcontNotaCreditoDebito(7, sede, periodo, false, librosElectronicosFoxcontLogica, ""), periodo.anio + "-" + periodo.mes));
                    attachments.Add(ComprimirAdjuntosExcel(reportesAcomprimir, "ReportesFoxCont_" + periodo.anio + "-" + periodo.mes));
                }
                if (mascaraList[6] == '1')
                {
                    //Obtener reporte de puntos de venta por comprobante
                    List<CentroDeAtencionExtendido> listaDePuntosDeVenta = centroDeAtencionLogica.ObtenerPuntosDeVentaVigentes();
                    int[] idsPuntosDeVentas = listaDePuntosDeVenta.Select(l => l.Id).ToArray();
                    var rptviewer = GenerarReportePorNumeroDeComprobanteConIcbper(sede, fechaDesde, fechaHasta, idsPuntosDeVentas, false);
                    string fileNameContent = "PuntosDeVentaPorComprobanteConICBPER_" + sede.DocumentoIdentidad;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                if (mascaraList[7] == '1')
                {
                    var rptviewer = generador.GenerarReporteDeVentasEIngresosSinConcepto(7, sede, periodo, false, operacionLogica, "");
                    string fileNameContent = "LibroElectroncioDeVentaSinConcepto_" + sede.DocumentoIdentidad;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                if (mascaraList[8] == '1')
                {
                    var rptviewer = generador.GenerarReporteDeVentasEIngresos(7, sede, periodo, false, operacionLogica, "");
                    string fileNameContent = "ReporteDeVentasDeInsumosControlados_" + sede.DocumentoIdentidad;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                if (mascaraList[9] == '1')
                {
                    //Obtener reporte de ventas en excel
                    var rptviewer = generador.GenerarReporteDeVentaEnExcel(7, sede, periodo, false, "");
                    string fileNameContent = "RegistroDeVentas_" + sede.DocumentoIdentidad + "_" + sede.Nombre + "_" + periodo.anio + "-" + periodo.mes;
                    attachments.Add(ComprimirAdjuntoExcel(rptviewer, fileNameContent));
                }
                return attachments;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar los reportes para adjuntar", e);
            }
        }

        public Attachment ComprimirAdjuntoExcel(ReportViewer rptviewer, string fileNameContent)
        {
            string filenameExcel = string.Format("{0}.{1}", fileNameContent, "xls").Replace(" ", "");
            byte[] bytesReporte = rptviewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streamids, out Warning[] warnings);
            byte[] compressedBytes;
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(filenameExcel, CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(bytesReporte))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                        fileToCompressStream.Close();
                    }
                }
                compressedBytes = outStream.ToArray();
            }
            string fileNameZip = string.Format("{0}.{1}", fileNameContent, "zip").Replace(" ", "");
            return new Attachment(new MemoryStream(compressedBytes), fileNameZip, "application/zip");
        }

        public Attachment ComprimirAdjuntosExcel(List<ReportViewerComprimir> reportesViewers, string fileNameContent)
        {
            byte[] compressedBytes;
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var rptviewer in reportesViewers)
                    {
                        string filenameExcel = string.Format("{0}.{1}", rptviewer.Nombre, "xls").Replace(" ", "");
                        byte[] bytesReporte = rptviewer.ReportViewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string fileNameExtension, out string[] streamids, out Warning[] warnings);
                        var fileInArchive = archive.CreateEntry(filenameExcel, CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(bytesReporte))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }
                    }

                }
                compressedBytes = outStream.ToArray();
            }
            string fileNameZip = string.Format("{0}.{1}", fileNameContent, "zip").Replace(" ", "");
            return new Attachment(new MemoryStream(compressedBytes), fileNameZip, "application/zip");
        }


        //REPORTE DE VENTAS POR CONCEPTO BASICO POR VENDEDOR - cambio a ventas por categoria
        public ActionResult ReporteDeVentasPorConceptoBasicoPorVendedor(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int idVendedor, string nombreVendedor)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<Resumen_Detalles_Consolidado_Por_Vendedor> resumenPorConceptoNegocioPorPrecioUnitarioPorVendedor = operacionLogica.ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorPrecioUnitarioPorVendedor(idVendedor, fechaDesde, fechaHasta);
            List<Resumen_Detalles_Consolidado_Por_Vendedor> resumenPorConceptoNegocioPorVendedor = operacionLogica.ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorVendedor(idVendedor, fechaDesde, fechaHasta);
            List<Resumen_Detalles_Consolidado_Por_Vendedor> resumenPorConceptoBasicoPorVendedor = operacionLogica.ObtenerResumenDetallesConsolidadoPorConceptoBasicoPorVendedor(idVendedor, fechaDesde, fechaHasta);

            //parámetro
            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreEmpleado = new ReportParameter("NombreVendedor", nombreVendedor);

            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorCategoria.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreEmpleado, logoSede, nombreCentroAtencion, fechaActualSistema, empleadoSede });

            ReportDataSource rptdatasourceConceptoNegocioPorPrecioUnitarioPorVendedor = new ReportDataSource("DataSetPorConceptoNegocioPorPrecioUnitarioPorVendedor", resumenPorConceptoNegocioPorPrecioUnitarioPorVendedor);
            ReportDataSource rptdatasourceConceptoNegocioPorVendedor = new ReportDataSource("DataSetPorConceptoNegocioPorVendedor", resumenPorConceptoNegocioPorVendedor);
            ReportDataSource rptdatasourceConceptoBasicoPorVendedor = new ReportDataSource("DataSetPorConceptoBasicoPorVendedor", resumenPorConceptoBasicoPorVendedor);

            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoNegocioPorPrecioUnitarioPorVendedor);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoNegocioPorVendedor);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoBasicoPorVendedor);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DE VENTAS CONSOLIDADAS POR CONCEPTO BASICO POR VENDEDORES cambiado ventas por categoria y vendedor
        public ActionResult ReporteDeVentasConsolidadoPorConceptoBasicoPorVendedores(string fechaInicio, string fechaFin)
        {
            //VentaPorConceptoDelVendedorAdministrador
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<Resumen_Detalles_Consolidado_Por_Vendedor> resumenPorConceptoBasicoDeVendedores = operacionLogica.ObtenerResumenDeVentasAgrupadasPorFamilia(fechaDesde, fechaHasta);
            List<Resumen_Detalles_Consolidado_Por_Vendedor> resumenPorConceptoBasicoPorVendedores = operacionLogica.ObtenerResumenDeVentasAgrupadasPorFamiliaYVendedor(fechaDesde, fechaHasta);

            //parámetros
            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);


            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorCategoriaYVendedor.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreSede, logoSede, fechaActualSistema, empleadoSede, nombreCentroAtencion });

            ReportDataSource rptdatasourceConceptoBasicoDeVendedores = new ReportDataSource("DataSetPorConceptoBasicoDeVendedores", resumenPorConceptoBasicoDeVendedores);
            ReportDataSource rptdatasourceConceptoBasicoPorVendedores = new ReportDataSource("DataSetPorConceptoBasicoPorVendedores", resumenPorConceptoBasicoPorVendedores);

            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoBasicoDeVendedores);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoBasicoPorVendedores);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DE VENTAS CONSOLIDADAS AL CONTADO Y AL CREDITO POR VENDEDORES
        public ActionResult ReporteDeVentasConsolidadoAlContadoAlCreditoPorVendedores(string fechaInicio, string fechaFin)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> resumenPorConceptoPorVendedorContadoCredito = operacionLogica.ObtenerResumenVentasPorConceptoPorVendedorContadoCredito(fechaDesde, fechaHasta);
            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasAlContadoYCreditoPorVendedores.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, fechaActualSistema, logoSede, empleadoSede });
            ReportDataSource rptdatasourceConceptoPorVendedorContadoCredito = new ReportDataSource("DataSetPorConceptoPorVendedorContadoCredito", resumenPorConceptoPorVendedorContadoCredito);
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceConceptoPorVendedorContadoCredito);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }


        public ActionResult VentasPorCaracteristica(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCentrosAtencion, string nombresCentrosAtencion, int idCaracteristica, string nombreCaracteristicaString)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombresCentrosAtencion);
            ReportParameter nombreCaracteristica = new ReportParameter("NombreCaracteristica", nombreCaracteristicaString);
            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorCaracteristica.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, nombreCentroAtencion, nombreCaracteristica });
            List<ItemConGrupoOperacionComercial> detalles = operacionLogica.ObtenerVentasConfirmadasPorCaracteristicaYConcepto(fechaDesde, fechaHasta, idsCentrosAtencion, idCaracteristica).OrderByDescending(d => d.Importe).ToList();
            List<ItemOperacionComercial> resumen = detalles.GroupBy(d => new { d.IdGrupo, d.NombreGrupo }).Select(d => new ItemOperacionComercial() { IdItem = d.Key.IdGrupo, NombreItem = d.Key.NombreGrupo, Cantidad = d.Sum(ds => ds.Cantidad), Importe = d.Sum(ds => ds.Importe) }).OrderByDescending(d => d.Importe).ToList();
            List<ItemOperacionComercial> top20 = resumen.Take(20).ToList();
            ReportDataSource detallesDataSet = new ReportDataSource("DetallesDataSet", detalles);
            ReportDataSource resumenDataSet = new ReportDataSource("ResumenDataSet", resumen);
            ReportDataSource top20DataSet = new ReportDataSource("Top20DataSet", top20);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet, resumenDataSet, top20DataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }


        public async Task<ActionResult> VentasDetalladasPorConceptoCaracteristicasFormaPago(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCaracteristicas, int idPuntoVenta, string nombrePuntoVenta)
        {
            idsCaracteristicas = idsCaracteristicas.Where(i => i != 0).ToArray();
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombrePuntoVenta);
            ReportParameter parametroCantidadCaracteristicas = new ReportParameter("CantidadCaracteristicas", idsCaracteristicas.Length.ToString());
            var caracteristicas = await maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroCaracteristicaConcepto);
            string[] nombres = { "", "", "", "", "", "" };
            for (int i = 0; i < idsCaracteristicas.Take(6).ToArray().Length; i++)
            {
                nombres[i] = caracteristicas.Single(c => c.id == idsCaracteristicas[i]).nombre;

            }
            ReportParameter parametroNombresCaracteristicas = new ReportParameter("NombresCaracteristicas", nombres);

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasDetalladasPorConceptoCaracteristicasModoPago.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametroNombreCentroAtencion, parametroCantidadCaracteristicas, parametroNombresCaracteristicas });

            List<ItemDetalladoOperacionComercial> detalles = operacionLogica.ObtenerVentasDetalladasPorConceptoCaracteristicasModoPago(fechaDesde, fechaHasta, idPuntoVenta, idsCaracteristicas).OrderBy(d => d.Fecha).ToList();

            ReportDataSource detallesDataSet = new ReportDataSource("DetallesDataSet", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public JsonResult VentasPorFamiliaCaracteristica(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCentrosAtencion, string nombresCentrosAtencion, int idFamilia, string nombreFamilia, int idCaracteristica, string nombreCaracteristica, int idValorCaracteristica, string nombreValorCaracteristica)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreCentroAtencion = new ReportParameter("PuntosVenta", nombresCentrosAtencion);
            ReportParameter parametroNombreFamilia = new ReportParameter("Familia", nombreFamilia);
            ReportParameter parametroNombreCaracteristica = new ReportParameter("Caracteristica", nombreCaracteristica);
            ReportParameter parametroNombreValorCaracteristica = new ReportParameter("ValorCaracteristica", nombreValorCaracteristica);
            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/ReporteVentaPorFamiliaCaracteristica.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreCentroAtencion, parametroNombreFamilia, parametroNombreCaracteristica, parametroNombreValorCaracteristica });

            List<VentaConceptoCliente> detalles = operacionLogica.ObtenerVentasPorFamiliaCaracteristica(fechaDesde, fechaHasta, idsCentrosAtencion, idFamilia, idCaracteristica, idValorCaracteristica);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetReporteVentasPorFamiliaCaracteristica", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            return DescargarDocumento(rptviewer, "ReporteDeVentasPorFamiliaCaracteristica");
        }
        public JsonResult VentasPorValorCaracteristica(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCentrosAtencion, string nombresCentrosAtencion, int idCaracteristica, string nombreCaracteristica, int idValorCaracteristica, string nombreValorCaracteristica)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreCentroAtencion = new ReportParameter("PuntosVenta", nombresCentrosAtencion);
            ReportParameter parametroNombreCaracteristica = new ReportParameter("Caracteristica", nombreCaracteristica);
            ReportParameter parametroNombreValorCaracteristica = new ReportParameter("ValorCaracteristica", nombreValorCaracteristica);
            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/ReporteVentaPorCaracteristica.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, /*parametrosBasicos.NombreEmpresa, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.EmpleadoSede,*/ nombreCentroAtencion, parametroNombreCaracteristica, parametroNombreValorCaracteristica });

            List<VentaConceptoCliente> detalles = operacionLogica.ObtenerVentasPorCaracteristica(fechaDesde, fechaHasta, idsCentrosAtencion, idCaracteristica, idValorCaracteristica);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetReporteVentasPorCaracteristica", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            return DescargarDocumento(rptviewer, "ReporteDeVentasPorCaracteristica");
        }
        public JsonResult VentasPorConcepto(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsCentrosAtencion, string nombresCentrosAtencion, int idConcepto, string nombreConcepto)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreCentroAtencion = new ReportParameter("PuntosVenta", nombresCentrosAtencion);
            ReportParameter parametroNombreConcepto = new ReportParameter("Concepto", nombreConcepto);
            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/ReporteVentaPorConcepto.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, /*parametrosBasicos.NombreEmpresa, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.EmpleadoSede,*/ nombreCentroAtencion, parametroNombreConcepto });

            List<VentaConceptoCliente> detalles = operacionLogica.ObtenerVentasPorConcepto(fechaDesde, fechaHasta, idsCentrosAtencion, idConcepto);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetReporteVentasPorConcepto", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            return DescargarDocumento(rptviewer, "ReporteDeVentasPorConcepto");

        }

        public ActionResult InvalidacionesAdministrador(string fechaInicio, string fechaFin, int  idPuntoVenta, string nombrePuntoVenta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombrePuntoVenta);

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/Invalidaciones.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametroNombreCentroAtencion });

            List<ResumenDeTransaccionGeneral> detalles = operacionLogica.ObtenerInvalidacionesDeOperacionesDeVenta(fechaDesde, fechaHasta, idPuntoVenta);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetInvalidaciones", detalles);
            
            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");

        }

        public ActionResult InvalidacionesVendedor(string fechaInicio, string fechaFin)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);


            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/Invalidaciones.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametroNombreCentroAtencion });

            List<ResumenDeTransaccionGeneral> detalles = operacionLogica.ObtenerInvalidacionesDeOperacionesDeVenta(fechaDesde, fechaHasta, ProfileData().IdCentroDeAtencionSeleccionado);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetInvalidaciones", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }


        public ActionResult NotasCredito(string fechaInicio, string fechaFin, int idPuntoVenta, string nombrePuntoVenta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombrePuntoVenta);

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/NotasCredito.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametroNombreCentroAtencion });

            List<ResumenDeTransaccionGeneral> detalles = operacionLogica.ObtenerNotasCreditoDeOperacionesDeVenta(fechaDesde, fechaHasta, idPuntoVenta);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetNotasCredito", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");

        }

        public ActionResult NotasDebito(string fechaInicio, string fechaFin, int idPuntoVenta, string nombrePuntoVenta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", nombrePuntoVenta);

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/NotasDebito.rdlc";
            var parametrosBasicos = ObtenerParametrosBasicos();
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, parametrosBasicos.NombreSede, parametrosBasicos.FechaActualSistema, parametrosBasicos.LogoSede, parametrosBasicos.Usuario, parametroNombreCentroAtencion });

            List<ResumenDeTransaccionGeneral> detalles = operacionLogica.ObtenerNotasDebitoDeOperacionesDeVenta(fechaDesde, fechaHasta, idPuntoVenta);
            ReportDataSource detallesDataSet = new ReportDataSource("DataSetNotasDebito", detalles);

            DefinirReportViewer(rptviewer, new List<ReportDataSource> { detallesDataSet });
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");

        }

        public JsonResult DescargarDocumento(ReportViewer reportViewer, string nombreReporte)
        {
            EstablecimientoComercial sede = this.ObtenerSede();
            DateTime fechaActual = DateTimeUtil.FechaActual();
            string filenameContent = nombreReporte + "_" + fechaActual.ToString("dd") + fechaActual.ToString("MM") + fechaActual.ToString("yyyy");
            string filename = string.Format("{0}.{1}", filenameContent, "xls");
            filename = filename.Replace(" ", "");

            byte[] bytes = reportViewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            return Json("Operacion realizada con exito");
        }
    }
}
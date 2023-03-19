using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Finanza;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class FinanzaReportes_MovimientosCajasController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IFinanzaReporte_Logica finanzaReportingLogica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencionLogica;

        public FinanzaReportes_MovimientosCajasController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            finanzaReportingLogica = Dependencia.Resolve<IFinanzaReporte_Logica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
        }
        public ActionResult MovimientosCajas_CobrosClientes(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, [System.Web.Http.FromUri] int[] idsCajas, string nombresCajas, bool todosLosClientes, [System.Web.Http.FromUri] int[] idsClientes, string nombresClientes)
        {
            var rptviewer = GenerarMovimientosCajas_CobrosClientes(fechaDesde, fechaHasta, todasLasCajas, idsCajas, nombresCajas, todosLosClientes, idsClientes, nombresClientes, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarMovimientosCajas_CobrosClientes(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, string nombresCajas, bool todosLosClientes, int[] idsClientes, string nombresClientes, bool fromRequest)
        {
            try
            {
                var detalladoPagos = operacionLogica.ObtenerPagosDeClientes(fechaDesde, fechaHasta, todasLasCajas, idsCajas, todosLosClientes, idsClientes);
                var resumenPagos = Reporte_Pago.Resumen(detalladoPagos);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter nombreDeReporte = new ReportParameter("NombreReporte", "COBROS DE CLIENTES");
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Finanzas/PagoClienteYProveedor.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                nombreDeReporte,
                parametroFechaDesde,
                parametroFechaHasta
                });
                ReportDataSource rptdatasourceResumenPagos = new ReportDataSource("DataSetResumenPagos", resumenPagos);
                ReportDataSource rptdatasourceDetalladoPagos = new ReportDataSource("DataSetDetallePagos", detalladoPagos);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenPagos);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoPagos);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de cobros de clientes", e);
            }
        }

        public ActionResult MovimientosCajas_PagosProveedores(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, [System.Web.Http.FromUri] int[] idsCajas, string nombresCajas, bool todosLosProveedores, [System.Web.Http.FromUri] int[] idsProveedores, string nombresProveedores)
        {
            var rptviewer = GenerarMovimientosCajas_PagosProveedores(fechaDesde, fechaHasta, todasLasCajas, idsCajas, nombresCajas, todosLosProveedores, idsProveedores, nombresProveedores, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarMovimientosCajas_PagosProveedores(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, string nombresCajas, bool todosLosProveedores, int[] idsProveedores, string nombresProveedores, bool fromRequest)
        {
            try
            {
                var detalladoPagos = operacionLogica.ObtenerPagosAProveedores(fechaDesde, fechaHasta, todasLasCajas, idsCajas, todosLosProveedores, idsProveedores);
                var resumenPagos = Reporte_Pago.Resumen(detalladoPagos);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter nombreDeReporte = new ReportParameter("NombreReporte", "PAGOS DE PROVEEDORES");
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Finanzas/PagoClienteYProveedor.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                nombreDeReporte,
                parametroFechaDesde,
                parametroFechaHasta
                });
                ReportDataSource rptdatasourceResumenPagos = new ReportDataSource("DataSetResumenPagos", resumenPagos);
                ReportDataSource rptdatasourceDetalladoPagos = new ReportDataSource("DataSetDetallePagos", detalladoPagos);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenPagos);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoPagos);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de pagos de proveedores", e);
            }
        }
     
        /*
        public ActionResult GenerarMovimientosCajas_PagosProveedores(string fechaInicio, string fechaFin, bool todasLasCajas, int[] idsCajas, string nombresCajas, bool todosLosClientes, int[] idsClientes, string nombresClientes, bool fromRequest)
        {
            {
                try
                {

                    //DECLARACION DE VARIABLES
                    DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                    DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

                    List<Reporte_Pago> resumenPagos = null, detalladoPagos = null;
                    ReportParameter nombreDeReporte = null;

                    string _nombresCentrosAtencion = "";

                    foreach (var id in idsPuntosDeCompraOVenta)
                    {
                        string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(id);
                        _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
                    }

                    if (esCliente)
                    {
                        nombreDeReporte = new ReportParameter("NombreReporte", "PAGOS DE CLIENTE");
                        detalladoPagos = operacionLogica.ObtenerPagosDeClientes(fechaDesde, fechaHasta, idsPuntosDeCompraOVenta, idsProveedoresOClientes);
                        resumenPagos = Reporte_Pago.Resumen(detalladoPagos);

                    }
                    else
                    {
                        nombreDeReporte = new ReportParameter("NombreReporte", "PAGOS AL PROVEEDOR");
                        detalladoPagos = operacionLogica.ObtenerPagosAProveedores(fechaDesde, fechaHasta, idsPuntosDeCompraOVenta, idsProveedoresOClientes);
                        resumenPagos = Reporte_Pago.Resumen(detalladoPagos);

                    }

                    ReportParameter nombreDeEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
                    ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
                    ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                    ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                    var sede = ObtenerSede();
                    string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                    ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                    ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                    DateTime fechaActual = DateTimeUtil.FechaActual();
                    ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                    var rptviewer = new ReportViewer();
                    rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/PagoClienteYProveedor.rdlc";
                    rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreDeReporte, nombreDeEmpresa, parametroFechaDesde, parametroFechaHasta, logoSede, empleadoSede, fechaActualSistema });
                    ReportDataSource rptdatasourceResumenPagos = new ReportDataSource("DataSetResumenPagos", resumenPagos);
                    ReportDataSource rptdatasourceDetalladoPagos = new ReportDataSource("DataSetDetallePagos", detalladoPagos);

                    rptviewer.ProcessingMode = ProcessingMode.Local;
                    rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenPagos);
                    rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoPagos);

                    rptviewer.SizeToReportContent = true;
                    rptviewer.Width = Unit.Percentage(100);
                    rptviewer.Height = Unit.Percentage(100);

                    ViewBag.ReportViewer = rptviewer;


                    return View("VisualizadorReporte");
                }
                catch (Exception e)
                {
                    throw new ControllerException("Error al generar el reporte de pagos", e);
                }
            }
        */
        }
    }
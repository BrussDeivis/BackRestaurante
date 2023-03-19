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
    public partial class FinanzaReportes_PorCobrarPagarController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IFinanzaReporte_Logica finanzaReportingLogica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencionLogica;

        public FinanzaReportes_PorCobrarPagarController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            finanzaReportingLogica = Dependencia.Resolve<IFinanzaReporte_Logica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
        }

        public ActionResult PorCobrarPagar_PorCobrarClientes()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            fechaActual = fechaActual.AddMilliseconds(-fechaActual.Millisecond);

            List<Cuota> DeudasDeCliente = operacionLogica.ObtenerReporteDeudasDeCliente();
            List<DeudaClienteViewModel> reporteDeDeudaDeClienteDetalle = DeudaClienteViewModel.Convert(DeudasDeCliente);
            List<DeudaClienteViewModel> reporteDeDeudaDeClienteResumen = DeudaClienteViewModel.Resumen(reporteDeDeudaDeClienteDetalle);

            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaActual = new ReportParameter("FechaActual", fechaActual.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/DeudaDeCliente.rdlc";

            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaActual, nombreEmpresa, nombreCentroAtencion, logoSede, empleadoSede });
            ReportDataSource rptdatasourceReporteResumen = new ReportDataSource("DataSetResumenDeudaCliente", reporteDeDeudaDeClienteResumen);
            ReportDataSource rptdatasourcereporteDetalle = new ReportDataSource("DataSetDetalleDeudaCliente", reporteDeDeudaDeClienteDetalle);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReporteResumen);
            rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteDetalle);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult PorCobrarPagar_PorPagarProveedores()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            fechaActual = fechaActual.AddMilliseconds(-fechaActual.Millisecond);

            List<Cuota> DeudasAProveedor = operacionLogica.ObtenerReporteDeudasAProveedor();
            List<DeudaProveedorViewModel> reporteDeDeudaAProveedorDetalle = DeudaProveedorViewModel.Convert(DeudasAProveedor);
            List<DeudaProveedorViewModel> reporteDeDeudaAProveedorResumen = DeudaProveedorViewModel.Resumen(reporteDeDeudaAProveedorDetalle);

            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaActual = new ReportParameter("FechaActual", fechaActual.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);
            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/DeudaAlProveedor.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaActual, nombreEmpresa, nombreCentroAtencion, empleadoSede, logoSede });
            ReportDataSource rptdatasourceReporteResumen = new ReportDataSource("DataSetResumenDeudaProveedor", reporteDeDeudaAProveedorResumen);
            ReportDataSource rptdatasourcereporteDetalle = new ReportDataSource("DataSetDetalleDeudaProveedor", reporteDeDeudaAProveedorDetalle);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceReporteResumen);
            rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteDetalle);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult PorCobrarPagar_PorCobrarPorCliente(bool todosLosClientes, [System.Web.Http.FromUri] int[] idsClientes, string nombresClientes)
        {
            var rptviewer = GenerarPorCobrarPagar_PorCobrarPorCliente(todosLosClientes, idsClientes, nombresClientes, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarPorCobrarPagar_PorCobrarPorCliente(bool todosLosClientes, int[] idsClientes, string nombresClientes, bool fromRequest)
        {
            try
            {
                var detalladoDeudas = operacionLogica.ObtenerDeudasDeClientes(todosLosClientes, idsClientes);
                var resumenDeudas = Reporte_Deuda.Resumen(detalladoDeudas);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter nombreDeReporte = new ReportParameter("NombreReporte", "POR COBRAR POR CLIENTES");
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Finanzas/DeudaClienteYProveedor.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                nombreDeReporte
                });
                ReportDataSource rptdatasourceResumenDeudas = new ReportDataSource("DataSetResumenDeudas", resumenDeudas);
                ReportDataSource rptdatasourceDetalladoDeudas = new ReportDataSource("DataSetDetalleDeudas", detalladoDeudas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenDeudas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoDeudas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de cobros por clientes", e);
            }
        }

        public ActionResult PorCobrarPagar_PorPagarPorProveedor(bool todosLosProveedores, [System.Web.Http.FromUri] int[] idsProveedores, string nombresProveedores)
        {
            var rptviewer = GenerarPorCobrarPagar_PorPagarPorProveedor(todosLosProveedores, idsProveedores, nombresProveedores, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarPorCobrarPagar_PorPagarPorProveedor(bool todosLosProveedores, int[] idsProveedores, string nombresProveedores, bool fromRequest)
        {
            try
            {
                var detalladoDeudas = operacionLogica.ObtenerDeudasDeClientes(todosLosProveedores, idsProveedores);
                var resumenDeudas = Reporte_Deuda.Resumen(detalladoDeudas);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter nombreDeReporte = new ReportParameter("NombreReporte", "POR PAGAR POR PROVEEDORES");
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Finanzas/DeudaClienteYProveedor.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                nombreDeReporte
                });
                ReportDataSource rptdatasourceResumenDeudas = new ReportDataSource("DataSetResumenDeudas", resumenDeudas);
                ReportDataSource rptdatasourceDetalladoDeudas = new ReportDataSource("DataSetDetalleDeudas", detalladoDeudas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenDeudas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoDeudas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el reporte de pagos por proveedores", e);
            }
        }
        /*   public ActionResult ReporteDeDeuda(bool esCliente, [System.Web.Http.FromUri] int[] idsPuntosDeCompraOVenta, [System.Web.Http.FromUri] int[] idsProveedoresOClientes)
           {
               try
               {
                   List<Reporte_Deuda> resumenDeudas = null, detalladoDeudas = null;
                   ReportParameter nombreDeReporte = null;
                   string _nombresCentrosAtencion = "";

                   foreach (var id in idsPuntosDeCompraOVenta)
                   {
                       string _nombreCentroAtencion = centroDeAtencionLogica.ObtenerNombreDeCentroDeAtencion(id);
                       _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
                   }

                   if (esCliente)
                   {
                       nombreDeReporte = new ReportParameter("NombreReporte", "DEUDAS DEL CLIENTE");

                       //DETALLADO
                       detalladoDeudas = operacionLogica.ObtenerDeudasDeClientes(idsPuntosDeCompraOVenta, idsProveedoresOClientes);

                       //RESUMEN
                       resumenDeudas = Reporte_Deuda.Resumen(detalladoDeudas);
                   }
                   else
                   {
                       nombreDeReporte = new ReportParameter("NombreReporte", "DEUDAS AL PROVEEDOR");

                       //DETALLADO
                       detalladoDeudas = operacionLogica.ObtenerDeudasAProveedores(idsPuntosDeCompraOVenta, idsProveedoresOClientes);

                       //RESUMEN
                       resumenDeudas = Reporte_Deuda.Resumen(detalladoDeudas);

                   }

                   //PARAMETROS
                   ReportParameter nombreDeEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
                   ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
                   //ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                   //ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                   var sede = ObtenerSede();
                   string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                   ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                   ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                   DateTime fechaActual = DateTimeUtil.FechaActual();
                   ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

                   var rptviewer = new ReportViewer();
                   //NOMBRE DEL REPORTE
                   rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/DeudaClienteYProveedor.rdlc";
                   //PARAMETROS DEL REPORTE
                   rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreCentroAtencion, nombreDeReporte, nombreDeEmpresa, empleadoSede, logoSede, fechaActualSistema });
                   //DATA SOURCE - RESUMEN
                   ReportDataSource rptdatasourceResumenDeudas = new ReportDataSource("DataSetResumenDeudas", resumenDeudas);
                   //DATA SOURCE - DETALLADO
                   ReportDataSource rptdatasourceDetalladoDeudas = new ReportDataSource("DataSetDetalleDeudas", detalladoDeudas);

                   //LOCAL
                   rptviewer.ProcessingMode = ProcessingMode.Local;
                   //AGREGAR DATA SOURCE - RESUMEN
                   rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenDeudas);

                   //AGREGAR DATA SOURCE - DETALLADO
                   rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoDeudas);

                   //TAMAÑOS
                   rptviewer.SizeToReportContent = true;
                   rptviewer.Width = Unit.Percentage(100);
                   rptviewer.Height = Unit.Percentage(100);

                   ViewBag.ReportViewer = rptviewer;
                   return View("VisualizadorReporte");
               }
               catch (Exception e)
               {
                   throw new ControllerException("Error al generar el reporte de deudas", e);
               }
           }*/
        public ActionResult PorCobrarPagar_PorCobrarGrupos(bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos)
        {
            var rptviewer = GenerarPorCobrarPagar_PorCobrarGrupos(todosLosGrupos, idsGrupos, nombresGrupos, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarPorCobrarPagar_PorCobrarGrupos(bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos, bool fromRequest)
        {
            List<OperacionGrupo> cuentasPorCobrarGrupos = finanzaReportingLogica.ObtenerCuentasPorCobrarGrupos(todosLosGrupos, idsGrupos);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", DateTimeUtil.FechaActual().ToString());
            ReportParameter parametroNombresGrupos = new ReportParameter("NombresGrupos", nombresGrupos ?? "Todos");
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Finanzas/PorCobrarPagar_PorCobrarGrupos.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFechaHasta,
                parametroNombresGrupos
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetCuentasPorCobrarGrupos", cuentasPorCobrarGrupos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }

        public ActionResult PorCobrarPagar_PorCobrarGrupoDetallado(int idGrupo, string nombreGrupo)
        {
            var rptviewer = GenerarPorCobrarPagar_PorCobrarGrupoDetallado(idGrupo, nombreGrupo, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarPorCobrarPagar_PorCobrarGrupoDetallado(int idGrupo, string nombreGrupo, bool fromRequest)
        {
            List<OperacionGrupoDetallado> cuentasPorCobrarGrupoDetallado = finanzaReportingLogica.ObtenerCuentasPorCobrarGrupoDetallado(idGrupo);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", DateTimeUtil.FechaActual().ToString());
            ReportParameter parametroNombreGrupo = new ReportParameter("NombreGrupo", nombreGrupo);
            ReportParameter parametroNombreResponsable = new ReportParameter("NombreResponsable", cuentasPorCobrarGrupoDetallado.FirstOrDefault().NombreResponsable);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Finanzas/PorCobrarPagar_PorCobrarGrupoDetallado.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFechaHasta,
                parametroNombreGrupo,
                parametroNombreResponsable
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetCuentasPorCobrarGrupoDetallado", cuentasPorCobrarGrupoDetallado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }
    }
}
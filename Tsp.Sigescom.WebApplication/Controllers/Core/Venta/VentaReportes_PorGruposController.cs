using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Venta;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class VentaReportes_PorGruposController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IVentaReporte_Logica ventaReporte_Logica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;

        public VentaReportes_PorGruposController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            ventaReporte_Logica = Dependencia.Resolve<IVentaReporte_Logica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
        }

        public ActionResult PorGrupos_VentasPorFamiliasGrupos(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos)
        {
            var rptviewer = GenerarPorGrupos_VentasPorFamiliasGrupos(idPuntoVenta, nombrePuntoVenta, nombreEstablecimiento, fechaDesde, fechaHasta, todasLasFamilias, idsFamilias, nombresFamilias, todosLosGrupos, idsGrupos, nombresGrupos, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarPorGrupos_VentasPorFamiliasGrupos(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos, bool fromRequest)
        {
            List<OperacionFamiliaGrupo> ventasPorFamiliasGrupos = ventaReporte_Logica.ObtenerVentasPorFamiliasGrupos(idPuntoVenta, fechaDesde, fechaHasta, todasLasFamilias, idsFamilias, todosLosGrupos, idsGrupos);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombrePuntoVenta = new ReportParameter("NombrePuntoVenta", nombrePuntoVenta);
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Ventas/PorGrupos_VentasPorFamiliasGrupos.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombrePuntoVenta,
                parametroNombreEstablecimiento,
                parametroFechaDesde,
                parametroFechaHasta,
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetVentasPorFamiliasGrupos", ventasPorFamiliasGrupos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }

        public ActionResult PorGrupos_VentasPorGrupos(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos)
        {
            var rptviewer = GenerarPorGrupos_VentasPorGrupos(idPuntoVenta, nombrePuntoVenta, nombreEstablecimiento, fechaDesde, fechaHasta, todosLosGrupos, idsGrupos, nombresGrupos, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarPorGrupos_VentasPorGrupos(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool todosLosGrupos, [System.Web.Http.FromUri] int[] idsGrupos, string nombresGrupos, bool fromRequest)
        {
            List<OperacionGrupo> ventasPorGrupos = ventaReporte_Logica.ObtenerVentasPorGrupos(idPuntoVenta, fechaDesde, fechaHasta, todosLosGrupos, idsGrupos);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombrePuntoVenta = new ReportParameter("NombrePuntoVenta", nombrePuntoVenta);
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombresGrupos = new ReportParameter("NombresGrupos", nombresGrupos ?? "Todos");
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Ventas/PorGrupos_VentasPorGrupos.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombrePuntoVenta,
                parametroFechaDesde,               
                parametroNombreEstablecimiento,
                parametroFechaHasta,
                parametroNombresGrupos
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetVentasPorGrupos", ventasPorGrupos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }

        public ActionResult PorGrupos_VentasPorGrupoDetallado(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int idGrupo, string nombreGrupo)
        {
            var rptviewer = GenerarPorGrupos_VentasPorGrupoDetallado(idPuntoVenta, nombrePuntoVenta, nombreEstablecimiento, fechaDesde, fechaHasta, idGrupo, nombreGrupo, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarPorGrupos_VentasPorGrupoDetallado(int idPuntoVenta, string nombrePuntoVenta, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int idGrupo, string nombreGrupo, bool fromRequest)
        {
            List<OperacionGrupoDetallado> ventasPorGrupoDetallado = ventaReporte_Logica.ObtenerVentasPorGrupoDetallado(idPuntoVenta, fechaDesde, fechaHasta, idGrupo);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombrePuntoVenta = new ReportParameter("NombrePuntoVenta", nombrePuntoVenta);
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreGrupo = new ReportParameter("NombreGrupo", nombreGrupo);
            ReportParameter parametroNombreResponsable = new ReportParameter("NombreResponsable", ventasPorGrupoDetallado.FirstOrDefault() == null ? "-" : ventasPorGrupoDetallado.FirstOrDefault().NombreResponsable);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Ventas/PorGrupos_VentasPorGrupoDetallado.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombrePuntoVenta,
                parametroFechaDesde,                
                parametroNombreEstablecimiento,
                parametroFechaHasta,
                parametroNombreGrupo,
                parametroNombreResponsable
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetVentasPorGrupoDetallado", ventasPorGrupoDetallado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }
    }
}
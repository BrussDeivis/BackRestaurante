using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class AlmacenReportesController
    {
        public ActionResult InventariosPorFecha_Inventario(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = GenerarInventariosPorFecha_Inventario(idAlmacen, nombreAlmacen, fechaHasta, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }



        public ActionResult InventariosPorFecha_InventarioSemaforo(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto)
        {

            var rptviewer = GenerarInventariosPorFecha_InventarioSemaforo(idAlmacen, nombreAlmacen, fechaHasta, idsFamilias, nombresFamilias, todasLasFamilias, estadoBajo, estadoNormal, estadoAlto, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ActionResult InventariosPorFecha_InventarioValorizado(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {

            var rptviewer = GenerarInventariosPorFecha_InventarioValorizado(idAlmacen, nombreAlmacen, fechaHasta, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarInventariosPorFecha_Inventario(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            List<InventarioFisico> inventario = almacenReportingLogica.ObtenerInventarioFisicoHistorico(idAlmacen, ProfileData().Empleado.Id, fechaHasta, todasLasFamilias, idsFamilias).OrderBy(i => i.Concepto).ToList();
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreAlmacen = new ReportParameter("NombreAlmacen", nombreAlmacen);
            ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Almacen/InventariosPorFecha_Inventario.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroNombreAlmacen,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetInventario", inventario);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            return rptviewer;
        }
        public ReportViewer GenerarInventariosPorFecha_InventarioSemaforo(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto, bool fromRequest)
        {
            try
            {
                List<InventarioSemaforo> inventario = almacenReportingLogica.InventarioSemaforoHistorico(idAlmacen, ProfileData().Empleado.Id, fechaHasta, todasLasFamilias, idsFamilias, estadoBajo, estadoNormal, estadoAlto);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroNombreAlmacen = new ReportParameter("NombreAlmacen", nombreAlmacen);
                ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/InventariosPorFecha_InventarioSemaforo.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroNombreAlmacen,
                parametroFechaHasta
            });
                ReportDataSource rptdatasourceStockPorFamilia = new ReportDataSource("DataSetInventarioSemaforo", inventario);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceStockPorFamilia);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener reporte de inventario semáforo", e);
            }
        }

        public ReportViewer GenerarInventariosPorFecha_InventarioValorizado(int idAlmacen, string nombreAlmacen, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            List<InventarioValorizado> inventario = almacenReportingLogica.InventarioValorizadoHistorico(idAlmacen, ProfileData().Empleado.Id, fechaHasta, todasLasFamilias, idsFamilias).OrderBy(i => i.Concepto).ToList();
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreAlmacen = new ReportParameter("NombreAlmacen", nombreAlmacen);
            ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Almacen/InventariosPorFecha_InventarioValorizado.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroNombreAlmacen,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetInventarioValorizado", inventario);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            return rptviewer;
        }

    }
}
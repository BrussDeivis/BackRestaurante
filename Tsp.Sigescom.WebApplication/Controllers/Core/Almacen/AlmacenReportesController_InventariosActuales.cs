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
        public ActionResult InventariosActuales_Inventario([System.Web.Http.FromUri] string[] almacenes, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = GenerarInventariosActuales_Inventario(almacenes, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult InventariosActuales_InventarioSemaforo([System.Web.Http.FromUri] string[] almacenes, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto)
        {
            var rptviewer = GenerarInventariosActuales_InventarioSemaforo(almacenes, idsFamilias, nombresFamilias, todasLasFamilias, estadoBajo, estadoNormal, estadoAlto, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ActionResult InventariosActuales_InventarioValorizado([System.Web.Http.FromUri] string[] almacenes, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = GenerarInventariosActuales_InventarioValorizado(almacenes, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarInventariosActuales_Inventario(string[] almacenesString, int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            var almacenes = ObtenerItems(almacenesString);
            List<InventarioFisico> inventario = inventarioActual_Logica.InventariosFisicosActuales(almacenes, todasLasFamilias, idsFamilias).OrderBy(i => i.Concepto).ToList();
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroAlmacenes = new ReportParameter("Almacenes", String.Join("|",almacenes.Select(a=> a.Nombre)));
            ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Almacen/InventariosActuales_Inventario.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroAlmacenes
            });
            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetInventario", inventario);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            return rptviewer;
        }
        public ReportViewer GenerarInventariosActuales_InventarioSemaforo(string[] almacenesString, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto, bool fromRequest)
        {
            try
            {
                var almacenes = ObtenerItems(almacenesString);
                List<InventarioSemaforo> inventario = inventarioActual_Logica.InventariosSemaforoActuales(almacenes, todasLasFamilias, idsFamilias, estadoBajo, estadoNormal, estadoAlto);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroAlmacenes = new ReportParameter("Almacenes", String.Join("|", almacenes.Select(a => a.Nombre)));
                ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/InventariosActuales_InventarioSemaforo.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroAlmacenes
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

        public ReportViewer GenerarInventariosActuales_InventarioValorizado(string[] almacenesString, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            var almacenes = ObtenerItems(almacenesString);
            List<InventarioValorizado> inventario = inventarioActual_Logica.InventariosValorizadosActuales(almacenes, todasLasFamilias, idsFamilias).OrderBy(i => i.Concepto).ToList();
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroAlmacenes = new ReportParameter("Almacenes", String.Join("|", almacenes.Select(a => a.Nombre)));
            ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Almacen/InventariosActuales_InventarioValorizado.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroFamilias,
                parametroAlmacenes
            });
            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetInventarioValorizado", inventario);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            return rptviewer;
        }

    }
}
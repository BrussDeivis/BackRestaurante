using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class AlmacenReportesController
    {
        public ActionResult Movimientos_Entradas(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = Generar_Movimientos_Entradas(idAlmacen, nombreAlmacen, fechaDesde, fechaHasta, idsFamilias,  nombresFamilias,  todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

       

        public ActionResult Movimientos_Salidas(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = Generar_Movimientos_Salidas(idAlmacen, nombreAlmacen, fechaDesde, fechaHasta, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult Movimientos_Vencimientos(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias)
        {
            var rptviewer = Generar_Movimientos_Vencimientos(idAlmacen, nombreAlmacen, fechaDesde, fechaHasta, idsFamilias, nombresFamilias, todasLasFamilias, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer Generar_Movimientos_Entradas(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            try
            {
                var entradas = almacenReportingLogica.Entradas(idAlmacen, fechaDesde, fechaHasta, todasLasFamilias, idsFamilias);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias?? "Todas");
                ReportParameter parametroAlamacen = new ReportParameter("Almacen", nombreAlmacen);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/Movimientos_Entradas.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.NombreSede,
                    parametrosBasicos.Usuario,
                    parametroFamilias,
                    parametroAlamacen,
                    parametroFechaDesde,
                    parametroFechaHasta
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("DataSetEntradas", entradas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Entradas", e);
            }
        }
        public ReportViewer Generar_Movimientos_Salidas(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] idsFamilias,  string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            try
            {
                var entradas = almacenReportingLogica.Salidas(idAlmacen, fechaDesde, fechaHasta, todasLasFamilias, idsFamilias);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias?? "Todas");
                ReportParameter parametroAlamacen = new ReportParameter("Almacen", nombreAlmacen);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/Movimientos_Salidas.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.NombreSede,
                    parametrosBasicos.Usuario,
                    parametroFamilias,
                    parametroAlamacen,
                    parametroFechaDesde,
                    parametroFechaHasta
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("DataSetSalidas", entradas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Salidas", e);
            }
        }

        public ReportViewer Generar_Movimientos_Vencimientos(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] idsFamilias, string nombresFamilias, bool todasLasFamilias, bool fromRequest)
        {
            try
            {
                var vencimientos = almacenReportingLogica.Vencimientos(idAlmacen, fechaDesde, fechaHasta, todasLasFamilias, idsFamilias);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroFamilias = new ReportParameter("Familias", nombresFamilias ?? "Todas");
                ReportParameter parametroAlamacen = new ReportParameter("Almacen", nombreAlmacen);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/Movimientos_Vencimientos.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.NombreSede,
                    parametrosBasicos.Usuario,
                    parametroFamilias,
                    parametroAlamacen,
                    parametroFechaDesde,
                    parametroFechaHasta
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("DataSetVencimientos", vencimientos);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Vencimientos", e);
            }
        }
    }
}
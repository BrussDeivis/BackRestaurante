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
        public ActionResult Kardex_Fisico(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int idConcepto, string nombreConcepto)
        {
            var rptviewer = Generar_Kardex_Fisico(idAlmacen, nombreAlmacen, fechaDesde, fechaHasta, idConcepto, nombreConcepto,true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ActionResult Kardex_Valorizado(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int idConcepto, string nombreConcepto)
        {
            var rptviewer = Generar_Kardex_Valorizado(idAlmacen, nombreAlmacen, fechaDesde, fechaHasta, idConcepto, nombreConcepto, true);

            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
       
        public ReportViewer Generar_Kardex_Fisico(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int idConcepto, string nombreConcepto, bool fromRequest)
        {
            try
            {
                var kardex = almacenReportingLogica.KardexFisico(idAlmacen, ProfileData().Empleado.Id, fechaDesde, fechaHasta, idConcepto);
                var minMax = almacenReportingLogica.ObtenerStockMinimoYMaximo(idConcepto);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroAlamacen = new ReportParameter("Almacen", nombreAlmacen);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter parametroConcepto = new ReportParameter("Concepto", nombreConcepto);
                ReportParameter parametroStockMinimo = new ReportParameter("StockMinimo", minMax.StockMinimo.ToString());
                ReportParameter parametroStockMaximo = new ReportParameter("StockMaximo", minMax.StockMaximo.ToString());
                ReportParameter parametroStockActual = new ReportParameter("StockActual", kardex.Last().CantidadSaldo.ToString());

                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/Kardex_Fisico.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.NombreSede,
                    parametrosBasicos.Usuario,
                    parametroAlamacen,
                    parametroFechaDesde,
                    parametroFechaHasta,
                    parametroConcepto,
                    parametroStockMinimo,
                    parametroStockMaximo,
                    parametroStockActual
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("DataSetKardexFisico", kardex);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Kardex Fisico", e);
            }
        }
        public ReportViewer Generar_Kardex_Valorizado(int idAlmacen, string nombreAlmacen, DateTime fechaDesde, DateTime fechaHasta, int idConcepto, string nombreConcepto, bool fromRequest)
        {
            try
            {
                var kardex = almacenReportingLogica.KardexValorizado(idAlmacen, ProfileData().Empleado.Id, fechaDesde, fechaHasta, idConcepto);
                var minMax = almacenReportingLogica.ObtenerStockMinimoYMaximo(idConcepto);

                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroAlamacen = new ReportParameter("Almacen", nombreAlmacen);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
                ReportParameter parametroConcepto = new ReportParameter("Concepto", nombreConcepto);
                ReportParameter parametroStockMinimo = new ReportParameter("StockMinimo", minMax.StockMinimo.ToString());
                ReportParameter parametroStockMaximo = new ReportParameter("StockMaximo", minMax.StockMaximo.ToString());
                ReportParameter parametroStockActual = new ReportParameter("StockActual", kardex.Last().CantidadSaldo.ToString());
                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Almacen/Kardex_Valorizado.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.NombreSede,
                    parametrosBasicos.Usuario,
                    parametroAlamacen,
                    parametroFechaDesde,
                    parametroFechaHasta,
                    parametroConcepto,
                    parametroStockMinimo,
                    parametroStockMaximo,
                    parametroStockActual
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("DataSetKardexValorizado", kardex);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Kardex Valorizado", e);
            }
        }
    }
}
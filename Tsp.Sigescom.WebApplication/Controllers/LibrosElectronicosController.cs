using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using System.Globalization;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.WebApplication.Controllers.reportGeneration;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class LibrosElectronicosController : BaseController
    {
        LibrosElectronicosGeneradorReportes generador = new LibrosElectronicosGeneradorReportes();
        private readonly ILibrosElectronicosLogica _librosElectronicosLogica;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly IOperacionLogica _logicaOperacion;
        protected readonly IMaestroLogica maestroLogica;
        public LibrosElectronicosController()
        {
            _librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            _actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            _logicaOperacion = Dependencia.Resolve<IOperacionLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
        }

        public ActionResult ObtenerReportesPLE(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = _librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var librosElectronicos = maestroLogica.ObtenerDetalleMaestroLibroElectronico();
                var idsLibrosElectronicosSeleccionados = new List<int> { MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas };
                var librosElectronicosSeleccionados = librosElectronicos.Where(le => idsLibrosElectronicosSeleccionados.Contains(le.Id)).ToList();
                string fileNameZip = sede.DocumentoIdentidad + "_LE" + periodo.nombre + ".zip";
                //Generamos el reporte en PLE TXT comprimido
                var compressedBytes = generador.GenerarLibrosElectronicosTXTComprimido(ProfileData().Empleado.Id, sede, periodo, librosElectronicosSeleccionados);
                return File(compressedBytes, "application/zip", fileNameZip);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener archivos txt para el PLE", e)), HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult ObtenerReportesExcelPLE(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = _librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var librosElectronicos = maestroLogica.ObtenerDetalleMaestroLibroElectronico();
                var idsLibrosElectronicosSeleccionados = new List<int> { MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras, MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas };
                var librosElectronicosSeleccionados = librosElectronicos.Where(le => idsLibrosElectronicosSeleccionados.Contains(le.Id)).ToList();
                string fileNameZip = sede.DocumentoIdentidad + "_LE" + periodo.nombre + ".zip";
                //Generamos el reporte en PLE TXT comprimido
                var compressedBytes = generador.GenerarLibrosElectronicosCSVComprimido(ProfileData().Empleado.Id, sede, periodo, librosElectronicosSeleccionados);
                return File(compressedBytes, "application/zip", fileNameZip);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener archivos csv para el PLE", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerReporteVentasExcel(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = _librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var rptviewer = generador.GenerarReporteDeVentaEnExcel(ProfileData().Empleado.Id, sede, periodo, true, Request.MapPath(Request.ApplicationPath));

                string filenameContent = this.ObtenerSede().DocumentoIdentidad + "_" + this.ObtenerSede().Nombre + "_" + periodo.anio + "-" + periodo.mes;
                string filename = string.Format("{0}.{1}", filenameContent, "xls");
                filename = filename.Replace(" ", "");

                byte[] bytes = rptviewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

                return Json("Operacion realizada con exito");
            }

            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas formato excel", e);
            }
        }
        public ActionResult ReporteDeVentasEIngresos(int idPeriodo)
        {
            try
            {
                var periodo = _librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var sede = ObtenerSede();

                var rptviewer = generador.GenerarReporteDeVentasEIngresos(ProfileData().Empleado.Id, sede, periodo, true, _logicaOperacion, Request.MapPath(Request.ApplicationPath));

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener Registro de Ventas e Ingresos", e)), HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult ReporteDeVentasEIngresosSinConcepto(int idPeriodo)
        {
            try
            {
                var periodo = _librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var sede = ObtenerSede();

                var rptviewer = generador.GenerarReporteDeVentasEIngresosSinConcepto(ProfileData().Empleado.Id, sede, periodo, true, _logicaOperacion, Request.MapPath(Request.ApplicationPath));

                ViewBag.ReportViewer = rptviewer;
                return View("VisualizadorReporte");
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener Registro de Ventas e Ingresos", e)), HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult LibroElectronico()
        {
            return View();
        }

        public JsonResult ObtenerPeriodosDeLibrosElectronicos()
        {
            try
            {
                List<Periodo> resultado = _librosElectronicosLogica.ObtenerPeriodos();
                List<ComboGenericoViewModel> comboGenericoPeriodos = new List<ComboGenericoViewModel>();
                foreach (var item in resultado)
                {
                    comboGenericoPeriodos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(comboGenericoPeriodos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }
}
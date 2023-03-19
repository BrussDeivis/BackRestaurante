using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.WebApplication.Controllers.reportGeneration;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class LibrosElectronicosFoxcontController : BaseController
    {
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly ILibrosElectronicosLogica librosElectronicosLogica;
        protected readonly ILibrosElectronicosFoxcontLogica librosElectronicosFoxcontLogica;
        LibrosElectronicosGeneradorReportes generador = new LibrosElectronicosGeneradorReportes();

        public LibrosElectronicosFoxcontController() : base()
        {
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            librosElectronicosFoxcontLogica = Dependencia.Resolve<ILibrosElectronicosFoxcontLogica>();
        }
        public JsonResult ReporteVentaFormatoFoxcontBoletaVentaFactura(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var rptviewer = generador.GenerarReporteDeVentaFormatoFoxcontBoletaVentaFactura(ProfileData().Empleado.Id, sede, periodo, true, librosElectronicosFoxcontLogica, Request.MapPath(Request.ApplicationPath));

                string filename;
                string filenameContent;
                filenameContent = periodo.mes + "-" + periodo.anio;
                filename = string.Format("{0}.{1}", filenameContent, "xls");
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
                throw new ControllerException("Error al generar reporte reporte de ventas formato foxcom", e);
            }
        }

        public JsonResult ReporteVentaFormatoFoxcontNotaCreditoDebito(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var rptviewer = generador.GenerarReporteDeVentaFormatoFoxcontNotaCreditoDebito(ProfileData().Empleado.Id, sede, periodo, true, librosElectronicosFoxcontLogica, Request.MapPath(Request.ApplicationPath));
                ViewBag.ReportViewer = rptviewer;

                string filename;
                string filenameContent;
                filenameContent = periodo.mes + "-" + periodo.anio;
                filename = string.Format("{0}.{1}", filenameContent, "xls");

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
                throw new ControllerException("Error al generar reporte reporte de ventas formato foxcom de notas de debito y credito", e);
            }
        }
    }
}
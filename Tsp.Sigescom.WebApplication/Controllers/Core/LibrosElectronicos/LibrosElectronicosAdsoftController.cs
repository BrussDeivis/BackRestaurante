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
    public partial class LibrosElectronicosAdsoftController : BaseController
    {
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly ILibrosElectronicosLogica librosElectronicosLogica;
        protected readonly ILibrosElectronicosAdsoftLogica librosElectronicosAdsoftLogica;
        LibrosElectronicosGeneradorReportes generador = new LibrosElectronicosGeneradorReportes();

        public LibrosElectronicosAdsoftController() : base()
        {
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            librosElectronicosAdsoftLogica = Dependencia.Resolve<ILibrosElectronicosAdsoftLogica>();
        }
        public JsonResult ReporteVentaFormatoAdsoft(int idPeriodo)
        {
            try
            {
                var periodo = librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                var sede = ObtenerSede();
                var rptviewer = generador.GenerarReporteDeVentaFormatoAdsoft(ProfileData().Empleado.Id, sede, periodo, true, librosElectronicosAdsoftLogica, Request.MapPath(Request.ApplicationPath));

                string filename;
                string filenameContent;
                filenameContent = this.ObtenerSede().DocumentoIdentidad + "_" + this.ObtenerSede().Nombre + "_" + periodo.anio + "-" + periodo.mes;
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
                throw new ControllerException("Error al generar reporte de ventas formato adsoft", e);
            }
        }
    }
}
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
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class LibrosElectronicosConcarController : BaseController
    {
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly ILibrosElectronicosLogica librosElectronicosLogica;
        protected readonly ILibrosElectronicosConcarLogica librosElectronicosConcarLogica;

        public LibrosElectronicosConcarController() : base()
        {
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            librosElectronicosConcarLogica = Dependencia.Resolve<ILibrosElectronicosConcarLogica>();
        }

        public ActionResult ObtenerLibrosElectronicosConcar(int idPeriodo)
        {
            try
            {
                var sede = ObtenerSede();
                var periodo = librosElectronicosLogica.ObtenerPeriodo(idPeriodo);
                string fileNameZip = sede.DocumentoIdentidad + "_CONCAR_" + periodo.nombre + ".zip";
                //Generamos el reporte en PLE TXT comprimido
                var compressedBytes = GenerarLibrosElectronicosConcar(sede, periodo);
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

        public byte[] GenerarLibrosElectronicosConcar(EstablecimientoComercial sede, Periodo periodo)
        {
            try
            {
                LibroElectronicoConcar librosElectronicosConcar = librosElectronicosConcarLogica.ObtenerLibrosElectronicosConcar(periodo);
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry fileInArchive03 = null;
                        var rptviewer03 = GenerarReporteConcarConRdlc(librosElectronicosConcar.RegistroCobranzas, true, Request.MapPath(Request.ApplicationPath));
                        string filenameContent03 = sede.DocumentoIdentidad + "_SubDiario03_" + periodo.nombre;
                        fileInArchive03 = archive.CreateEntry(string.Format("{0}.{1}", filenameContent03, "xls").Replace(" ", ""), CompressionLevel.Optimal);
                        fileBytes = rptviewer03.LocalReport.Render("Excel", null, out string mimeType03, out string encoding03, out string extension03, out string[] streamids03, out Warning[] warnings03);
                        using (var entryStream = fileInArchive03.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }

                        ZipArchiveEntry fileInArchive05 = null;
                        var rptviewer05 = GenerarReporteConcarConRdlc(librosElectronicosConcar.RegistroVentas, true, Request.MapPath(Request.ApplicationPath));
                        string filenameContent05 = sede.DocumentoIdentidad + "_SubDiario05_" + periodo.nombre;
                        fileInArchive05 = archive.CreateEntry(string.Format("{0}.{1}", filenameContent05, "xls").Replace(" ", ""), CompressionLevel.Optimal);
                        fileBytes = rptviewer05.LocalReport.Render("Excel", null, out string mimeType05, out string encoding05, out string extension05, out string[] streamids05, out Warning[] warnings05);
                        using (var entryStream = fileInArchive05.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }

                        ZipArchiveEntry fileInArchive31 = null;
                        var rptviewer31 = GenerarReporteConcarConRdlc(librosElectronicosConcar.RegistroNotas, true, Request.MapPath(Request.ApplicationPath));
                        string filenameContent31 = sede.DocumentoIdentidad + "_SubDiario31_" + periodo.nombre;
                        fileInArchive31 = archive.CreateEntry(string.Format("{0}.{1}", filenameContent31, "xls").Replace(" ", ""), CompressionLevel.Optimal);
                        fileBytes = rptviewer31.LocalReport.Render("Excel", null, out string mimeType31, out string encoding31, out string extension31, out string[] streamids31, out Warning[] warnings31);
                        using (var entryStream = fileInArchive31.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }

                        ZipArchiveEntry fileInArchive = null;
                        var rptviewer = GenerarReporteClienteConcarConRdlc(librosElectronicosConcar.RegistroClientes, true, Request.MapPath(Request.ApplicationPath));
                        string filenameCan03 = sede.DocumentoIdentidad + "_Can03_" + periodo.nombre;
                        fileInArchive = archive.CreateEntry(string.Format("{0}.{1}", filenameCan03, "dbf").Replace(" ", ""), CompressionLevel.Optimal);
                        fileBytes = rptviewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }
                    }
                    compressedBytes = outStream.ToArray();
                }
                return compressedBytes;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar los libros electronicos de concar", e);
            }
        }

        public ReportViewer GenerarReporteConcarConRdlc(List<DetalleAsientoContableConcar> registrosConcar, bool fromRequest, string requestMapPath)
        {
            try
            {
                var rptviewer = new ReportViewer();
                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteFormatoConcar.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { });
                ReportDataSource rptdatasourcereporte = new ReportDataSource("DataSetRegistroConcar", registrosConcar);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporte);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte concar", e);
            }
        }

        public ReportViewer GenerarReporteClienteConcarConRdlc(List<RegistroClienteConcar> registrosCliente, bool fromRequest, string requestMapPath)
        {
            try
            {
                var rptviewer = new ReportViewer();
                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteClienteFormatoConcar.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { });
                ReportDataSource rptdatasourcereporte = new ReportDataSource("DataSetRegistroClienteConcar", registrosCliente);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporte);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte concar", e);
            }
        }
         
    }
}
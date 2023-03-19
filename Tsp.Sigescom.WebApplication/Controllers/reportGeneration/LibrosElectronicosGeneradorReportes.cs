using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers.reportGeneration
{
    public class LibrosElectronicosGeneradorReportes
    {
        protected readonly ILibrosElectronicosLogica librosElectronicosLogica;
        public LibrosElectronicosGeneradorReportes()
        {
            librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
        }

        /// <summary>
        /// Generar reporte de ventas formato ADSOFT 
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="sede"></param>
        /// <param name="periodoLE"></param>
        /// <param name="fromRequest">Sirve para diferenciar si se esta haciendo una peticion interna (vista) o si se esta haciendo una peticion externa (hangfire) </param>
        /// <returns></returns>
        public ReportViewer GenerarReporteDeVentaFormatoAdsoft(int idEmpleado, EstablecimientoComercial sede, Periodo periodoLE, bool fromRequest, ILibrosElectronicosAdsoftLogica librosElectronicosAdsoftLogica, string requestMapPath)
        {
            try
            {
                List<ReporteVentaClienteAdsoft> reporteVentaClienteAdsoft = librosElectronicosAdsoftLogica.ObtenerLibrosElectronicosAdsoft(idEmpleado, periodoLE.FechaDesde, periodoLE.FechaHasta);
                var rptviewer = new ReportViewer();

                ReportParameter fecha = new ReportParameter("Periodo", periodoLE.anio + "-" + periodoLE.mes);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);

                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteVentaClienteFormatoAdsoft.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { fecha, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVenta = new ReportDataSource("DataSetVentaClienteFormatoAdsoft", reporteVentaClienteAdsoft);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVenta);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas formato adsoft", e);
            }
        }

        /// <summary>
        /// Generar reporte de venta formatos FOXCOM de Boleta de Venta y Factura
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="sede"></param>
        /// <param name="periodoLE"></param>
        /// <param name="fromRequest">Sirve para diferenciar si se esta haciendo una peticion interna (vista) o si se esta haciendo una peticion externa (hangfire) </param>
        /// <returns></returns>
        public ReportViewer GenerarReporteDeVentaFormatoFoxcontBoletaVentaFactura(int idEmpleado, EstablecimientoComercial sede, Periodo periodoLE, bool fromRequest, ILibrosElectronicosFoxcontLogica librosElectronicosFoxcontLogica, string requestMapPath)
        {
            try
            {
                List<ReporteVentaClienteFoxcom> reporteVentaClienteFoxcom = librosElectronicosFoxcontLogica.ObtenerVentaClienteFoxcomBoletaVentaFactura(idEmpleado, periodoLE.FechaDesde, periodoLE.FechaHasta);
                var rptviewer = new ReportViewer();

                ReportParameter periodo = new ReportParameter("Periodo", periodoLE.mes + "-" + periodoLE.anio);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);

                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteVentaClienteFormatoFoxcom.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { periodo, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVenta = new ReportDataSource("DataSetVentaCliente", reporteVentaClienteFoxcom);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVenta);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }

            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte reporte de ventas formato foxcom", e);
            }
        }

        /// <summary>
        /// Generar reporte de venta formato FOXCOM de nota de Credito y de nota de Debito
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="sede"></param>
        /// <param name="periodoLE"></param>
        /// <param name="fromRequest">Sirve para diferenciar si se esta haciendo una peticion interna (vista) o si se esta haciendo una peticion externa (hangfire) </param>
        /// <returns></returns>
        public ReportViewer GenerarReporteDeVentaFormatoFoxcontNotaCreditoDebito(int idEmpleado, EstablecimientoComercial sede, Periodo periodoLE, bool fromRequest, ILibrosElectronicosFoxcontLogica librosElectronicosFoxcontLogica, string requestMapPath)
        {
            try
            {
                List<ReporteVentaClienteFoxcom> reporteVentaClienteFoxcom = librosElectronicosFoxcontLogica.ObtenerVentaClienteFoxcomNotaCreditoDebito(idEmpleado, periodoLE.FechaDesde, periodoLE.FechaHasta);
                var rptviewer = new ReportViewer();

                ReportParameter periodo = new ReportParameter("Periodo", periodoLE.mes + "-" + periodoLE.anio);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);

                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteVentaClienteFormatoFoxcomNotaCreditoyDebito.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { periodo, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVenta = new ReportDataSource("DataSetVentaCliente", reporteVentaClienteFoxcom);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVenta);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }

            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte reporte de ventas formato foxcom de notas de debito y credito", e);
            }
        }

        public ReportViewer GenerarReporteDeVentasEIngresosSinConcepto(int idEmpleado, EstablecimientoComercial sede, Periodo periodo, bool fromRequest, IOperacionLogica operacionLogica, string requestMapPath)
        {
            try
            {
                List<ReporteVentaDetalladoSinConcepto> reporteVentaDetalladoSinConcepto = operacionLogica.ObtenerReporteVentaDetalladoSinConcepto(idEmpleado, periodo.FechaDesde, periodo.FechaHasta);

                var rptviewer = new ReportViewer();

                //ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter fecha = new ReportParameter("Periodo", periodo.anio + "-" + periodo.mes);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);


                string path = @"/Content/reports/Core/Ventas/ReporteVentaDetalladoSinConcepto.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { fecha, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVentaDetalladaSinConcepto = new ReportDataSource("DataSetVentaDetalladoSinConcepto", reporteVentaDetalladoSinConcepto);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVentaDetalladaSinConcepto);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas  e ingresos sin concepto", e);
            }
        }

        public ReportViewer GenerarReporteDeVentasEIngresos(int idEmpleado, EstablecimientoComercial sede, Periodo periodo, bool fromRequest, IOperacionLogica operacionLogica, string requestMapPath)
        {
            try
            {
                List<OperacionDeVenta> operacionesDeVentas = operacionLogica.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributablesIncluyendoConceptos(idEmpleado, periodo.FechaDesde, periodo.FechaHasta);
                List<ReporteVentaDetalladoViewModel> reporteVentaDetalladoViewModel = ReporteVentaDetalladoViewModel.Convert(operacionesDeVentas);

                var rptviewer = new ReportViewer();

                //ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter fecha = new ReportParameter("Periodo", periodo.anio + "-" + periodo.mes);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);

                string path = @"/Content/reports/Core/Contabilidad/LibrosElectronicos/ReporteVentaDetallado.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { fecha, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVentaDetallada = new ReportDataSource("DataSetVentaDetallado", reporteVentaDetalladoViewModel);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVentaDetallada);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas e ingresos de insumos", e);
            }
        }

        public byte[] GenerarLibrosElectronicosTXTComprimido(int idEmpleado, EstablecimientoComercial sede, Periodo periodo, List<ItemGenerico> librosSeleccionados)
        {
            try
            {
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var libroSeleccionado in librosSeleccionados)
                        {
                            ZipArchiveEntry fileInArchive = null;
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos)
                            {
                                var libroVentasIngresos = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos);
                                List<DetalleLibroVentasIngresos> registrosDeVentas = librosElectronicosLogica.ObtenerLibroElectronicoDeVentasEIngresos(periodo, idEmpleado);
                                fileInArchive = archive.CreateEntry(DetalleLibroVentasIngresos.NombreArchivoTXT(sede.DocumentoIdentidad, periodo, libroVentasIngresos, registrosDeVentas), CompressionLevel.Optimal);
                                fileBytes = Encoding.UTF8.GetBytes(DetalleLibroVentasIngresos.ContenidoTXT(registrosDeVentas));
                            }
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras)
                            {
                                var libroCompras = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras);
                                List<LibroCompras> registrosDeCompras = new List<LibroCompras>();
                                fileInArchive = archive.CreateEntry(LibroCompras.NombreArchivoTXT(sede.DocumentoIdentidad, periodo, libroCompras, registrosDeCompras), CompressionLevel.Optimal);
                                fileBytes = Encoding.UTF8.GetBytes(LibroCompras.ContenidoTXT(registrosDeCompras));
                            }
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas)
                            {
                                var libroComprasNoDomiciliadas = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas);
                                List<LibroComprasNoDomiciliadas> registrosDeComprasNoDomiciliadas = new List<LibroComprasNoDomiciliadas>();
                                fileInArchive = archive.CreateEntry(LibroComprasNoDomiciliadas.NombreArchivoTXT(sede.DocumentoIdentidad, periodo, libroComprasNoDomiciliadas, registrosDeComprasNoDomiciliadas), CompressionLevel.Optimal);
                                fileBytes = Encoding.UTF8.GetBytes(LibroComprasNoDomiciliadas.ContenidoTXT(registrosDeComprasNoDomiciliadas));
                            }
                            using (var entryStream = fileInArchive.Open())
                            using (var fileToCompressStream = new MemoryStream(fileBytes))
                            {
                                fileToCompressStream.CopyTo(entryStream);
                                fileToCompressStream.Close();
                            }
                        }
                    }
                    compressedBytes = outStream.ToArray();
                }
                return compressedBytes;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas PLE en TXT", e);
            }
        }
        public byte[] GenerarLibrosElectronicosCSVComprimido(int idEmpleado, EstablecimientoComercial sede, Periodo periodo, List<ItemGenerico> librosSeleccionados)
        {
            try
            {
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var libroSeleccionado in librosSeleccionados)
                        {
                            ZipArchiveEntry fileInArchive = null;
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos)
                            {
                                var libroVentasIngresos = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoVentasIngresos);
                                List<DetalleLibroVentasIngresos> registrosDeVentas = librosElectronicosLogica.ObtenerLibroElectronicoDeVentasEIngresos(periodo, idEmpleado);
                                fileInArchive = archive.CreateEntry(DetalleLibroVentasIngresos.NombreArchivoCSV(sede.DocumentoIdentidad, periodo, libroVentasIngresos, registrosDeVentas), CompressionLevel.Optimal);
                                fileBytes = Encoding.Unicode.GetBytes(DetalleLibroVentasIngresos.ContenidoCSV(registrosDeVentas));
                            }
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras)
                            {
                                var libroCompras = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoCompras);
                                List<LibroCompras> registrosDeCompras = new List<LibroCompras>();
                                fileInArchive = archive.CreateEntry(LibroCompras.NombreArchivoCSV(sede.DocumentoIdentidad, periodo, libroCompras, registrosDeCompras), CompressionLevel.Optimal);
                                fileBytes = Encoding.Unicode.GetBytes(LibroCompras.ContenidoCSV(registrosDeCompras));
                            }
                            if (libroSeleccionado.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas)
                            {
                                var libroComprasNoDomiciliadas = librosSeleccionados.Single(ls => ls.Id == MaestroSettings.Default.IdDetalleMaestroLibroElectronicoComprasNoDomiciliadas);
                                List<LibroComprasNoDomiciliadas> registrosDeComprasNoDomiciliadas = new List<LibroComprasNoDomiciliadas>();
                                fileInArchive = archive.CreateEntry(LibroComprasNoDomiciliadas.NombreArchivoCSV(sede.DocumentoIdentidad, periodo, libroComprasNoDomiciliadas, registrosDeComprasNoDomiciliadas), CompressionLevel.Optimal);
                                fileBytes = Encoding.Unicode.GetBytes(LibroComprasNoDomiciliadas.ContenidoCSV(registrosDeComprasNoDomiciliadas));
                            }
                            using (var entryStream = fileInArchive.Open())
                            using (var fileToCompressStream = new MemoryStream(fileBytes))
                            {
                                fileToCompressStream.CopyTo(entryStream);
                                fileToCompressStream.Close();
                            }
                        }
                    }
                    compressedBytes = outStream.ToArray();
                }
                return compressedBytes;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte de ventas PLE en CSV", e);
            }
        }


        public ReportViewer GenerarReporteDeVentaEnExcel(int idEmpleado, EstablecimientoComercial sede, Periodo periodo, bool fromRequest, string requestMapPath)
        {
            try
            {
                List<ReporteVentaCliente> registrosDeVenta = librosElectronicosLogica.ObtenerRegistrosDeVenta(periodo, idEmpleado);

                var rptviewer = new ReportViewer();

                ReportParameter fecha = new ReportParameter("Periodo", new DateTime(1, int.Parse(periodo.mes), 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")) + "-" + periodo.anio);
                ReportParameter ruc = new ReportParameter("RUC", sede.DocumentoIdentidad);
                ReportParameter razonSocial = new ReportParameter("RazonSocial", sede.Nombre);

                string path = @"/Content/reports/Core/Ventas/ReporteVentaCliente.rdlc";
                rptviewer.LocalReport.ReportPath = fromRequest ? requestMapPath + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[] { fecha, ruc, razonSocial });
                ReportDataSource rptdatasourcereporteVenta = new ReportDataSource("DataSetRegistroVentaCliente", registrosDeVenta);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVenta);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);

                return rptviewer;
            }

            catch (Exception e)
            {
                throw new ControllerException("Error al generar reporte reporte de ventas formato foxcom", e);
            }
        }
    }
}
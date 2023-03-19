using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models.Comprobante;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using SelectPdf;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using System.Linq;
using Tsp.Sigescom.Config;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Net.Http;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ComprobanteController : BaseController
    {
        private readonly IMaestroLogica maestroLogica;
        private readonly IConceptoLogica conceptoLogica;
        private readonly IOperacionLogica operacionLogica;
        private readonly IActorNegocioLogica actorNegocioLogica;
        private readonly IConfiguracionLogica configuracionLogica;
        private readonly IGeneracionArchivosLogica generacionArchivosLogica;
        private readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        private readonly IBarCodeUtil barCodeUtil;
        private readonly IPdfUtil pdfUtil;
        private readonly IMailer mailer;
        private readonly ISede_Logica sedeLogica;

        public ComprobanteController()
        {
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
            generacionArchivosLogica = Dependencia.Resolve<IGeneracionArchivosLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            pdfUtil = Dependencia.Resolve<IPdfUtil>();
            mailer = Dependencia.Resolve<IMailer>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();
        }

        #region GETS
        public ActionResult TiposDeComprobante()
        {
            return View();
        }

        public ActionResult SeriesDeComprobante()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerSelectoresTiposDeComprobante(int comprobantePara)
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante((TipoComprobantePara)comprobantePara, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region Genericos
        public JsonResult ObtenerTiposDeComprobanteGenerico()
        {
            try
            {
                var resultados = configuracionLogica.ObtenerTiposDeComprobante();
                List<ComboGenericoViewModel> tiposDeComprobante = new List<ComboGenericoViewModel>();
                tiposDeComprobante = ComboGenericoViewModel.Convert(resultados);
                return Json(tiposDeComprobante);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region CRUD: Crear, Obtener (Tipo De Comprobante) 
        public JsonResult CrearTipoDeComprobante(RegistroTipoComprobanteViewModel tipoDeComprobante)
        {
            try
            {
                OperationResult resultado;



                if (tipoDeComprobante.Id != 0)
                {
                    List<TipoDeComprobanteParaTransaccion> tp = TipoDeTransaccionTipoDeComprobanteViewModel.ConvertTipoDeComprobanteParaTransaccion(TipoDeTransaccionTipoDeComprobanteViewModel.MatchTiposTransaccion(tipoDeComprobante.TiposDeTransaccionConEmisionPropia, tipoDeComprobante.IdDeTiposDeTransaccionConEmisionPropia));
                    List<TipoDeComprobanteParaTransaccion> tp1 = TipoDeTransaccionTipoDeComprobanteViewModel.ConvertTipoDeComprobanteParaTransaccion(TipoDeTransaccionTipoDeComprobanteViewModel.MatchTiposTransaccion(tipoDeComprobante.TiposDeTransaccionConEmisionTerceros, tipoDeComprobante.IdDeTiposDeTransaccionConEmisionTerceros));

                    resultado = configuracionLogica.ActualizarTipoComprobante(tipoDeComprobante.Id, tipoDeComprobante.Nombre, tipoDeComprobante.NombreCorto, tipoDeComprobante.CodigoSunat, tp, tp1);
                }
                else
                {
                    resultado = configuracionLogica.CrearTipoDeComprobante(tipoDeComprobante.Nombre, tipoDeComprobante.NombreCorto, tipoDeComprobante.CodigoSunat, tipoDeComprobante.IdDeTiposDeTransaccionConEmisionPropia.ToArray(), tipoDeComprobante.IdDeTiposDeTransaccionConEmisionTerceros.ToArray());
                }
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerTipoDeComprobante(int idTipoDeComprobante)
        {
            try
            {
                TipoDeComprobante tipoDeComprobante = configuracionLogica.ObtenerTipoDeComprobante(idTipoDeComprobante);
                RegistroTipoComprobanteViewModel tipoDeComprobanteViewModel = new RegistroTipoComprobanteViewModel(tipoDeComprobante);
                return Json(tipoDeComprobanteViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerTiposDeComprobanteBandeja()
        {
            try
            {
                var resultados = configuracionLogica.ObtenerTiposDeComprobante();
                List<BandejaTipoComprobanteViewModel> tiposDeComprobante = BandejaTipoComprobanteViewModel.Convert(resultados);
                return Json(tiposDeComprobante);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region CRUD: Crear, Obtener (Serie De Comprobante)
        public JsonResult CrearSerieDeComprobante(RegistroSerieDeComprobanteViewModel serieDeComprobante)
        {
            try
            {
                OperationResult resultado;

                CompobrobarSiExiteNumeroDeSerieSegunElTipoDeComprobante(serieDeComprobante.Id, serieDeComprobante.TipoDeComprobante.Id, serieDeComprobante.NumeroDeSerie);


                if (serieDeComprobante.Id != 0)
                {
                    resultado = null;
                    resultado = configuracionLogica.ActualizarSerieDeComprobante(serieDeComprobante.Id, serieDeComprobante.CentroDeAtencion.Id, serieDeComprobante.TipoDeComprobante.Id,
                                                                                serieDeComprobante.NumeroDeSerie, serieDeComprobante.NumeroDeComprobanteSiguiente,
                                                                                serieDeComprobante.Autonumerica, serieDeComprobante.Vigente);
                }
                else
                {
                    resultado = configuracionLogica.CrearSerieDeComprobante(serieDeComprobante.CentroDeAtencion.Id, serieDeComprobante.TipoDeComprobante.Id,
                                                                                serieDeComprobante.NumeroDeSerie, serieDeComprobante.NumeroDeComprobanteSiguiente,
                                                                                serieDeComprobante.Autonumerica, serieDeComprobante.Vigente);
                }
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerSerieDeComprobante(int idSerieDeComprobante)
        {
            try
            {
                SerieDeComprobante serieDeComprobante = configuracionLogica.ObtenerSerieDeComprobante(idSerieDeComprobante);
                RegistroSerieDeComprobanteViewModel serieDeComprobanteViewModel = new RegistroSerieDeComprobanteViewModel(serieDeComprobante);
                return Json(serieDeComprobanteViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerSeriesDeComprobanteBandeja()
        {
            try
            {
                var resultados = configuracionLogica.ObtenerSeriesDeComprobante();
                List<BandejaSerieDeComprobanteViewModel> seriesDeComprobante = BandejaSerieDeComprobanteViewModel.Convert(resultados);
                return Json(seriesDeComprobante);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public void CompobrobarSiExiteNumeroDeSerieSegunElTipoDeComprobante(int idSerieComprobante, int idTipoDeComprobante, string numeroSerie)
        {
            if (idSerieComprobante > 0)
            {
                var result = configuracionLogica.ObtenerSerieDeComprobante(idSerieComprobante);
                if (numeroSerie != result.Numero)
                {
                    bool existeNumeoDeSerie = configuracionLogica.ExisteNumeroDeSerieDeComprobanteSegunTipoDeComprobante(idTipoDeComprobante, numeroSerie);
                    if (existeNumeoDeSerie)
                    {
                        throw new ControllerException("Ya se registro el número de serie .");
                    }
                }
            }
            else
            {
                bool existeNumeoDeSerie = configuracionLogica.ExisteNumeroDeSerieDeComprobanteSegunTipoDeComprobante(idTipoDeComprobante, numeroSerie);
                if (existeNumeoDeSerie)
                {
                    throw new ControllerException("Ya se registro el número de serie .");
                }
            }
        }
        #endregion

        #region Descarga de Documentos
        public async Task<ActionResult> DescargarDocumentoPdf(long idOperacion, int formato)
        {
            try
            {
                var sede = ObtenerSede();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                var htmlString = await GenerarHtmlStringOperacion(sede, operacion, formato);
                var pdfDocumento = pdfUtil.ObtenerPdfDocumento(htmlString, (FormatoImpresion)formato);
                byte[] fileBytes = pdfDocumento.Save();
                string fileName = operacion.Comprobante + " - " + operacion.Tercero + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActionResult> DescargarDocumentoPdfA4(long idOperacion)
        {
            try
            {
                var sede = ObtenerSede();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                var htmlString = await GenerarHtmlStringOperacion(sede, operacion, (int)FormatoImpresion.A4);
                var pdfDocumento = pdfUtil.ObtenerPdfDocumento(htmlString, FormatoImpresion.A4);
                byte[] fileBytes = pdfDocumento.Save();
                string fileName = operacion.Comprobante + " - " + operacion.Tercero + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }
 
        public async Task<ActionResult> DescargarComprobante(string id)
        {
            try
            {
                var idOperacion = Convert.ToInt64(DesEncriptar(id));
                EstablecimientoComercialExtendidoConLogo sede = sedeLogica.ObtenerSedeConLogo();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                var htmlString = await GenerarHtmlStringOperacion(sede, operacion, (int)FormatoImpresion.A4);
                var pdfDocumento = pdfUtil.ObtenerPdfDocumento(htmlString, FormatoImpresion.A4);
                byte[] fileBytes = pdfDocumento.Save();
                MemoryStream memoryStream = new MemoryStream(fileBytes);
                return new FileStreamResult(memoryStream, "application/pdf");
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }


        public async Task<ActionResult> DescargarDocumentoXml(long idOperacion)
        {
            try
            {
                var sede = ObtenerSede();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                byte[] fileBytes = await GenerarDocumentoXml(sede, operacion);
                string fileName = operacion.Comprobante + " - " + operacion.Tercero + ".xml";
                return File(fileBytes, "application/xml", fileName);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<byte[]> GenerarDocumentoXml(EstablecimientoComercialExtendidoConLogo sede, OperacionTipoTransaccionTipoComprobante operacion)
        {
            try
            {
                byte[] fileBytes = null;
                if (Diccionario.TiposDeComprobanteTributables.Contains(operacion.IdTipoComprobante))
                {
                    var ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(operacion.IdOperacion);
                    fileBytes = XmlBuilder.ObtenerXmlComprobante(ordenDeVenta, sede, maestroLogica, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                }
                else if (operacion.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
                {
                    var guiaRemision = operacionLogica.ObtenerGuiaRemision(operacion.IdOperacion);
                    int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                    var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                    guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                    guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                    var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                    var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                    var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                    fileBytes = XmlBuilder.ObtenerXmlComprobante(guiaRemision, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                }
                return fileBytes;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener el xml de documento de operación", e);
            }
        }

        public async Task<ActionResult> DescargarDocumentoZip(long idOperacion, int formato)
        {
            try
            {
                var sede = ObtenerSede();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                var htmlString = await GenerarHtmlStringOperacion(sede, operacion, formato);
                var pdfDocumento = pdfUtil.ObtenerPdfDocumento(htmlString, (FormatoImpresion)formato);

                string fileNameZip = operacion.Comprobante + " - " + operacion.Tercero + ".zip";
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry fileInArchive = null;
                        fileInArchive = archive.CreateEntry(operacion.Comprobante + " - " + operacion.Tercero + ".pdf", CompressionLevel.Optimal);
                        fileBytes = pdfDocumento.Save();
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }
                        if (Diccionario.TiposDeComprobanteTributablesMasGuiaRemision.Contains(operacion.IdTipoComprobante))
                        {
                            fileInArchive = archive.CreateEntry(operacion.Comprobante + " - " + operacion.Tercero + ".xml", CompressionLevel.Optimal);
                            fileBytes = await GenerarDocumentoXml(sede, operacion);
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
                return File(compressedBytes, "application/zip", fileNameZip);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento zip", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<string> GenerarHtmlStringOperacion(EstablecimientoComercialExtendidoConLogo sede, OperacionTipoTransaccionTipoComprobante operacion, int formato)
        {
            try
            {
                string htmlStringOperacion = "";
                if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(operacion.IdTipoTransaccion) || (Diccionario.TiposDeTransaccionMovimientoDeBienes_Ventas.Contains(operacion.IdTipoTransaccion) && operacion.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente && Diccionario.TiposDeTransaccionMovimientoDeBienes_Ventas.Contains(operacion.IdTipoTransaccion) && operacion.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna))
                {
                    var ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(operacion.IdOperacion);
                    var QrContentOrden = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                    var QrBytesOrden = barCodeUtil.ObtenerCodigoQR(QrContentOrden);
                    htmlStringOperacion = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, (FormatoImpresion)formato, QrBytesOrden, sede, this, maestroLogica);
                }
                else if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras.Contains(operacion.IdTipoTransaccion) || Diccionario.TiposDeTransaccionMovimientoDeBienes_Compras.Contains(operacion.IdTipoTransaccion))
                {
                    var ordenDecompra = operacionLogica.ObtenerOrdenDeCompra(operacion.IdOperacion);
                    htmlStringOperacion = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDecompra, (FormatoImpresion)formato, null, sede, this, maestroLogica);
                }
                else if (operacion.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
                {
                    var movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(operacion.IdOperacion);
                    var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                    var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                    var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                    var QrBytes = barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion);
                    htmlStringOperacion = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, (FormatoImpresion)formato, QrBytes, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, this);
                }
                else if (operacion.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna)
                {
                    var movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(operacion.IdOperacion);
                    var QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                    var QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                    htmlStringOperacion = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, (FormatoImpresion)formato, QrBytes, sede, null, null, null, this);
                }
                return htmlStringOperacion;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener el html string de operación", e);
            }
        }
        #endregion

        #region Envio de Documento por Correo Electronico
        public async Task<JsonResult> EnviarCorreoElectronicoConDocumento(long idOperacion, int formato, List<string> correosElectronicos)
        {
            try
            {
                var sede = ObtenerSede();
                var operacion = configuracionLogica.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                var htmlString = await GenerarHtmlStringOperacion(sede, operacion, formato);
                var pdfDocumento = pdfUtil.ObtenerPdfDocumento(htmlString, (FormatoImpresion)formato);
                List<LinkedResource> resources = new List<LinkedResource>();
                //Preparamos el correo electronico para enviarse
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, operacion);
                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, operacion, Request.MapPath(Request.ApplicationPath), resources);
                var documentosAdjuntos = new List<Attachment>() { new Attachment(new MemoryStream(pdfDocumento.Save()), operacion.Comprobante + " - " + operacion.Tercero + ".pdf", "application/pdf") };
                if (Diccionario.TiposDeComprobanteTributablesMasGuiaRemision.Contains(operacion.IdTipoComprobante))
                {
                    byte[] fileBytes = await GenerarDocumentoXml(sede, operacion);
                    documentosAdjuntos.Add(new Attachment(new MemoryStream(fileBytes), operacion.Comprobante + " - " + operacion.Tercero + ".xml", "application/xml"));
                }
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, documentosAdjuntos, resources);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
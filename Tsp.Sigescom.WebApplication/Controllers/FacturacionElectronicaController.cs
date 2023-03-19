using Hangfire;
using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.WebApplication.Models.FacturacionElectronica;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class FacturacionElectronicaController : BaseController
    {
        readonly ActorComercial emisor;

        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IMailer mailer;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IGeneracionArchivosLogica generacionArchivosLogica;
        protected readonly ISede_Logica sedeLogica;


        public FacturacionElectronicaController() : base()
        {
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            ///Se obtiene desde la logica por que si esta activado el hangfire la sede en la variable de seccion es null
            emisor = facturacionElectronicaLogica.ObtenerEmisor(ActorSettings.Default.IdActorNegocioSede);
            mailer = Dependencia.Resolve<IMailer>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            generacionArchivosLogica = Dependencia.Resolve<IGeneracionArchivosLogica>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();

        }

        [Authorize(Roles = "Contador")]
        public ActionResult Configuracion()
        {
            return View();
        }

        [Authorize(Roles = "Contador")]
        public ActionResult EmisionComprobantes()
        {
            ViewBag.fechaDesde = DateTime.Today.AddDays(-FacturacionElectronicaSettings.Default.UltimosNDiasAMostarDocumentosEmitidos).ToString("dd/MM/yyyy");
            ViewBag.fechaHasta = DateTime.Today.ToString("dd/MM/yyyy");
            return View();
        }

        [Authorize(Roles = "Contador")]
        public ActionResult EnvioComprobantes()
        {
            ViewBag.fechaEnvioDesde = DateTime.Today.AddDays(-FacturacionElectronicaSettings.Default.UltimosNDiasAMostarDocumentosEnviados).ToString("dd/MM/yyyy");
            ViewBag.fechaEnvioHasta = DateTime.Today.ToString("dd/MM/yyyy");
            return View();
        }

        [Authorize(Roles = "JefeVenta")]
        public ActionResult InvalidarRechazadas()
        {
            ViewBag.fechaInicio = DateTime.Today.AddDays(-FacturacionElectronicaSettings.Default.UltimosNDiasAMostarDocumentosEnviados).ToString("dd/MM/yyyy");
            ViewBag.fechaFin = DateTime.Today.ToString("dd/MM/yyyy");
            return View();
        }

        [Authorize(Roles = "Contador")]
        public JsonResult ListarDocumentosEntreFechas(string desde, string hasta)
        {
            DateTime fechaDesde = DateTime.Parse(desde);
            DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
            try
            {
                var resultados = facturacionElectronicaLogica.ObtenerDocumentosEntreFechas(fechaDesde, fechaHasta);
                List<DocumentoViewModel> documentos = new List<DocumentoViewModel>();
                foreach (var item in resultados)
                {
                    documentos.Add(new DocumentoViewModel(item));
                }
                var jsonResult = Json(documentos, JsonRequestBehavior.DenyGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        [Authorize(Roles = "Contador")]
        public JsonResult ListarEnviosEntreFechas(string desde, string hasta)
        {
            DateTime fechaEnvioDesde = DateTime.Parse(desde);
            DateTime fechaEnvioHasta = DateTime.Parse(hasta + " 23:59:59");
            try
            {
                var resultados = facturacionElectronicaLogica.ObtenerEnviosEntreFechas(fechaEnvioDesde, fechaEnvioHasta);
                List<EnvioViewModel> envios = EnvioViewModel.Convert(resultados);
                return Json(envios);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #region TRANSMISION DE DOCUMENTOS 

        [Authorize(Roles = "Contador")]
        public async Task<JsonResult> TransmitirAFacturacionElectronicaManual()
        {
            if (!Flags.FacturacionEnProceso)
            {
                Flags.FacturacionEnProceso = true;
                EstablecimientoComercialExtendido sede = ObtenerSede();
                try
                {
                    await facturacionElectronicaLogica.TransmitirAFacturacionElectronica(ProfileData().Empleado.Id, sede);
                    return new JsonHttpStatusResult(new { code_result = "TODO SE REALIZO CON EXITO", data = "OK", result_description = "TRANSMITIDO CORRECTAMENTE A FACTURACION ELECTRONICA" }, HttpStatusCode.OK);
                }
                catch (LogicaException e)
                {
                    mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL TRANSMITIR - " + ObtenerAsuntoCorreoEFactura(sede), Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> " + Util.SoloErrorString(e));
                    return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
                }
                finally
                {
                    Flags.FacturacionEnProceso = false;
                }
            }
            else
            {
                throw new ControllerException("Ya se tiene un proceso de transmisión o envío en proceso");
            }
        }
        #endregion

        #region ENVIO DE DOCUMENTOS
        public async Task<JsonResult> EnviarDocumentos()
        {
            if (!Flags.FacturacionEnProceso)
            {
                Flags.FacturacionEnProceso = true;
                try
                {
                    LogEnvioFacturacionElectronica logEnvio = new LogEnvioFacturacionElectronica();
                    var resultado = await EnvioDeComprobantes(ObtenerSede(), ProcesoEnvioFacturacion.InterfazGraficaUsuario, logEnvio);
                    return (resultado.code_result == OperationResultEnum.Success) ?
                        new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = resultado.title.Replace("<br/>", Environment.NewLine) }, HttpStatusCode.OK) :
                        new JsonHttpStatusResult(new { error = "ERROR AL INTENTAR ENVIAR ALGUNOS DOCUMENTOS", stackTrace = resultado.title.Replace("<br/>", Environment.NewLine) }, HttpStatusCode.InternalServerError);
                }
                catch (Exception e)
                {
                    await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, ObtenerAsuntoCorreoEFactura(ObtenerSede()), "ERROR AL ENVIAR DOCUMENTOS A SUNAT" + " <br/> " + ObtenerPresentacionCorreoEFactura(ObtenerSede()) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> ");
                    return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar enviar los documentos", e)), HttpStatusCode.InternalServerError);
                }
                finally
                {
                    Flags.FacturacionEnProceso = false;
                }
            }
            else
            {
                throw new ControllerException("Ya se tiene un proceso de transmisión o envío en proceso");
            }
        }

        public async Task<OperationResult> EnvioDeComprobantes(EstablecimientoComercial sede, ProcesoEnvioFacturacion procesoEnvio, LogEnvioFacturacionElectronica logEnvioFacturacion)
        {
            IFacturacionElectronicaLogica facturacionLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            try
            {
                facturacionLogica.ConsultarTickets();
            }
            catch (Exception) { }
            //1. Envio de resumenes de boleta
            facturacionLogica.EnvioBoletas(logEnvioFacturacion.BoletasVenta);
            //2. Envio de facturas
            facturacionLogica.EnvioFacturas(logEnvioFacturacion.Factura);
            //3. Envio de notas de credito
            facturacionLogica.EnvioNotasCredito(logEnvioFacturacion.NotaCredito);
            //4. Envio de notas de debito
            facturacionLogica.EnvioNotasDebito(logEnvioFacturacion.NotaDebito);
            //5. Envio de comunicaciones de baja
            facturacionLogica.EnvioComunicacionesBaja(logEnvioFacturacion.Factura);

            string mensajeDocumentosEnviados = "LISTA DE DOCUMENTOS ENVIADOS: <br/> " + MensajeDocumentosEnviados(logEnvioFacturacion);
            await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, logEnvioFacturacion.CodigoEstadoEnvio() + " - " + ObtenerAsuntoCorreoEFactura(sede), "DOCUMENTOS ENVIADOS A SUNAT" + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(procesoEnvio) + " <br/> " + mensajeDocumentosEnviados);

            return logEnvioFacturacion.CodigoEstadoEnvio().Contains(((int)EstadoLogEnvio.Error).ToString()) || logEnvioFacturacion.CodigoEstadoEnvio().Contains(((int)EstadoLogEnvio.Ambos).ToString()) ? new OperationResult(OperationResultEnum.Warning, mensajeDocumentosEnviados) : new OperationResult(OperationResultEnum.Success, mensajeDocumentosEnviados);
        }

        private string MensajeDocumentosEnviados(LogEnvioFacturacionElectronica logEnvioFacturacion)
        {
            string mensaje = "";
            mensaje += MensajeLogEnvioString("FACTURAS", logEnvioFacturacion.Factura);
            mensaje += MensajeLogEnvioString("BOLETAS DE VENTA", logEnvioFacturacion.BoletasVenta);
            mensaje += MensajeLogEnvioString("NOTAS DE CREDITO", logEnvioFacturacion.NotaCredito);
            mensaje += MensajeLogEnvioString("NOTAS DE DEBITO", logEnvioFacturacion.NotaDebito);
            mensaje += MensajeLogEnvioString("GUIAS DE REMISION", logEnvioFacturacion.GuiaRemision);
            mensaje += LogErroresString(logEnvioFacturacion);
            return mensaje;
        }

        private string MensajeLogEnvioString(string tipoComprobante, LogEnvio logEnvio)
        {
            string mensaje = tipoComprobante + ": " + " <br/> " +
                (logEnvio.NoHayDocumentos ? logEnvio.MensajeNoHayDocumentos + " <br/> " : ((logEnvio.Exito.Where(e => e.ModoEnvio == ModoEnvio.Adicion).Count() > 0) ?
                ("Adicionados:" + string.Join(", ", logEnvio.Exito.Where(e => e.ModoEnvio == ModoEnvio.Adicion).Select(e => e.Mensaje))) + " <br/> " : "") + ((logEnvio.Exito.Where(e => e.ModoEnvio == ModoEnvio.Anulacion).Count() > 0) ?
                ("Anulados:" + string.Join(", ", logEnvio.Exito.Where(e => e.ModoEnvio == ModoEnvio.Anulacion).Select(e => e.Mensaje)) + " <br/> ") : ""));
            mensaje += (logEnvio.Error.Count() > 0) ? string.Join(", <br/> ", logEnvio.Error) + " <br/> " : "";
            return mensaje;
        }

        private string LogErroresString(LogEnvioFacturacionElectronica logEnvioFacturacion)
        {
            string mensaje = "";
            mensaje += (logEnvioFacturacion.Factura.LogError.Count > 0) ? "FACTURAS:" + " <br/> " + string.Join(", ", logEnvioFacturacion.Factura.LogError) + " <br/> " : "";
            mensaje += (logEnvioFacturacion.BoletasVenta.LogError.Count > 0) ? "BOLETAS DE VENTA:" + " <br/> " + string.Join(", ", logEnvioFacturacion.BoletasVenta.LogError) + " <br/> " : "";
            mensaje += (logEnvioFacturacion.NotaCredito.LogError.Count > 0) ? "NOTAS DE CREDITO:" + " <br/> " + string.Join(", ", logEnvioFacturacion.NotaCredito.LogError) + " <br/> " : "";
            mensaje += (logEnvioFacturacion.NotaDebito.LogError.Count > 0) ? "NOTAS DE DEBITO:" + " <br/> " + string.Join(", ", logEnvioFacturacion.NotaDebito.LogError) + " <br/> " : "";
            mensaje += (logEnvioFacturacion.GuiaRemision.LogError.Count > 0) ? "GUIAS DE REMISION:" + " <br/> " + string.Join(", ", logEnvioFacturacion.GuiaRemision.LogError) + " <br/> " : "";
            mensaje = mensaje == "" ? "" : " <br/> " + mensaje.Replace(Environment.NewLine, " <br/> ");
            mensaje += mensaje == "" ? "" : ("Sitio web: " + System.Web.HttpContext.Current.Request.Headers["Host"]);
            return mensaje;
        }
  
        #endregion

        #region CONSULTA DE TICKETS

        [Authorize(Roles = "Contador")]
        public JsonResult ConsultarTickets()
        {
            try
            {
                facturacionElectronicaLogica.ConsultarTickets();
                return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "TODO SE REALIZO CON EXITO" }, HttpStatusCode.OK);
            }
            catch (LogicaException e)
            {
                mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL CONSULTAR - " + ObtenerAsuntoCorreoEFactura(ObtenerSede()), Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar consultar envios de resumen diario", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region REENVIAR ENVIOS QUE TIENEN ESTADO POR EXCEPCION

        [Authorize(Roles = "Contador")]
        public JsonResult ResolverEnviosConExcepcion()
        {
            try
            {
                facturacionElectronicaLogica.ResolverEnviosConExcepcion();
                return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "TODO SE REALIZO CON EXITO" }, HttpStatusCode.OK);
            }
            catch (LogicaException e)
            {
                mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL RESOLVER REENVIOS CON EXCEPCION - " + ObtenerAsuntoCorreoEFactura(ObtenerSede()), Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar resolver los envios con excepcion", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region REENVIAR ENVIOS QUE TIENEN ESTADO RECHAZADO

        [Authorize(Roles = "AdministradorTI")]
        public JsonResult ResolverEnvioRechazado(int idEnvio)
        {
            try
            {
                facturacionElectronicaLogica.ResolverEnvioRechazado(idEnvio);
                return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "TODO SE REALIZO CON EXITO" }, HttpStatusCode.OK);
            }
            catch (LogicaException e)
            {
                mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL RESOLVER REENVIOS RECHAZADOS - " + ObtenerAsuntoCorreoEFactura(ObtenerSede()), Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar resolver los envios rechazados", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region REENVIAR ENVIOS QUE TIENEN ESTADO PENDIENTE

        [Authorize(Roles = "AdministradorTI")]
        public JsonResult ResolverEnvioPendiente(int idEnvio)
        {
            try
            {
                facturacionElectronicaLogica.ResolverEnvioPendiente(idEnvio);
                return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "TODO SE REALIZO CON EXITO" }, HttpStatusCode.OK);
            }
            catch (LogicaException e)
            {
                mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL RESOLVER REENVIO PENDIENTE - " + ObtenerAsuntoCorreoEFactura(ObtenerSede()), Enumerado.GetDescription(ProcesoEnvioFacturacion.InterfazGraficaUsuario) + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar resolver envio pendiente", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region REGENERAR JSON DE DOCUMENTOS EMITIDOS

        [Authorize(Roles = "Contador")]
        public async Task<JsonResult> RegenerarJsonDocumento(long idDocumento)
        {
            try
            {
                await facturacionElectronicaLogica.RegenerarJsonDocumento(idDocumento);
                return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "TODO SE REALIZO CON EXITO" }, HttpStatusCode.OK);
            }
            catch (LogicaException e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar resolver el resuemn diario", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region DESCARGA DE DOCUMENTOS

        [Authorize(Roles = "Contador")]
        public ActionResult ObtenerArchivoDocumento(int idDocumento, string nombreArchivo)
        {
            var contenidoDocumento = Encoding.UTF8.GetString(facturacionElectronicaLogica.ObtenerArchivo(idDocumento));
 
            byte[] fileBytes = Encoding.UTF8.GetBytes(contenidoDocumento);
            string fileName = nombreArchivo + ".txt";
            return File(fileBytes, "application/txt", fileName);
        }

        [Authorize(Roles = "Contador")]
        public ActionResult ObtenerArchivoEnvio(int idEnvio, string nombreArchivo)
        {
            FirmadoResponse firmadoResponse = JsonConvert.DeserializeObject<FirmadoResponse>(Encoding.UTF8.GetString(facturacionElectronicaLogica.ObtenerArchivo(idEnvio)));

            nombreArchivo = nombreArchivo.Replace("/", "");
            byte[] fileBytes = null;//here is your file in bytes
            byte[] compressedBytes;
            string fileNameZip = nombreArchivo + ".zip";

            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    var fileInArchive = archive.CreateEntry(nombreArchivo + ".xml", CompressionLevel.Optimal);
                    fileBytes = Convert.FromBase64String(firmadoResponse.TramaXmlFirmado);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(fileBytes))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                        fileToCompressStream.Close();
                    }
                }
                compressedBytes = outStream.ToArray();
            }
            return File(compressedBytes, "application/zip", fileNameZip);
        }

        [Authorize(Roles = "Contador")]
        public ActionResult ObtenerArchivoRespuesta(int idRespuesta, string nombreArchivo)
        {
            EnviarDocumentoResponse enviarDocumentoResponse = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(Encoding.UTF8.GetString(facturacionElectronicaLogica.ObtenerArchivo(idRespuesta)));
            if (!enviarDocumentoResponse.Exito)
            {
                throw new LogicaException(enviarDocumentoResponse.MensajeError);//InvalidOperationException
            }
            nombreArchivo = nombreArchivo.Replace("/", "");
            byte[] compressedBytes = Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr);
            string fileNameZip = nombreArchivo + ".zip";

            return File(compressedBytes, "application/zip", fileNameZip);
        }

        #endregion

        #region OBTENCION DE XML

        [Authorize(Roles = "Contador")]
        public ActionResult DescargarXMLComprobante(long idOrden)
        {
            try
            {
                EstablecimientoComercialExtendido sede = ObtenerSede();
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrden);

                string fileNameZip = ordenDeVenta.Comprobante().CodigoTipo + " - " + ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".zip";
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry fileInArchive = null;
                         
                            fileInArchive = archive.CreateEntry(ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".xml", CompressionLevel.Optimal);
                            fileBytes = XmlBuilder.ObtenerXmlComprobante(ordenDeVenta, sede, maestroLogica, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                            using (var entryStream = fileInArchive.Open())
                            using (var fileToCompressStream = new MemoryStream(fileBytes))
                            {
                                fileToCompressStream.CopyTo(entryStream);
                                fileToCompressStream.Close();
                            }
                    }
                    compressedBytes = outStream.ToArray();
                }
                return File(compressedBytes, "application/zip", fileNameZip);
            }
            catch (Exception ex)
            {
                throw new ControllerException("Error al descargar el XML del comprobante", ex);
            }
        }

        #endregion

        #region FACTURACION ELECTRONICA CON HANGFIRE 
        public string ObtenerPresentacionCorreoEFactura(EstablecimientoComercial sede)
        {
            return "RUC: " + sede.DocumentoIdentidad + " <br/> " + "RAZON SOCIAL: " + sede.Nombre + " <br/> " + "NOMBRE COMERCIAL: " + sede.NombreComercial;
        }

        public string ObtenerAsuntoCorreoEFactura(EstablecimientoComercial sede)
        {
            return sede.NombreComercial + " - RUC: " + sede.DocumentoIdentidad + " - " + sede.Nombre;
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task TransmitirComprobantes(EstablecimientoComercialExtendido sede)
        {
            try
            {
                IFacturacionElectronicaLogica facturacionLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
                await facturacionLogica.TransmitirAFacturacionElectronica(ActorSettings.Default.IdActorNegocioEmpleadoPorDefecto, sede);
            }
            catch (Exception e)
            {
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL TRANSMITIR - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR TRANSMITIR " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
            }
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task EnvioDeComprobantes(EstablecimientoComercial sede)
        {
            LogEnvioFacturacionElectronica logEnvioFacturacion = new LogEnvioFacturacionElectronica();
            await EnvioDeComprobantes(sede, ProcesoEnvioFacturacion.Hangfire, logEnvioFacturacion);
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task ConsultarYReenviarComprobantes(EstablecimientoComercial sede)
        {
            IFacturacionElectronicaLogica facturacionLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            try
            {
                facturacionLogica.ConsultarTickets();
            }
            catch (Exception e)
            {
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL CONSULTAR - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR CONSULTAR TICKETS " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
            }
            try
            {
                await facturacionLogica.ResolverEnviosConExcepcion();
            }
            catch (Exception e)
            {
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL RESOLVER ENVIOS - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR RESOLVER ENVIOS CON EXCEPCION " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
            }
            try
            {
                facturacionLogica.ResolverEnviosPendientes();
            }
            catch (Exception e)
            {
                await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL RESOLVER ENVIOS - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR RESOLVER ENVIOS PENDIENTES " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
            }
        }

        public async Task TransmitirEnviarConsultarYReenviarComprobantes()
        {
            if (!Flags.FacturacionEnProceso)
            {
                Flags.FacturacionEnProceso = true;
                try
                {
                    EstablecimientoComercialExtendido sede = sedeLogica.ObtenerSedeExtendida();
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-PE");
                        await TransmitirComprobantes(sede);
                    }
                    catch (Exception e)
                    {
                        await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL TRANSMITIR - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR TRANSMITIR " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                    }
                    try
                    {
                        await EnvioDeComprobantes(sede);
                    }
                    catch (Exception e)
                    {
                        await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL ENVIAR COMPROBANTES - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR ENVIAR COMPROBANTES " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                    }
                    try
                    {
                        await ConsultarYReenviarComprobantes(sede);
                    }
                    catch (Exception e)
                    {
                        await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR AL CONSULTAR Y REENVIAR COMPROBANTES - " + ObtenerAsuntoCorreoEFactura(sede), "ERROR AL INTENTAR CONSULTAR Y REENVIAR COMPROBANTES " + " <br/> " + ObtenerPresentacionCorreoEFactura(sede) + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                    }
                    finally
                    {
                        Flags.FacturacionEnProceso = false;
                    }
                }
                catch (Exception e)
                {
                    await mailer.SendEmailAsync(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "ERROR INESPERADO", "ERROR AL INTENTAR CONSULTAR Y REENVIAR COMPROBANTES " + " <br/> " + " <br/> " + "PROCESO DE FACTURACION - " + Enumerado.GetDescription(ProcesoEnvioFacturacion.Hangfire) + " <br/> " + "EL ERROR ES EL SIGUIENTE: " + " <br/> " + Util.SoloErrorString(e).Replace(Environment.NewLine, " <br/> "));
                }
            }
            else
            {
                throw new ControllerException("Ya se tiene un proceso de transmisión o envío en proceso");
            }
        }

        #endregion
    }
}
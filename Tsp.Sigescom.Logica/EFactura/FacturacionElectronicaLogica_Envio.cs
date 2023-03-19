using Tsp.Sigescom.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Config;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Newtonsoft.Json;
using System.Text;
using OpenInvoicePeru.Comun.Dto.Modelos;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using Tsp.Sigescom.Modelo;
using System.IO;
using System.Threading;
using System.Net;
using System.Linq.Expressions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Utilitarios.RestHelper;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using System.Net.Http;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Drawing;
using Tsp.Sigescom.Modelo.ClasesNegocio.EFactura;
using System.Xml;

namespace Tsp.FacturacionElectronica.Logica
{
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    public partial class FacturacionElectronicaLogica
    {
        #region ENVIO DE FACTURAS

        public OperationResult EnvioFacturas(LogEnvio logEnvio)
        {
            try
            {
                Documento[] documentos = DevolverFacturasNoInvalidadasIncluidoBinarioPorEnviar().ToArray();
                if (documentos.Length > 0)
                {
                    var result = ResolverEnvioIndividual(documentos, logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(result, "Error al realizar el envio de facturas");
                }
                else
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay facturas por enviar");
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al resolver el envio de facturas" + " - " + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al resolver el envio de facturas" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult ResolverEnvioIndividual(Documento[] documentos, LogEnvio logEnvio)
        {
            for (int i = 0; i < documentos.Length; i++)
            {
                try
                {
                    DocumentoElectronico documentoElectronico = JsonConvert.DeserializeObject<DocumentoElectronico>(Encoding.UTF8.GetString(documentos[i].Binario.archivoBinario));
                    bool documentoElectronicoEsUnaNota = documentos[i].codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito || documentos[i].codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito;
                    if (documentoElectronicoEsUnaNota)
                    {
                        if (!_generacionArchivosLogica.DocumentoReferenciaFueAceptado(documentoElectronico.Relacionados.FirstOrDefault().TipoDocumento, documentoElectronico.Relacionados.FirstOrDefault().NroDocumento))
                        {
                            throw new LogicaException("El documento de referencia de :" + documentoElectronico.Relacionados.FirstOrDefault().TipoDocumento + " " + documentoElectronico.Relacionados.FirstOrDefault().NroDocumento + " aun no a sido informado");
                        }
                    }
                    var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute(ObtenerMetodoApi(documentoElectronico.TipoDocumento), documentoElectronico, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                    if (!documentoResponse.Exito)
                    {
                        throw new LogicaException(documentoResponse.MensajeError);
                    }
                    var archivoCertificado = ObtenerCertificado(documentoElectronico.Emisor.NroDocumento);
                    var firmadoResponse = FirmarDocumento(documentoResponse, archivoCertificado);
                    var resultadoEnvio = EnviarIndividual(documentoElectronico, firmadoResponse, documentos[i].id);
                    if (resultadoEnvio.code_result == OperationResultEnum.Information)
                    {
                        logEnvio.Error.Add(documentoElectronico.IdDocumento + ": " + resultadoEnvio.title);
                        break;
                    }
                    if (!string.IsNullOrEmpty(resultadoEnvio.title))
                    {
                        logEnvio.Error.Add(documentoElectronico.IdDocumento + ": " + resultadoEnvio.title);
                    }
                    Util.ManejoEnLogicaResultadoSinExito(resultadoEnvio, "Error al firmar y enviar individualmente");
                    logEnvio.Exito.Add(ItemEnvio.ItemAdicionado(documentoElectronico.IdDocumento));
                }
                catch (Exception e)
                {
                    logEnvio.Error.Add(documentos[i].ComprobanteDocumento() + ": " + Util.InicioErrorString(e));
                    logEnvio.LogError.Add(documentos[i].ComprobanteDocumento() + ": " + Util.SoloErrorString(e));
                }
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        private OperationResult EnviarIndividual(DocumentoElectronico documentoElectronico, FirmadoResponse firmadoResponse, long idDocumento)
        {
            IndicadorExcepcion logDeEnvio = new IndicadorExcepcion("");
            OperationResult resultadoCrearEnvio = new OperationResult();
            EnviarDocumentoResponse envioDocumentoResponse;
            try
            {
                try
                {
                    resultadoCrearEnvio = CrearEnvio("", FacturacionElectronicaSettings.Default.TipoEnvioIndividual, (int)EstadoEnvio.Pendiente, "", "", "", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)), null, ModoEnvio.Ninguno);
                    Util.ManejoEnLogicaResultadoSinExito(resultadoCrearEnvio, "Error al intentar crear envio del documento " + idDocumento);
                }
                catch (Exception)
                {
                    return new OperationResult(OperationResultEnum.Information, "ERROR AL CREAR ENVIO", "ERROR");
                }
                var resultadoCrearEnvioDocumento = CrearEnvioDocumento(resultadoCrearEnvio.data, idDocumento);
                Util.ManejoEnLogicaResultadoSinExito(resultadoCrearEnvioDocumento, "Error al intentar crear el envio documento de " + documentoElectronico.IdDocumento);
                var resultadoActualizarDocumentoAEnviado = ActualizarDocumentoAEnviado(idDocumento);
                Util.ManejoEnLogicaResultadoSinExito(resultadoActualizarDocumentoAEnviado, "Error al intentar actualizar el documento " + documentoElectronico.IdDocumento + " a enviado");
                envioDocumentoResponse = ResolverEnvioDocumento(firmadoResponse, documentoElectronico, resultadoCrearEnvio.data, logDeEnvio);
                if (!envioDocumentoResponse.Exito)
                {
                    ActualizarEnvioDeDocumento(envioDocumentoResponse, resultadoCrearEnvio.data);
                    throw new LogicaException("Error al realizar el envio individual " + envioDocumentoResponse.MensajeError);
                }
                return new OperationResult(OperationResultEnum.Success, (string.IsNullOrEmpty(logDeEnvio.RegistroExcepcion) ? "" : Environment.NewLine + logDeEnvio.RegistroExcepcion), "OK");
            }
            catch (LogicaException e)
            {
                envioDocumentoResponse = ResolverEnvioDocumento(firmadoResponse, documentoElectronico, resultadoCrearEnvio.data, logDeEnvio);
                if (!envioDocumentoResponse.Exito)
                {
                    ActualizarEnvioDeDocumento(envioDocumentoResponse, resultadoCrearEnvio.data);
                    throw new LogicaException("Error al realizar el envio individual " + envioDocumentoResponse.MensajeError);
                }
                throw new LogicaException("Error al realizar el envio individual", e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar el envio individual", e);
            }
        }

        public EnviarDocumentoResponse ResolverEnvioDocumento(FirmadoResponse firmadoResponse, DocumentoElectronico documentoElectronico, long idEnvio, IndicadorExcepcion logDeEnvio)
        {
            EnviarDocumentoResponse envioDocumentoResponse = EnviarDocumento(firmadoResponse, documentoElectronico);
            int numeroIntentos = 0;
            do
            {
                if (!envioDocumentoResponse.Exito)//En caso que sunat NO recepciona el documento
                {
                    logDeEnvio.RegistroExcepcion = Environment.NewLine + envioDocumentoResponse.MensajeError;
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarIterativasEnvio);
                    envioDocumentoResponse = EnviarDocumento(firmadoResponse, documentoElectronico);
                }
                if (envioDocumentoResponse.Exito)
                {
                    if (CodigoFESunatSettings.Default.C1033.Equals(envioDocumentoResponse.CodigoRespuesta))
                    {
                        envioDocumentoResponse = ConsultarConstanciaDocumento(documentoElectronico);
                    }
                    ActualizarEnvioDeDocumento(envioDocumentoResponse, idEnvio);
                }
                numeroIntentos++;
            } while (!envioDocumentoResponse.Exito && numeroIntentos < FacturacionElectronicaSettings.Default.NumeroIntentosConsultaCDREnvioIndividual);
            return envioDocumentoResponse;
        }

        public EnviarDocumentoResponse EnviarDocumento(FirmadoResponse firmadoResponse, DocumentoElectronico documentoElectronico)
        {
            var documentoRequest = new EnviarDocumentoRequest
            {
                Ruc = documentoElectronico.Emisor.NroDocumento,
                UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                EndPointUrl = DevolverUrlEnvioSunatFacturacionElectronica(),
                IdDocumento = documentoElectronico.IdDocumento,
                TipoDocumento = documentoElectronico.TipoDocumento,
                TramaXmlFirmado = firmadoResponse.TramaXmlFirmado
            };
            return RestHelper<EnviarDocumentoRequest, EnviarDocumentoResponse>.Execute("api/EnviarDocumento", documentoRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
        }

        public EnviarDocumentoResponse ConsultarConstanciaDocumento(DocumentoElectronico documento)
        {
            try
            {
                var consultaConstanciaRequest = new ConsultaConstanciaRequest
                {
                    Ruc = documento.Emisor.NroDocumento,
                    UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                    ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                    EndPointUrl = FacturacionElectronicaSettings.Default.URLWebServiceSunatConsultaDocumento,
                    IdDocumento = documento.IdDocumento,
                    TipoDocumento = documento.TipoDocumento,
                    Serie = documento.IdDocumento.Split('-')[0],
                    Numero = Convert.ToInt32(documento.IdDocumento.Split('-')[1])
                };
                var respuestaConsulta = RestHelper<ConsultaConstanciaRequest, EnviarDocumentoResponse>.Execute("api/ConsultarConstancia", consultaConstanciaRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!respuestaConsulta.Exito)
                {
                    throw new LogicaException("Error al consultar la constancia de documento " + respuestaConsulta.MensajeError);
                }
                return respuestaConsulta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al consultar la constancia de documento", e);
            }
        }

        public OperationResult ActualizarEnvioDeDocumento(EnviarDocumentoResponse envioDocumentoResponse, long idEnvio)
        {
            try
            {
                Envio envioAActualizar = GenerarEnvioAActualizar(envioDocumentoResponse, idEnvio);
                //if (envioAActualizar.codigoRespuesta == FacturacionElectronicaSettings.Default.CodigoRespuestaSunatExisteDocumentoYaInformadoAnteriormente)
                //{
                //    envioAActualizar.estado = (int)EstadoEnvio.Aceptado;
                //}
                var resultadoActualizarEnvio = _facturacionDatos.ActualizarEnvio(envioAActualizar);
                Util.ManejoEnLogicaResultadoSinExito(resultadoActualizarEnvio, "Error al intentar actualizar envio");
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar el envio de documento", e);
            }
        }

        public Envio GenerarEnvioAActualizar(EnviarDocumentoResponse envioDocumentoResponse, long idEnvio)
        {
            try
            {
                OperationResult resultadoCrearBinario = null;
                if (envioDocumentoResponse.Exito)
                {
                    resultadoCrearBinario = CrearBinario(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(envioDocumentoResponse)));
                }
                var envioAActualizar = new Envio
                {
                    id = idEnvio,
                    estado = envioDocumentoResponse.Exito ? DeterminarEstadoDeEnvio(envioDocumentoResponse) : (int)EstadoEnvio.Pendiente,
                    codigoRespuesta = envioDocumentoResponse.CodigoRespuesta,
                    observacion = envioDocumentoResponse.Exito ? envioDocumentoResponse.MensajeRespuesta + " " + (string.IsNullOrEmpty(envioDocumentoResponse.MensajeError) ? "" : envioDocumentoResponse.MensajeError) : envioDocumentoResponse.MensajeError,
                    idBinarioRespuesta = null
                };
                envioAActualizar.idBinarioRespuesta = envioDocumentoResponse.Exito ? resultadoCrearBinario.data : envioAActualizar.idBinarioRespuesta;
                return envioAActualizar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar el envio de documento", e);
            }
        }

        public int DeterminarEstadoDeEnvio(EnviarDocumentoResponse enviarDocumentoResponse)
        {
            int estadoEnvio;
            if (enviarDocumentoResponse.Exito && !string.IsNullOrEmpty(enviarDocumentoResponse.TramaZipCdr))
            {
                var codigoDeRespuesta = Convert.ToInt32(enviarDocumentoResponse.CodigoRespuesta);
                estadoEnvio = (codigoDeRespuesta == FacturacionElectronicaSettings.Default.CodigoRespuestaAceptado) ?
                    (int)EstadoEnvio.Aceptado :
                        (codigoDeRespuesta <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaConExcepcion &&
                        codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaConExcepcion) ?
                            (int)EstadoEnvio.ConExcepcion :
                            (codigoDeRespuesta <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaRechazado
                            && codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaRechazado) ?
                                (int)EstadoEnvio.Rechazado :
                                    (codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaConObservacion) ?
                                        (int)EstadoEnvio.AceptadoConObservaciones :
                                        (int)EstadoEnvio.ConExcepcion;
                //Verificacion del codigo de respuesta con los codigos considerados aceptados en siges, para el cambio de estado a aceptado 
                if (estadoEnvio != (int)EstadoEnvio.Aceptado)
                    estadoEnvio = Diccionario.CodigoFESunatConsideradosAceptadosEnSiges.Contains(enviarDocumentoResponse.CodigoRespuesta) ? (int)EstadoEnvio.Aceptado : estadoEnvio;
            }
            else
            {
                estadoEnvio = (int)EstadoEnvio.ConExcepcion;
            }
            if (estadoEnvio != (int)EstadoEnvio.Aceptado)
            {
                EstablecimientoComercial sede = _sedeLogica.ObtenerSede();
                _mailer.SendEmail(AplicacionSettings.Default.CorreoParaNotificacionDeErrorHangfire, "COMPROBANTE NO ACEPTADO - " + "RUC: " + sede.DocumentoIdentidad + " - " + sede.Nombre + " (" + sede.NombreComercial + ")", "Hay un envio con estado " + Enumerado.GetDescription((EstadoEnvio)estadoEnvio) + " en " + sede.Nombre);
            }
            return estadoEnvio;
        }

        public FirmadoResponse FirmarDocumento(DocumentoResponse documentoResponse, byte[] archivoCertificado)
        {
            try
            {
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(archivoCertificado),
                    PasswordCertificado = FacturacionElectronicaSettings.Default.ClaveCertificadoDigital,
                };
                var respuestaFirmado = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("api/Firmar", firmado, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!respuestaFirmado.Exito)
                {
                    throw new LogicaException(respuestaFirmado.MensajeError);
                }
                return respuestaFirmado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al firmar el documento electronico", e);
            }
        }
        #endregion

        #region RESUMEN DE BOLETAS
        public OperationResult EnvioBoletas(LogEnvio logEnvio)
        {
            try
            {
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                Documento[] documentosPorDia;
                var noHayBoletasPorEnviar = DevolverBoletasIncluidoBinarioPorEnviarPorDia().Count == 0;
                do
                {
                    documentosPorDia = DevolverBoletasIncluidoBinarioPorEnviarPorDia().OrderBy(d => d.idSigescom).ToArray();
                    if (documentosPorDia.Length > 0)
                    {
                        var resultResumenDiario = ResolverResumenDiarioPorDia(sede, documentosPorDia, logEnvio);
                    }
                }
                while (documentosPorDia.Length > 0);
                //Traer los documentos invalidados y enviados y que hayan sido aceptados una sola vez. Esto es para eviar aquellos documentos invalidados(Estado Sigescom = 2) que en un proceso normal fueron informados como adicionados(Estado = 1) pero no pudoeron ser informados como eliminados(Estado = 3)
                Documento[] documentosInvalidados = ObtenerBoletasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente().ToArray();
                if (documentosInvalidados.Length > 0)
                {
                    // Consultamos los envios en caso existan algunos pendientes, para evitar que se vuelvan a enviar, los resumenes que comunican las invalidaciones
                    ConsultarTickets();
                    List<DateTime> fechasDeEmisionDistintas = documentosInvalidados.Select(d => d.fechaEmision.Date).Distinct().ToList();
                    foreach (var item in fechasDeEmisionDistintas)
                    {
                        try
                        {
                            var resultInvalidados = ResolverResumenDiario(sede, documentosInvalidados.Where(d => d.fechaEmision.Date == item).ToArray(), false, "Error al firmar y enviar los documentos invalidados", "Error al consultar el ticket de los documentos invalidados", ModoEnvio.Anulacion); //False: por que no debe de alterarce el estado de los documentos invalidados para enviarse a sunat
                            Util.ManejoEnLogicaResultadoSinExito(resultInvalidados, "Error al resolver el resumen diario para boletas invalidadas no enviadas");
                            foreach (var serie in documentosInvalidados.Select(ed => ed.serieComprobante).Distinct().ToList())
                            {
                                logEnvio.Exito.Add(ItemEnvio.ItemAnulado("[" + serie + ":" + documentosInvalidados.Where(s => s.serieComprobante == serie).OrderBy(d => Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "-" + documentosInvalidados.Where(s => s.serieComprobante == serie).OrderByDescending(d => Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "]"));
                            }
                        }
                        catch (Exception e)
                        {
                            logEnvio.Error.Add("Error al resolver el resumen diario" + " - " + Util.InicioErrorString(e));
                            logEnvio.LogError.Add("Error al resolver el resumen diario" + " - " + Util.SoloErrorString(e));
                        }
                    }
                }
                if (noHayBoletasPorEnviar && documentosInvalidados.Length == 0)
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay boletas de venta por enviar");
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver resumen diario de boletas", e);
            }
        }

        public OperationResult ResolverResumenDiarioPorDia(EstablecimientoComercialExtendido sede, Documento[] documentos, LogEnvio logEnvio)
        {
            try
            {
                int tamanyoLote = FacturacionElectronicaSettings.Default.TamanyoLoteResumenDiario;
                int contadorResumenesProcesados = 0;
                int totalResumenesAEnviar = Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(documentos.Length) / Convert.ToDecimal(tamanyoLote))));
                while (contadorResumenesProcesados < totalResumenesAEnviar)
                {
                    contadorResumenesProcesados++;
                    Documento[] documentosLote = documentos.Skip((contadorResumenesProcesados - 1) * tamanyoLote).Take(tamanyoLote).ToArray();
                    var resultResumen = ResolverResumenDiario(sede, documentosLote, true, "Error al firmar y enviar el resumen diario", "Error al consultar el ticket del resumen diario", ModoEnvio.Adicion); // true : debido a que se trata del primer envio aunque un documento este marcado como anulado, debe de enviarse como adiciona. Posteriormente se enviara un resumen como anulado
                    Util.ManageIfResultIsNotSuccess(resultResumen, "Error al itentar reolver el envio de resumen");
                    foreach (var serie in documentosLote.Select(ed => ed.serieComprobante).Distinct().ToList())
                    {
                        logEnvio.Exito.Add(ItemEnvio.ItemAdicionado("[" + serie + ":" + documentosLote.Where(s => s.serieComprobante == serie).OrderBy(d => Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "-" + documentosLote.Where(s => s.serieComprobante == serie).OrderByDescending(d => Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "]"));
                    }

                    if (resultResumen.code_result == OperationResultEnum.Success && ((int)resultResumen.information == (int)EstadoEnvio.Aceptado || (int)resultResumen.information == (int)EstadoEnvio.AceptadoConObservaciones))
                    {
                        if (documentosLote.Any(d => d.estadoSigescom == (int)EstadoSigescomDocumentoElectronico.Invalidado))
                        {
                            //Obteniendo invalidados del resumen enviado
                            Documento[] documentosInvalidados = documentosLote.Where(d => d.estadoSigescom == (int)EstadoSigescomDocumentoElectronico.Invalidado).ToArray();
                            //Resolviendo los documentos invalidados
                            var resultResumenInvalidado = ResolverResumenDiarioInvalidados(sede, documentosInvalidados);
                            Util.ManageIfResultIsNotSuccess(resultResumenInvalidado, "Error al itentar reolver el envio de resumen");
                            logEnvio.Exito.Add(ItemEnvio.ItemAnulado(string.Join(", ", documentosInvalidados.Select(d => d.ComprobanteDocumento()))));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al resolver el resumen diario" + " - " + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al resolver el resumen diario" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK"); ;
        }

        public OperationResult ResolverResumenDiarioInvalidados(EstablecimientoComercialExtendido sede, Documento[] documentosInvalidados)
        {
            try
            {
                int tamanyoLote = FacturacionElectronicaSettings.Default.TamanyoLoteResumenDiario;
                int contadorResumenesProcesados = 0;
                int totalResumenesAEnviar = Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(documentosInvalidados.Length) / Convert.ToDecimal(tamanyoLote))));
                while (contadorResumenesProcesados < totalResumenesAEnviar)
                {
                    contadorResumenesProcesados++;
                    Documento[] documentosLote = documentosInvalidados.Skip((contadorResumenesProcesados - 1) * tamanyoLote).Take(tamanyoLote).ToArray();
                    //Se envian los invalidados del resumen enviado para ser dados de baja
                    var resultTicketDeInvalidados = ResolverResumenDiario(sede, documentosLote, false, "Error al firmar y enviar los documentos invalidados", "Error al consultar el ticket de los documentos invalidados", ModoEnvio.Anulacion); //false: por que no debe de alterarce el estado de los documentos invalidados para enviarce a sunat
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el resumen diario invalidado", e);
            }
        }

        public OperationResult ResolverResumenDiario(EstablecimientoComercialExtendido sede, Documento[] documentos, bool cambiarEstadoItemDeAnuladoAAdicionado, string mensajeErrorEnvio, string mensajeErrorConsultaTicket, ModoEnvio modoEnvio)
        {
            try
            {
                var documentoResumenDiario = _generacionArchivosLogica.ObtenerResumenDiario(documentos, sede, cambiarEstadoItemDeAnuladoAAdicionado);
                var documentoResumenDiarioResponse = RestHelper<ResumenDiarioNuevo, DocumentoResponse>.Execute("api/GenerarResumenDiario/v2", documentoResumenDiario, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!documentoResumenDiarioResponse.Exito)
                {
                    throw new LogicaException(documentoResumenDiarioResponse.MensajeError);
                }
                else
                {
                    var archivoCertificado = ObtenerCertificado(documentoResumenDiario.Emisor.NroDocumento);
                    var firmadoResponse = FirmarDocumento(documentoResumenDiarioResponse, archivoCertificado);
                    var resultadoEnvio = EnviarResumenDiario(documentoResumenDiario, firmadoResponse, documentos.Select(d => d.id).ToArray(), modoEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultadoEnvio, mensajeErrorEnvio);
                    //Esperar un tiempo para poder consultar el ticket
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarEnvioResumen);
                    //Consultar el ticket para ver la respuesta y registrar el estado de envio
                    var resultadoTicket = ConsultarTicket(sede.DocumentoIdentidad, new EnvioSimplificado() { Id = resultadoEnvio.data, NumeroTicket = (string)resultadoEnvio.information });
                    Util.ManejoEnLogicaResultadoSinExito(resultadoTicket, mensajeErrorConsultaTicket);
                    return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK")
                    {
                        information = resultadoTicket.information
                    };
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar firmar, enviar y consultar un resumen diario", e);
            }
        }

        public OperationResult EnviarResumenDiario(ResumenDiarioNuevo resumenDiarioNuevo, FirmadoResponse firmadoResponse, long[] idDocumentos, ModoEnvio modoEnvio)
        {
            try
            {
                OperationResult resultCrearEnvio = null;
                try
                {
                    resultCrearEnvio = CrearEnvio(resumenDiarioNuevo.IdDocumento.Split('-')[2], FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario, (int)EstadoEnvio.Pendiente, "", "", "", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)), null, modoEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvio, " Error al intentar crear envio de resumen diario " + resumenDiarioNuevo.IdDocumento);
                }
                catch (Exception)
                {
                    return new OperationResult(OperationResultEnum.Information, "ERROR AL CREAR ENVIO", "ERROR");
                }
                var resultCrearEnvioDocumentoMasivo = CrearEnvioDocumentoMasivo(resultCrearEnvio.data, idDocumentos);
                Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvioDocumentoMasivo, "Error al intentar crear el envio documento masivo");
                var resultActualizarDocumentosAEnviados = ActualizarDocumentosAEnviados(idDocumentos);
                Util.ManejoEnLogicaResultadoSinExito(resultActualizarDocumentosAEnviados, "Error al intentar acturalizar los documentos a enviados");

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = resumenDiarioNuevo.Emisor.NroDocumento,
                    UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                    ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                    EndPointUrl = DevolverUrlEnvioSunatFacturacionElectronica(),
                    IdDocumento = resumenDiarioNuevo.IdDocumento,
                    TramaXmlFirmado = firmadoResponse.TramaXmlFirmado
                };
                var enviarResumenResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("api/EnviarResumen", enviarDocumentoRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!enviarResumenResponse.Exito)
                {
                    throw new LogicaException(enviarResumenResponse.MensajeError);
                }
                var envioAActualizar = new Envio
                {
                    id = resultCrearEnvio.data,
                    estado = enviarResumenResponse.Exito ? (int)EstadoEnvio.Pendiente : (int)EstadoEnvio.Rechazado,
                    observacion = string.IsNullOrEmpty(enviarResumenResponse.MensajeError) ? "" : enviarResumenResponse.MensajeError,
                    numeroTicket = enviarResumenResponse.NroTicket
                };
                ActualizarEstadoObservacionTicketEnvio(envioAActualizar);
                //var resultCrearEnvio = CrearEnvio(resumenDiarioNuevo.IdDocumento.Split('-')[2], FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario, enviarResumenResponse.Exito ? (int)EstadoEnvio.Pendiente : (int)EstadoEnvio.Rechazado, "", (string.IsNullOrEmpty(enviarResumenResponse.MensajeError) ? "" : enviarResumenResponse.MensajeError), enviarResumenResponse.NroTicket, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)), null, modoEnvio);
                //Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvio, " Error al intentar crear envio de resumen diario " + enviarResumenResponse.NroTicket + " " + resumenDiarioNuevo.IdDocumento + " " + (string.IsNullOrEmpty(enviarResumenResponse.MensajeError) ? "" : enviarResumenResponse.MensajeError));
                //var resultCrearEnvioDocumentoMasivo = CrearEnvioDocumentoMasivo(resultCrearEnvio.data, idDocumentos);
                //Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvioDocumentoMasivo, "Error al intentar crear el envio documento masivo");
                //var resultActualizarDocumentosAEnviados = ActualizarDocumentosAEnviados(idDocumentos);
                //Util.ManejoEnLogicaResultadoSinExito(resultActualizarDocumentosAEnviados, "Error al intentar acturalizar los documentos a enviados");
                OperationResult result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK")
                {
                    data = resultCrearEnvio.data,
                    information = enviarResumenResponse.NroTicket
                };
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al enviar resumen diario", e);
            }
        }

        #endregion

        #region NOTAS

        public OperationResult EnvioNotasCredito(LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                Documento[] notasCreditoPorEnviar = DevolverNotasCreditoIncluidoBinarioPorEnviar().ToArray();
                if (notasCreditoPorEnviar.Length > 0)
                {
                    result = ResolverEnvioNotas(notasCreditoPorEnviar, logEnvio);
                }
                var documentosIndividualesInvalidados = ObtenerNotasCreditoInvalidadasAceptadasPeroSinComunicacionDeBaja().ToArray();
                if (documentosIndividualesInvalidados.Count() > 0)
                {
                    var resultadoResolverComunicacionBajaNoComunicadas = ResolverComunicacionBaja(sede, documentosIndividualesInvalidados, logEnvio);
                }
                Documento[] documentosInvalidados = ObtenerNotasCreditoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente().ToArray();
                if (documentosInvalidados.Count() > 0)
                {
                    Documento[] documentosBoletasInvalidados = documentosInvalidados.Where(d => d.serieComprobante.Substring(0, 1) == FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoBoleta).ToArray();
                    List<DateTime> fechasDeEmisionDistintasInvalidadas = documentosBoletasInvalidados.Select(d => d.fechaEmision.Date).Distinct().ToList();
                    foreach (var item in fechasDeEmisionDistintasInvalidadas)
                    {
                        try
                        {
                            var documentosDelDia = documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item).ToArray();
                            var resultadoInvalidados = ResolverResumenDiario(sede, documentosDelDia, false, "Error al firmar y enviar los documentos invalidados de notas de boleta", "Error al consultar el ticket de los documentos invalidados de las notas de boleta", ModoEnvio.Anulacion); //False: por que no debe de alterarce el estado de los documentos invalidados para enviarse a sunat
                            Util.ManejoEnLogicaResultadoSinExito(resultadoInvalidados, "Error al resolver el resumen diario para notas de boletas invalidadas no enviadas");
                        }
                        catch (Exception e)
                        {
                            logEnvio.Error.Add(string.Join(", ", documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item)) + " - " + Util.InicioErrorString(e));
                            logEnvio.LogError.Add(string.Join(", ", documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item)) + " - " + Util.SoloErrorString(e));
                        }
                    }
                }
                if (notasCreditoPorEnviar.Length == 0 && documentosIndividualesInvalidados.Count() == 0 && documentosInvalidados.Count() == 0)
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay notas de credito por enviar");
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al intentar enviar notas de credito" + " - " + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al intentar enviar notas de credito" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult EnvioNotasDebito(LogEnvio logEnvio)
        {
            try
            {
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                Documento[] notasDebitoPorEnviar = DevolverNotasDebitoIncluidoBinarioPorEnviar().ToArray();
                if (notasDebitoPorEnviar.Length > 0)
                {
                    ResolverEnvioNotas(notasDebitoPorEnviar, logEnvio);
                }
                Documento[] documentosInvalidadosEnvioIndividual = ObtenerNotasDebitoInvalidadasAceptadasPeroSinComunicacionDeBaja().ToArray();
                if (documentosInvalidadosEnvioIndividual.Count() > 0)
                {
                    ResolverComunicacionBaja(sede, documentosInvalidadosEnvioIndividual, logEnvio);
                }
                Documento[] documentosInvalidadosEnvioResumen = ObtenerNotasDebitoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente().ToArray();
                if (documentosInvalidadosEnvioResumen.Count() > 0)
                {
                    Documento[] documentosBoletasInvalidados = documentosInvalidadosEnvioResumen.Where(d => d.serieComprobante.Substring(0, 1) == FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoBoleta).ToArray();
                    List<DateTime> fechasDeEmisionDistintasInvalidadas = documentosBoletasInvalidados.Select(d => d.fechaEmision.Date).Distinct().ToList();
                    foreach (var item in fechasDeEmisionDistintasInvalidadas)
                    {
                        try
                        {
                            var documentosDelDia = documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item).ToArray();
                            var resultadoInvalidados = ResolverResumenDiario(sede, documentosDelDia, false, "Error al firmar y enviar los documentos invalidados de notas de boleta", "Error al consultar el ticket de los documentos invalidados de las notas de boleta", ModoEnvio.Anulacion); //False: por que no debe de alterarce el estado de los documentos invalidados para enviarse a sunat
                            Util.ManejoEnLogicaResultadoSinExito(resultadoInvalidados, "Error al resolver el resumen diario para notas de boletas invalidadas no enviadas");
                        }
                        catch (Exception e)
                        {
                            logEnvio.Error.Add(string.Join(", ", documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item)) + " - " + Util.InicioErrorString(e));
                            logEnvio.LogError.Add(string.Join(", ", documentosBoletasInvalidados.Where(d => d.fechaEmision.Date == item)) + " - " + Util.SoloErrorString(e));
                        }
                    }
                }
                if (notasDebitoPorEnviar.Length == 0 && documentosInvalidadosEnvioIndividual.Count() == 0 && documentosInvalidadosEnvioResumen.Count() == 0)
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay notas de debito por enviar");
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al intentar enviar notas" + " - " + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al intentar enviar notas" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult ResolverEnvioNotas(Documento[] documentos, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                List<Documento> documentosNotasDeBoletas = new List<Documento>();
                List<Documento> documentosNotasDeFacturas = new List<Documento>();
                List<Documento> documentosNotasDeFacturasAdicionadas = new List<Documento>();
                List<Documento> documentosNotasDeFacturasAnuladas = new List<Documento>();
                //Separamos las notas de acuerdo al tipo de comprobantes: notas de boleta y notas de facturas
                documentosNotasDeFacturas.AddRange(documentos.Where(d => d.serieComprobante.Substring(0, 1) == FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoFactura));
                documentosNotasDeBoletas.AddRange(documentos.Where(d => d.serieComprobante.Substring(0, 1) == FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoBoleta));
                //Separamos las factura por estado adicionado y anulado
                documentosNotasDeFacturasAdicionadas = documentosNotasDeFacturas.Where(d => d.estado == (int)EstadoDocumentoElectronico.Adicionado).ToList();
                documentosNotasDeFacturasAnuladas = documentosNotasDeFacturas.Where(d => d.estado == (int)EstadoDocumentoElectronico.Anulado).ToList();
                if (documentosNotasDeFacturasAdicionadas.Count > 0)
                {
                    //Envio de notas de facturas de forma individual
                    var resultEnvioDocumentoNotasFacturasAdicionadas = ResolverEnvioIndividual(documentosNotasDeFacturasAdicionadas.ToArray(), logEnvio);
                }
                if (documentosNotasDeFacturasAnuladas.Count > 0)
                {
                    var resultadoResolverEnvioIndividual = ResolverEnvioIndividual(documentosNotasDeFacturasAnuladas.ToArray(), logEnvio);
                    List<Documento> documentosAceptadosParaDarDeBaja = ObtenerDocumentos(documentos.Select(d => d.id).ToList()).Where(d => d.EnvioDocumento.Count(ed => ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones) > 0).ToList();
                    if (documentosAceptadosParaDarDeBaja.Count() > 0)
                    {
                        List<DateTime> fechasDeEmisionDistintas = documentosAceptadosParaDarDeBaja.Select(d => d.fechaEmision.Date).Distinct().ToList();
                        foreach (var item in fechasDeEmisionDistintas)
                        {
                            //Envio de notas de facturas con estado de documento electronico anulado por comunicacion de baja
                            var resultadoEnvioDocumentoNotasFacturasAnuladas = ResolverComunicacionBaja(sede, documentosAceptadosParaDarDeBaja.Where(d => d.fechaEmision.Date == item).ToArray(), logEnvio);
                        }
                    }
                }
                //Separamos las boletas por dia para el envio de los comprobantes
                if (documentosNotasDeBoletas.Count() > 0)
                {
                    List<DateTime> fechasDeEmisionDistintas = documentosNotasDeBoletas.Select(d => d.fechaEmision.Date).Distinct().ToList();
                    foreach (var item in fechasDeEmisionDistintas)
                    {
                        var resultadoEnvioDocumentoNotasBoletas = ResolverResumenDiarioPorDia(sede, documentosNotasDeBoletas.Where(d => d.fechaEmision.Date == item).ToArray(), logEnvio);
                        Util.ManejoEnLogicaResultadoSinExito(resultadoEnvioDocumentoNotasBoletas, "Error al intentar enviar las notas de boletas");
                    }
                }

                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas", e);
            }
        }

        #endregion

        #region COMUNICACION DE BAJA

        public OperationResult EnvioComunicacionesBaja(LogEnvio logEnvio)
        {
            try
            {
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                var noHayComunicacionesAEnviar = !HayFacturasInvalidadasNoEnviadas();
                while (HayFacturasInvalidadasNoEnviadas())
                {
                    var documentos = DevolverFacturasInvalidadasNoEnviadasPorDia().ToArray();
                    var resultadoResolverEnvioIndividual = ResolverEnvioIndividual(documentos, logEnvio);
                    var documentosAceptadosParaDarDeBaja = ObtenerDocumentos(documentos.Select(d => d.id).ToList()).Where(d => d.EnvioDocumento.Count(ed => ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones) > 0).ToList();
                    if (documentosAceptadosParaDarDeBaja.Count() > 0)
                    {
                        var resultadoResolverComunicacionBaja = ResolverComunicacionBaja(sede, documentosAceptadosParaDarDeBaja.ToArray(), logEnvio);
                    }
                }
                //Traer las facturas invalidadas y enviadas aceptadas una sola vez. Para eviar como una comunicacion de baja, ya que fueron informados como adicionado y falta comunicar su baja
                var documentosInvalidados = ObtenerFacturasInvalidadasAceptadasPeroSinComunicacionDeBaja().ToArray();
                if (documentosInvalidados.Length > 0)
                {
                    var fechasDeEmisionDistintas = documentosInvalidados.Select(d => d.fechaEmision.Date).Distinct().ToList();
                    foreach (var item in fechasDeEmisionDistintas)
                    {
                        var documentosPorFecha = documentosInvalidados.Where(d => d.fechaEmision.Date == item).ToArray();
                        var resultadoInvalidados = ResolverComunicacionBaja(sede, documentosPorFecha, logEnvio);
                    }
                }
                if (logEnvio.NoHayDocumentos && noHayComunicacionesAEnviar && documentosInvalidados.Length == 0)
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay facturas por enviar");
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al resolver comunicaciones de bajas de facturas" + "-" + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al resolver comunicaciones de bajas de facturas" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult ResolverComunicacionBaja(EstablecimientoComercialExtendido sede, Documento[] documentos, LogEnvio logEnvio)
        {
            try
            {
                ComunicacionBaja comunicacionBaja = _generacionArchivosLogica.ObtenerComunicacionBaja(documentos, sede);
                var documentoResponse = RestHelper<ComunicacionBaja, DocumentoResponse>.Execute("api/GenerarComunicacionBaja", comunicacionBaja, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!documentoResponse.Exito)
                {
                    throw new LogicaException(documentoResponse.MensajeError);
                }
                else
                {
                    var archivoCertificado = ObtenerCertificado(sede.DocumentoIdentidad);
                    var firmadoResponse = FirmarDocumento(documentoResponse, archivoCertificado);
                    var resultadoEnvio = EnviarComunicacionBaja(sede, comunicacionBaja, firmadoResponse, documentos.Select(d => d.id).ToArray());
                    Util.ManejoEnLogicaResultadoSinExito(resultadoEnvio, "Error al resolver comunicación de baja");
                    //Esperar un tiempo para poder consultar el ticket
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarEnvioResumen);
                    //Consultar el ticket para ver la respuesta, se registrara el estado de envio
                    OperationResult resultadoTicket = ConsultarTicket(sede.DocumentoIdentidad, new EnvioSimplificado() { Id = resultadoEnvio.data, NumeroTicket = (string)resultadoEnvio.information });
                    Util.ManejoEnLogicaResultadoSinExito(resultadoTicket, "Error al consultar ticket de comunicación de baja");
                    logEnvio.Exito.Add(ItemEnvio.ItemAnulado(string.Join(", ", documentos.Select(d => d.ComprobanteDocumento()))));
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add(string.Join(", ", documentos.Select(d => d.ComprobanteDocumento())) + "-" + Util.InicioErrorString(e));
                logEnvio.LogError.Add(string.Join(", ", documentos.Select(d => d.ComprobanteDocumento())) + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult EnviarComunicacionBaja(EstablecimientoComercial sede, ComunicacionBaja comunicacionBaja, FirmadoResponse firmadoResponse, long[] idDocumentos)
        {
            try
            {
                OperationResult result = null;
                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = sede.DocumentoIdentidad,
                    UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                    ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                    EndPointUrl = DevolverUrlEnvioSunatFacturacionElectronica(),
                    IdDocumento = comunicacionBaja.IdDocumento,
                    TipoDocumento = FacturacionElectronicaSettings.Default.CodigoTipoDocumentoResumenDiario,
                    TramaXmlFirmado = firmadoResponse.TramaXmlFirmado
                };
                var enviarResumenResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("api/EnviarResumen", enviarDocumentoRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!enviarResumenResponse.Exito)
                {
                    throw new LogicaException(enviarResumenResponse.MensajeError);
                }
                var resultCrearEnvio = CrearEnvio(comunicacionBaja.IdDocumento.Split('-')[2], FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja, enviarResumenResponse.Exito ? (int)EstadoEnvio.Pendiente : (int)EstadoEnvio.Rechazado, "", (string.IsNullOrEmpty(enviarResumenResponse.MensajeError) ? "" : enviarResumenResponse.MensajeError), enviarResumenResponse.NroTicket, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)), null, ModoEnvio.Ninguno);
                Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvio, " Error al intentar crear envio de comunicacion de baja " + enviarResumenResponse.NroTicket + " " + comunicacionBaja.IdDocumento + " " + (string.IsNullOrEmpty(enviarResumenResponse.MensajeError) ? "" : enviarResumenResponse.MensajeError));
                for (int i = 0; i < idDocumentos.Length; i++)
                {
                    var resultCrearEnvioDocumento = CrearEnvioDocumento(resultCrearEnvio.data, idDocumentos[i]);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvioDocumento, "Error al guardar el envio de documentos");
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK")
                {
                    data = resultCrearEnvio.data,
                    information = enviarResumenResponse.NroTicket
                };
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al enviar la comunicacion de baja", e);
            }
        }

        #endregion

        /*#region GUIA DE REMISION

        public OperationResult EnvioGuiasDeRemision(LogEnvio logEnvio)
        {
            try
            {
                Documento[] documentos = DevolverGuiasDeRemisionIncluidoBinarioPorEnviar().ToArray();
                if (documentos.Length > 0)
                {
                    ResolverEnvioIndividualGuia(documentos, logEnvio);
                }
                else
                {
                    logEnvio.LogNoHayDocumentos(true, "No hay guias de remision por enviar");
                }
            }
            catch (Exception e)
            {
                logEnvio.Error.Add("Error al enviar guias de remision" + " - " + Util.InicioErrorString(e));
                logEnvio.LogError.Add("Error al enviar guias de remision" + " - " + Util.SoloErrorString(e));
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        public OperationResult ResolverEnvioIndividualGuia(Documento[] documentos, LogEnvio logEnvio)
        {
            for (int i = 0; i < documentos.Length; i++)
            {
                try
                {
                    GuiaRemision guiaRemision = JsonConvert.DeserializeObject<GuiaRemision>(Encoding.UTF8.GetString(documentos[i].Binario.archivoBinario));
                    DocumentoResponse documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute(ObtenerMetodoApi(guiaRemision.TipoDocumento), guiaRemision, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                    if (!documentoResponse.Exito)
                    {
                        throw new LogicaException(documentoResponse.MensajeError);
                    }
                    var archivoCertificado = ObtenerCertificado(guiaRemision.Remitente.NroDocumento);
                    var firmadoResponse = FirmarDocumento(documentoResponse, archivoCertificado);
                    OperationResult result = EnviarGuiaRemision(guiaRemision, firmadoResponse, documentos[i].id);
                    Util.ManageIfResultIsNotSuccess(result, "Error al crear el envio de documento de guia");
                    logEnvio.Exito.Add(ItemEnvio.ItemAdicionado(guiaRemision.IdDocumento));
                }
                catch (Exception e)
                {
                    logEnvio.Error.Add(documentos[i].ComprobanteDocumento() + ": " + Util.InicioErrorString(e));
                    logEnvio.LogError.Add(documentos[i].ComprobanteDocumento() + " - " + Util.SoloErrorString(e));
                }
            }
            return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
        }

        private OperationResult EnviarGuiaRemision(GuiaRemision guiaRemision, FirmadoResponse firmadoResponse, long idDocumento)
        {
            try
            {
                int estadoEnvio;
                var documentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = guiaRemision.Remitente.NroDocumento,
                    UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                    ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                    EndPointUrl = DevolverUrlEnvioSunatGuiaDeRemisionElectronica(),
                    IdDocumento = guiaRemision.IdDocumento,
                    TipoDocumento = guiaRemision.TipoDocumento,
                    TramaXmlFirmado = firmadoResponse.TramaXmlFirmado
                };
                var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarDocumentoResponse>.Execute("api/EnviarDocumento", documentoRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                estadoEnvio = DeterminarEstadoDeEnvio(enviarDocumentoResponse);
                var resultCrearEnvio = CrearEnvio("", FacturacionElectronicaSettings.Default.TipoEnvioIndividual, estadoEnvio, enviarDocumentoResponse.CodigoRespuesta, enviarDocumentoResponse.MensajeRespuesta + " " + (string.IsNullOrEmpty(enviarDocumentoResponse.MensajeError) ? "" : enviarDocumentoResponse.MensajeError), "", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(enviarDocumentoResponse)), ModoEnvio.Ninguno);
                Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvio, " Error al intentar crear envio de individual de guia" + enviarDocumentoResponse.NombreArchivo + " " + guiaRemision.IdDocumento + " " + (string.IsNullOrEmpty(enviarDocumentoResponse.MensajeError) ? "" : enviarDocumentoResponse.MensajeError));
                var resultadoActualizarDocumentoAEnviado = ActualizarDocumentoAEnviado(idDocumento);
                Util.ManejoEnLogicaResultadoSinExito(resultadoActualizarDocumentoAEnviado, "Error al intentar actualizar el documento " + guiaRemision.IdDocumento + " a enviado");
                OperationResult result = CrearEnvioDocumento(resultCrearEnvio.data, idDocumento);
                Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvio, " Error al intentar crear el envio documento");
                return new OperationResult(OperationResultEnum.Success, result.title, estadoEnvio);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar enviar la guia de remision", e);
            }
        }

        #endregion*/

        #region GUIA DE REMISION

        public async Task<OperationResult> EnviarGuiaDeRemisionManual(DateTime fechaDesde, DateTime fechaHasta, EstablecimientoComercialExtendido sede, int idEmpleado, string path)
        {
            try
            {
                List<long> idsGuiasRemision = _operacionLogica.ObtenerIdsGuiaRemisionPorEnviarSunat(fechaDesde, fechaHasta);
                foreach (var idGuiaRemision in idsGuiasRemision)
                {
                    await TransmitirEnviarGuiaDeRemision(idGuiaRemision, sede, idEmpleado, path);
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al enviar guias de remision de manera manual", e);
            }
        }

        public async Task<OperationResult> TransmitirEnviarGuiaDeRemision(long idMovimiento, EstablecimientoComercialExtendido sede, int idEmpleado, string path)
        {
            try
            {
                MovimientoDeAlmacen movimiento = _operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
                var detalleMaestroComprobante = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroDocumento);
                var proveedores = _actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidades = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroModalidadDeTraslado);
                var motivos = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroMotivoDeTraslado);
                GuiaDeRemision guiaDeRemision = new GuiaDeRemision(movimiento, sede, new EstablecimientoComercialExtendidoConLogo(movimiento.Transaccion().Actor_negocio2.Actor_negocio2), null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas, proveedores, modalidades, motivos);
                GuiaRemision guiaRemision = _generacionArchivosLogica.ObtenerDocumentoElectronicoGuiaDeRemision(guiaDeRemision);
                Envio envioGuia = await ResolverEnvioGuiaRemision(guiaRemision, path);
                var resultado = await CrearGuiaDeRemision(guiaDeRemision, detalleMaestroComprobante, envioGuia, idEmpleado);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al transmitir y enviar las guias de remision", e);
            }
        }


        public async Task<OperationResult> CrearGuiaDeRemision(GuiaDeRemision guiasDeRemision, List<Detalle_maestro> detallesMaestroComprobante, Envio envio, int idEmpleado)
        {
            try
            {
                GuiaRemision guiaRemision = _generacionArchivosLogica.ObtenerDocumentoElectronicoGuiaDeRemision(guiasDeRemision);
                Documento documento = new Documento
                {
                    idSigescom = guiasDeRemision.IdOrden,
                    codigoTipoComprobante = guiaRemision.TipoDocumento,
                    serieComprobante = guiaRemision.IdDocumento.Split('-')[0],
                    numeroComprobante = guiaRemision.IdDocumento.Split('-')[1],
                    fechaEmision = DateTime.Parse(guiaRemision.FechaEmision),
                    tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == guiaRemision.TipoDocumento).nombre,
                    estado = guiasDeRemision.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                    estadoSigescom = guiasDeRemision.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                    Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(guiaRemision)) }
                };
                EnvioDocumento envioDocumento = new EnvioDocumento
                {
                    Envio = envio,
                    Documento = documento
                };
                Evento_transaccion eventoTransaccion = new Evento_transaccion
                {
                    id_empleado = idEmpleado,
                    id_transaccion = guiasDeRemision.IdOrden,
                    id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                    fecha = DateTimeUtil.FechaActual(),
                    comentario = "Estado cuando se transmite a facturacion electronica"
                };
                var resultCrearEnvioDocumento = _facturacionDatos.CrearEnvioDocumento(envioDocumento);
                Util.ManejoEnLogicaResultadoSinExito(resultCrearEnvioDocumento, "Error al intentar crear el envio documento");
                OperationResult resultado;
                if (envio.codigoRespuesta == FacturacionElectronicaSettings.Default.CodigoRespuestaAceptado.ToString())
                {
                    var urlGuiaRemisionSunat = await GenerarUrlGuiaRemisionRespuestaSunat(JsonConvert.DeserializeObject<EnviarDocumentoResponse>(Encoding.UTF8.GetString(envio.Binario1.archivoBinario)).TramaZipCdr);
                    resultado = _operacionLogica.CrearEventoTransaccionInformacionTransaccion(eventoTransaccion, urlGuiaRemisionSunat);
                    Util.ManejoEnLogicaResultadoSinExito(resultado, "Error al intentar crear el evento de transaccion y actualizar el estado transaccion");
                }
                else
                {
                    resultado = _operacionLogica.CrearEventoTransaccion(eventoTransaccion);
                    Util.ManejoEnLogicaResultadoSinExito(resultado, "Error al intentar crear el evento de transaccion");
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear guias de remision en facturación electrónica", e);
            }
        }
        public async Task<string> GenerarUrlGuiaRemisionRespuestaSunat(string constanciaRecepcion)
        {
            string urlGuiaRemisionSunat = string.Empty;
            var returnByte = Convert.FromBase64String(constanciaRecepcion);
            using (var memRespuesta = new MemoryStream(returnByte))
            {
                using (var zipFile = new ZipArchive(memRespuesta, ZipArchiveMode.Read))
                {
                    foreach (var entry in zipFile.Entries)
                    {
                        if (!entry.Name.EndsWith(".xml")) continue;
                        using (var ms = entry.Open())
                        {
                            var responseReader = new StreamReader(ms);
                            var responseString = await responseReader.ReadToEndAsync();

                            var xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(responseString);
                            var xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                            xmlnsManager.AddNamespace("ar", "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2");
                            xmlnsManager.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                            xmlnsManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                            urlGuiaRemisionSunat = xmlDoc.SelectSingleNode("/ar:ApplicationResponse/cac:DocumentResponse/cac:DocumentReference/cbc:DocumentDescription", xmlnsManager)?.InnerText;
                        }
                    }
                }
            }
            return urlGuiaRemisionSunat;
        }


        public async Task<Envio> ResolverEnvioGuiaRemision(GuiaRemision guiaRemision, string path)
        {
            try
            {
                DocumentoResponse documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute(ObtenerMetodoApi(guiaRemision.TipoDocumento), guiaRemision, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!documentoResponse.Exito)
                {
                    throw new LogicaException(documentoResponse.MensajeError);
                }
                var archivoCertificado = ObtenerCertificado(guiaRemision.Remitente.NroDocumento);
                var firmadoResponse = FirmarGuiaRemision(documentoResponse, archivoCertificado);
                var envioGuiaRemision = await EnviarGuiaRemision(guiaRemision, firmadoResponse, path);
                return envioGuiaRemision;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de guias de remision", e);
            }
        }

        public FirmadoResponse FirmarGuiaRemision(DocumentoResponse documentoResponse, byte[] archivoCertificado)
        {
            try
            {
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(archivoCertificado),
                    PasswordCertificado = FacturacionElectronicaSettings.Default.ClaveCertificadoDigital,
                };
                var respuestaFirmado = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("api/FirmarGuiaRemision", firmado, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!respuestaFirmado.Exito)
                {
                    throw new LogicaException(respuestaFirmado.MensajeError);
                }
                return respuestaFirmado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al firmar la guia de remisión", e);
            }
        }

        private async Task<Envio> EnviarGuiaRemision(GuiaRemision guiaRemision, FirmadoResponse firmadoResponse, string path)
        {
            try
            {
                var tokenEnvioResponse = ObtenerTokenEnvioGuiaRemision(guiaRemision.Remitente.NroDocumento);
                var envioDocumentoResponse = await EnvioGuiaRemision(guiaRemision, firmadoResponse, tokenEnvioResponse, path);
                var obtenerRespuestaSunat = true;
                var numeroIntentos = 0;
                RespuestaEnvioDocumentoResponse respuestaEnvioDocumentoResponse;
                do
                {
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarRespuestaGuiaRemisionEnMilisegundos);
                    respuestaEnvioDocumentoResponse = ObtenerRespuestaEnvioGuiaRemision(envioDocumentoResponse, tokenEnvioResponse);
                    obtenerRespuestaSunat = respuestaEnvioDocumentoResponse.codRespuesta == FacturacionElectronicaSettings.Default.CodigoApiEnProcesoRespuestaGuiaRemision;
                    numeroIntentos++;
                } while (obtenerRespuestaSunat && FacturacionElectronicaSettings.Default.NumeroIntentosConsultaCDREnvioIndividual >= numeroIntentos);
                EnviarDocumentoResponse enviarDocumentoResponse = respuestaEnvioDocumentoResponse.Convertir();
                var envio = GenerarEnvio(firmadoResponse, envioDocumentoResponse, enviarDocumentoResponse);
                return envio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar enviar la guia de remision", e);
            }
        }

        public Envio GenerarEnvio(FirmadoResponse firmadoResponse, EnvioDocumentoResponse envioDocumentoResponse, EnviarDocumentoResponse enviarDocumentoResponse)
        {
            try
            {
                var estadoEnvio = DeterminarEstadoDeEnvioGuiaRemision(enviarDocumentoResponse);
                estadoEnvio = enviarDocumentoResponse.CodigoRespuesta == FacturacionElectronicaSettings.Default.CodigoApiEnProcesoRespuestaGuiaRemision ? (int)EstadoEnvio.Pendiente : estadoEnvio;
                Envio envio = new Envio
                {
                    fechaEnvio = DateTimeUtil.FechaActual(),
                    identificadorEnvio = "",
                    tipoEnvio = FacturacionElectronicaSettings.Default.TipoEnvioIndividual,
                    estado = estadoEnvio,
                    codigoRespuesta = enviarDocumentoResponse.CodigoRespuesta,
                    observacion = enviarDocumentoResponse.MensajeRespuesta,
                    numeroTicket = envioDocumentoResponse.numTicket,
                    modoEnvio = (int)ModoEnvio.Ninguno
                };
                Binario envioBinario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(firmadoResponse)) };
                envio.Binario = envioBinario;
                Binario respuestaBinario = enviarDocumentoResponse.CodigoRespuesta == FacturacionElectronicaSettings.Default.CodigoApiEnProcesoRespuestaGuiaRemision ? null : new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(enviarDocumentoResponse)) };
                envio.Binario1 = respuestaBinario;
                return envio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar el envio ", e);
            }
        }

        private TokenEnvioResponse ObtenerTokenEnvioGuiaRemision(string ruc)
        {
            try
            {
                var tokenEnvioResponse = RestHelper<string, TokenEnvioResponse>.GetTokenSeguridad(ruc);
                return tokenEnvioResponse;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener token envio de guia de remision", e);
            }
        }

        private async Task<EnvioDocumentoResponse> EnvioGuiaRemision(GuiaRemision guiaRemision, FirmadoResponse firmadoResponse, TokenEnvioResponse tokenEnvioResponse, string path)
        {
            try
            {
                var nombreDocumento = $"{guiaRemision.Remitente.NroDocumento}-{MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente}-{guiaRemision.IdDocumento}";
                var urlApiCpeSunatDocumento = $"{FacturacionElectronicaSettings.Default.UrlApiCpeSunatEnvio}{nombreDocumento}";
                var archivoZip = await GenerarZip(firmadoResponse.TramaXmlFirmado, nombreDocumento, path);
                var hashArchivoZip = Obtener256Hash($"{path}/{nombreDocumento}.zip");
                File.Delete($"{path}/{nombreDocumento}.zip");
                var autorizacion = $"Bearer {tokenEnvioResponse.access_token}";
                var envioDocumentoRequest = new EnvioDocumentoRequest
                {
                    archivo = new Archivo
                    {
                        arcGreZip = archivoZip,
                        hashZip = hashArchivoZip,
                        nomArchivo = $"{nombreDocumento}.zip",
                    }
                };
                var envioDocumentoJson = JsonConvert.SerializeObject(envioDocumentoRequest);
                var envioDocumentoResponse = RestHelper<string, EnvioDocumentoResponse>.Execute(urlApiCpeSunatDocumento, autorizacion, envioDocumentoJson);
                return envioDocumentoResponse;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar realizar el envio de la guia de remision", e);
            }
        }

        private RespuestaEnvioDocumentoResponse ObtenerRespuestaEnvioGuiaRemision(EnvioDocumentoResponse envioDocumentoResponse, TokenEnvioResponse tokenEnvioResponse)
        {
            try
            {
                var urlRequest = $"{FacturacionElectronicaSettings.Default.UrlApiCpeSunatRespuesta}{envioDocumentoResponse.numTicket}";
                var autorizacion = $"Bearer {tokenEnvioResponse.access_token}";
                var respuestaEnvioDocumentoResponse = RestHelper<string, RespuestaEnvioDocumentoResponse>.ExecuteGet(urlRequest, autorizacion);
                return respuestaEnvioDocumentoResponse;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener la respuesta de envio de guia de remision", e);
            }
        }
        public int DeterminarEstadoDeEnvioGuiaRemision(EnviarDocumentoResponse enviarDocumentoResponse)
        {
            int estadoEnvio;
            if (enviarDocumentoResponse.Exito)
            {
                var codigoDeRespuesta = Convert.ToInt32(enviarDocumentoResponse.CodigoRespuesta);
                estadoEnvio = (codigoDeRespuesta == FacturacionElectronicaSettings.Default.CodigoRespuestaAceptado) ?
                    (int)EstadoEnvio.Aceptado :
                        (codigoDeRespuesta <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaConExcepcion &&
                        codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaConExcepcion) ?
                            (int)EstadoEnvio.ConExcepcion :
                            (codigoDeRespuesta <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaRechazado
                            && codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaRechazado) ?
                                (int)EstadoEnvio.Rechazado :
                                    (codigoDeRespuesta >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaConObservacion) ?
                                        (int)EstadoEnvio.AceptadoConObservaciones :
                                        (int)EstadoEnvio.ConExcepcion;
                //Verificacion del codigo de respuesta con los codigos considerados aceptados en siges, para el cambio de estado a aceptado 
                if (estadoEnvio != (int)EstadoEnvio.Aceptado)
                    estadoEnvio = Diccionario.CodigoFESunatConsideradosAceptadosEnSiges.Contains(enviarDocumentoResponse.CodigoRespuesta) ? (int)EstadoEnvio.Aceptado : estadoEnvio;
            }
            else
            {
                estadoEnvio = (int)EstadoEnvio.ConExcepcion;
            }
            return estadoEnvio;
        }
        public async Task<string> GenerarZip(string tramaXml, string nombreArchivo, string path)
        {
            byte[] memoryStreamBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                {
                    var zipItem = zipArchive.CreateEntry($"{nombreArchivo}.xml");
                    using (var zipFile = zipItem.Open())
                    {
                        var data = Convert.FromBase64String(tramaXml);
                        await zipFile.WriteAsync(data, 0, data.Length);
                    }
                }
                memoryStreamBytes = memoryStream.ToArray();
            }
            File.WriteAllBytes($"{path}/{nombreArchivo}.zip", memoryStreamBytes);
            var zipResult = File.ReadAllBytes($"{path}/{nombreArchivo}.zip");
            var resultado = Convert.ToBase64String(zipResult);
            return resultado;
        }

        public string Obtener256Hash(string filename)
        {
            HashAlgorithm hash = new SHA256Managed();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            string resul = ArrayToString(hash.ComputeHash(fs));
            fs.Close();
            return resul;
        }

        private string ArrayToString(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder(byteArray.Length);
            for (int i = 0; i <= byteArray.Length - 1; i++)
                sb.Append(byteArray[i].ToString("X2").ToLower());
            return sb.ToString();
        }

        public OperationResult ConsultarGuiasRemisionManual()
        {
            try
            {
                EstablecimientoComercial sede = _sedeLogica.ObtenerSede();
                List<EnvioSimplificado> enviosSinCodigoRespuesta = ObtenerEnviosSinCodigoDeRespuesta();
                List<EnvioSimplificado> enviosConTicketSinCodigoRespuestaTicketGuiaRemision = enviosSinCodigoRespuesta.Where(e => !string.IsNullOrEmpty(e.NumeroTicket) && e.CodigoTipoDocumento == MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();

                foreach (var envioAConsultar in enviosConTicketSinCodigoRespuestaTicketGuiaRemision)
                {
                    ResolverConsultaTicketGuiaRemision(sede.DocumentoIdentidad, envioAConsultar);
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al consultar guias de remision", e);
            }
        }
        #endregion
    }
}

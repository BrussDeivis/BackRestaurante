using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.EFactura;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Utilitarios.RestHelper;

namespace Tsp.FacturacionElectronica.Logica
{

    public partial class FacturacionElectronicaLogica
    {
        #region CONSULTA DE TICKETS

        public OperationResult ConsultarTickets()
        {
            try
            {
                //Obtener todos los envios que no tengan codigo de respuesta
                OperationResult result = null;
                EstablecimientoComercial sede = _sedeLogica.ObtenerSede();
                List<EnvioSimplificado> enviosSinCodigoRespuesta = ObtenerEnviosSinCodigoDeRespuesta();
                List<EnvioSimplificado> enviosSinTicketCodigoRespuesta = enviosSinCodigoRespuesta.Where(e => string.IsNullOrEmpty(e.NumeroTicket)).ToList();
                List<EnvioSimplificado> enviosIndividualesSinCodigoRespuesta = enviosSinTicketCodigoRespuesta.Where(e => string.IsNullOrEmpty(e.NumeroTicket) && FacturacionElectronicaSettings.Default.TipoEnvioIndividual.Equals(e.TipoEnvio)).ToList();
                List<EnvioSimplificado> enviosConTicketSinCodigoRespuestaTicket = enviosSinCodigoRespuesta.Where(e => !string.IsNullOrEmpty(e.NumeroTicket) && e.CodigoTipoDocumento != MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
                List<EnvioSimplificado> enviosConTicketSinCodigoRespuestaTicketGuiaRemision = enviosSinCodigoRespuesta.Where(e => !string.IsNullOrEmpty(e.NumeroTicket) && e.CodigoTipoDocumento == MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
                foreach (var envioAConsultar in enviosIndividualesSinCodigoRespuesta)
                {
                    try
                    {
                        var documentoElectronico = new DocumentoElectronico()
                        {
                            Emisor = new Compania() { NroDocumento = sede.DocumentoIdentidad },
                            IdDocumento = envioAConsultar.SerieDocumento + "-" + envioAConsultar.NumeroDocumento,
                            TipoDocumento = envioAConsultar.CodigoTipoDocumento
                        };
                        var respuestaConsulta = ResolverConsultaDocumento(documentoElectronico, envioAConsultar.Id);
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (var envioAConsultar in enviosConTicketSinCodigoRespuestaTicket)
                {
                    try
                    {
                        var respuestaConsulta = ResolverConsultaTicket(sede.DocumentoIdentidad, envioAConsultar);
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (var envioAConsultar in enviosConTicketSinCodigoRespuestaTicketGuiaRemision)
                {
                    try
                    {
                        ResolverConsultaTicketGuiaRemision(sede.DocumentoIdentidad, envioAConsultar);
                    }
                    catch (Exception)
                    {
                    }
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al consultar tickets", e);
            }
        }

        public EnviarDocumentoResponse ResolverConsultaDocumento(DocumentoElectronico documentoElectronico, long idEnvio)
        {

            var envioDocumentoResponse = ConsultarConstanciaDocumento(documentoElectronico);
            int numeroIntentos = 1;
            do
            {
                if (!envioDocumentoResponse.Exito)//En caso que sunat NO recepciona el documento
                {
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarIterativasEnvio);
                    envioDocumentoResponse = ConsultarConstanciaDocumento(documentoElectronico);
                }
                else
                {
                    ActualizarEnvioDeDocumento(envioDocumentoResponse, idEnvio);
                }
                numeroIntentos++;
            } while (!envioDocumentoResponse.Exito || numeroIntentos < FacturacionElectronicaSettings.Default.NumeroIntentosConsultaCDREnvioIndividual);
            return envioDocumentoResponse;
        }

        public OperationResult ConsultarTicket(string ruc, EnvioSimplificado envioAConsultar)
        {
            try
            {
                OperationResult result = null;
                var respuestaConsulta = ResolverConsultaTicket(ruc, envioAConsultar);
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK")
                {
                    information = DeterminarEstadoDeEnvio(respuestaConsulta)
                };
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar consultar en sunat", e);
            }
        }

        public EnviarDocumentoResponse ResolverConsultaTicket(string ruc, EnvioSimplificado envioAConsultar)
        {
            var envioDocumentoResponse = ConsultarConstanciaTicket(ruc, envioAConsultar);
            int numeroIntentos = 1;
            do
            {
                if (!envioDocumentoResponse.Exito)//En caso que sunat NO recepciona el documento
                {
                    Thread.Sleep(FacturacionElectronicaSettings.Default.TiempoEsperaParaConsultarIterativasEnvio);
                    envioDocumentoResponse = ConsultarConstanciaTicket(ruc, envioAConsultar);
                }
                else
                {
                    ActualizarEnvioDeDocumento(envioDocumentoResponse, envioAConsultar.Id);
                }
                numeroIntentos++;
            } while (!envioDocumentoResponse.Exito || numeroIntentos < FacturacionElectronicaSettings.Default.NumeroIntentosConsultaCDREnvioResumen);
            return envioDocumentoResponse;
        }

        public EnviarDocumentoResponse ConsultarConstanciaTicket(string ruc, EnvioSimplificado envioAConsultar)
        {
            try
            {
                var consultaTicketRequest = new ConsultaTicketRequest
                {
                    Ruc = ruc,
                    UsuarioSol = FacturacionElectronicaSettings.Default.UsuarioSol,
                    ClaveSol = FacturacionElectronicaSettings.Default.ClaveSol,
                    EndPointUrl = DevolverUrlEnvioSunatFacturacionElectronica(),
                    NroTicket = envioAConsultar.NumeroTicket
                };
                var respuestaConsulta = RestHelper<ConsultaTicketRequest, EnviarDocumentoResponse>.Execute("api/ConsultarTicket", consultaTicketRequest, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!respuestaConsulta.Exito)
                {
                    throw new LogicaException("Error al consultar el ticket " + envioAConsultar.NumeroTicket + " - Error " + respuestaConsulta.MensajeError);
                }
                return respuestaConsulta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al consultar la constancia de ticket", e);
            }
        }
        public void ResolverConsultaTicketGuiaRemision(string ruc, EnvioSimplificado envioAConsultar)
        {
            try
            {
                OperationResult resultado = new OperationResult();
                var tokenEnvioResponse = ObtenerTokenEnvioGuiaRemision(ruc);
                EnvioDocumentoResponse envioDocumentoResponse = new EnvioDocumentoResponse() { numTicket = envioAConsultar.NumeroTicket };
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
                if(obtenerRespuestaSunat == false)
                {
                    EnviarDocumentoResponse enviarDocumentoResponse = respuestaEnvioDocumentoResponse.Convertir();
                    ActualizarEnvioDeDocumento(enviarDocumentoResponse, envioAConsultar.Id);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver la consulta de guia de remision", e);
            }
        }
        #endregion
    }
}

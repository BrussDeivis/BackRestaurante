using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica : IFacturacionElectronicaLogica
    {
        public IFacturacionRepositorio _facturacionDatos;
        public IGeneracionArchivosLogica _generacionArchivosLogica;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly IOperacionLogica _operacionLogica;
        private readonly IEstablecimiento_Repositorio _establecimientoDatos;

        private readonly ISede_Logica _sedeLogica;

        private readonly IMaestroLogica _maestroLogica;
        private readonly IMailer _mailer;

        public FacturacionElectronicaLogica(IFacturacionRepositorio facturacionDatos, IActorNegocioLogica actorNegocioLogica, IOperacionLogica operacionLogica, IMaestroLogica maestroLogica, IGeneracionArchivosLogica generacionArchivosLogica, IMailer mailer, ISede_Logica sedeLogica, IEstablecimiento_Repositorio establecimientoDatos)
        {
            _facturacionDatos = facturacionDatos;
            _generacionArchivosLogica = generacionArchivosLogica;
            _actorNegocioLogica = actorNegocioLogica;
            _operacionLogica = operacionLogica;
            _maestroLogica = maestroLogica;
            _mailer = mailer;
            _sedeLogica = sedeLogica;
            _establecimientoDatos = establecimientoDatos;
        }

        public ActorComercial ObtenerEmisor(int idEmisor)
        {
            try
            {
                return _actorNegocioLogica.ObtenerActorComercial(idEmisor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public byte[] ObtenerCertificado(string nombreCertificado)
        {
            try
            {
                string downloadUrl = FacturacionElectronicaSettings.Default.UrlServidorFptCertificadosDigitales;
                string usuario = FacturacionElectronicaSettings.Default.UsuarioServidorFptCertificadosDigitales;
                string password = FacturacionElectronicaSettings.Default.PasswordServidorFptCertificadosDigitales;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}.pfx", downloadUrl, nombreCertificado));
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sourceURI + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(usuario, password);
                request.UseBinary = true;
                request.Proxy = null;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Stream responseStream = request.GetResponse().GetResponseStream();
                Stream stream = response.GetResponseStream();

                //Convertir a array de bytes el certificado
                MemoryStream memoryStream = new MemoryStream();
                byte[] chunk = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
                {
                    memoryStream.Write(chunk, 0, bytesRead);
                }
                response.Close();
                stream.Close();
                //Retornamos el certificado
                return memoryStream.ToArray();
            }
            catch (Exception)
            {
                throw new LogicaException("Error al obtener el certificado digital");
            }
        }

        #region DOCUMENTO

        public OperationResult CrearDocumentosMasivos(List<Documento> documentosAIngresar)
        {
            try
            {
                return _facturacionDatos.CrearDocumentosMasivos(documentosAIngresar);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error en logica al crear documentos masivos", e);
            }
        }

        public OperationResult ActualizarDocumentoAEnviado(long idDocumento)
        {
            try
            {
                return _facturacionDatos.ActualizarDocumentoAEnviado(idDocumento);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar los documentos enviados", e);
            }
        }

        public OperationResult ActualizarDocumentosAEnviados(long[] idDocumentos)
        {
            try
            {
                return _facturacionDatos.ActualizarDocumentosAEnviados(idDocumentos);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar los documentos enviados", e);
            }
        }

        public List<Documento> ObtenerDocumentos(List<long> idDocumentos)
        {
            try
            {
                //Debido a que la actualizacion de estados se ejecutan por comandos sql y no se refresca el context, Se crea una nueva instancia de facturacion datos para la obtencion de los documentos ya actualizados. 
                IFacturacionRepositorio nuevaFacturacionDatos = _facturacionDatos.ObtenerNuevoRepositorio();
                return nuevaFacturacionDatos.ObtenerDocumentos(idDocumentos).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Documento ObtenerDocumentoElectronicoIncluidoBinario(long id)
        {
            try
            {
                //Debido a que la actualizacion de estados se ejecutan por comandos sql y no se refresca el context, Se crea una nueva instancia de facturacion datos para la obtencion de los documentos ya actualizados. 
                IFacturacionRepositorio nuevaFacturacionDatos = _facturacionDatos.ObtenerNuevoRepositorio();
                return nuevaFacturacionDatos.ObtenerDocumentoElectronicoIncluidoBinario(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Documento> ObtenerDocumentosEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosEntreFechas(fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener documentos entre fechas", e);
            }
        }

        #endregion

        #region ENVIO 

        public OperationResult CrearEnvio(string identificadorEnvio, string tipoEnvio, int estado, string codigoRespuesta, string observacion, string numeroTicket, byte[] envio, byte[] respuesta, ModoEnvio modoEnvio)
        {
            try
            {
                Envio nuevoEnvio = new Envio
                {
                    fechaEnvio =DateTimeUtil.FechaActual(),
                    identificadorEnvio = identificadorEnvio,
                    tipoEnvio = tipoEnvio,
                    estado = estado,
                    codigoRespuesta = codigoRespuesta,
                    observacion = observacion,
                    numeroTicket = numeroTicket,
                    modoEnvio = (int)modoEnvio
                };
                Binario envioBinario = new Binario { archivoBinario = envio };
                nuevoEnvio.Binario = envioBinario;
                if (respuesta != null)
                {
                    Binario respuestaBinario = new Binario { archivoBinario = respuesta };
                    nuevoEnvio.Binario1 = respuestaBinario;
                }
                else
                {
                    nuevoEnvio.idBinarioRespuesta = null;
                }
                return _facturacionDatos.CrearEnvio(nuevoEnvio);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear envio ", e);
            }
        }

        public OperationResult ActualizarEstadoEnvio(Envio envioAActualizar)
        {
            try
            {
                return _facturacionDatos.ActualizarEstadoEnvio(envioAActualizar);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarEstadoObservacionTicketEnvio(Envio envioAActualizar)
        {
            try
            {
                return _facturacionDatos.ActualizarEstadoObservacionTicketEnvio(envioAActualizar);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarEnvios(List<Envio> enviosAActualizar)
        {
            try
            {
                return _facturacionDatos.ActualizarEnvios(enviosAActualizar);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar envios", e);
            }
        }

        public Envio ObtenerEnvio(long idEnvio)
        {
            try
            {
                return _facturacionDatos.ObtenerEnvio(idEnvio);
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR AL OBTENER EN ENVIO", e);
            }
        }

        public List<EnvioSimplificado> ObtenerEnviosSinCodigoDeRespuesta()
        {
            try
            {
                return _facturacionDatos.ObtenerEnviosSinCodigoDeRespuesta().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Envio> ObtenerEnviosEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _facturacionDatos.ObtenerEnviosInclusiveEnvioDocumentoYDocumentoEntreFechas(fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener envios entre fechas", e);

            }
        }

        /// <summary>
        /// Devuelve aquellos envios de (documentos individuales, comunicaciones de baja y resumenes) con excepcion.
        /// </summary>
        public List<Envio> ObteneEnviosConExcepcionAReenviar()
        {
            try
            {
                List<Envio> enviosPorReenviar = new List<Envio>();
                //Se obtiene los envios Con Excepcion. Como no han sido aceptados ni rechazados, deben volver a enviarse.
                enviosPorReenviar = _facturacionDatos.ObtenerEnviosIncluidoDocumentoIncluidoBinario((int)EstadoEnvio.ConExcepcion).ToList();
                return enviosPorReenviar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Envio> ObteneEnviosPendientesAReenviar()
        {
            try
            {
                List<Envio> enviosPorReenviar = new List<Envio>();
                //Se obtiene los envios pendientes. que deben de volver a enviarse.
                enviosPorReenviar = _facturacionDatos.ObtenerEnviosIncluidoDocumentoIncluidoBinario((int)EstadoEnvio.Pendiente).ToList();
                return enviosPorReenviar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region ENVIO DOCUMENTO

        public OperationResult CrearEnvioDocumento(long idEnvio, long idDocumento)
        {
            try
            {
                EnvioDocumento nuevoEnvioDocumento = new EnvioDocumento
                {
                    idEnvio = idEnvio,
                    idDocumento = idDocumento
                };
                return _facturacionDatos.CrearEnvioDocumento(nuevoEnvioDocumento);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear encio de documento", e);
            }
        }

        public OperationResult CrearEnvioDocumentoMasivo(long idEnvio, long[] idDocumentos)
        {
            try
            {
                return _facturacionDatos.CrearEnvioDocumentoMasivo(idEnvio, idDocumentos);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al crear envios de documentos", e);
            }
        }

        #endregion

        #region BINARIO

        public OperationResult CrearBinario(byte[] archivo)
        {
            try
            {
                Binario nuevoBinario = new Binario
                {
                    archivoBinario = archivo
                };
                return _facturacionDatos.CrearBinario(nuevoBinario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear binario", e);
            }
        }

        public byte[] ObtenerArchivo(long id)
        {
            try
            {
                return _facturacionDatos.ObtenerBinario(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarBinario(Binario binario)
        {
            try
            {
                return _facturacionDatos.ActualizarBinario(binario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
       
    }
}

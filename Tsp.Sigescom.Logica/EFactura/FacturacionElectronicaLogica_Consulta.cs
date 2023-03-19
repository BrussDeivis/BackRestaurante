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

namespace Tsp.FacturacionElectronica.Logica
{ 
    public partial class FacturacionElectronicaLogica
    {
        #region BOLETA DE VENTA

        public bool HayBoletasNoEnviadas()
        {
            try
            {
                //hay algun documento del tipo boleta como no enviado
                return _facturacionDatos.HayDocumentosNoEnviados(MaestroSettings.Default.CodigoDetalleMaestroBoleta);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar  averiguar si existen boletas no enviadas. ", e);
            }
        }

        public List<Documento> DevolverBoletasIncluidoBinarioPorEnviarPorDia()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviarPorDia(MaestroSettings.Default.CodigoDetalleMaestroBoleta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener boletas a enviar por dia", e);
            }
        }

        public List<Documento> ObtenerBoletasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente()
        {
            try
            {
                int[] estadosDeEnvioQueNoSeQuiere = new int[] { (int)EstadoEnvio.Pendiente, (int)EstadoEnvio.Rechazado };
                //Devuelve los documentos invalidados que hayan sido enviados y aceptados con o sin observacion una sola vez y tambien que no tenga envios en estado pendiente 
                return _facturacionDatos.ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico.Invalidado, 1, MaestroSettings.Default.CodigoDetalleMaestroBoleta, estadosDeEnvioQueNoSeQuiere).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener boletas invalidadas", e);
            }
        }

        public List<Documento> ObtenerFacturasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente()
        {
            try
            {
                int[] estadosDeEnvioQueNoSeQuiere = new int[] { (int)EstadoEnvio.Pendiente, (int)EstadoEnvio.Rechazado };
                //Devuelve los documentos invalidados que hayan sido enviados y aceptados con o sin observacion una sola vez y tambien que no tenga envios en estado pendiente 
                return _facturacionDatos.ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico.Invalidado, 1, MaestroSettings.Default.CodigoDetalleMaestroFactura, estadosDeEnvioQueNoSeQuiere).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener boletas invalidadas", e);
            }
        }

        #endregion

        #region FACTURAS 

        public List<Documento> DevolverFacturasNoInvalidadasIncluidoBinarioPorEnviar()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviar(MaestroSettings.Default.CodigoDetalleMaestroFactura, (int)EstadoDocumentoElectronico.Adicionado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region NOTAS 

        public List<Documento> DevolverNotasCreditoIncluidoBinarioPorEnviar()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviar(MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Documento> DevolverNotasDebitoIncluidoBinarioPorEnviar()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviar(MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Documento> DevolverGuiasDeRemisionIncluidoBinarioPorEnviar()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviar(MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Documento> DevolverGuiaDeRemisionIncluidoBinarioPorEnviar(long idDocumento)
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosIncluidoBinarioAEnviar(MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene las notas de credito invalidadas que solo se hayan enviado una vez y que no tengan envio pendiente, se obtiene para enviarse enn estado de anulacion
        /// </summary>
        /// <returns></returns>
        public List<Documento> ObtenerNotasCreditoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente()
        {
            try
            {
                int[] estadosDeEnvioQueNoSeQuiere = new int[] { (int)EstadoEnvio.Pendiente, (int)EstadoEnvio.Rechazado };
                return _facturacionDatos.ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico.Invalidado, 1, MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito, estadosDeEnvioQueNoSeQuiere).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene las notas de debito invalidadas que solo se hayan enviado una vez y que no tengan envio pendiente, se obtiene para enviarse enn estado de anualcion
        /// </summary>
        /// <returns></returns>
        public List<Documento> ObtenerNotasDebitoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente()
        {
            try
            {
                int[] estadosDeEnvioQueNoSeQuiere = new int[] { (int)EstadoEnvio.Pendiente, (int)EstadoEnvio.Rechazado };
                return _facturacionDatos.ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico.Invalidado, 1, MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito, estadosDeEnvioQueNoSeQuiere).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region COMUNICACION DE BAJA

        public bool HayFacturasInvalidadasNoEnviadas()
        {
            try
            {
                return _facturacionDatos.HayDocumentosNoEnviados(MaestroSettings.Default.CodigoDetalleMaestroFactura, (int)EstadoDocumentoElectronico.Anulado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Documento> DevolverFacturasInvalidadasNoEnviadasPorDia()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentosAEnviarPorDia(MaestroSettings.Default.CodigoDetalleMaestroFactura, (int)EstadoDocumentoElectronico.Anulado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Trae las facturas que esten invalidadas en sigescom, que se hayan enviado y aceptado por envio indivudual y no tengan la comunicacion de baja respectiva
        public List<Documento> ObtenerFacturasInvalidadasAceptadasPeroSinComunicacionDeBaja()
        {
            try
            {
                return _facturacionDatos.ObtenerDocumentos(MaestroSettings.Default.CodigoDetalleMaestroFactura, EstadoSigescomDocumentoElectronico.Invalidado, FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja, 0, FacturacionElectronicaSettings.Default.TipoEnvioIndividual, 1).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Trae las notas de credito que esten invalidadas en sigescom, que se hayan enviado y aceptado por envio indivudual y no tengan la comunicacion de baja respectiva
        public List<Documento> ObtenerNotasCreditoInvalidadasAceptadasPeroSinComunicacionDeBaja()
        {
            try
            {
                List<Documento> documentos = _facturacionDatos.ObtenerDocumentos(MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito, EstadoSigescomDocumentoElectronico.Invalidado, FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja, 0, FacturacionElectronicaSettings.Default.TipoEnvioIndividual, 1).ToList();
                return documentos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Trae las notas de debito que esten invalidadas en sigescom, que se hayan enviado y aceptado por envio indivudual y no tengan la comunicacion de baja respectiva
        public List<Documento> ObtenerNotasDebitoInvalidadasAceptadasPeroSinComunicacionDeBaja()
        {
            try
            {
                List<Documento> documentos = _facturacionDatos.ObtenerDocumentos(MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito, EstadoSigescomDocumentoElectronico.Invalidado, FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja, 0, FacturacionElectronicaSettings.Default.TipoEnvioIndividual, 1).ToList();
                return documentos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}

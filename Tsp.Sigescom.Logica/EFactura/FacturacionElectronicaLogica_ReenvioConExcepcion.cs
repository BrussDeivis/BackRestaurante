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

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica
    {
        #region REENVIAR ENVIOS QUE TIENEN ESTADO POR EXCEPCION

        public async Task<OperationResult> ResolverEnviosConExcepcion()
        {
            try
            {
                LogEnvioFacturacionElectronica logEnvioFacturacion = new LogEnvioFacturacionElectronica();
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                List<Envio> enviosConExcepcion = ObteneEnviosConExcepcionAReenviar();
                if (enviosConExcepcion.Count() > 0)
                {
                    //Separar en tipos de envio
                    List<Envio> enviosIndividualesConExcepcion = enviosConExcepcion.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioIndividual).ToList();
                    List<Envio> enviosResumenDiariosDeBoletasYNotasConExcepcion = enviosConExcepcion.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario).ToList();
                    List<Envio> enviosComunicacionBajaConExcepcion = enviosConExcepcion.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja).ToList();
                    //Separar el envio individual de las factura
                    List<Envio> enviosFacturasConExcepcion = enviosIndividualesConExcepcion.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroFactura).ToList();
                    //Separar el envio individual de las guias de remision
                    List<Envio> enviosGuiasConExcepcion = enviosIndividualesConExcepcion.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
                    //Separar el envio individual de las notas
                    List<Envio> enviosNotasCreditoConExcepcion = enviosIndividualesConExcepcion.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito).ToList();
                    List<Envio> enviosNotasDebitoConExcepcion = enviosIndividualesConExcepcion.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito).ToList();

                    //Resolver resumenes de boleta
                    ResolverResumenDiarioConExcepcion(sede, enviosResumenDiariosDeBoletasYNotasConExcepcion, logEnvioFacturacion.BoletasVenta);
                    //Resolver facturas
                    ResolverEnvioFacturasConExcepcion(enviosFacturasConExcepcion, logEnvioFacturacion.Factura);
                    //Resolver Notas
                    ResolverEnvioNotasCreditoConExcepcion(enviosNotasCreditoConExcepcion, enviosNotasDebitoConExcepcion, logEnvioFacturacion.NotaCredito);
                    //resolver comunicacion de baja
                    ResolverComunicacionBajaConExcepcion(sede, enviosComunicacionBajaConExcepcion, logEnvioFacturacion.Factura);
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new Exception("Error al resolver envios con excepcion de comprobantes electrónicos ", e);
            }
        }

        public OperationResult ResolverResumenDiarioConExcepcion(EstablecimientoComercialExtendido sede, List<Envio> enviosConExcepcion, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                foreach (Envio envio in enviosConExcepcion)
                {
                    if (ModoEnvio.Anulacion == (ModoEnvio)envio.modoEnvio)
                    {
                        Documento[] documentosDeEnvioConExcepcion = envio.EnvioDocumento.Select(ed => ed.Documento).ToArray();
                        var resultResolverResumenDiarioInvalidado = ResolverResumenDiarioInvalidados(sede, documentosDeEnvioConExcepcion);
                        Util.ManejoEnLogicaResultadoSinExito(resultResolverResumenDiarioInvalidado, "Error al resolver el envio de resumen diario de anuladas con excepcion");
                    }
                    else
                    {
                        Documento[] documentosDeEnvioConExcepcion = envio.EnvioDocumento.Select(ed => ed.Documento).ToArray();
                        var resultResolverResumenDiario = ResolverResumenDiarioPorDia(sede, documentosDeEnvioConExcepcion, logEnvio);
                        Util.ManejoEnLogicaResultadoSinExito(resultResolverResumenDiario, "Error al resolver el envio de resumen diario con excepcion");
                    }
                }
                var resultActualizarEstadosEnviosConExcepcion = ActualizarEstadosEnviosConExcepcion(enviosConExcepcion);
                Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosConExcepcion, "Error al actualizar estados de envios con excepcion a desacartados por excepcion");
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar el resumen diario con excepcion", e);
            }
        }

        public OperationResult ResolverEnvioFacturasConExcepcion(List<Envio> enviosConExcepcion, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (enviosConExcepcion.Count > 0)
                {
                    var resultResolverEnvioIndividual = ResolverEnvioIndividual(enviosConExcepcion.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);

                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioIndividual, "Error al resolver el envio de facturas con excepcion");
                    var resultActualizarEstadosEnviosConExcepcion = ActualizarEstadosEnviosConExcepcion(enviosConExcepcion);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosConExcepcion, "Error al actualizar estados de envios con excepcion a desacartados por excepcion");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de facturas con excepcion", e);
            }
        }

        public OperationResult ResolverEnvioNotasCreditoConExcepcion(List<Envio> notasCreditoConExcepcion, List<Envio> notasDebitoConExcepcion, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (notasCreditoConExcepcion.Count > 0)
                {
                    var resultResolverEnvioNotas = ResolverEnvioNotas(notasCreditoConExcepcion.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioNotas, "Error al resolver el envio de notas credito con excepcion");
                    var resultActualizarEstadosEnviosConExcepcion = ActualizarEstadosEnviosConExcepcion(notasCreditoConExcepcion);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosConExcepcion, "Error al actualizar estados de envios con excepcion a desacartados por excepcion");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas credito con excepcion", e);
            }
        }
        public OperationResult ResolverEnvioNotasDebitoConExcepcion(List<Envio> notasCreditoConExcepcion, List<Envio> notasDebitoConExcepcion, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (notasDebitoConExcepcion.Count > 0)
                {
                    var resultResolverEnvioNotas = ResolverEnvioNotas(notasDebitoConExcepcion.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioNotas, "Error al resolver el envio de notas debito con excepcion");
                    var resultActualizarEstadosEnviosConExcepcion = ActualizarEstadosEnviosConExcepcion(notasDebitoConExcepcion);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosConExcepcion, "Error al actualizar estados de envios con excepcion a desacartados por excepcion");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas debito con excepcion", e);
            }
        }

        public OperationResult ResolverComunicacionBajaConExcepcion(EstablecimientoComercialExtendido sede, List<Envio> enviosConExcepcion, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (enviosConExcepcion.Count > 0)
                {
                    var resultResolverComunicacionBaja = ResolverComunicacionBaja(sede, enviosConExcepcion.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverComunicacionBaja, "Error al resolver las comunicaciones de baja con excepcion");
                    var resultActualizarEstadosEnviosConExcepcion = ActualizarEstadosEnviosConExcepcion(enviosConExcepcion);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosConExcepcion, "Error al actualizar estados de envios con excepcion a desacartados por excepcion");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver las comunicaciones de baja con excepcion", e);
            }
        }

        public OperationResult ActualizarEstadosEnviosConExcepcion(List<Envio> envios)
        {
            try
            {
                OperationResult result = new OperationResult();
                foreach (var envio in envios)
                {
                    envio.estado = (int)EstadoEnvio.DescartadoPorExcepcion;
                    result = ActualizarEstadoEnvio(envio);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar los estados de envios con excepcion", e);
            }
        }

        #endregion

    }
}

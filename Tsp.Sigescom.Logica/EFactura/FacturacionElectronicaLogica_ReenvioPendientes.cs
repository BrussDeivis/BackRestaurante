using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica
    {
        #region REENVIAR ENVIOS QUE TIENEN ESTADO PENDIENTE

        public OperationResult ResolverEnviosPendientes()
        {
            try
            {
                LogEnvioFacturacionElectronica logEnvioFacturacion = new LogEnvioFacturacionElectronica();
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                List<Envio> enviosPendientes = ObteneEnviosPendientesAReenviar();
                if (enviosPendientes.Count() > 0)
                {
                    //Separar en tipos de envio
                    List<Envio> enviosIndividualesPendientes = enviosPendientes.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioIndividual).ToList();
                    List<Envio> enviosResumenDiariosDeBoletasYNotasPendientes = enviosPendientes.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario).ToList();
                    List<Envio> enviosComunicacionBajaPendientes = enviosPendientes.Where(e => e.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja).ToList();
                    //Separar el envio individual de las factura
                    List<Envio> enviosFacturasPendientes = enviosIndividualesPendientes.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroFactura).ToList();
                    //Separar el envio individual de las guias de remision
                    List<Envio> enviosGuiasPendientes = enviosIndividualesPendientes.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente).ToList();
                    //Separar el envio individual de las notas
                    List<Envio> enviosNotasCreditoPendientes = enviosIndividualesPendientes.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito).ToList();
                    List<Envio> enviosNotasDebitoPendientes = enviosIndividualesPendientes.Where(e => e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito).ToList();

                    //Resolver resumenes de boleta
                    ResolverResumenDiarioPendientes(sede, enviosResumenDiariosDeBoletasYNotasPendientes, logEnvioFacturacion.BoletasVenta);
                    //Resolver facturas
                    ResolverEnvioFacturasPendientes(enviosFacturasPendientes, logEnvioFacturacion.Factura);
                    //Resolver Notas
                    ResolverEnvioNotasCreditoPendientes(enviosNotasCreditoPendientes, enviosNotasDebitoPendientes, logEnvioFacturacion.NotaCredito);
                    //resolver comunicacion de baja
                    ResolverComunicacionBajaPendientes(sede, enviosComunicacionBajaPendientes, logEnvioFacturacion.Factura);
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new Exception("Error al resolver envios pendientes de comprobantes electrónicos ", e);
            }
        }

        public OperationResult ResolverResumenDiarioPendientes(EstablecimientoComercialExtendido sede, List<Envio> enviosPendientes, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                foreach (Envio envio in enviosPendientes)
                {
                    if (ModoEnvio.Anulacion == (ModoEnvio)envio.modoEnvio)
                    {
                        Documento[] documentosDeEnvioPendientes = envio.EnvioDocumento.Select(ed => ed.Documento).ToArray();
                        var resultResolverResumenDiarioInvalidado = ResolverResumenDiarioInvalidados(sede, documentosDeEnvioPendientes);
                        Util.ManejoEnLogicaResultadoSinExito(resultResolverResumenDiarioInvalidado, "Error al resolver el envio de resumen diario de anuladas pendientes");
                    }
                    else
                    {
                        Documento[] documentosDeEnvioPendientes = envio.EnvioDocumento.Select(ed => ed.Documento).ToArray();
                        var resultResolverResumenDiario = ResolverResumenDiarioPorDia(sede, documentosDeEnvioPendientes, logEnvio);
                        Util.ManejoEnLogicaResultadoSinExito(resultResolverResumenDiario, "Error al resolver el envio de resumen diario pendientes");
                    }
                }
                var resultActualizarEstadosEnviosPendientes = ActualizarEstadosEnviosPendientes(enviosPendientes);
                Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosPendientes, "Error al actualizar estados de envios pendientes a desacartados por pendientes");
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar el resumen diario pendientes", e);
            }
        }

        public OperationResult ResolverEnvioFacturasPendientes(List<Envio> enviosPendientes, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (enviosPendientes.Count > 0)
                {
                    var resultResolverEnvioIndividual = ResolverEnvioIndividual(enviosPendientes.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);

                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioIndividual, "Error al resolver el envio de facturas pendientes");
                    var resultActualizarEstadosEnviosPendientes = ActualizarEstadosEnviosPendientes(enviosPendientes);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosPendientes, "Error al actualizar estados de envios pendientes a desacartados por pendientes");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de facturas pendientes", e);
            }
        }

        public OperationResult ResolverEnvioNotasCreditoPendientes(List<Envio> notasCreditoPendientes, List<Envio> notasDebitoPendientes, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (notasCreditoPendientes.Count > 0)
                {
                    var resultResolverEnvioNotas = ResolverEnvioNotas(notasCreditoPendientes.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioNotas, "Error al resolver el envio de notas credito pendientes");
                    var resultActualizarEstadosEnviosPendientes = ActualizarEstadosEnviosPendientes(notasCreditoPendientes);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosPendientes, "Error al actualizar estados de envios pendientes a desacartados por pendientes");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas credito pendientes", e);
            }
        }
        public OperationResult ResolverEnvioNotasDebitoPendientes(List<Envio> notasCreditoPendientes, List<Envio> notasDebitoPendientes, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (notasDebitoPendientes.Count > 0)
                {
                    var resultResolverEnvioNotas = ResolverEnvioNotas(notasDebitoPendientes.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverEnvioNotas, "Error al resolver el envio de notas debito pendientes");
                    var resultActualizarEstadosEnviosPendientes = ActualizarEstadosEnviosPendientes(notasDebitoPendientes);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosPendientes, "Error al actualizar estados de envios pendientes a desacartados por pendientes");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas debito pendientes", e);
            }
        }

        public OperationResult ResolverComunicacionBajaPendientes(EstablecimientoComercialExtendido sede, List<Envio> enviosPendientes, LogEnvio logEnvio)
        {
            try
            {
                OperationResult result = null;
                if (enviosPendientes.Count > 0)
                {
                    var resultResolverComunicacionBaja = ResolverComunicacionBaja(sede, enviosPendientes.SelectMany(e => e.EnvioDocumento).Select(ed => ed.Documento).ToArray(), logEnvio);
                    Util.ManejoEnLogicaResultadoSinExito(resultResolverComunicacionBaja, "Error al resolver las comunicaciones de baja pendientes");
                    var resultActualizarEstadosEnviosPendientes = ActualizarEstadosEnviosPendientes(enviosPendientes);
                    Util.ManejoEnLogicaResultadoSinExito(resultActualizarEstadosEnviosPendientes, "Error al actualizar estados de envios pendientes a desacartados por pendientes");
                    result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver las comunicaciones de baja pendientes", e);
            }
        }

        public OperationResult ActualizarEstadosEnviosPendientes(List<Envio> envios)
        {
            try
            {
                OperationResult result = new OperationResult();
                foreach (var envio in envios)
                {
                    envio.estado = (int)EstadoEnvio.Descartado;
                    result = ActualizarEstadoEnvio(envio);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar los estados de envios pendientes", e);
            }
        }

        #endregion

    }
}

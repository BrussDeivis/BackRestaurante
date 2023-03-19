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
        #region REENVIAR ENVIOS QUE TIENEN ESTADO RECHAZADO

        public OperationResult ResolverEnvioRechazado(int idEnvio)
        {
            try
            {
                LogEnvioFacturacionElectronica logEnvioFacturacion = new LogEnvioFacturacionElectronica();
                OperationResult result = null;
                Envio envio = ObtenerEnvio(idEnvio);
                var documentoPrevio = ObtenerDocumentoElectronicoIncluidoBinario(envio.EnvioDocumento.First().Documento.id);
                bool envioEstaEnPendiente = false;
                if (1 == documentoPrevio.EnvioDocumento.Where(ed => ed.Envio.estado == (int)EstadoEnvio.Pendiente).Count())
                {
                    envioEstaEnPendiente = true;
                }
                if (envioEstaEnPendiente)
                {
                    ConsultarTickets();
                    var documentoConsultado = ObtenerDocumentoElectronicoIncluidoBinario(envio.EnvioDocumento.First().Documento.id);
                    if (0 == documentoPrevio.EnvioDocumento.Where(ed => ed.Envio.estado == (int)EstadoEnvio.Pendiente).Count())
                    {
                        var resultActualizarEstadoEnvioRechazado = ActualizarEstadoEnvioRechazado(envio);
                        Util.ManejoEnLogicaResultadoSinExito(result, "Error al actualizar el estado del envio rechazado a descartado por rechazo");
                    }
                    else
                    {
                        throw new LogicaException("Error al intentar consultar un envio pendiente");
                    }
                }
                else
                {
                    EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                    List<string> codigosNotas = new List<string> { MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito, MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito };
                    if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioIndividual)
                    {
                        if (envio.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroFactura)
                        {
                            result = ResolverEnvioFactura(envio);
                        }
                        else if (codigosNotas.Contains(envio.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante))
                        {
                            result = ResolverEnvioNota(envio);
                        }
                    }
                    else if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario)
                    {
                        result = ResolverResumenDiario(sede, envio);
                    }
                    else if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja)
                    {
                        result = ResolverComunicacionBaja(sede, envio);
                    }
                    Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el envio rechazado");
                    var resultActualizarEstadoEnvioRechazado = ActualizarEstadoEnvioRechazado(envio);
                    Util.ManejoEnLogicaResultadoSinExito(result, "Error al actualizar el estado del envio rechazado a descartado por rechazo");
                }
                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver el envio rechazado", e);
            }
        }

        public OperationResult ResolverEnvioPendiente(int idEnvio)
        {
            try
            {
                OperationResult result = null;
                Envio envio = ObtenerEnvio(idEnvio);
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                List<string> codigosNotas = new List<string> { MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito, MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito };
                if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioIndividual)
                {
                    if (envio.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroFactura)
                    {
                        result = ResolverEnvioFactura(envio);
                    }
                    else if (codigosNotas.Contains(envio.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante))
                    {
                        result = ResolverEnvioNota(envio);
                    }
                }
                else if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario)
                {
                    result = ResolverResumenDiario(sede, envio);
                }
                else if (envio.tipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja)
                {
                    result = ResolverComunicacionBaja(sede, envio);
                }
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el envio pendiente");
                var resultActualizarEstadoEnvioPendiente = ActualizarEstadoEnvioPendiente(envio);
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al actualizar el estado del envio pendiente a descartado");

                return new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver el envio pendiente", e);
            }
        }

        public OperationResult ResolverResumenDiario(EstablecimientoComercialExtendido sede, Envio envio)
        {
            try
            {
                OperationResult result;
                if (envio.modoEnvio == (int)ModoEnvio.Adicion)
                {
                    result = ResolverResumenDiarioPorDia(sede, envio.EnvioDocumento.Select(ed => ed.Documento).ToArray(), new LogEnvio());
                    Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el resumen diario");
                }
                else
                {
                    result = ResolverResumenDiarioInvalidados(sede, envio.EnvioDocumento.Select(ed => ed.Documento).ToArray());
                    Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el resumen diario");
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el resumen diario", e);
            }
        }

        public OperationResult ResolverEnvioFactura(Envio envio)
        {
            try
            {
                OperationResult result = ResolverEnvioIndividual(envio.EnvioDocumento.Select(ed => ed.Documento).ToArray(), new LogEnvio());
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el envio de factura");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de factura", e);
            }
        }

        public OperationResult ResolverEnvioNota(Envio envio)
        {
            try
            {
                OperationResult result = ResolverEnvioNotas(envio.EnvioDocumento.Select(ed => ed.Documento).ToArray(), new LogEnvio());
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver el envio de nota");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver el envio de notas", e);
            }
        }

        public OperationResult ResolverComunicacionBaja(EstablecimientoComercialExtendido sede, Envio envio)
        {
            try
            {
                OperationResult result = ResolverComunicacionBaja(sede, envio.EnvioDocumento.Select(ed => ed.Documento).ToArray(), new LogEnvio());
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al resolver las comunicacion de baja");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver las comunicacion de baja", e);
            }
        }

        public OperationResult ActualizarEstadoEnvioPendiente(Envio envioPendiente)
        {
            try
            {
                envioPendiente.estado = (int)EstadoEnvio.Descartado;
                return ActualizarEstadoEnvio(envioPendiente);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar el estado del envio pendiente", e);
            }
        }

        public OperationResult ActualizarEstadoEnvioRechazado(Envio envioRechazado)
        {
            try
            {
                envioRechazado.estado = (int)EstadoEnvio.DescartadoPorRechazo;
                return ActualizarEstadoEnvio(envioRechazado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar el estado del envio rechazado", e);
            }
        }

        #endregion

    }
}

using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica
    {
        #region TRANSMICION DE DOCUMENTOS 

        public string DevolverUrlEnvioSunatFacturacionElectronica()
        {
            return FacturacionElectronicaSettings.Default.EnvioSunatDesarrollo ? FacturacionElectronicaSettings.Default.URLWebServiceSunatFacturacionElectronicaDesarrollo : FacturacionElectronicaSettings.Default.URLWebServiceSunatFacturacionElectronicaProduccion;
        }

        public string DevolverUrlEnvioSunatGuiaDeRemisionElectronica()
        {
            return FacturacionElectronicaSettings.Default.EnvioSunatDesarrollo ? FacturacionElectronicaSettings.Default.URLWebServiceSunatGuiaDeRemisionElectronicaDesarrollo : FacturacionElectronicaSettings.Default.URLWebServiceSunatGuiaDeRemisionElectronicaProduccion;
        }

        public string ObtenerMetodoApi(string codigoTipoDocumento)
        {
            string metodoApi;
            switch (codigoTipoDocumento)
            {
                case "07":
                    metodoApi = "api/GenerarNotaCredito";
                    break;
                case "08":
                    metodoApi = "api/GenerarNotaDebito";
                    break;
                case "09":
                    metodoApi = "api/GenerarGuiaRemision";
                    break;
                default:
                    metodoApi = "api/GenerarFactura";
                    break;
            }
            return metodoApi;
        }

        public async Task<OperationResult> TransmitirAFacturacionElectronica(int idEmpleado, EstablecimientoComercialExtendido sede)
        {
            try
            {
                OperationResult result = null;
                var detalleMaestroComprobante = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroDocumento);
                var tiposNotasDeCredito = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                var tiposNotasDeDebito = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                var proveedores = _actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroModalidadDeTraslado);
                var motivosDeTransporte = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroMotivoDeTraslado);
                var fechaActual = _operacionLogica.FechaActual();
                //Transmision de Boletas
                TransmitirBoletasAFacturacionElectronica(detalleMaestroComprobante, idEmpleado, sede, fechaActual);
                //Transmision de Facturas
                List<Factura> ordenesDeVentasConFacturaConfirmadasEInvalidadas = Factura.Convert(_operacionLogica.ObtenerOrdenesDeVentaConFacturaConfirmadasEInvalidadas(idEmpleado, fechaActual), sede, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                TransmitirFacturasAFacturacionElectronica(ordenesDeVentasConFacturaConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);
                //Transmision de Notas de Credito de Boleta
                List<NotaDeCredito> notasDeCreditoDeBoletaConfirmadasEInvalidadas = NotaDeCredito.Convert(_operacionLogica.ObtenerOperacionesConNotaDeCreditoConfirmadasEInvalidadas(idEmpleado, fechaActual, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta), sede, tiposNotasDeCredito, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                TransmitirNotasDeCreditoAFacturacionElectronica(notasDeCreditoDeBoletaConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);
                //Transmision de Notas de Debito de Boleta
                List<NotaDeDebito> notasDeDebitoDeBoletaConfirmadasEInvalidadas = NotaDeDebito.Convert(_operacionLogica.ObtenerOperacionesConNotaDeDebitoConfirmadasEInvalidadas(idEmpleado, fechaActual, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta), sede, tiposNotasDeDebito, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                TransmitirNotasDeDebitoAFacturacionElectronica(notasDeDebitoDeBoletaConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);
                //Transmision de Notas de Credito de Factura
                List<NotaDeCredito> notasDeCreditoDeFacturaConfirmadasEInvalidadas = NotaDeCredito.Convert(_operacionLogica.ObtenerOperacionesConNotaDeCreditoConfirmadasEInvalidadas(idEmpleado, fechaActual, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura), sede, tiposNotasDeCredito, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                TransmitirNotasDeCreditoAFacturacionElectronica(notasDeCreditoDeFacturaConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);
                //Transmision de Notas de Debito de Factura
                List<NotaDeDebito> notasDeDebitoDeFacturaConfirmadasEInvalidadas = NotaDeDebito.Convert(_operacionLogica.ObtenerOperacionesConNotaDeDebitoConfirmadasEInvalidadas(idEmpleado, fechaActual, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura), sede, tiposNotasDeDebito, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                TransmitirNotasDeDebitoAFacturacionElectronica(notasDeDebitoDeFacturaConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);
                //Transmision de guia de remision
                List<GuiaDeRemision> guiasDeRemisionConfirmadasEInvalidadas = GuiaDeRemision.Convert(_operacionLogica.ObtenerGuiasDeRemisionConfirmadasEInvalidadas(idEmpleado, fechaActual), sede, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas, proveedores, modalidadesDeTransporte, motivosDeTransporte);
                TransmitirGuiaDeRemisionAFacturacionElectronica(guiasDeRemisionConfirmadasEInvalidadas, detalleMaestroComprobante, idEmpleado, fechaActual);

                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al transmitir comprobantes electrónicos ", e);
            }
        }

        public OperationResult TransmitirBoletasAFacturacionElectronica(List<Detalle_maestro> detallesMaestroComprobante, int idEmpleado, EstablecimientoComercialExtendido sede, DateTime fechaActual)
        {
            List<BoletaDeVenta> boletasDeVenta = new List<BoletaDeVenta>();
            try
            {
                OperationResult result = null;
                do
                {
                    List<Documento> listaDocumentosAIngresar = new List<Documento>();
                    List<Evento_transaccion> listaEventosAIngresar = new List<Evento_transaccion>();
                    boletasDeVenta = BoletaDeVenta.Convert(_operacionLogica.ObtenerOrdenesDeVentaConBoletaConfirmadasEInvalidadas(idEmpleado, fechaActual, FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura), sede, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                    foreach (var item in boletasDeVenta)
                    {
                        DocumentoElectronico documento = _generacionArchivosLogica.ObtenerDocumentoElectronicoBoleta(item);
                        listaDocumentosAIngresar.Add(new Documento
                        {
                            idSigescom = item.IdOrden,
                            codigoTipoComprobante = documento.TipoDocumento,
                            serieComprobante = documento.IdDocumento.Split('-')[0],
                            numeroComprobante = documento.IdDocumento.Split('-')[1],
                            fechaEmision = DateTime.Parse(documento.FechaEmision + " " + documento.HoraEmision),
                            tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == documento.TipoDocumento).nombre,
                            estado = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                            estadoSigescom = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                            Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documento)) }
                        });
                        listaEventosAIngresar.Add(new Evento_transaccion
                        {
                            id_empleado = idEmpleado,
                            id_transaccion = item.IdOrden,
                            id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                            fecha = fechaActual,
                            comentario = "Estado cuando se transmite a facturacion electrónica"
                        });
                    }
                    if (listaDocumentosAIngresar != null && listaDocumentosAIngresar.Count() > 0)
                    {
                        var resultCrearDocumentosMasivos = CrearDocumentosMasivos(listaDocumentosAIngresar);
                        Util.ManejoEnLogicaResultadoSinExito(resultCrearDocumentosMasivos, "Error al crear las boletas en E-Factura");
                        var resultCrearEventosDeTransacciones = _operacionLogica.CrearEventosDeTransacciones(listaEventosAIngresar);
                        Util.ManejoEnLogicaResultadoSinExito(resultCrearEventosDeTransacciones, "Error al actualizar los estados");
                    }
                } while (boletasDeVenta.Count == FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar transmitir boletas de venta a facturación electrónica", e);
            }
        }

        public OperationResult TransmitirFacturasAFacturacionElectronica(List<Factura> facturas, List<Detalle_maestro> detallesMaestroComprobante, int idEmpleado, DateTime fechaActual)
        {
            try
            {
                OperationResult result = null;
                List<Documento> listaDocumentosAIngresar = new List<Documento>();
                List<Evento_transaccion> listaEventosAIngresar = new List<Evento_transaccion>();
                var rondas = Math.Ceiling((double)facturas.Count / FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                for (int rondaActual = 0; rondaActual < rondas; rondaActual++)
                {
                    var subList = facturas.Skip(rondaActual * FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura).Take(FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                    foreach (var item in subList)
                    {
                        DocumentoElectronico documento = _generacionArchivosLogica.ObtenerDocumentoElectronicoFactura(item);
                        listaDocumentosAIngresar.Add(new Documento
                        {
                            idSigescom = item.IdOrden,
                            codigoTipoComprobante = documento.TipoDocumento,
                            serieComprobante = documento.IdDocumento.Split('-')[0],
                            numeroComprobante = documento.IdDocumento.Split('-')[1],
                            fechaEmision = DateTime.Parse(documento.FechaEmision + " " + documento.HoraEmision),
                            tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == documento.TipoDocumento).nombre,
                            estado = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                            estadoSigescom = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                            Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documento)) }
                        });
                        listaEventosAIngresar.Add(new Evento_transaccion
                        {
                            id_empleado = idEmpleado,
                            id_transaccion = item.IdOrden,
                            id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                            fecha = fechaActual,
                            comentario = "Estado cuando se transmite a facturacion electronica"
                        });
                    }
                }
                if (facturas != null && facturas.Count() > 0)
                {
                    var resultCrearDocumentosMasivos = CrearDocumentosMasivos(listaDocumentosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearDocumentosMasivos, "Error al crear las facturas en E-Factura");
                    var resultCrearEventosDeTransacciones = _operacionLogica.CrearEventosDeTransacciones(listaEventosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEventosDeTransacciones, "Error al actualizar los estados");
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar transmitir facturas a facturación electrónica", e);
            }
        }

        public OperationResult TransmitirNotasDeCreditoAFacturacionElectronica(List<NotaDeCredito> notasDeCredito, List<Detalle_maestro> detallesMaestroComprobante, int idEmpleado, DateTime fechaActual)
        {
            try
            {
                OperationResult result = null;
                List<Documento> listaDocumentosAIngresar = null;
                List<Evento_transaccion> listaEventosAIngresar = null;
                var rondas = Math.Ceiling((double)notasDeCredito.Count / FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                for (int rondaActual = 0; rondaActual < rondas; rondaActual++)
                {
                    listaDocumentosAIngresar = new List<Documento>();
                    listaEventosAIngresar = new List<Evento_transaccion>();
                    var subList = notasDeCredito.Skip(rondaActual * FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura).Take(FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                    foreach (var item in subList)
                    {
                        DocumentoElectronico documento = _generacionArchivosLogica.ObtenerDocumentoElectronicoNotaCredito(item);
                        listaDocumentosAIngresar.Add(new Documento
                        {
                            idSigescom = item.IdOrden,
                            codigoTipoComprobante = documento.TipoDocumento,
                            serieComprobante = documento.IdDocumento.Split('-')[0],
                            numeroComprobante = documento.IdDocumento.Split('-')[1],
                            fechaEmision = DateTime.Parse(documento.FechaEmision + " " + documento.HoraEmision),
                            tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == documento.TipoDocumento).nombre,
                            estado = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                            estadoSigescom = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                            Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documento)) }
                        });
                        listaEventosAIngresar.Add(new Evento_transaccion
                        {
                            id_empleado = idEmpleado,
                            id_transaccion = item.IdOrden,
                            id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                            fecha = fechaActual,
                            comentario = "Estado cuando se transmite a facturacion electronica"
                        });
                    }
                    var resultCrearDocumentosMasivos = CrearDocumentosMasivos(listaDocumentosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearDocumentosMasivos, "Error al crear las notas de credito en E-Factura");
                    var resultCrearEventosDeTransacciones = _operacionLogica.CrearEventosDeTransacciones(listaEventosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEventosDeTransacciones, "Error al actualizar los estados");
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar transmitir notas de credito a facturación electrónica", e);
            }
        }

        public OperationResult TransmitirNotasDeDebitoAFacturacionElectronica(List<NotaDeDebito> notasDeDebito, List<Detalle_maestro> detallesMaestroComprobante, int idEmpleado, DateTime fechaActual)
        {
            try
            {
                OperationResult result = null;
                List<Documento> listaDocumentosAIngresar = new List<Documento>();
                List<Evento_transaccion> listaEventosAIngresar = new List<Evento_transaccion>();
                var rondas = Math.Ceiling((double)notasDeDebito.Count / FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                for (int rondaActual = 0; rondaActual < rondas; rondaActual++)
                {
                    var subList = notasDeDebito.Skip(rondaActual * FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura).Take(FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                    foreach (var item in subList)
                    {
                        DocumentoElectronico documento = _generacionArchivosLogica.ObtenerDocumentoElectronicoNotaDebito(item);
                        listaDocumentosAIngresar.Add(new Documento
                        {
                            idSigescom = item.IdOrden,
                            codigoTipoComprobante = documento.TipoDocumento,
                            serieComprobante = documento.IdDocumento.Split('-')[0],
                            numeroComprobante = documento.IdDocumento.Split('-')[1],
                            fechaEmision = DateTime.Parse(documento.FechaEmision + " " + documento.HoraEmision),
                            tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == documento.TipoDocumento).nombre,
                            estado = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                            estadoSigescom = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                            Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documento)) }
                        });
                        listaEventosAIngresar.Add(new Evento_transaccion
                        {
                            id_empleado = idEmpleado,
                            id_transaccion = item.IdOrden,
                            id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                            fecha = fechaActual,
                            comentario = "Estado cuando se transmite a facturacion electronica"
                        });
                    }
                }
                if (notasDeDebito != null && notasDeDebito.Count() > 0)
                {
                    var resultCrearDocumentosMasivos = CrearDocumentosMasivos(listaDocumentosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearDocumentosMasivos, "Error al crear las notas de debito en E-Factura");
                    var resultCrearEventosDeTransacciones = _operacionLogica.CrearEventosDeTransacciones(listaEventosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEventosDeTransacciones, "Error al actualizar los estados");
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar transmitir notas de debito a facturación electrónica", e);
            }
        }

        public OperationResult TransmitirGuiaDeRemisionAFacturacionElectronica(List<GuiaDeRemision> guiasDeRemision, List<Detalle_maestro> detallesMaestroComprobante, int idEmpleado, DateTime fechaActual)
        {
            try
            {
                OperationResult result = null;
                List<Documento> listaDocumentosAIngresar = new List<Documento>();
                List<Evento_transaccion> listaEventosAIngresar = new List<Evento_transaccion>();
                var rondas = Math.Ceiling((double)guiasDeRemision.Count / FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                for (int rondaActual = 0; rondaActual < rondas; rondaActual++)
                {
                    var subList = guiasDeRemision.Skip(rondaActual * FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura).Take(FacturacionElectronicaSettings.Default.TamañoLoteTransferenciaDocumentosEFactura);
                    foreach (var item in subList)
                    {
                        GuiaRemision documento = _generacionArchivosLogica.ObtenerDocumentoElectronicoGuiaDeRemision(item);
                        listaDocumentosAIngresar.Add(new Documento
                        {
                            idSigescom = item.IdOrden,
                            codigoTipoComprobante = documento.TipoDocumento,
                            serieComprobante = documento.IdDocumento.Split('-')[0],
                            numeroComprobante = documento.IdDocumento.Split('-')[1],
                            fechaEmision = DateTime.Parse(documento.FechaEmision),
                            tipoComprobante = detallesMaestroComprobante.Single(d => d.codigo == documento.TipoDocumento).nombre,
                            estado = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoDocumentoElectronico.Adicionado : (int)EstadoDocumentoElectronico.Anulado,
                            estadoSigescom = item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? (int)EstadoSigescomDocumentoElectronico.Confirmado : (int)EstadoSigescomDocumentoElectronico.Invalidado,
                            Binario = new Binario { archivoBinario = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documento)) }
                        });
                        listaEventosAIngresar.Add(new Evento_transaccion
                        {
                            id_empleado = idEmpleado,
                            id_transaccion = item.IdOrden,
                            id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
                            fecha = fechaActual,
                            comentario = "Estado cuando se transmite a facturacion electronica"
                        });
                    }
                }
                if (guiasDeRemision != null && guiasDeRemision.Count() > 0)
                {
                    var resultCrearDocumentosMasivos = CrearDocumentosMasivos(listaDocumentosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearDocumentosMasivos, "Error al crear las guia de remision en E-Factura");
                    var resultCrearEventosDeTransacciones = _operacionLogica.CrearEventosDeTransacciones(listaEventosAIngresar);
                    Util.ManejoEnLogicaResultadoSinExito(resultCrearEventosDeTransacciones, "Error al actualizar los estados");
                }
                result = new OperationResult(OperationResultEnum.Success, "TODO SE REALIZO CON EXITO", "OK");
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar transmitir guias de remision a facturación electrónica", e);
            }
        }

        #endregion
    }
}

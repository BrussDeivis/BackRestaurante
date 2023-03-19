using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        public OperationResult DescuentoGlobalOperacionVenta(long idOrdenVenta, decimal importe, string observacion, int idEventoReferencia, UserProfileSessionData sesionUsuario)
        {
            try
            {
                Comprobante comprobante = transaccionRepositorio.ObtenerComprobanteDeTransaccion(idOrdenVenta);
                var idTipoComprobante = comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna ? MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna : MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                var prefijo = comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta ? FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoBoleta : (comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura ? FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoFactura : String.Empty);
                var serie = comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna ?
                    transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(idTipoComprobante, sesionUsuario.IdCentroDeAtencionSeleccionado) :
                    transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie(idTipoComprobante, sesionUsuario.IdCentroDeAtencionSeleccionado, prefijo);
                return GuardarNotaDeCreditoDeVenta(idOrdenVenta, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal, observacion, idTipoComprobante, serie.id, true, String.Empty, 0, importe.ToString(), idEventoReferencia, null, sesionUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la anulacion de la operacion de venta", e);
            }
        }
        public OperationResult AnularOperacionVenta(long idOrdenVenta, bool darDeBaja, string observacion, int idEventoReferencia, UserProfileSessionData sesionUsuario)
        {
            try
            {
                OperationResult resultado;
                Comprobante comprobante = transaccionRepositorio.ObtenerComprobanteDeTransaccion(idOrdenVenta);
                var idTipoComprobante = darDeBaja ? MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna : MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                var prefijo = (comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta ? FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoBoleta : (comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura ? FacturacionElectronicaSettings.Default.PrefijoSerieNotaCreditoNotaDebitoFactura : String.Empty));
                var serie = darDeBaja ? transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(idTipoComprobante, sesionUsuario.IdCentroDeAtencionSeleccionado) : transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie(idTipoComprobante, sesionUsuario.IdCentroDeAtencionSeleccionado, prefijo);
                if (darDeBaja)
                {
                    var operacionNotaCredito = GenerarNotaCreditoVenta(idOrdenVenta, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion, observacion, idTipoComprobante, serie.id, true, String.Empty, 0, String.Empty, idEventoReferencia, null, sesionUsuario);
                    resultado = ResolverInvalidarOperacionVenta(operacionNotaCredito, idOrdenVenta, observacion, sesionUsuario);
                }
                else
                {
                    resultado = GuardarNotaDeCreditoDeVenta(idOrdenVenta, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion, observacion, idTipoComprobante, serie.id, true, String.Empty, 0, String.Empty, idEventoReferencia, null, sesionUsuario);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la anulacion de la operacion de venta", e);
            }
        }

        public OperacionModificatoria GenerarNotaCreditoVenta(long idOrdenVenta, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, int idEventoReferencia, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                var fechaActual = DateTimeUtil.FechaActual();
                var ordenVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenVenta));
                var venta = new Venta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenVenta.IdVenta));
                //Calcular los detalles de la nota de acuerdo el tipo de nota que se emita
                List<Detalle_transaccion> detallesNota = CalcularDetalleNotaDebitoCredito(detalles, ordenVenta.DetalleTransaccion(), idTipoNota, valorDeNota, motivo, ordenVenta.Igv() > 0);
                var importeTotal = detallesNota.Sum(d => d.total) + ordenVenta.Icbper();
                ValidarNotaCreditoEnVenta(ordenVenta, importeTotal);
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                //Obtener el tipo de transaccion de movimiento de mercaderia
                int idTipoTransaccionMovimientoDeMercaderia = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(l => l.Key == Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value).Value;
                //Validar la accion de acuerdo a los permisos del empleado
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, idUnidadNegocio);
                Transaccion notaCredito = GenerarNotaDeCreditoDebito(sesionUsuario.Empleado.Id, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaActual, "NC", Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value, importeTotal, motivo, ordenVenta.Cliente().Id, sesionUsuario.IdCentroDeAtencionSeleccionado, sesionUsuario);
                ModoPago modoPago = ordenVenta.ModoDePago();
                Transaccion ordenNotaCredito = GenerarOrdenNotaDeCreditoDebito(notaCredito, sesionUsuario.Empleado.Id, idUnidadNegocio, idTipoNota, fechaActual, ((int)modoPago).ToString(), "NC", Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, motivo, ordenVenta.Cliente().Id, ordenVenta.AliasCliente(), sesionUsuario.IdCentroDeAtencionSeleccionado, detallesNota, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto cuando se crea la nota de credito", true, idTipoTransaccionMovimientoDeMercaderia != 0);
                if (idEventoReferencia != 0) ordenNotaCredito.id_evento_referencia = idEventoReferencia;
                ResolverIcbperNotaCredito(ordenVenta, ordenNotaCredito);
                ordenNotaCredito.id_transaccion_referencia = ordenVenta.Id;
                notaCredito.Transaccion1.Add(ordenNotaCredito);
                Transaccion pagoNota = DevolverPagoNotaCredito(notaCredito, ordenNotaCredito, fechaActual, idTipoNota, modoPago, importeTotal, sesionUsuario);
                Transaccion salidaMercaderiaNota = DevolverSalidaMercaderiaNotaCredito(venta, ordenVenta, notaCredito, ordenNotaCredito, fechaActual, idTipoTransaccionMovimientoDeMercaderia, detallesNota, sesionUsuario);
                var operacionModificatoria = new OperacionModificatoria
                {
                    Operacion = notaCredito,
                    OrdenDeOperacion = ordenNotaCredito,
                    MovimientoEconomico = pagoNota,
                    MovimientosBienes = salidaMercaderiaNota == null ? null : new List<Transaccion>() { salidaMercaderiaNota }
                };
                return operacionModificatoria;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la generacion de la nota de credito de venta", e);
            }
        }
        public void ResolverIcbperNotaCredito(OrdenDeVenta ordenDeVenta, Transaccion ordenDeNotaDeCredito)
        {
            if (ordenDeVenta.Icbper() > 0)
            {
                ordenDeNotaDeCredito.importe10 = ordenDeVenta.Icbper();
                ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, ordenDeVenta.Icbper().ToString()));
                ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, ordenDeVenta.NumeroBolsasDePlastico().ToString()));
            }
        }
        public Transaccion DevolverPagoNotaCredito(Transaccion notaCredito, Transaccion ordenNotaCredito, DateTime fechaActual, int idTipoNota, ModoPago modoPago, decimal importeTotal, UserProfileSessionData sesionUsuario)
        {
            Transaccion pago = null;
            if (idTipoNota != MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion)
            {
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, importeTotal, "Unica cuota generada de forma automática", false);
                ordenNotaCredito.Cuota.Add(cuota);
                if (modoPago == ModoPago.Contado)
                {
                    ValidarImporteAPagar(1, cuota.total, ordenNotaCredito.importe_total);
                    pago = GenerarPagoPorNotaCreditoODebito(ordenNotaCredito, CodigoPago(cuota), cuota.total, sesionUsuario.Empleado.Id, fechaActual, "", sesionUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al cobrar una venta");
                    cuota.SetPagoACuenta(ordenNotaCredito.importe_total);
                    VincularPagoConLaCuota(pago, cuota, ordenNotaCredito.importe_total);
                    notaCredito.Transaccion1.Add(pago);
                }
            }
            return pago;
        }
        public Transaccion DevolverSalidaMercaderiaNotaCredito(Venta venta, OrdenDeVenta ordenVenta, Transaccion notaCredito, Transaccion ordenDeNotaDeCredito, DateTime fechaActual, int idTipoTransaccionMovimientoDeMercaderia, List<Detalle_transaccion> detallesNota, UserProfileSessionData sesionUsuario)
        {
            Transaccion salidaMercaderiaPorVenta = null;
            var salidasMercaderiaVenta = venta.ObtenerSalidasDeMercaderia();
            if (salidasMercaderiaVenta.Count > 0 && idTipoTransaccionMovimientoDeMercaderia != 0)
            {
                salidaMercaderiaPorVenta = GenerarMovimientoDeMercaderia(ordenDeNotaDeCredito, sesionUsuario.Empleado.Id, sesionUsuario.IdCentroAtencionQueTieneElStockIntegrada, ordenVenta.IdCliente, idTipoTransaccionMovimientoDeMercaderia, fechaActual, "", detallesNota, sesionUsuario, ordenVenta.OperacionDeAlmacen().Id);
                salidaMercaderiaPorVenta.Transaccion3 = ordenDeNotaDeCredito;
                notaCredito.Transaccion1.Add(salidaMercaderiaPorVenta);
            }
            return salidaMercaderiaPorVenta;
        }
        public OperationResult GuardarNotaDeCreditoDeVenta(long idOrdenVenta, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, int idEventoReferencia, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtenemos la fecha actual como la fecha de emision
                DateTime fechaEmision = DateTimeUtil.FechaActual();
                //Obtenemos la orden de venta referencia de la nota de credito
                OrdenDeVenta ordenDeVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenVenta));
                //Obtenemos la venta referencia de la nota de credito
                Venta venta = new Venta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenDeVenta.IdVenta));
                //Calculamos los valores de detalles
                List<Detalle_transaccion> detallesDeNota = CalcularDetalleNotaDebitoCredito(detalles, ordenDeVenta.DetalleTransaccion(), idTipoNota, valorDeNota, motivo, ordenDeVenta.Igv() > 0);
                //Calculamos el importe total
                var importeTotal = detallesDeNota.Sum(d => d.total) + ordenDeVenta.Icbper();
                //Validar que puede realizarse la nota de credito sobre este comprobante 
                ValidarNotaCreditoEnVenta(ordenDeVenta, importeTotal);
                //Obtenemos la unidad de negocio 
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                ////Obtenemos la fecha actual
                //DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtenemos el tipo de transaccion de movimiento de mercaderia
                int idTipoTransaccionMovimientoDeMercaderia = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(l => l.Key == Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value).Value;
                //Validamos la accion a realizar
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, idUnidadNegocio);

                Transaccion notaDeCredito = GenerarNotaDeCreditoDebito(sesionUsuario.Empleado.Id, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaEmision, "NC", Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value, importeTotal, motivo, ordenDeVenta.Cliente().Id, sesionUsuario.IdCentroDeAtencionSeleccionado, sesionUsuario);

                //Decidir que modo de pago tendra la nota de credito
                ModoPago modoPago = ordenDeVenta.ModoDePago();

                //Generamos la orden de la nota de debito
                Transaccion ordenDeNotaDeCredito = GenerarOrdenNotaDeCreditoDebito(notaDeCredito, sesionUsuario.Empleado.Id, idUnidadNegocio, idTipoNota, fechaEmision, ((int)modoPago).ToString(), "NC", Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, motivo, ordenDeVenta.Cliente().Id, ordenDeVenta.AliasCliente(), sesionUsuario.IdCentroDeAtencionSeleccionado, detallesDeNota, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto cuando se crea la nota de credito", true, idTipoTransaccionMovimientoDeMercaderia != 0);
                //Si hay id eventro referencia ponerlo
                if (idEventoReferencia != 0) ordenDeNotaDeCredito.id_evento_referencia = idEventoReferencia;
                //Agregamos a la orden el parametro de icbper, si es que tiene icbper 
                if (ordenDeVenta.Icbper() > 0)
                {
                    ordenDeNotaDeCredito.importe10 = ordenDeVenta.Icbper();
                    ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, ordenDeVenta.Icbper().ToString()));
                    ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, ordenDeVenta.NumeroBolsasDePlastico().ToString()));
                }
                //Agregamos el id de la orden de venta como referencia de la orden nota de debito
                ordenDeNotaDeCredito.id_transaccion_referencia = ordenDeVenta.Id;
                //Agregamos la orden de venta en la venta
                notaDeCredito.Transaccion1.Add(ordenDeNotaDeCredito);

                if (idTipoNota != MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion)
                {
                    //Creamos la cuota, cuenta por cobrar unica
                    Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaEmision.Year) + "_" + 1, fechaEmision, fechaEmision, importeTotal, "Unica cuota generada de forma automática", false);
                    //Agregamos la cuota en la orden
                    ordenDeNotaDeCredito.Cuota.Add(cuota);
                    if (modoPago == ModoPago.Contado)
                    {
                        //Validamos monto a pagar
                        ValidarImporteAPagar(1, cuota.total, ordenDeNotaDeCredito.importe_total);
                        //Generamos el pago de la nota de credito
                        Transaccion pago = GenerarPagoPorNotaCreditoODebito(ordenDeNotaDeCredito, CodigoPago(cuota), cuota.total, sesionUsuario.Empleado.Id, fechaEmision, "", sesionUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al cobrar una venta");
                        cuota.SetPagoACuenta(ordenDeNotaDeCredito.importe_total);
                        VincularPagoConLaCuota(pago, cuota, ordenDeNotaDeCredito.importe_total);
                        //Agregamos el pago en la operacion wrapper
                        notaDeCredito.Transaccion1.Add(pago);
                    }
                }
                Transaccion salidaMercaderiaPorVenta = null;
                var salidasMercaderiaVenta = venta.ObtenerSalidasDeMercaderia();
                //Se genera el movimiento de la mercaderia
                if (salidasMercaderiaVenta.Count > 0 && idTipoTransaccionMovimientoDeMercaderia != 0)
                {
                    //Generamos la salida de mercaderia
                    salidaMercaderiaPorVenta = GenerarMovimientoDeMercaderia(ordenDeNotaDeCredito, sesionUsuario.Empleado.Id, sesionUsuario.IdCentroAtencionQueTieneElStockIntegrada, ordenDeVenta.IdCliente, idTipoTransaccionMovimientoDeMercaderia, fechaEmision, "", detallesDeNota, sesionUsuario, ordenDeVenta.OperacionDeAlmacen().Id);
                    //Salida de mercaderia como referencia de la orden de venta
                    salidaMercaderiaPorVenta.Transaccion3 = ordenDeNotaDeCredito;
                    //agregamos la salida de la mercaderia a la venta
                    notaDeCredito.Transaccion1.Add(salidaMercaderiaPorVenta);
                }
                if (idTipoTransaccionMovimientoDeMercaderia != 0)
                {
                    return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada { Operacion = notaDeCredito, OrdenDeOperacion = ordenDeNotaDeCredito, MovimientosBienes = (salidasMercaderiaVenta.Count > 0) ? new List<Transaccion>() { salidaMercaderiaPorVenta } : null }, sesionUsuario);
                }
                else
                {
                    var result = transaccionRepositorio.CrearTransaccion(notaDeCredito);
                    result.information = new Operacion(ordenDeNotaDeCredito).Id;
                    result.objeto = new OrdenDeVenta(ordenDeNotaDeCredito);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la confirmacion de la nota de credito de venta", e);
            }
        }


        public OperationResult GuardarNotaVenta(long idOrdenVenta, int idTipoNota, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerie, int numeroComprobante, decimal montoNota, int idEventoReferencia, List<DetalleOrdenDeNota> detalles, bool esDebito, bool esDiferida, string observacion, DatosPago datosPago, UserProfileSessionData sesionUsuario)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                List<Cuota> cuotasModificadas = new List<Cuota>();
                List<Estado_transaccion> estadosTransaccionNuevos = new List<Estado_transaccion>();
                List<Detalle_transaccion> detallesTransaccionModificados = new List<Detalle_transaccion>();
                var ordenVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenVenta));
                var venta = new Venta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenVenta.IdVenta));
                var operacionNota = new OperacionNota(venta, ordenVenta, fechaActual, idTipoNota, idTipoComprobante, idSerieComprobante, esPropio, numeroSerie, numeroComprobante, montoNota, detalles, esDebito, esDiferida, observacion, datosPago, sesionUsuario);

                RecalcularDetalleNotaDebitoCredito_(operacionNota, estadosTransaccionNuevos, cuotasModificadas, detallesTransaccionModificados);
                if (!esDebito) ValidarNotaCreditoEnVenta(operacionNota.OrdenVenta, operacionNota.ImporteTotal);

                Transaccion operacion = GenerarNotaCreditoDebito_(operacionNota);
                Transaccion ordenOperacion = GenerarOrdenNotaCreditoDebito_(operacion, operacionNota);
                Transaccion pago = GenerarPagoCreditoDebito_(operacion, ordenOperacion, operacionNota);
                Transaccion movimientoAlmacen = GenerarMovimientoAlmacenCreditoDebito_(operacion, ordenOperacion, operacionNota, sesionUsuario);

                var movimientoEconomicoConPuntos = venta.ObtenerPagos().FirstOrDefault(p => p.TrazaDePago().MedioDePago().id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos);
                var transaccionesModificadas = GenerarTransaccionesAModificar_(movimientoEconomicoConPuntos);

                var operacionModificatoria = new OperacionModificatoria() { Operacion = operacion, OrdenDeOperacion = ordenOperacion, MovimientoEconomico = pago, MovimientosBienes = movimientoAlmacen != null ? new List<Transaccion>() { movimientoAlmacen } : null, NuevosEstadosTransaccionesModificadas = estadosTransaccionNuevos, CuotasModificadas = cuotasModificadas, TransaccionesModificadas = transaccionesModificadas, DetallesTransaccionModificados = detallesTransaccionModificados };
                var resultado = AfectarInventarioFisicoYGuardarOperacion(operacionModificatoria, sesionUsuario);
                resultado.objeto = new OrdenDeVenta(operacionModificatoria.OrdenDeOperacion);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la confirmacion de la nota de credito de venta", e);
            }
        }

        private Transaccion GenerarNotaCreditoDebito_(OperacionNota operacionNota)
        {
            Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
            string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacionNota.SufijoCodigo, operacionNota.IdTipoTransaccion);
            Comprobante comprobante = GenerarComprobante(operacionNota.EsPropio, operacionNota.IdSerieComprobante, operacionNota.IdTipoComprobante, operacionNota.NumeroSerie, operacionNota.NumeroComprobante);
            Transaccion nota = new Transaccion(codigo, operacionGenerica.Id, operacionNota.FechaActual, operacionNota.IdTipoTransaccion, operacionNota.IdUnidadDeNegocio, true, operacionNota.FechaActual, operacionNota.FechaActual, operacionNota.ObservacionOperacion, operacionNota.FechaActual, operacionNota.IdEmpleado, operacionNota.ImporteTotal, operacionNota.IdCentroAtencion, operacionNota.IdMoneda, operacionNota.TipoDeCambio, null, operacionNota.IdCliente)
            {
                Comprobante = comprobante
            };
            return nota;
        }

        private Transaccion GenerarOrdenNotaCreditoDebito_(Transaccion operacion, OperacionNota operacionNota)
        {
            Transaccion ordenDeNota = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_O" + operacionNota.SufijoCodigo, operacionNota.IdTipoTransaccionOrden), null, operacionNota.FechaActual, operacionNota.IdTipoTransaccionOrden, operacionNota.IdUnidadDeNegocio, true, operacionNota.FechaActual, operacionNota.FechaActual, operacionNota.ObservacionOperacion, operacionNota.FechaActual, operacionNota.IdEmpleado, operacion.importe_total, operacionNota.IdCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, operacionNota.IdCliente, operacionNota.DescuentoGlobal, operacionNota.DescuentoPorItem, operacionNota.Anticipo, operacionNota.Gravada, operacionNota.Exonerada, operacionNota.Inafecta, operacionNota.Gratuita, operacionNota.Igv, operacionNota.Isc, operacionNota.Icbper, operacionNota.OtrosCargos, operacionNota.OtrosTributos)
            {
                Comprobante = operacion.Comprobante,
                id_transaccion_referencia = operacionNota.OrdenVenta.Id,
                id_actor_negocio_externo1 = operacionNota.OrdenVenta.IdGrupoCliente,
            };
            ordenDeNota.AgregarDetalles(Detalle_transaccion.Clone(operacionNota.DetallesOperacion));
            if (operacionNota.Icbper > 0)
            {
                ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, operacionNota.Icbper.ToString()));
            }
            if (operacionNota.NumeroBolsasPlastico > 0)
            {
                ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, operacionNota.NumeroBolsasPlastico.ToString()));
            }
            if (!string.IsNullOrEmpty(operacionNota.AliasCliente))
            {
                ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, operacionNota.AliasCliente));
            }
            ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat, maestroRepositorio.ObtenerDetalle(operacionNota.IdTipoNota).codigo));
            ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)operacionNota.PagoOperacion.ModoDePago).ToString()));
            ordenDeNota.Estado_transaccion.Add(new Estado_transaccion(operacionNota.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, operacionNota.FechaActual, "Estado asignado por defecto al emitir una nota."));
            //Informacion de la orden de almacen de la nota de credito
            ordenDeNota.enum1 = operacionNota.IndicadorImpactoAlmacenNota;
            if (ordenDeNota.enum1 == (int)IndicadorImpactoAlmacen.Inmediata || ordenDeNota.enum1 == (int)IndicadorImpactoAlmacen.Diferida)
            {
                ordenDeNota.id_actor_negocio_interno1 = operacionNota.IdAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(operacionNota.IdEmpleado, operacionNota.EsDiferidaOperacion ? MaestroSettings.Default.IdDetalleMaestroEstadoPendiente : MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionNota.FechaActual, "Estado asignado al registrar una nota de credito");
                ordenDeNota.Estado_transaccion.Add(estadoOrdenAlmacen);
            }
            operacion.Transaccion1.Add(ordenDeNota);
            return ordenDeNota;
        }
        private Transaccion GenerarPagoCreditoDebito_(Transaccion operacion, Transaccion ordenOperacion, OperacionNota operacionNota)
        {
            Transaccion pago = null;
            if (operacionNota.ImportePagoTotal > 0)
            {
                bool porCobrar = operacionNota.EsDebito;
                if (operacionNota.PagoOperacion.ModoDePago == ModoPago.CreditoConfigurado)
                {
                    int cont = 1;
                    foreach (var item in operacionNota.PagoOperacion.CuotasDeVenta())
                    {
                        var cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(porCobrar, operacionNota.FechaActual.Year) + "_" + cont++, operacionNota.FechaActual, item.fecha_vencimiento, item.capital, item.interes, item.total, "Cuota generada numero " + cont, porCobrar, item.cuota_inicial);
                        ordenOperacion.Cuota.Add(cuota);
                    }
                    var diferencia = operacionNota.ImportePagoTotal - ordenOperacion.Cuota.Sum(c => c.total);
                    ordenOperacion.Cuota.Last().total = ordenOperacion.Cuota.Last().capital = ordenOperacion.Cuota.Last().total + diferencia;
                }
                else
                {
                    var cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(porCobrar, operacionNota.FechaActual.Year) + "_" + 1, operacionNota.FechaActual, operacionNota.FechaActual, operacionNota.ImportePagoTotal, "Unica cuota generada de forma automática al emitir la nota interna", porCobrar);
                    ordenOperacion.Cuota.Add(cuota);
                }
                //Generamos el movimiento economico de la invalidacion de la operacion 
                if (operacionNota.PagoOperacion.HayIngresoDinero)
                {
                    var esCreditoRapidoConPagoInicial = (operacionNota.PagoOperacion.ModoDePago == ModoPago.CreditoRapido && operacionNota.PagoOperacion.Inicial > 0);
                    //Obtener la cuota de la que se realizara el pago, si es al contado 
                    Cuota cuotaACobrar = (operacionNota.PagoOperacion.ModoDePago == ModoPago.Contado || esCreditoRapidoConPagoInicial) ? ordenOperacion.Cuota.First() : ordenOperacion.Cuota.Single(c => c.cuota_inicial);
                    cuotaACobrar.SetPagoACuenta(esCreditoRapidoConPagoInicial ? operacionNota.PagoOperacion.Inicial : cuotaACobrar.total);
                    ValidarImporteAPagar(1, cuotaACobrar.total, ordenOperacion.importe_total);
                    pago = GenerarMovimientoEconomicoPagoACuentaCuota(operacion, cuotaACobrar, operacionNota.IdEmpleado, operacionNota.IdCaja, operacionNota.IdCliente, operacionNota.IdTipoTransaccionPago, operacionNota.FechaActual, operacionNota.FechaActual, operacionNota.ObservacionOperacion, operacionNota.PagoOperacion.Traza.MedioDePago.Id, operacionNota.PagoOperacion.Traza.Info.EntidadFinanciera.Id, operacionNota.PagoOperacion.Traza.Info.Observacion);
                    //Validar los medios de pago y actualizar transaccion de ingreso de dinero
                    if (operacionNota.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
                    {
                        pago.cantidad1 = operacionNota.PagoOperacion.Traza.Info.PuntosCajeados;
                    }
                    if (operacionNota.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta || operacionNota.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos)
                    {
                        pago.id_actor_negocio_interno1 = operacionNota.PagoOperacion.Traza.Info.CuentaBancaria.Id;
                    }
                    if (!string.IsNullOrEmpty(operacionNota.PagoOperacion.Traza.Info.InformacionJson)) pago.Traza_pago.First().extension_json = operacionNota.PagoOperacion.Traza.Info.InformacionJson;
                    operacion.Transaccion1.Add(pago);
                }
            }
            return pago;
        }

        public Transaccion GenerarMovimientoAlmacenCreditoDebito_(Transaccion operacion, Transaccion ordenOperacion, OperacionNota operacionNota, UserProfileSessionData sesionUsuario)
        {
            Transaccion entradaMercaderiaPorInvalidacionVenta = null;
            if (operacionNota.HayMovimientoAlmacenNota)
            {
                var salidasMercaderiaVenta = operacionNota.Venta.ObtenerSalidasDeMercaderia();
                if (salidasMercaderiaVenta.Count > 0)
                {
                    //Creacion de la salida de mercaderia de la invalidacion de la invalidacion
                    entradaMercaderiaPorInvalidacionVenta = GenerarMovimientoDeMercaderia(operacion, operacionNota.IdEmpleado, operacionNota.IdAlmacen, operacionNota.IdCliente, operacionNota.IdTipoTransaccionAlmacen, operacionNota.FechaActual, operacionNota.ObservacionOperacion, operacionNota.DetallesMovimientoAlmacenOperacion, sesionUsuario, salidasMercaderiaVenta.First().Id);
                    //Agregar como referencia de la entrada de mercaderia a la orden de invalidacion de la venta
                    entradaMercaderiaPorInvalidacionVenta.Transaccion3 = ordenOperacion;
                    //agregamos el trasldo interno a la orden de traslado 
                    operacion.Transaccion1.Add(entradaMercaderiaPorInvalidacionVenta);
                }
                if (operacionNota.NuevoComprobanteParaMovimientoAlmacen)
                {
                    Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna, sesionUsuario.IdCentroDeAtencionSeleccionado);
                    entradaMercaderiaPorInvalidacionVenta.Comprobante = GenerarComprobantePropioAutonumerable(serie.id);
                }
            }
            return entradaMercaderiaPorInvalidacionVenta;
        }
        public void RecalcularDetalleNotaDebitoCredito_(OperacionNota operacionNota, List<Estado_transaccion> estadosTransaccionNuevos, List<Cuota> cuotasModificadas, List<Detalle_transaccion> detallesTransaccionModificados)
        {
            if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion || operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal)
            {
                operacionNota.HayMovimientoAlmacenNota = operacionNota.HayMovimientoAlmacenOperacion;
                operacionNota.IndicadorImpactoAlmacenNota = operacionNota.EsOrdenOrigenPendiente ? (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial : (operacionNota.HaySeccionEntregaAlmacenOperacion ? (operacionNota.EsDiferidaOperacion ? (int)IndicadorImpactoAlmacen.Diferida : (int)IndicadorImpactoAlmacen.Inmediata) : (int)operacionNota.OrdenVenta.IndicadorImpactoAlmacen);
                if (operacionNota.EsOrdenOrigenCompletada)
                {
                    operacionNota.DetallesMovimientoAlmacenOperacion = operacionNota.DetallesBienesOperacion;
                }
                else if (operacionNota.EsOrdenOrigenParcial)
                {
                    var detallesOperacionOrigen = operacionNota.DetallesOperacion;
                    var ordenAlmacen = ObtenerOrdenAlmacen(operacionNota.OrdenVenta.Id);
                    //Calculamos los detalles a revocar de la operacion origen
                    operacionNota.DetallesOperacion.ForEach(di => di.cantidad_1 = ordenAlmacen.Detalles.Where(d => d.IdConcepto == di.id_concepto_negocio).Sum(d => d.Pendiente));
                    detallesOperacionOrigen.ForEach(di => di.cantidad_1 = ordenAlmacen.Detalles.Where(d => d.IdConcepto == di.id_concepto_negocio).Sum(d => d.Pendiente));
                    estadosTransaccionNuevos.Add(new Estado_transaccion(operacionNota.OrdenVenta.Id, operacionNota.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionNota.FechaActual, "Estado que se agregado al completar la orden de almacen"));
                    //Generamos los detalles de la orden original para actualizar
                    foreach (var detalleOperacionOrigen in detallesOperacionOrigen)
                    {
                        var detalle = transaccionRepositorio.ObtenerDetalleTransaccion(detalleOperacionOrigen.id);
                        detalle.cantidad_1 = detalleOperacionOrigen.cantidad_1;
                        detallesTransaccionModificados.Add(detalle);
                    }
                    //Detalles de movimiento de almacen de la nota 
                    operacionNota.DetallesMovimientoAlmacenOperacion = new List<Detalle_transaccion>();
                    var detallesMovimientoAlmacen = ordenAlmacen.Detalles.Where(d => d.Entregado > 0).ToList();
                    foreach (var detalle in detallesMovimientoAlmacen)
                    {
                        var detalleAlmacen = operacionNota.DetallesBienesOperacion.First(d => d.id_concepto_negocio == detalle.IdConcepto);
                        detalleAlmacen.cantidad = detalle.Entregado;
                        operacionNota.DetallesMovimientoAlmacenOperacion.Add(detalleAlmacen);
                    }
                }
                else if (operacionNota.EsOrdenOrigenPendiente)
                {
                    var detallesOperacionOrigen = operacionNota.DetallesOperacion;
                    detallesOperacionOrigen.ForEach(di => di.cantidad_1 = di.cantidad);
                    estadosTransaccionNuevos.Add(new Estado_transaccion(operacionNota.OrdenVenta.Id, operacionNota.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionNota.FechaActual, "Estado que se agregado al completar la orden de almacen"));
                    //Generamos los detalles de la orden original para actualizar
                    foreach (var detalleOperacionOrigen in detallesOperacionOrigen)
                    {
                        var detalle = transaccionRepositorio.ObtenerDetalleTransaccion(detalleOperacionOrigen.id);
                        detalle.cantidad_1 = detalleOperacionOrigen.cantidad_1;
                        detallesTransaccionModificados.Add(detalle);
                    }
                }
                RecalcularCuotas_(operacionNota, cuotasModificadas);
            }
            if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem)
            {
                var HayImpactoAlmacen = false;
                var idsConceptosIcbper = operacionNota.OrdenVenta.Detalles().Where(d => d.Producto.IdConceptoBasico == MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica).Select(d => d.Producto.Id).ToList();
                foreach (var detalleNota in operacionNota.DetallesNota)
                {
                    detalleNota.ImporteRevocado = detalleNota.MontoRevocado * detalleNota.PrecioUnitario;
                    detalleNota.IcbperRevocado = detalleNota.MontoRevocado * (idsConceptosIcbper.Contains(detalleNota.Producto.Id) ? operacionNota.OrdenVenta.ValorIcbper() : 0);
                    detalleNota.IgvRevocado = detalleNota.ImporteRevocado - (detalleNota.ImporteRevocado / (1 + (operacionNota.GravaIgv ? TransaccionSettings.Default.TasaIGV : 0)));
                    detalleNota.ImporteDevuelto = detalleNota.MontoDevuelto * detalleNota.PrecioUnitario;
                    detalleNota.IcbperDevuelto = detalleNota.MontoDevuelto * (idsConceptosIcbper.Contains(detalleNota.Producto.Id) ? operacionNota.OrdenVenta.ValorIcbper() : 0);
                    detalleNota.IgvDevuelto = detalleNota.ImporteDevuelto - (detalleNota.ImporteDevuelto / (1 + (operacionNota.GravaIgv ? TransaccionSettings.Default.TasaIGV : 0)));
                }
                operacionNota.Icbper = operacionNota.DetallesNota.Sum(d => d.IcbperRevocado + d.IcbperDevuelto);
                operacionNota.NumeroBolsasPlastico = operacionNota.Icbper == 0 ? 0 : (operacionNota.Icbper / operacionNota.OrdenVenta.ValorIcbper());
                operacionNota.ImporteTotal = operacionNota.DetallesNota.Sum(d => d.ImporteRevocado + d.IcbperRevocado + d.ImporteDevuelto + d.IcbperDevuelto);
                var detallesOperacion = new List<Detalle_transaccion>();
                var detallesMovimientoAlmacenOperacion = new List<Detalle_transaccion>();
                CalcularOperacionCompletada(operacionNota, estadosTransaccionNuevos);
                foreach (var detalleNota in operacionNota.DetallesNota)
                {
                    if (detalleNota.MontoRevocado > 0)
                    {
                        var detalle = transaccionRepositorio.ObtenerDetalleTransaccion(operacionNota.DetallesOperacion.Single(d => d.id_concepto_negocio == detalleNota.Producto.Id).id);
                        detalle.cantidad_1 += detalleNota.MontoRevocado;
                        detallesTransaccionModificados.Add(detalle);
                        HayImpactoAlmacen = HayImpactoAlmacen || false;
                    }
                    if ((detalleNota.MontoRevocado + detalleNota.MontoDevuelto) > 0)
                    {
                        var detalleOperacion = operacionNota.DetallesOperacion.First(d => d.id_concepto_negocio == detalleNota.Producto.Id);
                        detalleOperacion.cantidad = detalleNota.MontoRevocado + detalleNota.MontoDevuelto;
                        detalleOperacion.cantidad_1 = detalleNota.MontoRevocado;
                        detalleOperacion.total = detalleOperacion.cantidad * detalleOperacion.precio_unitario;
                        detallesOperacion.Add(detalleOperacion);
                        var detalleMovimientoOperacion = operacionNota.DetallesBienesOperacion.First(d => d.id_concepto_negocio == detalleNota.Producto.Id);
                        detalleMovimientoOperacion.cantidad = detalleNota.MontoDevuelto;
                        detalleMovimientoOperacion.cantidad_1 = 0;
                        detalleMovimientoOperacion.total = detalleMovimientoOperacion.cantidad * detalleMovimientoOperacion.precio_unitario;
                        detallesMovimientoAlmacenOperacion.Add(detalleMovimientoOperacion);
                    }
                    if (detalleNota.MontoDevuelto > 0)
                    {
                        operacionNota.HayMovimientoAlmacenNota = (!VentasSettings.Default.MostrarSeccionEntregaEnVenta) || (VentasSettings.Default.MostrarSeccionEntregaEnVenta && !operacionNota.EsDiferidaOperacion);
                        operacionNota.IndicadorImpactoAlmacenNota = operacionNota.EsDiferidaOperacion ? (int)IndicadorImpactoAlmacen.Diferida : (int)IndicadorImpactoAlmacen.Inmediata;
                        HayImpactoAlmacen = HayImpactoAlmacen || true;
                    }
                }
                operacionNota.DetallesOperacion = detallesOperacion;
                operacionNota.DetallesMovimientoAlmacenOperacion = detallesMovimientoAlmacenOperacion;
                RecalcularCuotas_(operacionNota, cuotasModificadas);
                if (!HayImpactoAlmacen)
                    operacionNota.IndicadorImpactoAlmacenNota = (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial;
            }
            else if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal)
            {
                operacionNota.DetallesOperacion = new List<Detalle_transaccion>() { new Detalle_transaccion(1, ConceptoSettings.Default.IdConceptoNegocioDescuentoGlobal, operacionNota.ObservacionOperacion, operacionNota.MontoNota, operacionNota.MontoNota, null, 0, null, null, 0, 0, 0, null, null, null) };
                operacionNota.Icbper = 0;
                operacionNota.ImporteTotal = operacionNota.MontoNota;
                RecalcularCuotas_(operacionNota, cuotasModificadas);
            }
            else if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem)
            {
                operacionNota.DetallesOperacion = operacionNota.DetallesNota.Where(d => d.MontoDetalle != 0).Select(d => { d.PrecioUnitario = d.MontoDetalle / d.Cantidad; d.Importe = d.MontoDetalle; return d; }).Select(d => d.DetalleTransaccion()).ToList();
                operacionNota.Icbper = 0;
                operacionNota.ImporteTotal = operacionNota.DetallesNota.Sum(d => d.MontoDetalle);
                RecalcularCuotas_(operacionNota, cuotasModificadas);
            }
            else if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora)
            {
                operacionNota.DetallesOperacion = new List<Detalle_transaccion>() { new Detalle_transaccion(1, ConceptoSettings.Default.IdConceptoNegocioInteresPorMora, operacionNota.ObservacionOperacion, operacionNota.MontoNota, operacionNota.MontoNota, null, 0, null, null, 0, 0, 0, null, null, null) };
                operacionNota.Icbper = 0;
                operacionNota.ImporteTotal = operacionNota.MontoNota;
                RecalcularCuotas_(operacionNota, cuotasModificadas);
            }
            else if (operacionNota.IdTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor)
            {
                operacionNota.DetallesOperacion = operacionNota.DetallesNota.Where(d => d.MontoDetalle != 0).Select(d => { d.PrecioUnitario = d.MontoDetalle / d.Cantidad; d.Importe = d.MontoDetalle; return d; }).Select(d => d.DetalleTransaccion()).ToList();
                operacionNota.Icbper = 0;
                operacionNota.ImporteTotal = operacionNota.DetallesNota.Sum(d => d.MontoDetalle);
                RecalcularCuotas_(operacionNota, cuotasModificadas);
            }
            //Verificar si la operacion que genera la nota graba IGV
            if (operacionNota.GravaIgv)
            {
                operacionNota.DetallesOperacion.ForEach(d => d.igv = Decimal.Round(d.total - (d.total / (1 + TransaccionSettings.Default.TasaIGV)), 2));
                operacionNota.DetallesBienesOperacion.ForEach(d => d.igv = Decimal.Round(d.total - (d.total / (1 + TransaccionSettings.Default.TasaIGV)), 2));
                if (operacionNota.DetallesMovimientoAlmacenOperacion != null)
                    operacionNota.DetallesMovimientoAlmacenOperacion.ForEach(d => d.igv = Decimal.Round(d.total - (d.total / (1 + TransaccionSettings.Default.TasaIGV)), 2));
            }
            if (operacionNota.IdTipoNota != MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion || operacionNota.IdTipoNota != MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal)
            {
                operacionNota.Igv = operacionNota.DetallesOperacion.Sum(d => d.igv);
            }
        }

        public void CalcularOperacionCompletada(OperacionNota operacionNota, List<Estado_transaccion> estadosTransaccionNuevos)
        {
            var ordenAlmacen = ObtenerOrdenAlmacen(operacionNota.OrdenVenta.Id);
            var hayMontoRevocado = operacionNota.DetallesNota.Sum(d => d.MontoRevocado) > 0;
            var todoRevocado = true;
            foreach (var detalle in ordenAlmacen.Detalles)
            {
                if (detalle.Pendiente > 0)
                {
                    todoRevocado = todoRevocado && (detalle.Pendiente == operacionNota.DetallesNota.FirstOrDefault(dn => dn.Producto.Id == detalle.IdConcepto).MontoRevocado);
                }
            }
            if (hayMontoRevocado && todoRevocado)
            {
                estadosTransaccionNuevos.Add(new Estado_transaccion(operacionNota.OrdenVenta.Id, operacionNota.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionNota.FechaActual, "Estado que se agregado al completar la orden de almacen"));
            }
        }

        public void RecalcularCuotas_(OperacionNota operacionNota, List<Cuota> cuotasModificadas)
        {
            var pagoOperacion = operacionNota.ImporteTotal;
            if (!operacionNota.EsDebito)
            {
                foreach (var cuota in operacionNota.Cuotas.OrderByDescending(c => c.fecha_vencimiento))
                {
                    if (pagoOperacion > 0 && cuota.saldo > 0)
                    {
                        cuota.revocado += cuota.saldo >= pagoOperacion ? Math.Round(pagoOperacion, 2) : Math.Round(cuota.saldo, 2);
                        pagoOperacion -= Math.Round(cuota.saldo, 2);
                        cuota.saldo = Math.Round(cuota.total, 2) - Math.Round(cuota.pago_a_cuenta, 2) - Math.Round(cuota.revocado, 2);
                        cuotasModificadas.Add(cuota);
                    }
                }
            }
            operacionNota.ImportePagoTotal = Math.Round(pagoOperacion, 2);
        }

        public void ValidarNotaCreditoEnVenta(OrdenDeVenta ordenDeVenta, decimal importeTotalDeNotaDeCredito)
        {
            if (importeTotalDeNotaDeCredito > 0)
            {
                //Obtener las operaciones referencias de la orden de venta
                var transaccionesReferenciaDeLaOrdenDeVenta = ordenDeVenta.Transaccion().Transaccion11;
                if (transaccionesReferenciaDeLaOrdenDeVenta.Count > 0)
                {
                    var sumaImporteDeNotasDeCredito = transaccionesReferenciaDeLaOrdenDeVenta.Where(t => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCredito.Contains(t.id_tipo_transaccion)).Sum(t => t.importe_total);
                    var sumaImporteDeNotasDeDebito = transaccionesReferenciaDeLaOrdenDeVenta.Where(t => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeDebito.Contains(t.id_tipo_transaccion)).Sum(t => t.importe_total);
                    if (importeTotalDeNotaDeCredito > ordenDeVenta.Total + sumaImporteDeNotasDeDebito - sumaImporteDeNotasDeCredito)
                    {
                        throw new LogicaException("Error al intentar realizar la nota de credito, debido a que el importe de la nota de credito es mayor a la de la venta");
                    }
                }
            }
        }

        public List<Transaccion> GenerarTransaccionesAModificar_(MovimientoEconomico movimientoEconomicoConPuntos)
        {
            //Verificar si se invalida una orden de venta con un pago de medio de pago puntos
            List<Transaccion> transaccionesAModificar = null;
            if (movimientoEconomicoConPuntos != null)
            {
                var extensionJsonTraza = movimientoEconomicoConPuntos.TrazaDePago().ExtensionJson;
                var puntosCanjeados = JsonConvert.DeserializeObject<List<PuntoCanjeado>>(extensionJsonTraza);
                var transaccionesDePuntos = transaccionRepositorio.ObtenerTransacciones(puntosCanjeados.Select(pc => pc.Id).ToArray());
                foreach (var transaccionDePunto in transaccionesDePuntos)
                {
                    transaccionesAModificar = new List<Transaccion>();
                    var puntosPendientesDeModificacion = puntosCanjeados.Single(pc => pc.Id == transaccionDePunto.id).Cantidad;
                    transaccionDePunto.cantidad2 -= puntosPendientesDeModificacion;
                    transaccionDePunto.cantidad3 += puntosPendientesDeModificacion;
                    transaccionesAModificar.Add(transaccionDePunto);
                }
            }
            return transaccionesAModificar;
        }
    }
}

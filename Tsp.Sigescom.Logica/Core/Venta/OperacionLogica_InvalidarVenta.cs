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
        public OperationResult ResolverInvalidarOperacionVenta(OperacionModificatoria operacionModificatoria, long idOrdenVenta, string observacion, UserProfileSessionData sesionUsuario)
        {
            try
            {
                var fechaActual = DateTimeUtil.FechaActual();
                var ordenVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenVenta));
                //Crear el estado invalidado para la orden de venta original
                Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(idOrdenVenta, sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual,
                    "Estado que se agrega al invalidar una Venta");
                //Modifcar las cuotas a modificar 
                List<Cuota> cuotasModificadas = new List<Cuota>();
                foreach (var cuota in ordenVenta.Cuotas().OrderByDescending(c => c.fecha_vencimiento))
                {
                    if (cuota.saldo > 0)
                    {
                        cuota.revocado = cuota.saldo;
                        cuota.saldo = cuota.total - cuota.pago_a_cuenta - cuota.revocado;
                        cuotasModificadas.Add(cuota);
                    }
                }
                //Todo : Resolver la invalidacion cuando es una venta que se pago con puntos (Devolucion de los puntos)
                operacionModificatoria.NuevosEstadosTransaccionesModificadas = new List<Estado_transaccion> { estadoDeLaOrdenDeVenta };
                operacionModificatoria.CuotasModificadas = cuotasModificadas;

                var resultado = AfectarInventarioFisicoYGuardarOperacion(operacionModificatoria, sesionUsuario);
                if (resultado.code_result == OperationResultEnum.Success)
                {
                    if (ordenVenta.EstaTransmitido())
                    {
                        OperationResult resultadoEFactura = facturacionRepositorio.ActualizarEstadoDocumento(idOrdenVenta, EstadoDocumentoElectronico.Anulado, EstadoSigescomDocumentoElectronico.Invalidado);
                        if (resultadoEFactura.code_result != OperationResultEnum.Success)
                        {
                            resultado.exceptions.Add(resultadoEFactura.exceptions.First());
                        }
                    }
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al resolver la invalidacion de una operacion de venta", e);
            }
        }

        public OperationResult InvalidarVenta(InvalidarVenta invalidarVenta, UserProfileSessionData sesionUsuario)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                List<Cuota> cuotasModificadas = new List<Cuota>();
                List<Estado_transaccion> nuevosEstadosTransaccion = new List<Estado_transaccion>();
                List<Detalle_transaccion> detallesTransaccionAModificar = new List<Detalle_transaccion>();
                var ordenVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(invalidarVenta.Id));
                var venta = new Venta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenVenta.IdVenta));
                var operacionInvalidacion = new OperacionInvalidacion(venta, ordenVenta, fechaActual, invalidarVenta.EsDiferida, invalidarVenta.Observacion, invalidarVenta.Pago, sesionUsuario);

                ValidarInvalidacionDeVenta(operacionInvalidacion.OrdenVenta);
                permisos_Logica.ValidarAccion(operacionInvalidacion.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, operacionInvalidacion.IdUnidadDeNegocio);
                CalcularDetallesYGenerarDetallesAModificarAlInvalidar(operacionInvalidacion, nuevosEstadosTransaccion, detallesTransaccionAModificar);

                Transaccion invalidacionVenta = GenerarInvalidarOperacion(operacionInvalidacion);
                Transaccion ordenInvalidacionVenta = GenerarOrdenInvalidarOperacion(invalidacionVenta, operacionInvalidacion);
                Transaccion pago = GenerarPagoInvalidarOperacion(invalidacionVenta, ordenInvalidacionVenta, operacionInvalidacion);
                Transaccion movimientoAlmacen = GenerarMovimientoAlmacenInvalidarOperacion(invalidacionVenta, ordenInvalidacionVenta, operacionInvalidacion, sesionUsuario);

                GenerarEstadosOperacionInvalidadaYCuotasActualizar(nuevosEstadosTransaccion, cuotasModificadas, operacionInvalidacion);
                var movimientoEconomicoConPuntos = venta.ObtenerPagos().SingleOrDefault(p => p.TrazaDePago().MedioDePago().id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos);
                List<Transaccion> transaccionesAModificar = GenerarTransaccionesAModificar(movimientoEconomicoConPuntos);

                var operacionModificatoria = new OperacionModificatoria() { Operacion = invalidacionVenta, OrdenDeOperacion = ordenInvalidacionVenta, MovimientoEconomico = pago, MovimientosBienes = movimientoAlmacen != null ? new List<Transaccion>() { movimientoAlmacen } : null, NuevosEstadosTransaccionesModificadas = nuevosEstadosTransaccion, CuotasModificadas = cuotasModificadas, TransaccionesModificadas = transaccionesAModificar, DetallesTransaccionModificados = detallesTransaccionAModificar };
                var resultadoInvalidacion = AfectarInventarioFisicoYGuardarOperacion(operacionModificatoria, sesionUsuario);
                if (resultadoInvalidacion.code_result == OperationResultEnum.Success && operacionInvalidacion.OrdenVenta.EstaTransmitido())
                {
                    OperationResult resultadoEFactura = facturacionRepositorio.ActualizarEstadoDocumento(operacionInvalidacion.OrdenVenta.Id, EstadoDocumentoElectronico.Anulado, EstadoSigescomDocumentoElectronico.Invalidado);
                    if (resultadoEFactura.code_result != OperationResultEnum.Success)
                    {
                        resultadoInvalidacion.exceptions.Add(resultadoEFactura.exceptions.First());
                    }
                }
                return resultadoInvalidacion;
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar registrar la anulacion", e);
            }
        }

        public void CalcularDetallesYGenerarDetallesAModificarAlInvalidar(OperacionInvalidacion operacionInvalidacion, List<Estado_transaccion> nuevosEstadosTransaccion, List<Detalle_transaccion> detallesTransaccionAModificar)
        {
            if (operacionInvalidacion.EsOrdenOrigenCompletada)
            {
                //Detalles de movimiento de almacen de la invalidacion
                operacionInvalidacion.DetallesMovimientoAlmacenOperacion = operacionInvalidacion.DetallesBienesOperacion;
            }
            else if (operacionInvalidacion.EsOrdenOrigenParcial)
            {
                var detallesOperacionOrigen = operacionInvalidacion.DetallesOperacion;
                var ordenAlmacen = ObtenerOrdenAlmacen(operacionInvalidacion.OrdenVenta.Id);
                operacionInvalidacion.DetallesOperacion.ForEach(di => di.cantidad_1 = ordenAlmacen.Detalles.Where(d => d.IdConcepto == di.id_concepto_negocio).Sum(d => d.Pendiente));
                detallesOperacionOrigen.ForEach(di => di.cantidad_1 = ordenAlmacen.Detalles.Where(d => d.IdConcepto == di.id_concepto_negocio).Sum(d => d.Pendiente));
                nuevosEstadosTransaccion.Add(new Estado_transaccion(operacionInvalidacion.OrdenVenta.Id, operacionInvalidacion.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionInvalidacion.FechaActual, "Estado que se agregado al invalidar una venta"));
                //Generamos los detalles de la orden original para actualizar
                foreach (var detalleOperacionOrigen in detallesOperacionOrigen)
                {
                    var detalle = transaccionRepositorio.ObtenerDetalleTransaccion(detalleOperacionOrigen.id);
                    detalle.cantidad_1 = detalleOperacionOrigen.cantidad_1;
                    detallesTransaccionAModificar.Add(detalle);
                }
                //Detalles de movimiento de almacen de la invalidacion
                operacionInvalidacion.DetallesMovimientoAlmacenOperacion = new List<Detalle_transaccion>();
                var detallesMovimientoAlmacen = ordenAlmacen.Detalles.Where(d => d.Entregado > 0).ToList();
                foreach (var detalle in detallesMovimientoAlmacen)
                {
                    var detalleAlmacen = operacionInvalidacion.DetallesBienesOperacion.First(d => d.id_concepto_negocio == detalle.IdConcepto);
                    detalleAlmacen.cantidad = detalle.Entregado;
                    operacionInvalidacion.DetallesMovimientoAlmacenOperacion.Add(detalleAlmacen);
                }
            }
            else if (operacionInvalidacion.EsOrdenOrigenPendiente)
            {
                var detallesOperacionOrigen = operacionInvalidacion.DetallesOperacion;
                detallesOperacionOrigen.ForEach(di => di.cantidad_1 = di.cantidad);
                nuevosEstadosTransaccion.Add(new Estado_transaccion(operacionInvalidacion.OrdenVenta.Id, operacionInvalidacion.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionInvalidacion.FechaActual, "Estado que se agregado al invalidar una venta"));
                //Generamos los detalles de la orden original para actualizar
                foreach (var detalleOperacionOrigen in detallesOperacionOrigen)
                {
                    var detalle = transaccionRepositorio.ObtenerDetalleTransaccion(detalleOperacionOrigen.id);
                    detalle.cantidad_1 = detalleOperacionOrigen.cantidad_1;
                    detallesTransaccionAModificar.Add(detalle);
                }
            }
        }

        public OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen)
        {
            var ordenAlmacen = ordenAlmacen_Datos.ObtenerOrdenAlmacen(idOrdenAlmacen);
            ordenAlmacen.IdsOrdenes.Insert(0, ordenAlmacen.Id);
            ordenAlmacen.Movimientos = ordenAlmacen_Datos.ObtenerMovimientosConfirmadosDeOrdenAlmacen(ordenAlmacen.IdsOrdenes.ToArray()).ToList();
            var detallesMovimientos = ordenAlmacen.Movimientos.Where(m => m.EsVigente).SelectMany(m => m.Detalles).ToList();
            foreach (var detalle in ordenAlmacen.Detalles)
            {
                detalle.Entregado = detallesMovimientos.Where(dmo => dmo.IdConcepto == detalle.IdConcepto).Sum(d => d.Cantidad);
                detalle.Pendiente = detalle.Ordenado - detalle.Entregado;
            }
            return ordenAlmacen;
        }

        public Transaccion GenerarInvalidarOperacion(OperacionInvalidacion operacionInvalidacion)
        {
            string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(c => c.Key == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta);
            Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionVenta, operacionInvalidacion.IdCentroAtencion);
            if (serie == null)
            {
                throw new LogicaException("No es posible realizar la invalidacion, no existe serie de nota de invalidacion de venta.");
            }
            Comprobante comprobante = GenerarComprobantePropioAutonumerable(serie.id);
            Transaccion invalidacionDeVenta = new Transaccion(codigo, operacionInvalidacion.Venta.IdTransaccionPadre, operacionInvalidacion.FechaActual, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta, operacionInvalidacion.IdUnidadDeNegocio, true, operacionInvalidacion.FechaActual, operacionInvalidacion.FechaActual, operacionInvalidacion.ObservacionOperacion, operacionInvalidacion.FechaActual, operacionInvalidacion.IdEmpleado, operacionInvalidacion.ImporteTotal, operacionInvalidacion.IdCentroAtencion, operacionInvalidacion.IdMoneda, operacionInvalidacion.TipoDeCambio, null, operacionInvalidacion.IdCliente)
            {
                Comprobante = comprobante,
                id_transaccion_referencia = operacionInvalidacion.Venta.Id
            };
            return invalidacionDeVenta;
        }

        public Transaccion GenerarOrdenInvalidarOperacion(Transaccion invalidacion, OperacionInvalidacion operacionInvalidacion)
        {
            Transaccion ordenInvalidacionVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(invalidacion.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(c => c.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta), null, operacionInvalidacion.FechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta, operacionInvalidacion.IdUnidadDeNegocio, true, operacionInvalidacion.FechaActual, operacionInvalidacion.FechaActual, operacionInvalidacion.ObservacionOperacion, operacionInvalidacion.FechaActual, operacionInvalidacion.IdEmpleado, operacionInvalidacion.ImporteTotal, operacionInvalidacion.IdCentroAtencion, operacionInvalidacion.IdMoneda, operacionInvalidacion.TipoDeCambio, null, operacionInvalidacion.IdCliente, operacionInvalidacion.DescuentoGlobal, operacionInvalidacion.DescuentoPorItem, operacionInvalidacion.Anticipo, operacionInvalidacion.Gravada, operacionInvalidacion.Exonerada, operacionInvalidacion.Inafecta, operacionInvalidacion.Gratuita, operacionInvalidacion.Igv, operacionInvalidacion.Isc, operacionInvalidacion.Icbper, operacionInvalidacion.OtrosCargos, operacionInvalidacion.OtrosTributos)
            {
                Comprobante = invalidacion.Comprobante,
                id_transaccion_referencia = operacionInvalidacion.OrdenVenta.Id,
                id_actor_negocio_externo1 = operacionInvalidacion.OrdenVenta.IdGrupoCliente,
            };
            ordenInvalidacionVenta.AgregarDetalles(operacionInvalidacion.DetallesOperacion);
            if (operacionInvalidacion.Icbper > 0)
            {
                ordenInvalidacionVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, operacionInvalidacion.Icbper.ToString()));
            }
            if (operacionInvalidacion.NumeroBolsasPlastico > 0)
            {
                ordenInvalidacionVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, operacionInvalidacion.NumeroBolsasPlastico.ToString()));
            }
            if (!string.IsNullOrEmpty(operacionInvalidacion.AliasCliente))
            {
                ordenInvalidacionVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, operacionInvalidacion.AliasCliente));
            }
            ordenInvalidacionVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)operacionInvalidacion.PagoOperacion.ModoDePago).ToString()));
            Estado_transaccion estadoDeLaOrdenInvalidacionDeVenta = new Estado_transaccion(operacionInvalidacion.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, operacionInvalidacion.FechaActual, "Estado inicial asignado automaticamente al registrar la invalidacion");
            ordenInvalidacionVenta.Estado_transaccion.Add(estadoDeLaOrdenInvalidacionDeVenta);
            ordenInvalidacionVenta.enum1 = (int)operacionInvalidacion.OrdenVenta.IndicadorImpactoAlmacen;
            ordenInvalidacionVenta.enum1 = operacionInvalidacion.EsOrdenOrigenPendiente ? (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial : (operacionInvalidacion.HaySeccionEntregaAlmacenOperacion ? (operacionInvalidacion.EsDiferidaOperacion ? (int)IndicadorImpactoAlmacen.Diferida : (int)IndicadorImpactoAlmacen.Inmediata) : ordenInvalidacionVenta.enum1);
            if (ordenInvalidacionVenta.enum1 == (int)IndicadorImpactoAlmacen.Inmediata || ordenInvalidacionVenta.enum1 == (int)IndicadorImpactoAlmacen.Diferida)
            {
                ordenInvalidacionVenta.id_actor_negocio_interno1 = operacionInvalidacion.IdAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(operacionInvalidacion.IdEmpleado, operacionInvalidacion.EsDiferidaOperacion ? MaestroSettings.Default.IdDetalleMaestroEstadoPendiente : MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, operacionInvalidacion.FechaActual, "Estado asignado al registrar una invalidacion");
                ordenInvalidacionVenta.Estado_transaccion.Add(estadoOrdenAlmacen);
            }
            invalidacion.Transaccion1.Add(ordenInvalidacionVenta);
            return ordenInvalidacionVenta;
        }

        public Transaccion GenerarPagoInvalidarOperacion(Transaccion invalidacion, Transaccion ordenInvalidacion, OperacionInvalidacion operacionInvalidacion)
        {
            Transaccion pago = null;
            if (operacionInvalidacion.ImportePagoTotal > 0)
            {
                if (operacionInvalidacion.PagoOperacion.ModoDePago == ModoPago.CreditoConfigurado)
                {
                    int cont = 1;
                    foreach (var item in operacionInvalidacion.PagoOperacion.CuotasDeVenta())
                    {
                        var cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, operacionInvalidacion.FechaActual.Year) + "_" + cont++, operacionInvalidacion.FechaActual, item.fecha_vencimiento, item.capital, item.interes, item.total, "Cuota generada numero " + cont, false, item.cuota_inicial);
                        ordenInvalidacion.Cuota.Add(cuota);
                    }
                    var diferencia = operacionInvalidacion.ImportePagoTotal - ordenInvalidacion.Cuota.Sum(c => c.total);
                    ordenInvalidacion.Cuota.Last().total = ordenInvalidacion.Cuota.Last().capital = ordenInvalidacion.Cuota.Last().total + diferencia;
                }
                else
                {
                    var cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, operacionInvalidacion.FechaActual.Year) + "_" + 1, operacionInvalidacion.FechaActual, operacionInvalidacion.FechaActual, operacionInvalidacion.ImportePagoTotal, "Unica cuota generada de forma automática al emitir la nota interna", false);
                    ordenInvalidacion.Cuota.Add(cuota);
                }
                //Generamos el movimiento economico de la invalidacion de la operacion 
                if (operacionInvalidacion.PagoOperacion.HayIngresoDinero)
                {
                    var esCreditoRapidoConPagoInicial = (operacionInvalidacion.PagoOperacion.ModoDePago == ModoPago.CreditoRapido && operacionInvalidacion.PagoOperacion.Inicial > 0);
                    //Obtener la cuota de la que se realizara el pago, si es al contado 
                    Cuota cuotaACobrar = (operacionInvalidacion.PagoOperacion.ModoDePago == ModoPago.Contado || esCreditoRapidoConPagoInicial) ? ordenInvalidacion.Cuota.First() : ordenInvalidacion.Cuota.Single(c => c.cuota_inicial);
                    cuotaACobrar.SetPagoACuenta(esCreditoRapidoConPagoInicial ? operacionInvalidacion.PagoOperacion.Inicial : cuotaACobrar.total);
                    ValidarImporteAPagar(1, cuotaACobrar.total, ordenInvalidacion.importe_total);
                    pago = GenerarMovimientoEconomicoPagoACuentaCuota(invalidacion, cuotaACobrar, operacionInvalidacion.IdEmpleado, operacionInvalidacion.IdCaja, operacionInvalidacion.IdCliente, TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorInvalidacionDeVenta, operacionInvalidacion.FechaActual, operacionInvalidacion.FechaActual, operacionInvalidacion.ObservacionOperacion, operacionInvalidacion.PagoOperacion.Traza.MedioDePago.Id, operacionInvalidacion.PagoOperacion.Traza.Info.EntidadFinanciera.Id, operacionInvalidacion.PagoOperacion.Traza.Info.Observacion);
                    //Validar los medios de pago y actualizar transaccion de ingreso de dinero
                    if (operacionInvalidacion.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
                    {
                        pago.cantidad1 = operacionInvalidacion.PagoOperacion.Traza.Info.PuntosCajeados;
                    }
                    if (operacionInvalidacion.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta || operacionInvalidacion.PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos)
                    {
                        pago.id_actor_negocio_interno1 = operacionInvalidacion.PagoOperacion.Traza.Info.CuentaBancaria.Id;
                    }
                    if (!string.IsNullOrEmpty(operacionInvalidacion.PagoOperacion.Traza.Info.InformacionJson)) pago.Traza_pago.First().extension_json = operacionInvalidacion.PagoOperacion.Traza.Info.InformacionJson;
                    invalidacion.Transaccion1.Add(pago);
                }
            }
            return pago;
        }
        public Transaccion GenerarMovimientoAlmacenInvalidarOperacion(Transaccion operacion, Transaccion ordenInvalidacion, OperacionInvalidacion operacionInvalidacion, UserProfileSessionData sesionUsuario)
        {
            Transaccion entradaMercaderiaPorInvalidacionVenta = null;
            if (operacionInvalidacion.HayMovimientoAlmacenOperacion)
            {
                var salidasMercaderiaVenta = operacionInvalidacion.Venta.ObtenerSalidasDeMercaderia();
                if (salidasMercaderiaVenta.Count > 0)
                {
                    entradaMercaderiaPorInvalidacionVenta = GenerarMovimientoDeMercaderia(operacion, operacionInvalidacion.IdEmpleado, operacionInvalidacion.IdAlmacen, operacionInvalidacion.IdCliente, TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta, operacionInvalidacion.FechaActual, operacionInvalidacion.ObservacionOperacion, operacionInvalidacion.DetallesMovimientoAlmacenOperacion, sesionUsuario, salidasMercaderiaVenta.First().Id);
                    entradaMercaderiaPorInvalidacionVenta.Transaccion3 = ordenInvalidacion;
                    operacion.Transaccion1.Add(entradaMercaderiaPorInvalidacionVenta);
                }
                if (operacionInvalidacion.EsOrdenOrigenParcial)
                {
                    Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna, sesionUsuario.IdCentroDeAtencionSeleccionado);
                    entradaMercaderiaPorInvalidacionVenta.Comprobante = GenerarComprobantePropioAutonumerable(serie.id);
                }
            }
            return entradaMercaderiaPorInvalidacionVenta;
        }

        public void GenerarEstadosOperacionInvalidadaYCuotasActualizar(List<Estado_transaccion> nuevosEstadosTransaccion, List<Cuota> cuotasActualizar, OperacionInvalidacion operacionInvalidacion)
        {
            nuevosEstadosTransaccion.Add(new Estado_transaccion(operacionInvalidacion.OrdenVenta.Id, operacionInvalidacion.IdEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, operacionInvalidacion.FechaActual, "Estado que se agrega al invalidar una Venta"));
            if (!operacionInvalidacion.EsCompletoEstadoCuotas)
            {
                foreach (var cuota in operacionInvalidacion.Cuotas.OrderByDescending(c => c.fecha_vencimiento))
                {
                    if (cuota.saldo > 0)
                    {
                        cuota.revocado = cuota.saldo;
                        cuota.saldo = cuota.total - cuota.pago_a_cuenta - cuota.revocado;
                        cuotasActualizar.Add(cuota);
                    }
                }
            }
        }

        public List<Transaccion> GenerarTransaccionesAModificar(MovimientoEconomico movimientoEconomicoConPuntos)
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


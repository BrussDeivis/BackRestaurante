using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        #region GENERADORES DE MOVIMIENTO DE MERCADERIA
        /// <summary>
        /// Permite calcular el costo unitario de los movimientos de mercaderia que hay en las operaciones del sistema (compras,ventas,invalidaciones,notascredito,movimientos,otros)
        /// </summary>
        /// <param name="idAlmacen">Es el almacen del cual se obtendra su semejante inventario actual</param>
        /// <param name="idTipoTransaccionMovimientoDeMercaderia">Es el tipo de transaccion que realizaa el movimiento de mercaderia</param>
        /// <param name="detallesOrden">son los detalles de la orden de operacion</param>
        /// <param name="sesionUsuario">Seccion de usuario necesario para la obtencion de inventario  actual</param>
        /// <param name="idOperacionMovientoOrigen">En el caso de movimientos relacionados a invalidaciones o notas de credito se obtiene los detalles del movimiento origen</param>
        /// <returns></returns>
        public List<Detalle_transaccion> CalcularDetallesDeMovimientoDeMercaderia(int idAlmacen, int idTipoTransaccionMovimientoDeMercaderia, List<Detalle_transaccion> detallesOrden, UserProfileSessionData sesionUsuario, long idOperacionMovientoOrigen, bool detallesDeMovimientoDesdeOrden)
        {
            List<Detalle_transaccion> detallesDeMovimiento = new List<Detalle_transaccion>();
            //Verificamos que la transaccion eeste en el diccionaario de los tipos de moovimiento a calcular los detalles de movimiento
            if (Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioSegunInventario.Contains(idTipoTransaccionMovimientoDeMercaderia))
            {
                //Obtener el id del inventario actual de la sesion de usuario
                var idTransaccionInventarioactual = sesionUsuario.ObtenerIdInventarioActual(idAlmacen);
                //Obtener los detalles del inventario actual para los conceptos de la operación  
                List<Detalle_transaccion> detallesInventarioactual = transaccionRepositorio.ObtenerDetallesTransaccion(idTransaccionInventarioactual, detallesOrden.Select(d => d.id_concepto_negocio).ToArray()).ToList();
                //Obtener el precio unitario de inventario, verificando que tenga el mismo lote
                foreach (var detalle in detallesOrden)
                {
                    var nuevoDetalle = detalle.Clone();
                    var detalleInventario = detallesInventarioactual.Single(dif => dif.id_concepto_negocio == nuevoDetalle.id_concepto_negocio);
                    nuevoDetalle.precio_unitario = detalleInventario.precio_unitario;
                    nuevoDetalle.total = nuevoDetalle.cantidad * nuevoDetalle.precio_unitario;
                    detallesDeMovimiento.Add(nuevoDetalle);
                }
            }
            //Verificcar que el tipo de transaaccion sea que no calcule los detalles de movimiento : ejemplo: compra
            else if (Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden.Contains(idTipoTransaccionMovimientoDeMercaderia))
            {
                detallesDeMovimiento = Detalle_transaccion.Clone(detallesOrden);
                foreach (var detalle in detallesDeMovimiento)
                {
                    detalle.total = TransaccionSettings.Default.AplicaLeyAmazonia ? detalle.total : detalle.total - detalle.igv;
                    detalle.precio_unitario = detalle.total / detalle.cantidad;
                }
            }
            //Verificar que el tipo de transasccion sea del tipo de cual se obtiene los detalles de movimiento de origen: ejemplo: devoluciones de compras, ventas, etc.
            else if (Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen.Contains(idTipoTransaccionMovimientoDeMercaderia))
            {
                var detallesMovimientoOperacionOrigen = transaccionRepositorio.ObtenerDetallesTransaccion(idOperacionMovientoOrigen).ToList();
                //Verificamos de donde se toma las cantidades de los detalles de orden para los detalles del movimiento
                if (detallesDeMovimientoDesdeOrden || idTipoTransaccionMovimientoDeMercaderia == TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta || idTipoTransaccionMovimientoDeMercaderia == TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra)
                {
                    foreach (var detalleOrden in detallesOrden)
                    {
                        if (detalleOrden.cantidad > 0)
                        {
                            var nuevoDetalle = detalleOrden.Clone();
                            var detalleOperacionOrigen = detallesMovimientoOperacionOrigen.Single(dif => dif.id_concepto_negocio == nuevoDetalle.id_concepto_negocio);
                            nuevoDetalle.precio_unitario = detalleOperacionOrigen.precio_unitario;
                            nuevoDetalle.total = nuevoDetalle.cantidad * nuevoDetalle.precio_unitario;
                            detallesDeMovimiento.Add(nuevoDetalle);
                        }
                    }
                }
                else
                {
                    detallesDeMovimiento = Detalle_transaccion.Clone(detallesMovimientoOperacionOrigen).ToList();
                    //clonamos los detalles del movimientoOperacionOrigen. Esto se hace por que la orden no tiene detalles, ya que estos son los mismos que los del movimiento de la operacion origen, excepto por que cambia la polaridad, lo en el origen entraba, ahora sale y viceversa.
                }
            }
            return detallesDeMovimiento;
        }
        /// <summary>
        /// Generar la salida de mercaderia para venta, con algun tipo de comprobante de la salida de mercaderia (guias de remision, nota de almacen)
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <param name="idCliente"></param>
        /// <param name="codigoDeSalidaDeMercaderia"></param>
        /// <param name="fechaEmision"></param>
        /// <param name="fechaRegistro"></param>
        /// <param name="salidaDeMercaderia"></param>
        /// <returns></returns>
        private Transaccion GenerarSalidaDeMercaderiaParaVenta(Transaccion venta, int idEmpleado, int idCentroAtencion, int idCliente, string codigoDeSalidaDeMercaderia, DateTime fechaRegistro, MovimientoDeAlmacen salidaDeMercaderia, UserProfileSessionData sesionDeUsuario)
        {
            var observacion = (String.IsNullOrEmpty(salidaDeMercaderia.Observacion()) || String.IsNullOrWhiteSpace(salidaDeMercaderia.Observacion())) ? "NINGUNO" : salidaDeMercaderia.Observacion();
            //Crear salida de mercaderia 
            Transaccion salidaMercaderiaPorVenta = new Transaccion(codigoDeSalidaDeMercaderia, null, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, venta.id_unidad_negocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, venta.importe_total, idCentroAtencion, venta.id_moneda, venta.tipo_cambio, null, idCliente)
            {
                //Agregar el comprobante a la salida de mercaderia
                Comprobante = GenerarComprobantePropio(salidaDeMercaderia.IdSerieSeleccionada, salidaDeMercaderia.NumeroIngresado)
            };
            //Obtener los detalles del movimiento de mercaderia
            var detallesDeMovimiento = CalcularDetallesDeMovimientoDeMercaderia(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, salidaDeMercaderia.DetalleTransaccion(), sesionDeUsuario, 0, false);
            //Agregar los detalles a la salida de mercaderia
            salidaMercaderiaPorVenta.AgregarDetalles(detallesDeMovimiento);
            //Resolver los parametros de transaccion de la salida de mercaderia
            salidaDeMercaderia.DocumentoReferencia = venta.Comprobante.numero_serie + "-" + venta.Comprobante.numero;
            salidaMercaderiaPorVenta = ResolverParametrosTransaccion(salidaMercaderiaPorVenta, salidaDeMercaderia.FechaInicioTraslado, salidaDeMercaderia.IdTransportista, salidaDeMercaderia.Placa, salidaDeMercaderia.IdConductor, salidaDeMercaderia.NumeroLicencia, salidaDeMercaderia.IdModalidadTransporte, salidaDeMercaderia.IdMotivoTraslado, null, salidaDeMercaderia.PesoBrutoTotal, salidaDeMercaderia.NumeroBultos, salidaDeMercaderia.DireccionOrigen, salidaDeMercaderia.IdUbigeoOrigen, salidaDeMercaderia.DireccionDestino, salidaDeMercaderia.IdUbigeoDestino, salidaDeMercaderia.DocumentoReferencia);
            //Agregamos el estado de la salida de mercaderia
            Estado_transaccion estadoDeLaSalidaMercaderia = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaRegistro, "Estado inicial asignado automaticamente al confirmar la salida de mercaderia de venta");
            salidaMercaderiaPorVenta.Estado_transaccion.Add(estadoDeLaSalidaMercaderia);
            return salidaMercaderiaPorVenta;
        }

        /// <summary>
        /// Genera una transaccion salida de mercadería a partir de un conjunto de detalles.
        /// </summary>
        /// <param name="operacion"></param>
        /// <param name="idEmpleado"></param>
        /// <param name="idAlmacen"></param>
        /// <param name="idTercero"></param>
        /// <param name="idTipoTransaccionMovimientoDeMercaderia"></param>
        /// <param name="fechaEmision"></param>
        /// <param name="fechaRegistro"></param>
        /// <param name="observacion"></param>
        /// <param name="detalles"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <param name="idOperacionMovimientoOrigen">En el caso de movimientos relacionados a invalidaciones o notas de credito se obtiene los detalles del movimiento origen</param>
        /// <returns></returns>
        private Transaccion GenerarMovimientoDeMercaderia(Transaccion operacion, int idEmpleado, int idAlmacen, int idTercero, int idTipoTransaccionMovimientoDeMercaderia, DateTime fechaRegistro, string observacion, List<Detalle_transaccion> detalles, UserProfileSessionData sesionDeUsuario, long idOperacionMovimientoOrigen)
        {
            //var detallesUnicos = GenerarDetallesSinLoteDeBienes(detalles);
            //Generar el codigo de la salida de mercaderia
            string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(m => m.Key == idTipoTransaccionMovimientoDeMercaderia).Value, idTipoTransaccionMovimientoDeMercaderia);
            //Crear la salida de mercaderia por venta
            Transaccion movimientoDeMercaderia = new Transaccion(codigo, null, fechaRegistro, idTipoTransaccionMovimientoDeMercaderia, operacion.id_unidad_negocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, operacion.importe_total, idAlmacen, operacion.id_moneda, operacion.tipo_cambio, null, idTercero)
            {
                //Agregar como comprobante, el mismo de la operación que origina el movimiento (orden)
                Comprobante = operacion.Comprobante
            };

            //Obtener los detalles del movimiento de mercaderia
            var detallesDeMovimiento = CalcularDetallesDeMovimientoDeMercaderia(idAlmacen, idTipoTransaccionMovimientoDeMercaderia, detalles, sesionDeUsuario, idOperacionMovimientoOrigen, false);
            //Agregar los detalles a la salida de mercaderia
            movimientoDeMercaderia.AgregarDetalles(detallesDeMovimiento);

            //Agregar los detalles a la salida de mercaderia
            //movimientoDeMercaderia.AgregarDetalles(Detalle_transaccion.Clone(detalles));

            //Agregamos el estado del movimiento de mercaderia
            Estado_transaccion estadoDelMovimientoMercaderia = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaRegistro, "Estado inicial asignado automaticamente al confirmar el movimiento de mercaderia");
            movimientoDeMercaderia.Estado_transaccion.Add(estadoDelMovimientoMercaderia);

            return movimientoDeMercaderia;
        }



        private Transaccion GenerarMovimientoDeMercaderia(Transaccion operacion, int idEmpleado, int idCentroAtencion, int idTercero, int idTipoTransaccionMovimientoDeMercaderia, DateTime fechaRegistro, string observacion, List<DetalleDeOperacion> detalles, UserProfileSessionData sesionDeUsuario, long idOperacionMovimientoOrigen)
        {
            //Obtener los detalles de transaccion de solo bienes
            List<Detalle_transaccion> detallesTransaccionSoloBienes = detalles.Where(d => d.Producto.EsBien).Select(d => d.DetalleTransaccion()).ToList();
            //Obtener los detalles de transaccion solo un detalle bienes
            //List<Detalle_transaccion> detallesUnicoConceptoBienes = GenerarDetallesSinLoteDeBienes(detallesTransaccionSoloBienes);
            //Generar el movimiento de mercaderia 
            return GenerarMovimientoDeMercaderia(operacion, idEmpleado, idCentroAtencion, idTercero, idTipoTransaccionMovimientoDeMercaderia, fechaRegistro, observacion, detallesTransaccionSoloBienes, sesionDeUsuario, idOperacionMovimientoOrigen);
        }



        //Resolver los parametro de transaccion de mmovimiento de mercaderia
        private Transaccion ResolverParametrosTransaccion(Transaccion movimientoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaTransporte, int idConductor, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string descripcionMotivo, decimal pesoBrutoTotal, int numeroBultos, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string documentoReferencia)
        {
            //Agregamos los parametros de transaccion 
            if (direccionOrigenTraslado != null)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDireccionOrigenTraslado, direccionOrigenTraslado.ToString()));
            }
            if (direccionDestinoTraslado != null)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDireccionDestinoTraslado, direccionDestinoTraslado.ToString()));
            }
            if (fechaInicioTransporte != null)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroFechaInicioTransporte, fechaInicioTransporte.ToString()));
            }
            if (idModalidadTransaporte == MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico)
            {
                if (idTransportista != 0)
                {
                    movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdTransportista, idTransportista.ToString()));
                }
            }
            if (idModalidadTransaporte == MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePrivado)
            {
                if (idConductor != 0)
                {
                    movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdConductor, idConductor.ToString()));
                }
                if (numeroLicenciaTransporte != null)
                {
                    movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroLicenciaTransportista, numeroLicenciaTransporte.ToString()));
                }
                if (placaTransporte != null)
                {
                    movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroPlacaMarcaTransportista, placaTransporte.ToString()));
                }
            }
            if (idModalidadTransaporte != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdModalidadTransporte, idModalidadTransaporte.ToString()));
            }
            if (idMotivoTransaporte != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdMotivoTransporte, idMotivoTransaporte.ToString()));
            }
            if (descripcionMotivo != null)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDescripcionMotivoTraslado, descripcionMotivo.ToString()));
            }
            if (pesoBrutoTotal != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroPesoBrutoTotal, pesoBrutoTotal.ToString()));
            }
            if (numeroBultos != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBultos, numeroBultos.ToString()));
            }
            if (idUbigeoOrigenTraslado != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdUbigeoOrigenTraslado, idUbigeoOrigenTraslado.ToString()));
            }
            if (idUbigeoDestinoTraslado != 0)
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdUbigeoDestinoTraslado, idUbigeoDestinoTraslado.ToString()));
            }
            if (!string.IsNullOrEmpty(documentoReferencia))
            {
                movimientoMercaderia.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDocumentoReferenciaDeGuia, documentoReferencia.ToString()));
            }
            return movimientoMercaderia;
        }

        #endregion

        #region ORDEN DE ALMACEN
        public Transaccion GenerarSerieComprobanteYOrdenDeAlmacen(Transaccion operacion, int idCentroAtencion, int idEmpleado, int idAlmacen, DateTime fechaTransporte, DateTime fechaRegistro, int idEstadoTransaccion, string observacionEstadoTransaccion, string observacion, List<DetalleDeOperacion> detalles)
        {
            try
            {
                //Obtenemos la serie de comprobante de la orden de almacen
                Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteOrdenDeAlmacen, idCentroAtencion);
                //Generar la orden de almacen con la serie obtenida
                return GenerarOrdenDeAlmacen(operacion, idEmpleado, idAlmacen, serie.id, fechaTransporte, fechaRegistro, idEstadoTransaccion, observacionEstadoTransaccion, observacion, detalles);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el intentar generar la serie de comprobante y la orden de almacen");
            }
        }

        public Transaccion GenerarOrdenDeAlmacen(Transaccion operacion, int idEmpleado, int idAlmacen, int idSerieComprobante, DateTime fechaTransporte, DateTime fechaRegistro, int idEstadoTransaccion, string observacionEstadoTransaccion, string observacion, List<DetalleDeOperacion> detalles)
        {
            try
            {
                //Obtener detalles de transaccion que afectan a las existencias
                List<Detalle_transaccion> detalles_transaccion_bienes = detalles.Where(d => d.Producto.EsBien).Select(d => d.DetalleTransaccion()).ToList();
                //Generar el codigo de la orden de almacen
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen).Value, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen);
                //Generar la orden de almacen
                Transaccion ordenDeAlmacen = new Transaccion(codigo, null, fechaRegistro, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen, operacion.id_unidad_negocio, true, fechaRegistro, fechaTransporte, observacion, fechaRegistro, idEmpleado, operacion.importe_total, idAlmacen, operacion.id_moneda, operacion.tipo_cambio, null, operacion.id_actor_negocio_externo)
                {
                    //Generar el comprobante de la orden de almacen
                    Comprobante = GenerarComprobantePropioAutonumerable(idSerieComprobante)
                };
                //Agregar los detalles a la orden de almacen
                ordenDeAlmacen.AgregarDetalles(detalles_transaccion_bienes);
                //Agregar parametro de transaccion de ingreso total de la orden
                ordenDeAlmacen.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia, ((int)IngresoTotal.No).ToString()));
                //Agregar el estado a la orden de almacen
                Estado_transaccion estadoDeOrdenDeAlmacen = new Estado_transaccion(idEmpleado, idEstadoTransaccion, fechaRegistro, observacionEstadoTransaccion);
                ordenDeAlmacen.Estado_transaccion.Add(estadoDeOrdenDeAlmacen);
                //Retornar la orden de almacen
                return ordenDeAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar la orden de almacen", e);
            }
        }

        public OperationResult GuardarOrdenDeAlmacen(long idOrdenDeOperacion, int idEmpleado, int idCentroAtencion, int idSerieComprobante, int idAlmacen, DateTime fechaTransporte, string observacion, List<DetalleDeOperacion> detalles, bool generacionTotalOrden)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtener la orden de operacion
                OperacionGenericaNivel3 ordenDeOperacion = new OperacionGenericaNivel3(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeOperacion));
                //Generar la orden de almacen
                Transaccion ordenDeAlmacen = GenerarOrdenDeAlmacen(ordenDeOperacion.Transaccion().Transaccion2, idEmpleado, idAlmacen, idSerieComprobante, fechaTransporte, fechaActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto al crear una orden de almacen", observacion, detalles);
                //Vincular la referencia a la orden de la operacion
                ordenDeAlmacen.id_transaccion_referencia = ordenDeOperacion.Id;
                //Vincular el id de transaccion padre de movimeinto con el id de padre de la orden
                ordenDeAlmacen.id_transaccion_padre = ordenDeOperacion.Transaccion().id_transaccion_padre;
                //Crear el parametro de transaccion
                Parametro_transaccion parametroTransaccion = null;
                if (generacionTotalOrden)
                {
                    parametroTransaccion = ordenDeOperacion.Transaccion().Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoGeneracionOrdenDeAlmacen);
                    parametroTransaccion.valor = "1";
                }
                return transaccionRepositorio.CrearTransaccionYActualizarParametroTransaccion(ordenDeAlmacen, parametroTransaccion);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar la orden de almacen", e);
            }
        }

        #endregion

        #region MOVIMIENTO DE ALMACEN

        //public OperationResult GuardarMovimientoDeAlmacen(long idOrdenDeAlmacen, int idEmpleado, int idCentroAtencion, bool esPropio, int idTipoComprobante, int idSerieComprobante, string serieDeComprobante, int numeroDeComprobante, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string documentoReferencia, string observacion, List<Detalle_transaccion> detalles, bool ingresoTotalOrden, UserProfileSessionData sesionUsuario)
        //{
        //    try
        //    {
        //        DateTime fechaActual = DateTimeUtil.FechaActual();
        //        //Obtener la orden del movimiento de mercaderia  
        //        OrdenDeMovimientoDeAlmacen ordenDeAlmacen = new OrdenDeMovimientoDeAlmacen(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeAlmacen));
        //        //Generar el comprobante del movimiento de mercaderia
        //        Comprobante comprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, serieDeComprobante, numeroDeComprobante);
        //        //Obtenemos el id del tipo de transaccion de movimiento de almacen
        //        int idTipoTransaccionMovimientoDeAlmacen = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.Single(m => m.Key == ordenDeAlmacen.OrdenDeOperacion().IdTipoTransaccion).Value;
        //        //Generar la ingreso de mercaderia
        //        Transaccion movimientoDeAlmacen = GenerarMovimientoDeMercaderia(ordenDeAlmacen.OrdenDeOperacion().Transaccion(), comprobante, idEmpleado, idCentroAtencion, ordenDeAlmacen.IdTercero(), idTipoTransaccionMovimientoDeAlmacen, fechaActual, observacion, fechaInicioTransporte, idTransportista, placaYMarcaTransporte, numeroLicenciaTransporte, idModalidadTransaporte, idMotivoTransaporte, direccionOrigenTraslado, idUbigeoOrigenTraslado, direccionDestinoTraslado, idUbigeoDestinoTraslado, documentoReferencia, detalles, sesionUsuario, 0);
        //        //Vincular la referencia a la orden de la operacion
        //        movimientoDeAlmacen.id_transaccion_referencia = ordenDeAlmacen.OrdenDeOperacion().Id;
        //        //Vincular el id de transaccion padre de movimeinto con el id de padre de la orden
        //        //todo: Revisar la asignacion de id_transaccion_padre ... está raro... ¿por que le pone el ide de la orden???
        //        movimientoDeAlmacen.id_transaccion_padre = ordenDeAlmacen.Id;
        //        //Crear el parametro de transaccion
        //        Parametro_transaccion parametroTransaccion = null;
        //        if (ingresoTotalOrden)
        //        {
        //            parametroTransaccion = ordenDeAlmacen.Transaccion().Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia);
        //            parametroTransaccion.valor = "1";
        //            transaccionRepositorio.ActualizarParametroTransaccion(parametroTransaccion);
        //        }
        //        //Guardar movimiento con todo actualizacion de inventario actual
        //        return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada() { Operacion = movimientoDeAlmacen, MovimientosBienes = new List<Transaccion>() { movimientoDeAlmacen }, OrdenDeOperacion = ordenDeAlmacen.Transaccion().Transaccion3 }, sesionUsuario);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al intentar guardar el movimiento de mercaderia", e);
        //    }
        //}

        #endregion

        #region MOVIMIENTO EN ORDEN DE ALMACEN

        public OperationResult GuardarMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen, UserProfileSessionData sesionUsuario)
        {
            try
            {
                var fechaActual = DateTimeUtil.FechaActual();
                var transaccionOrdenVenta = transaccionRepositorio.ObtenerTransaccion(movimientoOrdenAlmacen.IdOrdenDeAlmacen);
                //Verificamos la observacion de la guia de remision
                movimientoOrdenAlmacen.Observacion = (String.IsNullOrEmpty(movimientoOrdenAlmacen.Observacion) || String.IsNullOrWhiteSpace(movimientoOrdenAlmacen.Observacion)) ? "NINGUNO" : movimientoOrdenAlmacen.Observacion;
                //Verificar si se esta movimeinto todo los pendiente de la orden
                var movimientoDetallesOrdenCompleto = movimientoOrdenAlmacen.Detalles.Where(d => d.Pendiente > 0).Count() == movimientoOrdenAlmacen.Detalles.Where(d => d.Pendiente > 0).Where(d => d.Pendiente == d.IngresoSalidaActual).Count();
                //Obtener los detalles que estan en el movimiento de almacen
                movimientoOrdenAlmacen.Detalles = movimientoOrdenAlmacen.Detalles.Where(d => d.IngresoSalidaActual > 0).ToList();
                //Contruir los detalles de movimiento de orden de almacen
                var detallesMovimientoOrdenAlmacen = ConstruirDetallesMovimientoOrdenAlmacen(movimientoOrdenAlmacen);
                //Obtener la orden del movimiento de alamcen  
                Transaccion ordenAlmacen = transaccionRepositorio.ObtenerTransaccionInclusiveEstados(movimientoOrdenAlmacen.IdOrdenDeAlmacen);
                //Obtener el wrapper de movimiento de almacen  
                Transaccion operacionAlmacen = transaccionRepositorio.ObtenerTransaccionInclusiveEstados((long)ordenAlmacen.id_transaccion_padre);
                //Generar el comprobante del movimiento de almacen
                Comprobante comprobante = GenerarComprobante(movimientoOrdenAlmacen.TipoDeComprobante.EsPropio, movimientoOrdenAlmacen.TipoDeComprobante.SerieSeleccionada, movimientoOrdenAlmacen.TipoDeComprobante.TipoComprobante.Id, movimientoOrdenAlmacen.TipoDeComprobante.SerieIngresada, movimientoOrdenAlmacen.TipoDeComprobante.NumeroIngresado);
                //Obtenemos el id del tipo de transaccion de movimiento de almacen
                int idTipoTransaccionMovimientoDeAlmacen = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.Single(m => m.Key == ordenAlmacen.id_tipo_transaccion).Value;
                //Generar el movimiento de almacen
                Transaccion movimientoDeAlmacen = GenerarMovimientoDeMercaderia(operacionAlmacen, comprobante, sesionUsuario.Empleado.Id, (int)transaccionOrdenVenta.id_actor_negocio_interno1, movimientoOrdenAlmacen.Tercero.Id, idTipoTransaccionMovimientoDeAlmacen, fechaActual, movimientoOrdenAlmacen.Observacion, movimientoOrdenAlmacen.FechaInicioTraslado, movimientoOrdenAlmacen.Transportista.Transportista.Id, movimientoOrdenAlmacen.Transportista.Placa, movimientoOrdenAlmacen.Conductor.Conductor.Id, movimientoOrdenAlmacen.Conductor.NumeroLicencia, movimientoOrdenAlmacen.ModalidadTransporte.Id, movimientoOrdenAlmacen.MotivoTraslado.Id, movimientoOrdenAlmacen.PesoBrutoTotal, movimientoOrdenAlmacen.NumeroBultos, movimientoOrdenAlmacen.DireccionOrigen, movimientoOrdenAlmacen.UbigeoOrigen.Id, movimientoOrdenAlmacen.DireccionDestino, movimientoOrdenAlmacen.UbigeoDestino.Id, movimientoOrdenAlmacen.DocumentoReferencia, detallesMovimientoOrdenAlmacen, sesionUsuario, movimientoOrdenAlmacen.IdOrdenDeAlmacen, true);
                //Vincular la referencia a la orden de la operacion
                movimientoDeAlmacen.id_transaccion_referencia = ordenAlmacen.id;
                //Vincular el id de transaccion padre de movimiento a la operacion wrapper
                movimientoDeAlmacen.id_transaccion_padre = operacionAlmacen.id;
                //Verificar si se esta completo el movimiento de los detalles la orden
                if (movimientoDetallesOrdenCompleto)
                {
                    Estado_transaccion estadoDeMovimientoDeAlmacen = new Estado_transaccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaActual, "Estado asignado automaticamente al completar el movimiento de mercaderia");
                    ordenAlmacen.Estado_transaccion.Add(estadoDeMovimientoDeAlmacen);
                }
                else
                {
                    Estado_transaccion estadoDeMovimientoDeAlmacen = new Estado_transaccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoParcial, fechaActual, "Estado asignado automaticamente al hacer una entrega parcial de la mercaderia");
                    ordenAlmacen.Estado_transaccion.Add(estadoDeMovimientoDeAlmacen);
                }
                //Guardar movimiento con todo actualizacion de inventario actual
                return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada() { Operacion = movimientoDeAlmacen, MovimientosBienes = new List<Transaccion>() { movimientoDeAlmacen }, OrdenDeOperacion = ordenAlmacen }, sesionUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el movimiento en orden de almacen", e);
            }
        }

        internal List<Detalle_transaccion> ConstruirDetallesMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen)
        {
            List<Detalle_transaccion> detallesOrdenAlmacen = new List<Detalle_transaccion>();
            foreach (var item in movimientoOrdenAlmacen.Detalles.Where(d => d.IngresoSalidaActual > 0))
            {
                detallesOrdenAlmacen.Add(new Detalle_transaccion(item.IngresoSalidaActual, item.IdProducto, "", 1, 1, null, 0, null, null, 0, 0, 0, null, null, null));
            }
            return detallesOrdenAlmacen;
        }

        internal Transaccion GenerarMovimientoDeMercaderia(Transaccion operacion, Comprobante comprobante, int idEmpleado, int idCentroAtencion, int idTercero, int idTipoTransaccionMovimiento, DateTime fechaRegistro, string observacion, DateTime fechaInicioTransporte, int idTransportista, string placaTransporte, int idConductor, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, decimal pesoBrutoTotal, int numeroBultos, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string documentoReferencia, List<Detalle_transaccion> detalles, UserProfileSessionData sesionDeUsuario, long idOperacionMovimientoOrigen, bool detallesDeMovimientoDesdeOrden)
        {
            Transaccion movimientoMercaderia = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(m => m.Key == idTipoTransaccionMovimiento).Value, idTipoTransaccionMovimiento), null, fechaRegistro, idTipoTransaccionMovimiento, operacion.id_unidad_negocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, operacion.importe_total, idCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, idTercero)
            {
                Comprobante = comprobante
            };
            //Obtener los detalles del movimiento de mercaderia
            var detallesDeMovimiento = CalcularDetallesDeMovimientoDeMercaderia(idCentroAtencion, idTipoTransaccionMovimiento, detalles, sesionDeUsuario, idOperacionMovimientoOrigen, detallesDeMovimientoDesdeOrden);
            //Agregar los detalles a la salida de mercaderia
            movimientoMercaderia.AgregarDetalles(detallesDeMovimiento);
            //Agregar los parametros de transaccion 
            movimientoMercaderia = ResolverParametrosTransaccion(movimientoMercaderia, fechaInicioTransporte, idTransportista, placaTransporte, idConductor, numeroLicenciaTransporte, idModalidadTransaporte, idMotivoTransaporte, null, pesoBrutoTotal, numeroBultos, direccionOrigenTraslado, idUbigeoOrigenTraslado, direccionDestinoTraslado, idUbigeoDestinoTraslado, documentoReferencia);
            //Agregar el estado al movimiento en el orden almacen
            Estado_transaccion estadoDeMovimientoDeAlmacen = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaRegistro, "Estado inicial asignado automaticamente en registrar el movimiento de mercaderia");
            movimientoMercaderia.Estado_transaccion.Add(estadoDeMovimientoDeAlmacen);
            return movimientoMercaderia;
        }

        #endregion


        #region MOVIMIENTO INTERNO DE MERCADERIA

        // comofirma el movimiento de mercaderia interno integrado, osea te genera la transaccion wraper, la orden , la salida de mercaderia y el ingreso de mecaderia en un solo metodo. 
        public OperationResult ConfirmarMovimientoInternoMercaderiaIntegrado(int idEmpleado, int idCentroAtencion, int idAlmacenDestino, int idResponsableDestino, int idTipoComprobante, int idSerieComprobante,
           bool esPropio, string serieDeComprobante, int numeroDeComprobante, string observacion, List<Detalle_transaccion> detallesOrden, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Generamos el movimiento interno de mercaderia, el WRAPPER
                Transaccion movimientoInterno = GenerarMovimientoInternoDeAlmacen(idEmpleado, idCentroAtencion, idAlmacenDestino, idTipoComprobante, idSerieComprobante, esPropio,
                    serieDeComprobante, numeroDeComprobante, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionDesplazamientoInternoMercadería, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento, observacion);
                //Generamos la orden de movimiento
                Transaccion ordenDeMovimiento = GenerarOrdenDeMovimientoInterno(movimientoInterno, idEmpleado, idCentroAtencion, idAlmacenDestino, fechaRegistro,
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    "Estado inicial asignado automaticamente al confirmar un movimiento interno de mercaderia", detallesOrden, observacion);
                //Informacion de la orden de almacen de traslado
                ordenDeMovimiento.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaRegistro, "Estado asignado al confirmar el traslado interno");
                ordenDeMovimiento.Estado_transaccion.Add(estadoOrdenAlmacen);
                //Agregamos la orden de venta en la venta
                movimientoInterno.Transaccion1.Add(ordenDeMovimiento);
                //Generamos la salida de mercaderia
                Transaccion salidaMercaderiaPorMovimientoInterno = GenerarMovimientoInternoMercaderia(movimientoInterno, idEmpleado, idCentroAtencion, idAlmacenDestino, fechaRegistro,
                    TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno, "_SMDI", MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    "Estado inicial asignado automaticamente al confirmar la salida de mercaderia por movimiento interno", observacion, detallesOrden, sesionDeUsuario);
                //asignar los costos unitarios y totales a la orden en base a los obtenidos de la salida.
                detallesOrden.ForEach(dOrden =>
                {
                    var detalleSalida = salidaMercaderiaPorMovimientoInterno.Detalle_transaccion.SingleOrDefault(dSalida => dSalida.id_concepto_negocio == dOrden.id_concepto_negocio && dSalida.lote == dOrden.lote);
                    dOrden.precio_unitario = detalleSalida.precio_unitario;
                    dOrden.total = detalleSalida.total;
                });

                //agregamos la orden de movimiento como operacion referencia de la salida de mercaderia
                salidaMercaderiaPorMovimientoInterno.Transaccion3 = ordenDeMovimiento;
                //agregamos la salida de la mercaderia a la venta
                movimientoInterno.Transaccion1.Add(salidaMercaderiaPorMovimientoInterno);
                //Generamos la ingreso de mercaderia
                Transaccion ingresoMercaderiaPorMovimientoInterno = GenerarMovimientoInternoMercaderia(movimientoInterno, idResponsableDestino, idAlmacenDestino, idCentroAtencion, fechaRegistro,
                    TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno, "_IMDI", MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    "Estado inicial asignado automaticamente al confirmar el ingreso de mercaderia por movimiento interno", observacion, detallesOrden, sesionDeUsuario);
                //agregamos la orden de movimeinto como operacion referencia del ingreso de mercaderia
                ingresoMercaderiaPorMovimientoInterno.Transaccion3 = ordenDeMovimiento;
                //agregamos la ingreso de la mercaderia a la venta
                movimientoInterno.Transaccion1.Add(ingresoMercaderiaPorMovimientoInterno);


                return AfectarExisteciasyGuardarMovimientoInterno(movimientoInterno, detallesOrden, idCentroAtencion, idAlmacenDestino);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private OperationResult AfectarExisteciasyGuardarMovimientoInterno(Transaccion movimientoInterno, List<Detalle_transaccion> detalles, int idCentroAtencion, int idAlmacenDestino)
        {
            OperationResult result;
            do
            {
                result = transaccionRepositorio.CrearTransaccionActualizarDetalleTransaccionExistente(movimientoInterno, detalles, idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idAlmacenDestino, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
            }
            while (result.code_result == OperationResultEnum.Error && result.exceptions.FirstOrDefault().GetType() == typeof(ExistenciaException));
            result.information = new MovimientoDeAlmacen(movimientoInterno.Transaccion1.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno)).Id;
            return result;
        }

        // Genera la transacion wraper para un movimiento de mercaderia. esta transaccion va a contenert 3 transacciones : una orden, una entrada y una salida
        public Transaccion GenerarMovimientoInternoDeAlmacen(int idEmpleado, int idCentroAtencion, int idAlmacenDestino, int idTipoComprobante, int idSerieComprobante, bool esPropio, string serieDeComprobante,
            int numeroDeComprobante, DateTime fechaRegistro, int idTipoTransaccion, int idTipoAccionARealizar, int idTipoTransaccionValidar, string observacion)
        {
            int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
            int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            decimal tipoDeCambio = 1;
            //Validamos la accionn a realizar
            permisos_Logica.ValidarAccion(idEmpleado, idTipoAccionARealizar, idTipoTransaccionValidar, idUnidadNegocio);
            //Verificamos si el stock es centralizado
            if (AplicacionSettings.Default.StockCentralizado)
            {
                throw new LogicaException("La gestión de stock se encuentra configurada como CENTRALIZADA, por lo que no es posible desplazar mercaderia");
            }
            //Obtener operacion generica actual
            Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
            //Obtenemos el codigo
            string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("DIM", idTipoTransaccion);
            Comprobante comprobante;
            //comparamos para ver si esta seleccionando un documento propio o no propio
            if (esPropio)
            {
                //Obtenemos la serie
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieComprobante);
                //Obtenemos el tipo comprobante
                comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
                serie.proximo_numero = (serie.proximo_numero + 1);
            }
            else
            {
                comprobante = new Comprobante(idTipoComprobante, null, numeroDeComprobante, true, serieDeComprobante);
            }
            //crear una orden de traslado interno
            Transaccion movimientoInterno = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionDesplazamientoInternoMercadería,
                idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, 0, idCentroAtencion, idMoneda, tipoDeCambio, null, idAlmacenDestino)
            {
                //agregamos el comprobante al movimiento
                Comprobante = comprobante
            };
            //retornar movimiento
            return movimientoInterno;
        }

        // Genera la orden del movimiento interno de mercaderia
        public Transaccion GenerarOrdenDeMovimientoInterno(Transaccion movimiento, int idEmpleado, int idCentroAtencion, int idAlmacenDestino, DateTime fechaRegistro, int idOrdenTransaccion,
            int idEstadoTransaccion, string observacionEstadoTransaccion, List<Detalle_transaccion> detalles, string observacion)
        {
            //crear una orden de traslado
            Transaccion ordenMovimientoInterno = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(movimiento.codigo + "_ODM", idOrdenTransaccion), null, fechaRegistro, idOrdenTransaccion,
                movimiento.id_unidad_negocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, 0, idCentroAtencion, movimiento.id_moneda, movimiento.tipo_cambio,
                null, idAlmacenDestino)
            {
                //agregamos el comprobante a la orden de traslado de almacen
                Comprobante = movimiento.Comprobante
            };
            //agregamos los detalles
            ordenMovimientoInterno.AgregarDetalles(detalles);
            //agregamos el estado de la orden por defecto
            Estado_transaccion estadoOrdenMovimeintoInterno = new Estado_transaccion(idEmpleado, idEstadoTransaccion, fechaRegistro, observacionEstadoTransaccion);
            //agregamos el Estado de la orden de traslado interno a la salida de mercaderia
            ordenMovimientoInterno.Estado_transaccion.Add(estadoOrdenMovimeintoInterno);
            return ordenMovimientoInterno;
        }

        // genera le movimeiento de mercaderia, esta puede ser de ingreso o de salida de acuerdo al tipo de trnasdaciion que se tenga que usar
        private Transaccion GenerarMovimientoInternoMercaderia(Transaccion movimiento, int idEmpleado, int idActorInterno, int idActorExterno, DateTime fechaRegistro, int idTipoTransaccionMovimiento, string sufijoCodigo, int idEstadoTransaccion, string observacionEstadoTransaccion, string observacion, List<Detalle_transaccion> detallesOrden, UserProfileSessionData sesionDeUsuario)
        {
            //creamos movimiento de mercaderia por venta
            Transaccion movimientoMercaderia = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(movimiento.codigo + sufijoCodigo, idTipoTransaccionMovimiento),
                null, fechaRegistro, idTipoTransaccionMovimiento, movimiento.id_unidad_negocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro,
                idEmpleado, movimiento.importe_total, idActorInterno, movimiento.id_moneda, movimiento.tipo_cambio, null, idActorExterno)
            {
                //agregamos el comprobante a la orden de traslado de almacen
                Comprobante = movimiento.Comprobante
            };
            //Obtener los detalles del movimiento de mercaderia
            var detallesDeMovimiento = CalcularDetallesDeMovimientoDeMercaderia(idActorInterno, idTipoTransaccionMovimiento, detallesOrden, sesionDeUsuario, 0, false);
            //Agregar los detalles a la salida de mercaderia
            movimientoMercaderia.AgregarDetalles(detallesDeMovimiento);
            //agregamos el estado de la orden por defecto
            Estado_transaccion estadoMovimientoMercaderia = new Estado_transaccion(idEmpleado, idEstadoTransaccion, fechaRegistro, observacionEstadoTransaccion);
            //agregamos el estado al movimiento de mercaderia
            estadoMovimientoMercaderia.Transaccion = movimientoMercaderia;//VANVAN
            movimientoMercaderia.Estado_transaccion.Add(estadoMovimientoMercaderia);
            return movimientoMercaderia;
        }

        #endregion


        public void ResolverPosiblesProblemasDeConcurrenciaInventario(List<Detalle_transaccion> detallesInventarioactual_Existentes, DetalleTransaccionException exception)
        {
            //refrescar datos del inventario
            transaccionRepositorio.RefreshEntityCollection(detallesInventarioactual_Existentes);
        }

        /// <summary>
        /// Aqui se espera que exista un único comprobante para la serie afectada por la excepción de concurrencia. 
        /// Este comprobante podria estar asignado a la operacionContenedora y/o a alguna de sus operaciones hijas
        /// Regenera el único comprobante perteneciente a alguna de las operaciones hijas de <paramref name="operacionContenedora"/> cuya serie ha sido afectada por el problema de concurrencia
        /// </summary>
        /// <param name="operacionContenedora">Oeración wrapper que contiene a la orden, a los movimientos de bienes, movimiento de dinero, etc. </param>
        /// <param name="exception"></param>
        public void ResolverProblemaDeConcurrenciaSerieComprobante(Transaccion operacionContenedora, SerieComprobanteException exception)
        {
            List<Comprobante> comprobantes = operacionContenedora.Transaccion1.Select(t => t.Comprobante).Distinct().ToList();//conseguimos los comprobantes de las transacciones hijas
            comprobantes.Add(operacionContenedora.Comprobante);//añadimos el comprobante de la operacion contenedora
            comprobantes = comprobantes.Distinct().ToList();
            var comprobanteAfectado = comprobantes.Single(c => c.id_serie_comprobante == exception.serieComprobante.id);//se espera que solo haya un único comprobante para la serie afectada. De otro modo se genera una execpción.
            transaccionRepositorio.RefreshEntity(comprobanteAfectado.Serie_comprobante);
            RegenerarNumeracionComprobantePropioAutonumerable(comprobanteAfectado, exception.serieComprobante);
        }

        /// <summary>
        /// Aqui se espera que existan muchos comprobantes para una misma serie afectada por la excepción de concurrencia. 
        /// Cada uno de los comprobantes podrian estar asignados a una operacionContenedora y/o a alguna de sus operaciones hijas 
        /// </summary>
        /// <param name="operacionesContenedoras"></param>
        /// <param name="exception"></param>
        public void ResolverProblemaDeConcurrenciaSerieComprobante(List<Transaccion> operacionesContenedoras, SerieComprobanteException exception)
        {
            List<Comprobante> comprobantes = operacionesContenedoras.SelectMany(o => o.Transaccion1).Select(t => t.Comprobante).Distinct().ToList();//conseguimos los comprobantes de las transacciones hijas de cada transaccion contenedora
            comprobantes.AddRange(operacionesContenedoras.Select(o => o.Comprobante));//añadimos el comprobante de las operacion contenedoras
            comprobantes = comprobantes.Distinct().ToList(); // Aseguramos que no exista redundancia entre los contenedores y su transacciones hijas
            var serieAfectada = exception.serieComprobante;
            var comprobantesAfectados = comprobantes.Where(c => c.id_serie_comprobante == serieAfectada.id).ToList();
            transaccionRepositorio.RefreshEntity(serieAfectada);
            RegenerarNumeracionComprobantesPropiosAutonumerables(comprobantesAfectados, serieAfectada);
        }

        /// <summary>
        /// /*En el detalle movimiento de almacen pueden haber dos item con el mimsmo lote cosa que debe permitir guardar
        /// 1. Normal se debe guardar como detalles de transaccion en la orden de compra
        /// 2. En el ingreso o salida de mercaderia, se debe Unificar los items de detalle_transaccion cuando son del mismo lote:  sumar cantidades, sacar precio ponderado promedio. Este método se encarga del punto 2: generar un solo detalle de transaccion por cada lote a partir de los detallesMovimientoAlmacen.
        /// </summary>
        /// <param name="detallesMovimientoAlmacen"></param>
        /// <returns></returns>
        public List<Detalle_transaccion> UnificarPorConceptoYLote(List<Detalle_transaccion> detallesMovimientoAlmacen)
        {

            var detallesMovimientoAlmacenAgrupadosPorLote = detallesMovimientoAlmacen.GroupBy(dma => new { dma.id_concepto_negocio, dma.lote }).ToList();
            List<Detalle_transaccion> nuevosDetallesMovimientoAlmacenAgrupadosPorLote = new List<Detalle_transaccion>();
            foreach (var grupo in detallesMovimientoAlmacenAgrupadosPorLote)
            {
                List<ValorDetalleMaestroDetalleTransaccion> valoresDetalleMaestroDetallesTransaccion = new List<ValorDetalleMaestroDetalleTransaccion>();
                var cantidad_principal = grupo.Sum(dm => dm.cantidad);
                var cantidad_secundaria = grupo.Sum(dm => dm.cantidad_secundaria);
                var total = grupo.Sum(dm => dm.total);
                var costoUnitario = cantidad_principal != 0 ? total / cantidad_principal : 0;
                var igv = grupo.Sum(dm => dm.igv);
                var isc = grupo.Sum(dm => dm.isc);
                var descuento = grupo.Sum(dm => dm.descuento);

                Detalle_transaccion detalle = new Detalle_transaccion()
                {
                    id_concepto_negocio = grupo.Key.id_concepto_negocio,
                    cantidad = cantidad_principal,
                    cantidad_secundaria = cantidad_secundaria,
                    detalle = "Inventario Físico",
                    precio_unitario = costoUnitario,
                    total = total,
                    igv = igv,
                    isc = isc,
                    descuento = descuento,
                    lote = grupo.Key.lote,
                };
                nuevosDetallesMovimientoAlmacenAgrupadosPorLote.Add(detalle);
            }
            return nuevosDetallesMovimientoAlmacenAgrupadosPorLote;
        }


        /// <summary>
        /// se asume que si o si el concepto y lote están en el inventario. El costo unitario no se calcula, se asigna lo que recibe como parámetro
        /// </summary>
        /// <param name="detalleMovimientoBien"></param>
        /// <param name="detalleInventarioactual_Existente"></param>
        /// <param name="debeAumentarExistencia"></param>
        public void ResolverDetalleExistenteEnInventarioActual(Detalle_transaccion detalleMovimiento, Detalle_transaccion detalleInventarioactual, bool debeAumentarExistencia, decimal costoUnitario, bool esSalidaSujetaADisponibilidadDeStock)
        {
            if (!debeAumentarExistencia && esSalidaSujetaADisponibilidadDeStock && (detalleMovimiento.cantidad > detalleInventarioactual.cantidad)) throw new LogicaException(detalleInventarioactual.Concepto_negocio.nombre + "No cuenta con stock suficiente. Actualmente solo quedan " + detalleInventarioactual.cantidad + " unidades");
            var factor = debeAumentarExistencia ? 1 : -1;

            //Obtener la nueva cantidad de acuerdo al parametro aumentar existencia
            decimal nuevaCantidadPrincipal = detalleInventarioactual.cantidad + detalleMovimiento.cantidad * factor;
            decimal nuevaCantidadSecundaria = detalleInventarioactual.cantidad_secundaria + detalleMovimiento.cantidad_secundaria * factor;
            detalleInventarioactual.precio_unitario = costoUnitario;
            detalleInventarioactual.total = detalleInventarioactual.precio_unitario * nuevaCantidadPrincipal;
            //Modificar cantidad del inventario actual
            detalleInventarioactual.cantidad = nuevaCantidadPrincipal;
            detalleInventarioactual.cantidad_secundaria = nuevaCantidadSecundaria;
            ///resolvemos caracteristicas propias.
            var detalleTieneCaracteristicasPropias = detalleMovimiento.Valor_detalle_maestro_detalle_transaccion != null && detalleMovimiento.Valor_detalle_maestro_detalle_transaccion.Count() > 0;
            if (detalleTieneCaracteristicasPropias) ResolverCaracteristicasPropias(detalleMovimiento, detalleInventarioactual, debeAumentarExistencia);
        }

        public void ResolverDetalleNuevoEnInventarioActual(Detalle_transaccion detalleMovimiento, List<Detalle_transaccion> detallesInventarioactual_Nuevos, bool debeAumentarExistencia)
        {
            var nuevoDetalle = detalleMovimiento.Clone();
            nuevoDetalle.cantidad *= debeAumentarExistencia ? 1 : -1;
            nuevoDetalle.total *= debeAumentarExistencia ? 1 : -1;
            detallesInventarioactual_Nuevos.Add(nuevoDetalle);
        }

        public void ResolverCaracteristicasPropias(Detalle_transaccion detalleMovimientoBien, Detalle_transaccion detalleInventarioactual, bool debeAumentarExistencia)//si el detalle del movimiento  tiene valores de caracteristicas propias
        {
            foreach (var valorCaracteristicaPropiaDelDetalleMovimientoAlmacen in detalleMovimientoBien.Valor_detalle_maestro_detalle_transaccion.ToList())//por cada valor
            {
                if (debeAumentarExistencia)//En caso debe de aumentar cantidad, se debe agregar los valores de caracteristica propia. 
                {
                    detalleInventarioactual.Valor_detalle_maestro_detalle_transaccion.Add(valorCaracteristicaPropiaDelDetalleMovimientoAlmacen.Clone());
                }
                else //De lo contrario se debe actualizar el id_detalle_transaccion. De este modo, el valor ya no pertenece al detalle del inventario, si no al detalle del movimiento de almacén(lo estamos reciclando a nivel de base de datos).
                {
                    detalleInventarioactual.Valor_detalle_maestro_detalle_transaccion.SingleOrDefault(dbvd => dbvd.id == valorCaracteristicaPropiaDelDetalleMovimientoAlmacen.id).Detalle_transaccion = detalleMovimientoBien;
                    //todo: hacer caso de prueba en este escenario.... la idea es que se recicle el valor, no que se duplique. Si esto no anda bien, entonces debe eliminarse el registro (valor de caracteristica propia)
                }
            }
        }

        /// <summary>
        /// en base a un detalle de movimiento de bienesm resuelve el detalle correspondiente en el inventario actual, modificando el existente para el concepto de negocio y lote, o agregando un nuevo detalle al inventario, en caso no lo tenga.
        /// </summary>
        /// <param name="detallesMovimientoDeConcepto"></param>
        /// <param name="detallesInventarioActual_Existentes"></param>
        /// <param name="detallesInventarioActual_Nuevos"></param>
        /// <param name="idTransaccionInventarioactual"></param>
        /// <param name="debeAumentarExistencia"></param>
        public void ResolverDetallesDeInventarioActual(IGrouping<int, Detalle_transaccion> detallesMovimientoDeConcepto, List<Detalle_transaccion> detallesInventarioActual_Existentes, List<Detalle_transaccion> detallesInventarioActual_Nuevos, long idTransaccionInventarioactual, bool debeAumentarExistencia, bool esSalidaSujetaADisponibilidadDeStock, int idTipoTrnsaccionMovimientoBienes)
        {
            //Buscar los  detalles de transaccion del inventarioa ctual correspondiente al concepto de negocio (si hayy varios lotes, habrán varios detalles)
            var detallesInventarioActuak = detallesInventarioActual_Existentes.Where(dbdt => dbdt.id_concepto_negocio == detallesMovimientoDeConcepto.Key).ToList();
            var factor = debeAumentarExistencia ? 1 : -1;
            var debeRecalcularPrecioUnitario = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden.Union(Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen).Contains(idTipoTrnsaccionMovimientoBienes);
            //calcular costo unitario del concepto, sin importar el lote, ya que se maneja un costo unitario unico por concepto y no por lote.
            decimal CantidadPrincipalTodosLosLotes = detallesInventarioActuak.Sum(dif => dif.cantidad) + detallesMovimientoDeConcepto.Sum(dmc => dmc.cantidad) * factor;
            decimal ImporteTotalTodosLosLotes = detallesInventarioActuak.Sum(dif => dif.total) + (detallesMovimientoDeConcepto.Sum(dmc => dmc.total) * factor);
            decimal CostoUnitarioTodosLosLotes = debeRecalcularPrecioUnitario ? (CantidadPrincipalTodosLosLotes != 0 ? (ImporteTotalTodosLosLotes / CantidadPrincipalTodosLosLotes) : 0) : detallesInventarioActuak.First().precio_unitario;
            foreach (var detalleMovimiento in detallesMovimientoDeConcepto)
            {
                var detalleInventarioactual = detallesInventarioActuak.Single(d => d.lote == detalleMovimiento.lote);
                //Si el lote ingresado no existe en detalle de transaccion del inventario. Este caso se daría en una compra o cualquier otra operacion donde ingresa mercaderia.
                if (detalleInventarioactual == null)
                {
                    if (debeAumentarExistencia || !esSalidaSujetaADisponibilidadDeStock)
                    {
                        ResolverDetalleNuevoEnInventarioActual(detalleMovimiento, detallesInventarioActual_Nuevos, debeAumentarExistencia);
                    }
                    else throw new LogicaException("Producto y lote no existen en el inventario");
                }
                else
                {
                    ResolverDetalleExistenteEnInventarioActual(detalleMovimiento, detalleInventarioactual, debeAumentarExistencia, CostoUnitarioTodosLosLotes, esSalidaSujetaADisponibilidadDeStock);
                }
            }
        }
        /// <summary>
        /// Modifica los detalles existentes del inventario sumando o restando las cantidades, según el tipo de operación
        /// En caso el inventario, no tenga detalles con los mismos conceptos que los detalles de la operación, se crearán nuevos detalles para el inventario
        /// Se entiende que los detalles de movimientos de bienes corresponden a un solo almacen.
        /// </summary>
        /// <param name="ordenDeOperacion"></param>
        /// <param name="detallesMovimientoBienesAgrupadosPorLote"></param>
        /// <param name="detallesInventarioactual_Nuevos"></param>
        /// <param name="detallesInventarioactual_Existentes"></param>
        /// <param name="idTransaccionInventarioActual"></param>
        public AfectacionInventarioActual ResolverInventarioActual(List<Detalle_transaccion> detallesMovimientosBienes, long idTransaccionInventarioActual, TipoMovimientoOperacion tipoMovimiento, bool esSalidaSujetaADisponibilidadDeStock, int idTipoTransaccion)
        {
            var grupoDetallesMovimientoBienesPorAlmacen = Detalle_transaccion.Clone(detallesMovimientosBienes);
            //Verificar que no aplique la gestion de lotes, luego unificar los detalles de la operacion
            if (!AplicacionSettings.Default.PermitirGestionDeLotes)
            {
                grupoDetallesMovimientoBienesPorAlmacen = grupoDetallesMovimientoBienesPorAlmacen.Select(dmb => { dmb.lote = null; return dmb; }).ToList();
            }
            //Obtener los detalles del inventario actual para los conceptos de la operación  
            List<Detalle_transaccion> detallesInventarioActual_Existentes = transaccionRepositorio.ObtenerDetallesTransaccion(idTransaccionInventarioActual, grupoDetallesMovimientoBienesPorAlmacen.Select(dma => dma.id_concepto_negocio).ToArray()).ToList();
            //todo: aqui hay riesgos de duplicar registro en caso se esté realizando mas de 1 intento debido a problemas de concurrencia. tratar de resolver la concurrencia en el catch. o en todo caso asegurar que no se vuelvan a crear objetos que resulten en registros duplicados en bd, por ejemplo valores de caracteristicas propias.
            List<Detalle_transaccion> detallesInventarioActual_Nuevos = new List<Detalle_transaccion>();
            bool debeAumentarExistencia = tipoMovimiento == TipoMovimientoOperacion.Entrada;
            var detallesMovimientoBienesPorConceptoYLote = UnificarPorConceptoYLote(grupoDetallesMovimientoBienesPorAlmacen);
            var detallesMovimientosBienesPorConcepto = detallesMovimientoBienesPorConceptoYLote.GroupBy(dmb => dmb.id_concepto_negocio);

            //por cada detalle de movimiento, afectar el inventario según corresponda resolviendo valores de características propias
            foreach (var detallesPorConcepto in detallesMovimientosBienesPorConcepto)
            {
                ResolverDetallesDeInventarioActual(detallesPorConcepto, detallesInventarioActual_Existentes, detallesInventarioActual_Nuevos, idTransaccionInventarioActual, debeAumentarExistencia, esSalidaSujetaADisponibilidadDeStock, idTipoTransaccion);
            }
            return new AfectacionInventarioActual() { Detalles_nuevos = detallesInventarioActual_Nuevos, Detalles_modificados = detallesInventarioActual_Existentes };
        }

        public AfectacionInventarioActual ResolverInventarios(List<Transaccion> movimientosDeBienes, UserProfileSessionData sesionDeUsuario)
        {
            List<Detalle_transaccion> detallesInventariosactuals_Nuevos = new List<Detalle_transaccion>();
            List<Detalle_transaccion> detallesInventariosactuals_Existentes = new List<Detalle_transaccion>();
            if (movimientosDeBienes != null)
            {
                //Agrupar por almacen ver los movimientos que se hacen por almacenes y asi poder manejarlos mejor
                var gruposDetallesMovimientosBienesPorAlmacen = movimientosDeBienes.SelectMany(mb => mb.Detalle_transaccion).GroupBy(dt => new { idAlmacen = dt.Transaccion.id_actor_negocio_interno });
                foreach (var grupoDetallesMovimientoBienesPorAlmacen in gruposDetallesMovimientosBienesPorAlmacen)
                {
                    int idTipoTransaccion = grupoDetallesMovimientoBienesPorAlmacen.Select(d => d.Transaccion.id_tipo_transaccion).Distinct().SingleOrDefault();
                    TipoMovimientoOperacion tipoMovimiento = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.Contains(idTipoTransaccion) ? TipoMovimientoOperacion.Entrada : Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas.Contains(idTipoTransaccion) ? TipoMovimientoOperacion.Salida : throw new LogicaException("El tipo de transaccion de movimiento de bienes en almacen es incorrecta");

                    //si es salida, averiguar si en el almacen debe restringirse salidas en base a disponibilidad de stock
                    var esSalidaSujetaADisponibilidadDeStock = tipoMovimiento == TipoMovimientoOperacion.Salida && AlmacenConSalidasSujetasADisponibilidadDeStock(grupoDetallesMovimientoBienesPorAlmacen.Key.idAlmacen);
                    var detallesInventario = ResolverInventarioActual(grupoDetallesMovimientoBienesPorAlmacen.ToList(), sesionDeUsuario.ObtenerIdInventarioActual(grupoDetallesMovimientoBienesPorAlmacen.Key.idAlmacen), tipoMovimiento, esSalidaSujetaADisponibilidadDeStock, idTipoTransaccion);
                    detallesInventariosactuals_Nuevos.AddRange(detallesInventario.Detalles_nuevos);
                    detallesInventariosactuals_Existentes.AddRange(detallesInventario.Detalles_modificados);
                }
            }
            return new AfectacionInventarioActual() { Detalles_nuevos = detallesInventariosactuals_Nuevos, Detalles_modificados = detallesInventariosactuals_Existentes };
        }
        public bool AlmacenConSalidasSujetasADisponibilidadDeStock(int idAlmacen)
        {
            string jsonCentroAtencion = actor_Datos.ObtenerExtensionJsonDeActorNegocio(idAlmacen);
            bool salidasSinStock = string.IsNullOrEmpty(jsonCentroAtencion) ? false : JsonConvert.DeserializeObject<JsonCentroDeAtencion>(jsonCentroAtencion).salidabienessinstock;
            return !salidasSinStock;
        }

        public void RegenerarNumeracionComprobantePropioAutonumerable(Comprobante comprobante, Serie_comprobante serie)
        {
            comprobante.numero = serie.proximo_numero;
            serie.proximo_numero++;
            transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
        }

        public void RegenerarNumeracionComprobantesPropiosAutonumerables(List<Comprobante> comprobantes, Serie_comprobante serie)
        {
            foreach (var comprobante in comprobantes)
            {
                comprobante.numero = serie.proximo_numero;
                serie.proximo_numero++;
            }
            //todo> evitar aqui marcar la serie... tratar de enviarla a modificar desde el metodo que guarda las transacciones, de esa manera, siempre se tendr'a el control del manejo de datos.
            transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
        }

        #region AFECCION DE EXISTENCIAS
        /// <summary>
        /// Resuelve el inventario, guarda la operacion y resuelve excepciones de concurrencia luego de guardar
        /// </summary>
        /// <param name="operacion"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <returns></returns>
        public OperationResult AfectarInventarioFisicoYGuardarOperacion(OperacionIntegrada operacion, UserProfileSessionData sesionDeUsuario)
        {
            OperationResult result = null;
            AfectacionInventarioActual detallesInventariosactuals = null;
            bool hayProblemaDeConcurrencia;
            do
            {
                hayProblemaDeConcurrencia = false;
                try
                {
                    //conseguir detalles inventario
                    detallesInventariosactuals = ResolverInventarios(operacion.MovimientosBienes, sesionDeUsuario);
                    result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones()
                    {
                        //Transacciones_Crear = new List<Transaccion>(operacion.MovimientosBienes) { operacion.Operacion, operacion.OrdenDeOperacion, operacion.MovimientoEconomico },
                        Transacciones_Crear = operacion.OperacionCreacion == null ? new List<Transaccion>() { operacion.Operacion } : new List<Transaccion>() { operacion.Operacion, operacion.OperacionCreacion },
                        Transacciones_Modificar = operacion.OperacionOrigen == null ? new List<Transaccion>() : new List<Transaccion>() { operacion.OperacionOrigen },
                        DetallesTransaccion_Crear = detallesInventariosactuals.Detalles_nuevos,
                        DetallesTransaccion_Modificar = detallesInventariosactuals.Detalles_modificados,
                        Actores_Negocio_Modificar = operacion.ActoresNegocioModificados,
                        EstadosTransaccion_Crear = operacion.NuevosEstadosTransaccion
                    });
                    result.information = new Operacion(operacion.OrdenDeOperacion).Id;
                    result.objeto = new OrdenDeVenta(operacion.OrdenDeOperacion);
                }
                catch (SerieComprobanteException e)
                {
                    ResolverProblemaDeConcurrenciaSerieComprobante(operacion.Operacion, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (DetalleTransaccionException e)
                {
                    ResolverPosiblesProblemasDeConcurrenciaInventario(detallesInventariosactuals.Detalles_modificados, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (Exception e)
                {
                    throw new LogicaException("Error al validar y afectar existencias e intentar guardar la operacion", e);
                }
            } while (hayProblemaDeConcurrencia);
            return result;
        }




        public OperationResult AfectarInventarioFisicoYGuardarOperacion(OperacionModificatoria operacion, UserProfileSessionData sesionDeUsuario)
        {
            OperationResult result = null;
            AfectacionInventarioActual detallesInventariosactuals = null;
            bool hayProblemaDeConcurrencia;//por defecto una variable bool es false;
            do
            {
                hayProblemaDeConcurrencia = false;
                try
                {
                    //conseguir detalles inventario
                    detallesInventariosactuals = ResolverInventarios(operacion.MovimientosBienes, sesionDeUsuario);
                    List<Transaccion> transaccionesACrear = new List<Transaccion>();
                    if (operacion.MovimientosBienes != null) transaccionesACrear.AddRange(operacion.MovimientosBienes);
                    if (operacion.Operacion != null) transaccionesACrear.Add(operacion.Operacion);
                    if (operacion.OrdenDeOperacion != null) transaccionesACrear.Add(operacion.OrdenDeOperacion);
                    if (operacion.MovimientoEconomico != null) transaccionesACrear.Add(operacion.MovimientoEconomico);
                    List<Transaccion> transaccionesAModificar = new List<Transaccion>();
                    if (operacion.OperacionOrigen != null) transaccionesAModificar.Add(operacion.OperacionOrigen);
                    if (operacion.TransaccionesModificadas != null) transaccionesAModificar.AddRange(operacion.TransaccionesModificadas);
                    if (operacion.DetallesTransaccionModificados != null) detallesInventariosactuals.Detalles_modificados.AddRange(operacion.DetallesTransaccionModificados);
                    result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones()
                    {
                        Transacciones_Crear = transaccionesACrear,//new List<Transaccion>(operacion.MovimientosBienes) { operacion.Operacion, operacion.OrdenDeOperacion, operacion.MovimientoEconomico },
                        EstadosTransaccion_Crear = operacion.NuevosEstadosTransaccionesModificadas,
                        EstadosCuota_Crear = operacion.NuevosEstadosParaCuotasTransaccionesModificadas,
                        Transacciones_Modificar = transaccionesAModificar.Count() > 0 ? transaccionesAModificar : null,
                        DetallesTransaccion_Crear = detallesInventariosactuals.Detalles_nuevos,
                        DetallesTransaccion_Modificar = detallesInventariosactuals.Detalles_modificados,
                    });
                    result.information = new Operacion(operacion.Operacion).ObtenerOrdenDeOperacion().Id;
                }
                catch (SerieComprobanteException e)
                {
                    ResolverProblemaDeConcurrenciaSerieComprobante(operacion.Operacion, e);
                    hayProblemaDeConcurrencia = true;

                }
                catch (DetalleTransaccionException e)
                {
                    ResolverPosiblesProblemasDeConcurrenciaInventario(detallesInventariosactuals.Detalles_modificados, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (Exception e)
                {
                    throw new LogicaException("Error al validar y afectar existencias e intentar guardar la operacion", e);
                }
            } while (hayProblemaDeConcurrencia);
            return result;
        }




        private OperationResult AfectarInventarioFisicoYGuardarOperaciones(List<OperacionIntegrada> operaciones, UserProfileSessionData sesionDeUsuario)
        {
            OperationResult result = null;
            bool hayProblemaDeConcurrencia;//por defecto una variable bool es false;
            AfectacionInventarioActual detallesInventariosactuals = null;
            var actoresModificados = operaciones.SelectMany(o => o.ActoresNegocioModificados).ToList();
            //var transaccionesModificadas = operaciones.SelectMany(o => o.ActoresNegocioModificados).ToList();
            List<Actor_negocio> actoresActualizar = new List<Actor_negocio>();
            actoresModificados.ForEach(am => { if (am != null && !actoresActualizar.Contains(am)) actoresActualizar.Add(am); });

            var operacionesContenedoras = operaciones.Select(o => o.Operacion).ToList();
            do
            {
                hayProblemaDeConcurrencia = false;
                try
                {
                    var movimientosDeBienes = operaciones.SelectMany(o => o.MovimientosBienes).ToList();
                    detallesInventariosactuals = ResolverInventarios(movimientosDeBienes, sesionDeUsuario);
                    result = transaccionRepositorio.CrearTransaccionesCrearEstadosTransaccionCrearEstadosCuotaCrearActualizarDetallesTransaccion(operacionesContenedoras, null, null, detallesInventariosactuals.Detalles_nuevos, detallesInventariosactuals.Detalles_modificados, actoresActualizar);
                    if (operaciones.Count > 0)
                    {
                        //todo: analizar que se debe devolver realmente.
                        result.information = new Operacion(operacionesContenedoras.First()).ObtenerOrdenDeOperacion().Id;
                    }
                }
                catch (SerieComprobanteException e)
                {
                    //todo: resolver regeneracionMasiva
                    ResolverProblemaDeConcurrenciaSerieComprobante(operaciones.Select(o => o.Operacion).ToList(), e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (DetalleTransaccionException e)
                {
                    ResolverPosiblesProblemasDeConcurrenciaInventario(detallesInventariosactuals.Detalles_modificados, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (Exception e)
                {
                    throw new LogicaException("Error al validar y afectar existencias e intentar guardar la operacion", e);
                }
            } while (hayProblemaDeConcurrencia);
            return result;
        }

        private OperationResult AfectarInventarioFisicoYGuardarOperacion(OperacionVentaCobroCarteraCliente operacionVentaCobroCarteraCliente, UserProfileSessionData sesionDeUsuario)
        {
            OperationResult result = null;
            AfectacionInventarioActual detallesInventariosactuals = null;
            bool hayProblemaDeConcurrencia;
            do
            {
                hayProblemaDeConcurrencia = false;
                try
                {
                    //conseguir detalles inventario
                    detallesInventariosactuals = ResolverInventarios(operacionVentaCobroCarteraCliente.Ventas.SelectMany(o => o.MovimientosBienes).ToList(), sesionDeUsuario);
                    List<Transaccion> transaccionesACrear = new List<Transaccion>() { operacionVentaCobroCarteraCliente.OperacionWrapper };
                    List<Transaccion> transaccionesAModificar = new List<Transaccion>();
                    foreach (var operacionIntegrada in operacionVentaCobroCarteraCliente.Ventas)
                    {
                        if (operacionIntegrada.Operacion != null) transaccionesACrear.Add(operacionIntegrada.Operacion);
                        if (operacionIntegrada.OrdenDeOperacion != null) transaccionesACrear.Add(operacionIntegrada.OrdenDeOperacion);
                        if (operacionIntegrada.MovimientoEconomico != null) transaccionesACrear.Add(operacionIntegrada.MovimientoEconomico);
                        if (operacionIntegrada.MovimientosBienes != null) transaccionesACrear.AddRange(operacionIntegrada.MovimientosBienes);
                        if (operacionIntegrada.OperacionOrigen != null) transaccionesAModificar.Add(operacionIntegrada.OperacionOrigen);
                    }
                    result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones()
                    {
                        Transacciones_Crear = transaccionesACrear,
                        Transacciones_Modificar = transaccionesAModificar,
                        DetallesTransaccion_Crear = detallesInventariosactuals.Detalles_nuevos,
                        DetallesTransaccion_Modificar = detallesInventariosactuals.Detalles_modificados,
                    });
                    result.information = operacionVentaCobroCarteraCliente.OperacionWrapper.id;
                    result.objeto = operacionVentaCobroCarteraCliente;
                }
                catch (SerieComprobanteException e)
                {
                    //todo: resolver regeneracionMasiva
                    ResolverProblemaDeConcurrenciaSerieComprobante(operacionVentaCobroCarteraCliente.Ventas.Select(oi => oi.Operacion).ToList(), e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (DetalleTransaccionException e)
                {
                    ResolverPosiblesProblemasDeConcurrenciaInventario(detallesInventariosactuals.Detalles_modificados, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (Exception e)
                {
                    throw new LogicaException("Error al validar y afectar existencias e intentar guardar la operacion", e);
                }
            } while (hayProblemaDeConcurrencia);
            return result;
        }

        /// <summary>
        /// Resuelve el inventario, guarda la operacion y resuelve excepciones de concurrencia luego de guardar
        /// </summary>
        /// <param name="operacion"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <returns></returns>
        public OperationResult AfectarInventarioFisicoYGuardarInventarioEstadosTransacciones(Transaccion movimientoBienes, List<Estado_transaccion> estadosTransacciones, UserProfileSessionData sesionDeUsuario)
        {
            OperationResult result = null;
            AfectacionInventarioActual detallesInventariosactuals = null;
            bool hayProblemaDeConcurrencia;
            do
            {
                hayProblemaDeConcurrencia = false;
                try
                {
                    detallesInventariosactuals = ResolverInventarios(new List<Transaccion>() { movimientoBienes }, sesionDeUsuario);
                    detallesInventariosactuals.Detalles_modificados.AddRange(ResolverInventariosHistoricos(movimientoBienes, sesionDeUsuario));
                    result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones()
                    {
                        Transacciones_Crear = new List<Transaccion>(),
                        Transacciones_Modificar = new List<Transaccion>(),
                        DetallesTransaccion_Crear = detallesInventariosactuals.Detalles_nuevos,
                        DetallesTransaccion_Modificar = detallesInventariosactuals.Detalles_modificados,
                        EstadosTransaccion_Crear = estadosTransacciones,
                    });
                    result.information = new Operacion(movimientoBienes).Id;
                    result.objeto = new OrdenDeVenta(movimientoBienes);
                }
                catch (DetalleTransaccionException e)
                {
                    ResolverPosiblesProblemasDeConcurrenciaInventario(detallesInventariosactuals.Detalles_modificados, e);
                    hayProblemaDeConcurrencia = true;
                }
                catch (Exception e)
                {
                    throw new LogicaException("Error al validar y afectar existencias e intentar guardar el inventario y estado transaccion", e);
                }
            } while (hayProblemaDeConcurrencia);
            return result;
        }

        public List<Detalle_transaccion> ResolverInventariosHistoricos(Transaccion movimientoDeBienes, UserProfileSessionData sesionDeUsuario)
        {
            List<Detalle_transaccion> detallesInventariosHistoricos_Existentes = new List<Detalle_transaccion>();
            List<InventarioHistorico> inventariosHistoricos = inventarioHistorico_Datos.ObtenerInventariosHistoricosDespuesDe(movimientoDeBienes.id_actor_negocio_interno, movimientoDeBienes.fecha_inicio).ToList();
            TipoMovimientoOperacion tipoMovimiento = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.Contains(movimientoDeBienes.id_tipo_transaccion) ? TipoMovimientoOperacion.Entrada : Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas.Contains(movimientoDeBienes.id_tipo_transaccion) ? TipoMovimientoOperacion.Salida : throw new LogicaException("El tipo de transaccion de movimiento de bienes en almacen es incorrecta");

            var movimientoDeBienesPorConceptoLote = movimientoDeBienes.Detalle_transaccion.Select(dt => new { IdConcepto = dt.id_concepto_negocio, Lote = dt.lote });
            var detallesInventariosHistoricos = inventariosHistoricos.Where(ih => movimientoDeBienesPorConceptoLote.Contains(new { IdConcepto = ih.IdConcepto, Lote = ih.Lote }));

            foreach (var detalleInventarioHistorico in detallesInventariosHistoricos)
            {
                var cantidad = movimientoDeBienes.Detalle_transaccion.Single(dt => dt.id_concepto_negocio == detalleInventarioHistorico.IdConcepto && dt.lote == detalleInventarioHistorico.Lote).cantidad;
                detalleInventarioHistorico.Cantidad = detalleInventarioHistorico.Cantidad + (tipoMovimiento == TipoMovimientoOperacion.Entrada ? +cantidad : -cantidad);
                detallesInventariosHistoricos_Existentes.Add(detalleInventarioHistorico.ToDetalleTransaccion());
            }
            return detallesInventariosHistoricos_Existentes;
        }

        #endregion



        #region REPORTE DE SALIDAS DE ALCOHOL

        public List<Reporte_Concepto_Basico> ObtenerReporteDeSalidasDeAlcohol(DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosDeVenta)
        {
            try
            {
                int idFamiliaAlcohol = MaestroSettings.Default.IdDetalleMaestroConceptoBasicoAlcohol;
                // Que deberia usar tipos de transaccion de salida de mercaderia o ordenes de venta
                // Si mando id de punto de venta tengo que pasarle tipos de transaccion ordenes de venta
                // Si mando id de almacen tengo que pasarle tipos de transacciones de salida de mercaderia
                var reporteConceptos = transaccionRepositorio.ObtenerReportePorConceptoBasico(fechaDesde, fechaHasta, idFamiliaAlcohol, new int[] { TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta }, new int[] { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura }, idsPuntosDeVenta).ToList();

                return reporteConceptos.OrderByDescending(d => d.FechaInicio).ToList();

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener reporte por concepto básico", e);
            }
        }

        #endregion





        #region FECHAS

        public List<string> ObtenerFechaIncioyFinParaReporteAlmacen()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            string fechaDesde = fechaActual.AddDays(-7).Date.ToString("dd/MM/yyyy");
            string fechaHasta = fechaActual.Date.ToString("dd/MM/yyyy");

            return new List<string> { fechaDesde, fechaHasta };
        }

        #endregion

        #region OBTENCION DE DATOS

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaMovimientoMercaderia(int idCentroAtencion)
        {
            try
            {
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento)).ToList();
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e) { throw e; }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAlmacen(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        public List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteGuiaDeRemision(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        public List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteNotaDeAlmacen(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        public List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteOrdenDeAlmacen(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteOrdenDeAlmacen);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Obtiene las ordenes de ingreso de movimiento interno de mercaderia 
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        public List<OrdenDeTrasladoInterno> ObtenerOrdenesIngresoInternoMercaderia(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsTransaccionesOrdenesIngreso = { TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno };
                return OrdenDeTrasladoInterno.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(idsTransaccionesOrdenesIngreso, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCentroAtencion, fechaDesde, fechaHasta).OrderByDescending(oti => oti.fecha_inicio).ThenByDescending(oc => oc.id).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtine las ordenes de salida de movimiento interno de mercaderia
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        public List<OrdenDeTrasladoInterno> ObtenerOrdenesSalidaInternoMercaderia(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsTransaccionesOrdenesSalida = { TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno };
                return OrdenDeTrasladoInterno.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(idsTransaccionesOrdenesSalida, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCentroAtencion, fechaDesde, fechaHasta).OrderByDescending(oti => oti.fecha_inicio).ThenByDescending(oc => oc.id).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Orden_Recibir_Entregar> ObtenerOrdenesDeAlmacenPorRecibir(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                bool porRecibir = true;
                return transaccionRepositorio.ObtenerOrdenesPorRecibirOPorEntregarDeAlmacen(porRecibir, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las ordenes de almacen por recibir", e);
            }
        }

        public List<Orden_Recibir_Entregar> ObtenerOrdenesDeAlmacenPorEntregar(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                bool porRecibir = true;
                return transaccionRepositorio.ObtenerOrdenesPorRecibirOPorEntregarDeAlmacen(porRecibir, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las ordenes de almacen por entregar", e);
            }
        }

        public List<Entrada_Salida_Almacen> ObtenerEntradasDeAlmacen(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                bool esEntrada = true;
                return transaccionRepositorio.ObtenerEntradasOSalidasDeAlmacen(esEntrada, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las entradas de almacen", e);
            }
        }

        public List<Entrada_Salida_Almacen> ObtenerSalidasDeAlmacen(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                bool esEntrada = false;
                return transaccionRepositorio.ObtenerEntradasOSalidasDeAlmacen(esEntrada, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las entradas de almacen", e);
            }
        }

        public TrasladoInterno ObtenerMovimiento(long idMovimiento)
        {
            try
            {
                return (new TrasladoInterno(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idMovimiento)));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OrdenDeTrasladoInterno ObtenerOrdenMovimiento(long idOrdenMovimiento)
        {
            try
            {
                return (new OrdenDeTrasladoInterno(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenMovimiento)));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OrdenDeMovimientoDeAlmacen ObtenerOrdenDeMovimientoDeAlmacen(long idOrdenDeAlmacen)
        {
            try
            {
                return (new OrdenDeMovimientoDeAlmacen(transaccionRepositorio.ObtenerTransaccionInclusiveActorDeNegocio1Transaccion11(idOrdenDeAlmacen)));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public decimal ObtenerStockDeProducto(int idProducto, int idCentroAtencion)
        {
            try
            {
                return transaccionRepositorio.ObtenerDetalleTransaccion(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idProducto, null).cantidad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region GUIA REMISION

        public List<MovimientoDeAlmacen> ObtenerGuiasRemision(int[] idsCentroAtencion, DateTime desde, DateTime hasta)
        {
            try
            {
                List<int> idsTiposTransacciones = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas.ToList();
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.ToList());
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionGuiasDeRemision.ToList());
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente;
                List<MovimientoDeAlmacen> guiasRemision = MovimientoDeAlmacen.Convertir(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(idsTiposTransacciones.ToArray(), idsCentroAtencion, idTipoComprobante, desde, hasta).ToList()).OrderByDescending(g => g.FechaEmision).ToList();
                return guiasRemision;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las guias de remision", e);
            }
        }

        public MovimientoDeAlmacen ObtenerGuiaRemision(long idGuiaRemision)
        {
            try
            {
                MovimientoDeAlmacen guiaRemision = new MovimientoDeAlmacen(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idGuiaRemision));
                return guiaRemision;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener la guia de remision", e);
            }
        }

        public OperationResult GuardarGuiaRemision(int idTercero, bool esPropio, int idTipoComprobante, int idSerieComprobante, string serieDeComprobante, int numeroDeComprobante, DateTime fechaInicioTransporte, int idTransportista, string placaTransporte, int idConductor, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string descripcionMotivo, decimal pesoBrutoTotal, int numeroBultos, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string comprobanteReferencia, string observacion, List<Detalle_transaccion> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1;
                DateTime fechaActual = DateTimeUtil.FechaActual();
                observacion = (String.IsNullOrEmpty(observacion) || String.IsNullOrWhiteSpace(observacion)) ? "NINGUNO" : observacion;
                if (pesoBrutoTotal == 0) throw new LogicaException("Error, el peso bruto total tiene que ser diferente de 0");
                if (numeroBultos == 0) throw new LogicaException("Error, el numero de bultos tiene que ser diferente de 0");
                if (idModalidadTransaporte == MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePrivado)
                {
                    placaTransporte = placaTransporte.Replace("-", "");
                    numeroLicenciaTransporte = numeroLicenciaTransporte.Replace("-", "");
                    if (placaTransporte.Length < 6 || placaTransporte.Length > 8) throw new LogicaException("Error, la placa es incorrecta, no ingresar guiones");
                    if (numeroLicenciaTransporte.Length < 9 || numeroLicenciaTransporte.Length > 10) throw new LogicaException("Error, el numero de licencia es incorrecta, no ingresar guiones");
                }
                //Obtenemos el id del tipo de transaccion de guia de remision
                int idTipoTransaccionGuiaRemision = Diccionario.MapeoMotivoTrasladoGuiaRemisionVsTipoTransaccion.Single(m => m.Key == idMotivoTransaporte).Value;
                //Verificamos el valor de la descripcion de motivo
                descripcionMotivo = (idMotivoTransaporte == MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoOtros) ? descripcionMotivo : null;
                //Validar la accion a realizar
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, idTipoTransaccionGuiaRemision, idUnidadNegocio);
                //Obtener el codigo para la venta 
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == idTipoTransaccionGuiaRemision).Value, idTipoTransaccionGuiaRemision);
                //Generar la transaccion de la guia de remision
                Transaccion guiaRemision = new Transaccion(codigo, null, fechaActual, idTipoTransaccionGuiaRemision, idUnidadNegocio, true, fechaActual, fechaActual, observacion, fechaActual, sesionUsuario.Empleado.Id, 0, sesionUsuario.IdCentroDeAtencionSeleccionado, idMoneda, tipoDeCambio, null, idTercero)
                {
                    //Agregar el comprobante a la transaccion guia de remision
                    Comprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, serieDeComprobante, numeroDeComprobante)
                };
                //Agregar los detalles a la salida de mercaderia
                guiaRemision.AgregarDetalles(detalles);
                //Agregar los parametros de transaccion 
                guiaRemision = ResolverParametrosTransaccion(guiaRemision, fechaInicioTransporte, idTransportista, placaTransporte, idConductor, numeroLicenciaTransporte, idModalidadTransaporte, idMotivoTransaporte, descripcionMotivo, pesoBrutoTotal, numeroBultos, direccionOrigenTraslado, idUbigeoOrigenTraslado, direccionDestinoTraslado, idUbigeoDestinoTraslado, comprobanteReferencia);
                //Agregar el estado por defecto a la guia de remision
                Estado_transaccion estadoDeGuiaRemision = new Estado_transaccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado confirmado de guia de remision");
                guiaRemision.Estado_transaccion.Add(estadoDeGuiaRemision);
                //Guardar movimiento con todo actualizacion de inventario actual
                return transaccionRepositorio.CrearTransaccion(guiaRemision);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar la guia de remision", e);
            }
        }
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaGuiaRemision(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorOtrosMotivos);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobante para guia de remision", e);
            }
        }

        public List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteGuiaRemisionNotaAlmacen(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                int[] idsTipoComprobante = new int[] { MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente, MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna };
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(idsTipoComprobante).GroupBy(r => new { r.id_tipo_comprobante, r.es_propio }).Select(r => r.FirstOrDefault())
                    .ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobante para guia de remision", e);
            }
        }
        public OperationResult InvalidarGuiaRemision(long idGuiaRemision, int idEmpleado, int idCentroAtencion, string observacion)
        {
            try
            {
                MovimientoDeAlmacen guiaRemision = new MovimientoDeAlmacen(transaccionRepositorio.ObtenerTransaccion(idGuiaRemision));
                //Obtener datos para la invalidacion
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Validar la accion a realizar
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar, guiaRemision.IdTipoTransaccion, idUnidadNegocio);
                //Crear el estado invalidado para la guia de remision
                Estado_transaccion estadoDeLaOrdenDeGasto = new Estado_transaccion(idGuiaRemision, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaRegistro, observacion);

                return transaccionRepositorio.CrearTransaccionYCrearEstadoTransaccion(null, estadoDeLaOrdenDeGasto);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el gasto", e);
            }
        }
        #endregion

    }
}

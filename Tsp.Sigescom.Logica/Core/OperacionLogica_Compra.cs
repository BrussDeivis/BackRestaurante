using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        #region  GENERADORES DE COMPRA

        //Generar transaccion y generar el comprobante dentro de esta
        private Transaccion GenerarTransaccion(int idEmpleado, int idCentroAtencion, int idTercero, int idTipoTransaccion, bool esPropio, int idSerieComprobante, int idTipoComprobante, string numeroSerieDeComprobante, int numeroDeComprobante, DateTime fechaOperacion, DateTime fechaRegistro, decimal importeTotal, string observacion)
        {
            try
            {
                //Obtener los datos requeridos para la generacion de la trnasaccion
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                decimal tipoDeCambio = 1; //transaccionRepositorio.obtenerTipoDeCambio(fechaRegistro).valorVenta;
                //Obtener la operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Generar el codigo de la transaccion
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == idTipoTransaccion).Value, idTipoTransaccion);
                //Generar el comprobante de la transaccion
                Comprobante comprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Crear la transaccion compra
                Transaccion transaccion = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaOperacion, fechaOperacion, observacion, fechaOperacion, idEmpleado, importeTotal, idCentroAtencion, idMoneda, tipoDeCambio, null, idTercero)
                {
                    //Agregar el comprobante generado
                    Comprobante = comprobante
                };
                return transaccion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar la transaccion", e);
            }
        }

        //Generar una transaccion orden de una transaccion la wrapper operacion
        private Transaccion GenerarOrdenDeTransaccion(Transaccion operacion, int idEmpleado, int idCentroAtencion, int idProveedor, int idOrdenTransaccion, DateTime fechaOperacion, DateTime fechaRegistro, ModoOperacionEnum tipoDeCompra, TipoOperacionCompra tipoDeOperacionDeCompra, ModoPago modoDePago, int estadoTransaccion, string observacionEstadoTransaccion, string observacion, List<Detalle_transaccion> detalles, bool ingresoTotalOrden)
        {
            decimal descuentoGlobal = 0, descuentoPorItem = 0, anticipo = 0, gravada = 0, exonerada = 0, inafecta = 0, gratuita = 0, igv = 0, isc = 0, icbper = 0, otrosCargos = 0, otrosTributos = 0;
            if (tipoDeOperacionDeCompra == TipoOperacionCompra.NoGravada)
            {
                gravada = detalles.Sum(d => d.total - d.igv);
                igv = detalles.Sum(d => d.igv);
            }
            else
            {
                exonerada = detalles.Sum(d => d.total);
            }
            //Crear la transaccion orden de operacion
            Transaccion ordenDeOperacion = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == idOrdenTransaccion).Value, idOrdenTransaccion), null, fechaRegistro, idOrdenTransaccion, operacion.id_unidad_negocio, true, fechaOperacion, fechaOperacion, observacion, fechaOperacion, idEmpleado, operacion.importe_total, idCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, idProveedor, descuentoGlobal, descuentoPorItem, anticipo, gravada, exonerada, inafecta, gratuita, igv, isc, icbper, otrosCargos, otrosTributos)
            {
                //Agregar el comprobante de la operacion
                Comprobante = operacion.Comprobante
            };
            //Agregar los detalles de transaccion a la orden de operacion
            ordenDeOperacion.AgregarDetalles(detalles);
            //Agregar el estado por defecto a la orden de la operacion
            Estado_transaccion estadoDeLaOrdenDeCompra = new Estado_transaccion(idEmpleado, estadoTransaccion, fechaRegistro, observacionEstadoTransaccion);
            ordenDeOperacion.Estado_transaccion.Add(estadoDeLaOrdenDeCompra);
            //Agregar los parametros de la orden de operacion
            ordenDeOperacion.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)modoDePago).ToString()));
            ordenDeOperacion.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoOperacionCompra, ((int)tipoDeCompra).ToString()));
            ordenDeOperacion.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, ((int)tipoDeOperacionDeCompra).ToString()));
            if (tipoDeCompra == ModoOperacionEnum.Corporativa)
            {
                ordenDeOperacion.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroEstadoGeneracionOrdenDeAlmacen, ingresoTotalOrden ? ((int)IngresoTotal.Si).ToString() : ((int)IngresoTotal.No).ToString()));
            }
            return ordenDeOperacion;
        }

        private void ValidarAccionYRolPuntoDeCompraEnCompra(int idComprador, int idAccionARealizar, int idTipoTransaccion, int idPuntoDeCompra)
        {
            int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            //Validar la accion a realizar
            permisos_Logica.ValidarAccion(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idUnidadNegocio);
            //Validar los roles de los centro de atencion
            ValidarRolesPuntoDeCompra(idPuntoDeCompra);
        }

        private void ValidarAccionYRolesPuntoDeCompraCajaEnCompra(int idComprador, int idAccionARealizar, int idTipoTransaccion, int idPuntoDeCompra, int idCaja)
        {
            int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            //Validar la accion a realizar
            permisos_Logica.ValidarAccion(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idUnidadNegocio);
            //Validar los roles de los centro de atencion
            ValidarRolesPuntoDeCompraCaja(idPuntoDeCompra, idCaja);
        }

        private void ValidarAccionYRolesPuntoDeCompraAlmacenEnCompra(int idComprador, int idAccionARealizar, int idTipoTransaccion, int idPuntoDeCompra, int idAlmacen)
        {
            int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            //Validar la accion a realizar
            permisos_Logica.ValidarAccion(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idUnidadNegocio);
            //Validar los roles de los centro de atencion
            ValidarRolesPuntoDeCompraAlmacen(idPuntoDeCompra, idAlmacen);
        }

        private void ValidarAccionYRolesPuntoDeCompraCajaAlmacenEnCompra(int idComprador, int idAccionARealizar, int idTipoTransaccion, int idPuntoDeCompra, int idCaja, int idAlmacen)
        {
            int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            //Validar la accion a realizar
            permisos_Logica.ValidarAccion(idComprador, idAccionARealizar, idTipoTransaccion, idUnidadNegocio);
            //Validar los roles de los centro de atencion
            ValidarRolesPuntoDeCompraCajaAlmacen(idPuntoDeCompra, idCaja, idAlmacen);
        }

        public List<DetalleDeOperacion> CalcularDetallesDeCompra(List<DetalleDeOperacion> detalles, int idTipoComprobante, int tipoCompra, decimal flete)
        {
            //Agregar el flete como un detalle de operacion si es mayor a 0  
            if (flete > 0)
            {
                detalles.Add(new DetalleDeOperacion(ConceptoSettings.Default.IdConceptoNegocioFlete, 1, flete, flete, 0, 0, 0, null, null, null, null, false, "110", null));
            }
            foreach (var item in detalles)
            {
                //Verificar que la cantidad de los detalles de operacion no sean menores o iguales a 0
                if (item.Cantidad <= 0)
                    throw new LogicaException("No es posible realizar una venta con cantidad 0 en alguno de sus detalles");
                //Calcular el precio unitario del detalle
                item.PrecioUnitario = item.Importe / item.Cantidad;
                //Calcular el igv si el tipo de compra es diferente a la no gravada
                if (tipoCompra != (int)TipoOperacionCompra.NoGravada)
                    item.Igv = item.Importe - (item.Importe / ((decimal)1 + TransaccionSettings.Default.TasaIGV));
            }
            return detalles;
        }

        //Generar una transaccion orden de almacen para e ingreso de las mercaderias en compra coorporativa
        private void GenerarOrdenAlmacenCompra(Transaccion compra, Transaccion ordenDeCompra, int idComprador, int idPuntoDeCompra, int idAlmacen, DateTime fechaRegistro, bool hayIngresoDeMercaderia, List<DetalleDeOperacion> detallesDeCompra, List<DetalleDeOperacion> detallesDeIngreso)
        {
            detallesDeCompra = detallesDeCompra.Where(dc => dc.Producto.EsBien).ToList();
            List<DetalleDeOperacion> detallesDeOrden = new List<DetalleDeOperacion>();
            if (hayIngresoDeMercaderia)
            {
                foreach (var detalleDeCompra in detallesDeCompra)
                {
                    detalleDeCompra.Cantidad -= detallesDeIngreso.SingleOrDefault(d => d.Producto.Id == detalleDeCompra.Producto.Id && d.Lote == detalleDeCompra.Lote) != null ? detallesDeIngreso.SingleOrDefault(d => d.Producto.Id == detalleDeCompra.Producto.Id && d.Lote == detalleDeCompra.Lote).Cantidad : 0;
                    detallesDeOrden.Add(detalleDeCompra.Clone());
                }
            }
            else
            {
                detallesDeOrden = DetalleDeOperacion.Clone(detallesDeCompra);
            }
            //Obtener la serie de comprobante de la orden de almacen
            Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteOrdenDeAlmacen, idPuntoDeCompra);
            //Generar la orden de almacen
            Transaccion ordenDeAlmacen = GenerarOrdenDeAlmacen(compra, idComprador, idAlmacen, serie.id, fechaRegistro, fechaRegistro, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto al crear una orden de almacen", "Orden de almacen generado al momento de comprar en manera corporativa", detallesDeOrden);
            //Vincular la referencia a la orden de compra
            ordenDeAlmacen.Transaccion3 = ordenDeCompra;
            //Vincular a la compra como transaccion pradre de la orden de almacen
            compra.Transaccion1.Add(ordenDeAlmacen);
            //Generar el ingreso de mercaderia
        }

        #endregion

        #region COMPRA INTEGRADA

        public OperationResult ConfirmarCompra(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaOperacion, List<DetalleDeOperacion> detallesDeCompra, decimal flete, UserProfileSessionData sesionUsuario)
        {
            try
            {
                ValidarComprobante(idProveedor, esPropio, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Validar las acciones y validar roles de centros de atencion 
                ValidarAccionYRolesPuntoDeCompraCajaAlmacenEnCompra(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idPuntoDeCompra, idCaja, idAlmacen);
                //Obtener la fecha de registro esta es la fecha actual obtenida del repositorio 
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Gestionar el stock que se tenga en los detalles de compra

                //Resolver detalles de compra, agregar el flete como detalle de transaccion
                detallesDeCompra = CalcularDetallesDeCompra(detallesDeCompra, idTipoComprobante, tipoCompra, flete);
                //Obtener detalles de transaccion de los detalles de compra
                List<Detalle_transaccion> detalles_transaccion = detallesDeCompra.Select(d => d.DetalleTransaccion()).ToList();
                //Obtener el importe total de la compra
                decimal importeTotal = detalles_transaccion.Sum(dt => dt.total);
                //Generar la transaccion compra 
                Transaccion compra = GenerarTransaccion(idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionCompra, esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante, fechaOperacion, fechaRegistro, importeTotal, observacion);
                //Generar la transaccion orden de compra
                Transaccion ordenDeCompra = GenerarOrdenDeTransaccion(compra, idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, fechaOperacion, fechaRegistro, ModoOperacionEnum.PorMostrador, (TipoOperacionCompra)tipoCompra, ModoPago.Contado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al confirmar una compra", observacion, detalles_transaccion, true);
                //Informacion de la orden de almacen de compra
                ordenDeCompra.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                ordenDeCompra.id_actor_negocio_interno1 = idAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(idAlmacenero, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaRegistro, "Estado asignado al confirmar la compra");
                ordenDeCompra.Estado_transaccion.Add(estadoOrdenAlmacen);
                //Agregar la orden de compra a la compra
                compra.Transaccion1.Add(ordenDeCompra);
                //Generar la cuota unica de la operacion
                Cuota cuota = GenerarCuotaUnicaConPagoACuenta(idComprador, importeTotal, fechaOperacion, false);
                //Agregar la cuota a la transaccion orden de compra
                ordenDeCompra.Cuota.Add(cuota);
                //Generar el pago de la compra
                Transaccion pago = GenerarMovimientoEconomico(compra, cuota, idCajero, idCaja, idProveedor, TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores, fechaOperacion, fechaRegistro, observacion, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "Pago efectivo");
                //Agregar el pago a la transaccion compra
                compra.Transaccion1.Add(pago);
                //Preparar los detalles del ingreso de mercaderia

                //Generar el ingreso de mercaderia 
                Transaccion ingresoMercaderiaPorCompra = GenerarMovimientoDeMercaderia(compra, idAlmacenero, idAlmacen, idProveedor, TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra, fechaRegistro, observacion, detallesDeCompra, sesionUsuario, 0);
                //agregamos la orden de compra como operacion referencia de la entrada de mercaderia
                ingresoMercaderiaPorCompra.Transaccion3 = ordenDeCompra;
                //Agregar el traslado interno a la transaccion compra 
                compra.Transaccion1.Add(ingresoMercaderiaPorCompra);
                //Validar, afectar existencias y guardar la compra
                //return ValidarAfectarExistenciasyGuardarOperacion(ordenDeCompra, compra, detallesDeCompra, idAlmacen);
                return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada() { Operacion = compra, OrdenDeOperacion = ordenDeCompra, MovimientosBienes = new List<Transaccion>() { ingresoMercaderiaPorCompra } }, sesionUsuario);
            }
            catch (ExistenciaException e)
            {
                throw new LogicaException("Error al modificar las existencia de la compra", e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la compra", e);
            }
        }

        private void ValidarComprobante(int idProveedor, bool esPropio, int idTipoComprobante, string numeroSerie, int numeroComprobante)
        {
            ///esta logica: esPropio == true ? 0 : idTipoComprobante, no me convence, es como que trata de excluir de la validacion los comprobantes que sean propios
            bool existeNumeroDeComprobante = ExisteNumeroDeComprobante(idProveedor, esPropio == true ? 0 : idTipoComprobante, numeroSerie, numeroComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
            if (existeNumeroDeComprobante)
            {
                throw new LogicaException("Ya existe un registro con el proveedor y comprobante ingresado");
            }
        }

        public OperationResult ConfirmarCompraAlCreditoRapido(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaOperacion, List<DetalleDeOperacion> detallesDeCompra, decimal flete, UserProfileSessionData sesionUsuario)
        {
            try
            {
                ValidarComprobante(idProveedor, esPropio, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Validar las acciones y validar roles de centros de atencion 
                ValidarAccionYRolesPuntoDeCompraAlmacenEnCompra(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idPuntoDeCompra, idAlmacen);
                //Obtener la fecha de registro esta es la fecha actual obtenida del repositorio 
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Resolver detalles de compra, agregar el flete como detalle de transaccion
                detallesDeCompra = CalcularDetallesDeCompra(detallesDeCompra, idTipoComprobante, tipoCompra, flete);
                //Obtener detalles de transaccion de los detalles de compra
                List<Detalle_transaccion> detalles_transaccion = detallesDeCompra.Select(d => d.DetalleTransaccion()).ToList();
                //Obtener el importe total de la compra
                decimal importeTotal = detalles_transaccion.Sum(dt => dt.total);
                //Generar la transaccion compra 
                Transaccion compra = GenerarTransaccion(idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionCompra, esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante, fechaOperacion, fechaRegistro, importeTotal, observacion);
                //Generar la transaccion orden de compra
                Transaccion ordenDeCompra = GenerarOrdenDeTransaccion(compra, idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, fechaOperacion, fechaRegistro, ModoOperacionEnum.PorMostrador, (TipoOperacionCompra)tipoCompra, ModoPago.CreditoRapido, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al confirmar una compra", observacion, detalles_transaccion, true);
                //Informacion de la orden de almacen de compra
                ordenDeCompra.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                ordenDeCompra.id_actor_negocio_interno1 = idAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(idAlmacenero, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaRegistro, "Estado asignado al confirmar la compra");
                ordenDeCompra.Estado_transaccion.Add(estadoOrdenAlmacen);
                //Agregar la orden de compra a la compra
                compra.Transaccion1.Add(ordenDeCompra);
                //Generar la cuota unica de la operacion
                Cuota cuota = GenerarCuotaUnica(idComprador, importeTotal, fechaOperacion, false, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto al momento de generar la cuota");
                //Agregar la cuota a la transaccion orden de compra
                ordenDeCompra.Cuota.Add(cuota);
                //Generar el ingreso de mercaderia 
                Transaccion ingresoMercaderiaPorCompra = GenerarMovimientoDeMercaderia(compra, idAlmacenero, idAlmacen, idProveedor, TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra, fechaRegistro, observacion, detallesDeCompra, sesionUsuario, 0);
                //agregamos la orden de compra como operacion referencia de la entrada de mercaderia
                ingresoMercaderiaPorCompra.Transaccion3 = ordenDeCompra;
                //Agregar el traslado interno a la transaccion compra 
                compra.Transaccion1.Add(ingresoMercaderiaPorCompra);
                //Validar, afectar existencias y guardar la compra
                return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada { Operacion = compra, OrdenDeOperacion = ordenDeCompra, MovimientosBienes = new List<Transaccion>() { ingresoMercaderiaPorCompra } }, sesionUsuario);
            }
            catch (ExistenciaException e)
            {
                throw new LogicaException("Error al modificar las existencia en compra a credito rapido", e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la compra al credito rapido", e);
            }
        }

        public OperationResult ConfirmarCompraAlCredito(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaOperacion, List<DetalleDeOperacion> detallesDeCompra, decimal flete, List<Cuota> cuotas, UserProfileSessionData sesionUsuario)
        {
            try
            {
                ValidarComprobante(idProveedor, esPropio, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Validar las acciones y validar roles de centros de atencion 
                ValidarAccionYRolesPuntoDeCompraAlmacenEnCompra(idComprador, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idPuntoDeCompra, idAlmacen);
                //Obtener la fecha de registro esta es la fecha actual obtenida del repositorio 
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Resolver detalles de compra, agregar el flete como detalle de transaccion
                detallesDeCompra = CalcularDetallesDeCompra(detallesDeCompra, idTipoComprobante, tipoCompra, flete);
                //Obtener detalles de transaccion de los detalles de compra
                List<Detalle_transaccion> detalles_transaccion = detallesDeCompra.Select(d => d.DetalleTransaccion()).ToList();
                //Obtener el importe total de la compra
                decimal importeTotal = detalles_transaccion.Sum(dt => dt.total);
                //Generar la transaccion compra 
                Transaccion compra = GenerarTransaccion(idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionCompra, esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante, fechaOperacion, fechaRegistro, importeTotal, observacion);
                //Generar la transaccion orden de compra
                Transaccion ordenDeCompra = GenerarOrdenDeTransaccion(compra, idComprador, idPuntoDeCompra, idProveedor, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, fechaOperacion, fechaRegistro, ModoOperacionEnum.PorMostrador, (TipoOperacionCompra)tipoCompra, ModoPago.CreditoConfigurado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al confirmar una compra", observacion, detalles_transaccion, true);
                //Informacion de la orden de almacen de compra
                ordenDeCompra.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                ordenDeCompra.id_actor_negocio_interno1 = idAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(idAlmacenero, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaRegistro, "Estado asignado al confirmar la compra");
                ordenDeCompra.Estado_transaccion.Add(estadoOrdenAlmacen);
                //Agregar la orden de compra a la compra
                compra.Transaccion1.Add(ordenDeCompra);
                //Generar las cuotas del financiamiento y agregar a la orden de compra
                ordenDeCompra.Cuota = GenerarCuotas(idComprador, cuotas, fechaOperacion, false, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto al momento de generar la cuota");
                //Obtener si tiene cuota inicial
                var cuotaInicial = ordenDeCompra.Cuota.SingleOrDefault(c => c.cuota_inicial);
                if (cuotaInicial != null)
                {
                    //Agregar el pago a cuenta en la cuota inicial que se va a pagar
                    cuotaInicial.SetPagoACuenta(cuotaInicial.total);
                    //Generar el pago de la cuota inicial
                    Transaccion pago = GenerarMovimientoEconomico(compra, cuotaInicial, idCajero, idCaja, idProveedor, TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores, fechaOperacion, fechaRegistro, observacion, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "Pago efectivo");
                    //Agregar el pago a la transaccion compra
                    compra.Transaccion1.Add(pago);
                }
                //Generar el ingreso de mercaderia 
                Transaccion ingresoMercaderiaPorCompra = GenerarMovimientoDeMercaderia(compra, idAlmacenero, idAlmacen, idProveedor, TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra, fechaRegistro, observacion, detallesDeCompra, sesionUsuario, 0);
                //agregamos la orden de compra como operacion referencia de la entrada de mercaderia
                ingresoMercaderiaPorCompra.Transaccion3 = ordenDeCompra;
                //Agregar el traslado interno a la transaccion compra 
                compra.Transaccion1.Add(ingresoMercaderiaPorCompra);
                //Validar, afectar existencias y guardar la compra
                return AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada { Operacion = compra, OrdenDeOperacion = ordenDeCompra, MovimientosBienes = new List<Transaccion>() { ingresoMercaderiaPorCompra } }, sesionUsuario);
            }
            catch (ExistenciaException e)
            {
                throw new LogicaException("Error al modificar las existencia en compra a credito", e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la compra al credito", e);
            }
        }

        #endregion



        #region OPCIONES DE COMPRA CORPORATIVA

        public OperationResult ContabilizarCompraCorporativa(long idOrdenCompra)
        {
            //try
            //{
            //    OrdenDeCompra resultado = _logicaOperacion.obtenerOrdenDeCompra(idOrdenCompra);
            //    return Json(new CompraConDetallesViewModel(resultado));
            //}
            //catch (Exception e)
            //{
            //    return Json(Util.errorJson(e));
            //}
            return null;
        }

        public OperationResult RegistrarComprobanteCompraCorporativa(int idEmpleado, int idCentroAtencion, long idCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtener la compra a registrar comprobante
                Compra compra = new Compra(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idCompra));
                //Clonar la transaccion orden
                Transaccion nuevaOrdenCompra = compra.OrdenDeCompra().Transaccion().CloneTransaccionYDetalles();
                //Crear comprobante nuevo
                Comprobante nuevoComprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Vincular el comprobante a la nueva orden(clonada)
                nuevaOrdenCompra.Comprobante = nuevoComprobante;
                //Crear estado confirmado asociado a la nueva orden 
                Estado_transaccion estadoConfirmado = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado asignado cuando se confirma la orden compra");
                nuevaOrdenCompra.Estado_transaccion.Add(estadoConfirmado);
                //Apuntamos las cuotas a la nueva orden 
                nuevaOrdenCompra.Cuota = compra.OrdenDeCompra().Transaccion().Cuota.ToList();
                //Crear estado editado asociado a la orden actual
                Estado_transaccion estadoEditado = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoEditado, fechaActual, "Estado asignado cuando se edita la orden compra");
                estadoEditado.id_transaccion = compra.OrdenDeCompra().Transaccion().id;
                //Crear transaccion del repositorio y agregar el estado
                return transaccionRepositorio.CrearTransaccionYCrearEstadoTransaccion(nuevaOrdenCompra, estadoEditado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion



        #region INVALIDACION DE COMPRA

        public OperationResult InvalidarCompra(long idOrdenDeCompra, int idEmpleado, int idCentroAtencion, string observaciones, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Compra a Invalidar
                OrdenDeCompra ordenDeCompra = new OrdenDeCompra(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idOrdenDeCompra));
                //Obtenenmos la fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtenemos la unidad de negocio
                int idUnidadDeNegocio = ordenDeCompra.Transaccion().Transaccion2.id_unidad_negocio;
                //Validamos si la accion es valida
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idUnidadDeNegocio);
                //Obtenemos los datos de la orden de compra
                decimal tipoDeCambio = ordenDeCompra.TipoDeCambio;
                int idProveedor = ordenDeCompra.Proveedor().Id;
                long idCompra = ordenDeCompra.IdCompra;
                decimal importeTotal = ordenDeCompra.Total;
                var modoDePago = ordenDeCompra.ModoDePago();
                var tipoDeCompra = ordenDeCompra.TipoDeCompra();
                var tipoDeOperacionDeCompra = ordenDeCompra.TipoDeOperacionDeCompra();
                //Obtener la compra
                Compra compra = new Compra(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenDeCompra.IdCompra));

                bool hayMovimientoDeDinero = compra.ObtenerPagos() != null && compra.ObtenerPagos().Count > 0;
                bool hayMovimientoDeAlmacen = compra.ObtenerIngresoDeMercaderia() != null && compra.ObtenerIngresoDeMercaderia().Count > 0;
                //int idCaja = compra.ObtenerPagos().First().Transaccion().id_actor_negocio_interno;
                int idCaja = hayMovimientoDeDinero ? compra.ObtenerPagos().Count() > 0 ? compra.ObtenerPagos().First().Transaccion().id_actor_negocio_interno : idCentroAtencion : idCentroAtencion;
                //int idAlmacen = compra.ObtenerIngresoDeMercaderia().First().Transaccion().id_actor_negocio_interno;
                int idAlmacen = hayMovimientoDeAlmacen ? compra.ObtenerIngresoDeMercaderia().First().Transaccion().id_actor_negocio_interno : 0;

                decimal importePagoTotal;
                List<Cuota> cuotasModificadas = new List<Cuota>();

                if (ordenDeCompra.ModoDePago() == ModoPago.Contado)
                {
                    importePagoTotal = importeTotal;
                }
                else
                {
                    importePagoTotal = ordenDeCompra.Transaccion().Cuota.SelectMany(c => c.Pago_cuota).Sum(cp => cp.importe);
                    var importeRevocar = importeTotal - importePagoTotal;
                    foreach (var cuota in ordenDeCompra.Cuotas().OrderByDescending(c => c.fecha_vencimiento))
                    {
                        if (cuota.saldo > 0)
                        {
                            cuota.revocado = cuota.saldo;
                            cuota.saldo = cuota.total - cuota.pago_a_cuenta - cuota.revocado;
                            cuotasModificadas.Add(cuota);
                        }
                    }
                }
                //Obtenemos la serie de comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionCompra, idCentroAtencion);
                if (serie == null)
                {
                    throw new LogicaException("No existe serie de comprobante");
                }

                //Obtenemos los detalles a invalidar
                List<DetalleDeOperacion> detallesDeInvalidacionDeVenta = ordenDeCompra.Detalles();
                //List<DetalleDeOperacion> detallesDeInvalidacionDeVenta = new List<DetalleDeOperacion>();
                //foreach (var detalle in ordenDeCompra.Detalles())
                //{
                //    detallesDeInvalidacionDeVenta.Add(new DetalleDeOperacion(detalle.Concepto().Id,detalle.Cantidad, null, detalle.Precio, detalle.ImporteTotal, null, 0, null, null, 0, detalle.Igv, detalle.Descuento, detalle.Lote, detalle.Vencimiento, detalle.Registro), detalle.Concepto().EsBien));
                //}

                //creamos el estado invalidado para la orden de compra
                Estado_transaccion estadoDeOrdenDeCompra = new Estado_transaccion(idOrdenDeCompra, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual,
                    "Estado que se agrega al invalidar una Compra");

                OperationResult result = null;
                if (ordenDeCompra.TipoDeCompra() == ModoOperacionEnum.Corporativa)
                {
                    result = ResolverInvalidacionCompraCorporativa(ordenDeCompra, idEmpleado, true, serie.id, fechaActual, importeTotal, observaciones, idProveedor, idCentroAtencion, tipoDeCompra, tipoDeOperacionDeCompra, modoDePago, detallesDeInvalidacionDeVenta, estadoDeOrdenDeCompra, hayMovimientoDeDinero, hayMovimientoDeAlmacen);
                }
                else
                {
                    result = ResolverInvalidacionCompraIntegrada(ordenDeCompra, compra, idEmpleado, true, serie.id, fechaActual, importeTotal, importePagoTotal, observaciones, idProveedor, idCentroAtencion, idCaja, idAlmacen, tipoDeCompra, tipoDeOperacionDeCompra, modoDePago, detallesDeInvalidacionDeVenta, estadoDeOrdenDeCompra, cuotasModificadas, sesionUsuario);
                }

                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(new Exception("Error al intentar invalidar la compra", e));
            }

        }



        private OperationResult ResolverInvalidacionCompraCorporativa(OrdenDeCompra ordenDeCompra, int idEmpleado, bool esPropio, int idSerieComprobante, DateTime fechaActual, decimal importeTotal, string observacion, int idProveedor, int idCentroAtencion, ModoOperacionEnum tipoDeCompra, TipoOperacionCompra tipoDeOperacionDeCompra, ModoPago modoDePago, List<DetalleDeOperacion> detalles, Estado_transaccion estadoDeOrdenDeCompra, bool hayMovimientoDeDinero, bool hayMovimientoDeAlmacen)
        {
            try
            {
                //Obtener detalles de transaccion de los detalles de compra
                List<Detalle_transaccion> detalles_transaccion = detalles.Select(d => d.DetalleTransaccion()).ToList();
                //Generamos la invalidacion de la compra
                Transaccion invalidacionDeCompra = GenerarTransaccion(idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeCompra, true, idSerieComprobante, 0, "", 0, fechaActual, fechaActual, importeTotal, observacion);
                //Referenciamos a invalidacion con la compra
                invalidacionDeCompra.id_transaccion_referencia = ordenDeCompra.Transaccion().id_transaccion_padre;

                //Verificamos si es que hay entrega de mercaderia
                bool seEntregoMercaderiaTotal = hayMovimientoDeAlmacen ? (bool)ordenDeCompra.SeEntregoMercaderiaTotalMente() : true;

                //Generamos la orden de invalidacion de la compra
                Transaccion ordenInvalidacionDeCompra = GenerarOrdenDeTransaccion(invalidacionDeCompra, idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra, fechaActual, fechaActual, tipoDeCompra, tipoDeOperacionDeCompra, modoDePago, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al registrar la invalidacion", observacion, detalles_transaccion, seEntregoMercaderiaTotal);
                //Referenciamos a orden de invalidacion con la orden de compra
                ordenInvalidacionDeCompra.id_transaccion_referencia = ordenDeCompra.Id;
                //Agregamos la orden de la invalidacion a la invalidacion 
                invalidacionDeCompra.Transaccion1.Add(ordenInvalidacionDeCompra);
                decimal totalCuota = 0;
                foreach (var item in new Venta(ordenDeCompra.Transaccion().Transaccion2).ObtenerPagos())
                {
                    totalCuota += item.Total;
                }
                if (hayMovimientoDeDinero)
                {
                    //Creamos la cuota, cuenta por cobrar unica
                    Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, totalCuota, "Unica cuota generada de forma automática al invalidar la compra", true);
                    //Agregamos la cuota en la orden
                    ordenInvalidacionDeCompra.Cuota.Add(cuota);
                }


                return transaccionRepositorio.CrearTransacionYEstadoTransaccionYEstadosCuota(invalidacionDeCompra, estadoDeOrdenDeCompra, null);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el resolver la invalidacion de la compra corporativa", e);
            }

        }

        private OperationResult ResolverInvalidacionCompraIntegrada(OrdenDeCompra ordenDeCompra, Compra compra, int idEmpleado, bool esPropio, int idSerieComprobante, DateTime fechaActual, decimal importeTotal, decimal importePagoTotal, string observacion, int idProveedor, int idCentroAtencion, int idCaja, int idAlmacen, ModoOperacionEnum tipoDeCompra, TipoOperacionCompra tipoDeOperacionDeCompra, ModoPago modoDePago, List<DetalleDeOperacion> detalles, Estado_transaccion estadoDeOrdenDeCompra, List<Cuota> cuotasModificadas, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtener detalles de transaccion de los detalles de compra
                List<Detalle_transaccion> detalles_transaccion = detalles.Select(d => d.DetalleTransaccion()).ToList();
                //Generamos la invalidacion de la compra
                Transaccion invalidacionDeCompra = GenerarTransaccion(idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeCompra, true, idSerieComprobante, 0, "", 0, fechaActual, fechaActual, importeTotal, observacion);
                //Referenciamos a invalidacion con la compra
                invalidacionDeCompra.id_transaccion_referencia = ordenDeCompra.Transaccion().id_transaccion_padre;
                //Generamos la orden de invalidacion de la compra
                Transaccion ordenInvalidacionDeCompra = GenerarOrdenDeTransaccion(invalidacionDeCompra, idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra, fechaActual, fechaActual, tipoDeCompra, tipoDeOperacionDeCompra, modoDePago, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al registrar la invalidacion", observacion, detalles_transaccion, true);
                //Informacion de la orden de almacen de invalidacion de compra
                ordenInvalidacionDeCompra.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                ordenInvalidacionDeCompra.id_actor_negocio_interno1 = idAlmacen;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaActual, "Estado asignado al invalidar la compra");
                ordenInvalidacionDeCompra.Estado_transaccion.Add(estadoOrdenAlmacen);
                //Referenciamos a orden de invalidacion con la orden de compra
                ordenInvalidacionDeCompra.id_transaccion_referencia = ordenDeCompra.Id;
                //Agregamos la orden de la invalidacion a la invalidacion 
                invalidacionDeCompra.Transaccion1.Add(ordenInvalidacionDeCompra);
                if (importePagoTotal > 0)
                {
                    //Creamos la cuota, cuenta por cobrar unica
                    Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, importePagoTotal, importePagoTotal, "Unica cuota generada de forma automática al invalidar la compra", true);
                    //Agregamos la cuota en la orden
                    ordenInvalidacionDeCompra.Cuota.Add(cuota);
                    //Generamos el pago de compra
                    Transaccion pago = GenerarMovimientoEconomico(invalidacionDeCompra, cuota, idEmpleado, idCaja, idProveedor, TransaccionSettings.Default.IdTipoTransaccionIngresoDeDineroPorInvalidacionDeCompra, fechaActual, fechaActual, observacion, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "Pago efectivo");
                    //Agregamos el pago en la compra
                    invalidacionDeCompra.Transaccion1.Add(pago);
                }
                //Generar la salida de mercaderia de por invalidacion de la compra
                Transaccion salidaMercaderiaPorInvalidacionCompra = GenerarMovimientoDeMercaderia(invalidacionDeCompra, idEmpleado, idAlmacen, idProveedor, TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra, fechaActual, observacion, detalles, sesionUsuario, compra.ObtenerIngresoDeMercaderia().First().Id);
                //Agregar como referencia de la salida de mercaderia a la orden de invalidacion de la compra
                salidaMercaderiaPorInvalidacionCompra.Transaccion3 = ordenInvalidacionDeCompra;
                //Agregar el traslado interno a la transaccion compra 
                invalidacionDeCompra.Transaccion1.Add(salidaMercaderiaPorInvalidacionCompra);
                return AfectarInventarioFisicoYGuardarOperacion(new OperacionModificatoria() { Operacion = invalidacionDeCompra, OrdenDeOperacion = ordenInvalidacionDeCompra, MovimientosBienes = new List<Transaccion>() { salidaMercaderiaPorInvalidacionCompra }, NuevosEstadosTransaccionesModificadas = new List<Estado_transaccion>() { estadoDeOrdenDeCompra }, CuotasModificadas = cuotasModificadas }, sesionUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el resolver la invalidacion de la compra integrada", e);
            }
        }

        #endregion

        #region NOTA DE DEBITO Y CREDITO DE COMPRA

        public OperationResult GuardarNotaDeDebitoDeCompra(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtenemos la orden de compra referencia de la nota de credito
                OrdenDeCompra ordenDeCompra = new OrdenDeCompra(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeOperacion));
                //Obtenemos la unidad de negocio
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                //Obtenemos la fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Validamos la accion a realizar
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value, idUnidadNegocio);
                //Calculamos los valores de detalles
                List<Detalle_transaccion> detallesDeNota = CalcularDetalleNotaDebitoCredito(detalles, ordenDeCompra.DetalleTransaccion(), idTipoNota, valorDeNota, motivo, ordenDeCompra.Igv() > 0);
                //Generamos la nota de debito 
                Transaccion notaDeDebito = GenerarNotaDeCreditoDebito(sesionUsuario.Empleado.Id, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaActual, "ND", Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(m => m.Key == idTipoNota).Value, detallesDeNota.Sum(d => d.total), motivo, ordenDeCompra.Proveedor().Id, sesionUsuario.IdCentroDeAtencionSeleccionado, sesionUsuario);

                //Decidir que modo de pago tendra la nota de debito
                ModoPago modoPago = ordenDeCompra.ModoDePago();

                //Generamos la orden de la nota de debito
                Transaccion ordenDeNotaDeDebito = GenerarOrdenNotaDeCreditoDebito(notaDeDebito, sesionUsuario.Empleado.Id, idUnidadNegocio, idTipoNota, fechaActual, ((int)modoPago).ToString(), "ND", Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value, motivo, ordenDeCompra.Proveedor().Id, "", sesionUsuario.IdCentroDeAtencionSeleccionado, detallesDeNota, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto cuando se crea la nota de debito", false, Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(l => l.Key == Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value).Value != 0);
                //Agregamos el parametro tipo de compra de acuerdo a la orden de compra origen
                ordenDeNotaDeDebito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, ((int)ordenDeCompra.TipoDeOperacionDeCompra()).ToString()));
                //Agregamos el id de la orden de venta como referencia de la orden nota de debito
                ordenDeNotaDeDebito.id_transaccion_referencia = ordenDeCompra.Id;
                //Agregamos la orden de compra en la Compra
                notaDeDebito.Transaccion1.Add(ordenDeNotaDeDebito);

                //Creamos la cuota, cuenta por cobrar unica
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, detallesDeNota.Sum(d => d.total), "Unica cuota generada de forma automática", false);
                //Agregamos la cuota en la orden
                ordenDeNotaDeDebito.Cuota.Add(cuota);

                if (modoPago == ModoPago.Contado)
                {
                    //Validamos monto a pagar
                    ValidarImporteAPagar(1, cuota.total, ordenDeNotaDeDebito.importe_total);
                    //Generamos el pago de la nota de credito
                    Transaccion pago = GenerarPagoPorNotaCreditoODebito(ordenDeNotaDeDebito, CodigoPago(cuota), cuota.total, sesionUsuario.Empleado.Id, fechaActual, "", sesionUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado  inicial asignado automaticamente al cobrar una nota de credito");
                    cuota.SetPagoACuenta(ordenDeNotaDeDebito.importe_total);
                    VincularPagoConLaCuota(pago, cuota, ordenDeNotaDeDebito.importe_total);
                    //Agregamos el pago en la compra
                    notaDeDebito.Transaccion1.Add(pago);
                }
                return transaccionRepositorio.CrearTransaccion(notaDeDebito);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la confirmacion de la nota de debito de la venta", e);
            }
        }

        public OperationResult GuardarNotaDeCreditoDeCompra(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtenemos la orden de compra referencia de la nota de credito
                OrdenDeCompra ordenDeCompra = new OrdenDeCompra(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeOperacion));
                Compra compra = new Compra(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenDeCompra.IdCompra));
                //Calculamos los valores de detalles
                List<Detalle_transaccion> detallesDeNota = CalcularDetalleNotaDebitoCredito(detalles, ordenDeCompra.DetalleTransaccion(), idTipoNota, valorDeNota, motivo, ordenDeCompra.Igv() > 0);
                //Calculamos el importe total 
                var importeTotal = detallesDeNota.Sum(d => d.total) + ordenDeCompra.Icbper();
                //Obtenemos la unidad de negocio 
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                //Obtenemos la fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtenemos el tipo de transaccion de movimiento de mercaderia
                int idTipoTransaccionMovimientoDeMercaderia = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(l => l.Key == Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value).Value;
                //Validamos la accion a realizar
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value, idUnidadNegocio);
                //Generamos la nota de debito 
                Transaccion notaDeCredito = GenerarNotaDeCreditoDebito(sesionUsuario.Empleado.Id, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaActual, "NC", Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value, importeTotal, motivo, ordenDeCompra.Proveedor().Id, sesionUsuario.IdCentroDeAtencionSeleccionado, sesionUsuario);
                //Decidir que modo de pago tendra la nota de debito
                ModoPago modoPago = ordenDeCompra.ModoDePago();
                //Generamos la orden de la nota de debito
                Transaccion ordenDeNotaDeCredito = GenerarOrdenNotaDeCreditoDebito(notaDeCredito, sesionUsuario.Empleado.Id, idUnidadNegocio, idTipoNota, fechaActual, ((int)modoPago).ToString(), "NC", Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaCompra.Single(n => n.Key == idTipoNota).Value).Value, motivo, ordenDeCompra.Proveedor().Id, "", sesionUsuario.IdCentroDeAtencionSeleccionado, detallesDeNota, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto cuando se crea la nota de credito", true, idTipoTransaccionMovimientoDeMercaderia != 0);
                //Agregamos a la orden el parametro de icbper, si es que tiene icbper 
                if (ordenDeCompra.Icbper() > 0)
                {
                    ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, ordenDeCompra.Icbper().ToString()));
                    ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, ordenDeCompra.NumeroBolsasDePlastico().ToString()));
                }
                //Agregamos el parametro tipo de compra de acuerdo a la orden de compra origen
                ordenDeNotaDeCredito.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, ((int)ordenDeCompra.TipoDeOperacionDeCompra()).ToString()));
                //Agregamos el id de la orden de comprA como referencia de la orden nota de debito
                ordenDeNotaDeCredito.id_transaccion_referencia = ordenDeCompra.Id;
                //Agregamos la orden de compra en la compra
                notaDeCredito.Transaccion1.Add(ordenDeNotaDeCredito);

                //Creamos la cuota, cuenta por cobrar unica
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, importeTotal, "Unica cuota generada de forma automática", true);
                //Agregamos la cuota en la orden
                ordenDeNotaDeCredito.Cuota.Add(cuota);
                if (modoPago == ModoPago.Contado)
                {
                    //Validamos monto a pagar
                    ValidarImporteAPagar(1, cuota.total, ordenDeNotaDeCredito.importe_total);
                    //Generamos el pago de la nota de credito
                    Transaccion pago = GenerarPagoPorNotaCreditoODebito(ordenDeNotaDeCredito, CodigoPago(cuota), cuota.total, sesionUsuario.Empleado.Id, fechaActual, "", sesionUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado  inicial asignado automaticamente al cobrar una nota de credito");
                    cuota.SetPagoACuenta(ordenDeNotaDeCredito.importe_total);
                    //vincular el pago con la cuota.
                    VincularPagoConLaCuota(pago, cuota, ordenDeNotaDeCredito.importe_total);
                    //Agregamos el pago en la compra
                    notaDeCredito.Transaccion1.Add(pago);
                }

                ordenDeNotaDeCredito.enum1 = (int)IndicadorImpactoAlmacen.NoImpactaNoBienes;
                Transaccion salidaMercaderia = null;
                if (idTipoTransaccionMovimientoDeMercaderia != 0)
                {
                    //Informacion de la orden de almacen de nota de compra
                    ordenDeNotaDeCredito.enum1 = (int)IndicadorImpactoAlmacen.Inmediata;
                    ordenDeNotaDeCredito.id_actor_negocio_interno1 = sesionUsuario.IdCentroAtencionQueTieneElStockIntegrada;
                    Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, fechaActual, "Estado asignado al confirmar la nota de credito de compra");
                    ordenDeNotaDeCredito.Estado_transaccion.Add(estadoOrdenAlmacen);
                    //Generamos la salida de mercaderia
                    salidaMercaderia = GenerarMovimientoDeMercaderia(ordenDeNotaDeCredito, sesionUsuario.Empleado.Id, sesionUsuario.IdCentroAtencionQueTieneElStockIntegrada, ordenDeCompra.Proveedor().Id, idTipoTransaccionMovimientoDeMercaderia, fechaActual, "", detallesDeNota, sesionUsuario, compra.ObtenerIngresoDeMercaderia().First().Id);
                    //Salida de mercaderia como referencia de la orden de venta
                    salidaMercaderia.Transaccion3 = ordenDeNotaDeCredito;
                    //agregamos la salida de la mercaderia
                    notaDeCredito.Transaccion1.Add(salidaMercaderia);
                }
                return idTipoTransaccionMovimientoDeMercaderia != 0 ? AfectarInventarioFisicoYGuardarOperacion(new OperacionIntegrada { Operacion = notaDeCredito, OrdenDeOperacion = ordenDeNotaDeCredito, MovimientosBienes = new List<Transaccion>() { salidaMercaderia } }, sesionUsuario) : transaccionRepositorio.CrearTransaccion(notaDeCredito);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la confirmacion de la nota de credito de la compra", e);
            }
        }

        #endregion

        #region OBTENCION DE DATOS

        public Compra ObtenerCompra(long data)
        {
            try
            {
                return (new Compra(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(data)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener la compra", e);
            }
        }

        public OrdenDeCompra ObtenerOrdenDeCompra(long data)
        {
            try
            {
                return (new OrdenDeCompra(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(data)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener la orden de compra", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCompra(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobante para compra", e);
            }
        }

        public List<OrdenDeCompra> ObtenerOrdenesDeCompra(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeCompra.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ordenes de compra", e);
            }
        }

        public List<OperacionDeCompra> ObtenerOperacionesDeCompra(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                List<OperacionDeCompra> operacionesDeCompra = OperacionDeCompra.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras, Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion, fechaDesde, fechaHasta).ToList());

                return operacionesDeCompra.OrderByDescending(oc => oc.FechaEmision).ThenByDescending(oc => oc.Id).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener operaciones de compra", e);
            }
        }

        public List<OperacionDeCompra> ObtenerOperacionesDeCompra(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                List<OperacionDeCompra> operacionesDeCompra = OperacionDeCompra.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras, Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion, fechaDesde, fechaHasta).ToList());

                return operacionesDeCompra.OrderByDescending(oc => oc.FechaEmision).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener operaciones de compra", e);
            }
        }

        public List<OrdenDeCompra> ObtenerOrdenesDeCompraConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeCompra.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra }, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList()).OrderBy(c => c.FechaEmision).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra confirmadas", e);
            }
        }

        public List<Resumen_Compra> ObtenerResumenesCompraDeTipoNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int tipoComprobante)
        {
            try
            {
                int[] idsTiposComprobante = tipoComprobante == (int)TipoComprobanteReporteCompra.Todos ? Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion : tipoComprobante == (int)TipoComprobanteReporteCompra.Tributables ? Diccionario.TiposDeComprobanteTributables : Diccionario.TiposDeComprobanteNoTributablesCompra;
                return transaccionRepositorio.ObtenerResumenCompraPorTipoCompra(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeComprasExceptoInvalidacion, idsTiposComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, TipoOperacionCompra.NoGravada, fechaDesde, fechaHasta).OrderBy(c => c.FechaInicio).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra confirmadas", e);
            }
        }
        public List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int tipoComprobante)
        {
            try
            {
                int[] idsTiposComprobante = tipoComprobante == (int)TipoComprobanteReporteCompra.Todos ? Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion : tipoComprobante == (int)TipoComprobanteReporteCompra.Tributables ? Diccionario.TiposDeComprobanteTributables : Diccionario.TiposDeComprobanteNoTributablesCompra;
                return transaccionRepositorio.ObtenerResumenCompraPorTipoCompra(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeComprasExceptoInvalidacion, idsTiposComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, TipoOperacionCompra.GravadaDestinadaAVentasGravadas, fechaDesde, fechaHasta).OrderBy(c => c.FechaInicio).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra confirmadas", e);
            }
        }
        public List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int tipoComprobante)
        {
            try
            {
                int[] idsTiposComprobante = tipoComprobante == (int)TipoComprobanteReporteCompra.Todos ? Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion : tipoComprobante == (int)TipoComprobanteReporteCompra.Tributables ? Diccionario.TiposDeComprobanteTributables : Diccionario.TiposDeComprobanteNoTributablesCompra;
                return transaccionRepositorio.ObtenerResumenCompraPorTipoCompra(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeComprasExceptoInvalidacion, idsTiposComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, TipoOperacionCompra.GravadaDestinadaAVentasNoGravadas, fechaDesde, fechaHasta).OrderBy(c => c.FechaInicio).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra confirmadas", e);
            }
        }
        public List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadasYNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int tipoComprobante)
        {
            try
            {
                int[] idsTiposComprobante = tipoComprobante == (int)TipoComprobanteReporteCompra.Todos ? Diccionario.TiposDeComprobanteParaCompraExceptoNotaInvalidacion : tipoComprobante == (int)TipoComprobanteReporteCompra.Tributables ? Diccionario.TiposDeComprobanteTributables : Diccionario.TiposDeComprobanteNoTributablesCompra;
                return transaccionRepositorio.ObtenerResumenCompraPorTipoCompra(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeComprasExceptoInvalidacion, idsTiposComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, TipoOperacionCompra.GravadaDestinadaAVentasGravadasYNoGravadas, fechaDesde, fechaHasta).OrderBy(c => c.FechaInicio).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra confirmadas", e);
            }
        }

        public List<OperacionDeCompra> ObtenerOrdenesYNotasDeCreditoYDebitoDeComprasTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                int[] idsOrdenesYNotas = {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,TransaccionSettings.Default.IdTipoTransaccionAnulacionDeCompra,
                TransaccionSettings.Default.IdTipoTransaccionDescuentoSobreOrdenDeCompra, TransaccionSettings.Default.IdTipoTransaccionDebitoOrdenDeCompra};

                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito, MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito };
                return OperacionDeCompra.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsOrdenesYNotas, idsTiposComprobantes, idsEstados, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de compra y notas de credito, debito de compra tributables", e);
            }
        }

        public bool ExisteNumeroDeComprobante(int idProveedor, int idTipoComprobante, string numeroDeSerie, int numeroComprobante, int idEstadoActual)
        {
            try
            {
                return transaccionRepositorio.ExisteComprobante(idProveedor, idTipoComprobante, numeroDeSerie, numeroComprobante, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra, idEstadoActual);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar de validar el numero de comprobante", e);
            }
        }

        #endregion

        #region ACTUALIZAR INVENTARIO LOGICO

        //public OperationResult ActualizarInventarioFisico(List<Detalle_transaccion> detalles)
        //{
        //    try
        //    {
        //        List<Vinculo_Actor_Negocio> carteraDeClientes = new List<Vinculo_Actor_Negocio>();
        //        DateTime fechaActual = DateTimeUtil.FechaActual();
        //        DateTime fechaFin = fechaActual.AddYears(50);
        //        for (int i = 0; i < idClientes.Count; i++)
        //        {
        //            Vinculo_Actor_Negocio vinculo = new Vinculo_Actor_Negocio
        //            {
        //                id_actor_negocio_principal = idCentroDeAtencion,
        //                id_actor_negocio_vinculado = idClientes[i],
        //                desde = fechaActual,
        //                hasta = fechaFin,
        //                descripcion = "",
        //                tipo_vinculo = (int)TipoVinculo.CarteraDeCliente,
        //                es_vigente = true
        //            };
        //            carteraDeClientes.Add(vinculo);
        //        }
        //        return _actorRepositorio.ActualizarVinculosActorNegocio(idCentroDeAtencion, carteraDeClientes, (int)TipoVinculo.CarteraDeCliente);
        //    }
        //    catch (Exception e) {
        //        throw new LogicaException("Error al intentar actualizar inventario fisico", e);
        //    }
        //}
        #endregion

        #region

        public List<string> ObtenerFechaIncioyFinParaReporteCompra()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            string fechaDesde = fechaActual.AddDays(-1).Date.ToString("dd/MM/yyyy");
            string fechaHasta = fechaActual.Date.ToString("dd/MM/yyyy");

            return new List<string> { fechaDesde, fechaHasta };
        }

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Parking.Logica
{
    public partial class RestauranteLogica : IRestauranteLogica
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionRestaurante"></param>
        /// <returns></returns>
        public IEnumerable<Object> ObtenerAtencionesPorCObrar(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante)
        {
            ///devolver las atenciones con las ventas anidadas(comprobantes a emitir)
            return null;
        }

        /// <summary>
        ///RegistrarOrden de venta en estado Registrado, adjuntando la traza de pago(medio de pago, obs, etc). Asignar un comprobante temporal(Se debe tener parametrizado un comprobante generico para el tipo nota de venta, boleta y factura)
        ///
        /// 
        /// 
        /// /// Registrar el pago de una atención. Se tenga o no preconfigurado el pago.
        /// En caso se tienen ventas pregeneradas, se actualizan y se confirman(generar estado confirmado).
        /// En caso NO se tengan ventas pregeneradas, se crean y se confirman.
        /// 
        /// 
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionRestaurante"></param>
        /// <returns></returns>
        public OperationResult ConfirmarFacturacion(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante)
        {
            try
            {
                OperationResult result = null;
                Transaccion transaccionAtencion = _transaccionRepositorio.ObtenerTransaccion(atencion.Id);
                //Validar si la atención cumple las condiciones para poder ser cerrada. La etencion deberia estar en estado cerrado.
                if (transaccionAtencion.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCerrado)
                {
                    throw new LogicaException("No se puede registrar la información de pago por que la atención no se encuentra cerrada");
                }
                //Verificamos que este sin facturar la atencion
                if (transaccionAtencion.enum1 != (int)TipoPago.Ninguno)
                {
                    throw new LogicaException("No se puede registrar la información de pago por que la atención ya se encuentra facturada");
                }
                //Agregar los posibles eventos a la transaccion de atencion
                AgregarEventosTransaccionAtencion(transaccionAtencion, sesionRestaurante);
                //Quitar los detalles de las ordenes con estado devuelto y anulado
                var ordenesFiltradas = atencion.Ordenes.ToList();
                ordenesFiltradas.ForEach(o => { o.DetallesDeOrden = o.DetallesDeOrden.Where(dor => dor.Estado != (int)EstadoDeDetalleDeOrden.Devuelto && dor.Estado != (int)EstadoDeDetalleDeOrden.Anulado).ToList(); });
                ordenesFiltradas = ordenesFiltradas.Where(o => o.DetallesDeOrden.Count() > 0).ToList();
                atencion.Ordenes = ordenesFiltradas;
                //Segun el medio de pago realizar la facturacion debida
                transaccionAtencion.enum1 = atencion.TipoDePago;
                switch (atencion.TipoDePago)
                {
                    case (int)TipoPago.Simple:
                        result = ResolverVentaTipoPagoSimple(atencion, sesionRestaurante, transaccionAtencion); break;
                    case (int)TipoPago.DivididoPorMonto:
                        result = ResolverVentaTipoPagoDivididoPorMonto(atencion, sesionRestaurante, transaccionAtencion); break;
                    case (int)TipoPago.DivididoPorItem:
                        result = ResolverVentaTipoPagoDivididoPorItem(atencion, sesionRestaurante, transaccionAtencion); break;
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar la facturacion", e);
            }
        }

        private void AgregarEventosTransaccionAtencion(Transaccion transaccionAtencion, SesionRestaurante sesionRestaurante)
        {
            //Agregar el evento transmitido a la transaccion de atencion cuando se realiza la operacion
            transaccionAtencion.Evento_transaccion.Add(new Evento_transaccion
            {
                id_empleado = sesionRestaurante.SesionDeUsuario.Empleado.Id,
                id_transaccion = transaccionAtencion.id,
                id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoFacturado,
                fecha = DateTimeUtil.FechaActual(),
                comentario = "Evento cuando se realizaa la facturacion la atencion"
            });
            if (sesionRestaurante.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.IdRolCajero))
            {
                transaccionAtencion.Evento_transaccion.Add(new Evento_transaccion
                {
                    id_empleado = sesionRestaurante.SesionDeUsuario.Empleado.Id,
                    id_transaccion = transaccionAtencion.id,
                    id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    fecha = DateTimeUtil.FechaActual(),
                    comentario = "Evento cuando se realizaa la facturacion la atencion"
                });
            }
        }

        public Actor_negocio ObtenerYDesocuparActorNegocioMesa(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante, Transaccion transaccionAtencion)
        {
            Actor_negocio actorNegocioMesa = transaccionAtencion.Actor_negocio4;
            actorNegocioMesa.indicador1 = false;
            var ambienteMesa = sesionRestaurante.Ambientes.SingleOrDefault(a => a.Id == atencion.Mesa.IdAmbiente);
            if ((ambienteMesa != null && ambienteMesa.MesasTemporales) || !atencion.EsAtencionConMesa)
            {
                actorNegocioMesa.es_vigente = false;
            }
            return actorNegocioMesa;
        }

        /// <summary>
        /// Si es pago único, Generar una venta con los mismos detalles (Se asume que la orden viene sin detalles)
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionUsuario"></param>
        /// <param name="transaccionAtencion"></param>
        /// <returns></returns>
        public OperationResult ResolverVentaTipoPagoSimple(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante, Transaccion transaccionAtencion)
        {
            var datosVenta = atencion.Comprobantes.Single();
            datosVenta.TransaccionOrigen = transaccionAtencion;
            datosVenta.ActorReferencia = ObtenerYDesocuparActorNegocioMesa(atencion, sesionRestaurante, transaccionAtencion);
            CompletarDatosGeneralesDeVenta(datosVenta, atencion, sesionRestaurante.SesionDeUsuario);
            datosVenta.Orden.Detalles = GenerarDetallesDeVenta(atencion);
            var resultado = _operacionLogica.ConfirmarVentaIntegrada(ModoOperacionEnum.PorMostrador, sesionRestaurante.SesionDeUsuario, datosVenta);
            Dictionary<long, long> mapeoIdsDatosVentasIntegradasIdsOrden = new Dictionary<long, long>();
            mapeoIdsDatosVentasIntegradasIdsOrden.Add(0, (long)resultado.information);
            resultado.objeto = mapeoIdsDatosVentasIntegradasIdsOrden;
            return resultado;
        }

        /// <summary>
        /// Si son varios Pagos en partes iguales. Generar las ventas necesarias, dividiendo la cantidad en partes iguales y por lo tanto el importe tambien debe dividirse en partes iguales. Todas las ventas saldrán por consumo. (Se asume que las ordenes vienen sin detalles)
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionUsuario"></param>
        /// <param name="transaccionAtencion"></param>
        /// <returns></returns>
        public OperationResult ResolverVentaTipoPagoDivididoPorMonto(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante, Transaccion transaccionAtencion)
        {
            if (atencion.ImporteAtencion != atencion.Comprobantes.Sum(c => c.Orden.Total))
                throw new LogicaException("No se puede registrar los comprobantes debido a que los total de la atencion es diferente a la suma del importe de tus comprobantes");
            var datosVentas = atencion.Comprobantes;
            datosVentas.ForEach(v => v.Orden.UnificarDetalles = true);
            datosVentas.ForEach(v => v.TransaccionOrigen = transaccionAtencion);
            datosVentas.ForEach(v => v.ActorReferencia = ObtenerYDesocuparActorNegocioMesa(atencion, sesionRestaurante, transaccionAtencion));
            datosVentas.ForEach(v => CompletarDatosGeneralesDeVenta(v, atencion, sesionRestaurante.SesionDeUsuario));
            datosVentas.ForEach(v => v.Orden.Detalles = GenerarDetallesDeVenta(atencion, v.Orden.Total));
            RecalcularCantidadEImporteTipoPagoDivididoPorMonto(atencion, datosVentas);
            OperationResult resultado = _operacionLogica.ConfirmarVentasIntegradas(ModoOperacionEnum.PorMostrador, sesionRestaurante.SesionDeUsuario, datosVentas);
            return resultado;
        }

        /// <summary>
        /// Si son varios Pagos con items detallados, generar una venta por cada uno con los detalles indicados. Se asume que las ordenes vienen con detalles.
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionUsuario"></param>
        /// <param name="transaccionAtencion"></param>
        /// <returns></returns>
        public OperationResult ResolverVentaTipoPagoDivididoPorItem(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante, Transaccion transaccionAtencion)
        {
            var datosVentas = atencion.Comprobantes;
            var detallesDeOrden = atencion.Ordenes.SelectMany(o => o.DetallesDeOrden).ToList();
            foreach (var datoVenta in datosVentas)
            {
                datoVenta.Orden.Detalles = ConvertirDetalleOrdenADetalleOperacion(detallesDeOrden, datoVenta.Orden.Detalles.Select(d => d.Id).ToList());
            }
            //datosVentas.ForEach(v => v.Orden.Detalles.ToList().ForEach(d => d = ConvertirDetalleOrdenADetalleOperacion(detallesDeOrden, d.Id)));
            datosVentas.ForEach(v => v.TransaccionOrigen = transaccionAtencion);
            datosVentas.ForEach(v => v.ActorReferencia = ObtenerYDesocuparActorNegocioMesa(atencion, sesionRestaurante, transaccionAtencion));
            datosVentas.ForEach(v => CompletarDatosGeneralesDeVenta(v, atencion, sesionRestaurante.SesionDeUsuario));
            datosVentas.ForEach(v => AgruparDetallesDeVenta(v));
            var resultado = _operacionLogica.ConfirmarVentasIntegradas(ModoOperacionEnum.PorMostrador, sesionRestaurante.SesionDeUsuario, datosVentas);
            return resultado;
        }

        public List<DetalleDeOperacion> ConvertirDetalleOrdenADetalleOperacion(List<DetalleOrden> detallesDeOrden, List<long> idsDetalle)
        {
            List<DetalleDeOperacion> nuevosDetalles = new List<DetalleDeOperacion>();
            foreach (var idDetalle in idsDetalle)
            {
                var detalle = detallesDeOrden.First(d => d.Id == idDetalle);
                nuevosDetalles.Add(new DetalleDeOperacion()
                {
                    Id = idDetalle,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.Precio,
                    Importe = detalle.Importe,
                    Producto = new Concepto_Negocio_Comercial() { Id = detalle.IdItem, EsBien = detalle.EsBien }
                });
            }
            return nuevosDetalles;
        }

        /// <summary>
        /// Se establecen los datos como punto de venta, vendedor, entre otros parámetros necesarios para una venta.
        /// </summary>
        /// <param name="datosVenta"></param>
        /// <param name="atencion"></param>
        /// <param name="sesionUsuario"></param>
        public void CompletarDatosGeneralesDeVenta(DatosVentaIntegrada datosVenta, AtencionRestaurante atencion, UserProfileSessionData sesionUsuario)
        {
            ///Agregamos la transaccion origen a la transaccion 
            datosVenta.Orden.PuntoDeVenta = new ItemGenerico { Id = sesionUsuario.IdCentroDeAtencionSeleccionado, Nombre = sesionUsuario.CentroDeAtencionSeleccionado.Nombre };
            datosVenta.Orden.Vendedor = new ItemGenerico { Id = sesionUsuario.Empleado.Id };
            datosVenta.Orden.NumeroBolsasDePlastico = 0;
            datosVenta.MovimientoAlmacen = new DatosMovimientoDeAlmacen { HayComprobanteDeSalidaDeMercaderia = false };
        }

        /// <summary>
        /// Devuelve los detalles de venta generados a partir de los detalles de las ordenes de la <paramref name="atencion"/>.
        /// En caso de haber items que se repiten en distintos detalles, se agruparan en un solo detalle para la venta, siempre y cuando tengan el mismo precio unitario.
        /// </summary>
        /// <param name="atencion"></param>
        /// <returns></returns>
        /// 
        private List<DetalleDeOperacion> GenerarDetallesDeVenta(AtencionRestaurante atencion)
        {
            try
            {
                var detallesVenta = new List<DetalleDeOperacion>();
                var detallesAgrupados = atencion.Ordenes.SelectMany(o => o.DetallesDeOrden).GroupBy(d => new { d.IdItem, d.Precio });
                foreach (var grupo in detallesAgrupados)
                {
                    detallesVenta.Add(new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = grupo.Key.IdItem, EsBien = grupo.First().EsBien },
                        Cantidad = grupo.Sum(g => g.Cantidad),
                        PrecioUnitario = grupo.Key.Precio,
                        Importe = grupo.Sum(g => g.Importe),
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoCantidadCalculada
                    });
                }
                return detallesVenta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar determinar los detalles de venta", e);
            }
        }
        /// <summary>
        /// Devuelve los detalles de venta generados a partir de los detalles de las ordenes de la <paramref name="atencion"/> y del porcentaje obtenido del <paramref name="importeParcial"/> y el importe de atencion.
        /// En caso de haber items que se repiten en distintos detalles, se agruparan en un solo detalle para la venta, siempre y cuando tengan el mismo precio unitario.
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="importeParcial"></param>
        /// <returns></returns>
        /// 
        private List<DetalleDeOperacion> GenerarDetallesDeVenta(AtencionRestaurante atencion, decimal importeParcial)
        {
            try
            {
                var detallesVenta = new List<DetalleDeOperacion>();
                var detallesVentaAgrupados = GenerarDetallesDeVenta(atencion);
                var porcentageDelPago = importeParcial / atencion.ImporteAtencion;
                foreach (var detalle in detallesVentaAgrupados)
                {
                    detallesVenta.Add(new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = detalle.Producto.Id, EsBien = detalle.Producto.EsBien },
                        Cantidad = detalle.Cantidad * porcentageDelPago,
                        PrecioUnitario = detalle.PrecioUnitario,
                        Importe = Math.Round(detalle.Importe * porcentageDelPago, 2),
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoDeNingunValorCalculado
                    });
                }
                return detallesVenta;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los detalles de venta", e);
            }
        }

        private void AgruparDetallesDeVenta(DatosVentaIntegrada venta)
        {
            try
            {
                ///copiar y agrupar todos los items de las ordenes de la atención hacia la orden de venta 
                var detallesVenta = new List<DetalleDeOperacion>();
                var detallesAgrupados = venta.Orden.Detalles.GroupBy(d => new { d.Producto.Id, d.PrecioUnitario });
                foreach (var grupo in detallesAgrupados)
                {
                    detallesVenta.Add(new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = grupo.Key.Id, EsBien = grupo.First().Producto.EsBien },
                        Cantidad = grupo.Sum(g => g.Cantidad),
                        PrecioUnitario = grupo.Key.PrecioUnitario,
                        Importe = grupo.Sum(g => g.Importe),
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoCantidadCalculada
                    });
                }
                venta.Orden.Detalles = detallesVenta;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los detalles de venta", e);
            }
        }
        
        private void RecalcularCantidadEImporteTipoPagoDivididoPorMonto(AtencionRestaurante atencion, List<DatosVentaIntegrada> comprobantesVenta)
        {
            try
            {
                var detallesAtencion = GenerarDetallesDeVenta(atencion);
                for (int i = 0; i < comprobantesVenta.Count; i++)
                {
                    var detallesComprobante = AgruparDetallesComprobantes(comprobantesVenta);
                    if (atencion.Comprobantes[i].Orden.Total != comprobantesVenta[i].Orden.ImporteTotal)
                    {
                        var diferencia = atencion.Comprobantes[i].Orden.Total - comprobantesVenta[i].Orden.ImporteTotal;
                        for (int j = 0; j < detallesComprobante.Count; j++)
                        {
                            if (detallesComprobante[j].Importe != detallesAtencion.Single(d => d.Producto.Id == detallesComprobante[j].Producto.Id && d.PrecioUnitario == detallesComprobante[j].PrecioUnitario).Importe)
                            {
                                comprobantesVenta[i].Orden.Detalles.Single(d => d.Producto.Id == detallesComprobante[j].Producto.Id && d.PrecioUnitario == detallesComprobante[j].PrecioUnitario).Importe += diferencia;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < detallesAtencion.Count; j++)
                    {
                        var diferencia = detallesAtencion[j].Importe - comprobantesVenta.SelectMany(c => c.Orden.Detalles).Where(d => d.Producto.Id == detallesAtencion[j].Producto.Id && d.PrecioUnitario == detallesAtencion[j].PrecioUnitario).Sum(d => d.Importe);
                        if (diferencia != 0)
                        {
                            comprobantesVenta[i].Orden.Detalles.First(d => d.Producto.Id == detallesAtencion[j].Producto.Id && d.PrecioUnitario == detallesAtencion[j].PrecioUnitario).Importe += diferencia;
                        }
                    }
                }
                for (int i = 0; i < detallesAtencion.Count; i++)
                {
                    var diferencia = detallesAtencion[i].Cantidad - comprobantesVenta.SelectMany(c => c.Orden.Detalles).Where(d => d.Producto.Id == detallesAtencion[i].Producto.Id && d.PrecioUnitario == detallesAtencion[i].PrecioUnitario).Sum(d => d.Cantidad);
                    if (diferencia != 0)
                    {
                        comprobantesVenta.SelectMany(c => c.Orden.Detalles).First(d => d.Producto.Id == detallesAtencion[i].Producto.Id && d.PrecioUnitario == detallesAtencion[i].PrecioUnitario).Cantidad += diferencia;
                    }
                }
                foreach (var item in detallesAtencion)
                {
                    if (detallesAtencion.Single(i => i.Producto.Id == item.Producto.Id && i.PrecioUnitario == item.PrecioUnitario).Cantidad != comprobantesVenta.SelectMany(dv => dv.Orden.Detalles).Where(d => d.Producto.Id == item.Producto.Id && d.PrecioUnitario == item.PrecioUnitario).Sum(d => d.Cantidad))
                    {
                        throw new LogicaException("No se puede registrar los comprobantes debido a que los totales en las cantidades no son congruentes");
                    }
                    if (detallesAtencion.Single(i => i.Producto.Id == item.Producto.Id && i.PrecioUnitario == item.PrecioUnitario).Importe != comprobantesVenta.SelectMany(dv => dv.Orden.Detalles).Where(d => d.Producto.Id == item.Producto.Id && d.PrecioUnitario == item.PrecioUnitario).Sum(d => d.Importe))
                    {
                        throw new LogicaException("No se puede registrar los comprobantes debido a que los totales en los importes no son congruentes");
                    }
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al verificar la cantidad o importe en el tipo de pago dividido por monto", e);
            }
        }

        private List<DetalleDeOperacion> AgruparDetallesComprobantes(List<DatosVentaIntegrada> comprobantesGenerados)
        {
            try
            {
                var detallesVenta = new List<DetalleDeOperacion>();
                var detallesAgrupados = comprobantesGenerados.Select(c => c.Orden).SelectMany(o => o.Detalles).GroupBy(d => new { d.Producto.Id, d.PrecioUnitario });
                foreach (var grupo in detallesAgrupados)
                {
                    detallesVenta.Add(new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = grupo.Key.Id },
                        Cantidad = grupo.Sum(g => g.Cantidad),
                        PrecioUnitario = grupo.Key.PrecioUnitario,
                        Importe = grupo.Sum(g => g.Importe),
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoCantidadCalculada
                    });
                }
                return detallesVenta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar determinar los detalles de venta", e);
            }
        }

        public OperationResult ConfirmarPagoAtencion(long idAtencion, SesionRestaurante sesionRestaurante)
        {
            try
            {
                var atencion = _atencionRepositorio.ObtenerAtencionConDatosMinimosDeOrdenYDetallesSoloParaCerrarla(idAtencion);
                if (atencion.Confirmado)
                {
                    throw new LogicaException("No se puede confirmar el pago de la atencion, por que ya se encuentra confirmado");
                }
                var eventoTransaccionConfirmado = new Evento_transaccion
                {
                    id_empleado = sesionRestaurante.SesionDeUsuario.Empleado.Id,
                    id_transaccion = idAtencion,
                    id_evento = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    fecha = DateTimeUtil.FechaActual(),
                    comentario = "Evento cuando se realizaa la facturacion la atencion"
                };
                OperationResult result = _transaccionRepositorio.CrearEventoTransaccion(eventoTransaccionConfirmado);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar la facturacion", e);
            }
        }
    }
}



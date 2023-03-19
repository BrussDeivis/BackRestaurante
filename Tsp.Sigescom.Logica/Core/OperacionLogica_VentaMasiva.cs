using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        #region VENTAS MASIVAS
        public OperationResult ConfirmarVentaMasiva(UserProfileSessionData sesionDeUsuario, VentaMasiva ventaMasiva)
        {
            try
            {
                List<OperacionIntegrada> ventasIntegradasAGuardar = new List<OperacionIntegrada>();
                //Validar que las ventas sean mayores a 0
                var ventasValidas = ventaMasiva.Detalles.Where(d => d.Cantidad > 0);

                //Obtener maximos codigos para ventas y cuotas yy salidas de mercaderia
                int codigoMaximoVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionVenta).Value, TransaccionSettings.Default.IdTipoTransaccionVenta);
                int codigoMaximoOrdenVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta);
                int codigoMaximoCuota = transaccionRepositorio.ObtenerMaximoCodigoCuota("C" + ventaMasiva.FechaEmision.Year);
                int codigoMaximoSalidaMercaderia = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta).Value, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta);
                int codigoMaximoCobro = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion("C_" + ventaMasiva.FechaEmision.ToString("yyyy"), TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes);
                Codigo codigosMaximos;
                foreach (var ventaValida in ventasValidas)
                {
                    //Agregar a la venta valida el cliente generico
                    ventaValida.IdCliente = ActorSettings.Default.IdClienteGenerico;
                    //Obtener el tipo de comprobante
                    ventaMasiva.IdTipoDeComprobante = ObtenerIdTipoComprobante(ventaMasiva.IdSerieDeComprobante);
                    //Generar los codigos maximos
                    codigosMaximos = new Codigo(ventaMasiva.FechaEmision, codigoMaximoVenta, codigoMaximoOrdenVenta, codigoMaximoCuota, codigoMaximoCobro, codigoMaximoSalidaMercaderia);
                    //Obtener en registro de venta a venta masiva
                    RegistroDeVenta registroDeVenta = RegistroDeVenta.ConvertVentaMasiva(ventaMasiva, ventaValida);
                    DatosVentaIntegrada datosVentaIntegrada = new DatosVentaIntegrada(registroDeVenta);
                    //Validar y generar la transaccion venta 
                    OperacionIntegrada ventaIntegrada = GenerarVentaIntegrada(ModoOperacionEnum.VentaIntegradaMasivaDigitada, sesionDeUsuario, datosVentaIntegrada, true);
                    //Modificar los codigos de las operaciones
                    ModificarCodigoDeTransaccionesParaVentaMasivas(ventaIntegrada, codigosMaximos);
                    //ModificarCodigoDeTransaccionesParaVentaMasivas(ventaIntegrada.Operacion, codigosMaximos, ModoPago.Contado);
                    ventasIntegradasAGuardar.Add(ventaIntegrada);
                    codigoMaximoVenta++; codigoMaximoOrdenVenta++; codigoMaximoCuota++; codigoMaximoCobro++; codigoMaximoSalidaMercaderia++;
                }
                ResolverComprobantes(ventasIntegradasAGuardar);
                return AfectarInventarioFisicoYGuardarOperaciones(ventasIntegradasAGuardar, sesionDeUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar confirmar la operación", e);
            }
        }

        public int ObtenerIdTipoComprobante(int idSerieComprobante)
        {
            var serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieComprobante);
            return serie.id_tipo_comprobante;
        }

        public void ModificarCodigoDeTransaccionesParaVentaMasivas(OperacionIntegrada ventaIntegrada, Codigo codigosMaximos)
        {
            try
            {
                //Actualizar el codigo de venta
                ventaIntegrada.Operacion.codigo = codigosMaximos.SiguienteCodigoVenta;
                //Actualizar el codigo de orden de venta
                ventaIntegrada.OrdenDeOperacion.codigo = codigosMaximos.SiguienteCodigoOrdenVenta;
                //Actualizar el codigo de la unica cuota
                ventaIntegrada.OrdenDeOperacion.Cuota.First().codigo = codigosMaximos.SiguienteCodigoCodigoCuota;
                //Verificar que la venta tenga movimiento econimico
                if (ventaIntegrada.MovimientoEconomico != null)
                {
                    //Actualizar el codigo del unico cobro que debe de tener la venta
                    ventaIntegrada.MovimientoEconomico.codigo = codigosMaximos.SiguienteCodigoPago;
                }
                //Verificar que la venta tenga movimiento de bienes
                if (ventaIntegrada.MovimientosBienes != null && ventaIntegrada.MovimientosBienes.Count() > 0)
                {
                    //Actualizar el codigo de salida de mercaderia que debe de tener
                    ventaIntegrada.MovimientosBienes.First().codigo = codigosMaximos.SiguienteCodigoSalidaMercaderia;
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar, generar y guardar la venta", e);
            }
        }
        #endregion

        #region VENTAS Y COBROS POR VENDEDOR
        private Transaccion GenerarVentaYCobroEnBloque(int idEmpleado, int idCentroDeAtencion, DateTime fechaRegistro, decimal tipoDeCambio, string sufijoCodigo, int idTipoTransaccion, int accionARealizar, int idTipoTransaccionValidar, string observacion)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                //Validar la accion a realizar
                permisos_Logica.ValidarAccion(idEmpleado, accionARealizar, idTipoTransaccionValidar, idUnidadNegocio);
                //Obtener el codigo
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(sufijoCodigo, idTipoTransaccion);
                //Obtener operacion generica
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Crear una venta y cobro en bloque
                Transaccion ventaYCobroEnbloque = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, 0, idCentroDeAtencion, idMoneda, tipoDeCambio, null, idCentroDeAtencion)
                {
                    id_comprobante = operacionGenerica.IdComprobante
                };
                return ventaYCobroEnbloque;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar venta en bloque", e);
            }
        }


        public OperationResult ConfirmarVentasAlCreditoYCobranzasCarteraCliente(UserProfileSessionData sesionDeUsuario, VentaYCobranzaCarteraDeClientes ventasCobranzasMasiva)
        {
            try
            {
                //Generando la venta,cobro en bloque wrapper 
                Transaccion ventaCobroEnBloque = GenerarVentaYCobroEnBloque(sesionDeUsuario.Empleado.Id, ventasCobranzasMasiva.IdCentroAtencion, ventasCobranzasMasiva.FechaEmision, sesionDeUsuario.TipoDeCambio.ValorVenta, Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionVentaYCobroEnBloque).Value, TransaccionSettings.Default.IdTipoTransaccionVentaYCobroEnBloque, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionVentaYCobroEnBloque, "Transaccion generada al momento de realizar una venta y cobro por vendedor");
                //Resolver las ventas masivas, obtenemos operaciiones integradas para poder trabajar con el movimeiento de almacenes
                List<OperacionIntegrada> ventasIntegradas = ResolverVentasDeVentasCobranzasMasiva(ventasCobranzasMasiva, ventaCobroEnBloque, sesionDeUsuario);
                //Obtewnemos solo las operaciones de las operaciones integradas
                List<Transaccion> transaccionesVentasCobros = ventasIntegradas.Select(v => v.Operacion).ToList();
                //Resolver las cobros masivos
                List<Transaccion> transaccionesCobros = ResolverCobrosDeVentasCobranzasMasiva(ventasCobranzasMasiva, ventaCobroEnBloque, ventasIntegradas.Select(v => v.Operacion).ToList());
                //Agregamos los cobros a las tancsacciones de ventas y cobros
                transaccionesVentasCobros.AddRange(transaccionesCobros);
                //Agregar las ventas a guardar a la venta y cobro en bloque
                ventaCobroEnBloque.Transaccion1 = transaccionesVentasCobros;
                var movimientoBienes = ventasIntegradas.SelectMany(t => t.MovimientosBienes).ToList();

                var result = AfectarInventarioFisicoYGuardarOperacion(new OperacionVentaCobroCarteraCliente { OperacionWrapper = ventaCobroEnBloque, Ventas = ventasIntegradas, Cobros = transaccionesCobros }, sesionDeUsuario);

                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar confirmar la venta y cobro masivo", e);
            }
        }

        public List<OperacionIntegrada> ResolverVentasDeVentasCobranzasMasiva(VentaYCobranzaCarteraDeClientes ventasCobranzasMasiva, Transaccion ventaYCobroEnBloque, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                List<OperacionIntegradaSerie> ventasIntegradasSerie = new List<OperacionIntegradaSerie>();
                //Obtener venta masiva  
                var ventaMasiva = ventasCobranzasMasiva.Venta;
                //Obtener la ventas masivas validas
                var ventasValidas = ventaMasiva.Detalles.Where(d => d.Cantidad > 0).ToList();
                //Obtener maximos codigos para ventas y cuotas yy salidas de mercaderia
                int codigoMaximoVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionVenta).Value, TransaccionSettings.Default.IdTipoTransaccionVenta);
                int codigoMaximoOrdenVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta);
                int codigoMaximoCuota = transaccionRepositorio.ObtenerMaximoCodigoCuota("C" + ventasCobranzasMasiva.Cobranza.FechaEmision.Year);
                int codigoMaximoSalidaMercaderia = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta).Value, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta);
                //Agrupar las ventas por cliente, porque son ventas mono detalle
                var ventasValidasAgrupadasPorCliente = ventasValidas.GroupBy(vv => vv.IdCliente);
                Codigo codigosMaximos;
                foreach (var ventasPorCliente in ventasValidasAgrupadasPorCliente)
                {
                    //Obtener la serie de acuerdo al tipo de comprobante predeterminado y si este no tiene el comprobante predeterminado, se le asigna el comprobante por defecto.
                    Serie_comprobante serieComprobanteDeVenta = ventasPorCliente.FirstOrDefault().IdComprobantePredeterminado != 0 ? transaccionRepositorio.ObtenerPrimeraSerieDeComprobanteAutonumerable(ventasPorCliente.FirstOrDefault().IdComprobantePredeterminado, ventaMasiva.IdPuntoDeVenta) : transaccionRepositorio.ObtenerPrimeraSerieDeComprobanteAutonumerable(VentasSettings.Default.IdTipoComprobantePorDefectoEnVentasYCobrosPorVendedor, ventaMasiva.IdPuntoDeVenta);
                    //Verificar si tiene la serie para realizar la operacion
                    if (serieComprobanteDeVenta == null)
                    {
                        throw new LogicaException("No existe una serie autonumerable para el tipo de comprobrobante BV, por lo tanto no se puede realizar la venta");
                    }
                    ventaMasiva.IdSerieDeComprobante = serieComprobanteDeVenta.id;
                    ventaMasiva.IdTipoDeComprobante = serieComprobanteDeVenta.id_tipo_comprobante;
                    //Generar los codigos maximos
                    codigosMaximos = new Codigo(ventaMasiva.FechaEmision, codigoMaximoVenta, codigoMaximoCuota);
                    //Obtener en registro de venta a venta masiva
                    RegistroDeVenta registroDeVenta = RegistroDeVenta.ConvertVentaCobroPorVendedor(ventaMasiva, ventasPorCliente.ToList());
                    DatosVentaIntegrada datosVentaIntegrada = new DatosVentaIntegrada(registroDeVenta);
                    //Validar y generar la transaccion venta 
                    OperacionIntegrada ventaIntegrada = GenerarVentaIntegrada(ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada, sesionDeUsuario, datosVentaIntegrada, false);

                    //Modificar los codigos de las operaaciones
                    ModificarCodigoDeTransaccionesParaVentaMasivas(ventaIntegrada, codigosMaximos);
                    OperacionIntegradaSerie ventaIntegradaSerie = new OperacionIntegradaSerie(ventaIntegrada, ventaMasiva.IdSerieDeComprobante);
                    ventasIntegradasSerie.Add(ventaIntegradaSerie);
                    codigoMaximoVenta++;
                    codigoMaximoOrdenVenta++;
                    codigoMaximoCuota++;
                    codigoMaximoSalidaMercaderia++;
                }
                ResolverComprobantes(ventasIntegradasSerie);
                return ventasIntegradasSerie.Select(vis => vis.OperacionIntegrada).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver las ventas de venta y cobranza masiva", e);
            }
        }

        public List<Transaccion> ResolverCobrosDeVentasCobranzasMasiva(VentaYCobranzaCarteraDeClientes ventasCobranzasMasiva, Transaccion ventaYCobroEnBloque, List<Transaccion> ventasAGuardar)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                List<Transaccion> cobrosAGuardar = new List<Transaccion>();
                //Obtener cobranza masiva  
                var cobranzaMasiva = ventasCobranzasMasiva.Cobranza;
                //Obtener cobros validos
                var cobrosValidos = cobranzaMasiva.Detalles.Where(d => d.Importe > 0);
                //Obtener la serie de acuerdo al tipo de comprobante nota de ingreso
                Serie_comprobante serieComprobanteDeCobranza = transaccionRepositorio.ObtenerPrimeraSerieDeComprobanteAutonumerable(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaIngreso, ventasCobranzasMasiva.Cobranza.IdCaja);
                //Verificar si tiene la serie para realizar la operacion
                if (serieComprobanteDeCobranza == null)
                {
                    throw new LogicaException("No existe una serie autonumerable para el tipo de comprobrobante NOTA DE INGRESO, por lo tanto no se puede realizar la cobranza");
                }
                //Obtener todos las cuotas con saldo 
                var todasLasCuotasDeClientesConCobrosValidos = transaccionRepositorio.ObtenerCuotasConSaldo(true, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, cobrosValidos.Select(cv => cv.IdCliente).ToArray(), TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).ToList();
                //Obtener maximo codigo de pago
                int maximoCodigoPago = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion("C_" + ventasCobranzasMasiva.Cobranza.FechaEmision.ToString("yyyy"), Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Value);
                Codigo maximosCodigos;
                foreach (var cobro in cobrosValidos)
                {
                    //Generar el comprobante de pago
                    Comprobante comprobantePago = GenerarComprobantePropioAutonumerable(serieComprobanteDeCobranza);
                    var deudas = todasLasCuotasDeClientesConCobrosValidos.Where(c => c.Transaccion.id_actor_negocio_externo == cobro.IdCliente).ToList();
                    //Unificar cuotas antiguas y actuales: agregamos las deudas del mismo cliente productos de la ventas actual, en caso existan
                    if (ventasAGuardar.Any(v => v.id_actor_negocio_externo == cobro.IdCliente))
                    {
                        //Crear objetos ventas solo para poder acceder a la orden y sus cuotas. Esta no e suna nueva transaccion . No se guardará.
                        List<Transaccion> transaccionesVentas = ventasAGuardar.Select(v => v).Where(v => v.id_actor_negocio_externo == cobro.IdCliente).ToList();
                        foreach (var transaccionVenta in transaccionesVentas)
                        {
                            Venta venta = new Venta(transaccionVenta);
                            deudas.OrderBy(c => c.fecha_emision).ThenBy(c => c.id).ToList();
                            deudas.AddRange(venta.OrdenDeVenta().Cuotas());
                        }
                    }
                    ValidarImporteAPagar(deudas.Count(), cobro.Importe, deudas.Sum(d => d.saldo));
                    maximosCodigos = new Codigo(cobranzaMasiva.FechaEmision, maximoCodigoPago);
                    var nuevoCobro = GenerarCobroDeVenta(ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada, cobranzaMasiva.IdCajero, cobranzaMasiva.IdCaja, cobro.IdCliente, maximosCodigos.SiguienteCodigoPago, comprobantePago, 1, ventaYCobroEnBloque.id_unidad_negocio, ventaYCobroEnBloque.id_moneda, cobro.Importe, cobranzaMasiva.FechaEmision, fechaActual, "Pago unificado según deudas acumuladas", MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "NN");
                    ResolverPagosCuotas(nuevoCobro, deudas, cobro.Importe);
                    cobrosAGuardar.Add(nuevoCobro);
                    maximoCodigoPago++;
                }
                return cobrosAGuardar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver los cobros de venta y cobranza masiva", e);
            }
        }
        #endregion

        #region OBTENCION DE VENTAS Y COBROS POR VENDEDOR
        public List<Deuda_Actor_Negocio> ObtenerDeudasMasivasDeCarteraDeClientes(int idPuntoDeVenta, DateTime fecha)
        {
            //si no hay ventas o cobros en el dia para tal punto de venta, traer las deudas a las 00:00hrs
            //de lo contrario, traer las ventas 1 ms despues de la ultima venta.
            //fechaMaxima
            DateTime? fechaUltimaVentaMasiva = transaccionRepositorio.ObtenerFechaInicioUltimaTransaccion(
                                                idPuntoDeVenta,
                                                TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
                                                MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                                                fecha.Date.AddDays(1).AddMilliseconds(-1),
                                                MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta,
                                                ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString()
                                               );

            //obtener la fecha del ultimo cobro por venta de algun cliente de la cartera de cliente
            DateTime? fechaUltimaCobranza = transaccionRepositorio.ObtenerFechaUltimaTransaccionDeAlgunoDeLosActoresVinculadosSegunTransaccion1(
                                              idPuntoDeVenta,
                                              TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes,
                                              fecha.Date.AddDays(1).AddMilliseconds(-1)
                                            );

            //la fecha de la deuda debe ser la mayor entre la ultima cobranza, la ultima venta.
            var fechaDeuda = (fechaUltimaVentaMasiva == null && fechaUltimaCobranza == null) ?
                              fecha : (fechaUltimaVentaMasiva != null && fechaUltimaCobranza != null) ?
                                        (
                                         (DateTime)(DateTime.Compare((DateTime)fechaUltimaVentaMasiva, (DateTime)fechaUltimaCobranza) > 0 ?
                                         fechaUltimaVentaMasiva : fechaUltimaCobranza)
                                        ).AddMilliseconds(1)
                                      : ((DateTime)(fechaUltimaVentaMasiva != null ?
                                         fechaUltimaVentaMasiva : fechaUltimaCobranza)).AddMilliseconds(1);

            return transaccionRepositorio.ObtenerDeudasActorNegocioPorVinculoActorNegocio(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idPuntoDeVenta, (int)TipoVinculo.CarteraDeCliente, fechaDeuda, MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado).ToList();
        }

        public List<VentaYCobranzaCarteraDeClientes> ObtenerVentasYCobranzasMasivas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {


                int[] idsEstadosDeTransaccion = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                List<OrdenDeVenta> ordenesDeVenta = OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsEstadosDeTransaccion, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDesde, fechaHasta).ToList());

                List<MovimientoEconomico> cobros = MovimientoEconomico.Convert(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDesde, fechaHasta).ToList());

                List<VentaMasiva> ventasMasivas = GenerarVentasMasivas(ordenesDeVenta);
                List<CobranzaMasiva> cobranzasMasivas = GenerarCobranzasMasivas(cobros);
                List<VentaYCobranzaCarteraDeClientes> ventaYCobranzaMasivas = GenerarVentaYCobranzaMasivaDeCarteraDeClientes(ventasMasivas, cobranzasMasivas);
                return ventaYCobranzaMasivas.OrderByDescending(vcm => vcm.FechaEmision).ThenByDescending(vcm => vcm.IdTransaccion).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas y cobros masivos", e);
            }
        }
        public List<VentaMasiva> GenerarVentasMasivas(List<OrdenDeVenta> ordenesDeVenta)
        {
            try
            {
                List<VentaMasiva> ventasMasivas = new List<VentaMasiva>();
                List<VentaMonoDetalle> detallesVentaMasiva;
                VentaMasiva ventaMasiva = null;
                foreach (var ordenDeVenta in ordenesDeVenta)
                {
                    detallesVentaMasiva = new List<VentaMonoDetalle>();
                    foreach (var detalle in ordenDeVenta.DetalleTransaccion())
                    {
                        detallesVentaMasiva.Add(new VentaMonoDetalle(ordenDeVenta.IdCliente, ordenDeVenta.ApellidosYNombres, detalle.Concepto_negocio.nombre, detalle.cantidad, detalle.precio_unitario));
                    }
                    ventaMasiva = new VentaMasiva(ordenDeVenta.CentroDeAtencion().Id, ordenDeVenta.CentroDeAtencion().Nombre, ordenDeVenta.Empleado().Id, ordenDeVenta.Empleado().NombreCompleto, ordenDeVenta.FechaEmision, detallesVentaMasiva)
                    {
                        IdVentaCobroEnBloque = (long)ordenDeVenta.Transaccion().Transaccion2.id_transaccion_padre
                    };
                    ventasMasivas.Add(ventaMasiva);
                }
                return ventasMasivas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar venta y cobros masivos de cartera de clientes", e);
            }
        }
        public List<CobranzaMasiva> GenerarCobranzasMasivas(List<MovimientoEconomico> cobros)
        {
            try
            {
                List<CobranzaMasiva> cobranzasMasivas = new List<CobranzaMasiva>();
                List<DetalleCobranzaMasiva> detallesCobranzaMasiva;
                CobranzaMasiva cobranzaMasiva = null;
                foreach (var cobro in cobros)
                {
                    detallesCobranzaMasiva = new List<DetalleCobranzaMasiva>
                    {
                        new DetalleCobranzaMasiva(cobro.Tercero().Id, cobro.Tercero().RazonSocial, cobro.Total)
                    };
                    if (cobros.Count > 0)
                    {
                        cobranzaMasiva = new CobranzaMasiva(cobro.CentroDeAtencion().Id, cobro.Cajero().NombreCompleto, cobro.Empleado().Id, cobro.Empleado().NombreCompleto, cobro.FechaEmision, cobro.FechaDeRegistro, detallesCobranzaMasiva)
                        {
                            IdVentaCobroEnBloque = (long)cobro.Transaccion().id_transaccion_padre
                        };
                        cobranzasMasivas.Add(cobranzaMasiva);
                    }
                }
                return cobranzasMasivas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar venta y cobros masivos de cartera de clientes", e);
            }
        }


        public List<VentaYCobranzaCarteraDeClientes> GenerarVentaYCobranzaMasivaDeCarteraDeClientes(List<VentaMasiva> ventasMasivas, List<CobranzaMasiva> cobranzasMasivas)
        {
            try
            {
                List<VentaYCobranzaCarteraDeClientes> ventasYCobranzasCarteraDeClientes = new List<VentaYCobranzaCarteraDeClientes>();
                VentaYCobranzaCarteraDeClientes ventaYCobranzaCarteraDeClientes = null;
                var IdsVentaCobroEnBloque = ventasMasivas.Select(vm => vm.IdVentaCobroEnBloque).ToList();
                IdsVentaCobroEnBloque.AddRange(cobranzasMasivas.Select(cm => cm.IdVentaCobroEnBloque).ToList());

                foreach (var idVentaCobroEnBloque in IdsVentaCobroEnBloque.Distinct())
                {
                    var ventas = ventasMasivas.Where(vm => vm.IdVentaCobroEnBloque == idVentaCobroEnBloque).ToList();
                    var cobranzas = cobranzasMasivas.Where(cm => cm.IdVentaCobroEnBloque == idVentaCobroEnBloque).ToList();
                    var ventaMasiva = (ventas.Count > 0) ? ventas.First() : null;
                    var cobranzaMasiva = (cobranzas.Count > 0) ? cobranzas.First() : null;
                    if (ventaMasiva != null) ventaMasiva.Detalles = ventas.SelectMany(v => v.Detalles).ToList();
                    if (cobranzaMasiva != null) cobranzaMasiva.Detalles = cobranzas.SelectMany(c => c.Detalles).ToList();
                    var idCentroAtencion = ventas == null ? cobranzaMasiva.IdCaja : ventaMasiva.IdPuntoDeVenta;
                    var nombreCentroAtencion = ventaMasiva == null ? cobranzaMasiva.NombreCaja : ventaMasiva.NombrePuntoDeVenta;
                    ventaYCobranzaCarteraDeClientes = new VentaYCobranzaCarteraDeClientes(idVentaCobroEnBloque, idCentroAtencion, nombreCentroAtencion, ventaMasiva, cobranzaMasiva);
                    ventasYCobranzasCarteraDeClientes.Add(ventaYCobranzaCarteraDeClientes);
                }
                return ventasYCobranzasCarteraDeClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar venta y cobros masivos de cartera de clientes", e);
            }
        }
        public VentaYCobranzaCarteraDeClientes GenerarVentaYCobranzaMasivaDeCarteraDeClientes(List<OrdenDeVenta> ordenesDeVenta, List<MovimientoEconomico> cobros)
        {
            try
            {
                //Conseguimos el centro de atencion bien del punto de venta o del centro de atencion de a cartera de clientes a donde pertenece el cliente de la cobranza
                CentroDeAtencionExtendido centroDeAtencion = ordenesDeVenta != null && ordenesDeVenta.Any() ? ordenesDeVenta.First().CentroDeAtencion()
                : (cobros != null && cobros.Any() ? new CentroDeAtencionExtendido(cobros.First().Tercero().ActorDeNegocio.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.es_vigente && van.tipo_vinculo == (int)TipoVinculo.CarteraDeCliente).Actor_negocio) : null);
                VentaMasiva ventaMasiva = null;
                CobranzaMasiva cobranzaMasivo = null;
                List<VentaMonoDetalle> detallesVentaMasiva = new List<VentaMonoDetalle>();
                //Obtener los detalles de venta masiva
                foreach (var orden in ordenesDeVenta)
                {
                    foreach (var detalle in orden.DetalleTransaccion())
                    {
                        detallesVentaMasiva.Add(new VentaMonoDetalle(orden.IdCliente, orden.ApellidosYNombres, detalle.Concepto_negocio.nombre, detalle.cantidad, detalle.precio_unitario));
                    }
                }
                //if (ordenesDeVenta.Count > 0)
                //{
                //    //Generar la venta masiva
                //    ventaMasiva = new VentaMasivaMonoConcepto(ordenesDeVenta.First().CentroDeAtencion().Id, ordenesDeVenta.First().CentroDeAtencion().Nombre, ordenesDeVenta.First().CentroDeAtencion().Id,
                //    ordenesDeVenta.First().CentroDeAtencion().Nombre, ordenesDeVenta.First().DetalleTransaccion().First().Concepto_negocio.id, ordenesDeVenta.First().DetalleTransaccion().First().Concepto_negocio.nombre,
                //    ordenesDeVenta.First().Empleado().Id, ordenesDeVenta.First().Empleado().NombreCompleto, ordenesDeVenta.First().FechaEmision, ordenesDeVenta.First().FechaDeRegistro, detallesVentaMasiva);
                //}

                if (ordenesDeVenta.Count > 0)
                {
                    //Generar la venta masiva
                    ventaMasiva = new VentaMasiva(ordenesDeVenta.First().CentroDeAtencion().Id, ordenesDeVenta.First().CentroDeAtencion().Nombre,
                    ordenesDeVenta.First().Empleado().Id, ordenesDeVenta.First().Empleado().NombreCompleto, ordenesDeVenta.First().FechaEmision, detallesVentaMasiva)
                    {
                        NombreAlmacenero = ordenesDeVenta.First().OperacionDeAlmacen().Empleado().NombreCompleto,
                        NombreAlmacen = ordenesDeVenta.First().OperacionDeAlmacen().CentroDeAtencion().Nombre,
                    };
                }

                //Obtener los detalles de cobranza 
                List<DetalleCobranzaMasiva> detallesCobranzaMasiva = new List<DetalleCobranzaMasiva>();
                foreach (var cobro in cobros)
                {
                    detallesCobranzaMasiva.Add(new DetalleCobranzaMasiva(cobro.Tercero().Id, cobro.Tercero().RazonSocial, cobro.Total));
                }
                if (cobros.Count > 0)
                {
                    //Generar la cobranza masiva
                    cobranzaMasivo = new CobranzaMasiva(cobros.First().CentroDeAtencion().Id, cobros.First().CentroDeAtencion().Nombre, cobros.First().Empleado().Id,
                        cobros.First().Empleado().NombreCompleto, cobros.First().FechaEmision, cobros.First().FechaDeRegistro, detallesCobranzaMasiva);
                }

                //Obtener el id_transaccion poadre ya sea de la orden de venta o del cobro
                //long idTransaccion = ordenesDeVenta.Select(ov => ov.Transaccion().id_transaccion_padre).FirstOrDefault() == null ? (long)cobros.First().Transaccion().id_transaccion_padre : (long)ordenesDeVenta.First().Transaccion().id_transaccion_padre;


                long idTransaccion = ordenesDeVenta.Select(ov => ov.Transaccion().Transaccion2.id_transaccion_padre).FirstOrDefault() != null ? (long)ordenesDeVenta.FirstOrDefault().Transaccion().Transaccion2.id_transaccion_padre : cobros.Select(ov => ov.Transaccion().id_transaccion_padre).FirstOrDefault() != null ? (long)cobros.FirstOrDefault().Transaccion().id_transaccion_padre : 0;

                //Agregamos a la lista de venta y cobros masivos 
                return new VentaYCobranzaCarteraDeClientes(idTransaccion, centroDeAtencion.Id, centroDeAtencion.Nombre, ventaMasiva
                , cobranzaMasivo);

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar venta y cobros masivos de cartera de clientes", e);
            }
        }

        //public VentaYCobranzaCarteraDeClientes ObtenerVentasYCobranzasMasiva(DateTime fechaDeVenta)
        //{
        //    try
        //    {
        //        int[] idsEstadosDeTransaccion = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

        //        List<OrdenDeVenta> ordenesDeVenta = OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(
        //            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsEstadosDeTransaccion, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDeVenta).ToList());

        //        List<MovimientoEconomico> cobros = MovimientoEconomico.Convert(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDeVenta).ToList());

        //        return GenerarVentaYCobranzaMasivaDeCarteraDeClientes(ordenesDeVenta, cobros);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException(String.Format("Error al intentar obtener ventas y cobranzas masivas al: {0}", fechaDeVenta), e);
        //    }
        //}

        public VentaYCobranzaCarteraDeClientes ObtenerVentasYCobranzasMasiva(int idTransaccion)
        {
            try
            {
                int[] idsEstadosDeTransaccion = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                List<OrdenDeVenta> ordenesDeVenta = OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsEstadosDeTransaccion, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), idTransaccion).ToList());

                List<MovimientoEconomico> cobros = MovimientoEconomico.Convert(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), idTransaccion).ToList());

                return GenerarVentaYCobranzaMasivaDeCarteraDeClientes(ordenesDeVenta, cobros);
            }
            catch (Exception e)
            {
                throw new LogicaException(String.Format("Error al intentar obtener ventas y cobranzas masivas al: {0}", idTransaccion), e);
            }
        }

        public List<OrdenDeVenta> ObtenerVentasMasivasPorFecha(DateTime fecha)
        {
            try
            {

                DateTime desde = new DateTime();
                desde = fecha;
                DateTime hasta = new DateTime();
                hasta = desde.AddDays(1).AddMilliseconds(-1);
                int[] idsEstadosDeTransaccion = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
                    idsEstadosDeTransaccion, AplicacionSettings.Default.TipoVenta, ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada.ToString(), desde, hasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException(String.Format("Error al intentar obtener ventas masivas al: {0}", fecha), e);

            }
        }

        public List<MovimientoEconomico> ObtenerCobrosMasivosPorFecha(DateTime fecha)
        {
            try
            {
                DateTime desde = new DateTime();
                desde = fecha;
                DateTime hasta = new DateTime();
                hasta = desde.AddDays(1).AddMilliseconds(-1);

                return MovimientoEconomico.Convert(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, AplicacionSettings.Default.TipoVenta, ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada.ToString(), desde, hasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException(String.Format("Error al intentar obtener cobros masivos al: {0}", fecha), e);

            }
        }

        public List<ReciboDeIngresoEgreso> ObtenerRecibosDeCobranzaVentaMasivas(DateTime fecha)
        {
            return null;

        }

        #endregion

        #region REPORTE DE VENTA Y COBRANZA MASIVA POR VENDEDOR

        public EstadoCuentaCliente_VentaCobro ObtenerDeudasMasivasDeCarteraDeClientes(int idPuntoDeVenta, int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            DateTime? fechaUltimaVentaMasiva = transaccionRepositorio.ObtenerFechaInicioUltimaTransaccion(idPuntoDeVenta, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde.Date.AddDays(1).AddMilliseconds(-1), MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString());
            DateTime? fechaUltimaCobranza = transaccionRepositorio.ObtenerFechaUltimaTransaccionDeAlgunoDeLosActoresVinculadosSegunTransaccion1(idPuntoDeVenta, TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, fechaDesde.Date.AddDays(1).AddMilliseconds(-1));
            var fechaDeuda = (fechaUltimaVentaMasiva == null && fechaUltimaCobranza == null) ? fechaDesde : (fechaUltimaVentaMasiva != null && fechaUltimaCobranza != null) ? ((DateTime)(DateTime.Compare((DateTime)fechaUltimaVentaMasiva, (DateTime)fechaUltimaCobranza) > 0 ? fechaUltimaVentaMasiva : fechaUltimaCobranza)).AddMilliseconds(1) : ((DateTime)(fechaUltimaVentaMasiva != null ? fechaUltimaVentaMasiva : fechaUltimaCobranza)).AddMilliseconds(1);
            //Se calcula la deuda de las cuotas pendientes de pago a laa actualidad
            var deudaDeCliente = transaccionRepositorio.ObtenerDeudaActorNegocio(idCliente, fechaDesde);
            var deudaAnterior = deudaDeCliente == null ? 0 : deudaDeCliente.TotalOrden - deudaDeCliente.TotalPagoCuota;
            //Cuotas emitidas menores, que tengan pago mayores a la fecha de interes

            //Obtener los detalles de los venta y de los cobros
            var detallesDeEstadoDeCuentaDeVentas = transaccionRepositorio.ObtenerDetallesEstadoCuentaClienteVenta(
                TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCliente, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDesde, fechaHasta).ToList();
            var detallesDeEstadoDeCuentaDeCobros = transaccionRepositorio.ObtenerDetallesEstadoCuentaClienteCobro(
                TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, idCliente, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada).ToString(), fechaDesde, fechaHasta).ToList();
            //Resolvemos los detalles de los estado de cuenta de venta y cobro
            var detallesEstadoCuenta = ResolverDetallesEstadoDeCuentaVentaCobro(deudaAnterior, detallesDeEstadoDeCuentaDeVentas, detallesDeEstadoDeCuentaDeCobros);
            var resumen = new ResumenEstadoCuentaCliente_VentaCobro
            {
                SaldoAnterior = deudaAnterior ?? 0,
                EntregaTotal = detallesDeEstadoDeCuentaDeVentas == null ? 0 : detallesDeEstadoDeCuentaDeVentas.Sum(d => d.Importe),
                CobroTotal = detallesDeEstadoDeCuentaDeCobros == null ? 0 : detallesDeEstadoDeCuentaDeCobros.Sum(d => d.Cobro)
            };
            resumen.SaldoFinal = resumen.EntregaTotal - resumen.CobroTotal;
            var estadoCuentaCliente = new EstadoCuentaCliente_VentaCobro
            {
                IdCliente = idCliente,
                Resumen = resumen,
                Detalles = detallesEstadoCuenta
            };
            return estadoCuentaCliente;
        }


        public List<DetalleEstadoCuentaCliente_VentaCobro> ResolverDetallesEstadoDeCuentaVentaCobro(decimal? deudaAnterior, List<DetalleTransaccionVentaCobro> detallesDeEstadoDeCuentaDeVentas, List<DetalleTransaccionVentaCobro> detallesDeEstadoDeCuentaDeCobros)
        {
            var idsOperacion = detallesDeEstadoDeCuentaDeVentas.Select(d => d.IdOperacion).ToList();
            idsOperacion.AddRange(detallesDeEstadoDeCuentaDeCobros.Select(d => d.IdOperacion).ToList());
            idsOperacion = idsOperacion.Distinct().ToList();
            List<DetalleEstadoCuentaCliente_VentaCobro> detallesEstadoCuenta = new List<DetalleEstadoCuentaCliente_VentaCobro>();
            foreach (var idOperacion in idsOperacion)
            {
                var detallesVenta = detallesDeEstadoDeCuentaDeVentas.Where(d => d.IdOperacion == idOperacion).ToList();
                var detalleCobro = detallesDeEstadoDeCuentaDeCobros.SingleOrDefault(d => d.IdOperacion == idOperacion);
                var saldoAnterior = detallesEstadoCuenta.Count() == 0 ? deudaAnterior ?? 0 : detallesEstadoCuenta.Last().Saldo;
                if (detallesVenta != null && detallesVenta.Count() > 0)
                {
                    List<DetalleDeVenta> detalleDeVentas = new List<DetalleDeVenta>();
                    foreach (var item in detallesVenta)
                    {
                        detalleDeVentas.Add(new DetalleDeVenta()
                        {
                            Codigo = item.Codigo,
                            Concepto = item.Concepto,
                            Cantidad = item.Cantidad,
                            Importe = item.Importe
                        });
                    }
                    DetalleEstadoCuentaCliente_VentaCobro detalleEstadoCuentaCliente = new DetalleEstadoCuentaCliente_VentaCobro
                    {
                        IdOperacion = idOperacion,
                        Fecha = detallesVenta.First().Fecha,
                        SaldoAnterior = saldoAnterior,
                        DetallesDeVenta = detalleDeVentas,
                        Total = detalleDeVentas.Sum(d => d.Importe),
                        Cobro = detalleCobro == null ? 0 : detalleCobro.Cobro,
                        Saldo = saldoAnterior + detalleDeVentas.Sum(d => d.Importe) - (detalleCobro == null ? 0 : detalleCobro.Cobro)
                    };
                    detallesEstadoCuenta.Add(detalleEstadoCuentaCliente);
                }
                else
                {
                    List<DetalleDeVenta> detalleDeVentas = new List<DetalleDeVenta>(){
                        new DetalleDeVenta()
                        {
                            Codigo = "-",
                            Concepto = "-",
                            Cantidad = 0,
                            Importe = 0
                        }
                    };

                    DetalleEstadoCuentaCliente_VentaCobro detalleEstadoCuentaCliente = new DetalleEstadoCuentaCliente_VentaCobro
                    {
                        IdOperacion = idOperacion,
                        Fecha = detalleCobro.Fecha,
                        SaldoAnterior = saldoAnterior,
                        DetallesDeVenta = detalleDeVentas,
                        Cobro = detalleCobro == null ? 0 : detalleCobro.Cobro,
                        Saldo = saldoAnterior - (detalleCobro == null ? 0 : detalleCobro.Cobro)
                    };
                    detallesEstadoCuenta.Add(detalleEstadoCuentaCliente);
                }
            }
            return detallesEstadoCuenta;
        }

        #endregion


        //public List<Cuota> ObtenerCuotasConSaldo(int[] idsClientes)
        //{
        //    try
        //    {

        //        return transaccionRepositorio.ObtenerCuotasConSaldo(true, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsClientes, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).ToList();

        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener cuotas con saldo", e);
        //    }
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Pedido;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica.Core.Pedido
{
    public class Pedido_Logica : IPedido_Logica
    {
        private readonly ITransaccionRepositorio _repositorioTransaccion;
        private readonly IPedidoRepositorio _repositorioPedido;
        private readonly IPermisos_Logica _permisos_logica;
        private readonly ICodigosOperacion_Logica _codigosOperacion_Logica;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly IActorRepositorio _actorRepositorio;
        private readonly IOperacionLogica _operacionLogica;
        private readonly ICentroDeAtencion_Logica _centroDeAtencion_Logica;
        public Pedido_Logica(ITransaccionRepositorio repositorioTransaccion, IPermisos_Logica permisos_Logica, ICodigosOperacion_Logica codigosOperacion_Logica, IPedidoRepositorio pedidoRepositorio, IActorNegocioLogica actorNegocioLogica, ITransaccionRepositorio transaccionRepositorio, IActorRepositorio actorRepositorio, IOperacionLogica operacionLogica, ICentroDeAtencion_Logica centroDeAtencion_Logica)
        {
            _repositorioTransaccion = repositorioTransaccion;
            _codigosOperacion_Logica = codigosOperacion_Logica;
            _permisos_logica = permisos_Logica;
            _repositorioPedido = pedidoRepositorio;
            _actorNegocioLogica = actorNegocioLogica;
            _transaccionRepositorio = transaccionRepositorio;
            _actorRepositorio = actorRepositorio;
            _operacionLogica = operacionLogica;
            _centroDeAtencion_Logica = centroDeAtencion_Logica;
        }
        public PrincipalPedidoData ObetenerDatosParaPedidos(UserProfileSessionData profileSessionData)
        {
            var tieneRolCajeroDeNegocio = profileSessionData.Empleado.TieneRol(ActorSettings.Default.IdRolCajero);
            var tieneRolVendedorDeNegocio = profileSessionData.Empleado.TieneRol(ActorSettings.Default.IdRolVendedor);
            var data = new PrincipalPedidoData()
            {
                FechaActual = DateTimeUtil.FechaActual(),
                TieneRolCajeroDeNegocio = tieneRolCajeroDeNegocio,
                TieneRolVendedorDeNegocio = tieneRolVendedorDeNegocio,
                ComprobanteParaPedido = (int)TipoComprobantePara.Pedido,
            };
            return data;
        }
        public void CalcularDatosDePedidoIntegrada(DatosVentaIntegrada datosPedidoIntegrada, UserProfileSessionData profileSessionData)
        {
            datosPedidoIntegrada.FechaRegistro = DateTimeUtil.FechaActual();
            datosPedidoIntegrada.Orden.FechaEmision = datosPedidoIntegrada.FechaRegistro;
            datosPedidoIntegrada.Orden.Observacion = string.IsNullOrEmpty(datosPedidoIntegrada.Orden.Observacion) ? "NINGUNO" : Regex.Replace(datosPedidoIntegrada.Orden.Observacion, @"\s+", " ");
            datosPedidoIntegrada.Orden.PuntoDeVenta = new ItemGenerico(profileSessionData.CentroDeAtencionSeleccionado.Id);
            datosPedidoIntegrada.Orden.Vendedor = new ItemGenerico(profileSessionData.Empleado.Id);
        }
        public OperationResult AgregarPedido(DatosVentaIntegrada datosPedidoIntegrada, UserProfileSessionData profileSessionData)
        {
            try
            {
                OperationResult result;
                //Resolvemos los detalles del pedido
                var detallesDePedido = ResolverDetalle(datosPedidoIntegrada.Orden.Detalles, datosPedidoIntegrada);
                var detalles_transaccion = detallesDePedido.Select(d => d.DetalleTransaccion()).ToList();
                CalcularDatosDePedidoIntegrada(datosPedidoIntegrada, profileSessionData);
                //Generar pedido contenedor
                Transaccion pedido = GenerarPedido(datosPedidoIntegrada, profileSessionData);
                pedido.Comprobante = GenerarComprobantePropioAutonumerable(datosPedidoIntegrada.Orden.Comprobante.Serie.Id);
                //Generamos la orden de pedido
                Transaccion ordenDePedido = GenerarOrdenDePedido(pedido, profileSessionData, datosPedidoIntegrada);
                ordenDePedido.Comprobante = pedido.Comprobante;
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrden = new Estado_transaccion(profileSessionData.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado, datosPedidoIntegrada.Orden.FechaEmision, "Estado inicial asignado automaticamente al confirmar un pedido");
                ordenDePedido.Estado_transaccion.Add(estadoDeLaOrden);
                //agregamos la orden de pedido en el Pedido
                pedido.Transaccion1.Add(ordenDePedido);
                //crear la transaccion
                result = _repositorioTransaccion.CrearTransaccion(pedido);
                result.information = ordenDePedido.id;
                result.objeto = new OrdenDePedido(ordenDePedido);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar el pedido", e);
            }
        }

        public OperationResult InvalidarPedido(int IdOrdenPedido, string Observacion, UserProfileSessionData profileSessionData)
        {
            try
            {
                OperationResult result = new OperationResult();
                Estado_transaccion estadoDeLaOrdenInvalidado = new Estado_transaccion(IdOrdenPedido, profileSessionData.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, DateTimeUtil.FechaActual(), Observacion);
                _repositorioTransaccion.CrearEstadoDeTransaccionAhora(estadoDeLaOrdenInvalidado);
                return result;
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al invalidar el pedido", ex);
            }
        }


        public Transaccion GenerarOrdenDePedido(Transaccion pedido, UserProfileSessionData profileSessionData, DatosVentaIntegrada pedidoP)
        {
            //crear orden de pedido
            //Transaccion ordenDePedido = new Transaccion(_codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(pedido.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(m => m.Key == PedidoSettings.Default.IdTipoTransaccionOrdenPedido).Value, PedidoSettings.Default.IdTipoTransaccionOrdenPedido), null, pedidoP.FechaRegistro, PedidoSettings.Default.IdTipoTransaccionOrdenPedido, pedido.id_unidad_negocio, true, pedidoP.Orden.FechaEmision, pedidoP.Orden.FechaEmision, pedidoP.Orden.Observacion, pedidoP.Orden.FechaEmision, profileSessionData.Empleado.Id, pedido.importe_total, profileSessionData.IdCentroDeAtencionSeleccionado, pedido.id_moneda, pedido.tipo_cambio, null, pedidoP.Orden.Cliente.Id);

            Transaccion ordenDePedido = new Transaccion(_codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(pedido.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(mt => mt.Key == PedidoSettings.Default.IdTipoTransaccionOrdenPedido).Value, PedidoSettings.Default.IdTipoTransaccionOrdenPedido), null, pedidoP.FechaRegistro, PedidoSettings.Default.IdTipoTransaccionOrdenPedido, pedido.id_unidad_negocio, true, pedidoP.Orden.FechaEmision, pedidoP.Orden.FechaEmision, pedidoP.Orden.Observacion, pedidoP.Orden.FechaEmision, pedidoP.Orden.Vendedor.Id, pedido.importe_total, pedidoP.Orden.PuntoDeVenta.Id, pedido.id_moneda, pedido.tipo_cambio, null, pedidoP.Orden.Cliente.Id, pedidoP.Orden.DescuentoGlobal, pedidoP.Orden.DescuentoPorItem, pedidoP.Orden.Anticipo, pedidoP.Orden.Gravada, pedidoP.Orden.Exonerada, pedidoP.Orden.Inafecta, pedidoP.Orden.Gratuita, pedidoP.Orden.Igv, pedidoP.Orden.Isc, pedidoP.Orden.Icbper, pedidoP.Orden.OtrosCargos, pedidoP.Orden.OtrosTributos);
            //Agregar los detalles
            ordenDePedido.AgregarDetalles(pedidoP.Orden.DetallesDeVenta());
            //Parametro entrega orden de venta 
            ordenDePedido.enum1 = pedidoP.Orden.HayBienesEnLosDetalles() ? (PedidoSettings.Default.MostrarSeccionEntregaEnPedido ? (pedidoP.MovimientoAlmacen.EntregaDiferida ? (int)IndicadorImpactoAlmacen.Diferida : (int)IndicadorImpactoAlmacen.Inmediata) : (int)IndicadorImpactoAlmacen.Inmediata) : (int)IndicadorImpactoAlmacen.NoImpactaNoBienes;
            //Agrear el parametro alias si lo tiene
            if (!String.IsNullOrEmpty(pedidoP.Orden.Cliente.Alias) && !String.IsNullOrWhiteSpace(pedidoP.Orden.Cliente.Alias) && pedidoP.Orden.Cliente.Id == ActorSettings.Default.IdClienteGenerico)
            {
                ordenDePedido.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, pedidoP.Orden.Cliente.Alias));
            }
            ordenDePedido.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIdTipoComprobanteEmitir, pedidoP.Orden.IdTipoComprobanteaEmitir.ToString()));

            if (pedidoP.Orden.Icbper>0)
            {
                ordenDePedido.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, pedidoP.Orden.NumeroBolsasDePlastico.ToString()));
                ordenDePedido.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, pedidoP.Orden.Icbper.ToString()));
            }
            if (pedidoP.Orden.UnificarDetalles)
            {
                ordenDePedido.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado, VentasSettings.Default.ActivarDetalleUnificadoPersonalizado ? pedidoP.Orden.ValorDetalleUnificado : AplicacionSettings.Default.ValorDetalleUnificado));
            }
            return ordenDePedido;
        }
        public Comprobante GenerarComprobantePropioAutonumerable(int idSerieComprobante)
        {
            Serie_comprobante serie = _repositorioTransaccion.ObtenerSerieDeComprobante(idSerieComprobante);
            Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
            serie.proximo_numero++;
            _repositorioTransaccion.MarcarSerieComoModificada(serie);
            return comprobante;
        }

        public List<DetalleDeOperacion> ResolverDetalle(List<DetalleDeOperacion> detalles, DatosVentaIntegrada datosVentaIntegrada)
        {
            if (datosVentaIntegrada.Orden.Flete > 0)
            {
                detalles.Add(new DetalleDeOperacion(ConceptoSettings.Default.IdConceptoNegocioFlete, 1, datosVentaIntegrada.Orden.Flete, datosVentaIntegrada.Orden.Flete, 0, 0, 0, null, null, null, null, false, VentasSettings.Default.MascaraDeCalculoDeNingunValorCalculado, null));
            }
            foreach (var item in detalles)
            {
                if (item.Cantidad <= 0)
                {
                    throw new LogicaException("No es posible realizar un pedido con una cantidad 0 en alguno de sus detalles");
                }
                if (item.Importe < 0)
                {
                    throw new LogicaException("El total del detalle debe ser mayor o igual a cero");
                }
                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad))
                {
                    item.Cantidad = item.Importe / item.PrecioUnitario;
                }
                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario))
                {
                    item.PrecioUnitario = item.Importe / item.Cantidad;
                }
                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe))
                {
                    item.Importe = Math.Round(item.Cantidad * item.PrecioUnitario, 2);
                }
                if (datosVentaIntegrada.Orden.VentaGravaIgv)
                {
                    item.Igv = Math.Round(item.Importe - (item.Importe / (1 + TransaccionSettings.Default.TasaIGV)), 2);
                }
            }
            return detalles;
        }

        private bool VerificarCalculadoMascaraDeCalculo(string mascaraDeCalculo, ElementoDeCalculoEnVentasEnum orden)
        {
            List<int> mascaraDeCalculoArray = mascaraDeCalculo.Select(digito => int.Parse(digito.ToString())).ToList();
            return !Convert.ToBoolean(mascaraDeCalculoArray[(int)orden]);
        }
        private Transaccion GenerarPedido(DatosVentaIntegrada pedido, UserProfileSessionData profileSessionData)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int IdUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoCambio = profileSessionData.TipoDeCambio.ValorCompra;
                //validamos la accion a realizar
                _permisos_logica.ValidarAccion(profileSessionData.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionRegistrar, PedidoSettings.Default.IdTipoTransaccionOrdenPedido, IdUnidadNegocio);
                //obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(_repositorioTransaccion.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //obtenemos el codigo
                string codigo = _codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(m => m.Key == PedidoSettings.Default.IdTipoTransaccionPedido).Value, PedidoSettings.Default.IdTipoTransaccionPedido);

                Transaccion pedidoTransaccion = new Transaccion(codigo, operacionGenerica.Id, pedido.FechaRegistro, PedidoSettings.Default.IdTipoTransaccionPedido, IdUnidadNegocio, true, pedido.Orden.FechaEmision, pedido.Orden.FechaEmision, pedido.Orden.Observacion, pedido.Orden.FechaEmision, profileSessionData.Empleado.Id, pedido.Orden.ImporteTotal, profileSessionData.IdCentroDeAtencionSeleccionado, idMoneda, tipoCambio, null, pedido.Orden.Cliente.Id);
                return pedidoTransaccion;
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al intentar generar el pedido", ex);
            }
        }

        public List<ResumenOrdenPedido> ObtenerOrdenesDePedido(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                int[] idEstados = new int[]{MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado
                };
                return _repositorioPedido.ObtenerOrdenesPedidos(FechaDesde, FechaHasta, PedidoSettings.Default.IdTipoTransaccionOrdenPedido, idEstados).OrderBy(p=>p.EstaInvalidado).ToList();
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al Listar los Pedidos", ex);
            }
        }

        public DatosVentaIntegrada ObtenerOrdenDePedido(int idPedido)
        {
            try
            {
                DatosVentaIntegrada PedidoEditar = new DatosVentaIntegrada();
                var OrdenDePedido = _repositorioPedido.ObtenerOrdenDePedido(idPedido);
                PedidoEditar.Orden = new DatosOrdenVenta()
                {
                    Id = OrdenDePedido.id,
                    AplicarIGVCuandoEsAmazonia = OrdenDePedido.importe8 > 0,
                    Cliente = _actorNegocioLogica.ObtenerActorComercialPorId(ActorSettings.Default.IdRolCliente, OrdenDePedido.id_actor_negocio_externo),

                    Comprobante = new ComprobanteDeNegocio_
                    {
                        Id = OrdenDePedido.id_comprobante,
                        Tipo = new ItemGenerico
                        {
                            Id = OrdenDePedido.Comprobante.id_tipo_comprobante,
                            Codigo = OrdenDePedido.Comprobante.Detalle_maestro.codigo,
                            Nombre = OrdenDePedido.Comprobante.Detalle_maestro.nombre
                        },
                        Serie = new SerieComprobante_
                        {
                            Id = (int)OrdenDePedido.Comprobante.id_serie_comprobante,
                            Nombre = OrdenDePedido.Comprobante.numero_serie,
                            EsAutonumerica = OrdenDePedido.Comprobante.Serie_comprobante.es_autonumerable
                        },
                    },
                    Detalles = DetalleDeOperacion.Convert(OrdenDePedido.Detalle_transaccion.Where(dp=>dp.id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList()),
                    Flete = OrdenDePedido.Detalle_transaccion.SingleOrDefault(f => f.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioFlete) == null ? 0 : OrdenDePedido.Detalle_transaccion.SingleOrDefault(f => f.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioFlete).total,
                    Observacion = OrdenDePedido.comentario,
                    FechaEmision = OrdenDePedido.fecha_inicio,
                    Total = OrdenDePedido.importe_total,
                    IdEstado = (int)OrdenDePedido.id_estado_actual,
                    IdTransaccionPadre = (int)OrdenDePedido.id_transaccion_padre,
                };

                var parametroAlias = OrdenDePedido.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente);
                PedidoEditar.Orden.Cliente.Alias = parametroAlias != null ? Convert.ToString(parametroAlias.valor) : "";
                var parametroTipoComprobanteAEmitir = OrdenDePedido.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdTipoComprobanteEmitir);
                PedidoEditar.Orden.IdTipoComprobanteaEmitir = parametroTipoComprobanteAEmitir != null ? Convert.ToInt32(parametroTipoComprobanteAEmitir.valor) : 0;
                var ParametroIcbper = OrdenDePedido.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper);
                PedidoEditar.Orden.Icbper = ParametroIcbper != null ? Convert.ToDecimal(ParametroIcbper.valor) : 0;
                var ParametroDetalleUnificado = OrdenDePedido.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado);
                PedidoEditar.Orden.UnificarDetalles = ParametroDetalleUnificado != null ? true : false;
                PedidoEditar.Orden.ValorDetalleUnificado = ParametroDetalleUnificado != null ? ParametroDetalleUnificado.valor : string.Empty;

                PedidoEditar.MovimientoAlmacen = new DatosMovimientoDeAlmacen()
                {
                    EntregaDiferida = OrdenDePedido.enum1 == 1,
                };
                return PedidoEditar;
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al obtener el pedido", ex);
            }
        }

        public OrdenDePedido ObtenerOrdenDePedidoComprobante(long idPedido)
        {
            try
            {
                return (new OrdenDePedido(_transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idPedido)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public OperationResult EditarPedido(DatosVentaIntegrada datosPedidoIntegrada, UserProfileSessionData profileSessionData)
        {
            try
            {
                OperationResult result;
                // resolver detalles de la operacion
                var detallesDePedidos = ResolverDetalle(datosPedidoIntegrada.Orden.Detalles, datosPedidoIntegrada);
                var detalles_transaccion = detallesDePedidos.Select(d => d.DetalleTransaccion()).ToList();
                CalcularDatosDePedidoIntegrada(datosPedidoIntegrada, profileSessionData);
                //buscamos la transaccion
                Transaccion dbPedido = _repositorioPedido.ObtenerTransaccion((int)datosPedidoIntegrada.Orden.IdTransaccionPadre);
                Transaccion pedido = GenerarPedido(datosPedidoIntegrada, profileSessionData);
                pedido.id = (int)datosPedidoIntegrada.Orden.IdTransaccionPadre;
                pedido.id_comprobante = datosPedidoIntegrada.Orden.Comprobante.Id;
                pedido.codigo = dbPedido.codigo;
                //Generamos la orden de pedido
                Transaccion dbOrdenPedido = _repositorioPedido.ObtenerTransaccion((int)datosPedidoIntegrada.Orden.Id);
                Transaccion ordenPedido = GenerarOrdenDePedido(pedido, profileSessionData, datosPedidoIntegrada);
                ordenPedido.id = datosPedidoIntegrada.Orden.Id;
                ordenPedido.id_transaccion_padre = datosPedidoIntegrada.Orden.IdTransaccionPadre;
                ordenPedido.id_comprobante = datosPedidoIntegrada.Orden.Comprobante.Id;
                ordenPedido.id_estado_actual = datosPedidoIntegrada.Orden.IdEstado;
                ordenPedido.codigo = dbOrdenPedido.codigo;
                ordenPedido.Parametro_transaccion.SingleOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdTipoComprobanteEmitir).id = dbOrdenPedido.Parametro_transaccion.SingleOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdTipoComprobanteEmitir).id;


                foreach( var parametroOrdenPedido in ordenPedido.Parametro_transaccion)
                {
                    parametroOrdenPedido.id_transaccion = datosPedidoIntegrada.Orden.Id;
                    parametroOrdenPedido.id = dbOrdenPedido.Parametro_transaccion.SingleOrDefault(pt => pt.id_parametro == parametroOrdenPedido.id_parametro) == null ? 0 : dbOrdenPedido.Parametro_transaccion.SingleOrDefault(pt => pt.id_parametro == parametroOrdenPedido.id_parametro).id;
                }

                //Agregamos a los detalles de la orden el id de transaccion del pedido a actualizar
                ordenPedido.Detalle_transaccion.ToList().ForEach(d => d.id_transaccion = datosPedidoIntegrada.Orden.Id);
                //Agregamos la orden de cotizacion en la cotizacion
                pedido.Transaccion1.Add(ordenPedido);
                //Editamos la trnasaccion
                result = _repositorioTransaccion.ActualizarTransaccionTransaccion1DetallesParametro(pedido);
                result.information = pedido.Transaccion1.First().id;
                ordenPedido.Comprobante = new Comprobante();
                result.objeto = new OrdenDePedido(ordenPedido);
                return result;
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al confirmar la cotizacion", ex);

            }

        }
        public OrdenDePedido ObtenerOrdenDePedidoParaImprimir(OrdenDePedido ordenDePedido)
        {
            try
            {

                int[] idsActoresNegocio = { ordenDePedido.Transaccion().id_actor_negocio_interno };
                List<Actor_negocio> actoresDeNegocio = _actorRepositorio.ObtenerActoresDeNegocio(idsActoresNegocio).ToList();
                ordenDePedido.Transaccion().Actor_negocio2 = actoresDeNegocio.Single(an => an.id == ordenDePedido.Transaccion().id_actor_negocio_interno);
                return ordenDePedido;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public OperationResult ConfirmarPedido(ModoOperacionEnum tipoVenta,UserProfileSessionData sesionDeUsuario , DatosVentaIntegrada pedido)
        {
            try
            {
                pedido.NuevoEstado = new Estado_transaccion(pedido.Orden.Id,sesionDeUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, DateTimeUtil.FechaActual(), "Pedido Confirmado al realizarce una venta");
                return _operacionLogica.ConfirmarVentaIntegrada(tipoVenta, sesionDeUsuario, pedido);
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al validar el pedido", ex);
            }
        }
        #region Reportes

        public List<PedidosInvalidados> ObtenerReportePedidosInvalidados(DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosVenta, bool todasLosPuntosVenta, int idEstablecimientoComercial)
        {
            List<PedidosInvalidados> pedidosInvalidados = new List<PedidosInvalidados>();
            idsPuntosVenta = todasLosPuntosVenta ? _centroDeAtencion_Logica.ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial(idEstablecimientoComercial).Select(pv=>pv.Id).ToArray() : idsPuntosVenta;
            pedidosInvalidados = _repositorioPedido.ObtenerOrdenesDePedidoInvalidados(fechaDesde, fechaHasta, idsPuntosVenta).ToList();
            return pedidosInvalidados;
        }



        #endregion


    }
}

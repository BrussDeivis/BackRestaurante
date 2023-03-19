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
        #region METODOS GENERADORES DE VENTA

        /// <summary>
        /// Resolver los detalles de la orden de venta segun corresponda, tambien agrega como detalle el flete, calcula el igv para todos los detalles siempre que corresponda
        /// </summary>
        /// <param name="detalles"></param>
        /// <param name="datosVentaIntegrada"></param>
        /// <returns></returns>
        public void ResolverDetalles(List<DetalleDeOperacion> detalles, DatosVentaIntegrada datosVentaIntegrada)
        {
            //Agregar el flete como un detalle de operacion si es mayor a 0  
            if (datosVentaIntegrada.Orden.Flete > 0)
            {
                detalles.Add(new DetalleDeOperacion(ConceptoSettings.Default.IdConceptoNegocioFlete, 1, datosVentaIntegrada.Orden.Flete, datosVentaIntegrada.Orden.Flete, 0, 0, 0, null, null, null, null, false, VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas, null));
            }
            //Validar los montos de los detalles y calcular el igv de los detalles de la venta
            foreach (var item in detalles)
            {
                if (item.Cantidad <= 0)
                {
                    throw new LogicaException("No es posible realizar una venta con cantidad 0 en alguno de sus detalles");
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
                if (VentasSettings.Default.PermitirRegistroDePlacaEnVenta)
                {
                    item.Registro = datosVentaIntegrada.Orden.Placa;
                }
            }
        }

        /// <summary>
        /// Verificar la mascara para el calculo de los valores de venta
        /// </summary>
        /// <param name="mascaraDeCalculo"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        private bool VerificarCalculadoMascaraDeCalculo(string mascaraDeCalculo, ElementoDeCalculoEnVentasEnum orden)
        {
            List<int> mascaraDeCalculoArray = mascaraDeCalculo.Select(digito => int.Parse(digito.ToString())).ToList();
            //Retornamos si el valor de mascara es igual a 1
            return !Convert.ToBoolean(mascaraDeCalculoArray[(int)orden]);
        }

        /// <summary>
        /// Calcular los datos que se tendran venta integrada (segmentacion y calculos de los detalles, determinar la fecha de emision, resolver )
        /// </summary>
        /// <param name="datosVentaIntegrada"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <returns></returns>
        private void CalcularDatosDeVentaIntegrada(ModoOperacionEnum tipoDeVenta, DatosVentaIntegrada datosVentaIntegrada, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                datosVentaIntegrada.Orden.Observacion = string.IsNullOrEmpty(datosVentaIntegrada.Orden.Observacion) ? "NINGUNO" : Regex.Replace(datosVentaIntegrada.Orden.Observacion, @"\s+", " ");
                //Verificar el punto de venta, vendedor, caja, cajero, almcen, almacenero, verificando el modo de venta (venta modo caja)
                datosVentaIntegrada.Orden.PuntoDeVenta = (datosVentaIntegrada.EsVentaModoCaja || tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada) ? datosVentaIntegrada.Orden.PuntoDeVenta : new ItemGenerico(sesionDeUsuario.CentroDeAtencionSeleccionado.Id);
                datosVentaIntegrada.Orden.Vendedor = (datosVentaIntegrada.EsVentaModoCaja || tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada) ? datosVentaIntegrada.Orden.Vendedor : new ItemGenerico(sesionDeUsuario.Empleado.Id);
                datosVentaIntegrada.Pago.Caja = (tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada) ? datosVentaIntegrada.Pago.Caja : new ItemGenerico(sesionDeUsuario.CentroDeAtencionSeleccionado.Id);
                datosVentaIntegrada.Pago.Cajero = (tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada) ? datosVentaIntegrada.Pago.Cajero : new ItemGenerico(sesionDeUsuario.Empleado.Id);
                datosVentaIntegrada.MovimientoAlmacen.Almacen = (tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada || (datosVentaIntegrada.EsVentaModoCaja && VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja)) ? datosVentaIntegrada.MovimientoAlmacen.Almacen : new ItemGenerico(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                datosVentaIntegrada.MovimientoAlmacen.Almacenero = (tipoDeVenta == ModoOperacionEnum.VentaIntegradaMasivaDigitada || tipoDeVenta == ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada || (datosVentaIntegrada.EsVentaModoCaja && VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja)) ? datosVentaIntegrada.MovimientoAlmacen.Almacenero : new ItemGenerico(sesionDeUsuario.Empleado.Id);
                //Segmentar y resolver los calculos de los detalles de la orden de venta 
                //datosVentaIntegrada.Orden.Detalles = SegmentarDetallesSegunLotesExistentes(datosVentaIntegrada.Orden.Detalles);
                ResolverDetalles(datosVentaIntegrada.Orden.Detalles, datosVentaIntegrada);
                //Obtener la fecha de registro de la venta, esta es la fecha actual que se obtiene del repositorio
                datosVentaIntegrada.FechaRegistro = DateTimeUtil.FechaActual();
                //Deteterminar la fecha de emision de la venta, en el caso de no sea una venta con fecha pasada se tomara la fecha de registro
                datosVentaIntegrada.Orden.FechaEmision = datosVentaIntegrada.Orden.EsVentaPasada ? datosVentaIntegrada.Orden.FechaEmision : datosVentaIntegrada.FechaRegistro;
                //Establecer medio de pago 
                datosVentaIntegrada.Pago.Traza.MedioDePago = datosVentaIntegrada.Pago.Traza.MedioDePago ?? new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo);
                //Determinar la traza de pago
                datosVentaIntegrada.Pago.Traza.Info = datosVentaIntegrada.Pago.Traza.Info ?? new InfoPago();
                if (datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos || datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta)
                {
                    datosVentaIntegrada.Pago.Traza.Info.EntidadFinanciera = new ItemGenerico { Id = int.Parse(datosVentaIntegrada.Pago.Traza.Info.CuentaBancaria.Valor) };
                }
                datosVentaIntegrada.Pago.Traza.Info.EntidadFinanciera = Diccionario.IdsMediosDePagoQueTienenEntidadBancaria.Contains(datosVentaIntegrada.Pago.Traza.MedioDePago.Id) ? datosVentaIntegrada.Pago.Traza.Info.EntidadFinanciera : new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                datosVentaIntegrada.Pago.Traza.Info.OperadorTarjeta = datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito ? datosVentaIntegrada.Pago.Traza.Info.OperadorTarjeta : new ItemGenerico();
                datosVentaIntegrada.Pago.Traza.Info.Observacion = string.IsNullOrEmpty(datosVentaIntegrada.Pago.Traza.Info.Observacion) ? "NINGUNO" : datosVentaIntegrada.Pago.Traza.Info.Observacion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar calcular los datos de venta integrada", e);
            }
        }

        /// <summary>
        /// Validar la accion a realizar, validar los datos ingresados de la venta
        /// </summary>
        /// <param name="datosVentaIntegrada"></param>
        /// <param name="configConfirmacionDeVenta"></param>
        private void ValidarVenta(DatosVentaIntegrada datosVentaIntegrada, ConfiguracionAccion configConfirmacionDeVenta)
        {
            permisos_Logica.ValidarAccion(datosVentaIntegrada.Orden.Vendedor.Id, configConfirmacionDeVenta.IdAccionDeVenta, configConfirmacionDeVenta.IdTipoTransaccionOrdenDeVenta, configConfirmacionDeVenta.IdUnidadDeNegocioTransversal);
            //Validar los valores ingresados
            if (datosVentaIntegrada.Orden.Comprobante.Tipo.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && datosVentaIntegrada.Orden.Cliente.Id == ActorSettings.Default.IdClienteGenerico && datosVentaIntegrada.Orden.ImporteTotal >= FacturacionElectronicaSettings.Default.MontoMaximoAVenderCuandoClienteNoEstaIdenticicado)
            {
                throw new LogicaException("Al emitir una Boleta de venta con montos iguales o mayores a S/ 700.00, El cliente debe estar correctamente identificado");
            }
            if (datosVentaIntegrada.Orden.Comprobante.Tipo.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && actorRepositorio.obtenerActorDeNegocio(datosVentaIntegrada.Orden.Cliente.Id, ActorSettings.Default.IdRolCliente).Actor.id_documento_identidad != ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
            {
                throw new LogicaException("Al emitir una factura el cliente debe tener RUC");
            }
            if (datosVentaIntegrada.Orden.Comprobante.Tipo.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && datosVentaIntegrada.Orden.Cliente.Id == ActorSettings.Default.IdClienteGenerico)
            {
                throw new LogicaException("Al emitir una factura, el cliente debe estar correctamente identificado");
            }
            if (datosVentaIntegrada.Orden.ImporteTotal <= 0)
            {
                throw new LogicaException("Al realizar una venta el monto total de la venta debe de ser mayor a 0.00");
            }
            if ((datosVentaIntegrada.Orden.Comprobante.Tipo.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta || datosVentaIntegrada.Orden.Comprobante.Tipo.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura) && datosVentaIntegrada.Orden.Observacion.Length > 200)
            {
                throw new LogicaException("Al emitir una boleta o una factura la observacion no puede exceder a 200 caracteres");
            }
            if (datosVentaIntegrada.Orden.EsVentaPasada)
            {
                Transaccion transaccion = transaccionRepositorio.ObtenerTransaccionDeUltimoComprobante(datosVentaIntegrada.Orden.Comprobante.Serie.Id);
                if (transaccion != null)
                    if (transaccion.fecha_inicio > datosVentaIntegrada.Orden.FechaEmision)
                    {
                        throw new LogicaException("Al realizar una venta con fecha ingresada verificar que la fecha sea despues de la fecha del ultimo comprobante");
                    }
            }
            if (VentasSettings.Default.PermitirVentasConSoloBienesOSoloServicios)
            {
                var validadorBienServicio = conceptoRepositorio.EsBien(datosVentaIntegrada.Orden.Detalles.First().Producto.Id);
                foreach (var detalle in datosVentaIntegrada.Orden.Detalles)
                {
                    if (validadorBienServicio != conceptoRepositorio.EsBien(detalle.Producto.Id))
                    {
                        throw new LogicaException("No se puede realizar ventas que contengan bienes y servicios en un mismo comprobante");
                    }
                }
            }
        }

        /// <summary>
        /// Generar la transaccion venta y agrega el comprobante a la transaccion venta
        /// </summary>
        /// <param name="datosVentaIntegrada"></param>
        /// <param name="comprobante"></param>
        /// <param name="configConfirmacionDeVenta"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <returns></returns>
        private Transaccion GenerarVenta(DatosVentaIntegrada datosVentaIntegrada, Comprobante comprobante, ConfiguracionAccion configConfirmacionDeVenta, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                //Obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Obtener el codigo para la venta 
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == configConfirmacionDeVenta.IdTipoTransaccionVenta).Value, configConfirmacionDeVenta.IdTipoTransaccionVenta);
                //Crear la transaccion venta
                Transaccion venta = new Transaccion(codigo, operacionGenerica.Id, datosVentaIntegrada.FechaRegistro, configConfirmacionDeVenta.IdTipoTransaccionVenta, configConfirmacionDeVenta.IdUnidadDeNegocioTransversal, true, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.Observacion, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.Vendedor.Id, datosVentaIntegrada.Orden.ImporteTotal, datosVentaIntegrada.Orden.PuntoDeVenta.Id, configConfirmacionDeVenta.IdMonedaSoles, sesionDeUsuario.TipoDeCambio.ValorVenta, null, datosVentaIntegrada.Orden.Cliente.Id)
                {
                    Comprobante = comprobante
                };
                return venta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar venta", e);
            }
        }

        /// <summary>
        /// Generar la transaccion orden de venta, agregar los detalles a la orden, agegar el estado de la orden y resolver sus parametros de configuracion
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="tipoDeVenta"></param>
        /// <param name="datosVentaIntegrada"></param>
        /// <param name="configConfirmacionDeVenta"></param>
        /// <returns></returns>
        private Transaccion GenerarOrdenDeVenta(Transaccion venta, ModoOperacionEnum tipoDeVenta, DatosVentaIntegrada datosVentaIntegrada, ConfiguracionAccion configConfirmacionDeVenta)
        {
            int IdTipoTransaccionOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
            //Creamos una orden de venta
            Transaccion ordenDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(venta.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(mt => mt.Key == IdTipoTransaccionOrdenDeVenta).Value, IdTipoTransaccionOrdenDeVenta), null, datosVentaIntegrada.FechaRegistro, IdTipoTransaccionOrdenDeVenta, venta.id_unidad_negocio, true, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.Observacion, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.Vendedor.Id, venta.importe_total, datosVentaIntegrada.Orden.PuntoDeVenta.Id, venta.id_moneda, venta.tipo_cambio, null, datosVentaIntegrada.Orden.Cliente.Id, datosVentaIntegrada.Orden.DescuentoGlobal, datosVentaIntegrada.Orden.DescuentoPorItem, datosVentaIntegrada.Orden.Anticipo, datosVentaIntegrada.Orden.Gravada, datosVentaIntegrada.Orden.Exonerada, datosVentaIntegrada.Orden.Inafecta, datosVentaIntegrada.Orden.Gratuita, datosVentaIntegrada.Orden.Igv, datosVentaIntegrada.Orden.Isc, datosVentaIntegrada.Orden.Icbper, datosVentaIntegrada.Orden.OtrosCargos, datosVentaIntegrada.Orden.OtrosTributos)
            {
                //Agregamos el comprobante a la orden de venta
                Comprobante = venta.Comprobante,
            };
            //Establecemos los puntos ganados por la venta realizada viendo si no es una venta con medio de pago puntos y si no es una venta al cliente varios
            if (VentasSettings.Default.GenerarPuntosEnVentas && datosVentaIntegrada.Pago.Traza.MedioDePago.Id != MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos && datosVentaIntegrada.Orden.Cliente.Id != ActorSettings.Default.IdClienteGenerico)
            {
                var puntosGanados = Convert.ToInt32(Math.Truncate(ordenDeVenta.importe_total / VentasSettings.Default.ImporteDeVentaParaGenerarUnPunto));
                var puntosAcumulados = ObtenerPuntosDeCliente(datosVentaIntegrada.Orden.Cliente.Id).PuntosPorCanjear + puntosGanados;
                ordenDeVenta.EstablecerCantidadesPuntos(puntosGanados, 0, puntosGanados, puntosAcumulados);
            }
            if (datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
            {
                var puntosAcumulados = ObtenerPuntosDeCliente(datosVentaIntegrada.Orden.Cliente.Id).PuntosPorCanjear - datosVentaIntegrada.Pago.Traza.Info.PuntosCajeados;
                ordenDeVenta.EstablecerCantidadesPuntos(0, 0, 0, puntosAcumulados);
            }
            //Agregamos la informacion a la orden venta
            if (!string.IsNullOrEmpty(datosVentaIntegrada.Orden.Informacion))
            {
                ordenDeVenta.informacion = datosVentaIntegrada.Orden.Informacion;
            }
            //Agregamos el grupo de clientes, si esta seleccionado alguno
            if (datosVentaIntegrada.Orden.Cliente.SeleccionarGrupo)
            {
                if (!datosVentaIntegrada.Orden.Cliente.NingunGrupo)
                    ordenDeVenta.id_actor_negocio_externo1 = datosVentaIntegrada.Orden.Cliente.Grupo.Id;
            }
            //Agregamos los detalles
            ordenDeVenta.AgregarDetalles(datosVentaIntegrada.Orden.DetallesDeVenta());
            //Agregamos el estado de la orden por defecto
            Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(datosVentaIntegrada.Orden.Vendedor.Id, configConfirmacionDeVenta.IdEstadoDeVenta, datosVentaIntegrada.FechaRegistro, configConfirmacionDeVenta.ComentarioEstadoDeVenta);
            ordenDeVenta.Estado_transaccion.Add(estadoDeLaOrdenDeVenta);
            //Resolver los parametros que tiene la orden de venta
            ResolverParametrosDeOrdenDeVenta(ordenDeVenta, tipoDeVenta, datosVentaIntegrada);
            //Resolver el estado de la orden como orden de almacen 
            ordenDeVenta.enum1 = datosVentaIntegrada.Orden.HayBienesEnLosDetalles() ? (VentasSettings.Default.MostrarSeccionEntregaEnVenta ? (datosVentaIntegrada.MovimientoAlmacen.EntregaDiferida ? (int)IndicadorImpactoAlmacen.Diferida :  (int)IndicadorImpactoAlmacen.Inmediata) : (int)IndicadorImpactoAlmacen.Inmediata) : (int)IndicadorImpactoAlmacen.NoImpactaNoBienes;
            //Resolver el almacen de orden como orden de almacen y el estado de transaccion de la orden de almacen
            if (datosVentaIntegrada.Orden.HayBienesEnLosDetalles())
            {
                ordenDeVenta.id_actor_negocio_interno1 = datosVentaIntegrada.MovimientoAlmacen.Almacen.Id;
                Estado_transaccion estadoOrdenAlmacen = new Estado_transaccion(datosVentaIntegrada.MovimientoAlmacen.Almacenero.Id, datosVentaIntegrada.MovimientoAlmacen.EntregaDiferida ? MaestroSettings.Default.IdDetalleMaestroEstadoPendiente : MaestroSettings.Default.IdDetalleMaestroEstadoCompletada, datosVentaIntegrada.FechaRegistro, "Estado asignado al confirmar la venta");
                ordenDeVenta.Estado_transaccion.Add(estadoOrdenAlmacen);
            }
            return ordenDeVenta;
        }

        /// <summary>
        /// Resolver los parametros de la orden de venta (tipos de operacion, modo de pago, alias, detalle unificado, hay salida de mercaderia, icbper)
        /// </summary>
        /// <param name="ordenDeVenta"></param>
        /// <param name="tipoDeVenta"></param>
        /// <param name="datosVentaIntegrada"></param>
        private void ResolverParametrosDeOrdenDeVenta(Transaccion ordenDeVenta, ModoOperacionEnum tipoDeVenta, DatosVentaIntegrada datosVentaIntegrada)
        {
            //Agregar el parametro tipo de venta
            ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)tipoDeVenta).ToString()));
            //Agregar el parametro modo de pago
            ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)datosVentaIntegrada.Pago.ModoDePago).ToString()));
            //Agregar el parametro alias de cliente si lo tiene
            if (!String.IsNullOrEmpty(datosVentaIntegrada.Orden.Cliente.Alias) && !String.IsNullOrWhiteSpace(datosVentaIntegrada.Orden.Cliente.Alias) && datosVentaIntegrada.Orden.Cliente.Id == ActorSettings.Default.IdClienteGenerico)
            {
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, datosVentaIntegrada.Orden.Cliente.Alias));
            }
            //Agregar el parametro detalle unificado si lo tiene
            if (datosVentaIntegrada.Orden.UnificarDetalles)
            {
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado, VentasSettings.Default.ActivarDetalleUnificadoPersonalizado ? datosVentaIntegrada.Orden.ValorDetalleUnificado : AplicacionSettings.Default.ValorDetalleUnificado));
            }
            //Agregar el parametro de salida de mercadeia si lo tiene
            if (datosVentaIntegrada.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia)
            {
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTieneGuiaDeRemision, "1"));
            }
            //Agregar el parametro icbper y numero de bolsas de plastico si lo tienen
            if (datosVentaIntegrada.Orden.Icbper > 0)
            {
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico, datosVentaIntegrada.Orden.NumeroBolsasDePlastico.ToString()));
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, datosVentaIntegrada.Orden.Icbper.ToString()));
            }
        }

        /// <summary>
        /// Resolver las cuotas de la venta integrada de acuerdo el modo de pago que se tenga
        /// </summary>
        /// <param name="ordenDeVenta"></param>
        /// <param name="datosVentaIntegrada"></param>
        public void ResolverCuotasDeVentaIntegrada(Transaccion ordenDeVenta, DatosVentaIntegrada datosVentaIntegrada)
        {
            try
            {
                //Verfificar si el modo de pago es credito configurado para la generacion de cuotoas sino se crea una unica cuota por defecto
                if (datosVentaIntegrada.Pago.ModoDePago == ModoPago.CreditoConfigurado)
                {
                    int cont = 1;
                    foreach (var item in datosVentaIntegrada.Pago.CuotasDeVenta())
                    {
                        Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, datosVentaIntegrada.Orden.FechaEmision.Year) + "_" + cont++, datosVentaIntegrada.Orden.FechaEmision, item.fecha_vencimiento, item.capital, item.interes, item.total, "Cuota generada numero " + cont, true, item.cuota_inicial);
                        ordenDeVenta.Cuota.Add(cuota);
                    }
                    //cuadramos decimales en la última cuota
                    var cuotas = ordenDeVenta.Cuota;
                    var diferencia = ordenDeVenta.importe_total - cuotas.Sum(c => c.total);
                    cuotas.Last().total = cuotas.Last().capital = cuotas.Last().total + diferencia;

                }
                else
                {
                    //Crear la cuota unica, dependiendo de el parametro hayIngresoDinero se vera si la cuota se cobrara o sera una cuenta por cobrar
                    Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, datosVentaIntegrada.Orden.FechaEmision.Year) + "_" + 1, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Pago.ModoDePago == ModoPago.CreditoRapido ? datosVentaIntegrada.Orden.FechaEmision.AddDays(VentasSettings.Default.DiasDeVencimientoEnCreditoRapidoDeVenta) : datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.Orden.ImporteTotal, "Unica cuota generada de forma automática", true);
                    //Agregar el estado confirmado a la cuota
                    ordenDeVenta.Cuota.Add(cuota);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver la generacion de cuotas", e);
            }
        }

        /// <summary>
        /// Generar el ingreso de dinero de la venta integrada, si hay ingreso de dinero
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="ordenDeVenta"></param>
        /// <param name="datosVentaIntegrada"></param>
        /// <returns></returns>
        public Transaccion GenerarIngresoDeDineroDeVentaIntegrada(Transaccion venta, Transaccion ordenDeVenta, DatosVentaIntegrada datosVentaIntegrada)
        {
            try
            {
                //conseguir entidad bancaria para los medios de pago que no lo traen desde la interfaz
                Transaccion ingresoDeDinero = null;
                //Verificar si hay ingreso de dinero para realizar el ingreso de dinero
                if (datosVentaIntegrada.Pago.HayIngresoDinero)
                {
                    var esCreditoRapidoConPagoInicial = (datosVentaIntegrada.Pago.ModoDePago == ModoPago.CreditoRapido && datosVentaIntegrada.Pago.Inicial > 0);
                    //Obtener la cuota de la que se realizara el pago, si es al contado 
                    Cuota cuotaACobrar = (datosVentaIntegrada.Pago.ModoDePago == ModoPago.Contado || esCreditoRapidoConPagoInicial) ? ordenDeVenta.Cuota.First() : ordenDeVenta.Cuota.Single(c => c.cuota_inicial);
                    cuotaACobrar.SetPagoACuenta(esCreditoRapidoConPagoInicial ? datosVentaIntegrada.Pago.Inicial : cuotaACobrar.total);
                    ValidarImporteAPagar(1, cuotaACobrar.total, venta.importe_total);
                    ingresoDeDinero = GenerarCobroDeVenta(datosVentaIntegrada.Pago.Cajero.Id, datosVentaIntegrada.Pago.Caja.Id, datosVentaIntegrada.Orden.Cliente.Id, CodigoPago(cuotaACobrar), venta.Comprobante, venta.tipo_cambio, venta.id_unidad_negocio, venta.id_moneda, cuotaACobrar.pago_a_cuenta, datosVentaIntegrada.Orden.FechaEmision, datosVentaIntegrada.FechaRegistro, datosVentaIntegrada.Orden.Observacion, datosVentaIntegrada.Pago.Traza.MedioDePago.Id, datosVentaIntegrada.Pago.Traza.Info.EntidadFinanciera.Id, datosVentaIntegrada.Pago.Traza.Info.OperadorTarjeta.Id, datosVentaIntegrada.Pago.Traza.Info.Observacion);
                    //Validar los medios de pago y actualizar transaccion de ingreso de dinero
                    if (datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
                    {
                        ingresoDeDinero.cantidad1 = datosVentaIntegrada.Pago.Traza.Info.PuntosCajeados;
                    }
                    if (datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta || datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos)
                    {
                        ingresoDeDinero.id_actor_negocio_interno1 = datosVentaIntegrada.Pago.Traza.Info.CuentaBancaria.Id;
                    }
                    VincularPagoConLaCuota(ingresoDeDinero, cuotaACobrar, cuotaACobrar.pago_a_cuenta);
                    if (!string.IsNullOrEmpty(datosVentaIntegrada.Pago.Traza.Info.InformacionJson)) ingresoDeDinero.Traza_pago.First().extension_json = datosVentaIntegrada.Pago.Traza.Info.InformacionJson;
                }
                return ingresoDeDinero;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver la generación del ingreso de dinero", e);
            }
        }

        /// <summary>
        /// Generar la(s) salidad(as) de mercaderia de una venta integrada
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="ordenDeVenta"></param>
        /// <param name="datosVentaIntegrada"></param>
        /// <returns></returns>
        public List<Transaccion> GenerarSalidaDeMercaderiaDeVentaIntegrada(Transaccion venta, DatosVentaIntegrada datosVentaIntegrada, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                List<Transaccion> salidasDeMercaderia = new List<Transaccion>();
                //Verificar que la operacion que se realizo no sea con entrega diferida
                if (!datosVentaIntegrada.MovimientoAlmacen.EntregaDiferida)
                {
                    //Verificar si hay comprobantes de salida de mercaderia distintos al comprobante de la venta, Ejm: Guia de remision, Nota de almacen
                    if (datosVentaIntegrada.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia)
                    {
                        //Obtener el maximo de codigo para transaccion de salida de mercaderia
                        int maximoCodigoSalidaMercaderia = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(venta.codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(c => c.Key == TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta), TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta);
                        Codigo maximosCodigos;
                        foreach (var salida in datosVentaIntegrada.MovimientoAlmacen.ComprobantesDeSalidasDeMercaderia)
                        {
                            maximosCodigos = new Codigo(maximoCodigoSalidaMercaderia);
                            ///aqui aseguramos que el movimiento sea con la fecha de registro de la orden(es decir, la fecha actual)
                            Transaccion salidaMercaderiaPorVenta = GenerarSalidaDeMercaderiaParaVenta(venta, datosVentaIntegrada.MovimientoAlmacen.Almacenero.Id, datosVentaIntegrada.MovimientoAlmacen.Almacen.Id, salida.IdTercero, venta.codigo + maximosCodigos.SiguienteCodigoSalidaMercaderia, datosVentaIntegrada.FechaRegistro, salida, sesionDeUsuario);
                            salidasDeMercaderia.Add(salidaMercaderiaPorVenta);
                            maximoCodigoSalidaMercaderia++;
                        }
                    }
                    else if (datosVentaIntegrada.Orden.HayBienesEnLosDetalles()) //solo en caso de existir bienes
                    {
                        Transaccion salidaMercaderiaPorVenta = GenerarMovimientoDeMercaderia(venta, datosVentaIntegrada.MovimientoAlmacen.Almacenero.Id, datosVentaIntegrada.MovimientoAlmacen.Almacen.Id, datosVentaIntegrada.Orden.Cliente.Id, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, datosVentaIntegrada.FechaRegistro, datosVentaIntegrada.Orden.Observacion, datosVentaIntegrada.Orden.DetallesDeVentaQueSonBienes(), sesionDeUsuario, 0);
                        salidasDeMercaderia.Add(salidaMercaderiaPorVenta);
                    }
                }
                return salidasDeMercaderia;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver la(s) salida(as) de mercaderia de venta integrada", e);
            }
        }
        /// <summary>
        /// Calcular, validar y generar la transaccion venta a nivel de datos transaccion
        /// </summary>
        /// <param name="tipoDeVenta"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <param name="datosVentaIntegrada"></param>
        /// <param name="GenerarComprobante">Si es verdadero, se genera el coprobante y se asigna a las respectivas transacciones, de otro modo, el comprobante es nulo.</param>
        /// <returns></returns>
        public OperacionIntegrada GenerarVentaIntegrada(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada datosVentaIntegrada, bool GenerarComprobante)
        {
            try
            {
                CalcularDatosDeVentaIntegrada(tipoDeVenta, datosVentaIntegrada, sesionDeUsuario);
                ValidarVenta(datosVentaIntegrada, ConfiguracionAccion.ConfirmacionOrdenVenta);
                //si son ventas masivas, no se debe generar el comprobante.
                Comprobante comprobante = GenerarComprobante ? GenerarComprobantePropio(datosVentaIntegrada.Orden.Comprobante.Serie.Id, datosVentaIntegrada.Orden.Comprobante.Numero) : null;
                Transaccion venta = GenerarVenta(datosVentaIntegrada, comprobante, ConfiguracionAccion.ConfirmacionOrdenVenta, sesionDeUsuario);
                Transaccion ordenDeVenta = GenerarOrdenDeVenta(venta, tipoDeVenta, datosVentaIntegrada, ConfiguracionAccion.ConfirmacionOrdenVenta);
                //Resolver cuotas de la venta
                ResolverCuotasDeVentaIntegrada(ordenDeVenta, datosVentaIntegrada);
                //Resolver el cobro si es que se tiene que realizar el cobro
                Transaccion ingresoDeDinero = GenerarIngresoDeDineroDeVentaIntegrada(venta, ordenDeVenta, datosVentaIntegrada);
                List<Transaccion> salidasDeMercaderia = GenerarSalidaDeMercaderiaDeVentaIntegrada(venta, datosVentaIntegrada, sesionDeUsuario);
                List<Transaccion> transaccionesModificadas = ObtenerTransaccionesYModificarEstasPorPagoConPuntos(datosVentaIntegrada, ingresoDeDinero);
                //Resolver la pregeneracion de operacion 
                ResolverPreGeneracionOperacion(transaccionesModificadas, datosVentaIntegrada);
                var ventaIntegrada = new OperacionIntegrada(venta, ordenDeVenta, ingresoDeDinero, salidasDeMercaderia, datosVentaIntegrada.TransaccionOrigen, datosVentaIntegrada.TransaccionCreacion)
                {
                    TransaccionesModificadas = transaccionesModificadas,
                    ActoresNegocioModificados = (datosVentaIntegrada.ActorReferencia == null) ? new List<Actor_negocio>() : new List<Actor_negocio>() { datosVentaIntegrada.ActorReferencia },
                    NuevosEstadosTransaccion = (datosVentaIntegrada.NuevoEstado == null) ? new List<Estado_transaccion>() : new List<Estado_transaccion>() { datosVentaIntegrada.NuevoEstado }
                };
                ventaIntegrada.EnlazarTransacciones();
                return ventaIntegrada;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar calcular validar y generar la venta intergrada", e);
            }
        }

        public void ResolverPreGeneracionOperacion(List<Transaccion> transaccionesModificadas, DatosVentaIntegrada datosVentaIntegrada)
        {
            try
            {
                if (datosVentaIntegrada.Orden.EsOperacionPreGenerada)
                {
                    var transaccionPreGeneracion = transaccionRepositorio.ObtenerTransaccion(datosVentaIntegrada.Orden.IdOperacionPreGenerada);
                    transaccionPreGeneracion.indicador2 = true;
                    transaccionesModificadas.Add(transaccionPreGeneracion);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver la generacion de cuotas", e);
            }
        }

        #endregion

        #region VENTA INTEGRADA 
        public OperationResult ConfirmarVentaIntegrada(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada datosVentaIntegrada)
        {
            try
            {
                OperacionIntegrada ventaIntegrada = GenerarVentaIntegrada(tipoDeVenta, sesionDeUsuario, datosVentaIntegrada, true);
                //Guardar la venta integrada
                return AfectarInventarioFisicoYGuardarOperacion(ventaIntegrada, sesionDeUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la venta integrada", e);
            }
        }
        public PuntosDeCliente ObtenerPuntosDeCliente(int idCliente)
        {
            //Obtener la fecha actual a la que se van a a traer los puntos
            DateTime fechaActual = DateTimeUtil.FechaActual();
            //Obtener la fecha desde cuendo se obtendra los puntos pendientes de caje
            DateTime fechaDesdeParaObtencionPuntos = fechaActual.AddDays(-VentasSettings.Default.VigenciaEnDiasDePuntos);
            //Obtener los puntos pendientes de canje del cliente
            return transaccionRepositorio.ObtenerPuntosDeCliente(fechaDesdeParaObtencionPuntos, idCliente);
        }

        #endregion


        public List<Transaccion> ObtenerTransaccionesYModificarEstasPorPagoConPuntos(DatosVentaIntegrada datosVentaIntegrada, Transaccion ingresoDeDinero)
        {
            try
            {
                List<Transaccion> transaccionesAModificar = new List<Transaccion>();
                List<PuntoCanjeado> puntosCanjeados = new List<PuntoCanjeado>();
                //Verificar si el medio de pago se realizo por puntos
                if (datosVentaIntegrada.Pago.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
                {
                    //Obtener la fecha actual a la que se van a a traer los puntos
                    DateTime fechaActual = DateTimeUtil.FechaActual();
                    //Obtener la fecha desde cuendo se obtendra los puntos pendientes de caje
                    DateTime fechaDesdeParaObtencionPuntos = fechaActual.AddDays(-VentasSettings.Default.VigenciaEnDiasDePuntos);
                    //Obtener las transacciones para realizar el canje de puntos
                    var transaccionesConPuntosPendientesDeCanje = transaccionRepositorio.ObtenerTransaccionesParaCanjePuntos(fechaDesdeParaObtencionPuntos, datosVentaIntegrada.Orden.Cliente.Id);///////
                    var puntosPorCanjear = datosVentaIntegrada.Pago.Traza.Info.PuntosCajeados;
                    //A cada uno de las transacciones realizar el canje respectivo
                    foreach (var transaccionConPuntosPendiente in transaccionesConPuntosPendientesDeCanje)
                    {
                        if (transaccionConPuntosPendiente.cantidad3 >= puntosPorCanjear)
                        {
                            puntosCanjeados.Add(new PuntoCanjeado(transaccionConPuntosPendiente.id, puntosPorCanjear));
                            transaccionConPuntosPendiente.cantidad2 += puntosPorCanjear;
                            transaccionConPuntosPendiente.cantidad3 -= puntosPorCanjear;
                            puntosPorCanjear -= puntosPorCanjear;
                        }
                        else
                        {
                            puntosPorCanjear -= (int)transaccionConPuntosPendiente.cantidad3;
                            puntosCanjeados.Add(new PuntoCanjeado(transaccionConPuntosPendiente.id, (int)transaccionConPuntosPendiente.cantidad3));
                            transaccionConPuntosPendiente.cantidad2 += transaccionConPuntosPendiente.cantidad3;
                            transaccionConPuntosPendiente.cantidad3 -= transaccionConPuntosPendiente.cantidad3;
                        }
                        transaccionesAModificar.Add(transaccionConPuntosPendiente);
                        if (puntosPorCanjear == 0)
                        {
                            break;
                        }
                    }
                    //Modificar el json del medio de pago con el id de las transacciones a modificar
                    var jsonDeTraza = JsonConvert.SerializeObject(puntosCanjeados);
                    ingresoDeDinero.Traza_pago.Single(i => i.id_medio_pago == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos).extension_json = jsonDeTraza;
                }
                return transaccionesAModificar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la venta integrada", e);
            }
        }
        public OperationResult InvalidarOrdenVentaRegistrada(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, string observaciones)
        {
            try
            {
                OrdenDeVenta ordenDeVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeVenta));
                AccionOperativa accionAIntentar = new AccionOperativa(maestroRepositorio.ObtenerDetalle(MaestroSettings.Default.IdDetalleMaestroAccionInvalidar));
                validarAccionSoberOrdenVenta(ordenDeVenta, accionAIntentar, idEmpleado);
                DateTime fechaActual = DateTimeUtil.FechaActual();

                Estado_transaccion estadoDeLaOrdenAnulacionDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual, "Estado que se asigna cuando se invalida la orden de venta");
                ordenDeVenta.Transaccion().Estado_transaccion.Add(estadoDeLaOrdenAnulacionDeVenta);

                var result = transaccionRepositorio.ActualizarTransaccion(ordenDeVenta.Transaccion());
                result.data = ordenDeVenta.Id;
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar anular la orden de venta ", e);
            }
        }

        public void validarAccionSoberOrdenVenta(OrdenDeVenta ordenDeVenta, AccionOperativa accionAIntentar, int idEmpleado)
        {
            Empleado empleado = new Empleado(actorRepositorio.obtenerActorDeNegocio(idEmpleado, ActorSettings.Default.IdRolEmpleado));
            int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            if (ordenDeVenta == null)
            {
                throw new LogicaException("Servicio no válido");
            }
            List<AccionOperativa> accionesPosibles = ordenDeVenta.ObtenerAccionesPosibles();

            if (!accionesPosibles.Any(ap => ap.IdAccion == accionAIntentar.IdAccion))
            {
                throw new LogicaException("La acción " + accionAIntentar.NombreAccion + " no está permitida en el estado actual");
            }
            else if (empleado.ObtenerRolesHijosVigentes().Select(r => r.id).Intersect(accionAIntentar.rolesPermitidos(ordenDeVenta.TipoTransaccion().Id, idUnidadDeNegocio).Select(r => r.id)).Count() <= 0)
            {
                throw new LogicaException("El empleado no se encuentra autorizado para realizar la acción" + accionAIntentar.NombreAccion);
            }
        }

        public OperationResult RegistrarOrdenVenta(int idEmpleado, int idCentroAtencion, int idCliente, int idSerieDeComprobante, DateTime fechaRegistro, string observacion, List<Detalle_transaccion> detalles)
        {
            try
            {
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionRegistrar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idUnidadNegocio);
                //Obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Tipo de cambio
                decimal tipoDeCambio = 1;// _repositorioTransaccion.obtenerTipoDeCambio(fechaRegistro).valorVenta;
                                         //Obtenemos el codigo
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("V", TransaccionSettings.Default.IdTipoTransaccionVenta);
                //Obtenemos la serie del comprobante
                //Serie_comprobante serie = _repositorioTransaccion.obtenerSerieDeComprobante(idSerieDeComprobante);                
                //Calculos
                var ValorDeVenta = detalles.Sum(d => d.total);
                //obtinene el comprobante numero 0
                Comprobante comprobante = transaccionRepositorio.ObtenerComprobanteCero(idSerieDeComprobante);
                //conseguir numero siguiente
                //serie.proximo_numero = (int.Parse(serie.proximo_numero) + 1).ToString();
                //crear una Venta
                Transaccion venta = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionVenta,
                    idUnidadNegocio, true, fechaRegistro, fechaRegistro, "", fechaRegistro, idEmpleado, ValorDeVenta, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la venta
                venta.Comprobante = comprobante;
                //crear una orden de venta
                Transaccion ordenDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_OV",
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta), null, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
                    idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, ValorDeVenta, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la orden de venta
                ordenDeVenta.Comprobante = comprobante;
                //agregamos la venta a la orden de venta 
                ordenDeVenta.Transaccion2 = venta;
                //agregamos los detalles
                ordenDeVenta.AgregarDetalles(detalles);
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado, fechaRegistro,
                    "Estado inicial asignado automaticamente al registrar una venta");
                ordenDeVenta.Estado_transaccion.Add(estadoDeLaOrdenDeVenta);
                //agregamos la orden de venta en la Venta
                venta.Transaccion1.Add(ordenDeVenta);

                OperationResult result = transaccionRepositorio.CrearTransaccion(venta);

                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ConfirmarYPagarOrdenVenta(int idEmpleado, int idCentroAtencion, long idVenta, int idSerieDeComprobante, DateTime fechaConfirmacion, int idMedioDePago, int idEntidadFinanciera, string informacionBancaria, string observacion)
        {
            try
            {
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                AccionOperativa accionAIntentar = new AccionOperativa(maestroRepositorio.ObtenerDetalle(MaestroSettings.Default.IdDetalleMaestroAccionConfirmar));
                decimal tipoDeCambio = 1;// _repositorioTransaccion.obtenerTipoDeCambio(fechaConfirmacion).valorVenta;
                Transaccion venta = ObtenerVentaYOrdenVenta(idEmpleado, idCentroAtencion, idVenta);
                Venta _venta = new Venta(venta);
                validarAccionSoberOrdenVenta(_venta.OrdenDeVenta(), accionAIntentar, idEmpleado);
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionRegistrar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idUnidadNegocio);
                DateTime fechaActual = DateTimeUtil.FechaActual();


                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieDeComprobante);
                Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
                //conseguir numero siguiente
                serie.proximo_numero = (serie.proximo_numero + 1);



                _venta.OrdenDeVenta().Transaccion().Comprobante = comprobante;
                venta.Comprobante = comprobante;

                Estado_transaccion estadoDeLaOrdenAnulacionDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado que se asigna cuando se confirma y paga la orden de venta");
                _venta.OrdenDeVenta().Transaccion().Estado_transaccion.Add(estadoDeLaOrdenAnulacionDeVenta);

                var ValorDeVenta = venta.importe_total;
                //crear cuota : cuenta por cobrar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, fechaConfirmacion.Year) + "_" + 1, fechaConfirmacion, fechaConfirmacion, venta.importe_total,
                    "Unica cuota generada de forma automática al emitir documento", true);
                venta.Cuota.Add(cuota);
                //pago
                string codigoPago = cuota.codigo + "_P1";
                Transaccion pago = new Transaccion(codigoPago, null, fechaConfirmacion, TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, idUnidadNegocio, true, fechaConfirmacion, fechaConfirmacion, observacion, fechaConfirmacion,
                    idEmpleado, ValorDeVenta, idCentroAtencion, idMoneda, tipoDeCambio, null, Convert.ToInt32(venta.Actor_negocio1.id));
                //Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, int.Parse(serie.proximo_numero).ToString(), true, serie.numero);
                //vincular el pago con el comprobante
                pago.Comprobante = comprobante;
                //vincular el pago con la cuota.
                Pago_cuota pagoCuota = new Pago_cuota();
                pagoCuota.Transaccion = pago;
                pagoCuota.Cuota = cuota;
                pago.Pago_cuota.Add(pagoCuota);

                //creado traza 
                if (idMedioDePago == MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo)
                {
                    Traza_pago traza = new Traza_pago(idMedioDePago, informacionBancaria, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                    pago.Traza_pago.Add(traza);
                }
                else
                {
                    Traza_pago traza = new Traza_pago(idMedioDePago, informacionBancaria, idEntidadFinanciera);
                    pago.Traza_pago.Add(traza);
                }
                //agregar estado inicial: registrado
                Estado_transaccion estadoDelPago = new Estado_transaccion(idEmpleado, MaestroSettings
                    .Default.IdDetalleMaestroEstadoRegistrado, fechaConfirmacion, "");
                pago.Estado_transaccion.Add(estadoDelPago);
                pago.Transaccion2 = venta;

                OperationResult result = transaccionRepositorio.ActualizarTransaccion(venta);

                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }



        public OperationResult cancelarOrdenDeVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                //validamos los permisos pra la accion
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar,
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
                    MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal);
                //obtenemos fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //obtenemos la orden original
                OrdenDeVenta ordenOriginal = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeVenta));

                List<Estado_transaccion> estados = new List<Estado_transaccion>();
                //creamos un nuevo estado para la orden
                estados.Add(new Estado_transaccion(ordenOriginal.Id, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual, "Se Cancelo la venta"));
                //creamos los estados para los pagos si tiene
                if (ordenOriginal.Venta().ObtenerPagos() != null)
                {
                    foreach (var pago in ordenOriginal.Venta().ObtenerPagos())
                    {
                        estados.Add(new Estado_transaccion(pago.Id, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual, "Se Cancelo el pago"));
                    }
                }

                var result = transaccionRepositorio.CrearEstadosDeTransaccionesAhora(estados);
                return new OperationResult(OperationResultEnum.Success, "Se logró realizar la accion: Cancelar", ordenOriginal.Id);

            }
            catch (Exception e)
            {
                return new OperationResult(new Exception("Error al intentar Cancelar la Venta", e));
            }
        }


        public OrdenDeVenta ObtenerOrdenDeVenta(int idEmpleado, int idCentroAtencion, long idOrdenVenta)
        {
            try
            {
                return new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenVenta));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaccion ObtenerVentaYOrdenVenta(int idEmpleado, int idCentroAtencion, long idVenta)
        {
            try
            {
                return transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idVenta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<int> ObtenerIdsDeSeriesActivas(int[] idsTipoComprobante)
        {
            return transaccionRepositorio.ObtenerIdsSeriesComprobantes(idsTipoComprobante, true).ToList();
        }

        public long ObtenerIdDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                int[] idsTipoTransaccion = { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta };
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
                return transaccionRepositorio.ObtenerIdPrimeraTransaccion(idsTipoTransaccion, idsTiposComprobantes, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idSerie);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta tributables", e);
            }
        }
        public long ObtenerIdDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                int[] idsTipoTransaccion = { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta };
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
                return transaccionRepositorio.ObtenerIdUltimaTransaccion(idsTipoTransaccion, idsTiposComprobantes, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idSerie);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas y tributables", e);
            }
        }

        #region OBTNER NUMERO DE COMPROBA PRIMERA Y ULTIMA ORDEN CON BOLETA DE VENTA CONFIRMADAS Y CONFIRMADAS Y TRIBUTARIAS

        public long ObtenerNumeroDeComprobanteDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                int[] idsTipoTransaccion = { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta };
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
                return transaccionRepositorio.ObtenerNumeroDeComprobantePrimeraTransaccion(idsTipoTransaccion, idsTiposComprobantes, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idSerie);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta tributables", e);
            }
        }
        public long ObtenerNumeroDeComprobanteDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                int[] idsTipoTransaccion = { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta };
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
                return transaccionRepositorio.ObtenerNumeroDeComprobanteUltimaTransaccion(idsTipoTransaccion, idsTiposComprobantes, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idSerie);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas y tributables", e);
            }
        }

        #endregion



        public List<OperacionDeVenta> ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {


                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributablesExceptoBoleta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).OrderBy(t => t.fecha_inicio).ThenBy(t => t.Comprobante.Serie_comprobante).ThenBy(t => t.id).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta tributables", e);
            }
        }

        /// <summary>
        /// tributable: cuando son comprobantes validos: boletas, facturas
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        public List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributables, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).OrderBy(t => t.fecha_inicio).OrderBy(t => t.Comprobante.numero_serie).OrderBy(t => t.id).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes con boleta, factura y notas invalidadas y tributables", e);
            }
        }


        //Codigo CERNA: Obtiene informacion de Ventas e Ingresos SIN CONCEPTO
        public List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributablesSinConcepto(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoSinConcepto(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributables, idsEstados, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ReporteVentaDetalladoSinConcepto> ObtenerReporteVentaDetalladoSinConcepto(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                return transaccionRepositorio.ObtenerReporteVentaDetalladoSinConcepto(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributables, idsEstados, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de venta detallado sin concepto");
            }
        }


        //Codigo CERNA: Obtiene informacion de Ventas e Ingresos CON CONCEPTO
        public List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributablesIncluyendoConceptos(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYConceptoNegocio(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributables, idsEstados, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };

                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, Diccionario.TiposDeComprobanteTributables, idsEstados, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes con boleta, factura y notas de venta", e);
            }
        }

        public List<int> ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta()
        {
            return transaccionRepositorio.ObtenerIdsSeriesComprobantes(new int[] { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta }).ToList();
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVenta(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idCentroAtencion, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public List<OperacionDeVenta> ObtenerOperacionesDeVenta(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVenta, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener operaciones de venta", e);
            }
        }

        public List<Resumen_Venta> ObtenerResumenesVentas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int? idCliente, string comprobante)
        {
            try
            {
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionVer, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal);
                List<Resumen_Venta> resumenes = new List<Resumen_Venta>();
                if (string.IsNullOrEmpty(comprobante))
                {
                    //si tiene rol administrador, devolver las ventas de todos los centros de atención.
                    if (permisos_Logica.ValidarActorNegocioTieneRolVigente(idEmpleado, ActorSettings.Default.idRolJefeDeVenta))
                    {
                        if (idCliente != null)
                        {
                            resumenes = transaccionRepositorio.ObtenerResumenesVentas(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVentaExceptoNotaInvalidacion, fechaDesde, fechaHasta, (int)idCliente).ToList();
                        }
                        else
                        {
                            resumenes = transaccionRepositorio.ObtenerResumenesVentas(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVentaExceptoNotaInvalidacion, fechaDesde, fechaHasta).ToList();
                        }
                    }
                    //de lo contrario, se asume que es un vendedor, por lo que se devolverá las ventas del centro de atencion en el cual se ha logueado y que en el cual el empleado haya sido vendedor.
                    else
                    {
                        if (idCliente != null)
                        {
                            resumenes = transaccionRepositorio.ObtenerResumenesVentas(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVentaExceptoNotaInvalidacion, idEmpleado, idCentroAtencion, fechaDesde, fechaHasta, (int)idCliente).ToList();
                        }
                        else
                        {
                            resumenes = transaccionRepositorio.ObtenerResumenesVentas(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVentaExceptoNotaInvalidacion, idEmpleado, idCentroAtencion, fechaDesde, fechaHasta).ToList();
                        }
                    }
                }
                else
                {
                    resumenes = transaccionRepositorio.ObtenerResumenesVentas(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.TiposDeComprobanteParaVentaExceptoNotaInvalidacion, comprobante).ToList();
                }
                return resumenes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener resumenes de ventas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCentroAtencion, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) //y45
        {

            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaHasta).ToList());  //45

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta invalidadas", e);

            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) //y46
        {

            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta anuladas", e);
            }
        }


        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasYTransmitidas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) //y47
        {
            try
            {
                var resultado = OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaHasta).ToList());

                return resultado;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas y transmitidas", e);
            }
        }

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaConfirmadasYTransmitidasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) // confirmado  XY4.1     
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(new int[] { idCentroAtencion }, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList(); //xy4.1
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas y transmitidas", e);
            }
        }

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaInvalidadasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) //invalidadas XY 4.2
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(new int[] { idCentroAtencion }, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();  //4.2
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta invalidadas", e);
            }
        }

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaAnuladasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta) //XY 4.3
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(new int[] { idCentroAtencion }, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta anuladas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idSerie).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasOTransmitidas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idSerie).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas o transmitidas", e);
            }
        }
        public List<ResumenDeTransaccionVenta> ObtenerResumenDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                List<ResumenDeTransaccionVenta> ordenes = transaccionRepositorio.ObtenerResumenDeTransaccion(idSerie, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, Diccionario.EstadosDeOperacionesIgnorandoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, fechaDesde, fechaHasta).OrderBy(rt => rt.Fecha).ThenBy(rt => rt.Numero).ToList();
                return ordenes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de operaciones de venta", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaTransmitidasYConfirmadas(DateTime fechaDesde, DateTime fechaHasta, int idSerie) //XY2.1
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(idSerie, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta transmitidas y confirmadas", e);
            }
        }
        //public List<ResumenDeTransaccion> ObtenerResumenDeOperacionesDeVentaInvalidadas(DateTime fechaDesde, DateTime fechaHasta, int idSerie) //XY2.2
        //{
        //    try
        //    {

        //        return _repositorioTransaccion.ObtenerResumenDeTransaccion(idSerie, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado,fechaDesde, fechaHasta).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener ordenes de venta invalidadas", e);
        //    }
        //}
        public List<OrdenDeVenta> ObtenerOrdenesDeVentaInvalidadas(DateTime fechaDesde, DateTime fechaHasta, int idSerie) //XY2.2
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(idSerie, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta invalidadas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaAnuladas(DateTime fechaDesde, DateTime fechaHasta, int idSerie) //XY2.3
        {
            try
            {
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(idSerie, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta anuladas", e);
            }
        }

        public List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerReporteVentasConSerieYConceptoConfirmadaOTransmitida(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesSerieYConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();

            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadaOTransmitida_(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesPorSerieYConeptoBasico(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();

            return resultado;
        }



        public List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
                foreach (var item in resultado.Where(r => r.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito))
                {
                    item.Importe *= -1;
                    var icbper = item.Icbper;
                    item.Icbper = Math.Abs(icbper) * (-1);
                    item.ValorVenta *= -1;
                    item.Igv *= -1;
                }

                return resultado;
            }
            catch (LogicaException e)
            {

                throw new LogicaException("Error al obtener resumenes de comprobantes", e);
            }

        }

        public List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)//a11
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario_(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }
        //El estado para este reporte sera anulado
        public List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantePorSerieAnulada(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)//a12
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesAgrupadasPorSerie(idsPuntosDeVentas, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }


        //Nuevo metodo para obtenr el reporte por serie de comprobante y concepto basico (Validos)
        public List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobanteVentaPorSerieYConceptoBasicoConfirmado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            foreach (var item in resultado.Where(r => r.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito))
            {
                item.Cantidad *= -1;
                item.Importe *= -1;
            }

            return resultado;
        }
        //El estado para este reporte por serie de comprobante y concepto basico sera Invalidado
        public List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobantePorSerieYConceptoBasicoInvalidado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }
        //El estado para este reporte por serie de comprobante y concepto basico sera Anulado
        public List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobantePorSerieYConceptoBasicoAnulado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)//a13.2
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadaOTransmitida(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesPorSerieYConeptoBasico(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();

            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadasYTransmitidas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)//XY 6.1
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();
            return resultado;
        }
        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)//XY 6.2
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).ToList();
            return resultado;
        }
        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)//XY 6.3
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Consolidado> ObtenerReporteVentaCompraPagoCobroYGastos(DateTime fechaDesde, DateTime fechaHasta)//cod:XY7
        {
            int[] idsTransaccion = {
                TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes,
                TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores,
                TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios,
                TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios,
                TransaccionSettings.Default.IdTipoTransaccionPagoGastoFinanciero,
                TransaccionSettings.Default.IdTipoTransaccionPagoGastoPorTributos,
                TransaccionSettings.Default.IdTipoTransaccionPagoGastoServiciosTerceros,
                TransaccionSettings.Default.IdTipoTransaccionPagoOtrosGastosGestion
               };

            var resultado = transaccionRepositorio.ObtenerResumenTransacciones(idsTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja).ToList();
            return resultado;
        }

        public List<Cuota> ObtenerReporteDeudasAProveedor() //cod Y10
        {
            var porPagar = transaccionRepositorio.ObtenerCuotasConSaldo(false, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras).ToList();

            var porCobrar = transaccionRepositorio.ObtenerCuotasConSaldo(true, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras).ToList();

            return porPagar.Union(porCobrar).ToList();
        }

        /// <summary>
        /// Este metodo retorna las deudas del proveedor se consulta tanto las cuentas por cobrar y las cuentas por pagar para tener un mayor flujo de dinero porque se puede 
        /// dar el caso de que vendamos 1000 a un cliente y luego el cliente decida devolvernos mercaderia valorizado en 200. Entonces es por eso que se hace una union para mostrar en el 
        /// reporte el verdaero flujo del dinero.
        /// </summary>
        /// <returns></returns>
        public List<Cuota> ObtenerReporteDeudasDeCliente()
        {
            try
            {
                var porPagar = transaccionRepositorio.ObtenerCuotasConSaldo(false, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas).ToList();

                var porCobrar = transaccionRepositorio.ObtenerCuotasConSaldo(true, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas).ToList();


                return porCobrar.Union(porPagar).ToList();

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener deudas de cliente", e);
            }

        }






        public List<OrdenDeVenta> ObtenerOrdenesDeVenta(long[] idsOperacionesDeVentas)
        {
            try
            {
                int idTipoTransaccionVenta = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idsOperacionesDeVentas, idTipoTransaccionVenta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);

            }
        }

        public OrdenDeVenta ObtenerOrdenDeVenta(long idOrdenVenta)
        {
            try
            {
                return (new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idOrdenVenta)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public OrdenDeVenta ObtenerOrdenDeVentaConsultaComprobante(ConsultaComprobanteParameter consultaComprobante)
        {
            try
            {
                return (new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccionConsultaComprobante(consultaComprobante)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        /// <summary>
        /// OBTIENE UNA ORDEN DE VENTA POR PARAMETRO Y ESTA ORDEN DE VENTA SE ARMA CON SUS DATOS LISTOS PARA IMPRESION 
        /// </summary>
        /// <param name="ordenDeVenta"></param>
        /// <returns></returns>
        public OrdenDeVenta ObtenerOrdenDeVentaParaImprimir(OrdenDeVenta ordenDeVenta)
        {
            try
            {
                //int[] idsDetallesMaestro = { ordenDeVenta.Comprobante().IdTipo, ordenDeVenta.Transaccion().id_moneda };
                //List<Detalle_maestro> detallesDeMaestro = maestroRepositorio.ObtenerDetallesEspecificos(idsDetallesMaestro).ToList();
                //ordenDeVenta.Transaccion().Comprobante.Detalle_maestro = detallesDeMaestro.Single(dm => dm.id == ordenDeVenta.Comprobante().IdTipo);
                //ordenDeVenta.Transaccion().Detalle_maestro1 = detallesDeMaestro.Single(dm => dm.id == ordenDeVenta.Transaccion().id_moneda);
                //int[] idsActoresNegocio = { ordenDeVenta.Transaccion().id_empleado, ordenDeVenta.Transaccion().id_actor_negocio_interno, ordenDeVenta.Transaccion().id_actor_negocio_externo };
                int[] idsActoresNegocio = { ordenDeVenta.Transaccion().id_actor_negocio_interno };
                List<Actor_negocio> actoresDeNegocio = actorRepositorio.ObtenerActoresDeNegocio(idsActoresNegocio).ToList();
                //ordenDeVenta.Transaccion().Actor_negocio = actoresDeNegocio.Single(an => an.id == ordenDeVenta.Transaccion().id_empleado);
                //ordenDeVenta.Transaccion().Actor_negocio1 = actoresDeNegocio.Single(an => an.id == ordenDeVenta.Transaccion().id_actor_negocio_externo);
                ordenDeVenta.Transaccion().Actor_negocio2 = actoresDeNegocio.Single(an => an.id == ordenDeVenta.Transaccion().id_actor_negocio_interno);
                //int[] idsConceptosNegocio = ordenDeVenta.Transaccion().Detalle_transaccion.Select(d => d.id_concepto_negocio).ToArray();
                //List<Concepto_negocio> conceptosDeNegocio = conceptoRepositorio.ObtenerConceptosDeNegocio(idsConceptosNegocio).ToList();
                //foreach (var item in ordenDeVenta.Transaccion().Detalle_transaccion)
                //{
                //    item.Concepto_negocio = conceptosDeNegocio.Single(cn => cn.id == item.id_concepto_negocio);
                //}
                return ordenDeVenta;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public List<MovimientoDeAlmacen> ObtenerSalidaDeMercaderiaDeVenta(long idVenta)
        {
            try
            {
                return MovimientoDeAlmacen.Convertir(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta).Where(t => t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta", e);
            }
        }

        public MovimientoDeAlmacen ObtenerMovimientoDeMercaderia(long idMovimiento)
        {
            try
            {
                return (new MovimientoDeAlmacen(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idMovimiento)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error obtener la salida de mercaderia", e);
            }
        }

        public List<long> ObtenerIdsGuiaRemisionPorEnviarSunat(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return transaccionRepositorio.ObtenerIdsGuiaRemisionPorEnviar(fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error obtener las guias de remision por enviar a sunat", e);
            }
        }
        /// <summary>
        /// Devuelve el id de la orden de venta apartir del id de venta
        /// </summary>
        /// <param name="idOperacionVenta"></param>
        /// <returns></returns>
        public long obtenerIdOrdenDeVenta(long idOperacionVenta)
        {
            try
            {
                long idTipoTransaccionOperacion = transaccionRepositorio.ObtenerIdTipoTransaccion(idOperacionVenta);
                int idTipoTransaccionOrdenDeOperacion = idTipoTransaccionOperacion == TransaccionSettings.Default.IdTipoTransaccionVenta ?
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta :
                    (idTipoTransaccionOrdenDeOperacion = idTipoTransaccionOperacion == TransaccionSettings.Default.IdTipoTransaccionAnulacionDeVenta ? TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta :
                    (idTipoTransaccionOperacion == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta ?
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta :
                    (idTipoTransaccionOperacion == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta ?
                    TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionDeAnulacionDeVenta : 0)));

                return transaccionRepositorio.ObtenerUnicoIdTransaccion(idOperacionVenta, idTipoTransaccionOrdenDeOperacion);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener id de orden de venta", e);
            }
        }
        /// <summary>
        /// Devuelve el id de la orden del <paramref name="idVenta"/>, tener en cuenta que solo es para ventas.
        /// </summary>
        /// <param name="idVenta"></param>
        /// <returns></returns>
        public long ObtenerIdOrdenDeVenta(long idVenta)
        {
            try
            {
                return transaccionRepositorio.ObtenerUnicoIdTransaccion(idVenta, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener id de orden de venta", e);
            }
        }

        public OrdenDeVenta ObtenerOrdenDeVentaConIdOrden(long data)
        {
            try
            {
                return (new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(data)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener orden de venta", e);
            }
        }
        public async Task<List<Detalle_maestro>> ObtenerMediosDePagoVenta()
        {
            try
            {
                List<int> idsMedios = new List<int>();
                var mascara = VentasSettings.Default.MascaraMediosDePagoAMostrarEnVentas.ToCharArray();
                for (int i = 0; i < mascara.Count(); i++)
                {
                    if (mascara[i] == '1')
                    {
                        idsMedios.Add(Diccionario.ValoresMascaraMedioPagoAMostrarEnVentas.Single(v => v.Key == i).Value);
                    }
                }
                return (await maestroRepositorio.ObtenerDetallesEspecificos(idsMedios.ToArray())).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener medios de pago para venta", e);
            }
        }



        public List<OrdenDeVenta> ObtenerOrdenesDeVentaHoy(int IdEmpleado)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaActual.Date, fechaActual.Date.AddDays(1)).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de venta del dia de hoy", e);
            }
        }

        //public List<OrdenDeVenta> ObtenerOrdenesDeVentaHoyConfirmadas(int IdEmpleado, int idCentroAtencion)
        //{
        //    try
        //    {
        //        DateTime fechaActual = DateTimeUtil.FechaActual();
        //        return OrdenDeVenta.Convert_(_repositorioTransaccion.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCentroAtencion, fechaActual.Date, fechaActual.Date.AddDays(1)).ToList());
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        public List<DateTime> ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            DateTime fechaDesde = fechaActual;
            DateTime fechaHasta = fechaDesde.Date.AddDays(1).AddMilliseconds(-1);

            if (AplicacionSettings.Default.AplicarIntervaloAtencionEnReporteVentaDiario)
            {
                string[] inicio = AplicacionSettings.Default.HoraInicioIntervaloAtencion.Split(':');
                string[] fin = AplicacionSettings.Default.HoraFinIntervaloAtencion.Split(':');
                TimeSpan horaInicioIntervaloPorDefecto = new TimeSpan(Convert.ToInt16(inicio[0]), Convert.ToInt16(inicio[1]), Convert.ToInt16(inicio[2]));
                TimeSpan horaHastaIntervaloPorDefecto = new TimeSpan(Convert.ToInt16(fin[0]), Convert.ToInt16(fin[1]), Convert.ToInt16(fin[2]));
                TimeSpan horaActual = fechaActual.TimeOfDay;
                ///FIJANDO FECHA Y HORA INICIO
                /// si la hora actual es menor a la de inicio, 
                fechaDesde = horaActual < horaInicioIntervaloPorDefecto ?
                    fechaActual.Date.AddDays(-1).AddTicks(horaInicioIntervaloPorDefecto.Ticks) //debe mostrarse el reporte del dia anterior
                    : fechaActual.Date.AddTicks(horaInicioIntervaloPorDefecto.Ticks);// de lo contrario, el del mismo dia
                ///FIJANDO FECHA Y HORA FIN
                ///Si la hora fin es menor a la hora inicio
                ///
                fechaHasta = horaHastaIntervaloPorDefecto < horaInicioIntervaloPorDefecto ?
                    fechaDesde.Date.AddDays(1).AddTicks(horaHastaIntervaloPorDefecto.Ticks) //se trata del dia siguiente
                    : fechaDesde.Date.AddTicks(horaHastaIntervaloPorDefecto.Ticks);// de lo contrario, el del mismo dia

            }
            return new List<DateTime> { fechaDesde, fechaHasta };
        }

        public List<DateTime> ObtenerFechaIncioyFinBasadoEnFechaActual()
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            DateTime fechaDesde = fechaActual.Date;
            DateTime fechaHasta = fechaDesde.Date.AddTicks(-1).AddDays(1);

            return new List<DateTime> { fechaDesde, fechaHasta };
        }


        public List<OrdenDeVenta> ObtenerOrdenesDeVentaHoyConfirmadas(int IdEmpleado, int idCentroAtencion)
        {
            try
            {
                var fechas = ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();

                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCentroAtencion, fechas[0], fechas[1]).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<long> ObtenerIdOrdenesDeVentaConBoletaYFacturaConfirmadasEInvalidadas(int idEmpleado)
        {
            try
            {
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura };
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado };
                return transaccionRepositorio.ObtenerIdTransacciones(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsTiposComprobantes, idsEstados).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //--
        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConBoletaYFacturaConfirmadasEInvalidadas(int idEmpleado)
        {
            try
            {
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura };
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado };
                return null;//OrdenDeVenta.Convert_(_repositorioTransaccion.ObtenerTransacciones(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsTiposComprobantes, idsEstados ).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //--
        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConBoletaConfirmadasEInvalidadas(int idEmpleado)
        {
            try
            {
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado };
                return null; //OrdenDeVenta.Convert_(_repositorioTransaccion.ObtenerTransacciones(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsTiposComprobantes, idsEstados).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region OBTENER LAS ORDENES DE VENTA QUE SERAN USADOS PARA TRANSMITIRSE A FACTURACION ELECTRONICA

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConBoletaConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int cantidadAObtener)
        {
            try
            {
                DateTime fechaHasta = fechaActual.AddHours(-FacturacionElectronicaSettings.Default.NumeroDeHorasDeRetrazoAlTransmitirAEfactura);
                int idTipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta;
                int idEventoAEvitar = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido;
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idTipoTransaccion, idTipoComprobante, idEventoAEvitar, fechaHasta, cantidadAObtener).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta con boleta confirmadas e invalidadas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConFacturaConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual)
        {
            try
            {
                DateTime fechaHasta = fechaActual.AddHours(-FacturacionElectronicaSettings.Default.NumeroDeHorasDeRetrazoAlTransmitirAEfactura);
                int idTipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
                int idEventoAEvitar = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido;
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idTipoTransaccion, idTipoComprobante, idEventoAEvitar, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta con boleta confirmadas e invalidadas", e);
            }
        }

        public List<OperacionDeVenta> ObtenerOperacionesConNotaDeCreditoConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int idTipoComprobanteReferencia)
        {
            try
            {
                DateTime fechaHasta = fechaActual.AddHours(-FacturacionElectronicaSettings.Default.NumeroDeHorasDeRetrazoAlTransmitirAEfactura);
                int[] idsTiposTransacciones = { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta };
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                int idEventoAEvitar = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido;
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idsTiposTransacciones, idTipoComprobante, idEventoAEvitar, fechaHasta).Where(t => t.Transaccion3.Comprobante.Detalle_maestro.id == idTipoComprobanteReferencia).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las operaciones con nota de credito", e);
            }
        }

        public List<OperacionDeVenta> ObtenerOperacionesConNotaDeDebitoConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int idTipoComprobanteReferencia)
        {
            try
            {
                DateTime fechaHasta = fechaActual.AddHours(-FacturacionElectronicaSettings.Default.NumeroDeHorasDeRetrazoAlTransmitirAEfactura);
                int[] idsTiposTransacciones = { TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta, TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta };
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito;
                int idEventoAEvitar = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido;
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idsTiposTransacciones, idTipoComprobante, idEventoAEvitar, fechaHasta).Where(t => t.Transaccion3.Comprobante.Detalle_maestro.id == idTipoComprobanteReferencia).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las operaciones con nota de debito", e);
            }
        }

        public List<MovimientoDeAlmacen> ObtenerGuiasDeRemisionConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual)
        {
            try
            {
                DateTime fechaHasta = fechaActual.AddHours(-FacturacionElectronicaSettings.Default.NumeroDeHorasDeRetrazoAlTransmitirAEfactura);
                List<int> idsTiposTransacciones = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas.ToList();
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.ToList());
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionGuiasDeRemision.ToList());
                int idTipoComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente;
                int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado };
                int idEventoAEvitar = MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido;
                return MovimientoDeAlmacen.Convertir(transaccionRepositorio.ObtenerTransacciones(idsTiposTransacciones.ToArray(), idTipoComprobante, idEventoAEvitar, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las operaciones con nota de debito", e);
            }
        }

        #endregion

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasEnIntervaloDeFecha(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsPuntosDeVenta, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ordenes de venta confirmadas", e);
            }
        }

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta)
        {

            try
            {
                return OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsPuntosDeVenta, fechaDesde, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // c2 reporte venta de concepto por vendedor - administrador

        public List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasOTransferidasDeVendedorAdministrador(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta) //C2
        {
            int[] idsEstados = { MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido };
            try
            {
                //return OrdenDeVenta.Convert_(_repositorioTransaccion.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, idsEstados, idEmpleado, fechaDesde, fechaHasta).ToList());
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void ValidarInvalidacionDeVenta(OrdenDeVenta orden)
        {
            if (!orden.EsAnulableConNotaInterna())
            {
                throw new LogicaException("La venta no puede anularse. Es posible que haya excedido el plazo máximo permitido");
            }
        }
        /*
        public OperationResult InvalidarVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, int idCaja, int idAlmacen, string observaciones, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtener la fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtener la operacion de referencia actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Obtener la orden venta
                OrdenDeVenta ordenDeVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeVenta));
                //Obtener la venta
                Venta venta = new Venta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(ordenDeVenta.IdVenta));
                ValidarInvalidacionDeVenta(ordenDeVenta);
                //Extraer los datos
                var idUnidadDeNegocio = operacionGenerica.IdUnidadNegocio;
                var idMoneda = ordenDeVenta.IdMoneda;
                var tipoDeCambio = ordenDeVenta.TipoDeCambio;
                var idCliente = ordenDeVenta.Cliente().Id;
                var idVenta = ordenDeVenta.IdVenta;
                idCaja = venta.ObtenerPagos().Count() > 0 ? venta.ObtenerPagos().First().Transaccion().id_actor_negocio_interno : idCaja;
                decimal importePagoTotal;

                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
                    idUnidadDeNegocio);

                //Crear el codigo para Invalidar la venta con nota interna
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(c => c.Key == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta);

                //Obtener los detalles de la invalidacion
                List<DetalleDeOperacion> detallesDeInvalidacionDeVenta = ordenDeVenta.Detalles();

                List<Detalle_transaccion> detalles_transaccion = detallesDeInvalidacionDeVenta.Select(dv => dv.DetalleTransaccion()).ToList();
                //Obtener el importe total de la operacion
                decimal importeTotal = detalles_transaccion.Sum(d => d.total) + ordenDeVenta.Icbper();

                if (ordenDeVenta.ModoDePago() == ModoPago.Contado)
                {
                    importePagoTotal = importeTotal;
                }
                else
                {
                    importePagoTotal = ordenDeVenta.Transaccion().Cuota.SelectMany(c => c.Pago_cuota).Sum(cp => cp.importe);
                }

                //Obtener la serie del comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionVenta, idCentroAtencion);
                if (serie == null)
                {
                    throw new LogicaException("No es posible realizar la invalidacion, no existe serie de nota de invalidacion de venta.");
                }
                Comprobante comprobante = GenerarComprobantePropioAutonumerable(serie.id);

                //Obtener la invalidacion de venta
                Transaccion invalidacionDeVenta = new Transaccion(codigo, operacionGenerica.Id, fechaActual, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta, idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, importeTotal, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente)
                {
                    //Agregar el comprobante a la invalidacion de venta
                    Comprobante = comprobante,
                    //Agregar la venta como referencia
                    id_transaccion_referencia = idVenta
                };
                decimal descuentoGlobal = 0, descuentoPorItem = 0, anticipo = 0, gravada = 0, exonerada = 0, inafecta = 0, gratuita = 0, igv = 0, isc = 0, icbper = 0, otrosCargos = 0, otrosTributos = 0;
                if (detalles_transaccion.Sum(d => d.igv) > 0)
                {
                    gravada = detalles_transaccion.Sum(d => d.total - d.igv);
                    igv = detalles_transaccion.Sum(d => d.igv);
                }
                else
                {
                    exonerada = detalles_transaccion.Sum(d => d.total);
                }
                icbper = ordenDeVenta.Icbper();
                //Crear la orden de invalidacion de la venta
                Transaccion ordenInvalidacionDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_" + Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(c => c.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta, idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, importeTotal, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente, descuentoGlobal, descuentoPorItem, anticipo, gravada, exonerada, inafecta, gratuita, igv, isc, icbper, otrosCargos, otrosTributos)
                {
                    //Agregar el comprobante a la orden de invalidacion
                    Comprobante = comprobante,
                    //Agregar los detalles
                    id_transaccion_referencia = idOrdenDeVenta
                };
                //Agregar el icbper en la orden de invalidacion de la venta 
                ordenInvalidacionDeVenta.AgregarDetalles(detalles_transaccion);
                //Agregar la orden de venta como referencia
                if (ordenDeVenta.Icbper() > 0)
                {
                    ordenInvalidacionDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroIcbper, ordenDeVenta.Icbper().ToString()));
                }
                //Agregar el estado de la orden de invalidacion
                Estado_transaccion estadoDeLaOrdenInvalidacionDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al registrar la invalidacion");
                ordenInvalidacionDeVenta.Estado_transaccion.Add(estadoDeLaOrdenInvalidacionDeVenta);
                //Agregar la orden de invalidacion en la invalidacion de venta
                invalidacionDeVenta.Transaccion1.Add(ordenInvalidacionDeVenta);
                //Ver si la operacion de ordigen la venta se realizo con puntos
                var movimientoEconomicoConPuntos = venta.ObtenerPagos().SingleOrDefault(p => p.TrazaDePago().MedioDePago().id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos);
                //Creacion del pago de la invalidacion de la venta
                Transaccion pago = null;
                if (importePagoTotal > 0)
                {
                    //Crear cuota, cuenta por cobrar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                    Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, importePagoTotal, "Unica cuota generada de forma automática al emitir la nota interna", false)
                    {
                        pago_a_cuenta = importePagoTotal
                    };
                    //Agregar la cuota a la orden de la invalidacion
                    ordenInvalidacionDeVenta.Cuota.Add(cuota);
                    int idMedioPago = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
                    int idEntidadBancaria = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
                    string informacion = "Pago efectivo";
                    //verificar el medio de pago de la venta a invalidar 
                    if (movimientoEconomicoConPuntos != null)
                    {
                        idMedioPago = MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos;
                        idEntidadBancaria = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
                        informacion = "Pago con puntos";
                    }
                    //Generar el pago de la invalidacion de la operacion
                    pago = GenerarMovimientoEconomico(invalidacionDeVenta, cuota, idEmpleado, idCaja, idCliente, TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorInvalidacionDeVenta, fechaActual, fechaActual, observaciones, idMedioPago, idEntidadBancaria, informacion);
                    if (movimientoEconomicoConPuntos != null)
                    {
                        pago.cantidad1 = movimientoEconomicoConPuntos.Transaccion().cantidad1;
                    }
                    //Agregar el pago a la invalidacion de venta
                    invalidacionDeVenta.Transaccion1.Add(pago);
                }
                Transaccion entradaMercaderiaPorInvalidacionVenta = new Transaccion();
                var salidasMercaderiaVenta = venta.ObtenerSalidasDeMercaderia();
                if (salidasMercaderiaVenta.Count > 0)
                {
                    //Creacion de la salida de mercaderia de la invalidacion de la invalidacion
                    entradaMercaderiaPorInvalidacionVenta = GenerarMovimientoDeMercaderia(invalidacionDeVenta, idEmpleado, idAlmacen, idCliente, TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta, fechaActual, observaciones, detallesDeInvalidacionDeVenta, sesionUsuario, salidasMercaderiaVenta.First().Id);
                    //Agregar como referencia de la entrada de mercaderia a la orden de invalidacion de la venta
                    entradaMercaderiaPorInvalidacionVenta.Transaccion3 = ordenInvalidacionDeVenta;
                    //agregamos el trasldo interno a la orden de traslado 
                    invalidacionDeVenta.Transaccion1.Add(entradaMercaderiaPorInvalidacionVenta);
                }
                //Crear el estado invalidado para la orden de venta original
                Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(idOrdenDeVenta, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual,
                    "Estado que se agrega al invalidar una Venta");
                //Crear el estado invalidado para todas las cuotas generadas en la orden de ventac
                List<Estado_cuota> estadosDeCuota = new List<Estado_cuota>();
                foreach (var itemCuota in ordenDeVenta.Cuotas())
                {
                    estadosDeCuota.Add(new Estado_cuota(itemCuota.id, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual, "Cuota invalidada el momento de invalidar la venta"));
                }

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
                var operacionModificatoria = new OperacionModificatoria() { Operacion = invalidacionDeVenta, OrdenDeOperacion = ordenInvalidacionDeVenta, MovimientoEconomico = pago, MovimientosBienes = (salidasMercaderiaVenta.Count > 0) ? new List<Transaccion>() { entradaMercaderiaPorInvalidacionVenta } : null, NuevosEstadosTransaccionesModificadas = new List<Estado_transaccion>() { estadoDeLaOrdenDeVenta }, NuevosEstadosParaCuotasTransaccionesModificadas = estadosDeCuota, TransaccionesModificadas = transaccionesAModificar };
                //Ejecucion de la invalidacion de la venta
                var resultadoInvalidacion = AfectarInventarioFisicoYGuardarOperacion(operacionModificatoria, sesionUsuario);
                if (resultadoInvalidacion.code_result == OperationResultEnum.Success)
                {
                    if (ordenDeVenta.EstaTransmitido())
                    {
                        OperationResult resultadoEFactura = facturacionRepositorio.ActualizarEstadoDocumento(idOrdenDeVenta, EstadoDocumentoElectronico.Anulado, EstadoSigescomDocumentoElectronico.Invalidado);
                        if (resultadoEFactura.code_result != OperationResultEnum.Success)
                        {
                            resultadoInvalidacion.exceptions.Add(resultadoEFactura.exceptions.First());
                        }
                    }
                }
                return resultadoInvalidacion;
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar registrar la anulacion", e);
            }


        }
        */
        OperationResult AfectarExistenciasGuardarTransaccionyAgregarEstadoVenta(Transaccion OperacionDeVenta, List<Detalle_transaccion> detalles, int idAlmacen, Estado_transaccion estado, List<Estado_cuota> estadosCuota)
        {
            OperationResult result;
            do
            {
                //se afectan a las existencias restandole los detalles de la compra.
                List<Existencia> existencias = new List<Existencia>();
                existencias = transaccionRepositorio.ObtenerExistencias(detalles.Select(d => d.id_concepto_negocio).ToArray(), idAlmacen);
                //actualizamos las existencias
                foreach (var detalle in detalles)
                {
                    var existencia = existencias.Single(e => e.id_punto_atencion == idAlmacen && e.id_concepto_negocio == detalle.id_concepto_negocio);
                    //en caso no se cuente con disponibilidad de stock para algun producto, se retorna un error al controlador
                    if (OperacionDeVenta.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta)
                    {
                        existencia.existencia1 -= detalle.cantidad;
                    }
                    else
                    {
                        existencia.existencia1 += detalle.cantidad;
                    }
                }
                result = transaccionRepositorio.CrearTransaccionAgregarEstadoYActualizarExistencias(OperacionDeVenta, existencias, estado, estadosCuota);
            }
            while (result.code_result == OperationResultEnum.Error && result.exceptions.FirstOrDefault().GetType() == typeof(ExistenciaException));

            return result;
        }

        public OperationResult AnularVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, int idSerieComprobante, string observaciones)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                DateTime fechaActual = DateTimeUtil.FechaActual();

                // operaciones referencia a anular
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                // obtenemos el Id de la Unidad de negocio
                int idUnidadDeNegocio = operacionGenerica.IdUnidadNegocio;


                //Validamos si la accion es valida
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionAnular, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
                    idUnidadDeNegocio);

                // orden de venta a anular
                OrdenDeVenta ordenDeVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeVenta));

                // obtenemos el los datos de la orden de venta
                decimal tipoDeCambio = ordenDeVenta.TipoDeCambio;
                int idCliente = ordenDeVenta.Cliente().Id;
                long idVenta = ordenDeVenta.IdVenta;
                int idMedioDePago = ordenDeVenta.Venta().ObtenerPagos().First().TrazaDePago().MedioDePago().id;
                string informacionBancaria = ordenDeVenta.Venta().ObtenerPagos().First().TrazaDePago().Informacion;
                int idEntidadFinanciera = ordenDeVenta.Venta().ObtenerPagos().First().TrazaDePago().EntidadBancaria().id;

                //obtenemos los detalles a anular
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                foreach (var detalle in ordenDeVenta.Detalles())
                {
                    detalles.Add(new Detalle_transaccion(detalle.Cantidad, detalle.Producto.Id, "", detalle.PrecioUnitario, detalle.Importe, null, 0,
                        null, null, 0, detalle.Igv, detalle.Descuento));
                }
                var baseImponible = detalles.Sum(d => d.total);

                //Obtenemos la serie
                Serie_comprobante serieComprobanteAnulacion = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieComprobante);
                //validamos la serie
                if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && serieComprobanteAnulacion.numero.Substring(0, 1) != "B")
                {
                    throw new LogicaException("Serie Incorrecta. La serie de las Notas de credito referidas a Boletas de Venta, debe iniciar con B");
                }
                if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && serieComprobanteAnulacion.numero.Substring(0, 1) != "F")
                {
                    throw new LogicaException("Serie Incorrecta. La serie de las Notas de credito referidas a Facturas, debe iniciar con F");
                }
                int[] isdTipoDeComprobanteValidos = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura };
                if (!isdTipoDeComprobanteValidos.Contains(ordenDeVenta.IdTipoComprobante))
                {
                    throw new LogicaException("El tipo de comprobante de la operación de venta no califica para la emisión de una Nota de Credito");
                }

                //Obtenemos el tipo comprobante
                Comprobante comprobante = new Comprobante(serieComprobanteAnulacion.id_tipo_comprobante, serieComprobanteAnulacion.id, serieComprobanteAnulacion.proximo_numero, true, serieComprobanteAnulacion.numero);
                //conseguir numero siguiente
                serieComprobanteAnulacion.proximo_numero++;
                // creamos el codigo para la anulacion con nota interna
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("AV", TransaccionSettings.Default.IdTipoTransaccionAnulacionDeVenta);
                //crear una anulacion de venta
                Transaccion anulacionDeVenta = new Transaccion(codigo, operacionGenerica.Id, fechaActual, TransaccionSettings.Default.IdTipoTransaccionAnulacionDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible,
                    idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la anulacion venta
                anulacionDeVenta.Comprobante = comprobante;
                //crear una orden anulacion
                Transaccion ordenAnulacionDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_OAV",
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la orden anulacion
                ordenAnulacionDeVenta.Comprobante = comprobante;
                //agregamos la anulacion a la orden anulacion
                ordenAnulacionDeVenta.Transaccion2 = anulacionDeVenta;
                //agregamos los detalles
                ordenAnulacionDeVenta.AgregarDetalles(detalles);
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenAnulacionDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    fechaActual, "Estado inicial asignado automaticamente al registrar la anulacion");
                ordenAnulacionDeVenta.Estado_transaccion.Add(estadoDeLaOrdenAnulacionDeVenta);
                //agregamos la orden de compra como referencia
                ordenAnulacionDeVenta.id_transaccion_referencia = idOrdenDeVenta;
                //agregamos la orden anulacion en la anulacion
                anulacionDeVenta.Transaccion1.Add(ordenAnulacionDeVenta);
                //agregamos la orden de compra como referencia
                anulacionDeVenta.id_transaccion_referencia = idVenta;
                //crear cuota : cuenta por pagar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, ordenAnulacionDeVenta.importe_total,
                    "Unica cuota generada de forma automática al emitir la nota de credito", false);
                ordenAnulacionDeVenta.Cuota.Add(cuota);
                //crear el pago de la cuota
                string codigoPago = cuota.codigo + "_P" + 1.ToString();
                Transaccion pago = new Transaccion(codigoPago, null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorAnulacionDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, "", fechaActual, idEmpleado, baseImponible, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //vincular el pago con el comprobante
                pago.Comprobante = comprobante;
                //vincular el pago con la cuota.
                Pago_cuota pagoCuota = new Pago_cuota();
                pagoCuota.Transaccion = pago;
                pagoCuota.Cuota = cuota;
                pago.Pago_cuota.Add(pagoCuota);

                //creado traza 
                if (idMedioDePago == MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo)
                {
                    Traza_pago traza = new Traza_pago(idMedioDePago, informacionBancaria, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);

                    pago.Traza_pago.Add(traza);
                }
                else
                {
                    Traza_pago traza = new Traza_pago(idMedioDePago, informacionBancaria, idEntidadFinanciera);
                    pago.Traza_pago.Add(traza);
                }
                //agregamos la traza al pago
                //Traza_pago traza = new Traza_pago(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, "Pago efectivo", MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                //pago.Traza_pago.Add(traza);

                //agregar estado inicial: registrado
                Estado_transaccion estadoDelPago = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "");
                pago.Estado_transaccion.Add(estadoDelPago);

                //agregamos el cobro de la anulacion
                anulacionDeVenta.Transaccion1.Add(pago);

                //creamos la salida de mercaderia de por invalidacion de la venta
                Transaccion entradaMercaderiaPorAnulacionVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_EMAV",
                    TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente);

                //agregamos el comprobante a la orden de traslado de almacen
                entradaMercaderiaPorAnulacionVenta.Comprobante = comprobante;

                //agregamos los detalles
                entradaMercaderiaPorAnulacionVenta.AgregarDetalles(Detalle_transaccion.Clone(detalles));

                //agregamos el estado de la salida de mercaderia por defecto
                Estado_transaccion estadoEntradaMercaderiaPorAnulacionVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual,
                    "Estado inicial asignado automaticamente al confirmar la salida de mercaderia por anulacion de venta");

                //agregamos el estado de la orden de traslado interno a la salida de mercaderia
                entradaMercaderiaPorAnulacionVenta.Estado_transaccion.Add(estadoEntradaMercaderiaPorAnulacionVenta);

                //agregamos el trasldo interno a la orden de traslado 
                anulacionDeVenta.Transaccion1.Add(entradaMercaderiaPorAnulacionVenta);

                //creamos el estado invalidado para la orden de venta
                Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(idOrdenDeVenta, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaActual,
                    "Estado agregado al anular una venta");

                return AfectarExistenciasGuardarTransaccionyAgregarEstadoCompra(anulacionDeVenta, detalles, idCentroAtencion, estadoDeLaOrdenDeVenta);

            }
            catch (Exception e)
            {
                return new OperationResult(new Exception("Error al intentar registrar la anulacion", e));
            }

        }
        public OperationResult InvalidarAnulacionDeVenta(long idOrdenAnulacionDeVenta, int idEmpleado, int idCentroAtencion, string observaciones)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                DateTime fechaActual = DateTimeUtil.FechaActual();

                // operaciones referencia a anular
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                // obtenemos el Id de la Unidad de negocio
                int idUnidadDeNegocio = operacionGenerica.IdUnidadNegocio;
                // creamos el codigo para la invalidacion de la nota de credito
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("IAV", TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta);

                //Validamos si la accion es valida
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionInvalidar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
                    idUnidadDeNegocio);

                // orden anulacion de compra a invalidar
                OrdenDeAnulacionDeVenta ordenAnulacionDeVenta = new OrdenDeAnulacionDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenAnulacionDeVenta));

                // obtenemos el los datos de la orden de compra
                decimal tipoDeCambio = ordenAnulacionDeVenta.TipoDeCambio;
                int idCliente = ordenAnulacionDeVenta.Cliente().Id;
                long idAnulacionVenta = ordenAnulacionDeVenta.IdVenta;
                //obtenemos los detalles a anular
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                foreach (var detalle in ordenAnulacionDeVenta.Detalles())
                {
                    detalles.Add(new Detalle_transaccion(detalle.Cantidad, detalle.Producto.Id, "", detalle.PrecioUnitario, detalle.Importe, null, 0,
                        null, null, 0, detalle.Igv, detalle.Descuento));
                }
                var baseImponible = detalles.Sum(d => d.total);

                // obtenemos la serie de comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionAnulacionVenta, idCentroAtencion);
                // creamos el comprobante
                Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
                //conseguir el numero siguiente
                serie.proximo_numero++;

                //crear una invalidacion de anulacion de venta
                Transaccion invalidacionDeAnulacionDeVenta = new Transaccion(codigo, operacionGenerica.Id, fechaActual, TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible,
                    idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la invalidacion de anulacion venta
                invalidacionDeAnulacionDeVenta.Comprobante = comprobante;
                //crear una orden de invalidacion de  anulacion
                Transaccion ordenInvalidacionDeAnulacionDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_OIAV",
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //agregamos el comprobante a la orden anulacion
                ordenInvalidacionDeAnulacionDeVenta.Comprobante = comprobante;
                //agregamos la anulacion a la orden anulacion
                ordenInvalidacionDeAnulacionDeVenta.Transaccion2 = invalidacionDeAnulacionDeVenta;
                //agregamos los detalles
                ordenInvalidacionDeAnulacionDeVenta.AgregarDetalles(detalles);
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenInvalidacionDeAnulacionDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                    fechaActual, "Estado inicial asignado automaticamente al registrar la invalidacion de la anulacion de venta");
                ordenInvalidacionDeAnulacionDeVenta.Estado_transaccion.Add(estadoDeLaOrdenInvalidacionDeAnulacionDeVenta);

                //agregamos la orden de compra como referencia
                ordenInvalidacionDeAnulacionDeVenta.id_transaccion_referencia = idOrdenAnulacionDeVenta;

                //agregamos la orden anulacion en la anulacion
                invalidacionDeAnulacionDeVenta.Transaccion1.Add(ordenInvalidacionDeAnulacionDeVenta);

                //agregamos la orden de compra como referencia
                invalidacionDeAnulacionDeVenta.id_transaccion_referencia = idAnulacionVenta;

                //crear cuota : cuenta por cobrar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, ordenInvalidacionDeAnulacionDeVenta.importe_total,
                    "Unica cuota generada de forma automática al emitir la nota de invalidación", true);
                cuota.Estado_cuota.Add(new Estado_cuota(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, ""));
                ordenInvalidacionDeAnulacionDeVenta.Cuota.Add(cuota);

                //crear el cobro de la cuota
                string codigoPago = cuota.codigo + "_P" + 1.ToString();
                Transaccion pago = new Transaccion(codigoPago, null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionIngresoDineroInvalidacionDeAnulacionVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, "", fechaActual, idEmpleado, baseImponible, idCentroAtencion,
                    idMoneda, tipoDeCambio, null, idCliente);
                //vincular el pago con el comprobante
                pago.Comprobante = comprobante;
                //vincular el pago con la cuota.
                Pago_cuota pagoCuota = new Pago_cuota();
                pagoCuota.Transaccion = pago;
                pagoCuota.Cuota = cuota;
                pago.Pago_cuota.Add(pagoCuota);
                //agregamos la traza al pago
                Traza_pago traza = new Traza_pago(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, "Pago efectivo", MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                pago.Traza_pago.Add(traza);

                //agregar estado inicial: registrado
                Estado_transaccion estadoDelPago = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "");
                pago.Estado_transaccion.Add(estadoDelPago);

                //agregamos el cobro de la anulacion
                invalidacionDeAnulacionDeVenta.Transaccion1.Add(pago);

                //creamos la salida de mercaderia de por invalidacion de la venta
                Transaccion salidadDeMercaderiaPorInvalidacionDeAnulacionDeVenta = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_SMIAV",
                    TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta,
                    idUnidadDeNegocio, true, fechaActual, fechaActual, observaciones, fechaActual, idEmpleado, baseImponible, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente);

                //agregamos el comprobante a la orden de traslado de almacen
                salidadDeMercaderiaPorInvalidacionDeAnulacionDeVenta.Comprobante = comprobante;

                //agregamos los detalles
                salidadDeMercaderiaPorInvalidacionDeAnulacionDeVenta.AgregarDetalles(Detalle_transaccion.Clone(detalles));

                //agregamos el estado de la salida de mercaderia por defecto
                Estado_transaccion estadoSalidaMercaderiaPorInvalidacionDeAnulacionVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual,
                    "Estado inicial asignado automaticamente al confirmar el ingreso de mercaderia por invalidacion de anulacion de venta");

                //agregamos el estado de la orden de traslado interno a la salida de mercaderia
                salidadDeMercaderiaPorInvalidacionDeAnulacionDeVenta.Estado_transaccion.Add(estadoSalidaMercaderiaPorInvalidacionDeAnulacionVenta);

                //agregamos el trasldo interno a la orden de traslado 
                invalidacionDeAnulacionDeVenta.Transaccion1.Add(salidadDeMercaderiaPorInvalidacionDeAnulacionDeVenta);

                //creamos el estado invalidado para la orden de compra
                Estado_transaccion estadoDeLaOrdenDeAnulacionDeVenta = new Estado_transaccion(idOrdenAnulacionDeVenta, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaActual,
                    "Estado agregado al invalidar una anulacion de Venta");

                return AfectarExistenciasGuardarTransaccionyAgregarEstadoCompra(invalidacionDeAnulacionDeVenta, detalles, idCentroAtencion, estadoDeLaOrdenDeAnulacionDeVenta);

            }
            catch (Exception e)
            {
                return new OperationResult(new Exception("Error al intentar registrar la anulacion", e));
            }

        }

        OperationResult AfectarExistenciasGuardarTransaccionyAgregarEstadoCompra(Transaccion OperacionDeCompra, List<Detalle_transaccion> detalles, int idCentroAtencion, Estado_transaccion estado)
        {
            OperationResult result;
            do
            {
                //se afectan a las existencias restandole los detalles de la compra.
                List<Existencia> existencias = new List<Existencia>();
                int idAlmacen = AplicacionSettings.Default.StockCentralizado ? ActorSettings.Default.IdActorNegocioSede : idCentroAtencion;

                existencias = transaccionRepositorio.ObtenerExistencias(detalles.Select(d => d.id_concepto_negocio).ToArray(), idAlmacen);
                //actualizamos las existencias
                foreach (var detalle in detalles)
                {
                    var existencia = existencias.Single(e => e.id_punto_atencion == idAlmacen && e.id_concepto_negocio == detalle.id_concepto_negocio);
                    //en caso no se cuente con disponibilidad de stock para algun producto, se retorna un error al controlador
                    if (OperacionDeCompra.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra)
                    {
                        existencia.existencia1 += detalle.cantidad;
                    }
                    else
                    {
                        existencia.existencia1 -= detalle.cantidad;
                    }
                }
                result = transaccionRepositorio.CrearTransaccionAgregarEstadoYActualizarExistencias(OperacionDeCompra, existencias, estado);
            }
            while (result.code_result == OperationResultEnum.Error && result.exceptions.FirstOrDefault().GetType() == typeof(ExistenciaException));


            return result;
        }


        #region  REPORTE DE VENTA DE LOS PUNTOS DE VENTA POR COMPROBANTE

        public List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSerie(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesAgrupadasPorSerie(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();

                return resultado;
            }
            catch (LogicaException e)
            {
                throw new LogicaException("Error al obtener resumenes de comprobantes confirmadas de operaciones de ventas por serie", e);
            }
        }

        public List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSerie(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesAgrupadasPorSerie_(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener resumenes de comprobantes invalidados de operaciones de ventas por serie", e);
            }

        }

        #endregion


        #region OBTENER DATOS DE REPORTES CONSOLIDADOS

        public Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorA(int idEmpleado, int idSerie, DateTime fechaInicio, DateTime fechaFin, long idMinimo)
        {
            return transaccionRepositorio.ObtenerResumenTransaccionesDespuesDe(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaInicio, fechaFin, idSerie, idMinimo);
        }

        public Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(int idEmpleado, int idSerie, DateTime fechaInicio, DateTime fechaFin, long idMinimo, long idMaximo)
        {
            ///obtener aquellas cuyo ultimo estado es transmitido y el penultimo es confirmado
            return transaccionRepositorio.ObtenerResumenTransaccionesEntre(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaInicio, fechaFin, idSerie, idMinimo, idMaximo);
        }

        public List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(int idEmpleado, int idSerie, long idMinimo, long idMaximo)
        {
            /////obtener aquellas cuyo ultimo estado es transmitido y el penultimo es confirmado
            ////return _repositorioTransaccion.ObtenerTransaccionesConsolidadasEntre(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idSerie, idMinimo, idMaximo).ToList();

            ///obtener aquellas cuyo ultimo estado es transmitido y el penultimo es confirmado o que el ultimo estado sea anulado.
            return transaccionRepositorio.ObtenerTransaccionesConsolidadasEntre(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idSerie, idMinimo, idMaximo).ToList();
        }

        public Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMenorA(int idEmpleado, int idSerie, DateTime fechaInicio, DateTime fechaFin, long idMaximo)
        {
            return transaccionRepositorio.ObtenerResumenTransaccionesAntesDe(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaInicio, fechaFin, idSerie, idMaximo);
        }

        public Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadas(int idEmpleado, int idSerie, DateTime fechaInicio, DateTime fechaFin)
        {
            return transaccionRepositorio.ObtenerResumenTransacciones(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaInicio, fechaFin, idSerie);
        }

        #endregion

        #region LIBROS ELECTRONICOS

        public List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorA(int idEmpleado, int idSerie, long numeroDeComprobanteMinimo, long numeroDeComprobanteMaximo)
        {
            try
            {
                return transaccionRepositorio.ObtenerTransaccionesConsolidadasEntreNumeroDeComprobante(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idSerie, numeroDeComprobanteMinimo, numeroDeComprobanteMaximo).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obtener ordenes de ventas confirmadas", e);
            }
        }


        public List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorAFechaDesdeHasta(int idEmpleado, int idSerie, long numeroDeComprobanteMinimo, long numeroDeComprobanteMaximo, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return transaccionRepositorio.ObtenerTransaccionesConsolidadasEntreNumeroDeComprobantefechaHasta(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idSerie, numeroDeComprobanteMinimo, numeroDeComprobanteMaximo, fechaDesde, fechaHasta).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obtener ordenes de ventas confirmadas", e);
            }
        }


        #endregion

        #region OBTENCION DE DATOS 

        public List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioConfirmada(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)//confirmada XY5.1
        {
            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstados(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoConfirmadaYTransmitida(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)// confirmada XY5.2
        {
            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstados(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)//invalidadas XY5.3
        {

            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstados(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();  //4.2

            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstados(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();  //
            return resultado;
        }

        public List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)// XY5.5
        {
            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;

        }

        public List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)// XY5.6
        {
            var resultado = transaccionRepositorio.ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstado(idCentroAtencion, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;

        }

        public List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptoInvalidadas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta) //Invalidada C1.2
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(idEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptoAnuladas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta) //Anuladas C1.3
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(idEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //EL reporte venta de concepto por vendedor
        public List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptConfirmadas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta) //confirmado C1.1
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(idEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //El estado para este reporte de resumen de ventas por vendedor sera el de Confirmado
        public List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorConfirmadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//confirmada a14.1
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();
            return resultado;
        }

        //El estado para este reporte de resumen de ventas por vendedor sera el de Invalidado
        public List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorInvalidadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//invalidada a14.2
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).ToList();
            return resultado;
        }

        public List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorAnuladas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//Anuladas a14.3
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        //El estado para este reporte DETALLADO de ventas por vendedor sera el de Confirmado
        public List<DetalleTransaccionPorVendedor> ObtenerDetalleDeVentasPorVendedorConfirmadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//a14.4
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();
            return resultado;
        }

        //El estado para este reporte DETALLADO de ventas por vendedor sera el de Invalidado
        public List<DetalleTransaccionPorVendedor> ObtenerDetallaDeVentasPorVendedorInvalidadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//a14.5
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        //El estado para este reporte DETALLADO de ventas por vendedor sera el de anulado
        public List<DetalleTransaccionPorVendedor> ObtenerDetallaDeVentasPorVendedorAnuladas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)//a14.6
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(idsEmpleado, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        #endregion

        #region REPORTES DE VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO Y PRECIO UNITARIO

        //RESUMEN
        public List<Resumen_Transaccion_Por_Modalidad> ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveParametroTransaccionActoresYDetalleMaestroYEstado(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Por_Modalidad> ObtenerResumenesDeVentasPorModalidadYPorVendedorInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerResumenTransaccionesInclusiveParametroTransaccionActoresYDetalleMaestroYEstado(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }
        //DETALLES
        public List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocio(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }

        public List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocio(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }

        public List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocioYPrecioUnitario(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }

        public List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocioYPrecioUnitario(idEmpleado, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, modalidades).ToList();
            return resultado;
        }

        #endregion

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = new List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio>();

            var resumenOrdenesEnLAsQueIngresaDineroYSalenBienes = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDineroYSalenBienes, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();

            var resumenOrdenesEnLAsQueSaleDineroEIngresanBienes = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDineroEIngresanBienesExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();

            //Hacer valores negativos
            foreach (var item in resumenOrdenesEnLAsQueSaleDineroEIngresanBienes)
            {
                item.Cantidad *= -1;
                item.Importe *= -1;
            }

            //conseguimos los ids de los conceptos de negocio comunes entre ambas colecciones
            int[] idsConceptosDeNegocioComunes = resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.Select(o => o.IdConceptoNegocio).Intersect(resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.Select(o => o.IdConceptoNegocio)).Distinct().ToArray();

            resultado.AddRange(resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.Where(o => !idsConceptosDeNegocioComunes.Contains(o.IdConceptoNegocio)));

            resultado.AddRange(resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.Where(o => !idsConceptosDeNegocioComunes.Contains(o.IdConceptoNegocio)));

            //Unificar los items comunes
            foreach (var idConceptoNegocio in idsConceptosDeNegocioComunes)
            {
                var itemPositivo = resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.SingleOrDefault(o => o.IdConceptoNegocio == idConceptoNegocio);
                var itemNegativo = resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.SingleOrDefault(o => o.IdConceptoNegocio == idConceptoNegocio);

                resultado.Add(new Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio() { IdConceptoNegocio = idConceptoNegocio, CodigoBarra = itemPositivo.CodigoBarra, ConceptoNegocio = itemPositivo.ConceptoNegocio, NombreBasico = itemPositivo.NombreBasico, Cantidad = itemPositivo.Cantidad + itemNegativo.Cantidad, Importe = itemPositivo.Importe + itemNegativo.Importe });
            }
            return resultado.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio).ToList();
        }
        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoInvalidadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDineroYSalenBienes, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoAnuldada(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta) //anulada XY1.3
        {
            var resultado = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }

        #region REPORTES POR VENDEDORES
        public List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorPrecioUnitarioPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerResumenDetallesConsolidadoPorVendedor(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idEmpleado, fechaDesde, fechaHasta).ToList();
            var resumen = resultado.GroupBy(r => new
            {
                r.IdConceptoNegocio,
                r.PrecioUnitario
            }).Select(r => new Resumen_Detalles_Consolidado_Por_Vendedor()
            {
                IdConceptoNegocio = r.FirstOrDefault().IdConceptoNegocio,
                CodigoBarra = r.FirstOrDefault().CodigoBarra,
                ConceptoNegocio = r.FirstOrDefault().ConceptoNegocio,
                Cantidad = r.Sum(c => c.CantidadTotal),
                Importe = r.Sum(i => i.ImporteTotal)
            }).ToList();
            resumen = resumen.Where(r => r.Cantidad != 0).ToList();
            return resumen;
        }
        public List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerResumenDetallesConsolidadoPorVendedor(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idEmpleado, fechaDesde, fechaHasta).ToList();
            var resumen = resultado.GroupBy(r => r.IdConceptoNegocio)
                .Select(r => new Resumen_Detalles_Consolidado_Por_Vendedor()
                {
                    IdConceptoNegocio = r.FirstOrDefault().IdConceptoNegocio,
                    CodigoBarra = r.FirstOrDefault().CodigoBarra,
                    ConceptoNegocio = r.FirstOrDefault().ConceptoNegocio,
                    Cantidad = r.Sum(c => c.CantidadTotal),
                    Importe = r.Sum(i => i.ImporteTotal)
                }).ToList();
            resumen = resumen.Where(r => r.Cantidad != 0).ToList();

            return resumen;
        }
        public List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoBasicoPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerResumenDetallesConsolidadoPorVendedor(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idEmpleado, fechaDesde, fechaHasta).ToList();
            var resumen = resultado.GroupBy(r => r.IdConceptoBasico)
                .Select(r => new Resumen_Detalles_Consolidado_Por_Vendedor()
                {
                    IdConceptoBasico = r.FirstOrDefault().IdConceptoBasico,
                    ConceptoBasico = r.FirstOrDefault().ConceptoBasico,
                    Cantidad = r.Sum(c => c.CantidadTotal),
                    Importe = r.Sum(i => i.ImporteTotal)
                }).ToList();
            resumen = resumen.Where(r => r.Cantidad != 0).ToList();

            return resumen;
        }
        //-//
        public List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDeVentasAgrupadasPorFamilia(DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsEmpleado = actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolVendedor, true).Select(an => an.id).ToArray();
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerResumenDetallesConsolidadoPorVendedores(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsEmpleado, fechaDesde, fechaHasta).ToList();
            var resumen = resultado.GroupBy(r => r.IdConceptoBasico)
                .Select(r => new Resumen_Detalles_Consolidado_Por_Vendedor()
                {
                    IdConceptoBasico = r.FirstOrDefault().IdConceptoBasico,
                    ConceptoBasico = r.FirstOrDefault().ConceptoBasico,
                    Cantidad = r.Sum(c => c.CantidadTotal),
                    Importe = r.Sum(i => i.ImporteTotal)
                }).ToList();
            resumen = resumen.Where(r => r.Cantidad != 0).ToList();

            return resumen;
        }
        public List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDeVentasAgrupadasPorFamiliaYVendedor(DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsEmpleado = actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolVendedor, true).Select(an => an.id).ToArray();
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerResumenDetallesConsolidadoPorVendedores(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsEmpleado, fechaDesde, fechaHasta).ToList();

            var resumen = resultado.GroupBy(r => new { r.IdConceptoNegocio, r.ConceptoBasico })
                .Select(r => new Resumen_Detalles_Consolidado_Por_Vendedor()
                {
                    Empleado = r.FirstOrDefault().Empleado,
                    ConceptoNegocio = r.FirstOrDefault().ConceptoNegocio,
                    IdConceptoBasico = r.FirstOrDefault().IdConceptoBasico,
                    ConceptoBasico = r.FirstOrDefault().ConceptoBasico,
                    Cantidad = r.Sum(c => c.CantidadTotal),
                    Importe = r.Sum(i => i.ImporteTotal)
                }).ToList();
            resumen = resumen.Where(r => r.Cantidad != 0).ToList();
            return resumen;
        }
        //-//
        public List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> ObtenerResumenVentasPorConceptoPorVendedorContadoCredito(DateTime fechaDesde, DateTime fechaHasta)
        {
            int[] idsEmpleado = actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolVendedor, true).Select(an => an.id).ToArray();
            int[] idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
            var resultado = transaccionRepositorio.ObtenerConsolidadoPorVendedoresPorModoPagoPorConcepto(idsTipoTransaccion, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsEmpleado, MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)ModoPago.Contado).ToString(), fechaDesde, fechaHasta).ToList();
            List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> resumen = new List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito>();
            List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> resumenPorEmpleado;
            foreach (var idEmpleado in idsEmpleado)
            {

                resumenPorEmpleado = new List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito>();
                bool empleadoTieneRegistro = false;
                foreach (var idConcepto in resultado.Select(r => r.IdConceptoNegocio).Distinct())
                {
                    var contado = resultado.Where(r => r.IdEmpleado == idEmpleado && r.IdConceptoNegocio == idConcepto && r.EsContado).ToList();
                    var credito = resultado.Where(r => r.IdEmpleado == idEmpleado && r.IdConceptoNegocio == idConcepto && !r.EsContado).ToList();
                    var objeto = (contado.Count() > 0) ? contado : (credito.Count() > 0) ? credito : null;
                    if (objeto != null)
                    {
                        resumenPorEmpleado.Add(new Resumen_Por_Concepto_Por_Vendedor_Contado_Credito()
                        {
                            IdConceptoNegocio = idConcepto,
                            CodigoBarra = objeto.First().CodigoBarra,
                            ConceptoNegocio = objeto.First().ConceptoNegocio,
                            IdEmpleado = idEmpleado,
                            Empleado = objeto.First().Empleado,
                            CantidadContado = contado == null ? 0 : contado.Sum(c => c.CantidadConSigno),
                            ImporteContado = contado == null ? 0 : contado.Sum(c => c.ImporteConSigno),
                            CantidadCredito = credito == null ? 0 : credito.Sum(c => c.CantidadConSigno),
                            ImporteCredito = credito == null ? 0 : credito.Sum(c => c.ImporteConSigno),
                        });
                        empleadoTieneRegistro = empleadoTieneRegistro || true;
                    }
                    else
                    {
                        resumenPorEmpleado.Add(new Resumen_Por_Concepto_Por_Vendedor_Contado_Credito()
                        {
                            IdConceptoNegocio = idConcepto,
                            CodigoBarra = resultado.Where(r => r.IdConceptoNegocio == idConcepto).First().CodigoBarra,
                            ConceptoNegocio = resultado.Where(r => r.IdConceptoNegocio == idConcepto).First().ConceptoNegocio,
                            IdEmpleado = idEmpleado,
                            CantidadContado = 0,
                            ImporteContado = 0,
                            CantidadCredito = 0,
                            ImporteCredito = 0,
                        });
                        empleadoTieneRegistro = empleadoTieneRegistro || false;
                    }
                }
                if (empleadoTieneRegistro)
                {
                    resumenPorEmpleado.ForEach(r => r.Empleado = resumenPorEmpleado.Where(rr => rr.Empleado != null).FirstOrDefault().Empleado);
                    resumenPorEmpleado = resumenPorEmpleado.Where(re => re.CantidadContado != 0 || re.CantidadCredito != 0).ToList();
                    resumen.AddRange(resumenPorEmpleado);
                }
            }


            return resumen;
        }
        #endregion



        #region OBTENER ORDENES DE VENTA CONFIRMADAS, ANULADAS, INVALIDADAS

        public List<OperacionDeVenta> ObtenerOperacionesDeVentaConfirmadasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {

            try
            {
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionPorActorDeNegocio(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idCliente, fechaDesde, fechaHasta).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<OperacionDeVenta> ObtenerOperacionesDeVentaInvalidadasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionPorActorDeNegocio(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, idCliente, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<OperacionDeVenta> ObtenerOperacionesDeVentaAnuladasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {

            try
            {
                return OperacionDeVenta.Convert_(transaccionRepositorio.ObtenerTransaccionesInclusiveDetalleTransaccionPorActorDeNegocio(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoAnulado, idCliente, fechaDesde, fechaHasta).ToList());

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MovimientoEconomico> ObtenerCobranzasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                int[] idsEstadosDeTransaccion = { MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                                                };
                var resultado = transaccionRepositorio.ObtenerTransaccionesPorActorDeNegocio(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes
                  , idCliente, fechaDesde, fechaHasta).ToList();

                return MovimientoEconomico.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cobros", e);
            }
        }

        public EstadoDeCuenta ObtenerEstadoDeCuentaCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {

            //Calcular la deuda menor a la fecha desde  Calcular la deudas de ventas confirmadas?
            decimal saldoAnterior = ObtenerDeudasPorCliente(idCliente, fechaDesde).Deuda();


            decimal totalEntregas = 0, totalPagos = 0;
            decimal saldo = saldoAnterior;

            EstadoDeCuenta estadoDeCuenta = new EstadoDeCuenta();
            List<EstadoDeCuenta.DetalleEstadoDeCuenta> detalleEstadoDeCuenta = new List<EstadoDeCuenta.DetalleEstadoDeCuenta>();

            //Obtener las  ventas confirmadas mayor igual  a la fecha desde y menor igual a la fecha hasta
            List<OperacionDeVenta> confirmadas = ObtenerOperacionesDeVentaConfirmadasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();

            //Obtener los cobros realizados mayor igual a la fecha desde y menor igual a la fecha hasta
            List<MovimientoEconomico> cobranzas = ObtenerCobranzasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();

            //Unir las operaciones y ordenarlo por fecha de emision
            List<OperacionGenericaNivel3> operaciones = (OperacionGenericaNivel3.Convertir(confirmadas).Union(OperacionGenericaNivel3.Convertir(cobranzas))).OrderBy(o => o.FechaEmision).ToList();

            List<OperacionClasificada> operacionesClasificadas = OperacionClasificada.Clasificar(operaciones).ToList();

            //Recorrer las operaciones clasificads y decidir si es de entrada o es un pago
            foreach (var operacionClasificada in operacionesClasificadas)
            {
                int clasificador = operacionClasificada.Clasificador;
                string nombreOperacion = clasificador == 1 ? "EN" : "PA";
                if (nombreOperacion == "EN")
                {
                    foreach (var item in operacionClasificada.Operacion.DetalleTransaccion())
                    {
                        saldo = saldo + item.total;
                        detalleEstadoDeCuenta.Add(new EstadoDeCuenta.DetalleEstadoDeCuenta(operacionClasificada.Operacion.FechaEmision, nombreOperacion, item.cantidad, item.Concepto_negocio.nombre, item.precio_unitario, item.total, saldo));
                        totalEntregas = totalEntregas + item.total;
                    }
                }
                else
                {
                    decimal total = operacionClasificada.Operacion.Total;
                    saldo = saldo - total;
                    detalleEstadoDeCuenta.Add(new EstadoDeCuenta.DetalleEstadoDeCuenta(operacionClasificada.Operacion.FechaEmision, nombreOperacion, 0, "", 0, -total, saldo));
                    totalPagos = totalPagos + total;
                }
            }

            estadoDeCuenta.SaldoAnterior = saldoAnterior;
            estadoDeCuenta.Entregas = totalEntregas;
            estadoDeCuenta.Pagos = totalPagos;
            estadoDeCuenta.SaldoFinal = (totalEntregas - totalPagos) + saldoAnterior;
            estadoDeCuenta.Detalle = detalleEstadoDeCuenta.OrderBy(dec => dec.Fecha.Date).ThenBy(dec => dec.Fecha.TimeOfDay).ToList();


            return estadoDeCuenta;
        }

        public EstadoDeCuenta ObtenerEstadoDeCuentaConVentasAnuladasEInvalidadasCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<OperacionDeVenta> confirmadas = ObtenerOperacionesDeVentaConfirmadasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();
            List<OperacionDeVenta> invalidas = ObtenerOperacionesDeVentaInvalidadasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();
            List<OperacionDeVenta> anuladas = ObtenerOperacionesDeVentaAnuladasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();
            List<MovimientoEconomico> cobranzas = ObtenerCobranzasPorCliente(idCliente, fechaDesde, fechaHasta).ToList();
            List<OperacionGenericaNivel3> operaciones = (OperacionGenericaNivel3.Convertir(confirmadas).Union(OperacionGenericaNivel3.Convertir(invalidas))
                                                                                                       .Union(OperacionGenericaNivel3.Convertir(anuladas))
                                                                                                       .Union(OperacionGenericaNivel3.Convertir(cobranzas)))
                                                                                                       .OrderBy(o => o.FechaEmision).ToList();


            List<OperacionClasificada> operacionesClasificadas = OperacionClasificada.Clasificar(operaciones).OrderBy(oc => oc.Clasificador).ToList();
            EstadoDeCuenta estadoDeCuenta = new EstadoDeCuenta();
            List<EstadoDeCuenta.DetalleEstadoDeCuenta> detalleEstadoDeCuenta = new List<EstadoDeCuenta.DetalleEstadoDeCuenta>();
            decimal saldoAnterior = ObtenerDeudasPorCliente(idCliente, fechaDesde).Deuda();
            decimal totalEntregas = 0, totalPagos = 0;
            decimal saldo = 0;
            saldo = saldoAnterior;

            foreach (var operacionClasificada in operacionesClasificadas)
            {
                int clasificador = operacionClasificada.Clasificador;
                string nombreOperacion = clasificador == 1 ? "EN" : clasificador == 2 ? "IN" : clasificador == 3 ? "AN" : "PA";
                if (nombreOperacion == "EN")
                {

                    foreach (var item in operacionClasificada.Operacion.DetalleTransaccion())
                    {
                        saldo = saldo + item.total;
                        detalleEstadoDeCuenta.Add(new EstadoDeCuenta.DetalleEstadoDeCuenta(operacionClasificada.Operacion.FechaEmision, nombreOperacion, item.cantidad, item.Concepto_negocio.nombre, item.precio_unitario, item.total, saldo));
                        totalEntregas = totalEntregas + item.total;

                    }
                }
                else
                {
                    if (nombreOperacion == "IN" || nombreOperacion == "AN")
                    {

                        foreach (var item in operacionClasificada.Operacion.DetalleTransaccion())
                        {
                            saldo = saldo - item.total;
                            detalleEstadoDeCuenta.Add(new EstadoDeCuenta.DetalleEstadoDeCuenta(operacionClasificada.Operacion.FechaEmision, nombreOperacion, item.cantidad, item.Concepto_negocio.nombre, item.precio_unitario, item.total, -saldo));

                        }
                    }
                    else
                    {
                        decimal total = operacionClasificada.Operacion.Total;
                        saldo = saldo - total;
                        detalleEstadoDeCuenta.Add(new EstadoDeCuenta.DetalleEstadoDeCuenta(operacionClasificada.Operacion.FechaEmision, nombreOperacion, 0, "", 0, -total, saldo));
                        totalPagos = totalPagos + total;
                    }

                }
            }
            estadoDeCuenta.SaldoAnterior = saldoAnterior;
            estadoDeCuenta.Entregas = totalEntregas;
            estadoDeCuenta.Pagos = totalPagos;
            estadoDeCuenta.SaldoFinal = (totalEntregas - totalPagos) + saldoAnterior;
            estadoDeCuenta.Detalle = detalleEstadoDeCuenta;
            return estadoDeCuenta;
        }

        public Deuda_Actor_Negocio ObtenerDeudasPorCliente(int idCliente, DateTime fecha)
        {
            ////si no hay ventas o cobros en el dia para tal punto de venta, traer las deudas a las 00:00hrs
            ////fechaMaxima
            //DateTime? fechaUltimaVenta = _repositorioTransaccion.ObtenerFechaInicioUltimaTransaccion(idCliente,
            //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido,
            //    MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fecha.Date.AddDays(1).AddMilliseconds(-1));

            ////obtener la fecha del ultimo cobro por venta de algun cliente de la cartera de cliente
            //DateTime? fechaUltimaCobranza = _repositorioTransaccion.ObtenerFechaUltimaTransaccionActorNegocioExterno(idCliente,
            //    TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, fecha.Date.AddDays(1).AddMilliseconds(-1));
            ////la fecha de la deuda debe ser la mayor entre la ultima cobranza, la ultima venta.


            //var fechaDeuda = (fechaUltimaVenta == null && fechaUltimaCobranza == null) ? fecha
            //    : (fechaUltimaVenta != null && fechaUltimaCobranza != null) ?
            //    ((DateTime)(DateTime.Compare((DateTime)fechaUltimaVenta, (DateTime)fechaUltimaCobranza) > 0 ? fechaUltimaVenta : fechaUltimaCobranza))
            //    : ((DateTime)(fechaUltimaVenta != null ? fechaUltimaVenta : fechaUltimaCobranza));

            return transaccionRepositorio.ObtenerDeudaActorNegocio(idCliente, fecha);
        }

        #endregion

        #region METODOS OTROS

        public DateTime FechaActual()
        {
            return DateTimeUtil.FechaActual();
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeVenta ordenDeVenta)
        {
            string asunto = ordenDeVenta.Comprobante().NombreTipo + " " + ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " " + sede.Nombre;
            return asunto;
        }

        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeVenta ordenDeVenta, string host, List<LinkedResource> resources)
        {
            string cuerpo = @"<html>
                      <body>
                      <p> Estimado usuario: " + ordenDeVenta.Cliente().RazonSocial + @"</p>
                      <p> Adjuntamos la " + ordenDeVenta.Comprobante().NombreTipo + " " + ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + @" Emitida por " + sede.Nombre + " el " + ordenDeVenta.FechaEmision.ToString("dd/MM/yyyy") + @".</p>
                      <p>Atentamente,</p>
                      <h4>Siges Comercial.</h4>
                      </body>" +
                      ObtenerPieDePaginaCorreoElectronico(host, resources) +
                      "</html>";
            return cuerpo;
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoDeAlmacen movimientoDeAlmacen)
        {
            string asunto = sede.Nombre + ", Documento : " + movimientoDeAlmacen.Comprobante().NumeroDeSerie + " - " + movimientoDeAlmacen.Comprobante().NumeroDeComprobante;
            return asunto;
        }

        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoDeAlmacen movimientoDeAlmacen)
        {
            string cuerpo = @"<html>
                      <body>
                      <p>" + sede.Nombre + @"</p>
                      <p>Documento : " + movimientoDeAlmacen.Comprobante().NumeroDeSerie + " - " + movimientoDeAlmacen.Comprobante().NumeroDeComprobante + @" Adjuntado,</p>
                      <p>Saludos.</p>
                      <p>SIGES (Sistema de Gestion Comercial), Un producto de Tech Solutions Perú<br>www.siges.tsolperu.com</br></p>
                      </body>
                      </html>";
            return cuerpo;
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeMovimientoDeAlmacen ordenDeAlmacen)
        {
            string asunto = sede.Nombre + ", Documento : " + ordenDeAlmacen.Comprobante().NumeroDeSerie + " - " + ordenDeAlmacen.Comprobante().NumeroDeComprobante;
            return asunto;
        }

        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeMovimientoDeAlmacen ordenDeAlmacen)
        {
            string cuerpo = @"<html>
                      <body>
                      <p>" + sede.Nombre + @"</p>
                      <p>Documento : " + ordenDeAlmacen.Comprobante().NumeroDeSerie + " - " + ordenDeAlmacen.Comprobante().NumeroDeComprobante + @" Adjuntado,</p>
                      <p>Saludos.</p>
                      <p>SIGES (Sistema de Gestion Comercial), Un producto de Tech Solutions Perú<br>www.siges.tsolperu.com</br></p>
                      </body>
                      </html>";
            return cuerpo;
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoEconomico movimiento)
        {
            string asunto = sede.Nombre + ", Documento : " + movimiento.Comprobante().NumeroDeSerie + " - " + movimiento.Comprobante().NumeroDeComprobante;
            return asunto;
        }

        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoEconomico movimiento)
        {
            string cuerpo = @"<html>
                      <body>
                      <p>" + sede.Nombre + @"</p>
                      <p>Documento : " + movimiento.Comprobante().NumeroDeSerie + " - " + movimiento.Comprobante().NumeroDeComprobante + @" Adjuntado,</p>
                      <p>Saludos.</p>
                      <p>SIGES (Sistema de Gestion Comercial), Un producto de Tech Solutions Perú<br>www.siges.tsolperu.com</br></p>
                      </body>
                      </html>";
            return cuerpo;
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OperacionTipoTransaccionTipoComprobante operacion)
        {
            string asunto = operacion.TipoComprobante + " " + operacion.Comprobante + " " + sede.Nombre;
            return asunto;
        }

        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OperacionTipoTransaccionTipoComprobante operacion, string host, List<LinkedResource> resources)
        {
            string cuerpo = @"<html>
                      <body>
                      <p> Estimado usuario: " + operacion.Tercero + @"</p>
                      <p> Adjuntamos la " + operacion.TipoComprobante + " " + operacion.Comprobante + @" Emitida por " + sede.Nombre + " el " + operacion.FechaEmision + @".</p>
                      <p>Atentamente,</p>
                      <h4>Siges Comercial.</h4>
                      </body>" +
                      ObtenerPieDePaginaCorreoElectronico(host, resources) +
                      "</html>";
            return cuerpo;
        }

        public string ObtenerPieDePaginaCorreoElectronico(string host, List<LinkedResource> resources)
        {
            string mediaType = MediaTypeNames.Image.Jpeg;
            LinkedResource logoSiges = new LinkedResource(host + @"/content/images/logo-siges.png", mediaType);
            LinkedResource logoTsp = new LinkedResource(host + @"/content/images/logo-tsp.png", mediaType);

            logoSiges.ContentId = "logoSiges";
            logoTsp.ContentId = "logoTsp";
            logoSiges.ContentType.MediaType = logoTsp.ContentType.MediaType = mediaType;
            logoSiges.TransferEncoding = logoTsp.TransferEncoding = TransferEncoding.Base64;
            logoSiges.ContentType.Name = logoTsp.ContentType.Name = logoSiges.ContentId;
            logoSiges.ContentLink = new Uri("cid:" + logoSiges.ContentId);
            logoTsp.ContentLink = new Uri("cid:" + logoTsp.ContentId);

            resources.Add(logoSiges);
            resources.Add(logoTsp);


            string cuerpo = @"<div font-size='small' style='user-select: none;'>
                <table cellpadding='0' cellspacing='0' style='vertical-align: -webkit-baseline-middle; font-size: small; font-family: Arial;'>
                   <tbody>
                      <tr>
                         <td>
                            <table cellpadding='0' cellspacing='0' style='vertical-align: -webkit-baseline-middle; font-size: small; font-family: Arial;'>
                               <tbody>
                                  <tr>
                                     <td style='vertical-align: top;'>
                                        <table cellpadding='0' cellspacing='0' style='vertical-align: -webkit-baseline-middle; font-size: small; font-family: Arial;'>
                                           <tbody>
                                              <tr>
                                                 <td style='text-align: center;'><img src='" + logoSiges.ContentLink + @"' width='130' style='max-width: 100px; display: block;'></td>
                                              </tr>
                                              <tr>
                                                 <td height='10'></td>
                                              </tr>
                                              <tr>
                                                <td style='text-align: center;'><img src='" + logoTsp.ContentLink + @"' width='130' style='max-width: 100px; display: block;'></td>
                                             </tr>
                                           </tbody>
                                        </table>
                                     </td>
                                     <td width='20'>
                                        <div></div>
                                     </td>
                                     <td style='padding: 0px; vertical-align: middle;'>
                                        <h3 color='#000000' style='margin: 0px; font-size: 16px; color: rgb(0, 0, 0);'><span>SIGES - Sistema de Gestión Comercial y Facturación Electrónica</span></h3>
                                        <p color='#000000' font-size='small' style='margin: 0px; color: rgb(0, 0, 0); font-size: 12px; line-height: 20px;'><span>Todo funciona mejor</span></p>
                                        <p color='#000000' font-size='small' style='margin: 0px; font-weight: 500; color: rgb(0, 0, 0); font-size: 12px; line-height: 20px;'><span>www.siges.tsolperu.com</span></p>
                                        <table cellpadding='0' cellspacing='0' style='vertical-align: -webkit-baseline-middle; font-size: small; font-family: Arial; width: 100%;'>
                                           <tbody>
                                              <tr>
                                                 <td height='20'></td>
                                              </tr>
                                              <tr>
                                                 <td color='#F2547D' direction='horizontal' height='1' style='width: 100%; border-bottom: 1px solid rgb(242, 84, 125); border-left: none; display: block;'></td>
                                              </tr>
                                              <tr>
                                                 <td height='30'></td>
                                              </tr>
                                           </tbody>
                                        </table>
                                        <h3 color='#000000' style='margin: 0px; font-size: 16px; color: rgb(0, 0, 0);'><span>SIGES es un producto de TECH SOLUTIONS PERÚ E.I.R.L.</span></h3>
                                        <p color='#000000' font-size='small' style='margin: 0px; color: rgb(0, 0, 0); font-size: 12px; line-height: 20px;'><span>www.tsolperu.com</span></p>
                                     </td>
                                  </tr>
                               </tbody>
                            </table>
                         </td>
                      </tr>
                   </tbody>
                </table>
             </div>";
            return cuerpo;
        }

        public OperationResult CrearEstadosDeTransacciones(List<Estado_transaccion> estadosDeTransacciones)
        {
            try
            {
                return transaccionRepositorio.CrearEstadosMasivosDeTransacciones(estadosDeTransacciones);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear los estados de las transacciones", e);
            }
        }

        public OperationResult CrearEventosDeTransacciones(List<Evento_transaccion> eventosDeTransacciones)
        {
            try
            {
                return transaccionRepositorio.CrearEventosMasivosDeTransacciones(eventosDeTransacciones);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear los eventos de las transacciones", e);
            }
        }

        public OperationResult CrearEventoTransaccion(Evento_transaccion eventoTransaccion)
        {
            try
            {
                return transaccionRepositorio.CrearEventoTransaccion(eventoTransaccion);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear los eventos de las transacciones", e);
            }
        }

        public OperationResult CrearEventoTransaccionInformacionTransaccion(Evento_transaccion eventoTransaccion, string informacionTransaccion)
        {
            try
            {
                return transaccionRepositorio.CrearEventoTransaccionInformacionTransaccion(eventoTransaccion, informacionTransaccion);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear los eventos de las transacciones", e);
            }
        }
        #endregion

        #region NOTA DE DEBITO Y CREDITO DE VENTA

        public OperationResult GuardarNotaDeDebitoDeVenta(long idOrdenDeVenta, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario)
        {
            try
            {
                //Obtenemos la orden de venta referencia de la nota de credito
                OrdenDeVenta ordenDeVenta = new OrdenDeVenta(transaccionRepositorio.ObtenerTransaccionInclusiveEstados(idOrdenDeVenta));
                //Obtenemos la unidad de negocio
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                //Obtenemos la fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Validamos la accion a realizar
                permisos_Logica.ValidarAccion(sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, idUnidadNegocio);
                //Calculamos los valores de detalles
                List<Detalle_transaccion> detallesDeNota = CalcularDetalleNotaDebitoCredito(detalles, ordenDeVenta.DetalleTransaccion(), idTipoNota, valorDeNota, motivo, ordenDeVenta.Igv() > 0);
                //Generamos la nota de debito 
                Transaccion notaDeDebito = GenerarNotaDeCreditoDebito(sesionUsuario.Empleado.Id, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaActual, "ND", Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(m => m.Key == idTipoNota).Value, detallesDeNota.Sum(d => d.total), motivo, ordenDeVenta.Cliente().Id, sesionUsuario.IdCentroDeAtencionSeleccionado, sesionUsuario);
                //Decidir que modo de pago tendra la nota de debito
                ModoPago modoPago = ordenDeVenta.ModoDePago();
                //Generamos la orden de la nota de debito
                Transaccion ordenDeNotaDeDebito = GenerarOrdenNotaDeCreditoDebito(notaDeDebito, sesionUsuario.Empleado.Id, idUnidadNegocio, idTipoNota, fechaActual, ((int)modoPago).ToString(), "ND", Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value, motivo, ordenDeVenta.Cliente().Id, ordenDeVenta.AliasCliente(), sesionUsuario.IdCentroDeAtencionSeleccionado, detallesDeNota, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto cuando se crea la nota de debito", true, Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(l => l.Key == Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value).Value != 0);
                //Agregamos el id de la orden de venta como referencia de la orden nota de debito
                ordenDeNotaDeDebito.id_transaccion_referencia = ordenDeVenta.Id;
                //Agregamos la orden de venta en la venta
                notaDeDebito.Transaccion1.Add(ordenDeNotaDeDebito);
                //Creamos la cuota, cuenta por cobrar unica
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, detallesDeNota.Sum(d => d.total), "Unica cuota generada de forma automática", true);
                //Agregamos la cuota en la orden
                ordenDeNotaDeDebito.Cuota.Add(cuota);
                if (modoPago == ModoPago.Contado)
                {
                    //Validamos monto a pagar
                    ValidarImporteAPagar(1, cuota.total, ordenDeNotaDeDebito.importe_total);
                    //Generamos el pago de la nota de credito
                    Transaccion pago = GenerarPagoPorNotaCreditoODebito(ordenDeNotaDeDebito, CodigoPago(cuota), cuota.total, sesionUsuario.Empleado.Id, fechaActual, "", sesionUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado  inicial asignado automaticamente al cobrar una venta");
                    cuota.SetPagoACuenta(ordenDeNotaDeDebito.importe_total);
                    //vincular el pago con la cuota.
                    VincularPagoConLaCuota(pago, cuota, ordenDeNotaDeDebito.importe_total);

                    //Agregamos el pago en la venta
                    notaDeDebito.Transaccion1.Add(pago);
                }
                var result = transaccionRepositorio.CrearTransaccion(notaDeDebito);
                result.information = new Operacion(ordenDeNotaDeDebito).Id;
                result.objeto = new OrdenDeVenta(ordenDeNotaDeDebito);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al realizar la confirmacion de la de la nota de debito", e);
            }
        }


        #endregion

        #region METODOS PARA REPORTE DE VENTAS  ADSOFT Y AFEX 

        public List<Venta_Cliente> ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                //Trae todas las ventas que son con boletas y facturas, en este caso no pregunta por el estado que tiene dicha venta
                var resultado = transaccionRepositorio.ObtenerVentasCliente(Diccionario.TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, fechaDesde, fechaHasta).ToList();

                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas", e);
            }
        }

        public List<Venta_Cliente> ObtenerVentasClienteQueSeanConNotasDeCreditoYDebito(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                //Trae todas las ventas que sean con Notas de Credito y Debito
                var resultado = transaccionRepositorio.ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito, fechaDesde, fechaHasta).ToList();

                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas", e);
            }
        }

        public List<Venta_Cliente> ConsolidarRegistroDeVentas(Periodo periodo, int idEmpleado)
        {
            try
            {
                //Declaracion de variables
                List<Venta_Cliente> registros = new List<Venta_Cliente>();
                DateTime fechaDesde = periodo.FechaDesde, fechaHasta = periodo.FechaHasta;

                //Obtener las ventas del periodo, aqui estan tambien las invalidadas
                List<Venta_Cliente> ventasDelPeriodo = ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(idEmpleado, fechaDesde, fechaHasta)
                                                              .OrderBy(vp => vp.FechaEmision)
                                                              .ThenBy(vp => vp.NumeroSerie)
                                                              .ThenBy(vp => vp.NumeroComprobante)
                                                              .ToList();

                //Filtrar las ventas del periodo donde el tipo de comprobante sea boleta
                List<Venta_Cliente> ventasDelPeriodoConBoleta = ventasDelPeriodo
                                                  .Where(vp => vp.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta
                                                   )
                                                  .OrderBy(vp => vp.FechaEmision)
                                                  .ThenBy(vp => vp.NumeroSerie)
                                                  .ThenBy(vp => vp.NumeroComprobante)
                                                  .ToList();

                //Obtener las series de comprobante  de "boletas de venta" para poder consolidar
                List<int> idsSeries = ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta();

                //Recorrer todas la series del tipo comprobante "boleta"
                foreach (var idSerie in idsSeries)
                {
                    //Filtrar la ventas segun el idSerie
                    List<Venta_Cliente> ventasDeLaSerie = ventasDelPeriodoConBoleta
                                                                          .Where(vi => vi.IdSerie == idSerie)
                                                                          .OrderBy(vp => vp.FechaEmision)
                                                                          .ThenBy(vp => vp.NumeroSerie)
                                                                          .ThenBy(vp => vp.NumeroComprobante)
                                                                          .ToList();
                    //Si exite registros con dicha serie
                    if (ventasDeLaSerie.Count() > 0)
                    {
                        //Este contador nos servira para ir recorriendo la ventas de dicha serie
                        int contVentasDeLaSerie = 0;
                        //Calculamos la cantidad de ventas que hay con dicha serie
                        int cantidadDeVentasDeLaSerie = ventasDeLaSerie.Count();
                        //Inicializamos en false por que todavia no se recorre las "ventas de la serie por fecha"
                        bool terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha = false;
                        //Cantidad donde se quedo, se utiliza cuando se va recorrer las "ventas por serie de la fecha"
                        int cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = 0;

                        do
                        {
                            //Creamos una lista para guardar las ventas agrupadas por cliente varios,clientes identificados e invalidadas
                            List<Venta_Cliente> ventasAgrupadasPorTipoAgrupamiento = new List<Venta_Cliente>();
                            List<Venta_Cliente> itemVentasDeLaSerie = null;
                            //Sacamos el elemento segun se va recorriendo la ventas de la serie
                            itemVentasDeLaSerie = ventasDeLaSerie.Skip(contVentasDeLaSerie).Take(1).ToList();
                            //Sacamos todos los registros que pertenecen a la fecha dada por  "item  de la venta  de la serie"
                            List<Venta_Cliente> ventasDeLaSeriePorFecha = ventasDeLaSerie

                                                      .Where(vsf => vsf.FechaEmision == itemVentasDeLaSerie.First().FechaEmision)
                                                      .ToList();

                            //Cantidad de la ventas de la serie por fecha
                            int cantidadDeVentasDeLaSeriePorFecha = ventasDeLaSeriePorFecha.Count();
                            //Si termino el recorrido de la ventas por fecha se reiniciara el valor
                            int contVentasDeLaSeriePorFecha = terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha ? 0 : cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha;
                            //Agregamos el primer registro de la venta de la serie
                            ventasAgrupadasPorTipoAgrupamiento.Add(itemVentasDeLaSerie.First());

                            List<Venta_Cliente> element = ventasDeLaSeriePorFecha.Where(vs => vs.NumeroComprobante == 353).ToList();



                            bool consolido = false;
                            //Se termino de recorrer "todas las ventas de la serie por fecha" ?
                            while (contVentasDeLaSeriePorFecha < cantidadDeVentasDeLaSeriePorFecha - 1)
                            {
                                //Sacamos un el siguiente item de las "ventas de la serie por fecha"
                                var item = ventasDeLaSeriePorFecha.Skip(contVentasDeLaSeriePorFecha + 1).Take(1).ToList();
                                //Si el elemento siguiente es del mismo tipo de agrupamiento y  el mismo idactornegocio externo(si es el mismo cliente) agrega a ventasAgrupadas
                                if (itemVentasDeLaSerie.First().IdActorNegocioExterno == item.First().IdActorNegocioExterno && itemVentasDeLaSerie.First().TipoAgrupamiento == item.First().TipoAgrupamiento)
                                {
                                    ventasAgrupadasPorTipoAgrupamiento.Add(item.First());
                                    //Se agrego un registor mas
                                    contVentasDeLaSeriePorFecha++;
                                    contVentasDeLaSerie++;
                                }
                                else
                                {
                                    //El contador de ventas de la serie por fecha es igual a la cantidad que hay en las ventas de la serie por fecha?
                                    terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha
                                        = contVentasDeLaSeriePorFecha == cantidadDeVentasDeLaSeriePorFecha - 1;
                                    //Aumenta en 1 para poder preguntar por el siguiente elemento
                                    contVentasDeLaSeriePorFecha++;
                                    contVentasDeLaSerie++;
                                    //Consolidamos hasta donde se consiguio, guardamos en una nueva lista de ventas agrupadas consolidadas
                                    List<Venta_Cliente> ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(ventasAgrupadasPorTipoAgrupamiento);
                                    consolido = true;
                                    //Agregar esa lista consolidas a la lista registros
                                    registros.AddRange(ventasAgrupadasConsolidadas);
                                    //Se termina el ciclo while porque ya no econtro mas elementos siguientes que cumpla la condicion
                                    break;
                                }
                            }
                            //Puede darse el caso de que un dia se realizo solo ventas para cliente varios, identificado o ese dia se hayan invalidado todas las ventas
                            if (contVentasDeLaSeriePorFecha == cantidadDeVentasDeLaSeriePorFecha - 1)
                            {
                                terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha = true;
                                //Aumenta en 1 para poder preguntar por el siguiente elemento
                                contVentasDeLaSerie++;
                                //Actualizamos el contador
                                cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = contVentasDeLaSeriePorFecha;
                                //Consolidamos todas la ventas del dia, guardamos en una nueva lista de ventas agrupadas consolidadas
                                List<Venta_Cliente> ventasAgrupadasConsolidadas;
                                if (consolido)
                                {
                                    var item = ventasDeLaSeriePorFecha.Skip(contVentasDeLaSeriePorFecha).Take(1).ToList();
                                    ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(item);
                                }
                                else
                                {
                                    ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(ventasAgrupadasPorTipoAgrupamiento);
                                }
                                //Agregar esa lista consolidas  a una nueva lista registro de ventas consolidadas
                                registros.AddRange(ventasAgrupadasConsolidadas);
                            }
                            else
                            {
                                cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = contVentasDeLaSeriePorFecha;
                            }

                        } while (contVentasDeLaSerie < cantidadDeVentasDeLaSerie);
                    }
                }
                //Las unicas operaciones que se consolidan son las boletas
                //Obtener las ventas con factura
                List<Venta_Cliente> ventasConFactura = ventasDelPeriodo.Where(v => !v.EsInvalidada && v.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList();
                //Agregamos todas las operaciones con facturas
                registros.AddRange(ventasConFactura);
                //Se agregaran todas las ventas invalidadas con factura
                List<Venta_Cliente> ventasInvalidadasConFactura = ventasDelPeriodo.Where(v => v.EsInvalidada && v.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList();
                registros.AddRange(ventasInvalidadasConFactura);
                //Conseguir las ventas con notas de credito y debito
                List<Venta_Cliente> ventasConNotasDeCreditoYDebito = ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas(idEmpleado, fechaDesde, fechaHasta);
                registros.AddRange(ventasConNotasDeCreditoYDebito);

                registros = registros.OrderBy(r => r.NumeroSerie).ThenBy(r => r.NumeroComprobante).ThenBy(r => r.FechaEmision).ThenBy(r => r.IdTipoComprobante).ToList();

                return registros;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo consolidar registro de ventas", e);
            }
        }

        public List<Venta_Cliente> ConsolidadarVentasCliente(List<Venta_Cliente> ventasAgrupadas)
        {

            List<Venta_Cliente> ventasConsolidadas = ventasAgrupadas
                                         .GroupBy(vcs => new
                                         {

                                             Fecha = new { y = vcs.FechaEmision.Year, m = vcs.FechaEmision.Month, d = vcs.FechaEmision.Day },
                                             vcs.IdSerie,
                                             vcs.NumeroSerie,
                                             vcs.CodigoComprobante,
                                             vcs.IdTipoComprobante,
                                             vcs.CodigoMoneda,
                                         }).Select(vcc => new Venta_Cliente()
                                         {
                                             Anyo = vcc.Key.Fecha.y,
                                             Mes = vcc.Key.Fecha.m,
                                             Dia = vcc.Key.Fecha.d,
                                             IdTipoComprobante = vcc.Key.IdTipoComprobante,
                                             CodigoComprobante = vcc.Key.CodigoComprobante,
                                             IdSerie = vcc.Key.IdSerie,
                                             NumeroSerie = vcc.Key.NumeroSerie,
                                             NumeroComprobante = (int)vcc.Min(vc => vc.NumeroComprobante),
                                             NumeroInicial = (int)vcc.Min(vc => vc.NumeroComprobante),
                                             NumeroFinal = (int)vcc.Max(vc => vc.NumeroComprobante),
                                             Igv = vcc.Sum(vc => (decimal)vc.Igv),
                                             ValorDeVenta = vcc.Sum(vc => vc.ValorDeVenta),
                                             ImporteTotal = vcc.Sum(vc => vc.ImporteTotal),
                                             ValorIcbper = vcc.Sum(vc => vc.ValorIcbper),
                                             PrimerNombre = vcc.FirstOrDefault().PrimerNombre,
                                             IdTipoDocumento = vcc.FirstOrDefault().IdTipoDocumento,
                                             NumeroDocumento = vcc.FirstOrDefault().NumeroDocumento,
                                             CodigoMoneda = vcc.Key.CodigoMoneda,
                                             IdEstadoActual = vcc.FirstOrDefault().IdEstadoActual,
                                             IdEstadoAnteriorAlActual = vcc.FirstOrDefault().IdEstadoAnteriorAlActual

                                         })
                                      .OrderBy(t => t.FechaEmision).ThenBy(t => t.NumeroSerie).ThenBy(t => t.NumeroComprobante).ToList();
            return ventasConsolidadas;
        }

        public List<Venta_Cliente> ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                //Trae las ventas que son con notas de credito y debito en estado confirmado
                return transaccionRepositorio.ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito, fechaDesde, fechaHasta)
                  .OrderBy(t => t.FechaEmision).ThenBy(t => t.NumeroSerie).ThenBy(t => t.Id).ToList();

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ventas invalidadas que sean con notas de credito y debito ", e);
            }
        }

        #endregion

        #region METODOS PARA REPORTE POR NUMERO DE COMPROBANTE CON ICBPER

        public List<Venta_Cliente> ObtenerVentasClientesQueSeanConComprobantesTributablesConfirmadas(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                //Obtiene las ventas confirmadas
                var resultado = transaccionRepositorio.ObtenerVentasClienteConOperacionDeReferenciaSegunElUltimoEstado(Diccionario.TiposDeComprobanteTributables, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas confirmadas", e);
            }
        }

        public List<Venta_Cliente> ObtenerVentasClientesQueSeanConComprobantesTributablesInvalidadas(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                var resultado = transaccionRepositorio.ObtenerVentasClienteSegunElEstadoQueDebeTener(Diccionario.TiposDeComprobanteParaVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();

                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas invalidadas", e);
            }
        }

        public List<Venta_Cliente> ObtenerVentasClientesConfirmadasConIcbper(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultados = ObtenerVentasClientesQueSeanConComprobantesTributablesConfirmadas(idsPuntosDeVentas, fechaDesde, fechaHasta);
                resultados = resultados.Where(r => r.Icbper != 0).ToList();

                return resultados;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas confirmadas con icbper", e);
            }
        }

        public List<Venta_Cliente> ObtenerVentasClientesInvalidadasConIcbper(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                var resultados = ObtenerVentasClientesQueSeanConComprobantesTributablesInvalidadas(idsPuntosDeVentas, fechaDesde, fechaHasta);

                resultados = resultados.Where(r => r.Icbper != 0).ToList();

                return resultados;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas invalidadas con icbper", e);
            }
        }

        #endregion

        #region METODOS PARA REPORTE DE DEUDA Y PAGO PROVEEDOR Y CLIENTE

        public List<Reporte_Deuda> ObtenerDeudasAProveedores(bool todosLosProveedores, int[] idsProveedores)
        {

            try
            {
                var deudas = new List<Reporte_Deuda>();
                if (todosLosProveedores)
                {
                    deudas = transaccionRepositorio.ObtenerDeudas(false, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras).ToList();
                }
                else
                {
                    deudas = transaccionRepositorio.ObtenerDeudas(false, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras, idsProveedores).ToList();
                }
                return deudas.OrderBy(d => d.PrimerNombre).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener deudas a proveedores", e);
            }
        }

        public List<Reporte_Deuda> ObtenerDeudasDeClientes(bool todosLosClientes, int[] idsClientes)
        {
            try
            {
                var deudas = new List<Reporte_Deuda>();
                if (todosLosClientes)
                {
                    deudas = transaccionRepositorio.ObtenerDeudas(true, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas).ToList();
                }
                else
                {
                    deudas = transaccionRepositorio.ObtenerDeudas(true, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas, idsClientes).ToList();
                }
                return deudas.OrderBy(d => d.PrimerNombre).ToList();
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al obtener deudas de clientes", e);
            }
        }

        public List<Reporte_Pago> ObtenerPagosAProveedores(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, bool todosLosProveedores, int[] idsProveedores)
        {
            try
            {
                var ingresoDeDinero = new List<Reporte_Pago>();
                var salidaDeDinero = new List<Reporte_Pago>();
                if (todasLasCajas)
                {
                    if (todosLosProveedores)
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosProveedores).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosProveedores).ToList();
                    }
                    else
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagosExterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosProveedores, idsProveedores).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagosExterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosProveedores, idsProveedores).ToList();
                    }
                }
                else
                {
                    if (todosLosProveedores)
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagosInterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosProveedores, idsCajas).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagosInterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosProveedores, idsCajas).ToList();
                    }
                    else
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosProveedores, idsCajas, idsProveedores).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosProveedores, idsCajas, idsProveedores).ToList();
                    }
                }
                foreach (var item in ingresoDeDinero)
                {
                    item.PagoACuenta = item.PagoACuenta * -1;
                }
                return ingresoDeDinero.Union(salidaDeDinero).OrderBy(d => d.PrimerNombre).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener pagos a proveedores", e);
            }
        }

        public List<Reporte_Pago> ObtenerPagosDeClientes(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, bool todosLosClientes, int[] idsClientes)
        {
            try
            {
                var ingresoDeDinero = new List<Reporte_Pago>();
                var salidaDeDinero = new List<Reporte_Pago>();
                if (todasLasCajas)
                {
                    if (todosLosClientes)
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosClientes).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosClientes).ToList();
                    }
                    else
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagosExterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosClientes, idsClientes).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagosExterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosClientes, idsClientes).ToList();
                    }
                }
                else
                {
                    if (todosLosClientes)
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagosInterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosClientes, idsCajas).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagosInterno(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosClientes, idsCajas).ToList();
                    }
                    else
                    {
                        ingresoDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosClientes, idsCajas, idsClientes).ToList();
                        salidaDeDinero = transaccionRepositorio.ObtenerPagos(fechaDesde, fechaHasta, Diccionario.TiposDeTransaccionDeSalidaDeDineroHaciaLosClientes, idsCajas, idsClientes).ToList();
                    }
                }

                foreach (var item in salidaDeDinero)
                {
                    item.PagoACuenta = item.PagoACuenta * -1;
                }
                return ingresoDeDinero.Union(salidaDeDinero).OrderBy(d => d.PrimerNombre).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener pagos de clientes", e);
            }
        }
        #endregion

        #region FECHAS PARA REPORTES
        public List<string> ObtenerFechaIncioyFinConPrecisionDeMilisegundosParaReporteVentaPuntoDeVenta()
        {

            DateTime fechaDesde = DateTimeUtil.FechaActual(); ;
            fechaDesde = fechaDesde.AddMilliseconds(-fechaDesde.Millisecond);

            DateTime fechaHasta = fechaDesde.Date.AddDays(1).AddMilliseconds(-1);
            fechaHasta.AddMilliseconds(999);

            return new List<string> { fechaDesde.ToString("dd/MM/yyyy HH:mm:ss.fff"), fechaHasta.ToString("dd/MM/yyyy HH:mm:ss.fff") };
        }

        #endregion

        #region CANJE DE COMPROBANTES



        public OperationResult RegistrarCanjeDeComprobante(int idEmpleado, int idCentroAtencion, List<long> idsOrdenes, int idTipoComprobante, int idSerieComprobante, int numeroDeComprobante)
        {
            try
            {
                DateTime fechaEmision = DateTimeUtil.FechaActual();
                //Traer las ordenes de venta las cuales seran canjeadas
                List<OrdenDeVenta> ordenesDeVenta = OrdenDeVenta.Convert_(transaccionRepositorio.ObtenerTransacciones(idsOrdenes.ToArray()).ToList());
                //Verificar que todas las ordens de ventas sean notas de venta y pertenescan al mismo cliente 
                VerificarComprobanteParaCanje(ordenesDeVenta);
                //Generar comprobante
                Comprobante comprobante = GenerarComprobantePropio(idSerieComprobante, numeroDeComprobante);
                //Generamos un nuevo wrapper con los datos de las ordenes a canjear
                Transaccion venta = GenerarOperacionDeCanjeDeComprobante(ordenesDeVenta, TransaccionSettings.Default.IdTipoTransaccionVenta, fechaEmision, "Venta generada al momento de realizar el canje de comprobantes");
                //Generar nueva orden de venta con los datos de las ordenes a canjear
                Transaccion ordenDeVenta = GenerarOperacionDeCanjeDeComprobante(ordenesDeVenta, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, fechaEmision, "Orden de venta generada al momento de realizar el canje de comprobantes");
                //Modificar el codigo de la orden de venta
                ordenDeVenta.codigo = venta.codigo + "_" + ordenDeVenta.codigo;
                //Agregamos el estado de la orden de venta
                Estado_transaccion estadoDeLaOrdenDeVenta = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaEmision, "Estado asignado al confirmar el canje de comprobantes");
                ordenDeVenta.Estado_transaccion.Add(estadoDeLaOrdenDeVenta);
                //Agregar el parametro tipo de venta
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)ordenesDeVenta.First().TipoDeVenta()).ToString()));
                //Agregar el parametro modo de pago
                ordenDeVenta.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)ordenesDeVenta.First().ModoDePago()).ToString()));
                //Generar los detalles de la nueva orden en base a las ordenes de ventas
                List<Detalle_transaccion> detallesDeTransasaccion = GenerarDetalleDeCanjeDeComprobante(ordenesDeVenta, idTipoComprobante);
                //Agregar los detalles a la orden de venta generada
                ordenDeVenta.AgregarDetalles(detallesDeTransasaccion);
                //Resolver las cuotas de la orden de venta
                ResolverCuotaDeOrdenDeVentaDeCanjeDeComprobante(ordenesDeVenta, ordenDeVenta);
                //Agregar la orden de venta a la venta
                venta.Transaccion1.Add(ordenDeVenta);
                //Hacer que los cobros de las ordes a canjear apunten al nuevo wrapper
                ResolverCobroDeCanjeDeComprobante(ordenesDeVenta, venta);
                //Hacer que las salidas de mercaderia de las ordenes a canjear apunten al nuevo wrapper 
                ResolverSalidaDeMercaderiaDeCanjeDeComprobante(ordenesDeVenta, venta, ordenDeVenta);
                //Asignar el comprobante 
                venta.Comprobante = comprobante;
                ordenDeVenta.Comprobante = comprobante;
                //Crear los estados de transaccciones canjeados para las ordenes canjeadas
                List<Estado_transaccion> estadosOrdenesCajeados = new List<Estado_transaccion>();
                foreach (var idOrden in idsOrdenes)
                {
                    var transaccionOrden = transaccionRepositorio.ObtenerTransaccion(idOrden);
                    ordenDeVenta.Transaccion11.Add(transaccionOrden);
                    estadosOrdenesCajeados.Add(new Estado_transaccion(idOrden, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoCanjeado, fechaEmision, "Estado asignado al realizar el canje de comprobantes"));
                }
                List<Transaccion> transaccionesACrear = new List<Transaccion>() { venta, ordenDeVenta };
                //Guardar las nuevas transacciones
                return transaccionRepositorio.CrearTransacionesYEstados(transaccionesACrear, estadosOrdenesCajeados, null);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar, generar y guardar la venta", e);
            }
        }

        public void VerificarComprobanteParaCanje(List<OrdenDeVenta> ordenesDeVenta)
        {
            try
            {
                var idCliente = ordenesDeVenta.First().IdCliente;
                var idComprobante = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
                var idEstadoConfirmado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
                foreach (var ordenDeVenta in ordenesDeVenta)
                {
                    if (idCliente != ordenDeVenta.IdCliente)
                    {
                        throw new LogicaException("No se puede realizar el caje de comprobante, el cliente debe ser el mismo");
                    }
                    if (idComprobante != ordenDeVenta.IdTipoComprobante)
                    {
                        throw new LogicaException("No se puede realizar el caje de comprobante, los comprobantes tienen que ser notas de venta interna");
                    }
                    if (idEstadoConfirmado != ordenDeVenta.IdEstadoActual)
                    {
                        throw new LogicaException("No se puede realizar el caje de comprobante, las ventas tienen que estar confirmadas");
                    }
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar verificar los comprobantes para canje", e);
            }
        }

        public Transaccion GenerarOperacionDeCanjeDeComprobante(List<OrdenDeVenta> ordenesDeVenta, int idTipoTransaccion, DateTime fechaEmision, string observacion)
        {
            try
            {
                //Obtener todos los datos para la generacion de la venta
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = ordenesDeVenta.First().IdMoneda;
                decimal tipoDeCambio = ordenesDeVenta.First().TipoDeCambio;
                int idCliente = ordenesDeVenta.First().IdCliente;
                int idVendedor = ordenesDeVenta.First().IdVendedor;
                int idPuntoDeVenta = ordenesDeVenta.First().IdPuntoDeVenta;
                decimal importeTotal = ordenesDeVenta.Sum(o => o.Total);
                //Obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Obtener el codigo para la venta 
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(n => n.Key == idTipoTransaccion).Value, idTipoTransaccion);
                //Crear la transaccion venta
                Transaccion operacion = new Transaccion(codigo, operacionGenerica.Id, fechaEmision, idTipoTransaccion, idUnidadNegocio, true, fechaEmision, fechaEmision, observacion, fechaEmision, idVendedor, importeTotal, idPuntoDeVenta, idMoneda, tipoDeCambio, null, idCliente);
                return operacion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar verificar los comprobantes para canje", e);
            }
        }

        public List<Detalle_transaccion> GenerarDetalleDeCanjeDeComprobante(List<OrdenDeVenta> ordenesDeVenta, int idTipoComprobante)
        {
            try
            {
                VerificarPlacaDeOrdenesDeVenta(ordenesDeVenta);
                List<DetalleDeOperacion> detallesDeOperaciones = ordenesDeVenta.SelectMany(o => o.DetalleTransaccion()).GroupBy(d => new
                {
                    idConcepto = d.id_concepto_negocio,
                    d.lote
                }).Select(c => new DetalleDeOperacion()
                {
                    Producto = new Concepto_Negocio_Comercial() { Id = c.Key.idConcepto },
                    Cantidad = c.Sum(i => i.cantidad),
                    Importe = c.Sum(i => i.total),
                    PrecioUnitario = c.Sum(i => i.total) / c.Sum(i => i.cantidad),
                    Lote = c.Key.lote,
                    MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPrecioUnitarioCalculado
                }).ToList();
                ResolverDetalles(detallesDeOperaciones, new DatosVentaIntegrada() { Orden = new DatosOrdenVenta() { Comprobante = new ComprobanteDeNegocio_() { Tipo = new ItemGenerico(idTipoComprobante) }, Placa = ordenesDeVenta.First().Detalles().First().Registro } });
                var detallesTransacciones = detallesDeOperaciones.Select(d => d.DetalleTransaccion()).ToList();
                return detallesTransacciones;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar verificar los comprobantes para canje", e);
            }
        }
        public void VerificarPlacaDeOrdenesDeVenta(List<OrdenDeVenta> ordenesDeVenta)
        {
            if (VentasSettings.Default.PermitirRegistroDePlacaEnVenta)
            {
                string placa = ordenesDeVenta.First().Detalles().First().Registro;
                foreach (var ordenDeVenta in ordenesDeVenta)
                {
                    if (placa != ordenDeVenta.Detalles().First().Registro)
                    {
                        throw new LogicaException("Error al intentar realizar el canje, canjear los comprobantes con placas diferentes por separado.");
                    }
                    placa = ordenDeVenta.Detalles().First().Registro;
                }
            }
        }


        public void ResolverCuotaDeOrdenDeVentaDeCanjeDeComprobante(List<OrdenDeVenta> ordenesDeVenta, Transaccion orden)
        {
            try
            {
                foreach (var ordenDeVenta in ordenesDeVenta)
                {
                    foreach (var cuota in ordenDeVenta.Cuotas())
                    {
                        orden.Cuota.Add(cuota);
                    }
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver las cuotas del canje de comprobantes", e);
            }
        }


        public void ResolverCobroDeCanjeDeComprobante(List<OrdenDeVenta> ordenesDeVenta, Transaccion venta)
        {
            try
            {
                var idsTransaccionesPadre = ordenesDeVenta.Select(ov => (long)ov.Transaccion().id_transaccion_padre).ToArray();
                List<Transaccion> cobros = transaccionRepositorio.ObtenerTransaccionesPorTipoYContenganIdTransaccionPadre(idsTransaccionesPadre, TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes).ToList();
                foreach (var cobro in cobros)
                {
                    venta.Transaccion1.Add(cobro);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver los cobros del canje de comprobante", e);
            }
        }


        public void ResolverSalidaDeMercaderiaDeCanjeDeComprobante(List<OrdenDeVenta> ordenesDeVenta, Transaccion venta, Transaccion ordenDeVenta)
        {
            try
            {
                var idsTransaccionesPadre = ordenesDeVenta.Select(ov => (long)ov.Transaccion().id_transaccion_padre).ToArray();
                List<Transaccion> salidasDeMercaderia = transaccionRepositorio.ObtenerTransaccionesPorTipoYContenganIdTransaccionPadre(idsTransaccionesPadre, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta).Where(t => t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();
                ordenDeVenta.Transaccion11 = new List<Transaccion>();
                foreach (var salida in salidasDeMercaderia)
                {
                    venta.Transaccion1.Add(salida);
                    ordenDeVenta.Transaccion11.Add(salida);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar resolver las salidas de mercaderia del canje de comprobantes", e);
            }
        }


        #endregion

        #region REPORTE DE UTILIDAD DE VENTAS POR CONCEPTOS

        public List<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion)
        {
            try
            {
                List<CostoUtilidadPorConcepto> resultado;
                if (reporteGlobal)
                {
                    resultado = transaccionRepositorio.ObtenerReporteDeUtilidadDeVentasPorFamilia(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
                }
                else
                {
                    resultado = transaccionRepositorio.ObtenerReporteDeUtilidadDeVentasPorFamilia(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de utilidad de ventas", e);
            }

        }
        public List<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion, int[] idsConceptosBasicos)
        {
            try
            {
                List<CostoUtilidadPorConcepto> resultado;
                if (reporteGlobal)
                {
                    resultado = transaccionRepositorio.ObtenerReporteDeUtilidadDeVentasPorConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsConceptosBasicos, fechaDesde, fechaHasta).ToList();
                }
                else
                {
                    resultado = transaccionRepositorio.ObtenerReporteDeUtilidadDeVentasPorConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsCentrosAtencion, idsConceptosBasicos, fechaDesde, fechaHasta).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de utilidad de ventas", e);
            }

        }
        #endregion

        #region REPORTE DE PUNTOS
        public List<Reporte_Puntos_Canjeados> ObtenerReporteDePuntosCanjeados(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerReportePuntosCanjeados(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, fechaDesde, fechaHasta, idsCentrosAtencion).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de puntos canjeados", e);
            }

        }
        public List<Reporte_Puntos_Pendientes> ObtenerReporteDePuntosPendientes()
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaDesdeParaObtencionPuntos = fechaActual.AddDays(-VentasSettings.Default.VigenciaEnDiasDePuntos);
                var resultado = transaccionRepositorio.ObtenerReportePuntosPendientes(fechaDesdeParaObtencionPuntos).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de puntos pendientes", e);
            }
        }

        #endregion

        #region Reporte Ventas por caracteristias
        public List<ItemConGrupoOperacionComercial> ObtenerVentasConfirmadasPorCaracteristicaYConcepto(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idCaracteristica)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerItemOperacionPorCaracteristica(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idsCentrosAtencion, idCaracteristica).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas confirmadas por caracteristica y concepto", e);
            }
        }
        /// <summary>
        /// devuelve un registro por cada item vendido. Es decir, si se vendió 1 item con cantidad 2,se debe devolver 2 filas.
        /// </summary>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idPuntoVenta"></param>
        /// <param name="idsCaracteristicas"></param>
        /// <returns></returns>
        public List<ItemDetalladoOperacionComercial> ObtenerVentasDetalladasPorConceptoCaracteristicasModoPago(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta, int[] idsCaracteristicas)
        {
            try
            {
                List<ItemDetalladoOperacionComercial> resultado = transaccionRepositorio.ObtenerItemsDetalladoDeVentaConMedioPago(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idsCaracteristicas, idPuntoVenta).ToList();

                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas confirmadas detalladas", e);
            }

        }

        #endregion

        #region Reporte de ventas por familia, caracteristica y concepto
        public List<VentaConceptoCliente> ObtenerVentasPorFamiliaCaracteristica(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idFamilia, int idCaracteristica, int idValorCaracteristica)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerItemVentaPorFamiliaCaracteristica(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idsCentrosAtencion, idFamilia, idCaracteristica, idValorCaracteristica).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas confirmadas por caracteristica y concepto", e);
            }
        }
        public List<VentaConceptoCliente> ObtenerVentasPorCaracteristica(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idCaracteristica, int idValorCaracteristica)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerItemVentaPorCaracteristica(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idsCentrosAtencion, idCaracteristica, idValorCaracteristica).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas confirmadas por caracteristica y concepto", e);
            }
        }
        public List<VentaConceptoCliente> ObtenerVentasPorConcepto(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idConcepto)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerItemVentaPorConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idsCentrosAtencion, idConcepto).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas confirmadas por caracteristica y concepto", e);
            }
        }


        #endregion

        public List<ResumenDeTransaccionGeneral> ObtenerInvalidacionesDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerResumenDeTransacciones(Diccionario.TiposdeTransaccionInvalidacionesDeVentas, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idPuntoVenta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener invalidaciones de operaciones de venta", e);
            }
        }

        public List<ResumenDeTransaccionGeneral> ObtenerNotasCreditoDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerResumenDeTransacciones(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCredito, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idPuntoVenta, MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat).OrderBy(nd => nd.Fecha).ToList();


                var nombresTiposOperacion = maestroRepositorio.ObtenerDetalles(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);

                resultado.ForEach(r => r.NombreTipoOperacionSunat = nombresTiposOperacion.Single(nto => nto.codigo == r.CodigoTipoOperacionSunat).nombre);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener notas de crédito de operaciones de venta", e);
            }
        }

        public List<ResumenDeTransaccionGeneral> ObtenerNotasDebitoDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerResumenDeTransacciones(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeDebito, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta, idPuntoVenta, MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat).OrderBy(nd => nd.Fecha).ToList();
                var nombresTiposOperacion = maestroRepositorio.ObtenerDetalles(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica).ToList();

                resultado.ForEach(r => r.NombreTipoOperacionSunat = nombresTiposOperacion.Single(nto => nto.codigo == r.CodigoTipoOperacionSunat).nombre);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener notas de débito de operaciones de venta", e);
            }
        }
    }

}

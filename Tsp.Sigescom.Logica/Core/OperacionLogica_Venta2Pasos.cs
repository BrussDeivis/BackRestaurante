using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Logica
{
    public  partial class OperacionLogica
    {
        /// <summary>
        /// Registra una venta con su respectiva orden en estado registrado. Esto solo constituye un compromiso.
        /// LA orden se establece en estado registrado y solo contará con un comprobante temporal. Entiendase que su comprobante definitivo debe asignarse o generarse cuando pase al estado Confirmado.
        /// En venta en 2 pasos, el registro constituye el primer paso.
        /// </summary>
        /// <param name="tipoDeVenta"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <param name="datosVenta"></param>
        /// <returns></returns>
        public OperationResult RegistrarOrdenDeVenta(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada datosVenta)
        {
            var venta = GenerarVentaSoloOrden(tipoDeVenta, sesionDeUsuario, datosVenta);
            OperationResult result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones() { Transacciones_Crear = new List<Transaccion>() { venta.Operacion } });
            return result;
        }

        public OperacionSoloOrden GenerarVentaSoloOrden(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada datosVenta)
        {
            ValidarVenta(datosVenta, ConfiguracionAccion.RegistroOrdenVenta);
            Comprobante comprobante = ObtenerComprobanteTemporal(datosVenta.Orden.Comprobante.Tipo.Id);
            Transaccion venta = GenerarVenta(datosVenta, comprobante, ConfiguracionAccion.ConfirmacionOrdenVenta, sesionDeUsuario);
            Transaccion ordenDeVenta = GenerarOrdenDeVenta(venta, tipoDeVenta, datosVenta, ConfiguracionAccion.RegistroOrdenVenta);
            return new OperacionSoloOrden(venta, ordenDeVenta, datosVenta.TransaccionOrigen);
        }
       
        public Comprobante ObtenerComprobanteTemporal(int idTipoComprobante)
        {
            return new Comprobante() { id = Diccionario.TiposDeComprobanteParaVenta.Single(tc => tc == idTipoComprobante) };
        }

        /// <summary>
        /// Registra ordenes de venta. No genera movimiento de almacen o dinero.
        /// </summary>
        /// <param name="tipoDeVenta"></param>
        /// <param name="sesionUsuario"></param>
        /// <param name="datosVentas"></param>
        /// <returns></returns>
        public OperationResult RegistrarOrdenesDeVenta(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionUsuario, List<DatosVentaIntegrada> datosVentas)
        {
            List<Transaccion> transaccionesVentas = new List<Transaccion>();
            foreach (var datosVenta in datosVentas)
            {
                transaccionesVentas.Add(GenerarVentaSoloOrden(tipoDeVenta, sesionUsuario, datosVenta).Operacion);
            }
            OperationResult result = transaccionRepositorio.RegistrarTransacciones(new RegistroTransacciones() { Transacciones_Crear = transaccionesVentas });;
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDeVenta"></param>
        /// <param name="sesionDeUsuario"></param>
        /// <param name="datosVentasIntegradas"></param>
        /// <returns></returns>
        public OperationResult ConfirmarVentasIntegradas(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, List<DatosVentaIntegrada> datosVentasIntegradas)
        {
            try
            {
                List<OperacionIntegrada> ventasIntegradasAGuardar = new List<OperacionIntegrada>();
                //Obtener maximos codigos para ventas y cuotas yy salidas de mercaderia
                int codigoMaximoVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionVenta).Value, TransaccionSettings.Default.IdTipoTransaccionVenta);
                int codigoMaximoOrdenVenta = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Value, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta);
                int codigoMaximoCuota = transaccionRepositorio.ObtenerMaximoCodigoCuota("C" + DateTimeUtil.FechaActual().Year);
                int codigoMaximoSalidaMercaderia = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(Diccionario.MapeoTipoTransaccionVsCodigoDeOperacion.Single(ttcp => ttcp.Key == TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta).Value, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta);
                int codigoMaximoCobro = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion("C_" + DateTimeUtil.FechaActual().ToString("yyyy"), TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes);
                Codigo codigosMaximos;
                foreach (var datosVentaIntegrada in datosVentasIntegradas)
                {
                    OperacionIntegrada ventaIntegrada = GenerarVentaIntegrada(tipoDeVenta, sesionDeUsuario, datosVentaIntegrada, true);
                    ventaIntegrada.Id = datosVentaIntegrada.Id;
                    //Generar los codigos maximos
                    codigosMaximos = new Codigo(datosVentaIntegrada.FechaRegistro, codigoMaximoVenta, codigoMaximoOrdenVenta, codigoMaximoCuota, codigoMaximoCobro, codigoMaximoSalidaMercaderia);
                    //Modificar los codigos de las operaciones
                    ModificarCodigoDeTransaccionesParaVentaMasivas(ventaIntegrada, codigosMaximos);
                    //Agregar las ventas integradas para guardarse
                    ventasIntegradasAGuardar.Add(ventaIntegrada);
                    codigoMaximoVenta++; codigoMaximoOrdenVenta++; codigoMaximoCuota++; codigoMaximoCobro++; codigoMaximoSalidaMercaderia++;
                }
                ResolverComprobantes(ventasIntegradasAGuardar);
                var result = AfectarInventarioFisicoYGuardarOperaciones(ventasIntegradasAGuardar, sesionDeUsuario);
                Dictionary<long, long> mapeoIdsDatosVentasIntegradasIdsOrden = new Dictionary<long, long>();
                foreach (var venta in ventasIntegradasAGuardar)
                {
                    mapeoIdsDatosVentasIntegradasIdsOrden.Add(venta.Id, venta.OrdenDeOperacion.id);
                }
                result.objeto = mapeoIdsDatosVentasIntegradasIdsOrden;
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar confirmar ventas integradass", e);
            }
        }

        /// <summary>
        /// Devuelve las ventas cuya transaccion origen corresponde al <paramref name="idOperacionOrigen"/>
        /// </summary>
        /// <param name="idOperacionOrigen"></param>
        /// <returns></returns>
        public List<DatosVentaIntegrada> ObtenerOrdenesDeVenta_SegunOperacionOrigen(long idOperacionOrigen)
        {
            try
            {
                List<DatosVentaIntegrada> datosVentas=null;
                var ordenes = transaccionRepositorio.ObtenerTransaccionesSegunOrigen_InclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idOperacionOrigen, TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta);
                ordenes.ForEach(o=>datosVentas.Add(DatosVentaIntegrada.Convert(o)));
                return datosVentas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ventas integradas para operación origen "+ idOperacionOrigen+" .",e);
            }
        }
    }
}

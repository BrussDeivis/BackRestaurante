using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
//using Tsp.Sigescom.Modelo.Properties;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        public List<OrdenDeGasto> ObtenerOrdenesDeGasto(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<Transaccion> gastos = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, fechaDesde, fechaHasta).ToList();
                return OrdenDeGasto.Convert_(gastos.OrderByDescending(g => g.fecha_registro_sistema).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el obtener las ordenes de gastos", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaGasto(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenGasto);
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

        private Transaccion GenerarGasto(int idEmpleado, int idUnidadNegocio, bool esPropio, int idSerieComprobante, int idTipoComprobante, int numeroDeComprobante, string numeroSerieDeComprobante, DateTime fechaEmision, DateTime fechaRegistro, string sufijoCodigo, int idTipoTransaccion, decimal importeTotal, string observacion, int idTercero, int idCentroAtencion)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                decimal tipoDeCambio = 1;
                //Obtener la operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //Generar el codigo de la transaccion
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(sufijoCodigo, idTipoTransaccion);
                //Generar el comprobante de la transaccion
                Comprobante comprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //Crear la transaccion de gasto
                Transaccion transaccion = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaEmision, fechaEmision, observacion, fechaEmision, idEmpleado, importeTotal, idCentroAtencion, idMoneda, tipoDeCambio, null, idTercero)
                {
                    Comprobante = comprobante
                };
                return transaccion;
            }
            catch (Exception e)
            {
                throw new LogicaException("error al generar la transaccion de gasto", e);
            }
        }

        private Transaccion GenerarOrdenGasto(Transaccion gasto, int idEmpleado, int idUnidadNegocio, DateTime fechaEmision, DateTime fechaRegistro, string sufijoCodigo, int idOrdenTransaccion, string observacion, int idProveedor, int idCentroAtencion, List<Detalle_transaccion> detalles, int estadoTransaccion, string observacionEstadoTransacciones)
        {
            Transaccion ordenGasto = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(gasto.codigo + "_O" + sufijoCodigo, idOrdenTransaccion), null, fechaRegistro, idOrdenTransaccion, idUnidadNegocio, true, fechaEmision, fechaEmision, observacion, fechaEmision, idEmpleado, gasto.importe_total, idCentroAtencion, gasto.id_moneda, gasto.tipo_cambio, null, idProveedor)
            {
                //Agregar el comprobante a la orden de gasto
                Comprobante = gasto.Comprobante
            };
            ordenGasto.AgregarDetalles(detalles);
            //Agregar el estado de la orden por defecto
            Estado_transaccion estadoDeLaOrdenDeGasto = new Estado_transaccion(idEmpleado, estadoTransaccion, fechaRegistro, observacionEstadoTransacciones);
            ordenGasto.Estado_transaccion.Add(estadoDeLaOrdenDeGasto);
            //Agregar los parametros 
            ordenGasto.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, ((int)ModoPago.Contado).ToString()));
            ordenGasto.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra, ((int)ModoOperacionEnum.PorMostrador).ToString()));
            return ordenGasto;
        }


        public List<Detalle_transaccion> GenerarDetalleGasto(int idConcepto, string nombreConcepto, string detalle, decimal igv, decimal total)
        {
            var detalles = new Detalle_transaccion[] { new Detalle_transaccion(1, idConcepto, nombreConcepto + " " + detalle, total, total, null, 0, null, null, 0, igv, 0, null, null, null) };
            return detalles.ToList();
        }

        public OperationResult GuardarGasto(int idEmpleado, int idCentroAtencion, int idProveedor, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, int idConcepto, string nombreConcepto, string detalle, string observacion, DateTime fechaEmision, decimal igv, decimal total, bool esContado, bool esCredito, List<Cuota> cuotas)
        {
            try
            {
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Validar la accion a realizar
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, idUnidadNegocio);
                //Generar el detalle de gasto
                var detalles = GenerarDetalleGasto(idConcepto, nombreConcepto, detalle, igv, total);
                //Generar el gasto 
                Transaccion gasto = GenerarGasto(idEmpleado, idUnidadNegocio, esPropio, idSerieComprobante, idTipoComprobante, numeroDeComprobante, numeroSerieDeComprobante, fechaEmision, fechaRegistro, "G", TransaccionSettings.Default.IdTipoTransaccionGasto, total, observacion, idProveedor, idCentroAtencion);
                //Generar la orden de gasto
                Transaccion ordenDeGasto = GenerarOrdenGasto(gasto, idEmpleado, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, fechaEmision, fechaRegistro, "G", TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, observacion, idProveedor, idCentroAtencion, detalles, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado inicial asignado automaticamente al confirmar un gasto");
                //Agregar la orden de gasto a gasto
                gasto.Transaccion1.Add(ordenDeGasto);
                Cuota cuotaAPagar = null;
                if (!esContado && !esCredito)
                {
                    ordenDeGasto.Cuota = GenerarCuotas(idEmpleado, cuotas, fechaRegistro, false, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado por defecto al momento de generar la cuota");
                    cuotaAPagar = ordenDeGasto.Cuota.SingleOrDefault(c => c.cuota_inicial);
                }
                else
                {
                    var cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaRegistro.Year) + "_" + 1, fechaRegistro, fechaRegistro, gasto.importe_total, "Unica cuota generada de forma automática", false);
                    ordenDeGasto.Cuota.Add(cuota);
                    cuotaAPagar = esContado ? cuota : null;
                }
                if (cuotaAPagar != null)
                {
                    cuotaAPagar.SetPagoACuenta(cuotaAPagar.total);
                    Transaccion pago = GenerarMovimientoEconomico(gasto, cuotaAPagar, idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionPagoGasto, fechaRegistro, fechaRegistro, observacion, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "Pago efectivo");
                    gasto.Transaccion1.Add(pago);
                }
                return transaccionRepositorio.CrearTransaccion(gasto);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el gasto", e);
            }
        }

        public OperationResult InvalidarGasto(long idOrdenDeGasto, int idEmpleado, int idCentroAtencion, string observacion)
        {
            try
            {
                OrdenDeGasto ordenDeGasto = new OrdenDeGasto(transaccionRepositorio.ObtenerTransaccion(idOrdenDeGasto));
                //Obtener datos para la invalidacion
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                DateTime fechaRegistro = DateTimeUtil.FechaActual();
                //Validar la accion a realizar
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, idUnidadNegocio);
                //Generar el detalle de invalidacion de gasto
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                foreach (var detalle in ordenDeGasto.Detalles())
                {
                    detalles.Add(new Detalle_transaccion(detalle.Cantidad, detalle.Producto.Id, null, detalle.PrecioUnitario, detalle.Importe, null, 0, null, null, 0, detalle.Igv, detalle.Descuento, detalle.Lote, detalle.Vencimiento, detalle.Registro));
                }
                decimal importePagoTotal = ordenDeGasto.ModoDePago() == ModoPago.Contado ? ordenDeGasto.Total : ordenDeGasto.Transaccion().Cuota.SelectMany(c => c.Pago_cuota).Sum(cp => cp.importe);
                var idProveedor = ordenDeGasto.Transaccion().id_actor_negocio_externo;
                //Obtener la serie del comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionGasto, idCentroAtencion);
                if(serie == null)
                {
                    throw new LogicaException("No hay serie del comprobante nota de invalidacion de gasto");
                }
                //Generar el invalidacion de gasto 
                Transaccion invalidacionGasto = GenerarGasto(idEmpleado, idUnidadNegocio, true, serie.id, serie.id_tipo_comprobante, serie.proximo_numero, serie.numero, fechaRegistro, fechaRegistro, "G", TransaccionSettings.Default.IdTipoTransaccionInvalidacionGasto, importePagoTotal, observacion, idProveedor, idCentroAtencion);
                //Generar la orden de comra
                Transaccion ordenInvalidacionGasto = GenerarOrdenGasto(invalidacionGasto, idEmpleado, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, fechaRegistro, fechaRegistro, "G", TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionGasto, observacion, idProveedor, idCentroAtencion, detalles, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, "Estado asignado automaticamente al confirmar la invalidacion de gasto");
                //Agregar la orden de invalidacion de gasto en invalidacion de gasto
                invalidacionGasto.Transaccion1.Add(ordenInvalidacionGasto);
                //Crear la cuota, cuenta por cobrar unica
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaRegistro.Year) + "_" + 1, fechaRegistro, fechaRegistro, invalidacionGasto.importe_total, invalidacionGasto.importe_total, "Unica cuota generada de forma automática", false);
                ordenInvalidacionGasto.Cuota.Add(cuota);
                //Generar el pago de gasto
                Transaccion pago = GenerarMovimientoEconomico(invalidacionGasto, cuota, idEmpleado, idCentroAtencion, idProveedor, TransaccionSettings.Default.IdTipoTransaccionCobroInvalidacionGasto, fechaRegistro, fechaRegistro, observacion, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto, "Pago efectivo");
                invalidacionGasto.Transaccion1.Add(pago);

                //Crear el estado invalidado para la orden de gasto y las cuotas
                Estado_transaccion estadoDeLaOrdenDeGasto = new Estado_transaccion(idOrdenDeGasto, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaRegistro,
                    "Estado que se agrega al invalidar un gasto");
                
                List<Estado_cuota> estadosDeCuota = new List<Estado_cuota>();
                //foreach (var itemCuota in ordenDeGasto.Cuotas())
                //{
                //    estadosDeCuota.Add(new Estado_cuota(itemCuota.id, idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaRegistro, "Cuota invalidada el momento de invalidar el gasto"));
                //}

                return transaccionRepositorio.CrearTransacionYEstadoTransaccionYEstadosCuota(invalidacionGasto, estadoDeLaOrdenDeGasto, estadosDeCuota);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el gasto", e);
            }
        }

        public OrdenDeGasto ObtenerGasto(long idOrdenGasto)  
        {
            try
            {
                return new OrdenDeGasto(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenGasto));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener la orden de gasto", e);
            }
        }

        public List<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerReporteGastoPorConcepto(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion)
        {
            List<Resumen_Transaccion_Gasto_Por_Concepto> resultado;
            if (reporteGlobal)
            {
                resultado = transaccionRepositorio.ObtenerResumenTransaccionesDeGastosPorConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            }
            else
            {
                resultado = transaccionRepositorio.ObtenerResumenTransaccionesDeGastosPorConcepto(TransaccionSettings.Default.IdTipoTransaccionOrdenGasto, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
            }
            return resultado;
        }

        //public int ObtenerTipoTransaccion(int tipoGasto)
        //{
        //    int tipoTransaccion = 0;
        //    switch (tipoGasto)
        //    {
        //        case 1:
        //            tipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionGastoServiciosTerceros;
        //            break;
        //        case 2:
        //            tipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionOtrosGastosGestion;
        //            break;
        //        case 3:
        //            tipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionGastoFinanciero;
        //            break;
        //        case 4:
        //            tipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionGastoPorTributos;
        //            break;
        //    }
        //    return tipoTransaccion;
        //}
        //public int ObtenerOrdenTransaccion(int tipoGasto)
        //{
        //    int ordenTransaccion = 0;
        //    switch (tipoGasto)
        //    {
        //        case 1:
        //            ordenTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenGastoServiciosTerceros;
        //            break;
        //        case 2:
        //            ordenTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenOtrosGastosGestion;
        //            break;
        //        case 3:
        //            ordenTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenGastoFinanciero;
        //            break;
        //        case 4:
        //            ordenTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenGastoPorTributos;
        //            break;
        //    }
        //    return ordenTransaccion;
        //}
        //public int ObtenerPagoTransaccion(int tipoGasto)
        //{
        //    int pagoTransaccion = 0;
        //    switch (tipoGasto)
        //    {
        //        case 1:
        //            pagoTransaccion = TransaccionSettings.Default.IdTipoTransaccionPagoGastoServiciosTerceros;
        //            break;
        //        case 2:
        //            pagoTransaccion = TransaccionSettings.Default.IdTipoTransaccionPagoOtrosGastosGestion;
        //            break;
        //        case 3:
        //            pagoTransaccion = TransaccionSettings.Default.IdTipoTransaccionPagoGastoFinanciero;
        //            break;
        //        case 4:
        //            pagoTransaccion = TransaccionSettings.Default.IdTipoTransaccionPagoGastoPorTributos;
        //            break;
        //    }
        //    return pagoTransaccion;
        //}
    }
}

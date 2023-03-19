using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica
    {
        #region INGRESOS Y EGRESOS VARIOS

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaIngresoVarios()
        {
            try
            {
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobantes para ingresos varios", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaIngresoVarios(int idCentroDeAtencion)
        {
            try
            {
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroDeAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobantes para ingresos varios", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaEgresoVarios()
        {
            try
            {
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobantes para egresos varios", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaEgresoVarios(int idCentroDeAtencion)
        {
            try
            {
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroDeAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de comprobantes para egresos varios", e);
            }
        }

        public OperationResult GuardarIngresoVarios(int idEmpleado, int idCaja, decimal importe, int idEmisor, int idPagador, int idSerieDeComprobante, string observacion)
        {
            try
            {
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCobroVarios, idUnidadNegocio);
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //tipo de cambio
                decimal tipoDeCambio = 1;
                //obtenemos el codigo
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("CV", TransaccionSettings.Default.IdTipoTransaccionCobroVarios);
                //obtenemos la serie del comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieDeComprobante);
                //crear un comprobante
                Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
                //conseguir numero siguiente
                serie.proximo_numero++;
                //crear un ingreso varios
                Transaccion ingresoVarios = new Transaccion(codigo, operacionGenerica.Id, fechaActual, TransaccionSettings.Default.IdTipoTransaccionCobroVarios,
                    idUnidadNegocio, true, fechaActual, fechaActual, observacion, fechaActual, idEmpleado, importe, idCaja,
                    idMoneda, tipoDeCambio, null, idPagador);
                //agregamos el comprobante al ingreso varios
                ingresoVarios.Comprobante = comprobante;
                //crear una orden de ingreso varios
                Transaccion ordenDeIngresoVarios = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_OCV",
                    TransaccionSettings.Default.IdTipoTransaccionOrdenDeCobroVarios), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCobroVarios,
                    idUnidadNegocio, true, fechaActual, fechaActual, observacion, fechaActual, idEmpleado, importe, idCaja, idMoneda, tipoDeCambio, null, idPagador);
                //agregamos el comprobante a la orden de ingreso varios
                ordenDeIngresoVarios.Comprobante = comprobante;
                //agregamos a ingreso varios a la orden de ingreso varios 
                ordenDeIngresoVarios.Transaccion2 = ingresoVarios;
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenDeIngresoVarios = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual,
                    "Estado inicial asignado automaticamente al confirmar un ingreso varios");
                ordenDeIngresoVarios.Estado_transaccion.Add(estadoDeLaOrdenDeIngresoVarios);
                //Agregamos el emisor del dinero
                Actor_negocio_por_transaccion actorNegocioPorTransaccion = new Actor_negocio_por_transaccion() { id_actor_negocio = idEmisor, id_rol = ActorSettings.Default.IdRolCliente, Transaccion = ordenDeIngresoVarios };
                //agregamos la orden del ingreso varios en la ingreso varios
                ingresoVarios.Transaccion1.Add(ordenDeIngresoVarios);
                //crear cuota : cuenta por cobrar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(true, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, ordenDeIngresoVarios.importe_total, ordenDeIngresoVarios.importe_total, "Unica cuota generada de forma automática al emitir factura", true);
                ordenDeIngresoVarios.Cuota.Add(cuota);
                //pago
                string codigoPago = cuota.codigo + "_P1";
                Transaccion pago = new Transaccion(codigoPago, null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios, idUnidadNegocio,
                    true, fechaActual, fechaActual, "", fechaActual, idEmpleado, importe, idCaja, idMoneda, tipoDeCambio, null, idPagador);
                //vincular el pago con el comprobante
                pago.Comprobante = comprobante;
                //Agregar el estado al pago
                Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al realizar un ingreso varios");
                pago.Estado_transaccion.Add(estadoTransaccion);
                //vincular el pago con la cuota.
                Pago_cuota pagoCuota = new Pago_cuota();
                pagoCuota.Transaccion = pago;
                pagoCuota.Cuota = cuota;
                pagoCuota.importe = importe;
                pago.Pago_cuota.Add(pagoCuota);
                //creado traza 
                Traza_pago traza = new Traza_pago(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, observacion, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                pago.Traza_pago.Add(traza);
                //agregar estado inicial: registrado
                Estado_transaccion estadoDelPago = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al confirmar un ingreso varios");
                pago.Estado_transaccion.Add(estadoDelPago);

                ingresoVarios.Transaccion1.Add(pago);

                return transaccionRepositorio.CrearTransaccion(ingresoVarios);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el intentar guardar el ingreso de dinero", e);
            }
        }

        public OperationResult GuardarEgresoVarios(int idEmpleado, int idCaja, decimal importe, int idEmisor, int idBeneficiario, int idSerieDeComprobante, string observacion)
        {
            try
            {
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                permisos_Logica.ValidarAccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenPagoVarios, idUnidadNegocio);
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //tipo de cambio
                decimal tipoDeCambio = 1;
                //obtenemos el codigo
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("PV", TransaccionSettings.Default.IdTipoTransaccionPagoVarios);
                //obtenemos la serie del comprobante
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieDeComprobante);
                //crear un comprobante
                Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
                //conseguir numero siguiente
                serie.proximo_numero++;
                //crear un egreso varios
                Transaccion egresoVarios = new Transaccion(codigo, operacionGenerica.Id, fechaActual, TransaccionSettings.Default.IdTipoTransaccionPagoVarios,
                    idUnidadNegocio, true, fechaActual, fechaActual, observacion, fechaActual, idEmpleado, importe, idCaja,
                    idMoneda, tipoDeCambio, null, idBeneficiario);
                //agregamos el comprobante al egreso varios
                egresoVarios.Comprobante = comprobante;
                //crear una orden de egreso varios
                Transaccion ordenDeEgresoVarios = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(codigo + "_OPV",
                    TransaccionSettings.Default.IdTipoTransaccionOrdenPagoVarios), null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionOrdenPagoVarios,
                    idUnidadNegocio, true, fechaActual, fechaActual, observacion, fechaActual, idEmpleado, importe, idCaja, idMoneda, tipoDeCambio, null, idBeneficiario);
                //agregamos el comprobante a la orden de egreso varios
                ordenDeEgresoVarios.Comprobante = comprobante;
                //agregamos a egreso varios a la orden de egreso varios 
                ordenDeEgresoVarios.Transaccion2 = egresoVarios;
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenDeIngresoVarios = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual,
                    "Estado inicial asignado automaticamente al confirmar un egreso varios");
                ordenDeEgresoVarios.Estado_transaccion.Add(estadoDeLaOrdenDeIngresoVarios);
                //Agregamos el emisor del dinero
                Actor_negocio_por_transaccion actorNegocioPorTransaccion = new Actor_negocio_por_transaccion() { id_actor_negocio = idEmisor, id_rol = ActorSettings.Default.IdRolCliente, Transaccion = ordenDeEgresoVarios };
                //agregamos la orden del egreso varios en la egreso varios
                egresoVarios.Transaccion1.Add(ordenDeEgresoVarios);
                //crear cuota : cuenta por cobrar unica (se entiende que aun no se contempla el tema de financiamiento en mas de una cuota)
                Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(false, fechaActual.Year) + "_" + 1, fechaActual, fechaActual, ordenDeEgresoVarios.importe_total, ordenDeEgresoVarios.importe_total,
                    "Unica cuota generada de forma automática al emitir factura", false);
                ordenDeEgresoVarios.Cuota.Add(cuota);
                //pago
                string codigoPago = cuota.codigo + "_P1";
                Transaccion pago = new Transaccion(codigoPago, null, fechaActual, TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios, idUnidadNegocio,
                    true, fechaActual, fechaActual, "", fechaActual, idEmpleado, importe, idCaja, idMoneda, tipoDeCambio, null, idBeneficiario);
                //vincular el pago con el comprobante
                pago.Comprobante = comprobante;
                //Agregar el estado al pago
                Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al realizar un egreso varios");
                pago.Estado_transaccion.Add(estadoTransaccion);
                //vincular el pago con la cuota.
                Pago_cuota pagoCuota = new Pago_cuota();
                pagoCuota.Transaccion = pago;
                pagoCuota.Cuota = cuota;
                pagoCuota.importe = importe;
                pago.Pago_cuota.Add(pagoCuota);
                //creado traza 
                Traza_pago traza = new Traza_pago(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, observacion, MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
                pago.Traza_pago.Add(traza);
                //agregar estado inicial: registrado
                Estado_transaccion estadoDelPago = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al confirmar un egreso varios");
                pago.Estado_transaccion.Add(estadoDelPago);

                egresoVarios.Transaccion1.Add(pago);

                return transaccionRepositorio.CrearTransaccion(egresoVarios);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el intentar guardar el egreso de dinero", e);
            }
        }

        #endregion

        #region CUENTAS POR COBAR Y PAGAR

        private string ObtenerSiguienteCodigoParaNuevaCuota(bool porCobrar, int year)
        {
            try
            {
                string empiezaEn = (porCobrar ? "C" : "P") + year.ToString();
                int maximo = transaccionRepositorio.ObtenerMaximoCodigoCuota(empiezaEn);
                return empiezaEn + (maximo + 1).ToString();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener el siguiente codigo para nueva cuota", e);
            }
        }

        public List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrar()
        {
            try
            {
                bool porCobrar = true;
                return transaccionRepositorio.ObtenerCuentasPorCobrarOPagar(porCobrar).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las cuentas por cobrar", e);
            }
        }

        public List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorPagar()
        {
            try
            {
                bool porCobrar = false;
                return transaccionRepositorio.ObtenerCuentasPorCobrarOPagar(porCobrar).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las cuentas por pagar", e);
            }
        }

        public List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarPorGrupos(bool todosLosGrupos, int?[] idsGrupos)
        {
            try
            {
                bool porCobrar = true;
                return todosLosGrupos ? transaccionRepositorio.ObtenerCuentasPorCobrarOPagar(porCobrar).ToList() : transaccionRepositorio.ObtenerCuentasPorCobrarOPagarPorGrupos(porCobrar, idsGrupos).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las cuentas por cobrar", e);
            }
        }

        public List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorPagarPorGrupos(bool todosLosGrupos, int?[] idsGrupos)
        {
            try
            {
                bool porCobrar = false;
                return todosLosGrupos ? transaccionRepositorio.ObtenerCuentasPorCobrarOPagar(porCobrar).ToList() : transaccionRepositorio.ObtenerCuentasPorCobrarOPagarPorGrupos(porCobrar, idsGrupos).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las cuentas por pagar", e);
            }
        }
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCobrar(int idTipoTransaccionOrden, int idCentroDeAtencion)
        {
            try
            {
                int idTipoTransaccionPago = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == idTipoTransaccionOrden).Value;
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(idTipoTransaccionPago)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroDeAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los tipos de comprobante para cobrar", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaPagar(int idTipoTransaccionOrden, int idCentroDeAtencion)
        {
            try
            {
                int idTipoTransaccionPago = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == idTipoTransaccionOrden).Value;
                var resultado = (await transaccionRepositorio.ObtenerTipoComprobantePorTipoDeTransaccion(idTipoTransaccionPago)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroDeAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los tipos de comprobante para pagar", e);
            }
        }

        public CuentaPorCobrarPagar ObtenerCuentaIncluidoOperacion(long idCuota)
        {
            try
            {
                return (new CuentaPorCobrarPagar(transaccionRepositorio.ObtenerCuotaIncluidoOperacion(idCuota)));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener la cuenta incluido la operacion", e);
            }
        }

        #endregion

        #region PAGOS Y COBROS

        public string CodigoPago(Cuota cuota)
        {
            string codigoPago = cuota.Pago_cuota == null ? cuota.codigo + "_P" + 1.ToString() : cuota.codigo + "_P" + (cuota.Pago_cuota.Count() + 1).ToString();
            return codigoPago;
        }

        public string CodigoPago(DateTime fecha, int idTipoTransaccionOrden)
        {
            return codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion("P_" + fecha.ToString("yyyy"), Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == idTipoTransaccionOrden).Value);
        }

        public Cuota GenerarCuotaUnica(int idComprador, decimal importeTotal, DateTime fechaRegistro, bool porCobrar, int estadoTransaccion, string observacionEstadoTransaccion)
        {
            //Crear la cuota, cuenta por cobrar unica
            Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(porCobrar, fechaRegistro.Year) + "_" + 1, fechaRegistro, fechaRegistro, importeTotal, "Unica cuota generada de forma automática", porCobrar);
            return cuota;
        }

        public Cuota GenerarCuotaUnicaConPagoACuenta(int idComprador, decimal importeTotal, DateTime fechaRegistro, bool porCobrar)
        {
            //Crear la cuota, cuenta por cobrar unica
            Cuota cuota = new Cuota(ObtenerSiguienteCodigoParaNuevaCuota(porCobrar, fechaRegistro.Year) + "_" + 1, fechaRegistro, fechaRegistro, importeTotal, importeTotal, "Unica cuota generada de forma automática", porCobrar);
            return cuota;
        }

        public List<Cuota> GenerarCuotas(int idComprador, List<Cuota> cuotas, DateTime fechaRegistro, bool porCobrar, int estadoTransaccion, string observacionEstadoTransaccion)
        {
            List<Cuota> cuotasGeneradas = new List<Cuota>();
            string codigoCuota = ObtenerSiguienteCodigoParaNuevaCuota(porCobrar, fechaRegistro.Year);
            int cont = 1;
            foreach (var item in cuotas)
            {
                Cuota cuota = new Cuota(codigoCuota + "_" + cont, fechaRegistro, item.fecha_vencimiento, item.capital, item.interes, item.total, "Cuota generada numero " + cont, porCobrar, item.cuota_inicial);
                cuotasGeneradas.Add(cuota);
                cont++;
            }
            return cuotasGeneradas;
        }
        /// <summary>
        /// Validar el importe total a pagar con el total de la deuda a cobrar
        /// </summary>
        /// <param name="cantidadDeCuotas"></param>
        /// <param name="totalAPagar"></param>
        /// <param name="deudaTotal"></param>
        internal void ValidarImporteAPagar(int cantidadDeCuotas, decimal totalAPagar, decimal deudaTotal)
        {
            if (deudaTotal < totalAPagar)
            {
                throw new LogicaException(String.Format("El importe a pagar debe ser menor o igual a la deuda total. Se intenta pagar S/ {0} y el valor de la deuda es de S/ {1}", totalAPagar, deudaTotal));
            }
            if (cantidadDeCuotas <= 0)
            {
                throw new LogicaException("El cliente no tiene deudas por lo que no se puede procesar ningún pago");
            }
            if (totalAPagar <= 0)
            {
                throw new LogicaException("El Importe a pagar debe ser mayor a 0");
            }
        }

        //Vincular un pago con las cuotas desde las mas antiguas a la mas reciente segun el importe total a pagar
        public void ResolverPagosCuotas(Transaccion pago, List<Cuota> cuotas, decimal importeTotalAPagar)
        {
            //Vincular pago con cuotas
            decimal importeParcial;
            decimal importeDisponible = importeTotalAPagar;
            int numeroCuota = 0;
            while (importeDisponible > 0)
            {
                Cuota cuotaAPagar = cuotas.ElementAt(numeroCuota++);
                var deuda = cuotaAPagar.saldo;
                importeParcial = importeDisponible >= deuda ? deuda : importeDisponible;
                //Vincular el pago con la cuota 
                VincularPagoConLaCuota(pago, cuotaAPagar, importeParcial);
                //Actualizar el pago a cuenta 
                cuotaAPagar.pago_a_cuenta += importeParcial;
                cuotaAPagar.saldo = cuotaAPagar.total - cuotaAPagar.pago_a_cuenta;
                importeDisponible -= importeParcial;
            }
        }

        /// <summary>
        /// Vincular el la transaccion de pago con la cuota 
        /// </summary>
        /// <param name="pago"></param>
        /// <param name="cuota"></param>
        /// <param name="importe"></param>
        public void VincularPagoConLaCuota(Transaccion pago, Cuota cuota, decimal importe)
        {
            Pago_cuota pagoCuota = new Pago_cuota
            {
                Transaccion = pago,
                Cuota = cuota,
                importe = importe
            };
            pago.Pago_cuota.Add(pagoCuota);
        }


        public OperationResult GuardarMovimientoEconomico(MovimientoEconomico_ movimiento, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                //Obtener datos para realizar el pago 
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idTipoTransaccionPago = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == movimiento.IdTipoTransaccion).Value;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1;//_repositorioTransaccion.obtenerTipoDeCambio(fechaDePago).valorVenta;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                //Obtener serie de comprobanta
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(movimiento.Comprobante.Serie.Id);
                Comprobante comprobante = GenerarComprobantePropioAutonumerable(serie.id);
                //Obtener el codigo de pago
                string codigoPago = CodigoPago(fechaActual, movimiento.IdTipoTransaccion);
                Transaccion movimientoEconomico = new Transaccion(codigoPago, null, fechaActual, idTipoTransaccionPago, idUnidadNegocio, true, fechaActual, fechaActual, movimiento.Observacion, fechaActual, sesionDeUsuario.Empleado.Id, movimiento.Total, sesionDeUsuario.IdCentroDeAtencionSeleccionado, idMoneda, tipoDeCambio, null, movimiento.IdActorComercial)
                {
                    //Agregar el comprobante al pago
                    Comprobante = comprobante
                };
                //Agregar el estado al movimiento economico
                Estado_transaccion estadoTransaccion = new Estado_transaccion(sesionDeUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al realizar un movimiento economico");
                movimientoEconomico.Estado_transaccion.Add(estadoTransaccion);
                if (movimiento.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta)
                {
                    movimientoEconomico.id_actor_negocio_interno1 = movimiento.Traza.CuentaBancaria.Id;
                }
                //Generar la traza de pago y Agregar al pago
                Traza_pago traza = new Traza_pago(movimiento.Traza.MedioDePago.Id, movimiento.Traza.InformacionDePago, movimiento.Traza.EntidadFinanciera.Id);
                movimientoEconomico.Traza_pago.Add(traza);
                //Por cada detalle de movimiento obtener cuota y generar el pago cuota correspondiente
                for (int i = 0; i < movimiento.PagoCuota.Count(); i++)
                {
                    Cuota cuota = transaccionRepositorio.ObtenerCuota(movimiento.PagoCuota[i].IdCuota);
                    cuota.Comprobante = comprobante;
                    cuota.pago_a_cuenta += movimiento.PagoCuota[i].Importe;
                    cuota.saldo = cuota.total - cuota.pago_a_cuenta - cuota.revocado;
                    Pago_cuota pagoCuota = new Pago_cuota()
                    {
                        id_cuota = cuota.id,
                        importe = movimiento.PagoCuota[i].Importe
                    };
                    //Agregar el pago cuota al pago
                    movimientoEconomico.Pago_cuota.Add(pagoCuota);
                }
                return transaccionRepositorio.CrearTransaccion(movimientoEconomico);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar cobrar / pagar la cuota", e);
            }
        }

        //Generar una transaccion de cobro de una venta
        private Transaccion GenerarCobroDeVenta(int idEmpleado, int idCentroAtencion, int idCliente, string codigoDeCobro, Comprobante comprobante, decimal tipoDeCambio, int idUnidadDeNegocio, int idMoneda, decimal totalAPagar, DateTime fechaDeCobro, DateTime fechaRegistro, string observacion, int idMedioDePago, int idEntidadBancaria, int? idTipoOperador, string informacionDePago)
        {
            //Generar el cobro
            Transaccion cobro = new Transaccion(codigoDeCobro, null, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, idUnidadDeNegocio, true, fechaDeCobro, fechaDeCobro, observacion, fechaDeCobro, idEmpleado, totalAPagar, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente)
            {
                //Agregar el comprobante al cobro
                Comprobante = comprobante
            };
            //Agregar la traza al cobro
            idEntidadBancaria = (idMedioDePago == MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo) ? MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto : idEntidadBancaria;
            cobro.Traza_pago.Add(new Traza_pago(idMedioDePago, informacionDePago, idEntidadBancaria, idTipoOperador == 0 ? null : idTipoOperador));
            //Agregar el estado al cobro
            Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDeCobro, "Estado inicial asignado automaticamente al realizar un cobro de venta");
            cobro.Estado_transaccion.Add(estadoTransaccion);
            return cobro;
        }

        //Generar una transaccion de cobro de una venta teniendo en cuanta que aqui se guarda el tipo de venta (modo de operacion) como un parametro de transaccion
        private Transaccion GenerarCobroDeVenta(ModoOperacionEnum modoDeOperacion, int idEmpleado, int idCentroAtencion, int idCliente, string codigoDeCobro, Comprobante comprobante, decimal tipoDeCambio, int idUnidadDeNegocio, int idMoneda, decimal totalAPagar, DateTime fechaDeCobro, DateTime fechaRegistro, string observacion, int idMedioDePago, int idEntidadBancaria, string informacionDePago)
        {
            //Generar el cobro
            Transaccion cobro = new Transaccion(codigoDeCobro, null, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, idUnidadDeNegocio, true, fechaDeCobro, fechaDeCobro, observacion, fechaDeCobro, idEmpleado, totalAPagar, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente)
            {
                //Agregar el comprobante al cobro
                Comprobante = comprobante
            };
            //Agregar el parametro modo de operacion
            cobro.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta, ((int)modoDeOperacion).ToString()));
            //Agregar la traza al cobro
            idEntidadBancaria = (idMedioDePago == MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo) ? MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto : idEntidadBancaria;
            cobro.Traza_pago.Add(new Traza_pago(idMedioDePago, informacionDePago, idEntidadBancaria));
            //Agregar el estado al cobro
            Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDeCobro, "Estado inicial asignado automaticamente al realizar un cobro de venta");
            cobro.Estado_transaccion.Add(estadoTransaccion);
            return cobro;
        }

        //Generar una transaccion movimiento economico (cobro o pago) y crear el pago cuota.
        private Transaccion GenerarMovimientoEconomico(Transaccion operacion, Cuota cuota, int idEmpleado, int idCentroAtencion, int idTercero, int idTipoTransaccion, DateTime fechaOperacion, DateTime fechaRegistro, string observacion, int idMedioDePago, int idEntidadBancaria, string informacionDeMedioPago)
        {
            //Generar el codigo de movimiento economico
            string codigoPago = CodigoPago(cuota);
            //Generar el movimiento economico
            Transaccion movimientoEconomico = new Transaccion(codigoPago, null, fechaRegistro, idTipoTransaccion, operacion.id_unidad_negocio, true, fechaOperacion, fechaOperacion, observacion, fechaOperacion, idEmpleado, cuota.total, idCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, idTercero)
            {
                //Agregar el comprobante al movimiento economico
                Comprobante = operacion.Comprobante
            };
            //Agregar el estado al movimiento economico
            Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaOperacion, "Estado inicial asignado automaticamente al realizar un movimiento economico");
            movimientoEconomico.Estado_transaccion.Add(estadoTransaccion);
            //Agregar el pago cuota al movimiento economico
            Pago_cuota pagoCuota = new Pago_cuota
            {
                Cuota = cuota,
                importe = cuota.total
            };
            movimientoEconomico.Pago_cuota.Add(pagoCuota);
            //Agregar la traza al pago
            Traza_pago trazaDeMovimientoEconomico = new Traza_pago(idMedioDePago, informacionDeMedioPago, idEntidadBancaria);
            movimientoEconomico.Traza_pago.Add(trazaDeMovimientoEconomico);
            return movimientoEconomico;
        }

        private Transaccion GenerarMovimientoEconomicoPagoACuentaCuota(Transaccion operacion, Cuota cuota, int idEmpleado, int idCentroAtencion, int idTercero, int idTipoTransaccion, DateTime fechaOperacion, DateTime fechaRegistro, string observacion, int idMedioDePago, int idEntidadBancaria, string informacionDeMedioPago)
        {
            //Generar el codigo de movimiento economico
            string codigoPago = CodigoPago(cuota);
            //Generar el movimiento economico
            Transaccion movimientoEconomico = new Transaccion(codigoPago, null, fechaRegistro, idTipoTransaccion, operacion.id_unidad_negocio, true, fechaOperacion, fechaOperacion, observacion, fechaOperacion, idEmpleado, cuota.pago_a_cuenta, idCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, idTercero)
            {
                //Agregar el comprobante al movimiento economico
                Comprobante = operacion.Comprobante
            };
            //Agregar el estado al movimiento economico
            Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaOperacion, "Estado inicial asignado automaticamente al realizar un movimiento economico");
            movimientoEconomico.Estado_transaccion.Add(estadoTransaccion);
            //Agregar el pago cuota al movimiento economico
            Pago_cuota pagoCuota = new Pago_cuota
            {
                Cuota = cuota,
                importe = cuota.pago_a_cuenta
            };
            movimientoEconomico.Pago_cuota.Add(pagoCuota);
            //Agregar la traza al pago
            Traza_pago trazaDeMovimientoEconomico = new Traza_pago(idMedioDePago, informacionDeMedioPago, idEntidadBancaria);
            movimientoEconomico.Traza_pago.Add(trazaDeMovimientoEconomico);
            return movimientoEconomico;
        }

        public List<Cobro_Pago> ObtenerCobros(DateTime desde, DateTime hasta)
        {
            try
            {
                bool esCobro = true;
                return transaccionRepositorio.ObtenerCobrosOPagos(esCobro, desde, hasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cobros", e);
            }
        }

        public List<Cobro_Pago> ObtenerPagos(DateTime desde, DateTime hasta)
        {
            try
            {
                bool esCobro = false;
                return transaccionRepositorio.ObtenerCobrosOPagos(esCobro, desde, hasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener pagos", e);
            }
        }

        public MovimientoEconomico ObtenerMovimientoEconomico(long idOperacion)
        {
            try
            {
                return new MovimientoEconomico(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idOperacion));
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el movimiento economico", e);
            }
        }

        public OperationResult InvalidarMovimientoEconomico(long idOperacion, string observacion, UserProfileSessionData profileSessionData)
        {
            try
            {
                OperationResult result = new OperationResult();
                List<Cuota> cuotasActualizar = new List<Cuota>();
                var movimientoEconomico = new MovimientoEconomico(transaccionRepositorio.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idOperacion));
                foreach (var detalle in movimientoEconomico.Detalles())
                {
                    var cuota = transaccionRepositorio.ObtenerCuota(detalle.Idcuota);
                    cuota.pago_a_cuenta -= detalle.Importe;
                    cuota.saldo += detalle.Importe;
                    cuotasActualizar.Add(cuota);
                }
                Estado_transaccion estadoMovimientoEconomicoInvalidado = new Estado_transaccion(idOperacion, profileSessionData.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, DateTimeUtil.FechaActual(), observacion);
                transaccionRepositorio.CrearEstadoTransaccionActualizarCuotas(estadoMovimientoEconomicoInvalidado, cuotasActualizar);
                return result;
            }
            catch (Exception ex)
            {
                throw new LogicaException("Error al invalidar el pedido", ex);
            }
        }
        #endregion

        #region CUENTAS BANCARIAS

        public List<CuentaBancaria> ObtenerCuentasBancarias()
        {
            try
            {
                var cuentasBancarias = actorRepositorio.ObtenerCuentasBancarias().ToList();
                return cuentasBancarias;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las cuentas bancarias", e);
            }
        }

        public List<CuentaBancaria> ObtenerCuentasBancariasPorEntidadFinanciera(int idEntidadFinanciera)
        {
            try
            {
                var cuentasBancarias = actorRepositorio.ObtenerCuentasBancariasPorEntidadFinanciera(idEntidadFinanciera).ToList();
                return cuentasBancarias;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las cuentas bancarias", e);
            }
        }

        public List<ItemGenerico> ObtenerCuentasBancariasConEntidadFinancieraConMoneda()
        {
            try
            {
                var cuentasBancarias = actorRepositorio.ObtenerCuentasBancariasConEntidadFinancieraConMoneda().ToList();
                return cuentasBancarias;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las cuentas bancarias", e);
            }
        }


        public Actor_negocio GenerarActorDeNegocioDeCuentaBancaria(CuentaBancaria cuentaBancaria)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                //Crear el actor de negocio de cuenta bancaria
                Actor_negocio cuentaBancariaActorNegocio = new Actor_negocio(ActorSettings.Default.IdRolCuentaBancaria, fechaActual, fechaFin, "", cuentaBancaria.EstaActivo, false, "");
                //Crear al actor
                Actor actor = new Actor(cuentaBancaria.TipoCuenta.Id, fechaActual, cuentaBancaria.NumeroCuenta, cuentaBancaria.NumeroCCI, cuentaBancaria.Titular, "", ActorSettings.Default.IdTipoActorCuentaBancaria, ActorSettings.Default.IdFotoActorPorDefecto, ActorSettings.Default.IdClaseActorNoEspecificadoDeTipoActorCuentaBancaria, ActorSettings.Default.IdEstadoLegalNoEspecificadoDeTipoActorCuentaBancaria, "", "", "")
                {
                    id_detalle_multiproposito = cuentaBancaria.EntidadFinanciera.Id,
                    id_detalle_multiproposito1 = cuentaBancaria.Moneda.Id,
                    informacion_multiproposito = cuentaBancaria.Ubigeo
                };
                //Asignar en actor a la cuenta bancaria
                cuentaBancariaActorNegocio.Actor = actor;
                return cuentaBancariaActorNegocio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar un actor de negocio a partir de la cuenta bancaria", e);
            }
        }
        public bool ExisteNumeroCuentaYEntidadFinanciera(int idEntidadFinanciera, string numeroCuenta, string numeroCCI)
        {
            try
            {
                bool existe = actorRepositorio.ExisteNumeroDocumentoIdDetalleMultiPropositoActor(idEntidadFinanciera, numeroCuenta, numeroCCI);
                return existe;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe numero de cuenta", e);
            }
        }
        public bool ExisteNumeroCuentaYEntidadFinanciera(int idActor, int idEntidadFinanciera, string numeroCuenta, string numeroCCI)
        {
            try
            {
                bool existe = actorRepositorio.ExisteNumeroDocumentoIdDetalleMultiPropositoActor(idActor, idEntidadFinanciera, numeroCuenta, numeroCCI);
                return existe;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe numero de cuenta", e);
            }
        }
        public OperationResult CrearCuentaBancaria(CuentaBancaria cuentaBancaria)
        {
            try
            {
                if (ExisteNumeroCuentaYEntidadFinanciera(cuentaBancaria.EntidadFinanciera.Id, cuentaBancaria.NumeroCuenta, cuentaBancaria.NumeroCCI))
                {
                    throw new ControllerException("Ya existe el numero de cuenta registrado en la misma entidad financiera.");
                }
                Actor_negocio cuentaBancariaActorNegocio = GenerarActorDeNegocioDeCuentaBancaria(cuentaBancaria);
                var resultado = actor_Datos.CrearActorNegocio(cuentaBancariaActorNegocio);
                //conseguir datos luego de guardar
                cuentaBancaria.Id = cuentaBancariaActorNegocio.id;
                cuentaBancaria.IdActor = cuentaBancariaActorNegocio.id_actor;
                resultado.information = cuentaBancaria;
                return resultado;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar la cuenta bancaria", e);
            }
        }
        public OperationResult ActualizarCuentaBancaria(CuentaBancaria cuentaBancaria)
        {
            try
            {
                if (ExisteNumeroCuentaYEntidadFinanciera(cuentaBancaria.IdActor, cuentaBancaria.EntidadFinanciera.Id, cuentaBancaria.NumeroCuenta, cuentaBancaria.NumeroCCI))
                {
                    throw new ControllerException("Ya existe el numero de cuenta registrado en la misma entidad financiera.");
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                //Crear el actor de negocio de cuenta bancaria
                Actor_negocio cuentaBancariaActorNegocio = new Actor_negocio(cuentaBancaria.Id, cuentaBancaria.IdActor, ActorSettings.Default.IdRolCuentaBancaria, fechaActual, fechaFin, "", cuentaBancaria.EstaActivo, false, "");
                //Crear al actor
                Actor actor = new Actor(cuentaBancaria.IdActor, cuentaBancaria.TipoCuenta.Id, fechaActual, cuentaBancaria.NumeroCuenta, cuentaBancaria.NumeroCCI, cuentaBancaria.Titular, "", ActorSettings.Default.IdTipoActorCuentaBancaria, ActorSettings.Default.IdFotoActorPorDefecto, ActorSettings.Default.IdClaseActorNoEspecificadoDeTipoActorCuentaBancaria, ActorSettings.Default.IdEstadoLegalNoEspecificadoDeTipoActorCuentaBancaria, "", "", "")
                {
                    id_detalle_multiproposito = cuentaBancaria.EntidadFinanciera.Id,
                    id_detalle_multiproposito1 = cuentaBancaria.Moneda.Id,
                    informacion_multiproposito = cuentaBancaria.Ubigeo
                };
                //Asignar en actor a la cuenta bancaria
                cuentaBancariaActorNegocio.Actor = actor;
                var resultado = actor_Datos.ActualizarActorNegocio(cuentaBancariaActorNegocio);
                resultado.information = cuentaBancaria;
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar la cuenta bancaria", e);
            }
        }

        #endregion
        #region INICIALIZAR CAJA
        public List<CajaInicializar> ObtenerInicializarCaja()
        {
            try
            {
                var cajas = centroDeAtencionDatos.ObtenerCentrosDeAtencionSegunRolHijo(ActorSettings.Default.IdRolCaja, true).ToList();

                return CajaInicializar.Convert(cajas);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cajas a inicializar", e);
            }
        }
        public OperationResult GuardarInicializarCaja(int idEmpleado, List<CajaInicializar> cajasAInicializar)
        {
            try
            {
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                var fechaActual = DateTimeUtil.FechaActual();
                List<Transaccion> arqueosDeCaja = new List<Transaccion>();
                string prefijoCodigo = "AC";
                int maximo = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(prefijoCodigo, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja);
                foreach (var cajaInicializar in cajasAInicializar)
                {
                    string codigo = prefijoCodigo + (maximo + 1).ToString();
                    Transaccion arqueoCaja = CrearArqueoCaja(operacionGenerica.Id, operacionGenerica.IdComprobante, idEmpleado, fechaActual, codigo, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, "", cajaInicializar.Id, cajaInicializar.Monto);
                    arqueosDeCaja.Add(arqueoCaja);
                    maximo++;
                }
                return transaccionRepositorio.CrearTransacciones(arqueosDeCaja);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar inicializar cajas", e);
            }
        }
        #endregion

        #region ARQUEO DE CAJA
        public Transaccion CrearArqueoCaja(long idOperacion, long idComprobante, int idEmpleado, DateTime fechaRegistro, string codigo, int idTipoTransaccion, int accionARealizar, int idTipoTransaccionValidar, string observacion, int idCentroDeAtencion, decimal total)
        {
            try
            {

                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1;
                //Validamos la accion a realizar
                permisos_Logica.ValidarAccion(idEmpleado, accionARealizar, idTipoTransaccionValidar, idUnidadNegocio);
                //Crear transaccion arqueo de caja
                Transaccion arqueoCaja = new Transaccion(codigo, idOperacion, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, total, idCentroDeAtencion, idMoneda, tipoDeCambio, null, idCentroDeAtencion)
                {
                    id_comprobante = idComprobante
                };
                Estado_transaccion estado = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaRegistro, "Estado confirmado al crear la transaccion arqueo de caja");
                arqueoCaja.Estado_transaccion.Add(estado);
                return arqueoCaja;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al crear el arqueo de caja", e);
            }
        }

        public OperationResult GenerarArqueoDeCaja(int idEmpleado)
        {
            try
            {
                List<Transaccion> arqueosDeCaja = new List<Transaccion>();
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Obtener las cajs vigentes para realizar el arque de caja de todas
                var cajasVigentes = actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEntidadInterna, ActorSettings.Default.IdRolCaja, true).ToList();
                string prefijoCodigo = "AC";
                int maximo = codigosOperacion_Logica.ObtenerMaximoCodigoParaTransaccion(prefijoCodigo, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja);
                foreach (var cajaVigente in cajasVigentes)
                {
                    string codigo = prefijoCodigo + (maximo + 1).ToString();
                    //Obtener su ultimo arque de caja que tenga la caja vigente 
                    Transaccion ultimoArqueoDeCaja = transaccionRepositorio.ObtenerUltimaTransaccionAntesDe(cajaVigente.id, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, fechaActual);

                    var hayArqueoDeCajaAnterior = ultimoArqueoDeCaja != null;
                    DateTime fechaPrimeraTransaccion = (DateTime)transaccionRepositorio.ObtenerFechaPrimeraTransaccion();
                    var fechaDesdeParaSaldo = hayArqueoDeCajaAnterior ? ultimoArqueoDeCaja.fecha_inicio : fechaPrimeraTransaccion;
                    var fechaHastaParaSaldo = fechaActual;


                    //Obtener los movimientos de dinero desde la fecha del ultimo arqueo de caja hasta la fecha actual
                    List<Movimiento_Caja> movimientos = transaccionRepositorio.ObtenerMovimientoDeCaja(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, cajaVigente.id, fechaDesdeParaSaldo, fechaHastaParaSaldo).ToList();
                    //Obtnener el saldo de dinero en la caja de acuerdo a los movimientos
                    decimal nuevoImporteArqueoCaja = hayArqueoDeCajaAnterior ? ultimoArqueoDeCaja.importe_total : 0 + (movimientos.Count > 0 ? movimientos.Where(m => m.EsIngreso).Sum(m => m.Monto) - movimientos.Where(m => !m.EsIngreso).Sum(m => m.Monto) : 0);
                    //Creamos el nuevo arqueo de caja con el nuevo importe
                    Transaccion arqueoCaja = CrearArqueoCaja(operacionGenerica.Id, operacionGenerica.IdComprobante, idEmpleado, fechaActual, codigo, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, "", cajaVigente.id, nuevoImporteArqueoCaja);
                    arqueosDeCaja.Add(arqueoCaja);
                    maximo++;
                }
                return transaccionRepositorio.CrearTransacciones(arqueosDeCaja);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar arqueo de caja", e);
            }
        }
        /// <summary>
        /// Obtiene el saldo que tendria una caja de acuerdo a la fecha
        /// </summary>
        /// <param name="idCaja"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public decimal ObtenerSaldoDeCaja(int idCaja, DateTime fecha)
        {
            try
            {
                //Obtener su ultimo arque de caja que tenga la caja vigente 
                Transaccion ultimoArqueoDeCaja = transaccionRepositorio.ObtenerUltimaTransaccionAntesDe(idCaja, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, fecha);
                //Obtener los movimientos de dinero desde la fecha del ultimo arqueo de caja hasta la fecha actual
                List<Movimiento_Caja> movimientos = transaccionRepositorio.ObtenerMovimientoDeCaja(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, idCaja, ultimoArqueoDeCaja.fecha_inicio, fecha).ToList();
                //Obtnener el saldo de dinero en la caja de acuerdo a los movimientos
                decimal saldoDeCaja = ultimoArqueoDeCaja.importe_total + movimientos.Where(m => m.EsIngreso).Sum(m => m.Monto) - movimientos.Where(m => !m.EsIngreso).Sum(m => m.Monto);
                //Retornar el saldo de caja


                return saldoDeCaja;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el saldo de caja", e);
            }
        }
        /// <summary>
        /// Obtiene el saldo que tendria las cajasseleccionadas de acuerdo a la fecha
        /// </summary>
        /// <param name="idsCaja"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public decimal ObtenerSaldoDeCaja(int[] idsCaja, DateTime fecha)
        {
            try
            {
                decimal saldo = 0;
                foreach (var idCaja in idsCaja)
                {
                    saldo += ObtenerSaldoDeCaja(idCaja, fecha);
                }
                return saldo;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el saldo de las cajas seleccionadas", e);
            }
        }
        /// <summary>
        /// Obtiene el saldo de acuerdo a la fecha fecha
        /// </summary>
        /// <param name="idCaja"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public decimal ObtenerSaldoDeCaja(DateTime fecha)
        {
            try
            {
                //Obtener su ultimo arque de caja que tenga la caja vigente 
                Transaccion ultimoArqueoDeCaja = transaccionRepositorio.ObtenerUltimaTransaccionAntesDe(TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, fecha);
                //Obtener los movimientos de dinero desde la fecha del ultimo arqueo de caja hasta la fecha actual
                List<Movimiento_Caja> movimientos = transaccionRepositorio.ObtenerMovimientoDeCaja(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, ultimoArqueoDeCaja.fecha_inicio, fecha).ToList();
                //Obtnener el saldo de dinero en la caja de acuerdo a los movimientos
                decimal saldoDeCaja = ultimoArqueoDeCaja.importe_total + movimientos.Where(m => m.EsIngreso).Sum(m => m.Monto) - movimientos.Where(m => !m.EsIngreso).Sum(m => m.Monto);
                //Retornar el saldo de caja
                return saldoDeCaja;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el saldo de caja", e);
            }
        }

        public List<Resumen_Movimiento_Caja> ObtenerSaldosIniciales(DateTime fechaDesde, bool reporteGlobal, int[] idsCentrosAtencion)
        {
            try
            {
                List<Resumen_Movimiento_Caja> movimientosResumen = new List<Resumen_Movimiento_Caja>();

                if (reporteGlobal)
                {
                    var cajasVigentes = actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEntidadInterna, ActorSettings.Default.IdRolCaja, true).ToList();
                    foreach (var cajaVigente in cajasVigentes)
                    {
                        var saldoInicial = ObtenerSaldoDeCaja(cajaVigente.id, fechaDesde);
                        var resumen = new Resumen_Movimiento_Caja()
                        {
                            IdCaja = cajaVigente.id,
                            SaldoInicial = saldoInicial
                        };
                        movimientosResumen.Add(resumen);
                    }
                }
                else
                {
                    foreach (var idCentroAtencion in idsCentrosAtencion)
                    {
                        var saldoInicial = ObtenerSaldoDeCaja(idCentroAtencion, fechaDesde);
                        var resumen = new Resumen_Movimiento_Caja()
                        {
                            IdCaja = idCentroAtencion,
                            SaldoInicial = saldoInicial
                        };
                        movimientosResumen.Add(resumen);
                    }
                }
                return movimientosResumen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el saldo inicial de caja", e);
            }
        }
        #endregion

        #region REPORTE DE CAJA

        public List<Movimiento_Caja> ObtenerReporteDeCaja(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion)
        {
            try
            {
                List<Movimiento_Caja> resultado;
                if (reporteGlobal)
                {
                    resultado = transaccionRepositorio.ObtenerMovimientoDeCaja(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, fechaDesde, fechaHasta).ToList();
                }
                else
                {
                    resultado = transaccionRepositorio.ObtenerMovimientoDeCaja(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, idsCentrosAtencion, fechaDesde, fechaHasta).ToList();
                }
                var resultadoOrdenado = resultado.OrderBy(r => r.Fecha).ToList();
                return resultadoOrdenado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de caja", e);
            }

        }
        #endregion

        public List<DetalleCuotaPago> ObtenerDetallesCuotaPagoOperacion(long idOperacion)
        {
            try
            {
                var detallesCuotaPago = transaccionRepositorio.ObtenerDetallesCuotaPagoDeOperacion(idOperacion).ToList();
                return detallesCuotaPago;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el reporte de caja", e);
            }

        }
    }
}

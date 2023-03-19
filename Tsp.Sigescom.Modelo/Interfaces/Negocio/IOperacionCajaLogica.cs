using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {
        //OperationResult pagarCuota(int idEmpleado, int idCaja, int idCuota, int idMoneda, decimal tipoDeCambio, decimal importeAPagar, DateTime fechaDePago, int idMedioPago, int idEntidadBancaria, string informacionMedioPago, string comentario);

        //OperationResult pagarCuota(int idEmpleado, int idCaja, int[] idsCuotas, decimal importeAPagar, DateTime fechaDePago, int idMedioDePago, int idEntidadBancaria, string informacionDeMedioPago, string comentario);

        //OperationResult pagarCuota(int idEmpleado, int idCaja, int idCuota, decimal importeAPagar, DateTime fechaDePago, int idMedioPago, int idEntidadBancaria, string informacionMedioPago, string comentario);

        //OperationResult pagarCuota(int idEmpleado, int idCaja, int[] idsCuotas, int idMoneda, decimal tipoDeCambio, decimal importeAPagar, DateTime fechaDePago, int idMedioPago, int idEntidadBancaria, string informacionMedioPago, string comentario);

        List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrar();
        List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorPagar();
        List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarPorGrupos(bool todosLosGrupos, int?[] idsGrupos);
        List<Cuenta_Cobrar_Pagar> ObtenerCuentasPorPagarPorGrupos(bool todosLosGrupos, int?[] idsGrupos);
        List<Cobro_Pago> ObtenerCobros(DateTime desde, DateTime hasta);
        List<Cobro_Pago> ObtenerPagos(DateTime desde, DateTime hasta);
        MovimientoEconomico ObtenerMovimientoEconomico(long idOperacion);
        OperationResult InvalidarMovimientoEconomico(long idOperacion, string observacion, UserProfileSessionData profileSessionData);
         Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCobrar(int idTipoTransaccionOrden, int idCentroDeAtencion);

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaPagar(int idTipoTransaccionOrden, int idCentroDeAtencion);

        OperationResult GuardarMovimientoEconomico(MovimientoEconomico_ movimiento, UserProfileSessionData sesionDeUsuario);

        CuentaPorCobrarPagar ObtenerCuentaIncluidoOperacion(long idCuota);

        OperationResult GuardarIngresoVarios(int idEmpleado, int idCaja, decimal importe, int idEmisor, int idPagador, int idSerieDeComprobante, string observacion);

        OperationResult GuardarEgresoVarios(int idEmpleado, int idCaja, decimal importe, int idEmisor, int idBeneficiario, int idSerieDeComprobante, string observacion);

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaIngresoVarios();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaIngresoVarios(int idCentroDeAtencion);
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaEgresoVarios();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaEgresoVarios(int idCentroDeAtencion);
        List<CuentaBancaria> ObtenerCuentasBancarias();
        List<CuentaBancaria> ObtenerCuentasBancariasPorEntidadFinanciera(int idEntidadFinanciera);
        List<ItemGenerico> ObtenerCuentasBancariasConEntidadFinancieraConMoneda();
        OperationResult CrearCuentaBancaria(CuentaBancaria cuentaBancaria);
        OperationResult ActualizarCuentaBancaria(CuentaBancaria cuentaBancaria);
        List<CajaInicializar> ObtenerInicializarCaja();
        OperationResult GuardarInicializarCaja(int idEmpleado, List<CajaInicializar> cajasAInicializar);
        OperationResult GenerarArqueoDeCaja(int idEmpleado);
        decimal ObtenerSaldoDeCaja(int idCaja, DateTime fecha);
        decimal ObtenerSaldoDeCaja(int[] idsCaja, DateTime fecha);
        decimal ObtenerSaldoDeCaja(DateTime fecha);
        List<Resumen_Movimiento_Caja> ObtenerSaldosIniciales(DateTime fechaDesde, bool reporteGlobal, int[] idsCentrosAtencion);
        List<Movimiento_Caja> ObtenerReporteDeCaja(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion);
        List<DetalleCuotaPago> ObtenerDetallesCuotaPagoOperacion(long idOperacion);
    }
}

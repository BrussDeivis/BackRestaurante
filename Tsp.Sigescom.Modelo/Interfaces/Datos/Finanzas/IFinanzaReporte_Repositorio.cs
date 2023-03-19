using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos
{
    public interface IFinanzaReporte_Repositorio
    {
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresos(int idCaja, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorOperaciones(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso, int[] mediosPago);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorOperacionesYMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones, int[] mediosPago);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancaria(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso, int[] mediosPago);
        IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones, int[] mediosPago);
        IEnumerable<DetalleFlujo> ObtenerIngresosEgresos(int idCaja, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<DetalleFlujo> ObtenerIngresosEgresosPorMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] mediosPago);
        IEnumerable<DetalleFlujo> ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] mediosPago);
        IEnumerable<DetalleFlujo> ObtenerIngresosEgresosEnCuentaBancaria(int idCuenta, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<OperacionGrupo> ObtenerCuentasPorCobarGrupos(int[] idsGrupos);
        IEnumerable<OperacionGrupoDetallado> ObtenerCuentasPorCobarGrupoDetallado(int idGrupo);
    }
}

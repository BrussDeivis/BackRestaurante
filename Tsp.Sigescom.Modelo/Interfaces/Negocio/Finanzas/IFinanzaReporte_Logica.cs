using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;

namespace Tsp.Sigescom.Modelo.Negocio.Finanza
{
    public interface IFinanzaReporte_Logica
    {
        PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData);
        List<IngresoEgreso> Ingresos(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago, bool todasLasOperaciones, int[] operaciones);
        List<IngresoEgreso> Egresos(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago, bool todasLasOperaciones, int[] operaciones);
        FlujoIngresoEgreso Flujo(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago);
        List<OperacionGrupo> ObtenerCuentasPorCobrarGrupos(bool todosLosGrupos, int[] idsGrupos);
        List<OperacionGrupoDetallado> ObtenerCuentasPorCobrarGrupoDetallado(int idGrupo);
    }
}

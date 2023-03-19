using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Negocio.Venta.Report;

namespace Tsp.Sigescom.Modelo.Negocio.Venta
{
    public interface IVentaReporte_Logica
    {
        PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData);
        List<OperacionFamiliaGrupo> ObtenerVentasPorFamiliasGrupos(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias, bool todosLosGrupos, int[] idsGrupos);
        List<OperacionGrupo> ObtenerVentasPorGrupos(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosGrupos, int[] idsGrupos);
        List<OperacionGrupoDetallado> ObtenerVentasPorGrupoDetallado(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int idGrupo);
    }
}

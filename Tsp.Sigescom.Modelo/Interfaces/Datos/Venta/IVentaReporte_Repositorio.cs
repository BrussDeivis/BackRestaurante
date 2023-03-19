using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Venta
{
    public interface IVentaReporte_Repositorio
    {
        IEnumerable<OperacionFamiliaGrupo> ObtenerVentasPorFamiliasGrupos(int[] idsTipoTransaccion, int idEstado, int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int[] idsFamilias, int[] idsGrupos);
        IEnumerable<OperacionGrupo> ObtenerVentasPorGrupos(int[] idsTipoTransaccion, int idEstado,  int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int[] idsGrupos);
        IEnumerable<OperacionGrupoDetallado> ObtenerVentasPorGrupoDetallado(int[] idsTipoTransaccion, int idEstado,  int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int idGrupo);
    }
}

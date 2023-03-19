using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Comprobante;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen
{
    public interface IOrdenAlmacen_Repositorio
    {
        IEnumerable<OrdenAlmacenResumen> ObtenerOrdenesAlmacen(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, int[] idsModoEntrega, int[] idsEstados, int[] idsAlmacenes);
        IEnumerable<OrdenAlmacenResumen> ObtenerOrdenesAlmacenBidireccional(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, int[] idsModoEntrega, int[] idsEstados, int[] idsAlmacenes);
        bool VerificarOrdenAlmacenBidireccional(long idOrdenAlmacen);
        OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen);
        OrdenAlmacen ObtenerOrdenAlmacenBireccional(long idOrdenAlmacen, bool porIngresar);
        IEnumerable<OrdenDeOrdenAlmacen> ObtenerOrdenesDeOrdenAlmacen(long[] IdsOrdenes);
        IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] IdsOrdenes);
        IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] IdsOrdenes, bool porIngresar);
        IEnumerable<MovimientoDeOrdenAlmacen> ObtenerNotasCreditoDeOrdenAlmacen(long idOrdenAlmacen);
        IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosConfirmadosDeOrdenAlmacen(long[] IdsOrdenes);
        OrdenAlmacen ObtenerOrdenAlmacenConIdMovimientoOrdenAlmacen(long IdMovimientoOrdenAlmacen);
        IEnumerable<DetalleDeOrdenAlmacen> ObtenerStockActualDetallesOrdenAlmacen(int[] idsConceptos, int idAlmacen);
    }
}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Comprobante;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen
{
    public interface IMovimientos_Repositorio
    {
        IEnumerable<InventarioFisico> ObtenerSaldosDeMovimientosPorConceptoYLote(int[] idsConceptos, int[] idsAlmacenes);
        IEnumerable<InventarioFisico> ObtenerSaldosDeMovimientosPorConcepto(int[] idsConceptos, int[] idsAlmacenes);
        IEnumerable<MovimientoAlmacen> ObtenerMovimientosDeAlmacenes(int idAlmacen, int[] idsConceptosNegocio, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<MovimientoAlmacen> ObtenerMovimientosDeAlmacenesConOrdenYOrigen(int idAlmacen, int[] idsConceptosNegocio, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<MovimientoAlmacen> ObtenerDetallesMovimientoAlmacen(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<ComprobanteDeOperacion> ObtenerComprobantesDeOrdenes(long[] idOrdenes);

        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta);
        Movimientos_concepto_negocio_actor_negocio_interno ObtenerMovimientosDeConceptoNegocio(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocio(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta, int[] familias);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocio(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioConLotePorEntidadInterna(int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioConLotePorEntidadInternaYConceptoNegocio(int idActorNegocioInterno, int idConceptoNegocio, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioPorEntidadInternaYConceptoNegocio(int idActorNegocioInterno, int idConceptoNegocio, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<EntradaAlmacen> ObtenerEntradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<SalidaAlmacen> ObtenerSalidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<EntradaAlmacen> ObtenerEntradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias);
        IEnumerable<SalidaAlmacen> ObtenerSalidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias);
    }
}

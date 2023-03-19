using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen

{
    public interface IInventarioActualRepositorio
    {
        

        IEnumerable<Detalle_transaccion> ObtenerDetallesInventarioActualIncluyendoConceptoNegocio(int idActorNegocioInterno);
        Transaccion ObtenerUltimaOperacionInventarioActual();
        IEnumerable<InventarioValorizado> ObtenerInventarioValorizadoActual(int idAlmacen);
        IEnumerable<InventarioValorizado> ObtenerInventarioValorizadoActual(int idAlmacen, int[] familias);
        IEnumerable<InventarioValorizado> ObtenerInventariosValorizadosActuales(int[] idsAlmacenes, int[] familias);
        IEnumerable<InventarioValorizado> ObtenerInventariosValorizadosActuales(int[] idsAlmacenes);
        InventarioValorizado ObtenerInventarioValorizadoActual(int idAlmacen, int idConcepto);
        IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforoActual(int idAlmacen);
        IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforoActual(int idAlmacen, int[] familias);
        IEnumerable<InventarioSemaforo> ObtenerInventariosSemaforoActuales(int[] idsAlmacenes);
        IEnumerable<InventarioSemaforo> ObtenerInventariosSemaforoActuales(int[] idsAlmacenes, int[] familias);
        IEnumerable<InventarioFisico> ObtenerInventariosFisicosActuales(int[] idsAlmacenes);
        IEnumerable<InventarioFisico> ObtenerInventariosFisicosActuales(int[] idsAlmacenes, int[] familias);
        IEnumerable<InventarioFisico> ObtenerInventarioFisicoActual(int idAlmacen);
        IEnumerable<InventarioFisico> ObtenerInventarioFisicoActual(int idAlmacen, int[] familias);
        IEnumerable<InventarioFisico> ObtenerInventarioFisicoConceptosActual(int idAlmacen, int[] idsConceptos);
        InventarioFisico ObtenerInventarioFisicoActual(int idAlmacen, int idConcepto);
        IEnumerable<InventarioVencimiento> ObtenerVencimientosInventarioActual(int idAlmacen, DateTime vencimientoDesde, DateTime vencimientoHasta);
        IEnumerable<InventarioVencimiento> ObtenerVencimientosInventarioActual(int idAlmacen, DateTime vencimientoDesde, DateTime vencimientoHasta, int[] familias);

        /// <summary>
        /// Este metodo retorna todos los conceptos basicos incluyendo las caracteristicas
        /// </summary>
        IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActual(int idAlmacen, int idCentroAtencionPrecios);

        /// <summary>
        /// Este metodo se encargara de retornar el inventario valorizadoo sengun el idConceptoBasico, idsValoresDeCaracteristicas
        /// </summary>
        IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorCaracteristicasYFamilias(int idAlmacen, int idCentroAtencionPrecios, int[] idsValoresDeCaracteristicas, int[] idsConceptosBasicos);

        /// <summary>
        /// Este metodo se encarga de retornar el inventario valorizado segun idConceptoBasico
        /// </summary>
        IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorFamilias(int idAlmacen, int idCentroAtencionPrecios, int[] idsConceptosBasicos);

        /// <summary>
        /// Este metodo se encargara de retornar el inventario valorizado segun los idsValoresDeCaracteristicas
        /// </summary>
        IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorCaracteristicas(int idAlmacen, int idCentroAtencionPrecios, int[] idsValoresDeCaracteristicas);
        Dictionary<long, long> ObtenerIdsInventarioActualPorAlmacen();

    }
}

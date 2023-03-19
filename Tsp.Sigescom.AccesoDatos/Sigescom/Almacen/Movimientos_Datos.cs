using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using System.Data.Entity;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System.Data.SqlClient;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Comprobante;

namespace Tsp.Sigescom.AccesoDatos.Almacen
{
    public partial class Movimientos_Datos : IMovimientos_Repositorio
    {
        public IEnumerable<InventarioFisico> ObtenerSaldosDeMovimientosPorConceptoYLote(int[] idsConceptos, int[] idsAlmacenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var saldos = _db.Transaccion
                    .Where(t => idsAlmacenes.Contains(t.id_actor_negocio_interno) && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                   )
                .SelectMany(t => t.Detalle_transaccion).Where(dt => idsConceptos.Contains(dt.id_concepto_negocio)).
                GroupBy(dt => new
                {
                    IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                    IdConceptoNegocio = dt.id_concepto_negocio,
                    IdLote = dt.lote
                }).Select(m => new InventarioFisico()
                {
                    IdAlmacen = m.Key.IdAlmacen,
                    IdConcepto = m.Key.IdConceptoNegocio,
                    Lote = m.Key.IdLote,
                    Cantidad = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    CantidadSecundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum() - (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum()
                });
                return saldos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<InventarioFisico> ObtenerSaldosDeMovimientosPorConcepto(int[] idsConceptos, int[] idsAlmacenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var saldos = _db.Transaccion
                    .Where(t => idsAlmacenes.Contains(t.id_actor_negocio_interno) && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                   )
                .SelectMany(t => t.Detalle_transaccion).Where(dt => idsConceptos.Contains(dt.id_concepto_negocio)).
                GroupBy(dt => new
                {
                    IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                    IdConceptoNegocio = dt.id_concepto_negocio,
                }).Select(m => new InventarioFisico()
                {
                    IdAlmacen = m.Key.IdAlmacen,
                    IdConcepto = m.Key.IdConceptoNegocio,
                    Cantidad = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    CantidadSecundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum() - (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum()
                });
                return saldos.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idAlmacen
                    && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion).GroupBy(dt => new
                {
                    IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                    IdConceptoNegocio = dt.id_concepto_negocio,
                    Lote = dt.lote
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Id_entidad_interna = m.Key.IdEntidadInterna,
                    Lote = m.Key.Lote,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocio(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idAlmacen
                    && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .SelectMany(t => t.Detalle_transaccion).GroupBy(dt => new
                    {
                        IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                        IdConceptoNegocio = dt.id_concepto_negocio,
                    }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                    {
                        Id_entidad_interna = m.Key.IdEntidadInterna,
                        Lote = null,
                        Id_concepto_negocio = m.Key.IdConceptoNegocio,
                        Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                    });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idActorNegocioInterno
                    && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .SelectMany(t => t.Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)))
                    .GroupBy(dt => new
                    {
                        IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                        IdConceptoNegocio = dt.id_concepto_negocio,
                        Lote = dt.lote
                    }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                    {
                        Id_entidad_interna = m.Key.IdEntidadInterna,
                        Lote = m.Key.Lote,
                        Id_concepto_negocio = m.Key.IdConceptoNegocio,
                        Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                    });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocio(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                     && t.fecha_inicio <= fechaHasta
                     && t.id_actor_negocio_interno == idAlmacen
                     && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                     && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico))).
                GroupBy(dt => new
                {
                    IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                    IdConceptoNegocio = dt.id_concepto_negocio,
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Id_entidad_interna = m.Key.IdEntidadInterna,
                    Lote = null,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosDeConceptoNegocioYLote(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimiento = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idAlmacen
                    && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto)).
                GroupBy(dt => new
                {
                    IdConceptoNegocio = dt.id_concepto_negocio,
                    Lote = dt.lote
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Lote = m.Key.Lote,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt =>idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                });
                return movimiento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioConLotePorEntidadInterna(int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idActorNegocioInterno
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                 .SelectMany(t => t.Detalle_transaccion).
                 GroupBy(dt => new
                 {
                     IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                     IdConceptoNegocio = dt.id_concepto_negocio,
                     Lote = dt.lote
                 }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                 {
                     Id_entidad_interna = m.Key.IdEntidadInterna,
                     Id_concepto_negocio = m.Key.IdConceptoNegocio,
                     Lote = m.Key.Lote,
                     Entradas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                     Salidas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && !antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                     Entradas_secundaria = (decimal)m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && dt.cantidad_secundaria != null).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                     Salidas_secundaria = (decimal)m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && !antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && dt.cantidad_secundaria != null).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum()
                 });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioPorEntidadInternaYConceptoNegocio(int idEntidadInterna, int idConceptoNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var movimientos = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && idEntidadInterna == t.id_actor_negocio_interno
                    && t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConceptoNegocio)
                    .Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                    {
                        Id_entidad_interna = m.Transaccion.id_actor_negocio_interno,
                        Id_concepto_negocio = m.id_concepto_negocio,
                        EsIngreso = m.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen).valor,
                        Costo_Unitario = m.precio_unitario,
                        Total = m.total,
                        Cantidad_Principal = m.cantidad,
                        Cantidad_Secundaria = (decimal)m.cantidad_secundaria,
                        Fecha_inicio = m.Transaccion.fecha_inicio
                    });
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar movimientos", e);
            }
        }

        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioConLotePorEntidadInternaYConceptoNegocio(int idActorNegocioInterno, int idConceptoNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var movimientos = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && idActorNegocioInterno == t.id_actor_negocio_interno
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConceptoNegocio)
                    .GroupBy(dt => new
                    {
                        IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                        IdConceptoNegocio = dt.id_concepto_negocio,
                        Lote = dt.lote
                    }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                    {
                        Id_entidad_interna = m.Key.IdEntidadInterna,
                        Id_concepto_negocio = m.Key.IdConceptoNegocio,
                        Lote = m.Key.Lote,
                        Entradas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Salidas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && !antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Entradas_secundaria = (decimal)m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && dt.cantidad_secundaria != null).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Salidas_secundaria = (decimal)m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && !antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && dt.cantidad_secundaria != null).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                        Fecha_inicio = m.Select(dt => dt.Transaccion.fecha_inicio).FirstOrDefault()
                    });
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar movimientos", e);
            }
        }


        public IEnumerable<MovimientoAlmacen> ObtenerDetallesMovimientoAlmacen(int idActorNegocioInterno, int idConceptoNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var detalles = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idActorNegocioInterno && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => dt.id_concepto_negocio == idConceptoNegocio)
                                .Select(dt => new MovimientoAlmacen()
                                {
                                    IdTransaccion = dt.Transaccion.id,
                                    IdOrden = dt.Transaccion.id_transaccion_referencia,
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    IdActorNegocioExterno = dt.Transaccion.id_actor_negocio_externo,
                                    IdActorNegocioInterno = dt.Transaccion.id_actor_negocio_interno,
                                    IdEmpleado = dt.Transaccion.id_empleado,
                                    Empleado = dt.Transaccion.Actor_negocio.Actor.primer_nombre,
                                    NombreActorNegocioExterno = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                                    NombreActorNegocioInterno = dt.Transaccion.Actor_negocio2.Actor.primer_nombre,
                                    IdTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                                    NombreTipoTransaccion = dt.Transaccion.Tipo_transaccion.nombre,
                                    IdTipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.id,
                                    NombreTipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.nombre,
                                    NumeroSerieDeSerieComprobante = dt.Transaccion.Comprobante.Serie_comprobante.numero,
                                    NumeroSerieDeComprobante = dt.Transaccion.Comprobante.numero_serie,
                                    NumeroComprobante = dt.Transaccion.Comprobante.numero,
                                    CodigoTipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                                    Cantidad = dt.cantidad,
                                    ImporteUnitario = dt.precio_unitario,
                                    ImporteTotal = dt.total,
                                    EsEntrada = dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen).valor
                                }
                                );
                return detalles.OrderBy(dtk => dtk.Fecha).ThenBy(dtk => dtk.IdTransaccion);
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        public IEnumerable<ComprobanteDeOperacion> ObtenerComprobantesDeOrdenes(long[] idOrdenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var comprobantes = _db.Transaccion
                                .Where(t => idOrdenes.Contains(t.id))
                                .Select(t => new ComprobanteDeOperacion()
                                {
                                    IdOperacion = t.id,
                                    Comprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                }
                                );
                return comprobantes;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener comprobantes de ordenes", e); }
        }

        public IEnumerable<MovimientoAlmacen> ObtenerMovimientosDeAlmacenes(int idAlmacen, int[] idsConceptosNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var detalles = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idAlmacen == t.id_actor_negocio_interno && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => idsConceptosNegocio.Contains(dt.id_concepto_negocio))
                                .Select(dt => new MovimientoAlmacen()
                                {
                                    IdDetalleTransaccion = dt.id,
                                    IdTransaccion = dt.Transaccion.id,
                                    IdTransaccionPadre = dt.Transaccion.id_transaccion_padre ?? 0,
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    IdTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                                    Cantidad = dt.cantidad,
                                    ImporteUnitario = dt.precio_unitario,
                                    ImporteTotal = dt.total,
                                    IdConcepto = dt.id_concepto_negocio
                                    //EsEntrada = dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen).valor
                                }
                                ).ToList();
                detalles.ForEach(d => d.EsEntrada = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.Contains(d.IdTipoTransaccion));
                return detalles.OrderBy(dtk => dtk.Fecha).ThenBy(dtk => dtk.IdTransaccion);
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener movimientos de almacenes", e); }
        }

        public IEnumerable<MovimientoAlmacen> ObtenerMovimientosDeAlmacenesConOrdenYOrigen(int idAlmacen, int[] idsConceptosNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var movimientos = ObtenerMovimientosDeAlmacenes(idAlmacen, idsConceptosNegocio, fechaDesde, fechaHasta);
                var idsTiposTransaccionMovimientos = movimientos.Select(m => m.IdTipoTransaccion).Distinct().ToArray();
                var tiposOperacionSegunInventario = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioSegunInventario;
                var tiposOperacionSegunOrden = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden;
                var tiposOperacionSegunTransaccionOrigen = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen;
                var idsTiposTransaccionMovimiento = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.Union(Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimientosSegunOrden = movimientos.Where(m => tiposOperacionSegunOrden.Contains(m.IdTipoTransaccion)).ToList();
                var movimientosSegunOrdenAgrupadosPorTransaccion = movimientosSegunOrden.GroupBy(mso => mso.IdTransaccion);
                var idsTipoTransaccionOrden = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.Where(t => idsTiposTransaccionMovimientos.Contains(t.Value)).Select(t => t.Key).ToArray();


                var idsTransaccionesPadreMovimientosSegunOrden = movimientosSegunOrden.Select(m => m.IdTransaccionPadre).Distinct().ToArray();
                var transaccionesOrdenConDetalles = _db.Transaccion.Where(t => idsTransaccionesPadreMovimientosSegunOrden.Contains(t.id_transaccion_padre) && idsTipoTransaccionOrden.Contains(t.id_tipo_transaccion)).Include(t => t.Detalle_transaccion).ToList();

                foreach (var g in movimientosSegunOrdenAgrupadosPorTransaccion)
                {
                    long? idTransaccionPadre = g.First().IdTransaccionPadre;
                    var transaccionOrdenConDetalle = transaccionesOrdenConDetalles.FirstOrDefault(t => t.id_transaccion_padre == idTransaccionPadre);
                    if (transaccionOrdenConDetalle != null)
                    {
                        List<Detalle_transaccion> detallesOrden = transaccionOrdenConDetalle.Detalle_transaccion.ToList();
                        g.ToList().ForEach(m =>
                        {
                            Detalle_transaccion detalleOrden = detallesOrden.FirstOrDefault(d => d.id_concepto_negocio == m.IdConcepto && d.cantidad == m.Cantidad);
                            m.ImporteTotalOrden = detalleOrden.total;
                            m.IgvOrden = detalleOrden.igv;
                        });
                    }
                }

                var movimientosSegunMovimientoOrigen = movimientos.Where(m => tiposOperacionSegunTransaccionOrigen.Contains(m.IdTipoTransaccion)).ToList();

                var idsmovimientosSegunMovimientoOrigen = movimientosSegunMovimientoOrigen.Select(m => m.IdTransaccion);
                var transaccionesReferenciaMovimientosSegunOrigen = _db.Transaccion.Where(t => idsmovimientosSegunMovimientoOrigen.Contains(t.id) && t.Transaccion3 != null).Select(tt => new { Transaccion = tt, IdOrigen = tt.Transaccion3.Transaccion3.Transaccion11.FirstOrDefault(_t => idsTiposTransaccionMovimiento.Contains(_t.id_tipo_transaccion)).id }).ToList();

                movimientosSegunMovimientoOrigen.ForEach(m =>
                {
                    var transaccionReferencia = transaccionesReferenciaMovimientosSegunOrigen.FirstOrDefault(tt => tt.Transaccion.id == m.IdTransaccion);
                    if (transaccionReferencia != null)
                    {
                        m.IdTransaccionMovimientoOrigen = transaccionReferencia.IdOrigen;
                    }

                });
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener movimientos de almacenes", e);
            }
        }

        public IEnumerable<EntradaAlmacen> ObtenerEntradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var entradas = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == true)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Select(dt => new EntradaAlmacen()
                                {
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    Operacion = dt.Transaccion.Tipo_transaccion.nombre,
                                    Origen = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                                    Empleado = dt.Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                    TipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = dt.Transaccion.Comprobante.numero_serie + "-" + dt.Transaccion.Comprobante.numero,
                                    Cantidad = dt.cantidad,
                                    Concepto = dt.Concepto_negocio.nombre

                                }
                                );

            return entradas;
        }
        public IEnumerable<EntradaAlmacen> ObtenerEntradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var entradas = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == true)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico))
                                .Select(dt => new EntradaAlmacen()
                                {
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    Operacion = dt.Transaccion.Tipo_transaccion.nombre,
                                    Origen = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                                    Empleado = dt.Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                    TipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = dt.Transaccion.Comprobante.numero_serie + "-" + dt.Transaccion.Comprobante.numero,
                                    Cantidad = dt.cantidad,
                                    Concepto = dt.Concepto_negocio.nombre

                                }
                                );

            return entradas;
        }

        public IEnumerable<SalidaAlmacen> ObtenerSalidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var salidas = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == false)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Select(dt => new SalidaAlmacen()
                                {
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    Operacion = dt.Transaccion.Tipo_transaccion.nombre,
                                    Destino = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                                    Empleado = dt.Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                    TipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = dt.Transaccion.Comprobante.numero_serie + "-" + dt.Transaccion.Comprobante.numero,
                                    Cantidad = dt.cantidad,
                                    Concepto = dt.Concepto_negocio.nombre
                                }
                                );
            return salidas;
        }
        public IEnumerable<SalidaAlmacen> ObtenerSalidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var salidas = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == false)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico))
                                .Select(dt => new SalidaAlmacen()
                                {
                                    Fecha = dt.Transaccion.fecha_inicio,
                                    Operacion = dt.Transaccion.Tipo_transaccion.nombre,
                                    Destino = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                                    Empleado = dt.Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                    TipoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = dt.Transaccion.Comprobante.numero_serie + "-" + dt.Transaccion.Comprobante.numero,
                                    Cantidad = dt.cantidad,
                                    Concepto = dt.Concepto_negocio.nombre
                                }
                                );
            return salidas;
        }
        public Movimientos_concepto_negocio_actor_negocio_interno ObtenerMovimientosDeConceptoNegocio(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimiento = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                    && t.fecha_inicio <= fechaHasta
                    && t.id_actor_negocio_interno == idAlmacen
                    && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto)).
                GroupBy(dt => new
                {
                    IdConceptoNegocio = dt.id_concepto_negocio,
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Lote = null,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                }).SingleOrDefault();
                return movimiento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Movimientos_concepto_negocio_actor_negocio_interno ObtenerMovimientosDeConceptoNegocioYLote_UnConcepto(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimiento = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                     && t.fecha_inicio <= fechaHasta
                     && t.id_actor_negocio_interno == idAlmacen
                     && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                     && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto)).
                GroupBy(dt => new
                {
                    IdConceptoNegocio = dt.id_concepto_negocio,
                    Lote = dt.lote
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Lote = m.Key.Lote,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                }).SingleOrDefault();
                return movimiento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Movimientos_concepto_negocio_actor_negocio_interno ObtenerMovimientosDeConceptoNegocio_UnConcepto(int idAlmacen, int idConcepto, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                int[] idsTiposDeTransaccionMovimientoDeBienes_Entradas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas;
                int[] idsTiposDeTransaccionMovimientoDeBienes_Salidas = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas;
                int[] idsTiposTransaccionMovimientoAlmacen = idsTiposDeTransaccionMovimientoDeBienes_Entradas.Union(idsTiposDeTransaccionMovimientoDeBienes_Salidas).ToArray();
                var movimiento = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde
                     && t.fecha_inicio <= fechaHasta
                     && t.id_actor_negocio_interno == idAlmacen
                     && idsTiposTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion)
                     && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                .SelectMany(t => t.Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto)).
                GroupBy(dt => new
                {
                    IdConceptoNegocio = dt.id_concepto_negocio,
                }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                {
                    Lote = null,
                    Id_concepto_negocio = m.Key.IdConceptoNegocio,
                    Entradas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Salidas_principal = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                    Entradas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Salidas_secundaria = (decimal)m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.cantidad_secundaria).DefaultIfEmpty(0).Sum(),
                    Total = m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Entradas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum() - m.Where(dt => idsTiposDeTransaccionMovimientoDeBienes_Salidas.Contains(dt.Transaccion.id_tipo_transaccion)).Select(dt => dt.total).DefaultIfEmpty(0).Sum()
                }).SingleOrDefault();
                return movimiento;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
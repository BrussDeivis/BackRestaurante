using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;

namespace Tsp.Sigescom.AccesoDatos.Almacen

{
    public partial class InventarioActual_Datos: IInventarioActualRepositorio
    {

        public IEnumerable<Detalle_transaccion> ObtenerDetallesInventarioActualIncluyendoConceptoNegocio(int idActorNegocioInterno)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                return _db.Transaccion
                                   .Where(t => t.id_actor_negocio_interno == idActorNegocioInterno
                                               && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                   ).SelectMany(t => t.Detalle_transaccion)
                                    .Include(dt => dt.Concepto_negocio);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener detalles de transaccion inclusive concepto negocio", e);
            }
        }

        public Transaccion ObtenerUltimaOperacionInventarioActual()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOperacionInventarioActual).OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<InventarioVencimiento> ObtenerVencimientosInventarioActual(int idAlmacen, DateTime vencimientoDesde, DateTime vencimientoHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).SelectMany(t => t.Detalle_transaccion).Where(dt => dt.vencimiento >= vencimientoDesde && dt.vencimiento <= vencimientoHasta).Select(dt => new InventarioVencimiento()
                {
                    IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                    CodigoBarra = dt.Concepto_negocio.codigo_barra,
                    Concepto = dt.Concepto_negocio.nombre,
                    Cantidad = dt.cantidad,
                    Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                    Lote = dt.lote,
                    UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                    FechaVencimiento = dt.vencimiento
                }
                ) ;
                return inventario;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        public IEnumerable<InventarioVencimiento> ObtenerVencimientosInventarioActual(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).SelectMany(t => t.Detalle_transaccion).Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico) && dt.vencimiento >= fechaDesde && dt.vencimiento <= fechaHasta).Select(dt => new InventarioVencimiento()
                {
                    IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                    CodigoBarra = dt.Concepto_negocio.codigo_barra,
                    Concepto = dt.Concepto_negocio.nombre,
                    Cantidad = dt.cantidad,
                    Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                    Lote = dt.lote,
                    UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                    FechaVencimiento = dt.vencimiento
                }
                );
                return inventario;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }



        public InventarioFisico ObtenerInventarioFisicoActual(int idAlmacen, int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).SelectMany(t=>t.Detalle_transaccion).Where(dt=>dt.id_concepto_negocio== idConcepto).GroupBy(dt => new { dt.Concepto_negocio.codigo_barra, dt.Concepto_negocio.nombre }).Select(dt => new InventarioFisico()
            {
                CodigoBarra = dt.Key.codigo_barra,
                Concepto = dt.Key.nombre,
                Cantidad = dt.Sum(d => d.cantidad),
                Familia = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                UnidadMedida = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro.codigo
            }
            ).SingleOrDefault();

            return inventario;
        }

        public InventarioValorizado ObtenerInventarioValorizadoActual(int idAlmacen, int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConcepto).GroupBy(dt => new { dt.Concepto_negocio.codigo_barra, dt.Concepto_negocio.nombre }).Select(dt => new InventarioValorizado()
            {
                CodigoBarra = dt.Key.codigo_barra,
                Concepto = dt.Key.nombre,
                Cantidad = dt.Sum(d => d.cantidad),
                Familia = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                UnidadMedida = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.Sum(d=> d.total),
                ValorUnitario = dt.Sum(d=> d.precio_unitario)
            }
            ).SingleOrDefault();

            return inventario;

        }
        public IEnumerable<InventarioFisico> ObtenerInventarioFisicoActual(int idAlmacen)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Select(dt => new InventarioFisico()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            );

            return inventario;
        }
        public IEnumerable<InventarioFisico> ObtenerInventarioFisicoConceptosActual(int idAlmacen, int[] idsConceptos)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Where(dt=>idsConceptos.Contains(dt.id_concepto_negocio)).Select(dt => new InventarioFisico()
            {
                IdConcepto= dt.id_concepto_negocio,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            );

            return inventario;
        }
        public IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforoActual(int idAlmacen)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Select(dt => new InventarioSemaforo()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                StockMinimo = dt.Concepto_negocio.stock_minimo
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioValorizado> ObtenerInventarioValorizadoActual(int idAlmacen)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Select(dt => new InventarioValorizado()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.total,
                ValorUnitario = dt.precio_unitario
            }
            );

            return inventario;
        }
        public IEnumerable<InventarioFisico> ObtenerInventarioFisicoActual(int idAlmacen, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioFisico()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioValorizado> ObtenerInventarioValorizadoActual(int idAlmacen, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioValorizado()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.total,
                ValorUnitario = dt.precio_unitario
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforoActual(int idAlmacen, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioSemaforo()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                StockMinimo=dt.Concepto_negocio.stock_minimo
            }
            );
            return inventario;
        }


      

        public IEnumerable<InventarioFisico> ObtenerInventariosFisicosActuales(int[] idsAlmacenes)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t=> t.Detalle_transaccion).Select(dt => new InventarioFisico()
            {
                IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioSemaforo> ObtenerInventariosSemaforoActuales(int[] idsAlmacenes)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t => t.Detalle_transaccion).Select(dt => new InventarioSemaforo()
            {
                IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                StockMinimo = dt.Concepto_negocio.stock_minimo
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioValorizado> ObtenerInventariosValorizadosActuales(int[] idsAlmacenes)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t => t.Detalle_transaccion).Select(dt => new InventarioValorizado()
            {
                IdDetalleTransaccion= dt.id,
                IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                IdConcepto= dt.id_concepto_negocio,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.total,
                ValorUnitario = dt.precio_unitario
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioFisico> ObtenerInventariosFisicosActuales(int[] idsAlmacenes, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t => t.Detalle_transaccion).Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioFisico()
            {
                IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioValorizado> ObtenerInventariosValorizadosActuales(int[] idsAlmacenes, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t => t.Detalle_transaccion).Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioValorizado()
            {
                IdAlmacen = dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.total,
                ValorUnitario = dt.precio_unitario
            }
            );
            return inventario;
        }
        public IEnumerable<InventarioSemaforo> ObtenerInventariosSemaforoActuales(int[] idsAlmacenes, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && idsAlmacenes.Contains(t.id_actor_negocio_interno)).SelectMany(t => t.Detalle_transaccion).Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioSemaforo()
            {
                IdAlmacen=dt.Transaccion.id_actor_negocio_interno,
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                StockMinimo = dt.Concepto_negocio.stock_minimo
            }
            );
            return inventario;
        }
    


        public IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActual(int idAlmacen, int idCentroAtencionPrecios)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                                 && t.id_actor_negocio_interno == idAlmacen
                                                 && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(t=>t.Detalle_transaccion)
                                      .Where(dt => dt.Concepto_negocio.es_vigente)
                           .Select(
                               riv => new Reporte_Inventario_Valorizado()
                               {
                                   IdConceptoNegocio = riv.id_concepto_negocio,
                                   Producto = riv.Concepto_negocio.nombre,
                                   Cantidad = riv.cantidad,
                                   Lote = riv.lote,
                                   CostoUnitario= riv.precio_unitario,
                                   PrecioVigente = riv.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idCentroAtencionPrecios && p.es_vigente && p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal)
                                                          .OrderByDescending(p => p.id)
                                                          .FirstOrDefault(),
                                   ValorCaracteristicasConceptoNegocio = riv.Concepto_negocio.Valor_caracteristica_concepto_negocio.Distinct()
                               }
                            );
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorCaracteristicasYFamilias(int idAlmacen, int idCentroAtencionPrecios, int[] idsValoresDeCaracteristicas,  int[] idsConceptosBasicos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var resultado = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                               && t.id_actor_negocio_interno == idAlmacen
                                               && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(t => t.Detalle_transaccion)
                                   .Where(dt => dt.Concepto_negocio.es_vigente 
                                   && dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Any(vccn => idsValoresDeCaracteristicas.Contains(vccn.id_valor_caracteristica)) 
                                   && idsConceptosBasicos.Contains(dt.Concepto_negocio.Detalle_maestro4.id))
                 .Select(
                      riv => new Reporte_Inventario_Valorizado()
                      {
                          IdConceptoNegocio = riv.id_concepto_negocio,
                          Producto = riv.Concepto_negocio.nombre,
                          Cantidad = riv.cantidad,
                          Lote = riv.lote,
                          CostoUnitario = riv.precio_unitario,
                          PrecioVigente = riv.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idCentroAtencionPrecios && p.es_vigente && p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal).OrderByDescending(p => p.id).FirstOrDefault(),
                          ValorCaracteristicasConceptoNegocio = riv.Concepto_negocio.Valor_caracteristica_concepto_negocio.Where(w => idsValoresDeCaracteristicas.Contains(w.id_valor_caracteristica)).Distinct()
                      }
                   );
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorFamilias(int idAlmacen, int idCentroAtencionPrecios,  int[] idsConceptosBasicos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var resultado = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                           && t.id_actor_negocio_interno == idAlmacen
                                           && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                               .SelectMany(t => t.Detalle_transaccion)
                               .Where(dt => dt.Concepto_negocio.es_vigente && idsConceptosBasicos.Contains(dt.Concepto_negocio.Detalle_maestro4.id))
                               .Select(
                                      riv => new Reporte_Inventario_Valorizado()
                                      {
                                          IdConceptoNegocio = riv.id_concepto_negocio,
                                          Producto = riv.Concepto_negocio.nombre,
                                          Cantidad = riv.cantidad,
                                          Lote = riv.lote,
                                          CostoUnitario = riv.precio_unitario,
                                          PrecioVigente = riv.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idCentroAtencionPrecios && p.es_vigente && p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal).OrderByDescending(p => p.id).FirstOrDefault(),
                                          ValorCaracteristicasConceptoNegocio = riv.Concepto_negocio.Valor_caracteristica_concepto_negocio.Distinct()
                                      }
                                   ).ToList();

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Reporte_Inventario_Valorizado> ObtenerInventarioActualPorCaracteristicas(int idAlmacen, int idCentroAtencionPrecios, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                var resultado =

                     _db.Transaccion
                               .Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                            && t.id_actor_negocio_interno == idAlmacen
                                            && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                               .SelectMany(t => t.Detalle_transaccion)
                               .Where(dt => dt.Concepto_negocio.es_vigente && dt.Concepto_negocio.Valor_caracteristica_concepto_negocio
                                             .Any(vccn => idsValoresDeCaracteristicas.Contains(vccn.id_valor_caracteristica))
                                      )
                .Select(
                      riv => new Reporte_Inventario_Valorizado()
                      {
                          IdConceptoNegocio = riv.id_concepto_negocio,
                          Producto = riv.Concepto_negocio.nombre,
                          Lote = riv.lote,
                          Cantidad = riv.cantidad,
                          CostoUnitario = riv.precio_unitario,
                          PrecioVigente = riv.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idCentroAtencionPrecios && p.es_vigente && p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal).OrderByDescending(p => p.id).FirstOrDefault(),
                          ValorCaracteristicasConceptoNegocio = riv.Concepto_negocio.Valor_caracteristica_concepto_negocio.Where(w => idsValoresDeCaracteristicas.Contains(w.id_valor_caracteristica)).Distinct()
                      }
                   );
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Dictionary<long, long> ObtenerIdsInventarioActualPorAlmacen
                ()
        {
            try
            {
                   /*
                    * Retornar el id_actor_negocio_interno y el id de transaccion
                    */
                   SigescomEntities _db = new SigescomEntities();
                Dictionary<long, long> idsAlmacenIdsInventarioFisico = new Dictionary<long, long>();
                var transacciones = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual &&
                                                               t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(t => new { IdAlmacen = t.id_actor_negocio_interno, IdInventarioActual = t.id }).ToList();
                foreach (var transaccion in transacciones)
                {
                    idsAlmacenIdsInventarioFisico.Add(transaccion.IdAlmacen, transaccion.IdInventarioActual);
                }
                return idsAlmacenIdsInventarioFisico;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener los ids de almacen e ids de inventario fisico", e);
            }
        }
    }
}
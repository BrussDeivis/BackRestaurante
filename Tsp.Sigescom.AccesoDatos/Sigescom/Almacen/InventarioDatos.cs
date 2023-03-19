using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;

namespace Tsp.Sigescom.AccesoDatos.Almacen

{
    public partial class InventarioDatos: IInventarioRepositorio
    {
        public IEnumerable<VencimientoConceptoNegocio> ObtenerVencimientoConceptosIngresados(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var detalles = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.id_actor_negocio_interno== idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => dt.vencimiento >= fechaDesde && dt.vencimiento <= fechaHasta)
                                .Select(dt => new VencimientoConceptoNegocio()
                                {
                                    IdConcepto = dt.id_concepto_negocio,
                                    CodigoBarra = dt.Concepto_negocio.codigo_barra,
                                    Concepto = dt.Concepto_negocio.nombre,
                                    UnidadMedida= dt.Concepto_negocio.Detalle_maestro.codigo,
                                    Lote = dt.lote,
                                    FechaVencimiento = (DateTime)dt.vencimiento

                                }
                                ).Distinct();
                return detalles;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener el vencimiento de conceptos ingresados", e); }
        }

        public IEnumerable<VencimientoConceptoNegocio> ObtenerVencimientoConceptosIngresados(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var detalles = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .SelectMany(t => t.Detalle_transaccion)
                                .Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico) && dt.vencimiento >= fechaDesde && dt.vencimiento <= fechaHasta)
                                .Select(dt => new VencimientoConceptoNegocio()
                                {
                                    IdConcepto = dt.id_concepto_negocio,
                                    CodigoBarra = dt.Concepto_negocio.codigo_barra,
                                    Concepto = dt.Concepto_negocio.nombre,
                                    UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                                    Lote = dt.lote,
                                    FechaVencimiento = (DateTime)dt.vencimiento
                                }
                                ).Distinct();
                return detalles;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener el vencimiento de conceptos ingresados", e); }
        }

        public InventarioFisico ObtenerInventario(long idInventario, int idConcepto, string lote)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto && dt.lote == lote).Select(dt => new InventarioFisico()
            {
                CodigoBarra = dt.Concepto_negocio.codigo_barra,
                Concepto = dt.Concepto_negocio.nombre,
                Cantidad = dt.cantidad,
                Familia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                Lote = dt.lote,
                UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo
            }
            ).SingleOrDefault();
            return inventario;
        }
        public InventarioValorizado ObtenerInventarioValorizado(long idInventario, int idConcepto, string lote)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto && dt.lote == lote).Select(dt => new InventarioValorizado()
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
            ).SingleOrDefault();
            return inventario;
        }

        public InventarioFisico ObtenerInventarioFisico(long idInventario, int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id == idInventario).SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConcepto).GroupBy(dt => new { dt.Concepto_negocio.codigo_barra, dt.Concepto_negocio.nombre }).Select(dt => new InventarioFisico()
            {
                CodigoBarra = dt.Key.codigo_barra,
                Concepto = dt.Key.nombre,
                Cantidad = dt.Sum(t => t.cantidad),
                Familia = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                UnidadMedida = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro.codigo
            }
            ).SingleOrDefault();
            return inventario;
        }

        /// <summary>
        /// devuelve el inventario valorizado de un concepto, tomando en cuenta todos sus lotes
        /// </summary>
        /// <param name="idInventario"></param>
        /// <param name="idConcepto"></param>
        /// <returns></returns>
        public InventarioValorizado ObtenerInventarioValorizado(long idInventario, int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.Where(t => t.id == idInventario).SelectMany(t=> t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConcepto).GroupBy(dt => new { dt.Concepto_negocio.codigo_barra, dt.Concepto_negocio.nombre }).Select(dt => new InventarioValorizado()
            {
                CodigoBarra = dt.Key.codigo_barra,
                Concepto = dt.Key.nombre,
                Cantidad = dt.Sum(t=> t.cantidad),
                Familia = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                UnidadMedida = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro.codigo,
                ValorTotal = dt.Sum(t=> t.total),
                ValorUnitario = dt.FirstOrDefault().precio_unitario
            }
            ).SingleOrDefault();
            return inventario;
        }
        public IEnumerable<InventarioFisico> ObtenerInventarioFisico(long idInventario)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id==idInventario).Detalle_transaccion.Where(dt => dt.Concepto_negocio.es_vigente || dt.Concepto_negocio.fecha_baja > dt.Transaccion.fecha_inicio).Select(dt => new InventarioFisico()
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
        public IEnumerable<InventarioValorizado> ObtenerInventarioValorizado(long idInventario)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => dt.Concepto_negocio.es_vigente || dt.Concepto_negocio.fecha_baja > dt.Transaccion.fecha_inicio).Select(dt => new InventarioValorizado()
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
        public IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforo(long idInventario)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => dt.Concepto_negocio.es_vigente || dt.Concepto_negocio.fecha_baja > dt.Transaccion.fecha_inicio).Select(dt => new InventarioSemaforo()
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
        
        public IEnumerable<InventarioFisico> ObtenerInventarioFisico(long idInventario, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico) && dt.Concepto_negocio.es_vigente || dt.Concepto_negocio.fecha_baja > dt.Transaccion.fecha_inicio).Select(dt => new InventarioFisico()
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
        public IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforo(long idInventario, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)&& dt.Concepto_negocio.es_vigente || dt.Concepto_negocio.fecha_baja > dt.Transaccion.fecha_inicio).Select(dt => new InventarioSemaforo()
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
        public IEnumerable<InventarioValorizado> ObtenerInventarioValorizado(long idInventario, int[] familias)
        {
            SigescomEntities _db = new SigescomEntities();
            var inventario = _db.Transaccion.SingleOrDefault(t => t.id == idInventario).Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioValorizado()
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




    }
}
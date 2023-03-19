using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;

namespace Tsp.Sigescom.AccesoDatos.Transacciones
{
    public partial class ConsultaTransaccion_Datos : IConsultaTransaccion_Repositorio
    {
        public DateTime? ObtenerFechaPrimeraTransaccion(int idAlmacen)
        {
            SigescomEntities _db = new SigescomEntities();
            var result = _db.Transaccion.OrderBy(t => t.fecha_inicio).FirstOrDefault(t => t.id_actor_negocio_interno == idAlmacen);
            return result != null ? (DateTime?)result.fecha_inicio : null;
        }


        public IEnumerable<Transaccion> ObtenerTransaccionesSegunTipoYConTipoTransaccionPadreDiferenteA(int idTipoTransaccion, int idTipoTransaccionPadre)
        {
            SigescomEntities _db = new SigescomEntities();
            var result = _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.id_transaccion_padre != null && t.Transaccion2.id_tipo_transaccion != idTipoTransaccionPadre);
            return result;
        }
        public IEnumerable<Transaccion> ObtenerTransaccionesPadre(long[] idsTransaccionesHijas)
        {
            SigescomEntities _db = new SigescomEntities();
            var result = _db.Transaccion.Where(t => idsTransaccionesHijas.Contains(t.id)).Select(t => t.Transaccion2);
            return result;
        }
        public IEnumerable<TransaccionPorSerieDeComprobanteYCategoria> ObtenerTransaccionesPorSerieDeComprobanteYCategoria(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var ventasPorFamilia = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && t.id_estado_actual == idEstadoActual && idsPuntosDeVentas.Contains(t.id_actor_negocio_interno))
                .SelectMany(t => t.Detalle_transaccion).Select(dt => new
                {
                    IdTipoComprobante = dt.Transaccion.Comprobante.id_tipo_comprobante,
                    NombreCortoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.valor,
                    Serie = dt.Transaccion.Comprobante.numero_serie,
                    IdFamilia = dt.Concepto_negocio.id_concepto_basico,
                    Cantidad = dt.cantidad,
                    Importe = dt.total
                }).ToList();
                var idsFamiliaDeVentas = ventasPorFamilia.Select(v => v.IdFamilia).ToList();
                var familiasConCategoria = _db.Detalle_maestro.Where(dm => idsFamiliaDeVentas.Contains(dm.id)).SelectMany(d => d.Categoria_concepto).Select(cc => new
                {
                    IdFamilia = cc.Detalle_maestro.id,
                    IdCategoria = cc.Detalle_maestro1.id,
                    Categoria = cc.Detalle_maestro1.nombre
                }).ToList();
                var resultado = ventasPorFamilia.Select(v => new
                {
                    v.IdTipoComprobante,
                    v.NombreCortoComprobante,
                    v.Serie,
                    v.Cantidad,
                    v.Importe,
                    IdCategoria = familiasConCategoria.SingleOrDefault(f => f.IdFamilia == v.IdFamilia) == null ? 0 : familiasConCategoria.Single(f => f.IdFamilia == v.IdFamilia).IdCategoria,
                    Categoria = familiasConCategoria.SingleOrDefault(f => f.IdFamilia == v.IdFamilia) == null ? "NN" : familiasConCategoria.Single(f => f.IdFamilia == v.IdFamilia).Categoria,
                }).GroupBy(v => new
                {
                    v.IdTipoComprobante,
                    v.NombreCortoComprobante,
                    v.Serie,
                    v.IdCategoria,
                    v.Categoria
                }).Select(vf => new TransaccionPorSerieDeComprobanteYCategoria
                {
                    IdCategoria = vf.Key.IdCategoria,
                    Categoria = vf.Key.Categoria,
                    IdTipoComprobante = vf.Key.IdTipoComprobante,
                    NombreCortoComprobante = vf.Key.NombreCortoComprobante,
                    Serie = vf.Key.Serie,
                    Cantidad = vf.Sum(f => f.Cantidad),
                    Importe = vf.Sum(f => f.Importe),
                });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones por serie de comprobante y categoria", e);

            }
        }
        public IEnumerable<TransaccionPorSerieDeComprobanteYConcepto> ObtenerTransaccionesPorSerieDeComprobanteYConcepto(int[] idsSeries, int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && t.id_estado_actual == idEstadoActual && idsSeries.Contains((int)t.Comprobante.id_serie_comprobante))
                .SelectMany(t => t.Detalle_transaccion)
                .GroupBy(dt => new
                {
                    dt.Transaccion.Comprobante.id_tipo_comprobante,
                    dt.Transaccion.Comprobante.Detalle_maestro.valor,
                    dt.Transaccion.Comprobante.numero_serie,
                    IdConcepto = dt.Concepto_negocio.id,
                    NombreConcepto = dt.Concepto_negocio.nombre
                }).Select(g => new TransaccionPorSerieDeComprobanteYConcepto
                {
                    IdTipoComprobante = g.Key.id_tipo_comprobante,
                    NombreCortoComprobante = g.Key.valor,
                    Serie = g.Key.numero_serie,
                    IdConcepto = g.Key.IdConcepto,
                    Concepto = g.Key.NombreConcepto,
                    Cantidad = g.Sum(c => c.cantidad),
                    Importe = g.Sum(t => t.total)
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones por serie de comprobante y concepto", e);

            }
        }
    }
}
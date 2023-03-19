using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class LibrosElectronicosConcarDatos : ILibrosElectronicosConcarRepositorio
    {
        public IEnumerable<OperacionVenta> ObtenerOperacionesVenta(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                            .Select(t => new OperacionVenta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                FechaDocumento = t.fecha_inicio,
                                FechaVencimiento = t.fecha_fin,
                                IdMoneda = t.id_moneda,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                NumeroDocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                TipoCambio = t.tipo_cambio,
                                TotalBien = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Sum(dt => dt.total),
                                IgvBien = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Sum(dt => dt.igv),
                                TotalServicio = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "0").Sum(dt => dt.total),
                                IgvServicio = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "0").Sum(dt => dt.igv),
                                Icbper = t.importe10,
                                IdTipoTransaccion = (int)t.id_tipo_transaccion,
                                NombreTipoTransaccionWrapper = t.Tipo_transaccion.Tipo_transaccion2.nombre,
                                IdEstado = (int)t.id_estado_actual,

                                IdTipoComprobanteReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.Comprobante.id_tipo_comprobante,
                                SerieComprobanteReferencia = t.Transaccion3 == null ? null : t.Transaccion3.Comprobante.numero_serie,
                                NumeroComprobanteReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.Comprobante.numero,
                                FechaEmisionReferencia = t.Transaccion3 == null ? new DateTime() : t.Transaccion3.fecha_inicio,
                                FechaVencimientoReferencia = t.Transaccion3 == null ? new DateTime() : t.Transaccion3.fecha_fin,
                                ImporteTotalReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.importe_total,
                                IgvReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.importe8,
                                NombreTipoTransaccionReferenciaWrapper = t.Transaccion3.Tipo_transaccion.Tipo_transaccion2.nombre,
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<OperacionVenta> ObtenerOperacionesIngresoDineroPorVenta(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var PagoCuotas = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)).SelectMany(t => t.Pago_cuota).Select(pc => new { IdCuota = pc.id_cuota, MontoPago = pc.importe, FechaEmision = pc.Transaccion.fecha_inicio }).ToList();
                var IdCuotas = PagoCuotas.Select(cp => cp.IdCuota).ToList();
                var TransaccionOrdenMontoPagos = _db.Cuota.Where(c => IdCuotas.Contains(c.id)).Select(c => new { IdCuota = c.id, IdTransaccionOrden = c.Transaccion.id, }).ToList();
                var IdTransacciones = TransaccionOrdenMontoPagos.Select(tomp => tomp.IdTransaccionOrden).ToList();
                var resultado = _db.Transaccion.Where(t => IdTransacciones.Contains(t.id))
                            .Select(t => new OperacionVenta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                FechaDocumento = t.fecha_inicio,
                                FechaVencimiento = t.fecha_fin,
                                IdMoneda = t.id_moneda,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                NumeroDocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                TipoCambio = t.tipo_cambio,
                                MontoTotalPago = 0,
                                IdTipoTransaccion = (int)t.id_tipo_transaccion,
                                NombreTipoTransaccionWrapper = t.Tipo_transaccion.Tipo_transaccion2.nombre,
                                IdEstado = (int)t.id_estado_actual,

                                IdTipoComprobanteReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.Comprobante.id_tipo_comprobante,
                                SerieComprobanteReferencia = t.Transaccion3 == null ? null : t.Transaccion3.Comprobante.numero_serie,
                                NumeroComprobanteReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.Comprobante.numero,
                                FechaEmisionReferencia = t.Transaccion3 == null ? new DateTime() : t.Transaccion3.fecha_inicio,
                                FechaVencimientoReferencia = t.Transaccion3 == null ? new DateTime() : t.Transaccion3.fecha_fin,
                                ImporteTotalReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.importe_total,
                                IgvReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.importe8,
                                NombreTipoTransaccionReferenciaWrapper = t.Transaccion3.Tipo_transaccion.Tipo_transaccion2.nombre,
                            }).ToList();
                resultado.ForEach(r => r.FechaEmision = PagoCuotas.FirstOrDefault(pc => pc.IdCuota == TransaccionOrdenMontoPagos.FirstOrDefault(tomp => tomp.IdTransaccionOrden == r.Id).IdCuota).FechaEmision);
                resultado.ForEach(r => r.MontoTotalPago = PagoCuotas.FirstOrDefault(pc => pc.IdCuota == TransaccionOrdenMontoPagos.FirstOrDefault(tomp => tomp.IdTransaccionOrden == r.Id).IdCuota).MontoPago);
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<RegistroCliente> ObtenerRegistroClientes(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)).Select(t => t.Actor_negocio1).Distinct()
                            .Select(an => new RegistroCliente()
                            {
                                Id = an.id,
                                NumeroDocumento = an.Actor.numero_documento_identidad,
                                Nombre = an.Actor.primer_nombre,
                                Direccion = an.Actor.Direccion.FirstOrDefault(d => d.es_activo).detalle
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

    }
}
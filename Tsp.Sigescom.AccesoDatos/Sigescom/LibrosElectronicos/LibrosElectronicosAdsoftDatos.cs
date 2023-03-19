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
    public partial class LibrosElectronicosAdsoftDatos : ILibrosElectronicosAdsoftRepositorio
    {
        public IEnumerable<VentaClienteAdsoft> ObtenerVentasCliente(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                            .Select(t => new VentaClienteAdsoft()
                            {
                                Id = t.id,
                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                IdTipoTransaccion = t.id_tipo_transaccion,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                ValorIgv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorVenta = t.importe_total - t.importe8 - t.importe10,
                                Total = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo

                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<VentaClienteAdsoft> ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                            .Select(t => new VentaClienteAdsoft()
                            {
                                Id = t.id,
                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                ValorIgv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorVenta = t.importe_total - t.importe8 - t.importe10,
                                Total = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo,
                                NumeroSerieReferencia = t.Transaccion3.Comprobante.numero_serie,
                                NumeroComprobanteReferencia = t.Transaccion3.Comprobante.numero,
                                FechaEmisionReferencia = t.Transaccion3.fecha_inicio,
                                CodigoComprobanteReferencia = t.Transaccion3.Comprobante.Detalle_maestro.codigo
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
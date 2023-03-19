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
    public partial class LibrosElectronicosFoxcontDatos : ILibrosElectronicosFoxcontRepositorio
    {
        public IEnumerable<VentaClienteFoxcom> ObtenerVentasClienteFoxcom(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                    .Select(t => new VentaClienteFoxcom()
                    {
                        Id = t.id,
                        FechaEmision = t.fecha_inicio,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                        NumeroSerie = t.Comprobante.numero_serie,
                        NumeroComprobante = t.Comprobante.numero,
                        IdActorNegocioExterno = t.id_actor_negocio_externo,
                        CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                        NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                        RazonSocial = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        Igv = t.importe8,
                        Icbper = t.importe10,
                        ValorVenta = t.importe_total - t.importe8 - t.importe10,
                        Total = t.importe_total,
                        IdEstadoActual = (int)t.id_estado_actual,
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }
        public IEnumerable<VentaClienteFoxcom> ObtenerVentasClienteFoxcomConOperacionReferencia(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                    .Select(t => new VentaClienteFoxcom()
                    {
                        Id = t.id,
                        FechaEmision = t.fecha_inicio,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                        NumeroSerie = t.Comprobante.numero_serie,
                        NumeroComprobante = t.Comprobante.numero,
                        IdActorNegocioExterno = t.id_actor_negocio_externo,
                        CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                        NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                        RazonSocial = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        Igv = t.importe8,
                        Icbper = t.importe10,
                        ValorVenta = t.importe_total - t.importe8 - t.importe10,
                        Total = t.importe_total,
                        IdEstadoActual = (int)t.id_estado_actual,
                        CodigoComprobanteReferencia = t.Transaccion3.Comprobante.Detalle_maestro.codigo,
                        NumeroSerieReferencia = t.Transaccion3.Comprobante.numero_serie,
                        NumeroComprobanteReferencia = t.Transaccion3.Comprobante.numero,
                        FechaEmisionReferencia = t.Transaccion3.fecha_inicio,
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }
    }
}
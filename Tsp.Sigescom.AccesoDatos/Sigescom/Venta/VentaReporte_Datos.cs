using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Venta;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class VentaReporte_Datos : IVentaReporte_Repositorio
    {
        public IEnumerable<OperacionFamiliaGrupo> ObtenerVentasPorFamiliasGrupos(int[] idsTipoTransaccion, int idEstado, int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int[] idsFamilias, int[] idsGrupos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Detalle_transaccion.Where(dt => dt.Transaccion.fecha_inicio >= fechaDesde
                                                        && dt.Transaccion.fecha_inicio <= fechaHasta
                                                        && dt.Transaccion.id_actor_negocio_interno == idPuntoVenta
                                                        && idsTipoTransaccion.Contains(dt.Transaccion.id_tipo_transaccion)
                                                        && dt.Transaccion.id_estado_actual == idEstado
                                                        && idsFamilias.Contains((int)dt.Concepto_negocio.id_concepto_basico)
                                                        && idsGrupos.Contains((int)dt.Transaccion.id_actor_negocio_externo1))
                   .Select(dt => new OperacionFamiliaGrupo()
                   {
                       IdTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                       IdGrupo = (int)dt.Transaccion.id_actor_negocio_externo1,
                       NombreGrupo = dt.Transaccion.Actor_negocio3.Actor.primer_nombre,
                       IdFamilia = dt.Concepto_negocio.id_concepto_basico,
                       NombreFamilia = dt.Concepto_negocio.Detalle_maestro4.nombre,
                       Importe = dt.total
                   });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de ventas por familias grupos", e);
            }
        }
        public IEnumerable<OperacionGrupo> ObtenerVentasPorGrupos(int[] idsTipoTransaccion, int idEstado, int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int[] idsGrupos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde
                                                        && t.fecha_inicio <= fechaHasta
                                                        && t.id_actor_negocio_interno == idPuntoVenta
                                                        && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                                                        && t.id_estado_actual == idEstado
                                                        && idsGrupos.Contains((int)t.id_actor_negocio_externo1))
                   .Select(t => new OperacionGrupo()
                   {
                       IdTipoTransaccion = t.id_tipo_transaccion,
                       IdGrupo = (int)t.id_actor_negocio_externo1,
                       NombreGrupo = t.Actor_negocio3.Actor.primer_nombre,
                       DocumentoResponsable = t.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)t.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.numero_documento_identidad,
                       NombreResponsable = t.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)t.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                       IdCliente = t.id_actor_negocio_externo,
                       NombreCliente = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       IdComprobante = t.id_comprobante,
                       InfoComprobante = "[(" + t.Comprobante.Detalle_maestro.valor + ") " + t.Comprobante.numero_serie + "-" + t.Comprobante.numero + "]",
                       Importe = t.importe_total,
                   });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de ventas por grupos", e);
            }
        }

        public IEnumerable<OperacionGrupoDetallado> ObtenerVentasPorGrupoDetallado(int[] idsTipoTransaccion, int idEstado, int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int idGrupo)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde
                                                        && t.fecha_inicio <= fechaHasta
                                                        && t.id_actor_negocio_interno == idPuntoVenta
                                                        && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                                                        && t.id_estado_actual == idEstado
                                                        && (int)t.id_actor_negocio_externo1 == idGrupo)
                   .Select(t => new OperacionGrupoDetallado()
                   {
                       Id = t.id,
                       IdTipoTransaccion = t.id_tipo_transaccion,
                       NombreResponsable = t.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)t.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                       DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                       NombreCliente = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       Emision = t.fecha_inicio,
                       TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Importe = t.importe_total

                   });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de ventas por grupo detallado", e);
            }
        }




    }
}
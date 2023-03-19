using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Pedido;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel;

namespace Tsp.Sigescom.AccesoDatos.Sigescom.Pedido { 
    public class Pedido_Datos:RepositorioBase,IPedidoRepositorio
    {
        public IEnumerable<ResumenOrdenPedido> ObtenerOrdenesPedidos(DateTime fechaDesde, DateTime fechaHasta, int idTipoTransaccion, int[] idEstados)
        {
            var resumen = _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idEstados.Contains((int)t.id_estado_actual)).Select(rop => new ResumenOrdenPedido()
            { 
                DocumentoCliente = rop.Actor_negocio1.Actor.numero_documento_identidad,
                NombreCliente = rop.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                Comprobante = rop.Comprobante.numero_serie + "-" + rop.Comprobante.numero,
                Alias =  rop.Parametro_transaccion.FirstOrDefault(pp => pp.id_parametro==MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? rop.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                Estado = rop.Detalle_maestro.nombre,
                Vendedor = rop.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                FechaInicio = rop.fecha_inicio,
                Id = rop.id,
                TipoComprobante = rop.Comprobante.Detalle_maestro.valor,
                IdEstado = (int)rop.id_estado_actual,
                Total=rop.importe_total.ToString(),
            }).OrderByDescending(lp=>lp.FechaInicio);

            return resumen;
        }

        public Transaccion ObtenerOrdenDePedido(int idOrdenPedido)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Comprobante.Detalle_maestro).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio2).
                    SingleOrDefault(t => t.id == idOrdenPedido);
            }
            catch (Exception ex)
            {
                throw new DatosException("Error al obtener Pedido", ex);
            }
        }
        public Transaccion ObtenerTransaccion(long id)
        {
            try
            {
                return _db.Transaccion.SingleOrDefault(st => st.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public IEnumerable<PedidosInvalidados> ObtenerOrdenesDePedidoInvalidados(DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosVenta)
        {
            var resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == PedidoSettings.Default.IdTipoTransaccionOrdenPedido && t.id_estado_actual==MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado && idsPuntosVenta.Contains(t.id_actor_negocio_interno)).Select(rop => new PedidosInvalidados()
            {
                DocumentoCliente = rop.Actor_negocio1.Actor.numero_documento_identidad,
                NombreCliente = rop.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                Comprobante = rop.Comprobante.numero_serie + "-" + rop.Comprobante.numero,
                Alias = rop.Parametro_transaccion.FirstOrDefault(pp => pp.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? rop.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                Vendedor = rop.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                FechaEmision = rop.fecha_inicio,
                Id = rop.id,
                IdEstado = (int)rop.id_estado_actual,
                FechaInvalidacion = rop.Estado_transaccion.FirstOrDefault(et=>et.id_estado==MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).fecha,
                Observacion = rop.Estado_transaccion.FirstOrDefault(et=>et.id_estado==MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).comentario,
                Total = rop.importe_total,
            }).OrderBy(lp => lp.FechaInvalidacion);

            return resumen;
        }
    }
}

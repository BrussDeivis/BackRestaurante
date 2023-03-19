using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Operacion
    {
        private Transaccion transaccion;

        public Operacion(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public List<Venta> ObtenerVentas(DateTime FechaInicio, DateTime FechaFin)
        {
            return Venta.Convert(transaccion.Transaccion11.Where(t => t.fecha_inicio > FechaInicio && t.fecha_inicio > FechaFin
            && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionVenta).ToList());
        }

        public OrdenDeOperacion ObtenerOrdenDeOperacion()
        {
            return new OrdenDeOperacion(transaccion.Transaccion1.Single(t => Diccionario.IdsTiposDeTransaccionOrdenesDeOperaciones.Contains(t.id_tipo_transaccion) && t.Estado_transaccion.First().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado));
        }

        public long Id
        {
            get { return this.transaccion.id; }
        }
        public int IdUnidadNegocio
        {
            get { return this.transaccion.id_unidad_negocio; }
        }
        public string Codigo
        {
            get { return this.transaccion.codigo; }
        }
        public int IdActorNegocioExterno
        {
            get { return this.transaccion.id_actor_negocio_externo; }
        }

        public long IdComprobante
        {
            get { return this.transaccion.id_comprobante; }
        }
        public decimal ImporteTotal
        {
            get { return this.transaccion.importe_total; }
        }
    }

    public class OrdenDeOperacion
    {
        private Transaccion transaccion;

        public OrdenDeOperacion(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public long Id
        {
            get { return this.transaccion.id; }
        }
    }
}

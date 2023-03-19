using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Compra
    {
        private readonly Transaccion transaccion;

        public Compra(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public Operacion OperacionGenerica()
        {
            return new Operacion(this.transaccion.Transaccion2);
        }
        public OrdenDeCompra OrdenDeCompra()
        {
            return new OrdenDeCompra(this.transaccion.Transaccion1.Single(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra && t.EstadoActual.id != MaestroSettings.Default.IdDetalleMaestroEstadoEditado));
        }
        public List<MovimientoEconomico> ObtenerPagos()
        {
            return MovimientoEconomico.Convert(this.transaccion.Transaccion1.Where(t=>t.id_tipo_transaccion==TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores).ToList());
        }
        public List<IngresoDeAlmacen> ObtenerIngresoDeMercaderia()
        {
            return IngresoDeAlmacen.Convertir(this.transaccion.Transaccion1.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList());
        }
        public long Id
        {
            get { return this.transaccion.id; }
        }
        public string Codigo
        {
            get { return this.transaccion.codigo; }
        }
        public DateTime FechaRegistro()
        {
            return this.transaccion.fecha_registro_sistema;
        }
        public int IdComprador()
        {
            return this.transaccion.id_empleado;
        }
        public int Cliente()
        {
            return this.transaccion.id_actor_negocio_externo;
        }
        public Transaccion Transaccion()
        {
            return this.transaccion;
        }
        public static List<Compra> Convert(List<Transaccion> transacciones)
        {
            List<Compra> compras = new List<Compra>();
            foreach (var transaccion in transacciones)
            {
                compras.Add(new Compra(transaccion));
            }
            return compras;
        }
    }
}
     
   

    


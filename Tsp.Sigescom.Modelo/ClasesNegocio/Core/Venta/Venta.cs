using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Venta
    {
        private readonly Transaccion transaccion;

        public Venta(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public Operacion OperacionGenerica()
        {
            return new Operacion(this.transaccion.Transaccion2);
        }
        public OrdenDeVenta OrdenDeVenta()
        {
            return new OrdenDeVenta(this.transaccion.Transaccion1.Single(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta));
        }
        public List<MovimientoEconomico> ObtenerPagos()
        {
            List<Transaccion> transacciones = this.transaccion.Transaccion1.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes).ToList();
            return transacciones != null ? MovimientoEconomico.Convert(transacciones) : null;
        }
        public List<SalidaDeAlmacen> ObtenerSalidasDeMercaderia()
        {
            return SalidaDeAlmacen.Convertir(this.transaccion.Transaccion1.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList());
        }
        public long Id
        {
            get { return this.transaccion.id; }
        }
        public string Codigo
        {
            get { return this.transaccion.codigo; }
        }
        public long IdTransaccionPadre
        {
            get { return (int)this.transaccion.id_transaccion_padre; }
        }
        public DateTime FechaRegistro()
        {
            return this.transaccion.fecha_registro_sistema;
        }
        public int Vendedor()
        {
            return this.transaccion.id_empleado;
        }
        public int IdCliente()
        {
            return this.transaccion.id_actor_negocio_externo;
        }
        public Cliente Cliente()
        {
            return new Cliente(this.transaccion.Actor_negocio1);//Actor_Negocio => Empleado, Actor_Negocio1 => Cliente , Actor_Negocio2 => Entidad Interna
        }
        public Comprobante Comprobante()
        {
            return this.transaccion.Comprobante;
        }
        public Transaccion Transaccion()
        {
            return this.transaccion;
        }
        public static List<Venta> Convert(List<Transaccion> transacciones)
        {
            List<Venta> ventas = new List<Venta>();
            foreach (var transaccion in transacciones)
            {
                ventas.Add(new Venta(transaccion));
            }
            return ventas;
        }
       
    }
}
     
   

    


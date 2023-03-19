using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class VentaCobranzaEnBloque
    {
        private readonly Transaccion transaccion;

        public VentaCobranzaEnBloque(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public Operacion OperacionGenerica()
        {
            return new Operacion(this.transaccion.Transaccion2);
        }
        public List<Venta> Ventas()
        {
            return  Venta.Convert(this.transaccion.Transaccion1.Where( t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionVenta).ToList());
        }

        public List<MovimientoEconomico> Cobros()
        {
            return MovimientoEconomico.Convert(this.transaccion.Transaccion1.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes).ToList());
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
        public int Vendedor()
        {
            return this.transaccion.id_empleado;
        }


        public Transaccion Transaccion()
        {
            return this.transaccion;
        }
        public static List<VentaCobranzaEnBloque> Convert(List<Transaccion> transacciones)
        {
            List<VentaCobranzaEnBloque> ventasEnBloque = new List<VentaCobranzaEnBloque>();
            foreach (var transaccion in transacciones)
            {
                ventasEnBloque.Add(new VentaCobranzaEnBloque(transaccion));
            }
            return ventasEnBloque;
        }

    }
}

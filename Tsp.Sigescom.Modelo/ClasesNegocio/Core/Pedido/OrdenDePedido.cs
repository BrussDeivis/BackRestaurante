using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido
{
    public class OrdenDePedido: OperacionDeVenta
    {
        public OrdenDePedido()
        {

        }
        public OrdenDePedido(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public static List<OrdenDePedido> Convert(List<Transaccion> transacciones)
        {
            List<OrdenDePedido> ordenesDeCotizacion = new List<OrdenDePedido>();
            foreach (var transaccion in transacciones)
            {
                ordenesDeCotizacion.Add(new OrdenDePedido(transaccion));
            }
            return ordenesDeCotizacion;
        }

        public long IdPedido
        {
            get { return (long)this.transaccion.id_transaccion_padre; }
        }
    }
}

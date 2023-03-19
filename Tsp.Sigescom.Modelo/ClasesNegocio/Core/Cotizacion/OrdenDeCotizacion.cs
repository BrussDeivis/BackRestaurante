using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeCotizacion : OperacionDeVenta
    {
        public OrdenDeCotizacion()
        {

        }

        public OrdenDeCotizacion(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public static List<OrdenDeCotizacion> Convert(List<Transaccion> transacciones)
        {
            List<OrdenDeCotizacion> ordenesDeCotizacion = new List<OrdenDeCotizacion>();
            foreach (var transaccion in transacciones)
            {
                ordenesDeCotizacion.Add(new OrdenDeCotizacion(transaccion));
            }
            return ordenesDeCotizacion;
        }

        public long IdCotizacion
        {
            get { return (long)this.transaccion.id_transaccion_padre; }
        }

        public bool ConvertidoAVenta
        {
            get { return this.transaccion.indicador2; }
        }
    }
}

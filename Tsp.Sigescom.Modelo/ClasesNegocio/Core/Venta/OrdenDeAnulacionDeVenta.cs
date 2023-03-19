using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeAnulacionDeVenta : OperacionDeVenta
    {
        public OrdenDeAnulacionDeVenta()
        {

        }
        public OrdenDeAnulacionDeVenta(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public static List<OrdenDeAnulacionDeVenta> Convert_(List<Transaccion> transaciones)
        {
            List<OrdenDeAnulacionDeVenta> ordenes = new List<OrdenDeAnulacionDeVenta>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeAnulacionDeVenta(transaccion));
            }
            return ordenes;
        }
    }
}

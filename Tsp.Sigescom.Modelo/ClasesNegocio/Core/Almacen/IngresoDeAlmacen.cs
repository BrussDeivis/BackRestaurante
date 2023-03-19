using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class IngresoDeAlmacen : FlujoDeAlmacen
    {
        public IngresoDeAlmacen()
        {

        }
        public IngresoDeAlmacen(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public static List<IngresoDeAlmacen> Convertir(List<Transaccion> transacciones)
        {
            List<IngresoDeAlmacen> ingresos = new List<IngresoDeAlmacen>();
            foreach (var transaccion in transacciones)
            {
                ingresos.Add(new IngresoDeAlmacen(transaccion));
            }
            return ingresos;
        }

    }
}
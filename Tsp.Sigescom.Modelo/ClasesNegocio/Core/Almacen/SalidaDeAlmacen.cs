using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class SalidaDeAlmacen : FlujoDeAlmacen
    {
        public SalidaDeAlmacen()
        {

        }
        public SalidaDeAlmacen(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public static List<SalidaDeAlmacen> Convertir(List<Transaccion> transacciones)
        {
            List<SalidaDeAlmacen> entregas = new List<SalidaDeAlmacen>();
            foreach(var transaccion in transacciones)
            {
                entregas.Add(new SalidaDeAlmacen(transaccion));
            }
            return entregas;
        }

    }
}

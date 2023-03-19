using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class Ultimo_Precio_Compra_Venta
    {
        public decimal? UltimoPrecioCompra { get; set; }
        public decimal? UltimoPrecioVenta { get; set; }

        public Ultimo_Precio_Compra_Venta()
        { }

        public static List<Ultimo_Precio_Compra_Venta> Convert()
        {
            return new List<Ultimo_Precio_Compra_Venta>();
        }
    }

}

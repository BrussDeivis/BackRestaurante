using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class ItemRestaurante_Consulta
    {
        public string Codigo { get; set; }
        public string NombreItem { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }


        public ItemRestaurante_Consulta(){}

        public static List<ItemRestaurante_Consulta> Convert()
        {
            return new List<ItemRestaurante_Consulta>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class ResumenOrdenesPorMozo_Consulta
    {
        public long Id { get; set; }
        public string Mozo { get; set; }
        /// <summary>
        /// Cantidad de órdenes
        /// </summary>
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }


        public ResumenOrdenesPorMozo_Consulta(){}

        public static List<ResumenOrdenesPorMozo_Consulta> Convert()
        {
            return new List<ResumenOrdenesPorMozo_Consulta>();
        }

    }
}

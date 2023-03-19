using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class ResumenOrden_Consulta
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public decimal Importe { get; set; }
        public string Mozo { get; set; }
        public DateTime Fecha { get; set; }

        public ResumenOrden_Consulta(){}

        public static List<ResumenOrden_Consulta> Convert()
        {
            return new List<ResumenOrden_Consulta>();
        }

    }
}

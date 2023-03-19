using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class DetalleOrden_Consulta
    {
        public DateTime FechaHora { get; set; }
        public string Codigo { get; set; }
        public string NombreItem { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }
        public string Mozo { get; set; }
        public string Mesa { get; set; }


        public DetalleOrden_Consulta(){}

        public static List<DetalleOrden_Consulta> Convert()
        {
            return new List<DetalleOrden_Consulta>();
        }

    }
}

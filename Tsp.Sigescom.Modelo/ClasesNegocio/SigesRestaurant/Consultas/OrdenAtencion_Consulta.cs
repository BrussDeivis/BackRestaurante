using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class OrdenAtencion_Consulta
    {
        public DateTime FechaHoraAtencion { get; set; }
        public long IdAtencion { get; set; }
        public string CodigoOrden { get; set; }
        public string Mesa { get; set; }
        public string Mozo { get; set; }
        public decimal Importe { get; set; }
        public bool Facturado { get; set; }
        public string Estado { get; set; }
        public bool EsAtencionEnSalon { get; set; }

        public OrdenAtencion_Consulta(){}

        public static List<OrdenAtencion_Consulta> Convert()
        {
            return new List<OrdenAtencion_Consulta>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class CobroAtencion
    {
        public long IdAtencion { get; set; }
        public string Ambiente { get; set; }
        public string Mesa { get; set; }
        public string Mozo { get; set; }
        public decimal Importe { get; set; }
        public bool EstaFacturado { get; set; }
        public bool EstaConfirmado { get; set; }

    }
}

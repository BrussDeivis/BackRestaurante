using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class ResumenAtencion
    {
        public long Id { get; set; }
        public string NombreAmbiente { get; set; }
        public int IdMesa { get; set; }
        public string NombreMesa { get; set; }
        public string NombreMozo { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Total { get => ImporteTotal.ToString("N2"); }
        public bool ImporteTotalCero { get => ImporteTotal == 0; }
        public bool Facturado { get; set; }
        public string EstaFacturado { get => Facturado ? "SI" : "NO"; }
        public bool Confirmado { get; set; }
        public string EstaConfirmado { get => Confirmado ? "SI" : "NO"; }
        public DateTime FechaAtencion { get; set; }
        public string FechaAtencionString { get => FechaAtencion.ToString("dd-MM-yyyy hh:mm tt"); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class AtencionHotel : EstadoAtencionHotel
    {
        public long Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string FechaIngresoString { get => FechaIngreso.ToString("dd/MM/yyyy"); }
        public DateTime FechaSalida { get; set; }
        public string FechaSalidaString { get => FechaSalida.ToString("dd/MM/yyyy"); }
        public int Noches { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public Habitacion Habitacion { get; set; }
        public IEnumerable<Huesped> Huespedes { get; set; }
        public string Anotacion { get; set; }
        public List<Anotacion> Anotaciones { get; set; }
        public string AnotacionesJson { get; set; }

        public AtencionHotel() { }
    }
}

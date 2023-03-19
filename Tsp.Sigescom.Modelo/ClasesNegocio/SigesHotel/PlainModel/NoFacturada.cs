using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class NoFacturada
    {
        public string Codigo { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public IEnumerable<Huesped> Huespedes { get; set; }
        public string Titular { get => Huespedes.Count() > 0 ? Huespedes.Single(h => h.EsTitular).Nombre : "-"; }
        public string Estado { get; set; }
        public decimal Importe { get; set; }

        public List<NoFacturada> Convert()
        {
            return new List<NoFacturada>();
        }
    }
}

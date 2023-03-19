using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Ingreso
    {
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<Huesped> Huespedes { get; set; }
        public string Titular { get => Huespedes?.Single(h => h.EsTitular).Nombre; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public int Noches { get; set; }

        public List<Ingreso> Convert()
        {
            return new List<Ingreso>();
        }
    }

}

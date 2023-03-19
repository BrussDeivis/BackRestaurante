using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Anulada
    {
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Empleado { get; set; }

        public List<Anulada> Convert()
        {
            return new List<Anulada>();
        }
    }
}

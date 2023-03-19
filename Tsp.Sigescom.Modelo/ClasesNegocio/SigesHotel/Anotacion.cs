using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Anotacion
    {
        public string Fecha { get; set; }
        public string Mensaje { get; set; }

        public Anotacion() { }
        public Anotacion(string fecha, string mensaje)
        {
            Fecha = fecha;
            Mensaje = mensaje;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class TipoHabitacionesBandeja
    {
        private List<Precio> precios;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Precio {
            get
            {
                string preciotemp = "";
                foreach (var precio in precios)
                {
                    preciotemp += precio.Detalle_maestro3.nombre + ": " + precio.valor+", ";
                }
                return preciotemp;
            }
        }
        public List<Precio> Precios { set => precios = value; }
        public string CapacidadNinio { get; set; }
        public string CapacidadAdulto { get; set; }
        public string Capacidad
        {
            get
            {
                return "Adulto: "+CapacidadAdulto+", Niños: "+CapacidadNinio;
            }
        }
        public bool EsVigente { get; set; }
    }
}

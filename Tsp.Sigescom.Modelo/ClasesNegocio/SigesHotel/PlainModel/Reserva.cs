using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Reserva
    {
        public string Codigo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public int Noches { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public bool EstaFacturado { get => ModoFacturacion != (int)ModoFacturacionHotel.NoEspecificado; }
        public decimal Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Responsable { get; set; }
        public int ModoFacturacion { get; set; }
        public List<Reserva> Convert()
        {
            return new List<Reserva>();
        }
    }
}

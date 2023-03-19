using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Salida
    {
        public DateTime FechaSalida { get; set; }
        public string Codigo { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public Estado_transaccion EstadoIngresoReferencia { get; set; }
        public Estado_transaccion EstadoIngreso { get; set; }
        public DateTime FechaIngreso { get => EstadoIngreso == null ? EstadoIngresoReferencia.fecha : EstadoIngreso.fecha; }
        public IEnumerable<Huesped> Huespedes { get; set; }
        public string Titular { get => Huespedes?.Single(h => h.EsTitular).Nombre; }
        public int Noches { get; set; }
        public decimal Importe { get; set; }

        public List<Salida> Convert()
        {
            return new List<Salida>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class ConsumoSimple
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaString { get => Fecha.ToString("dd/MM/yyyy hh:mm tt"); }
        public string Huesped { get; set; }
        public decimal Importe { get; set; }
        public List<DetalleConsumo> DetallesConsumo { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class ConsumoHabitacion
    {
        public long IdAtencion { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string FechaAtencion { get => FechaDesde.ToString("dd/MM/yyyy") + " - " + FechaHasta.ToString("dd/MM/yyyy"); }
        public string Titular { get; set; }
        public decimal Importe { get; set; }
        public List<ItemGenerico> Huespedes { get; set; }
        public ItemGenerico HuespedConsumo { get; set; }
        public List<ConsumoSimple> Consumos { get; set; }
        public List<DetalleDeOperacion> Detalles { get; set; }

    }

}

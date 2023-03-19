using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionTipoTransaccionTipoComprobante
    {
        public long IdOperacion { get; set; }
        public int IdTipoTransaccion { get; set; }
        public int IdTipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string Tercero { get; set; }
        public DateTime FechaInicio { get; set; }
        public string FechaEmision { get => FechaInicio.ToString("dd/MM/yyyy"); }
        public string Comprobante { get => SerieComprobante + " - " + NumeroComprobante.ToString(); }
    }
}

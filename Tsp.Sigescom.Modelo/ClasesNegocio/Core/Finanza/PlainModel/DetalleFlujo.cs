using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class DetalleFlujo
    {
        public DateTime Fecha { get; set; }
        public int IdOperacion { get; set; }
        public string Operacion { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public bool EsIngreso { get; set; }
        public decimal Importe { get; set; }
        public decimal Saldo { get; set; }

        public List<DetalleFlujo> Convert()
        {
            return new List<DetalleFlujo>();
        }

    }
}

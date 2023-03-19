using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class IngresoEgreso
    {
        public DateTime Fecha { get; set; }
        public int IdOperacion { get; set; }
        public string Operacion { get; set; }
        public int IdMedioPago { get; set; }
        public string MedioPago { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public string Informacion { get; set; }
        public decimal Importe { get; set; }

        public List<IngresoEgreso> Convert()
        {
            return new List<IngresoEgreso>();
        }

    }
}

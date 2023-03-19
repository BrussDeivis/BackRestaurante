using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class TransaccionPorSerieDeComprobanteYConcepto
    {
        public int IdTipoComprobante { get; set; }
        public string NombreCortoComprobante { get; set; }
        public string Serie { get; set; }
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public TransaccionPorSerieDeComprobanteYConcepto()
        {

        }

        public static List<TransaccionPorSerieDeComprobanteYConcepto> Convert()
        {
            return new List<TransaccionPorSerieDeComprobanteYConcepto>();
        }
    }
}

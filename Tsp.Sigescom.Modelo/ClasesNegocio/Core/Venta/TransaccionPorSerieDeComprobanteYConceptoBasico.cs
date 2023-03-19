using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class TransaccionPorSerieDeComprobanteYConceptoBasico
    {
        public int IdTipoComprobante { get; set; }
        public string NombreCortoComprobante { get; set; }
        public string Serie { get; set; }
        public string ConceptoBasico { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public TransaccionPorSerieDeComprobanteYConceptoBasico()
        {

        }

        public static List<TransaccionPorSerieDeComprobanteYConceptoBasico> Convert()
        {
            return new List<TransaccionPorSerieDeComprobanteYConceptoBasico>();
        }
    }
}

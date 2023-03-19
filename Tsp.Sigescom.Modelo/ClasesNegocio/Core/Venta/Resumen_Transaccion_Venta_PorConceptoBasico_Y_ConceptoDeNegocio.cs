using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]

    public class Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio
    {
        public string NombreBasico { get; set; }
        public string CodigoBarra { get; set; }
        public int IdConceptoNegocio { get; set; }

        public string ConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }

        //public decimal Cantidad { get { return CantidadQueSale??0 - CantidadQueEntra??0; } }
        //public decimal? CantidadQueEntra { get; set; }
        //public decimal? CantidadQueSale { get; set; }


        //public decimal Importe { get { return ImporteQueEntra??0 - ImporteQueSale??0; } }
        //public decimal? ImporteQueEntra { get; set; }
        //public decimal? ImporteQueSale { get; set; }

        public decimal PrecioUnitario
        {  get   { return Importe / (Cantidad!=0? Cantidad : 1); }}

        public Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio()
        {        }

        public static List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> Convert()
        {
            return new List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio>();
        }
    }
}

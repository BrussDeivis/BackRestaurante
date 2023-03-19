using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]

    public class Resumen_Transaccion_Por_Serie_y_Concepto_Negocio
    {
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public string NombreCortoDocumento { get; set; }
        public string CodigoBarra { get; set; }
        public string ConceptoNegocio { get; set; }
        public int IdConceptoNegocio { get; set; }
        public decimal Cantidad{ get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public Resumen_Transaccion_Por_Serie_y_Concepto_Negocio()
        { }

        public static List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> Convert()
        {
            return new List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio>();
        }


    }
}

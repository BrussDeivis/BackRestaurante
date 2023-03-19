using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]

    public class Resumen_Transaccion_Por_Serie_y_ConceptoBasico
    {
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public string NombreCortoDocumento { get; set; }
        public string ConceptoBasico { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }


        public Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
        { }

        public static List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> Convert()
        {
            return new List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico>();
        }

    }
}

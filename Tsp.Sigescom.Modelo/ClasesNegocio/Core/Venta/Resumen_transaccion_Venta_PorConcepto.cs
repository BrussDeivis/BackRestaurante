using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class Resumen_transaccion_Venta_PorConcepto
    {
        public int IdConceptoNegocio { get; set; }
        public string CodigoBarra { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }

        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }

        public Resumen_transaccion_Venta_PorConcepto()
        { }

        public static List<Resumen_transaccion_Venta_PorConcepto> Convert()
        {
            return new List<Resumen_transaccion_Venta_PorConcepto>();
        }
        //public decimal TotalDeGrupo;             
    }
}

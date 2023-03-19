using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]

    public class Resumen_Transaccion_Inventario_Fisico_Valorado
    {
        public string CodigoBarra { get; set; }
        public string ConceptoNegocio { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Cantidad{ get; set; }
        public decimal PrecioVenta
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public Resumen_Transaccion_Inventario_Fisico_Valorado()
        { }

        public static List<Resumen_Transaccion_Inventario_Fisico_Valorado> Convert()
        {
            return new List<Resumen_Transaccion_Inventario_Fisico_Valorado>();
        }


    }
}

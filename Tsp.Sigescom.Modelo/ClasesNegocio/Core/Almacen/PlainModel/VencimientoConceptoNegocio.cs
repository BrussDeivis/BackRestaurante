using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class VencimientoConceptoNegocio
    {
        public int IdConcepto { get; set; }
        public string CodigoBarra { get; set; }
        public string Concepto { get; set; }
        public string UnidadMedida { get; set; }

        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public VencimientoConceptoNegocio()
        {
        }

        public static List<VencimientoConceptoNegocio> Convert()
        {
            return new List<VencimientoConceptoNegocio>();
        }

    }
}

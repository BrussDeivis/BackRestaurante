using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ImporteConcepto
    {
        public ItemGenerico Concepto { get; set; }
        public decimal Importe { get; set; }




        public ImporteConcepto(Precio precio)
        {
            Concepto = new ItemGenerico {Id=precio.id_concepto_negocio };
            Importe = precio.valor;
        }

        public static List<ImporteConcepto> Convert(List<Precio> importes)
        {
            List<ImporteConcepto> importesConceptos = new List<ImporteConcepto>();
            foreach (var importe in importes)
            {
                importesConceptos.Add(new ImporteConcepto(importe));
            }
            return importesConceptos;
        }
    }
}

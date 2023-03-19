using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Resumen_Transaccion_Gasto_Por_Concepto
    {
        public DateTime Fecha { get; set; }
        public int IdConceptoNegocio { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public string Detalle { get; set; }
        public string ConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        //public decimal Pago { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }

        //public decimal Deuda
        //{
        //    get
        //    { return Importe - Pago; }
        //}

        public static List<Resumen_Transaccion_Gasto_Por_Concepto> Resumen(List<Resumen_Transaccion_Gasto_Por_Concepto> resultado)
        {
            return resultado.GroupBy(t => t.IdConceptoNegocio
            ).Select(t => new Resumen_Transaccion_Gasto_Por_Concepto()
            {
                IdConceptoNegocio = t.Key,
                ConceptoNegocio = t.First().ConceptoNegocio,
                Cantidad = t.Sum(tt => tt.Cantidad),
                Importe = t.Sum(tt => tt.Importe),
                //Pago = t.Sum(tt => tt.Pago)
            }).ToList();
        }

        public Resumen_Transaccion_Gasto_Por_Concepto()
        { }

        public static List<Resumen_Transaccion_Gasto_Por_Concepto> Convert()
        {
            return new List<Resumen_Transaccion_Gasto_Por_Concepto>();
        }

    }
}

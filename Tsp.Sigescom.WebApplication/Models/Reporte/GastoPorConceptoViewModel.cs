using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class GastoPorConceptoViewModel
    {
        public string ConceptoBasico { get; set; }
        public int IdConceptoNegocio { get; set; }
        public string ConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }

        public GastoPorConceptoViewModel()
        { }

        public GastoPorConceptoViewModel(Resumen_Transaccion_Gasto_Por_Concepto gasto)
        {
            this.IdConceptoNegocio = gasto.IdConceptoNegocio;
            this.ConceptoNegocio = gasto.ConceptoNegocio;
            this.Cantidad = gasto.Cantidad;
            this.PrecioUnitario = gasto.PrecioUnitario;
            this.Importe = gasto.Importe;
            ;
        }

        public static List<GastoPorConceptoViewModel> Convert(List<Resumen_Transaccion_Gasto_Por_Concepto> gastos)
        {
            var reporteConsolidadoViewModels = new List<GastoPorConceptoViewModel>();
            foreach (var gasto in gastos)
            { 
                reporteConsolidadoViewModels.Add(new GastoPorConceptoViewModel(gasto)); 
            }
            return reporteConsolidadoViewModels;
        }
    }
}
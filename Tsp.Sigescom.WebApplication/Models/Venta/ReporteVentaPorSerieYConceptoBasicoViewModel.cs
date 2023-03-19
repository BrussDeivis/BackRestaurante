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
    public class ReporteVentaPorSerieYConceptoBasicoViewModel 
    {

        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public string NombreCortoDocumento { get; set; }        
        public string ConceptoBasico { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }

        public ReporteVentaPorSerieYConceptoBasicoViewModel()
        { }

        public ReporteVentaPorSerieYConceptoBasicoViewModel(Resumen_Transaccion_Por_Serie_y_ConceptoBasico ordenVenta)
        {        
            this.IdSerie = ordenVenta.IdSerie;
            this.Serie = ordenVenta.Serie;
            this.NombreCortoDocumento = ordenVenta.NombreCortoDocumento;          
            this.ConceptoBasico = ordenVenta.ConceptoBasico;
            this.Cantidad = ordenVenta.Cantidad;
            this.PrecioUnitario = ordenVenta.PrecioUnitario;
            this.Importe = ordenVenta.Importe;

           }

        public ReporteVentaPorSerieYConceptoBasicoViewModel(long id, DateTime fecha, string tipoDocumento, string ordenVenta, string serie, string numero, decimal importe,string razonSocial)
        {
           
        }

        public static List<ReporteVentaPorSerieYConceptoBasicoViewModel>Convert(List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteVentaPorSerieYConceptoBasicoViewModel>();
            foreach (var ordenVenta in ordenes)
            {
                reporteVentaViewModels.Add(new ReporteVentaPorSerieYConceptoBasicoViewModel(ordenVenta));
            }

            return reporteVentaViewModels;
        }

        
    }
}
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
    public class ReporteVentaPorSerieYConceptoViewModel // ReporteVentaViewModel 
    {
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public string NombreCorto { get; set; }
        public string CodigoBarra { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }

        public ReporteVentaPorSerieYConceptoViewModel()
        {            }

        public ReporteVentaPorSerieYConceptoViewModel(Resumen_Transaccion_Por_Serie_y_Concepto_Negocio ordenVenta)
        {
           

            this.IdSerie = ordenVenta.IdSerie;
            this.Serie = ordenVenta.Serie;
            this.NombreCorto = ordenVenta.NombreCortoDocumento;
            this.CodigoBarra = ordenVenta.CodigoBarra;
            this.Concepto = ordenVenta.ConceptoNegocio;
            this.Cantidad = ordenVenta.Cantidad;
            this.PrecioUnitario = ordenVenta.PrecioUnitario;
            this.Importe = ordenVenta.Importe;

            

    }

        public ReporteVentaPorSerieYConceptoViewModel(long id, DateTime fecha, string tipoDocumento, string ordenVenta, string serie, string numero, decimal importe,string razonSocial)
        {
            //Id = id;
            //Fecha = fecha;
            //TipoDocumento = tipoDocumento;
            //OrdenVenta = ordenVenta;
            //Serie = serie;
            //Numero = numero;
            //Importe = importe;
            //Cliente = razonSocial;
        }

        public static List<ReporteVentaPorSerieYConceptoViewModel> Convert(List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteVentaPorSerieYConceptoViewModel>();
            foreach (var ordenVenta in ordenes)
            {
                reporteVentaViewModels.Add(new ReporteVentaPorSerieYConceptoViewModel(ordenVenta));
            }

            return reporteVentaViewModels;
        }

      
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models//Tsp.Sigescom.WebApplication.Models.concepto
  {
    [Serializable]
  
    public class VentaPorConceptoDelVendedorViewModel
    {
        public string CodigoBarra { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad  { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }

        public VentaPorConceptoDelVendedorViewModel()
        {
            
        }
        public VentaPorConceptoDelVendedorViewModel(string codigoBarra, string nombreconcepto, decimal cantidadVendida , decimal precioUnitario, decimal importe, string vendedor)
        {
            CodigoBarra = codigoBarra;
            Concepto = nombreconcepto;
            Cantidad = cantidadVendida;
            PrecioUnitario = precioUnitario;
            Importe=importe;           
        }

        public VentaPorConceptoDelVendedorViewModel(Resumen_transaccion_Venta_PorConcepto orden)
        {
            CodigoBarra = orden.CodigoBarra;
            Concepto = orden.Concepto; 
            Cantidad = orden.Cantidad;
            PrecioUnitario = orden.PrecioUnitario;
            Importe = orden.Importe;
        }

        public static List<VentaPorConceptoDelVendedorViewModel> Convert(List<Resumen_transaccion_Venta_PorConcepto> ordenes)
        {
            var reporteVentaViewModels = new List<VentaPorConceptoDelVendedorViewModel>();
            foreach (var orden in ordenes)
            {
                reporteVentaViewModels.Add(new VentaPorConceptoDelVendedorViewModel(orden));
            }
            return reporteVentaViewModels;
        }

    }
}
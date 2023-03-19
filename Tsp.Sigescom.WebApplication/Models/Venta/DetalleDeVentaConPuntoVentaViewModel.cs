using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
namespace Tsp.Sigescom.WebApplication.Models//Tsp.Sigescom.WebApplication.Models.concepto
  {
    [Serializable]
  
    public class DetalleDeVentaConPuntoVentaViewModel
    {
        public long CodigoConcepto { get; set; }
        public string NombreBasico { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad  { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public string NombrePuntoVenta { get; set; }
        public int IdPuntoVenta { get; set; }

        public DetalleDeVentaConPuntoVentaViewModel()
        {
            
        }
        public DetalleDeVentaConPuntoVentaViewModel(long codigoConcepto, string nombreBasico, string conceptoNegocio, decimal cantidadVendida , decimal precioUnitario, decimal importe)
        {
            CodigoConcepto = codigoConcepto;
            NombreBasico = nombreBasico;
            Concepto = conceptoNegocio;
            Cantidad = cantidadVendida;
            PrecioUnitario = precioUnitario;
            Importe=importe;
        }
          
        public DetalleDeVentaConPuntoVentaViewModel(DetalleDeOperacion detalleOrdenDeVenta)
        {
            CodigoConcepto = detalleOrdenDeVenta.DetalleTransaccion().id_concepto_negocio;

            NombreBasico = detalleOrdenDeVenta.Producto.NombreConceptoBasico; 
            Concepto = detalleOrdenDeVenta.Producto.Nombre; 

            Cantidad = detalleOrdenDeVenta.Cantidad;

            PrecioUnitario = detalleOrdenDeVenta.PrecioUnitario;
            Importe = detalleOrdenDeVenta.Importe;
            IdPuntoVenta = detalleOrdenDeVenta.Operacion().CentroDeAtencion().Id;
            NombrePuntoVenta = detalleOrdenDeVenta.Operacion().CentroDeAtencion().Nombre;
        }


        public static List<DetalleDeVentaConPuntoVentaViewModel> Convert(List<OrdenDeVenta> ordenes)
        {
            var reporteVentaViewModels = new List<DetalleDeVentaConPuntoVentaViewModel>();
            foreach (var detalle in ordenes.SelectMany(o => o.Detalles()))
            {
                reporteVentaViewModels.Add(new DetalleDeVentaConPuntoVentaViewModel(detalle));
            }

            return reporteVentaViewModels;
        }


    }
}
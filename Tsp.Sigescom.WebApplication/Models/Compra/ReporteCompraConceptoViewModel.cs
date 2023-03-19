using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models//este viewModel Uso para Reportes por concepto
  {
    [Serializable]
  
    public class ReporteCompraConceptoViewModel
    {
        public long CodigoConcepto { get; set; }
        public string CodigoBarra { get; set; }
        public string NombreBasico { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad  { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdPuntoCompra { get; set; }
        public string NombrePuntoCompra { get; set; }

        public ReporteCompraConceptoViewModel()
        {
            
        }
        public ReporteCompraConceptoViewModel(long codigoConcepto, string codigoBarra, string nombreBasico, string conceptoNegocio, decimal cantidadVendida ,int idPuntoCompra,string nombrePuntoCompra, decimal precioUnitario, decimal importe)
        {
            CodigoConcepto = codigoConcepto;
            CodigoBarra = codigoBarra;
            NombreBasico = nombreBasico;
            Concepto = conceptoNegocio;
            Cantidad = cantidadVendida;
            PrecioUnitario = precioUnitario;
            Importe=importe;
            IdPuntoCompra = idPuntoCompra;
            NombrePuntoCompra = nombrePuntoCompra;
        }

        public ReporteCompraConceptoViewModel(DetalleDeOperacion detalleOrdenDeCompra)
        {
            CodigoConcepto = detalleOrdenDeCompra.Producto.Id;
            CodigoBarra = detalleOrdenDeCompra.Producto.CodigoBarra;
            NombreBasico = detalleOrdenDeCompra.Producto.NombreConceptoBasico;
            Concepto = detalleOrdenDeCompra.Producto.Nombre;
            Cantidad = detalleOrdenDeCompra.Cantidad;
            PrecioUnitario = detalleOrdenDeCompra.PrecioUnitario;
            Importe = detalleOrdenDeCompra.Importe;
            IdPuntoCompra = detalleOrdenDeCompra.Operacion().CentroDeAtencion().Id;
            NombrePuntoCompra = detalleOrdenDeCompra.Operacion().CentroDeAtencion().Nombre;
        }

        public ReporteCompraConceptoViewModel(Detalle_transaccion detalleOrdenDeCompra)
        {
            CodigoConcepto = detalleOrdenDeCompra.Concepto_negocio.id;
            CodigoBarra = detalleOrdenDeCompra.Concepto_negocio.codigo_barra;
            NombreBasico = detalleOrdenDeCompra.Concepto_negocio.Detalle_maestro4.nombre;
            Concepto = detalleOrdenDeCompra.Concepto_negocio.nombre;
            Cantidad = detalleOrdenDeCompra.cantidad;
            PrecioUnitario = detalleOrdenDeCompra.precio_unitario;
            Importe = detalleOrdenDeCompra.total;
            IdPuntoCompra = detalleOrdenDeCompra.Transaccion.Actor_negocio2.id;
            NombrePuntoCompra = detalleOrdenDeCompra.Transaccion.Actor_negocio2.Actor.primer_nombre;
        }


        public static List<ReporteCompraConceptoViewModel> Convert(List<OrdenDeCompra> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteCompraConceptoViewModel>();

            foreach (var detalle in ordenes.SelectMany(o => o.DetalleTransaccion()))
            {
                reporteVentaViewModels.Add(new ReporteCompraConceptoViewModel(detalle));
            }
           
            return reporteVentaViewModels;
        }


    }
}
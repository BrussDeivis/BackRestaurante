using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ReporteCompraComprobante
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TipoDocumento { get; set; }
        public string OrdenCompra { get; set; }
        public int Numero { get; set; }
        public decimal Importe { get; set; }
        public string Proveedor { get; set; }
        public string ModoPago { get; set; }

        public ReporteCompraComprobante()
        {

        }
        public ReporteCompraComprobante(OrdenDeCompra ordenCompra)
        {
            this.Id = ordenCompra.Id;
            this.Fecha = ordenCompra.FechaEmision;
            this.FechaRegistro = ordenCompra.FechaDeRegistro;
            this.TipoDocumento = ordenCompra.Comprobante().NombreCortoTipo;
            this.OrdenCompra= ordenCompra.Codigo;
            this.Numero = ordenCompra.Comprobante().NumeroDeComprobante;
            this.Importe = ordenCompra.Total;
            this.Proveedor = ordenCompra.Proveedor().RazonSocial;
            this.ModoPago =   Enumerado.GetDescription(ordenCompra.ModoDePago());
        }

       
        public static List<ReporteCompraComprobante> Convert(List<OrdenDeCompra> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteCompraComprobante>();
            foreach (var orden in ordenes)
            {  reporteVentaViewModels.Add(new ReporteCompraComprobante(orden));     }
            return reporteVentaViewModels;
        }

        //internal static List<ReporteCompraComprobante> Resumen(List<ReporteCompraComprobante> reporteVentaViewModelDetalles)
        //{
            //List<ReporteCompraComprobante> reporteVentaViewModelResumenes = new List<ReporteCompraComprobante>();
            //foreach (var item in reporteVentaViewModelDetalles.Select(d => new {tipo=d.TipoDocumento, serie=d.Serie}).Distinct())
            //{
            //    reporteVentaViewModelResumenes.Add(new ReporteCompraComprobante(0,DateTime.Now,item.tipo,"",item.serie,"",reporteVentaViewModelDetalles.Where(d=>d.TipoDocumento== item.tipo && d.Serie== item.serie).Sum(d=>d.Importe),""));
            //}
            //return reporteVentaViewModelResumenes;
        //}
                                                                                                                                                                                                                                                                                                                                               
        //public static List<BandejaOrdenVentaViewModel> Convert(List<OrdenDeVenta> ordenesDeVenta)
        //{
        //    List<BandejaOrdenVentaViewModel> ordenes = new List<BandejaOrdenVentaViewModel>();
        //    foreach (var ordenDeVenta in ordenesDeVenta)
        //    {
        //        ordenes.Add(new BandejaOrdenVentaViewModel(ordenDeVenta));
        //    }
        //    return ordenes;
        //}
    }
}
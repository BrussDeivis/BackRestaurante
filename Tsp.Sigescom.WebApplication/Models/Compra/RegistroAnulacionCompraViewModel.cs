using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroAnulacionCompraViewModel
    {
        public long Id { get; set; }
        public string Proveedor { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public string Fecha { get; set; }
        public string Observacion { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public string SubTotal { get; set; }
        public string Igv { get; set; }
        public string Total { get; set; }

        public RegistroAnulacionCompraViewModel()
        {

        }

        public RegistroAnulacionCompraViewModel(OrdenDeCompra orden)
        {
            this.Id = orden.Id;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.Proveedor = orden.Proveedor().RazonSocial;
            this.Igv = orden.Igv().ToString("N2");
            this.Total = orden.Total.ToString("N2");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("N2");
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
        }
    }
}
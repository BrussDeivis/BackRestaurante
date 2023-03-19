using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroAnulacionVentaViewModel
    {
        public long Id { get; set; }
        public string Cliente { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public string Observacion { get; set; }
        public int IdMedioDePago { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public string SubTotal { get; set; }
        public string Igv { get; set; }
        public string Total { get; set; }

        public RegistroAnulacionVentaViewModel()
        {

        }
        public RegistroAnulacionVentaViewModel(OrdenDeVenta orden)
        {
            this.Id = orden.Id;
            this.Cliente = orden.Cliente().RazonSocial;
            this.IdMedioDePago = orden.Venta().ObtenerPagos().First().TrazaDePago().MedioDePago().id;
            this.Igv = orden.Igv().ToString("N2");
            this.Total = orden.Total.ToString("N2");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("N2");
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
        }
    }
}    
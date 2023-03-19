using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Venta
{
    [Serializable]
    [DataContract]
    public class BandejaOrdenesCajaVentaViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public string Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public long IdTipoDocumento { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }


        public BandejaOrdenesCajaVentaViewModel(OrdenDeVenta orden)
        {
            this.Id = orden.Id;
            this.IdVenta = orden.IdVenta;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.TipoDocumento = orden.Comprobante().NombreCortoTipo +" "+ orden.Comprobante().NumeroDeComprobante == "0" ? orden.Comprobante().NombreCortoTipo + " " + orden.Comprobante().NumeroDeSerie +"-"+ orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            this.IdTipoDocumento = orden.Comprobante().SerieComprobante().id;
            this.NombreDocumento = orden.Comprobante().NombreTipo;
            this.IdCliente = orden.Cliente().Id;
            this.Cliente = orden.Cliente().RazonSocial;
            this.Empleado = orden.Empleado().Nombres;
            this.Total = orden.Total.ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
        }

        public static List<BandejaOrdenesCajaVentaViewModel> Convert(List<OrdenDeVenta> ordenesDeVenta)
        {
            List<BandejaOrdenesCajaVentaViewModel> ordenes = new List<BandejaOrdenesCajaVentaViewModel>();
            foreach (var ordenDeVenta in ordenesDeVenta)
            {
                ordenes.Add(new BandejaOrdenesCajaVentaViewModel(ordenDeVenta));
            }
            return ordenes;
        }
    }
}
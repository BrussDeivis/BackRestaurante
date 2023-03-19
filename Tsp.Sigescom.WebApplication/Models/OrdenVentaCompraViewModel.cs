using Tsp.Sigescom.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class OrdenVentaCompraViewModel
    {

        [DataMember]
        public long Id { get; set; }
        public string Cliente { get; set; }
        // tipo y numero de doumento del cliente
        public string DocumentoCliente { get; set; }
        //tipo de comprobante de la orden de venta
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? IGV { get; set; }
        public decimal? Total { get; set; }
        public DateTime Fecha { get; set; }

        public OrdenVentaCompraViewModel()
        {

        }

        public OrdenVentaCompraViewModel(OrdenDeVenta ov)
        {
            this.Id = ov.Id;
            this.Fecha = ov.FechaEmision;
            this.Cliente = ov.Cliente().RazonSocial;
            this.DocumentoCliente= ov.Cliente().CodigoTipoDocumentoIdentidad()+ " "+ ov.Cliente().DocumentoIdentidad;
            this.TipoComprobante = ov.Comprobante().CodigoTipo;
            this.SerieComprobante = ov.Comprobante().NumeroDeSerie;
            this.NumeroComprobante = ov.Comprobante().NumeroDeComprobante.ToString().PadLeft(6, '0');
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(ov.Detalles());
            this.SubTotal = ov.ValorDeVenta;
            this.IGV = ov.Igv();
            this.Total = ov.Total;
        }
        public OrdenVentaCompraViewModel(OrdenDeCompra oc)
        {
            this.Id = oc.Id;
            this.TipoComprobante = oc.Comprobante().CodigoTipo;
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(oc.Detalles());
            this.NumeroComprobante = oc.Comprobante().NumeroDeSerie + "-" + oc.Comprobante().NumeroDeComprobante.ToString().PadLeft(6, '0');
            this.SubTotal = oc.ValorDeVenta;
            this.IGV = oc.Igv();
            this.Total = oc.Total;
        }
        public static List<OrdenVentaCompraViewModel> convert(List<OrdenDeCompra> documentos)
        {
            var documentosDeCompra = new List<OrdenVentaCompraViewModel>();

            foreach (var documento in documentos)
            {
                documentosDeCompra.Add(new OrdenVentaCompraViewModel(documento));
            }
            return documentosDeCompra;
        }
        public static List<OrdenVentaCompraViewModel> convert(List<OrdenDeVenta> documentos)
        {
            var documentosDeVenta = new List<OrdenVentaCompraViewModel>();

            foreach (var documento in documentos)
            {
                documentosDeVenta.Add(new OrdenVentaCompraViewModel(documento));
            }
            return documentosDeVenta;
        }
    }
}
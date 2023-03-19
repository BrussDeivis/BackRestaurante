using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaCompraViewModel
    {
        /// <summary>
        /// Id de orden de compra
        /// </summary>
        [DataMember]
        public long Id { get; set; }
        public long IdCompra { get; set; }
        public string Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string CodigoDocumento { get; set; }
        public string Numero { get; set; }
        public string Proveedor{ get; set; }
        public string Ruc { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public bool EsAnuladoConNotaInterna { get; set; }
        public bool EsAnuladoConNotaDeCredito { get; set; }
        public bool EsOrdenDeCompra { get; set; }
        public string ModoPago { get; set; }

        public BandejaCompraViewModel( OrdenDeCompra orden)
        {
            this.Id = orden.Id;
            this.IdCompra = orden.IdCompra;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.TipoDocumento = orden.Comprobante().Tipo().Nombre;
            this.CodigoDocumento = orden.Comprobante().Tipo().Codigo;
            this.Numero = orden.Comprobante().NumeroDeSerie+" - "+ orden.Comprobante().NumeroDeComprobante;
            this.Proveedor = orden.Proveedor().RazonSocial;
            this.Ruc = orden.Proveedor().DocumentoIdentidad;
            this.Total = orden.Total.ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.EsAnuladoConNotaInterna = orden.esAnulableConNotaInterna();
            this.EsAnuladoConNotaDeCredito = orden.esAnulableConNotaDeCredito();
        }

        public BandejaCompraViewModel(OperacionDeCompra orden)
        {
            this.Id = orden.Id;
            this.IdCompra = orden.IdCompra;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.TipoDocumento = orden.Comprobante().NombreTipo;
            this.CodigoDocumento = orden.Comprobante().Tipo().Codigo;
            this.Numero = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante;
            this.Proveedor = orden.Proveedor().RazonSocial;
            this.Ruc = orden.Proveedor().DocumentoIdentidad;
            this.Total = orden.Total.ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.EsAnuladoConNotaInterna = orden.esAnulableConNotaInterna();
            this.EsAnuladoConNotaDeCredito = orden.esAnulableConNotaDeCredito();
            this.EsOrdenDeCompra = orden.esOrdenDeCompra();
            this.ModoPago = Enumerado.GetDescription(orden.ModoDePago());
        }

        public static List<BandejaCompraViewModel> Convert(List<OrdenDeCompra> ordenesDeCompra)
        {
            List<BandejaCompraViewModel> ordenes = new List<BandejaCompraViewModel>();
            foreach (var ordenDeCompra in ordenesDeCompra)
            {
                ordenes.Add(new BandejaCompraViewModel(ordenDeCompra));
            }
            return ordenes;
        }

        public static List<BandejaCompraViewModel> Convert_(List<OperacionDeCompra> operacionesDeCompra)
        {
            List<BandejaCompraViewModel> ordenes = new List<BandejaCompraViewModel>();
            foreach (var operacionDeCompra in operacionesDeCompra)
            {
                ordenes.Add(new BandejaCompraViewModel(operacionDeCompra));
            }
            return ordenes;
        }
    }
}
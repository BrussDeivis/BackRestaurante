using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaVentasYCobrosPorVendedorViewModel
    {

        [DataMember]
        public long IdTransaccion { get; set; }
        public string FechaEmision { get; set; }
        public string FechaRegistro { get; set; }
        public string CentroDeAtencion { get; set; }
        public string IdCentroDeAtencion { get; set; }
        public string Vendedor { get; set; }
        public string Cajero { get; set; }
        public string TotalVenta { get; set; }
        public string TotalCobranza { get; set; }

        public BandejaVentasYCobrosPorVendedorViewModel()
        {
        }

        public BandejaVentasYCobrosPorVendedorViewModel(VentaYCobranzaCarteraDeClientes ventaYCobranzaMasiva)
        {
            this.IdTransaccion = ventaYCobranzaMasiva.IdTransaccion;
            this.FechaEmision = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.FechaEmision.ToString("dd/MM/yyyy") : ventaYCobranzaMasiva.Cobranza.FechaEmision.ToString("dd/MM/yyyy");
            this.FechaRegistro = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss.fff") : ventaYCobranzaMasiva.Cobranza.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss.fff");
            this.IdCentroDeAtencion = ventaYCobranzaMasiva.IdCentroAtencion.ToString();
            this.CentroDeAtencion = ventaYCobranzaMasiva.NombreCentroAtencion ;
            this.Vendedor = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.NombreVendedor : "";
            this.Cajero = ventaYCobranzaMasiva.Cobranza != null ? ventaYCobranzaMasiva.Cobranza.NombreCajero: "";
            this.TotalVenta = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.Detalles.Sum(d => d.PrecioUnitario * d.Cantidad).ToString("N2"): 0.ToString("N2");
            this.TotalCobranza = ventaYCobranzaMasiva.Cobranza != null ? ventaYCobranzaMasiva.Cobranza.Detalles.Sum(d => d.Importe).ToString("N2") : 0.ToString("N2"); 
        }



        public static List<BandejaVentasYCobrosPorVendedorViewModel> Convert(List<VentaYCobranzaCarteraDeClientes> ventasYCobranzasMasivas)
        {
            List<BandejaVentasYCobrosPorVendedorViewModel> VentasYCobrosViewModel = new List<BandejaVentasYCobrosPorVendedorViewModel>();

            foreach (var item in ventasYCobranzasMasivas)
            {
                VentasYCobrosViewModel.Add(new BandejaVentasYCobrosPorVendedorViewModel(item));
            }
            return VentasYCobrosViewModel;
        }

    }
}
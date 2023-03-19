using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ReporteVentaClienteFormatoAdsoftViewModel
    {


        public string NumeroSerie { get; set; }
        public string NumeroComprobante { get; set; }
        public string Fecha { get; set; }
        public string NumeroRUC { get; set; }
        public long IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string CodigoComprobante { get; set; }
        public string TipoMoneda { get; set; }

        public decimal Detraccion { get; set; }
        public decimal ImporteVenta { get; set; }
        public decimal ImpuestoSelectivoAlConsumo { get; set; }
        public decimal Icbper { get; set; }
        public decimal ImporteTotalInafecta { get; set; }
        public decimal ImporteTotalExonerada { get; set; }
        public decimal ImporteTotalExportacion { get; set; }

        public decimal Recargo { get; set; }
        public decimal ImporteIGV { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; }

        public string SerDqm { get; set; }
        public string NumDqm { get; set; }
        public string FecDqm { get; set; }
        public string TipDqm { get; set; }
        public string SerieFin { get; set; }
        public string NumeroFin { get; set; }
        public string NumeroDNI { get; set; }
        public string Pasaporte { get; set; }
        public string CuentaDeVenta { get; set; }
        public decimal TipoCambio { get; set; }

        public ReporteVentaClienteFormatoAdsoftViewModel()
        { }

        public ReporteVentaClienteFormatoAdsoftViewModel(Venta_Cliente venta)
        {


            //Para en caso de los reportes si es invalidada se tiene que poner 0
            this.NumeroSerie = venta.NumeroSerie;
            this.NumeroComprobante = venta.NumeroComprobante > 0 ? venta.NumeroComprobante.ToString() : venta.NumeroInicial.ToString() ;
            this.Fecha = venta.FechaEmision.ToString("dd/MM/yyyy");
            this.NumeroRUC = ActorSettings.Default.IdTipoDocumentoIdentidadRuc == venta.IdTipoDocumento ? venta.NumeroDocumento : "";
            this.RazonSocial = venta.EsInvalidada ? "ANULADO" : venta.PrimerNombre.Replace("|", " ");
            this.CodigoComprobante = venta.CodigoComprobante;
            this.TipoMoneda = venta.CodigoMoneda;

            this.Detraccion = 0;
            this.ImporteVenta = venta.EsInvalidada ? 0 : venta.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal > 0 ? venta.BaseImponibleOperacionGravadaConSigno : venta.ImporteTotalComprobantePago;
            //venta.ImporteTotalComprobantePago;
            this.ImpuestoSelectivoAlConsumo = 0;
            this.ImporteTotalInafecta = 0;
            this.ImporteTotalExonerada = venta.EsInvalidada ? 0 : venta.ImporteTotalOperacionExoneradaConSigno;
            this.ImporteTotalExportacion = 0;

            this.Recargo = 0;
            this.ImporteIGV = venta.EsInvalidada ? 0 : venta.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal;
            this.Icbper = venta.EsInvalidada ? 0 : venta.Icbper;
            this.ImporteTotal = venta.EsInvalidada ? 0 : venta.ImporteTotalComprobantePago;
            this.Estado = venta.EsInvalidada ? "A":""; 

            if (venta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito || venta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito)
            {
                this.SerDqm = venta.NumeroSerieReferencia;
                this.NumDqm = venta.NumeroComprobanteReferencia.ToString();
                this.FecDqm = venta.FechaEmisionReferencia.ToString("dd/MM/yyyy") ;
                this.TipDqm =  venta.CodigoComprobanteReferencia;
                this.SerieFin = venta.NumeroSerieReferencia;
            }
            else
            {
                this.SerDqm = "";
                this.NumDqm =  "";
                this.FecDqm = "";
                this.TipDqm =  "";
                this.SerieFin = "";
            }

 

            this.NumeroFin = venta.NumeroFinal > 0 ? venta.NumeroFinal.ToString() : venta.NumeroComprobante.ToString();
            this.NumeroDNI = ActorSettings.Default.IdTipoDocumentoIdentidadDni == venta.IdTipoDocumento ? venta.NumeroDocumento : "";
            this.Pasaporte = "";
            this.CuentaDeVenta = "";
            this.TipoCambio = 0;
        }

        public static List<ReporteVentaClienteFormatoAdsoftViewModel> Convert(List<Venta_Cliente> ventas)
        {
            List<ReporteVentaClienteFormatoAdsoftViewModel> ventasViewModel = new List<ReporteVentaClienteFormatoAdsoftViewModel>();
            foreach (var item in ventas)
            {
                ventasViewModel.Add(new ReporteVentaClienteFormatoAdsoftViewModel(item));
            }
            return ventasViewModel;
        }

    }
}
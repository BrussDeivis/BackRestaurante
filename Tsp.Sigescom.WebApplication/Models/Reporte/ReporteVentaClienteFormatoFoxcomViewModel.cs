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
    public class ReporteVentaClienteFormatoFoxcomViewModel
    {
        public string Fecha { get; set; }
        public string CodigoComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public string NumeroComprobante { get; set; }
        public long IdCliente { get; set; }
        public string CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public decimal BaseImponibleGravada { get; set; }
        public decimal IGVoIPM { get; set; }
        public decimal Icbper { get; set; }
        public decimal ImporteTotalExonerada { get; set; }
        public decimal Retencion { get; set; }
        public decimal ImporteTotalComprobante { get; set; }
        public string NumeroSerieReferencia { get; set; }
        public string NumeroComprobanteReferencia { get; set; }
        public string FechaEmisionReferencia { get; set; }
        public string CodigoComprobanteReferencia { get; set; }

        public ReporteVentaClienteFormatoFoxcomViewModel()
        { }

        public ReporteVentaClienteFormatoFoxcomViewModel(Venta_Cliente venta)
        {
            this.Fecha = venta.FechaEmision.ToString("dd/MM/yyyy");
            this.CodigoComprobante = venta.CodigoComprobante;
            this.NumeroSerie = venta.NumeroSerie;
            this.NumeroComprobante = venta.NumeroComprobante.ToString().PadLeft(7, '0');
            this.CodigoDocumento = venta.CodigoDocumento;
            this.NumeroDocumento = venta.NumeroDocumento;
            this.RazonSocial = venta.PrimerNombre.Replace("|", " ");
           
            this.BaseImponibleGravada = venta.EsInvalidada ? 0 : venta.BaseImponibleOperacionGravadaConSigno;
            this.IGVoIPM = venta.EsInvalidada ? 0 : venta.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal;
            this.Icbper = venta.EsInvalidada ? 0 : venta.Icbper;
            this.ImporteTotalExonerada = venta.EsInvalidada ? 0 : venta.ImporteTotalOperacionExoneradaConSigno;
            this.Retencion = 0;
            this.ImporteTotalComprobante = venta.EsInvalidada ? 0 : venta.ImporteTotalComprobantePago;

            if (venta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito || venta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito)
            {
                this.NumeroSerieReferencia = venta.NumeroSerieReferencia;
                this.NumeroComprobanteReferencia = venta.NumeroComprobanteReferencia.ToString();
                this.FechaEmisionReferencia = venta.FechaEmisionReferencia.ToString("dd/MM/yyyy");
                this.CodigoComprobanteReferencia = venta.CodigoComprobanteReferencia;

            }
            else
            {
                this.NumeroSerieReferencia = "";
                this.NumeroComprobanteReferencia = "";
                this.FechaEmisionReferencia = "";
                this.CodigoComprobanteReferencia = "";

            }
        }

        public static List<ReporteVentaClienteFormatoFoxcomViewModel> Convert(List<Venta_Cliente> ventas)
        {
            List<ReporteVentaClienteFormatoFoxcomViewModel> ventasViewModel = new List<ReporteVentaClienteFormatoFoxcomViewModel>();
            foreach (var item in ventas)
            {
                ventasViewModel.Add(new ReporteVentaClienteFormatoFoxcomViewModel(item));
            }
            return ventasViewModel;
        }

    }
}
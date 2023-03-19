using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class VentaClienteFoxcom
    {
        public long Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CodigoComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public int NumeroComprobante { get; set; }
        public int IdActorNegocioExterno { get; set; }
        public string CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoComprobante { get; set; }
        public int IdEstadoActual { get; set; }
        public string CodigoComprobanteReferencia { get; set; }
        public string NumeroSerieReferencia { get; set; }
        public int NumeroComprobanteReferencia { get; set; }
        public DateTime FechaEmisionReferencia { get; set; }

        public int Signo { get => this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito ? -1 : 1; }
        public bool EsNota { get => this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito || this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito; }
        public bool EsInvalidada { get => this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; }
        public decimal Total { get; set; }
        public decimal ImporteTotal { get => Total * Signo; }
        public decimal? Igv { get; set; }
        public decimal ImporteIgv { get => Igv != null ? (decimal)Igv * Signo : 0; }
        public decimal Icbper { get; set; }
        public decimal ImporteIcbper { get => Icbper * Signo; }
        public decimal ValorVenta { get; set; }
        public decimal ImporteValorVenta { get => ValorVenta * Signo; }

        public decimal ImporteBaseImponibleOperacionGravada { get => Math.Abs(ImporteIgv) > 0 ? ImporteValorVenta : 0; }
        public decimal ImpuestoGeneralVentasYOImpuestoPromocionMunicipal { get => Math.Abs(ImporteIgv) > 0 ? ImporteIgv : 0; }
        public decimal ImporteTotalOperacionExonerada { get => Math.Abs(ImporteIgv) > 0 ? 0 : ImporteValorVenta; }
    }
}

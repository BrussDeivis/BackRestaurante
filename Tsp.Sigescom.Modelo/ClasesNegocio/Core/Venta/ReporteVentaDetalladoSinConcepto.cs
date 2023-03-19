using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ReporteVentaDetalladoSinConcepto
    {
        public string CUO { get => ""; } //CUO = Codigo Unico de la Operacion
        public DateTime FechaEmisionComprobante { get; set; }
        public DateTime FechaVencimientoComprobante { get; set; }
        public int IdTipoComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public string TipoDocumentoIdentidadCliente { get; set; }
        public string NumeroDocumentoIdentidadCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public decimal ValorExportacion { get => 0; }
        public decimal BaseImponibleGravada { get => EsInvalidada ? 0 : EsNotaCredito ? ValorIgv > 0 ? -ValorVenta : 0 : ValorIgv > 0 ? ValorVenta : 0; }
        public decimal ImporteTotalExonerada { get => EsInvalidada ? 0 : EsNotaCredito ? ValorIgv <= 0 ? -ValorTotal : 0 : ValorIgv <= 0 ? ValorTotal : 0; }
        public decimal ImporteTotalInafecta { get => 0; }
        public decimal ISC { get => EsInvalidada ? 0 : ValorIsc; }
        public decimal IGVoIPM { get => EsInvalidada ? 0 : EsNotaCredito ? ValorIgv > 0 ? -ValorIgv : 0 : ValorIgv > 0 ? ValorIgv : 0; }
        public decimal TributosYCargosNoImponible { get => 0; }
        public decimal ImporteTotalComprobante { get => EsInvalidada ? 0 : EsNotaCredito ? -ValorTotal : ValorTotal; }
        public decimal TipoCambio { get => ValorTipoCambio; }
        public DateTime? FechaComprobanteModifica { get => EsComprobanteQueModificaAOtro && !EsInvalidada ? FechaComprobanteReferencia : null; }
        public string TipoComprobanteModifica { get => EsComprobanteQueModificaAOtro && !EsInvalidada ? TipoComprobanteReferencia : null; }
        public string SerieComprobanteModifica { get => EsComprobanteQueModificaAOtro && !EsInvalidada ? SerieComprobanteReferencia : null; }
        public int NumeroComprobanteModifica { get => EsComprobanteQueModificaAOtro && !EsInvalidada ? NumeroComprobanteReferencia : 0; }

        public int IdEstado { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorIgv { get; set; }
        public decimal ValorIsc { get; set; }
        public decimal ValorTipoCambio { get; set; }
        public DateTime? FechaComprobanteReferencia { get; set; }
        public string TipoComprobanteReferencia { get; set; }
        public string SerieComprobanteReferencia { get; set; }
        public int NumeroComprobanteReferencia { get; set; }
        public IEnumerable<string> Icbpers { get; set; }
       

        public bool EsInvalidada { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; }
        public bool EsNotaCredito { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito; }
        public bool EsComprobanteQueModificaAOtro { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito || this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito; }
        public decimal ValorVenta { get => ValorTotal - ValorIgv - Icbper; }
        public decimal Icbper { get => (Icbpers != null) && Icbpers.Count() > 0 ? Icbpers.Sum(i => System.Convert.ToDecimal(i)) : 0; }

        public ReporteVentaDetalladoSinConcepto()
        {
        }

        public static List<ReporteVentaDetalladoSinConcepto> Convert()
        {
            return new List<ReporteVentaDetalladoSinConcepto>();
        }

    }
}

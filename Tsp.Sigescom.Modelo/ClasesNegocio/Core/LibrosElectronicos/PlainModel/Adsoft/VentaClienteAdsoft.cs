using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class VentaClienteAdsoft
    {
        public long Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public string CodigoComprobante { get; set; }
        public string NombreCortoComprobante { get; set; }

        public int? IdSerie { get; set; }
        public string NumeroSerie { get; set; }
        public int NumeroComprobante { get; set; }
        public int IdActorNegocioExterno { get; set; }
        public int IdTipoDocumento { get; set; }
        public string CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public int IdTipoTransaccion { get; set; }
        public int IdTipoComprobante { get; set; }
        public int IdEstadoActual { get; set; }
        public int? IdEstadoAnteriorAlActual { get; set; }

        public int NumeroInicial { get; set; }
        public int NumeroFinal { get; set; }
        public string CodigoMoneda { get; set; }

        public string NumeroSerieReferencia { get; set; }
        public int NumeroComprobanteReferencia { get; set; }
        public DateTime FechaEmisionReferencia { get; set; }
        public string CodigoComprobanteReferencia { get; set; }

        public int Anyo { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }

        public int Signo { get => this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito ? -1 : 1; }
        public decimal Total { get; set; }
        public decimal ImporteTotal { get => Total * Signo; }
        public decimal? ValorIgv { get; set; }
        public decimal Igv { get => ValorIgv != null ? (decimal)ValorIgv * Signo : 0; }
        public decimal ValorIcbper { get; set; }
        public decimal Icbper { get => ValorIcbper * Signo; }
        public decimal ValorVenta { get; set; }
        public decimal ValorDeVenta { get => ValorVenta * Signo; }

        public bool EsInvalidada
        {
            get
            {
                return (this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado || this.IdEstadoAnteriorAlActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado);
            }
        }

        public bool ElComprobanteOriginalEsDeUnperiodoAnterior()
        {
            return (this.FechaEmisionReferencia.Year <= this.FechaEmision.Year && this.FechaEmisionReferencia.Month < this.FechaEmision.Month);
        }

        public decimal ImporteTotalComprobantePago
        {
            get
            {
                return ImporteTotal;
            }
        }

        public decimal BaseImponibleOperacionGravadaConSigno
        {
            get
            {
                return Math.Abs((decimal)Igv) > 0 ? ValorDeVenta : 0;
            }
        }

        public decimal ImpuestoGeneralVentasYOImpuestoPromocionMunicipal
        {
            get
            {
                return Math.Abs((decimal)Igv) > 0 ? (decimal)Igv : 0;
            }
        }

        public decimal ImporteTotalOperacionExoneradaConSigno
        {
            get
            {
                return Math.Abs((decimal)Igv) > 0 ? 0 : ValorDeVenta;
            }
        }

        //1 = EsInvalidada, 2 = Es cliente identificado, 3 = Es cliente generico
        public int TipoAgrupamiento
        {
            get { return EsInvalidada ? 1 : IdActorNegocioExterno != ActorSettings.Default.IdClienteGenerico ? 2 : 3; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Venta_Cliente
    {
        private long id;
        private DateTime fechaEmision;

        private int anyo;
        private int mes;
        private int dia;

        private int idTipoTransaccion;
        private int idTipoComprobante;
        private string codigoComprobante;
        private string nombreCortoComprobante;
 
        private int? idSerie;
        private string numeroSerie;
        private int numeroComprobante;
        private int idActorNegocioExterno;
        private int idTipoDocumento;
        private string codigoDocumento;
        private string numeroDocumento;
        private string primerNombre;
        private decimal importeTotal;
        private decimal valorVenta;
        private decimal? igv;
        private decimal valorIcbper;

        private int idEstadoActual;
        private int? idEstadoAnteriorAlActual;

        private int numeroInicial;
        private int numeroFinal;
        private string codigoMoneda;

        private string numeroSerieReferencia;
        private int numeroComprobanteReferencia;
        private DateTime fechaEmisionReferencia;
        private string codigoComprobanteReferencia;


        public Venta_Cliente()
        {

        }

        public long Id { get => id; set => id = value; }
        public DateTime FechaEmision { get => new DateTime(Anyo, Mes, Dia); set => fechaEmision = value; }
        public string CodigoComprobante { get => codigoComprobante; set => codigoComprobante = value; }
        public string NombreCortoComprobante { get => nombreCortoComprobante; set => nombreCortoComprobante = value; }

        public int? IdSerie { get => idSerie; set => idSerie = value; }
        public string NumeroSerie { get => numeroSerie; set => numeroSerie = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public int IdActorNegocioExterno { get => idActorNegocioExterno; set => idActorNegocioExterno = value; }
        public int IdTipoDocumento { get => idTipoDocumento; set => idTipoDocumento = value; }
        public string CodigoDocumento { get => codigoDocumento; set => codigoDocumento = value; }
        public string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        public string PrimerNombre { get => primerNombre; set => primerNombre = value; }
        public int IdTipoTransaccion { get => idTipoTransaccion; set => idTipoTransaccion = value; }
        public int IdTipoComprobante { get => idTipoComprobante; set => idTipoComprobante = value; }
        public int IdEstadoActual { get => idEstadoActual; set => idEstadoActual = value; }
        public int? IdEstadoAnteriorAlActual { get => idEstadoAnteriorAlActual; set => idEstadoAnteriorAlActual = value; }

        public int NumeroInicial { get => numeroInicial; set => numeroInicial = value; }
        public int NumeroFinal { get => numeroFinal; set => numeroFinal = value; }
        public string CodigoMoneda { get => codigoMoneda; set => codigoMoneda = value; }

        public string NumeroSerieReferencia { get => numeroSerieReferencia; set => numeroSerieReferencia = value; }
        public int NumeroComprobanteReferencia { get => numeroComprobanteReferencia; set => numeroComprobanteReferencia = value; }
        public DateTime FechaEmisionReferencia { get => fechaEmisionReferencia; set => fechaEmisionReferencia = value; }
        public string CodigoComprobanteReferencia { get => codigoComprobanteReferencia; set => codigoComprobanteReferencia = value; }

        public int Anyo { get => anyo; set => anyo = value; }
        public int Mes { get => mes; set => mes = value; }
        public int Dia { get => dia; set => dia = value; }

        public int Signo { get => this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito  ? -1 : 1; }
        public decimal ImporteTotal { get => importeTotal * Signo; set => importeTotal = value; }
        public decimal? Igv { get => this.igv != null ? (decimal)this.igv * Signo : 0; set => igv = value; }
        public decimal ValorIcbper { get => valorIcbper; set => valorIcbper = value; }
        public decimal Icbper { get => ValorIcbper * Signo ; }
        public decimal ValorDeVenta { get => valorVenta * Signo; set => valorVenta = value; }

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
                return Math.Abs((decimal)Igv) > 0 ? 0 : ImporteTotal;
            }
        }

        //1 = EsInvalidada, 2 = Es cliente identificado, 3 = Es cliente generico
        public int TipoAgrupamiento
        {
            get { return EsInvalidada ? 1 : idActorNegocioExterno != ActorSettings.Default.IdClienteGenerico ? 2 : 3; }
        }


        public static List<Venta_Cliente> Convert()
        {
            return new List<Venta_Cliente>();
        }
    }

}
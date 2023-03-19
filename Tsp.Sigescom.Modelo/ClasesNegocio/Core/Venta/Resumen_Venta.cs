using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Resumen_Venta
    {
        private long id;
        private DateTime fechaEmision;
        private int idTipoComprobante;
        private string tipoComprobante;
        private string codigoComprobante;
        private string serieComprobante;
        private int numeroComprobante;
        private int idCliente;
        private int idTipoDocumentoCliente;
        private string documentoCliente;
        private string nombreCliente;
        private string primerNombreCajero;
        private string segundoNombreCajero;
        private string tercerNombreCajero;
        private decimal importeTotal;
        private string valorParametroTipoDeVenta;
        private string valorParametroModoDePago;
        private string valorParametroAliasDeCliente;
        private int? idEstado;
        private string estado;
        private bool transmitido;

        public Resumen_Venta()
        {

        }


        public long Id { get => id; set => id = value; }
        public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public int IdTipoComprobante { get => idTipoComprobante; set => idTipoComprobante = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string CodigoComprobante { get => codigoComprobante; set => codigoComprobante = value; }
        public string SerieComprobante { get => serieComprobante; set => serieComprobante = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdTipoDocumentoCliente { get => idTipoDocumentoCliente; set => idTipoDocumentoCliente = value; }
        public string DocumentoCliente { get => documentoCliente; set => documentoCliente = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string PrimerNombreCajero { get => primerNombreCajero; set => primerNombreCajero = value; }
        public string SegundoNombreCajero { get => segundoNombreCajero; set => segundoNombreCajero = value; }
        public string TercerNombreCajero { get => tercerNombreCajero; set => tercerNombreCajero = value; }
        public decimal ImporteTotal { get => importeTotal; set => importeTotal = value; }
        public string ValorParametroTipoDeVenta { get => valorParametroTipoDeVenta; set => valorParametroTipoDeVenta = value; }
        public string ValorParametroModoDePago { get => valorParametroModoDePago; set => valorParametroModoDePago = value; }
        public string ValorParametroAliasDeCliente { get => valorParametroAliasDeCliente; set => valorParametroAliasDeCliente = value; }
        public int? IdEstado { get => idEstado; set => idEstado = value; }
        public string Estado { get => estado; set => estado = value; }
        public bool Transmitido { get => transmitido; set => transmitido = value; }

        public string TipoDeVenta { get => ValorParametroTipoDeVenta != null ? Enumerado.GetDescription((ModoOperacionEnum)Convert.ToInt32(ValorParametroTipoDeVenta)) : Enumerado.GetDescription(ModoOperacionEnum.PorMostrador); }
        public string ModoDePago { get => ValorParametroModoDePago != null ? Enumerado.GetDescription((ModoPago)Convert.ToInt32(ValorParametroModoDePago)) : Enumerado.GetDescription(ModoPago.Contado); }
        public string AliasDeCliente { get => ValorParametroAliasDeCliente ?? ""; }
        public string Fecha { get => FechaEmision.ToString("dd/MM/yyyy"); }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        public string Cliente { get => (IdCliente == ActorSettings.Default.IdClienteGenerico ? "-" : DocumentoCliente) + " | " + (TransaccionSettings.Default.MostrarAliasDeClienteGenerico && !String.IsNullOrEmpty(AliasDeCliente) ? NombreCliente.Replace("|", "") + " | " + AliasDeCliente : NombreCliente.Replace("|", " ")); }
        public string Cajero { get => SegundoNombreCajero; }
        public string Total { get => ImporteTotal.ToString("N2"); }
        public string EstaTransmitido { get => Transmitido ? "SI" : "NO"; }
        public bool PuedeCanjearse { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna && IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado; }
    }
    public class Guia_Remision
    {
        public long Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public string TipoComprobante { get; set; }
        public string CodigoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public int IdTercero { get; set; }
        public string DocumentoTercero { get; set; }
        public string NombreTercero { get; set; }
        public DateTime FechaInicioTraslado { get; set; }
        public int IdTransportista { get; set; }
        public string MarcaYPlaca { get; set; }
        public string NumeroLicencia { get; set; }
        public int IdModalidadTransporte { get; set; }
        public int IdMotivoTraslado { get; set; }
        public string DireccionOrigen { get; set; }
        public int IdUbigeoOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public int IdUbigeoDestino { get; set; }
        public string Estado { get; set; }
        public bool Transmitido { get; set; }

        //public string TipoDeVenta { get => ValorParametroTipoDeVenta != null ? Enumerado.GetDescription((ModoOperacionEnum)Convert.ToInt32(ValorParametroTipoDeVenta)) : Enumerado.GetDescription(ModoOperacionEnum.PorMostrador); }
        //public string ModoDePago { get => ValorParametroModoDePago != null ? Enumerado.GetDescription((ModoPago)Convert.ToInt32(ValorParametroModoDePago)) : Enumerado.GetDescription(ModoPago.Contado); }
        //public string AliasDeCliente { get => ValorParametroAliasDeCliente ?? ""; }
        //public string Fecha { get => FechaEmision.ToString("dd/MM/yyyy"); }
        //public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        //public string Cliente { get => (IdTercero == ActorSettings.Default.IdClienteGenerico ? "-" : DocumentoTercero) + " | " + (TransaccionSettings.Default.MostrarAliasDeClienteGenerico && !String.IsNullOrEmpty(AliasDeCliente) ? NombreCliente + " | " + AliasDeCliente : NombreCliente.Replace("|", " ")); }
        //public string Total { get => ImporteTotal.ToString("0.00"); }
        //public string EstaTransmitido { get => Transmitido ? "SI" : "NO"; }

        public Guia_Remision()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Cuenta_Cobrar_Pagar
    {

        private int idCuota;
        private string codigoCuota;
        private long idTransaccion;
        private int idTipoTransaccion;
        private string tipoTransaccion;
        private int idActorComercial;
        private string codigoTipoDocumentoActorComercial;
        private string numeroDocumentoActorComercial;
        private string actorComercial;
        private string nombreGrupo;
        private string codigoTipoComprobante;
        private string tipoComprobante;
        private string serieComprobante;
        private int numeroComprobante;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private decimal pagoACuenta;
        private decimal total;

        public int IdCuota { get => idCuota; set => idCuota = value; }
        public string CodigoCuota { get => codigoCuota; set => codigoCuota = value; }
        public long IdTransaccion { get => idTransaccion; set => idTransaccion = value; }
        public int IdTipoTransaccion { get => idTipoTransaccion; set => idTipoTransaccion = value; }
        public string TipoTransaccion { get => tipoTransaccion; set => tipoTransaccion = value; }
        public int IdActorComercial { get => idActorComercial; set => idActorComercial = value; }
        public string CodigoTipoDocumentoActorComercial { get => codigoTipoDocumentoActorComercial; set => codigoTipoDocumentoActorComercial = value; }
        public string NumeroDocumentoActorComercial { get => numeroDocumentoActorComercial; set => numeroDocumentoActorComercial = value; }
        public string ActorComercial { get => actorComercial; set => actorComercial = value; }
        public string NombreGrupo { get => nombreGrupo??"NINGUNO"; set => nombreGrupo = value; }
        public string CodigoTipoComprobante { get => codigoTipoComprobante; set => codigoTipoComprobante = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string SerieComprobante { get => serieComprobante; set => serieComprobante = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public decimal PagoACuenta { get => pagoACuenta; set => pagoACuenta = value; }
        public decimal Revocado { get => pagoACuenta; set => pagoACuenta = value; }
        public decimal Total { get => total; set => total = value; }

        public string DocumentoActorComercial { get => CodigoTipoDocumentoActorComercial + ":" + NumeroDocumentoActorComercial; }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        public string FechaEmision { get => FechaInicio.ToString("dd/MM/yyyy"); }
        public string FechaVencimiento { get => FechaFin.ToString("dd/MM/yyyy"); }
        public decimal Saldo { get; set;  }
        public string NombreActorComercial { get => ActorComercial.Replace("|", " "); }


        //this.IdCuota = cuenta.Id;
        //this.CodigoCuota = cuenta.Codigo;
        //this.IdTransaccion = cuenta.IdOperacionBase;
        //this.IdTipoTransaccion = cuenta.OperacionBase().TipoTransaccion().id;
        //this.TipoTransaccion = cuenta.OperacionBase().TipoTransaccionSuperior().nombre;
        //this.IdActorComercial = cuenta.clienteProveedor().Id;
        //this.ActorComercial = cuenta.clienteProveedor().RazonSocial;
        //this.DocumentoActorComercial = cuenta.clienteProveedor().DocumentoIdentidad;
        //this.Comprobante = cuenta.OperacionBase().Comprobante().NumeroDeSerie + "-" + cuenta.OperacionBase().Comprobante().NumeroDeComprobante;
        //this.TipoComprobante = cuenta.OperacionBase().Comprobante().NombreTipo;
        //this.CodigoDocumento = cuenta.OperacionBase().Comprobante().Tipo().Codigo;
        //this.Saldo = cuenta.Saldo();
        //this.Estado = cuenta.Estado.nombre;
        //this.Emitida = cuenta.FechaDeEmision.ToString("dd/MM/yyyy");
        //this.Vence = cuenta.FechaDeVencimiento.ToString("dd/MM/yyyy");
        //this.Total = cuenta.Total();
        //this.AccionCobrarPagar = Saldo > 0 && cuenta.Estado.id == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
    }



}

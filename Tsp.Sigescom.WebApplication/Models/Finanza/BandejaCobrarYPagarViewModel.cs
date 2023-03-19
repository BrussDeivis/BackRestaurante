using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaCobrarYPagarViewModel
    {
        [DataMember]
        public long IdTransaccion { get; set; }
        public long IdCuota { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public string DocumentoActorComercial { get; set; }
        public int IdActorComercial { get; set; }
        public string ActorComercial { get; set; }
        public string CodigoCuota { get; set; }
        public decimal Saldo { get; set; }
        public string TipoComprobante { get; set; }
        public string CodigoDocumento { get; set; }
        public string Comprobante { get; set; }
        public string Emitida { get; set; }
        public string Vence { get; set; }
        public string CodigoUnico { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public bool AccionCobrarPagar { get; set; }

        public BandejaCobrarYPagarViewModel()
        {
        }

        public BandejaCobrarYPagarViewModel(CuentaPorCobrarPagar cuenta)
        {
            this.IdCuota = cuenta.Id;
            this.CodigoCuota = cuenta.Codigo;
            this.IdTransaccion = cuenta.IdOperacionBase;
            this.IdTipoTransaccion = cuenta.OperacionBase().TipoTransaccion().Id;
            this.TipoTransaccion = cuenta.OperacionBase().TipoTransaccionSuperior().Nombre;
            this.IdActorComercial = cuenta.ClienteProveedor().Id;
            this.ActorComercial = cuenta.ClienteProveedor().RazonSocial;
            this.DocumentoActorComercial = cuenta.ClienteProveedor().DocumentoIdentidad;
            this.Comprobante = cuenta.OperacionBase().Comprobante().NumeroDeSerie + "-" + cuenta.OperacionBase().Comprobante().NumeroDeComprobante;
            this.TipoComprobante = cuenta.OperacionBase().Comprobante().NombreTipo;
            this.CodigoDocumento = cuenta.OperacionBase().Comprobante().Tipo().Codigo;
            this.Saldo = cuenta.Saldo();
            this.Emitida = cuenta.FechaDeEmision.ToString("dd/MM/yyyy");
            this.Vence = cuenta.FechaDeVencimiento.ToString("dd/MM/yyyy");
            this.Total = cuenta.Total();
            this.AccionCobrarPagar = Saldo > 0;
        }

        public static List<BandejaCobrarYPagarViewModel> Convert(List<CuentaPorCobrarPagar> lista)
        {
            List<BandejaCobrarYPagarViewModel> respuesta = new List<BandejaCobrarYPagarViewModel>();
            foreach (var cuenta in lista)
            {
                respuesta.Add(new BandejaCobrarYPagarViewModel(cuenta));
            }
            return respuesta;
        }

    }

    
}
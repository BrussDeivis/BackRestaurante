using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class SolicitudAnticipoYDevolucionViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdAgrupadorAnticipo { get; set; }
        public int IdAnticipo { get; set; }
        //public long IdOrdenAnticipo { get; set; }
        public string NumeroSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string IdTipoSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public ComboActorGenerico Solicitante { get; set; }
        public ComboActorGenerico Benefactor { get; set; }
        public ComboActorGenerico Beneficiario { get; set; }
        public ComboGenerico Moneda { get; set; }
       // public List<CuentaBancoViewModel> Cuentas { get; set; }
       // public List<JustificanteViewModel> Detalles { get; set; }
        public decimal Total { get; set; }

        public string CodigoTipoSolicitud { get; set; }
        public string CodigoTipoSolicitudNumeroReferencia { get; set; }
      

        public SolicitudAnticipoYDevolucionViewModel()
        {

        }
        //public SolicitudAnticipoYDevolucionViewModel(SolicitudDeAnticipo s)
        //{
        //    this.Id = s.Id;
        //    this.IdAgrupadorAnticipo = s.anticipo().anticipoAgrupador().Id;
        //    this.IdAnticipo = s.anticipo().Id;
        //    this.NumeroSolicitud = s.NumeroSolicitud;
        //    this.EstadoSolicitud = s.EstadoActual.nombre;
        //    this.FechaSolicitud = s.FechaSolicitud;
        //    this.IdTipoSolicitud = TipoSolicitud.Anticipo.ToString();
        //    this.CodigoTipoSolicitud = s.anticipo().anticipoAgrupador().Codigo;
        //    this.Moneda = new ComboGenerico(s.moneda().Id, s.moneda().Codigo);
        //    this.Solicitante = new ComboActorGenerico(s.Solicitante().Id, s.Solicitante().DocumentoIdentidad,
        //        (s.Solicitante().ApellidoPaterno + " " + s.Solicitante().ApellidoMaterno + "," + s.Solicitante().Nombres),
        //         s.Solicitante().ApellidoPaterno);
        //    this.Benefactor = new ComboActorGenerico(s.Benefactor().Id, s.Benefactor().DocumentoIdentidad, s.Benefactor().RazonSocial, s.Benefactor().NombreCorto); ;
        //    this.Beneficiario = new ComboActorGenerico(s.Beneficiario().Id, s.Beneficiario().DocumentoIdentidad, s.Beneficiario().RazonSocial, s.Beneficiario().NombreCorto); ;

        //   this.Cuentas = CuentaBancoViewModel.convert(s.cuentasBancos());
        //   this.Detalles = JustificanteViewModel.convert(s.anticipo().anticipoAgrupador().justificantesAnticipo());
        //   this.Total = s.Total;
        //}
        //public SolicitudAnticipoYDevolucionViewModel(SolicitudDeDevolucionAnticipo s)
        //{
        //    this.Id = s.Id;
        //    this.IdAgrupadorAnticipo = s.anticipo().devolucionAnticipo().Id;
        //    this.IdAnticipo = s.anticipo().Id;
        //    this.NumeroSolicitud = s.NumeroSolicitud;
        //    this.EstadoSolicitud = s.EstadoActual.nombre;
        //    this.FechaSolicitud = s.FechaSolicitud;
        //    this.IdTipoSolicitud = TipoSolicitud.Devolucion.ToString();
        //    this.CodigoTipoSolicitud = s.anticipo().devolucionAnticipo().Codigo;
        //    this.CodigoTipoSolicitud = s.anticipo().anticipoAgrupador().Codigo;
        //    this.Moneda = new ComboGenerico(s.moneda().Id, s.moneda().Codigo);
        //    this.Solicitante = new ComboActorGenerico(s.Solicitante().Id, s.Solicitante().DocumentoIdentidad,
        //        (s.Solicitante().ApellidoPaterno + " " + s.Solicitante().ApellidoMaterno + "," + s.Solicitante().Nombres),
        //        s.Solicitante().Nombres.Substring(0, 1) + s.Solicitante().ApellidoPaterno);
        //    this.Beneficiario = new ComboActorGenerico(s.Benefactor().Id, s.Benefactor().DocumentoIdentidad, s.Benefactor().RazonSocial, s.Benefactor().NombreCorto); ;
        //    this.Benefactor = new ComboActorGenerico(s.Beneficiario().Id, s.Beneficiario().DocumentoIdentidad, s.Beneficiario().RazonSocial, s.Beneficiario().NombreCorto); ;

        //    this.Cuentas = CuentaBancoViewModel.convert(s.cuentasBancos());
        //    this.Detalles = JustificanteViewModel.convert(s.anticipo().anticipoAgrupador().justificantesAnticipo());
        //    this.Total = s.Total;
        //}
        //public List<SolicitudAnticipoYDevolucionViewModel> convert(List<SolicitudDeAnticipo> list)
        //{
        //    List<SolicitudAnticipoYDevolucionViewModel> respuesta = new List<SolicitudAnticipoYDevolucionViewModel>();
        //    foreach (var item in list)
        //    {
        //        respuesta.Add(new SolicitudAnticipoYDevolucionViewModel(item));
        //    }
        //    return respuesta;
        //}
        //public List<SolicitudAnticipoYDevolucionViewModel> convert(List<SolicitudDeDevolucionAnticipo> list)
        //{
        //    List<SolicitudAnticipoYDevolucionViewModel> respuesta = new List<SolicitudAnticipoYDevolucionViewModel>();
        //    foreach (var item in list)
        //    {
        //        respuesta.Add(new SolicitudAnticipoYDevolucionViewModel(item));
        //    }
        //    return respuesta;
        //}
    }
}
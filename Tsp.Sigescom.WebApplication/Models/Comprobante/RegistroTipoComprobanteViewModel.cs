using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    public class RegistroTipoComprobanteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string CodigoSunat { get; set; }
        public List<int> IdDeTiposDeTransaccionConEmisionPropia { get; set; }
        public List<int> IdDeTiposDeTransaccionConEmisionTerceros { get; set; }
        public List<TipoDeTransaccionTipoDeComprobanteViewModel> TiposDeTransaccionConEmisionPropia { get; set; }
        public List<TipoDeTransaccionTipoDeComprobanteViewModel> TiposDeTransaccionConEmisionTerceros { get; set; }

        public RegistroTipoComprobanteViewModel()
        {
            this.IdDeTiposDeTransaccionConEmisionPropia = new List<int>();
            this.IdDeTiposDeTransaccionConEmisionTerceros = new List<int>();
            this.TiposDeTransaccionConEmisionPropia = new List<TipoDeTransaccionTipoDeComprobanteViewModel>();
            this.TiposDeTransaccionConEmisionTerceros = new List<TipoDeTransaccionTipoDeComprobanteViewModel>();
        }

        public RegistroTipoComprobanteViewModel(TipoDeComprobante tipoDeComprobante)
        {
            this.Id = tipoDeComprobante.Id;
            this.Nombre = tipoDeComprobante.Nombre;
            this.NombreCorto = tipoDeComprobante.Valor;
            this.CodigoSunat = tipoDeComprobante.Codigo;
            this.IdDeTiposDeTransaccionConEmisionPropia = tipoDeComprobante.IdDeTiposDeTransaccionesConEmisionPropia();
            this.IdDeTiposDeTransaccionConEmisionTerceros = tipoDeComprobante.IdDeTiposDeTransaccionesConEmisionTerceros();
            this.TiposDeTransaccionConEmisionPropia = TipoDeTransaccionTipoDeComprobanteViewModel.Convert(tipoDeComprobante.TipoComprobanteParaTransaccionTiposDeTransaccionsConEmisionPropia());
            this.TiposDeTransaccionConEmisionTerceros = TipoDeTransaccionTipoDeComprobanteViewModel.Convert(tipoDeComprobante.TipoComprobanteParaTransaccionTiposDeTransaccionsConEmisionTerceros());
        }
    }
}
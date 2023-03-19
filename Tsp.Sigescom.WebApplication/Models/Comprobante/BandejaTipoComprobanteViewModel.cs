using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    [Serializable]
    [DataContract]
    public class BandejaTipoComprobanteViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string CodigoSunat { get; set; }
        public string TransaccionesConEmisionPropia { get; set; }
        public string TransaccionesConEmisionDeTerceros { get; set; }
    
        public BandejaTipoComprobanteViewModel()
        {

        }

        public BandejaTipoComprobanteViewModel(TipoDeComprobante tipoDeComprobante)
        {
            this.Id = tipoDeComprobante.Id;
            this.Nombre = tipoDeComprobante.Nombre;
            this.NombreCorto = tipoDeComprobante.Valor;
            this.CodigoSunat = tipoDeComprobante.Codigo;
            this.TransaccionesConEmisionPropia = TipoDeTransaccionTipoDeComprobanteViewModel.ConcatenarTiposDeTransacciones(TipoDeTransaccionTipoDeComprobanteViewModel.TiposDeTransaccionesConEmisionPropia(tipoDeComprobante.TiposDeTransaccionesConEmisionPropia()));
            this.TransaccionesConEmisionDeTerceros = TipoDeTransaccionTipoDeComprobanteViewModel.ConcatenarTiposDeTransacciones(TipoDeTransaccionTipoDeComprobanteViewModel.TiposDeTransaccionesConEmisionTerceros(tipoDeComprobante.TiposDeTransaccionesConEmisionTerceros()));
              
        }


        public static List<BandejaTipoComprobanteViewModel> Convert(List<TipoDeComprobante> tiposDeComprobante)
        {
            List<BandejaTipoComprobanteViewModel> tiposDeComprobanteViewModel = new List<BandejaTipoComprobanteViewModel>();

            foreach (var item in tiposDeComprobante)
            {
                tiposDeComprobanteViewModel.Add(new BandejaTipoComprobanteViewModel(item));
            }
            return tiposDeComprobanteViewModel;
        }


    }
}
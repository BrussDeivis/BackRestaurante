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
    public class BandejaSerieDeComprobanteViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string EstablecimientoComercial { get; set; }
        public string CentroDeAtencion { get; set; }
        public string TipoDeComprobante { get; set; }
        public string NumeroDeSerie { get; set; }
        public int NumeroDeComprobanteSiguiente { get; set; }
        public bool Autonumerica { get; set; }
        public bool Vigente { get; set; }


        public BandejaSerieDeComprobanteViewModel()
        {

        }

        public BandejaSerieDeComprobanteViewModel(SerieDeComprobante serieDeComprobante)
        {
            this.Id = serieDeComprobante.Id;
            this.EstablecimientoComercial = serieDeComprobante.CentroDeAtencion().EstablecimientoComercial != null ? serieDeComprobante.EstablecimientoComercial().NombreInterno : "";
            this.CentroDeAtencion = serieDeComprobante.CentroDeAtencion().Nombre;
            this.TipoDeComprobante = serieDeComprobante.Comprobante().Nombre;
            this.NumeroDeSerie = serieDeComprobante.Numero;
            this.NumeroDeComprobanteSiguiente = serieDeComprobante.ProximoNumeroDeComprobante;
            this.Autonumerica = serieDeComprobante.EsAutonumerable;
            this.Vigente = serieDeComprobante.Vigente;
        }

        public static List<BandejaSerieDeComprobanteViewModel> Convert(List<SerieDeComprobante> seriesDeComprobante)
        {
            List<BandejaSerieDeComprobanteViewModel> seriesDeComprobanteViewModel = new List<BandejaSerieDeComprobanteViewModel>();

            foreach (var item in seriesDeComprobante)
            {
                seriesDeComprobanteViewModel.Add(new BandejaSerieDeComprobanteViewModel(item));
            }
            return seriesDeComprobanteViewModel;
        }

    }
}
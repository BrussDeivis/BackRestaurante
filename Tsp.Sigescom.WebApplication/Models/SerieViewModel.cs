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
    public class SerieViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string IdTipoDeComprobante { get; set; }
        public string TipoDeComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public string IdSede { get; set; }
        public string Sede { get; set; }
        public bool Autonumerico { get; set; }
        public int NumeroSiguiente { get; set; }
        public bool Vigente { get; set; }
        //public string ProximoNumero { get; set; }

        public SerieViewModel()
        {
        }

        public SerieViewModel(Serie_comprobante s)
        {
            this.Id = s.id;
            this.NumeroSerie = s.numero;
            this.Autonumerico = s.es_autonumerable;
        }
        public SerieViewModel(SerieDeComprobante s)
        {
            this.Id = s.Id;
            this.IdTipoDeComprobante = s.Comprobante().Id.ToString();
            this.TipoDeComprobante = s.Comprobante().Nombre;
            this.NumeroSerie = s.Numero;
            this.IdSede = s.CentroDeAtencion().Id.ToString();
            this.Sede = s.CentroDeAtencion().Nombre;
            this.Autonumerico = s.EsAutonumerable;
            this.NumeroSiguiente = s.ProximoNumeroDeComprobante;
            this.Vigente = s.Vigente;
        }

        public SerieViewModel(int id,string numero,bool autonumerico)
        {
            this.Id = id;
            this.NumeroSerie = numero;
            this.Autonumerico = autonumerico;
        }
        public static List<SerieViewModel> convert(List<Serie_comprobante> series)
        {
            var ListaSeries = new List<SerieViewModel>();

            foreach (var serie in series)
            {
                ListaSeries.Add(new SerieViewModel(serie));
            }
            return ListaSeries;
        }
        public static List<SerieViewModel> Convert(List<SerieDeComprobante> series)
        {
            var ListaSeries = new List<SerieViewModel>();

            foreach (var serie in series)
            {
                ListaSeries.Add(new SerieViewModel(serie));
            }
            return ListaSeries;
        }
    }
}
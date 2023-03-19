using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    public class RegistroSerieDeComprobanteViewModel
    {
        public int  Id { get; set; }
        public ComboGenericoViewModel EstablecimientoComercial { get; set; }
        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public ComboGenericoViewModel TipoDeComprobante { get; set; }
        public string NumeroDeSerie { get; set; }
        public int NumeroDeComprobanteSiguiente { get; set; }
        public bool Autonumerica { get; set; }
        public bool Vigente { get; set; }

        public RegistroSerieDeComprobanteViewModel()
        {

        }

        public RegistroSerieDeComprobanteViewModel(SerieDeComprobante serieDeComprobante)
        {
            this.Id = serieDeComprobante.Id;
            this.EstablecimientoComercial = new ComboGenericoViewModel(serieDeComprobante.EstablecimientoComercial().Id, serieDeComprobante.EstablecimientoComercial().NombreInterno);
            this.CentroDeAtencion = new ComboGenericoViewModel(serieDeComprobante.CentroDeAtencion().Id,serieDeComprobante.CentroDeAtencion().Nombre);
            this.TipoDeComprobante = new ComboGenericoViewModel(serieDeComprobante.Comprobante().Id,serieDeComprobante.Comprobante().Nombre);
            this.NumeroDeSerie = serieDeComprobante.Numero;
            this.NumeroDeComprobanteSiguiente = serieDeComprobante.ProximoNumeroDeComprobante;
            this.Autonumerica = serieDeComprobante.EsAutonumerable;
            this.Vigente = serieDeComprobante.Vigente;
            
        }
    }
}
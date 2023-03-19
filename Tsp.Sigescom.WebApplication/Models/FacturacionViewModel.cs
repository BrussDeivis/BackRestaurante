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
    public class FacturacionViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public ComboGenerico TipoDeTercero { get; set; }
        public ComboActorGenerico Tercero { get; set; }
        public ComboGenerico TipoDeComprobante { get; set; }
        public SerieViewModel SerieComprobante { get; set; }
        public string FechaEmision { get; set; }
        public string Concepto { get; set; }
        public ComboGenerico Moneda { get; set; }
        public ComboGenerico TipoPago { get; set; }
        public string Glosa { get; set; }
        public string Observacion { get; set; }

        public FacturacionViewModel()
        {

        }

        public FacturacionViewModel(OperacionDeVenta ov)
        {
            //this.Id = ov.Id;
            //this.Tercero = new ComboActorGenerico(ov.Venta().Cliente().Id, ov.Venta().Cliente().DocumentoIdentidad, ov.Venta().Cliente().RazonSocial);
            //this.TipoDeComprobante = new ComboGenerico(ov.Comprobante().IdTipo,ov.Comprobante().NombreTipo,ov.Comprobante().CodigoTipo);
            //this.SerieComprobante = new SerieViewModel((int)ov.Comprobante().IdSerie,ov.Comprobante().NumeroDeSerie,ov.Comprobante().Serie().es_autonumerable);
            //this.FechaEmision = ov.FechaEmision.ToString("dd/MM/yyyy");
            //this.Moneda = new ComboGenerico(ov.IdMoneda,ov.CodigoMoneda);
        }

        public static new List<FacturacionViewModel> convert(List<OperacionDeVenta> documentos)
        {
            var documentosDeVenta = new List<FacturacionViewModel>();

            foreach (var documento in documentos)
            {
                documentosDeVenta.Add(new FacturacionViewModel(documento));
            }
            return documentosDeVenta;
        }
    }
}
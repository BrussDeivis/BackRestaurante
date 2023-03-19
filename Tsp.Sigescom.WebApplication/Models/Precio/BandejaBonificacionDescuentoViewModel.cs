using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaBonificacionDescuentoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Valor { get; set; }
        public string CantidadMaxima { get; set; }
        public string CantidadMinima { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string ProductoReferencia { get; set; }
        public bool EsVigente { get; set; }

        public BandejaBonificacionDescuentoViewModel(Precio precio)
        {
            this.Id = precio.id;
            this.Producto = precio.Concepto_negocio1.nombre;
            this.Valor = precio.valor.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
            this.CantidadMinima = precio.cantidad_minima.ToString();
            this.CantidadMaxima = precio.cantidad_maxima.ToString();
            this.FechaDesde = precio.fecha_inicio.ToString("dd/MM/yyyy");
            this.FechaHasta = precio.fecha_fin.ToString("dd/MM/yyyy");
            this.EsVigente = precio.es_vigente;
            this.ProductoReferencia = precio.Concepto_negocio != null ? precio.Concepto_negocio.nombre : "";
        }

        public static List<BandejaBonificacionDescuentoViewModel> Convert(List<Precio> precios)
        {
            var precios_ = new List<BandejaBonificacionDescuentoViewModel>();

            foreach (var precio in precios)
            {
                precios_.Add(new BandejaBonificacionDescuentoViewModel(precio));
            }
            return precios_;
        }
    }
    

}
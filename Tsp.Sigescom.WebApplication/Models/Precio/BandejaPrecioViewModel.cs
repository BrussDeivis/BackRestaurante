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
    public class BandejaPrecioViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Tarifa { get; set; }
        public string Precio { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public bool EsPrecioVigente { get; set; }

        public BandejaPrecioViewModel(Precio precio)
        {
            this.Id = precio.id;
            this.Producto = precio.Concepto_negocio1.nombre;
            this.Tarifa = precio.Detalle_maestro3.nombre;
            this.Precio = precio.valor.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
            this.FechaDesde = precio.fecha_inicio.ToString("dd/MM/yyyy");
            this.FechaHasta = precio.fecha_fin.ToString("dd/MM/yyyy");
            this.EsPrecioVigente = precio.es_vigente;
        }

        public static List<BandejaPrecioViewModel> Convert(List<Precio> precios)
        {
            var precios_ = new List<BandejaPrecioViewModel>();

            foreach (var precio in precios)
            {
                precios_.Add(new BandejaPrecioViewModel(precio));
            }
            return precios_;
        }
    }
    

}
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BonificacionDescuentoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public ComboGenericoViewModel Mercaderia { get; set; }
        public ComboGenericoViewModel MercaderiaReferencia { get; set; }
        public decimal Valor { get; set; }
        public int CantidadMaxima { get; set; }
        public int CantidadMinima { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public bool Porcentaje { get; set; }
        public string Descripcion { get; set; }


        public BonificacionDescuentoViewModel()
        {

        }

        public BonificacionDescuentoViewModel(Precio precio)
        {
            this.Id = precio.id;
            this.Mercaderia = new ComboGenericoViewModel(precio.id_concepto_negocio, precio.Concepto_negocio1.nombre);
            this.MercaderiaReferencia = new ComboGenericoViewModel((int)precio.id_concepto_negocio_referencial, precio.Concepto_negocio.nombre);
            this.Valor = precio.valor;
            this.CantidadMinima = (int)precio.cantidad_minima;
            this.CantidadMaxima = (int)precio.cantidad_maxima;
            this.FechaDesde = precio.fecha_inicio;
            this.FechaHasta = precio.fecha_fin;
            this.Porcentaje = (bool)precio.porcentaje;
            this.Descripcion = precio.descripcion;
        }

    }
    

}
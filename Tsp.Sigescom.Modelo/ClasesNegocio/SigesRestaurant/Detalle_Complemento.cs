using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{

    public class Detalle_Item_Restaurante
    {
        public string AnotacionIndicacion { get; set; }
        public string AnotacionObservacion { get; set; }
        public List<Detalle_Complemento> DetallesComplemento { get; set; }
        //Detalle_Complemento

        public Detalle_Item_Restaurante()
        { }
    }
    public class Detalle_Complemento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Item_Complemento> ItemsComplemento { get; set; }//Detalles_Complemento
        //public string DetalleComplementoJson { get { return "{ id: " + this.Id + " , nombre:" + this.Nombre + "}"; } }

        public Detalle_Complemento()
        { }

        //public Detalle_Complemento(Valor_caracteristica detalleComplemento)
        //{

        //    this.Id = detalleComplemento.id;
        //    this.Nombre = detalleComplemento.valor;
        //}

        //public static List<Detalle_Complemento> Convert(List<Valor_caracteristica> valoresCaracteristica)
        //{
        //    List<Detalle_Complemento> items = new List<Detalle_Complemento>();
        //    foreach (var detalleComplemento in valoresCaracteristica)
        //    {
        //        items.Add(new Detalle_Complemento(detalleComplemento));
        //    }
        //    return items;
        //}
    }

    public class Item_Complemento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //public string DetalleComplementoJson { get { return "{ id: " + this.Id + " , nombre:" + this.Nombre + "}"; } }
        public Item_Complemento()
        { }
    }
}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class Complemento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool MostrarFamilia { get; set; }
        public int NumeroComplementos { get; set; }
        public string Familia { get; set; }
        public IEnumerable<Item_Complemento> Detalles_Complemento { get; set; }
        public bool EsMultiple { get; set; }
        public int Maximo { get; set; }
        public bool EstaActivoRestaurante { get; set; }

        public Complemento()
        { }
        //public Complemento(Detalle_maestro detalle){
        //    this.Id = detalle.id;
        //    this.Nombre = detalle.nombre;
        //    this.Maximo = 0;
        //    this.Detalles_Complemento = null;
        //}



        //public static List<Complemento> Convert()
        //{
        //    return null;
        //}
    }


}

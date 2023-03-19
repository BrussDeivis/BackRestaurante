using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class Familia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<Complemento> Complementos {get;set;}

        public Familia()
        {
        
        }
        public Familia(Detalle_maestro detalleMaestro)
        {
            Id = detalleMaestro.id;
            Nombre = detalleMaestro.nombre;
        }

        public static List<Familia> Convert(List<Detalle_maestro> detalle)
        {
            return null;
        }
    }


}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EstadoOcupado { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public int IdAmbiente { get; set; }
        public string NombreAmbiente { get; set; }
        public string JsonData { get; set; }

        public Mesa()
        {}

        public Mesa(Actor_negocio actorNegocio)
        {
            Data_Mesa data = JsonConvert.DeserializeObject<Data_Mesa>(actorNegocio.extension_json);
            this.Id =  actorNegocio.id;
            this.EstadoOcupado = actorNegocio.indicador1;
            this.Nombre = data.nombre;
            this.Fila = data.fila;
            this.Columna = data.columna;
            this.IdAmbiente = (int)actorNegocio.id_actor_negocio_padre;
            this.NombreAmbiente = actorNegocio.Actor_negocio2.PrimerNombre;
        }



        public static List<Mesa> Convert(List<Actor_negocio> actoresNegocio)
        {
            List<Mesa> items = new List<Mesa>();
            foreach (var actorNegocio in actoresNegocio)
            {
                items.Add(new Mesa(actorNegocio));
            }
            return items;
        }
        
    }


}

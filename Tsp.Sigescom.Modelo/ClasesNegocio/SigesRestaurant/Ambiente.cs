using Newtonsoft.Json;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class Ambiente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Filas { get; set; }
        public int Columnas { get; set; }
        public bool MesasTemporales { get; set; }
        public ItemGenerico Establecimiento { get; set; }
        public Ambiente()
        {}

        public Ambiente(Actor_negocio actorNegocio)
        {
            Data_Ambiente data = JsonConvert.DeserializeObject<Data_Ambiente>(actorNegocio.extension_json);
           this.Id =  actorNegocio.id;
            this.Nombre = data.nombre;
            this.Filas = data.filas;
            this.Columnas = data.columnas;
            this.MesasTemporales = data.mesastemporales;
        }

        public static List<Ambiente> Convert(List<Actor_negocio> actorNegocio)
        {
            List<Ambiente> items = new List<Ambiente>();
            foreach (var actor in actorNegocio)
            {
                items.Add(new Ambiente(actor));
            }
            return items;
        }
    }


}

using Newtonsoft.Json;
using System;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom
{
    public class CentroDeAtencion
    {

        public int Id { get; set; }
        public int IdActor { get; set; }

        public EstablecimientoComercial EstablecimientoComercial { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string ExtensionJson { get; set; }
        public bool SalidaBienesSinStock { get => string.IsNullOrEmpty(ExtensionJson) ? false : JsonConvert.DeserializeObject<JsonCentroDeAtencion>(ExtensionJson).salidabienessinstock; }


        
        


        public CentroDeAtencion()
        {

        }


        public CentroDeAtencion(Actor_negocio actorDeNegocio)
        {
            this.Id = actorDeNegocio.id;
            this.Codigo = actorDeNegocio.codigo_negocio;
            this.Nombre = actorDeNegocio.Actor.primer_nombre;
        
        }

        public ItemGenerico ToItemGenerico()
        {
            return new ItemGenerico(Id, Nombre);
        }
    }
}

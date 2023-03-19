using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.Custom
{
    public class EstablecimientoComercialConLogo_:ActorComercial_
    {
        public string NombreInterno { get; set; }
        public bool EsSede { get; set; }
        public bool EsSucursal { get; set; }
        public int? IdCentroDeAtencionParaObtencionDePrecios { get; set; }
        public int? IdCentroDeAtencionParaObtencionDeStock { get; set; }
        public byte[] Logo { get; set; }
        public string JsonExtension { get; set; }
        public string CodigoEstablecimientoDigemid()
        {
            return JsonConvert.DeserializeObject<JsonEstablecimientoComercial>(JsonExtension).codigodigemid;
        }

        public ItemGenerico ToItemGenerico()
        {
            return new ItemGenerico(Id, NombreCorto);
        }

        public Establecimiento ToEstablecimiento()
        {
            return new Establecimiento(Id, NombreCorto);
        }
    }
}

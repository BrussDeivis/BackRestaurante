using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Huesped : ActorComercial_
    {
        public int IdHuesped { get; set; }
        public ItemGenerico MotivoDeViaje { get; set; }
        public string JsonHuesdep { get; set; }
        public bool EsTitular { get; set; }

        public Huesped() { }
    }
}

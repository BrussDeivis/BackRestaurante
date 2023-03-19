using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class AmbienteHotel
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public ItemGenerico Establecimiento { get; set; }
        public bool EsVigente { get; set; }

        public AmbienteHotel()
        {

        }
    }
}

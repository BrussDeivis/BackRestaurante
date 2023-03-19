using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Complemento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ItemGenerico> Valores { get; set; }
    }
}

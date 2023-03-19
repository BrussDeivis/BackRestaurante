using Newtonsoft.Json;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class CentroAtencionRestaurante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsPuntoDelivery { get; set; }
        public bool EsPuntoAlPaso { get; set; }
      
    }


}

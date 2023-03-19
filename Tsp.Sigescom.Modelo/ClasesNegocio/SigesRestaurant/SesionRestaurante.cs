using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class SesionRestaurante
    {
        public int IdCentroAtencion { get {return this.SesionDeUsuario.IdCentroDeAtencionSeleccionado; } }
        public UserProfileSessionData SesionDeUsuario { get; set; }
        public List<Ambiente> Ambientes { get; set; }
    }
}

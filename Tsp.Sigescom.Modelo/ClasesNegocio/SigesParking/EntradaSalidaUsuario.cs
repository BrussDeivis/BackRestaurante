using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class EntradaSalidaUsuario: EntradaSalida
    {
        public string DocumentoUsuario { get; set; }
        public string NombreUsuario { get; set; }


        public EntradaSalidaUsuario()
        {
        }

        public new List<EntradaSalidaUsuario> Convert()
        {
            return null;
        }
    }
}

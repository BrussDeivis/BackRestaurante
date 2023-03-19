using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Sucursal : EstablecimientoComercialExtendido
    {
        public Sucursal(Actor_negocio actorDeNegocio) : base(actorDeNegocio)
        {
        }


        public static List<Sucursal> Convert(List<Actor_negocio> actoresDeNegocio)
        {
            List<Sucursal> sucursales = new List<Sucursal>();

            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                sucursales.Add(new Sucursal(actorDeNegocio));
            }
            return sucursales;
        }


    }

}

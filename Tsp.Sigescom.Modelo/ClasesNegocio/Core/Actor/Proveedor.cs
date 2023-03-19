using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Proveedor : ActorComercial
    {
        public object ActorNegocio { get; set; }

        public Proveedor(Actor_negocio actorDeNegocio):base(actorDeNegocio)
        {
        }
        

        public List<Servicio> Servicios()
        {
                return Servicio.convert(this.ActorDeNegocio.Actor_negocio_rol.Select(anr => anr.Rol).ToList());
        }

        public static List<Proveedor> Convert(List<Actor_negocio> actoresDeNegocio)
        {
            var proveedores = new List<Proveedor>();

            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                proveedores.Add(new Proveedor(actorDeNegocio));
            }
            return proveedores;
        }

    }
   
}

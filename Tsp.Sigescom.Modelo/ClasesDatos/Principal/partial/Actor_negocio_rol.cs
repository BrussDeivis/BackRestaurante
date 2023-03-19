using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Actor_negocio_rol
    {
        public Actor_negocio_rol()
        {

        }
        public Actor_negocio_rol(int idRol)
        {
            this.id_rol = idRol;
        }
        public Actor_negocio_rol(int idActorDeNegocio, int idRol)
        {
            this.id_actor_negocio = idActorDeNegocio;
            this.id_rol = idRol;            
        }
    }
}

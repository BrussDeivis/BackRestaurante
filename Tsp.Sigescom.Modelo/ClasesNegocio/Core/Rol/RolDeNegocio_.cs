using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class RolDeNegocio_
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int AplicaA { get; set; }
        public RolDeNegocio_()
        {

        }
        public RolDeNegocio_(Rol rol)
        {
            this.Id = rol.id;
            this.Nombre = rol.nombre;
            this.Descripcion = rol.descripcion;
            this.AplicaA = (int)(rol.aplica_a??0);
        }

        public static List<RolDeNegocio_> Convert_(List<Rol> roles)
        {
            var rolesDeNegocio = new List<RolDeNegocio_>();

            foreach (var rol in roles)
            {
                rolesDeNegocio.Add(new RolDeNegocio_(rol));
            }
            return rolesDeNegocio;
        }

        
    }
}

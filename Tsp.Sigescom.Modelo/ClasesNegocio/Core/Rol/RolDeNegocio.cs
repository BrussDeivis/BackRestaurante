using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class RolDeNegocio
    {
        private Rol rol;

        public RolDeNegocio()
        {

        }
        public RolDeNegocio(Rol rol)
        {
            this.rol = rol;
        }

        //public RolDeNegocio(int id, string nombre, int descripcion, int aplicaA)
        //{
        //    //RolDeNegocio = new Rol(,idServicio, codigo, "", "", "", "", ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 0, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);
        //}



        public static List<RolDeNegocio> Convert_(List<Rol> roles)
        {
            var rolesDeNegocio = new List<RolDeNegocio>();

            foreach (var rolDeNegocio in roles)
            {
                rolesDeNegocio.Add(new RolDeNegocio(rolDeNegocio));
            }
            return rolesDeNegocio;
        }

        public int Id
        {
            get { return this.rol.id; }
        }

        public string Nombre
        {
            get { return this.rol.nombre; }
        }

        public string Descripcion
        {
            get { return this.rol.descripcion; }
        }

        public int AplicaA
        {
            get { return (int)this.rol.aplica_a; }
        }
    }
}

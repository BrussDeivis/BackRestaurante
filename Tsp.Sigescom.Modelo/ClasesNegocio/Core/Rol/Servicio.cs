using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Servicio
    {

        Rol rol;
        public Servicio()
        {

        }


        public Servicio(Rol rolServicio)
        {
            this.rol = rolServicio;
        }

        public int Id
        {
            get
            {
                return this.rol.id;
            }
        }


        public string Nombre
        {
            get
            {
                return this.rol.nombre;
            }
        }

        //public List<ConceptoDeServicio> obtenerConceptos()
        //{
        //    return ConceptoDeServicio.convert(this.rol.Concepto_negocio.Where(cn => cn.es_vigente).ToList()); 
        //}

        public static List<Servicio> convert(List<Rol> roles)
        {
            List<Servicio> servicios = new List<Servicio>();
            foreach (var rol in roles)
            {
                servicios.Add(new Servicio(rol));
            }

            return servicios;
        }
    }
}

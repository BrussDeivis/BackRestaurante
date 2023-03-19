using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Custom
{
    public class Empleado_
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        //public int IdUsuario { get; set; }
        public ItemGenerico TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombresYApellidos { get { return Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno; } }
        public string ApellidosYNombres { get { return ApellidoPaterno + " " + ApellidoMaterno + "," + Nombres; } }
        public IEnumerable<RolDeNegocio_> Roles { get; set; }
        public string NombreCorto { get; set; }
        

        public Empleado_()
        {
        }


        public Empleado_(Actor_negocio actorDeNegocio)
        {
            this.Id = actorDeNegocio.id;
            this.IdActor = actorDeNegocio.id_actor;
            this.Nombres = actorDeNegocio.PrimerNombre.Split('|')[2]; ;
            this.ApellidoPaterno = actorDeNegocio.PrimerNombre.Split('|')[0]; ;
            this.ApellidoMaterno = actorDeNegocio.PrimerNombre.Split('|')[1]; ;
            this.NumeroDocumentoIdentidad = actorDeNegocio.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = new ItemGenerico() { Id = actorDeNegocio.IdDocumentoIdentidad, Nombre = actorDeNegocio.Actor.Detalle_maestro.nombre };
            this.Roles = RolDeNegocio_.Convert_(actorDeNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente == true).Select(an => an.Rol).ToList());
        }
        public Actor_negocio Convert()
        {
            return new Actor_negocio
            {
                id = this.Id,
                Actor = new Actor()
                {
                    primer_nombre = this.ApellidoPaterno + "|" + ApellidoMaterno + "|" + Nombres,
                    segundo_nombre = this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno,
                    tercer_nombre = this.Nombres + " " + this.ApellidoPaterno.Substring(0, 1) + ". " + this.ApellidoMaterno.Substring(0, 1) + ".",
                    Detalle_maestro = new Detalle_maestro()
                    {
                        id = this.TipoDocumentoIdentidad.Id,
                        nombre = this.TipoDocumentoIdentidad.Nombre
                    }
                }
            };
            //this.Id = actorDeNegocio.id;
            //this.IdActor = actorDeNegocio.id_actor;
            //this.Nombres = actorDeNegocio.PrimerNombre;
            //this.ApellidoPaterno = actorDeNegocio.SegundoNombre;
            //this.ApellidoMaterno = actorDeNegocio.TercerNombre;
            //this.NumeroDocumentoIdentidad = actorDeNegocio.DocumentoIdentidad;
            //this.TipoDocumentoIdentidad = new ItemGenerico() { Id = actorDeNegocio.IdDocumentoIdentidad, Nombre = actorDeNegocio.Actor.Detalle_maestro.nombre };
        }

        public bool TieneRol(int idRol)
        {
            return Roles.Select(r => r.Id).Contains(idRol);
        }
    }
}

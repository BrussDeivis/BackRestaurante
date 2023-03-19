using Tsp.Sigescom.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Empleado
    {
        private Actor_negocio actorDeNegocio;
        //private List<Rol> roles;

        public Empleado(Actor_negocio actorDeNegocio) 
        {
            this.actorDeNegocio = actorDeNegocio;
        }

        public int Id
        {
            get { return this.actorDeNegocio.id; }
        }

        public string IdUsuario
        {
            get { return this.actorDeNegocio.id_usuario; }
        }

        public string ApellidoPaterno
        {
            get { return this.actorDeNegocio.PrimerNombre.Split('|')[0]; }
        }
        public string ApellidoMaterno
        {
            get { return this.actorDeNegocio.PrimerNombre.Split('|')[1]; }
        }
        public string NombreCompleto
        {
            get { return this.actorDeNegocio.SegundoNombre; }
        }
        /// <summary>
        /// Nombres e iniciales de los apellidos
        /// </summary>
        public string NombreCorto
        {
            get { 
                var nombreArray = this.actorDeNegocio.PrimerNombre.Split('|');
                return (nombreArray[2] + " " + nombreArray[0].Substring(0, 1) + ". " + nombreArray[1].Substring(0, 1) + ".");
            }
        }
        public string Nombres
        {
            get { return this.actorDeNegocio.PrimerNombre.Split('|')[2]; }
        }
        public int IdTipoPersona
        {
            get { return this.actorDeNegocio.IdTipoActor; }
        }
        public int IdEstadoLegal
        {
            get { return this.actorDeNegocio.IdEstadoLegal; }
        }
        public int IdClaseActor
        {
            get { return this.actorDeNegocio.IdClaseActor; }
        }
        public string TipoPersona
        {
            get { return this.actorDeNegocio.Actor.Tipo_actor.nombre; }
        }
        public string Codigo
        {
            get { return this.actorDeNegocio.codigo_negocio; }
        }

        public int IdTipoDocumentoIdentidad
        {
            get { return this.actorDeNegocio.IdDocumentoIdentidad; }
        }
        public string TipoDocumento
        {
            get { return this.actorDeNegocio.Actor.Detalle_maestro.valor; }
        }

        public string DocumentoIdentidad
        {
            get { return this.actorDeNegocio.DocumentoIdentidad; }
        }

        public bool EsVigente
        {
            get { return this.actorDeNegocio.es_vigente; }
            set { this.actorDeNegocio.es_vigente = value; }
        }

        public DateTime FechaInicio
        {
            get { return this.actorDeNegocio.fecha_inicio; }
            set { this.actorDeNegocio.fecha_inicio = value; }
        }

        public DateTime FechaFin
        {
            get { return this.actorDeNegocio.fecha_fin; }
            set { this.actorDeNegocio.fecha_fin = value; }
        }
        public DateTime FechaNacimiento()
        {
            return this.actorDeNegocio.FechaNacimiento;
        }
        public string Correo()
        {
            return this.actorDeNegocio.Actor.correo;
        }
        public string Telefono()
        {
            return this.actorDeNegocio.Actor.telefono;
        }
        //public Direccion DomicilioPersonal()
        //{
        //    return this.actorDeNegocio.Actor.Direccion.SingleOrDefault(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal);
        //}

        public bool TieneRol(int idRol)
        {
            return this.ObtenerRolesHijosVigentes().
                Select(r => r.id).Contains(idRol);
        }
        public Direccion DomicilioPersonal()
        {
           return  this.actorDeNegocio.Actor.Direccion.OrderByDescending(d => d.id_tipo_direccion).FirstOrDefault(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal);
        }
        public Direccion DomicilioFiscal()
        {
            return this.actorDeNegocio.Actor.Direccion.OrderByDescending(d => d.id_tipo_direccion).FirstOrDefault(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal);

        }
        public Clase_actor Sexo()
        {
            return this.actorDeNegocio.Actor.Clase_actor;
        }
        public Estado_legal EstadoLegal()
        {
            return this.actorDeNegocio.Actor.Estado_legal;
        }
        public List<Direccion> Direcciones()
        {
            var hayDireccion = this.actorDeNegocio.Actor.Direccion.Any();
            return hayDireccion ? this.actorDeNegocio.Actor.Direccion.ToList() : null;
        }

        public bool HayUsuario()
        {
            bool resultado = this.actorDeNegocio.id_usuario != null ? true : false;
            return resultado;
        }

       

        public Actor_negocio ActorNegocio
        {
            get { return actorDeNegocio; }
        }

        

        /// <summary>
        /// devuelve los roles hijos del rol empleado a los que se encuentra asociado el empleado.
        /// </summary>
        /// <returns></returns>
        public List<Rol> ObtenerRolesHijosVigentes()
        {
            return this.actorDeNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente==true).Select(an => an.Rol).ToList();
             
            //return this.actorDeNegocio.Actor.Actor_negocio.Where(an=>an.Rol.id_rol_padre==idRolEmpleado).Select(an=>an.Rol).ToList();
        }

        public string cargo()
        {
            return ObtenerRolesHijosVigentes().First().nombre;
        }
        public int[] ObtenerAccionesPosibles(int idTipoTransaccion)
        {
            return ObtenerRolesHijosVigentes().SelectMany(r => r.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion).Select(apr => apr.id_accion_posible)).Distinct().ToArray();
                
             
            //return this.actorDeNegocio.Actor.Actor_negocio.Where(an=>an.Rol.id_rol_padre==idRolEmpleado).Select(an=>an.Rol).ToList();
        }

        public static List<Empleado> Convert(List<Actor_negocio> actoresDeNegocio)
        {
            var empleados = new List<Empleado>();

            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                empleados.Add(new Empleado(actorDeNegocio));
            }
            return empleados;
        }

    }

}

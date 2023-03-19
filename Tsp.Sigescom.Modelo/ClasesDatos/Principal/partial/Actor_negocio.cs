using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Actor_negocio
    {
        /// <summary>
        /// aqui se almacenan temporalmente los parametros definitivos de un actor de negocio, para luego ser persistidos en la capa de datos
        /// </summary>
        public List<Parametro_actor_negocio> nuevosParametros;
       
        public void SetData(int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool indicador1, string extension_json)
        {
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }

        public void SetData(int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, bool indicador1, string extension_json)
        {
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.es_vigente = esVigente;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }

        public void SetData(int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool indicador1, string extension_json)
        {
            this.id_actor = idActor;
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }

        public void SetData(int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, bool indicador1, string extension_json)
        {
            this.id_actor = idActor;
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.es_vigente = esVigente;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }

        public void SetData( int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, int idActorNegocioPadre, bool indicador1, string extension_json)
        {
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.es_vigente = esVigente;
            this.id_actor_negocio_padre = idActorNegocioPadre;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }

        public void SetData(int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente,int idActorNegocioPadre, bool indicador1, string extension_json)
        {
            this.id_actor = idActor;
            this.id_rol = idRol;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.codigo_negocio = codigo;
            this.es_vigente = esVigente;
            this.id_actor_negocio_padre = idActorNegocioPadre;
            this.indicador1 = indicador1;
            this.extension_json = extension_json;
        }



        public Actor_negocio(int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, bool indicador1, string extension_json)
        {
            try
            {
                InitSets();
                if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }
                SetData(idRol, fechaInicio, fechaFin, codigo, esVigente, indicador1, extension_json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        public Actor_negocio(int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool indicador1, string extension_json)
        {
            try
            {
                InitSets();
                if (idActor < 1) { throw new IdNoValidoException(idActor, "Actor"); }
                if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }
                this.id_actor = idActor;
                SetData(idRol, fechaInicio, fechaFin, codigo, indicador1, extension_json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Utilizado para actualizar empleado, cliente , proveedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idActor"></param>
        /// <param name="idRol"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="codigo"></param>
        /// <param name="esVigente"></param>
        public Actor_negocio(int id, int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, bool indicador1, string extension_json)
        {
            try
            {
                InitSets();
                if (idActor < 1) { throw new IdNoValidoException(idActor, "Actor"); }
                if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }
                this.id_actor = idActor;
                this.id = id;
                SetData(idRol, fechaInicio, fechaFin, codigo, esVigente, indicador1, extension_json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Actor_negocio(int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, int idActorNegocioPadre, bool indicador1, string extension_json)
        {
            try
            {
                InitSets();
                if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }
                SetData(idRol, fechaInicio, fechaFin, codigo, esVigente, idActorNegocioPadre, indicador1, extension_json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// se crea un nuevo actor de negocio, instanciando un nuevo actor ya que no se recibe un id de Actor.
        /// se instancia la direccion fiscal para ese actor
        /// se asume que se ha validado previamente que no existe un actor con el Documento de identidad pasado por parametro.
        /// </summary>
        /// <param name="idActor"></param>
        /// <param name="idRol"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="codigo"></param>
        /// <param name="esVigente"></param>
        public Actor_negocio(int idActor, int idRol, DateTime fechaInicio, DateTime fechaFin, string codigo, bool esVigente, bool indicador1, string extension_json, int? idActorNegocioPadre)
        {
            try
            {
                InitSets();
                if (idActor < 1) { throw new IdNoValidoException(idActor, "Actor"); }
                if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }
                this.id_actor = idActor;
                this.id_actor_negocio_padre = idActorNegocioPadre;
                SetData(idRol, fechaInicio, fechaFin, codigo, esVigente, indicador1,extension_json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void InitSets()
        {
            this.Actor_negocio_rol = new HashSet<Actor_negocio_rol>();
            this.Parametro_actor_negocio = new HashSet<Parametro_actor_negocio>();
        }

        public string PrimerNombre
        {
            get { return this.Actor.primer_nombre; }
        }

        public string SegundoNombre
        {
            get { return this.Actor.segundo_nombre; }
        }

        public string TercerNombre
        {
            get { return this.Actor.tercer_nombre; }
        }

        public int IdDocumentoIdentidad
        {
            get { return this.Actor.id_documento_identidad; }
        }

        public string DocumentoIdentidad
        {
            get { return this.Actor.numero_documento_identidad; }
        }

        public int IdTipoActor
        {
            get { return this.Actor.id_tipo_actor; }
        }

        public int IdEstadoLegal
        {
            get { return this.Actor.id_estado_legal; }
        }

        public int IdClaseActor
        {
            get { return this.Actor.id_clase_actor; }
        }

        public DateTime FechaNacimiento
        {
            get { return this.Actor.fecha_nacimiento; }
        }

        public string Telefono
        {
            get { return this.Actor.telefono; }
        }

        public string Correo
        {
            get { return this.Actor.correo; }
        }

        public int IdDetalleMultiproposito
        {
            get { return (int)this.Actor.id_detalle_multiproposito; }
        }
    }
}

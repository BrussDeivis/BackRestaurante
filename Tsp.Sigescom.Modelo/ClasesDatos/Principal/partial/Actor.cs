using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
   public partial class Actor
    {

        public Actor(int idDocumentoIdentidad, DateTime fechaNacimiento, string numeroDocumento, string primerNombre, string segundoNombre, string telefono, int idTipoActor, int idFoto, int idClaseActor, int idEstadoLegal, string correo, string tercerNombre, string paginaWeb)
        {
            InitSets();
            ValidarIds(idDocumentoIdentidad, idTipoActor, idFoto, idClaseActor, idEstadoLegal);
            SetData(idDocumentoIdentidad, fechaNacimiento, numeroDocumento, primerNombre, segundoNombre, telefono, idTipoActor, idFoto, idClaseActor, idEstadoLegal, correo, tercerNombre, paginaWeb);
        }

        public Actor(int id, int idDocumentoIdentidad, DateTime fechaNacimiento, string numeroDocumento, string primerNombre, string segundoNombre, string telefono, int idTipoActor, int idFoto, int idClaseActor, int idEstadoLegal, string correo, string tercerNombre, string paginaWeb)
        {
            InitSets();
            this.id = id;
            ValidarIds(idDocumentoIdentidad, idTipoActor, idFoto, idClaseActor, idEstadoLegal);
            SetData(idDocumentoIdentidad, fechaNacimiento, numeroDocumento, primerNombre, segundoNombre, telefono, idTipoActor, idFoto, idClaseActor, idEstadoLegal, correo, tercerNombre, paginaWeb);
        }
        public void SetData(int idDocumentoIdentidad, DateTime fechaNacimiento, string numeroDocumento, string primerNombre, string segundoNombre, string telefono, int idTipoActor, int idFoto, int idClaseActor, int idEstadoLegal, string correo, string tercerNombre, string paginaWeb)
        {
            this.id_documento_identidad = idDocumentoIdentidad;
            this.numero_documento_identidad = numeroDocumento;
            this.fecha_nacimiento = fechaNacimiento;
            this.primer_nombre = primerNombre;
            this.segundo_nombre = segundoNombre;
            this.telefono = telefono;
            this.id_tipo_actor = idTipoActor;
            this.id_foto = idFoto;
            this.id_clase_actor = idClaseActor;
            this.id_estado_legal = idEstadoLegal;
            this.correo = correo;
            this.tercer_nombre = tercerNombre;
            this.pagina_web = paginaWeb;
        }

        ////public void setData(int id,int idDocumentoIdentidad, DateTime fechaNacimiento, string numeroDocumento, string primerNombre, string segundoNombre, string telefono, int idTipoActor, int idFoto, int idClaseActor, int idEstadoLegal, string correo, string tercerNombre, string paginaWeb)
        ////{
        ////    setData(idDocumentoIdentidad, fechaNacimiento, numeroDocumento, primerNombre, segundoNombre, telefono, idTipoActor, idFoto, idClaseActor, idEstadoLegal, correo, tercerNombre, paginaWeb);
        ////}
        protected void ValidarIds(int idDocumentoIdentidad, int idTipoActor, int idFoto, int idClaseActor, int idEstadoLegal)
        {
            if (idDocumentoIdentidad < 1) { throw new IdNoValidoException(idDocumentoIdentidad, "Documento de Identidad"); }
            if (idTipoActor < 1) { throw new IdNoValidoException(idTipoActor, "Tipo de Actor"); }
            if (idFoto < 1) { throw new IdNoValidoException(idFoto, "Foto"); }
            if (idClaseActor < 1) { throw new IdNoValidoException(idClaseActor, "Clase de Actor"); }
            if (idEstadoLegal < 1) { throw new IdNoValidoException(idEstadoLegal, "Estado Legal"); }
        }
        protected void InitSets()
        {
            this.Actor_negocio = new HashSet<Actor_negocio>();
            this.Direccion = new HashSet<Direccion>();
        }
        public Direccion DireccionPrincipal
        {
            get { return this.Direccion.LastOrDefault(d => d.es_principal = true); }
        }
        
    }
}

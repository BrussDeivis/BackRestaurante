using Tsp.Sigescom.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ActorGenericoViewModel
    {
        [DataMember]
        public int IdActor { get; set; }
        //public int IdTipoPersona { get; set; }
        public ComboGenerico TipoPersona { get; set; }
        public string RazonSocial { get; set; }
        //public int IdTipoDocumentoIdentidad { get; set; }
        public ComboGenerico TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }

        public string NombreComercial { get; set; }
        public string NombreCorto { get; set; }
        public int IdNacionDireccion { get; set; }
        public ComboGenerico UbigeoDireccion { get; set; }
        public int IdViaDireccion { get; set; }
        public int IdZonaDireccion { get; set; }
        public string DetalleDireccion { get; set; }
        public bool VigenciaDireccion { get; set; }

        public ActorGenericoViewModel()
        {

        }

        public ActorGenericoViewModel(Actor a)
        {
            this.IdActor = a.id;
            this.TipoPersona = new ComboGenerico(a.id_tipo_actor,a.Tipo_actor.nombre);
            this.RazonSocial = a.primer_nombre;
            this.TipoDocumentoIdentidad = new ComboGenerico(a.id_documento_identidad,a.Detalle_maestro.codigo);
            this.NumeroDocumentoIdentidad = a.numero_documento_identidad;
            this.NombreComercial = a.segundo_nombre;
            this.NombreCorto = a.tercer_nombre;
            this.IdNacionDireccion = a.DireccionPrincipal.id_nacion;
            this.UbigeoDireccion = new ComboGenerico(a.DireccionPrincipal.id_ubigeo, a.DireccionPrincipal.Ubigeo.descripcion_larga);
            //this.IdViaDireccion = a.DireccionPrincipal.id_tipo_via;
            //this.IdZonaDireccion = a.DireccionPrincipal.id_tipo_zona;
            this.DetalleDireccion = a.DireccionPrincipal.detalle;
            this.VigenciaDireccion = a.DireccionPrincipal.es_activo;
        }

        public ActorGenericoViewModel(int idActor, int idTipoPersona, string RazonSocial,int idTipoDocumentoIdentidad,string numeroDocumentoIdentidad,
            string nombreComercial,string nombreCorto, int idNacionDireccion,int idUbigeoDireccion,string nombreUbigeoDireccion,int idViaDireccion,int idZonaDireccion,
            string detalleDireccion,bool vigenciaDireccion)
        {
            this.IdActor = idActor;
            //this.TipoPersona = idTipoPersona;
            this.RazonSocial = RazonSocial;
            //this.TipoDocumentoIdentidad = idTipoDocumentoIdentidad;
            this.NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
            this.NombreComercial = nombreComercial;
            this.NombreCorto = nombreCorto;
            this.IdNacionDireccion = idNacionDireccion;
            this.UbigeoDireccion = new ComboGenerico(idUbigeoDireccion,nombreUbigeoDireccion);
            this.IdViaDireccion = idViaDireccion;
            this.IdZonaDireccion = idZonaDireccion;
            this.DetalleDireccion = detalleDireccion;
            this.VigenciaDireccion = vigenciaDireccion;
        }
    }
}
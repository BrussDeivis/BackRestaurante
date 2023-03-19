using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ClienteViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public ComboGenericoViewModel TipoPersona { get; set; }
        public ComboDocumentoIdentidadViewModel TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreComercial { get; set; }
        public string NombreCorto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public ComboGenericoViewModel EstadoLegalActor { get; set; }
        public ComboGenericoViewModel ClaseActor { get; set; }
        public List<DireccionViewModel> Direcciones { get; set; }
        public bool mostrarApellidoPaternoMaternoYNombre { get; set; }
        public int IdComprobantePredeterminado { get; set; }

        public ClienteViewModel()
        {

        }
        public ClienteViewModel(Actor actor)
        {
            this.IdActor = actor.id;
            this.TipoPersona = new ComboGenericoViewModel(actor.id_tipo_actor, actor.Tipo_actor.nombre);
            this.RazonSocial = actor.primer_nombre;


            this.ApellidoPaterno = TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actor.primer_nombre.Split('|')[0] : "";
            this.ApellidoMaterno = TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actor.primer_nombre.Split('|')[1] : "";
            this.Nombres = TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actor.primer_nombre.Split('|')[2] : "";
            this.Telefono = actor.telefono;

            this.NombreComercial = actor.segundo_nombre;
            this.NombreCorto = actor.tercer_nombre;
            this.Correo = actor.correo;
            this.ClaseActor = new ComboGenericoViewModel(actor.id_clase_actor, actor.Clase_actor.nombre);
            this.EstadoLegalActor = new ComboGenericoViewModel(actor.id_estado_legal, actor.Estado_legal.nombre);
            this.TipoDocumentoIdentidad = new ComboDocumentoIdentidadViewModel(actor.id_documento_identidad, actor.Detalle_maestro.codigo);
            this.NumeroDocumentoIdentidad = actor.numero_documento_identidad;
            this.Telefono = actor.telefono;
            this.mostrarApellidoPaternoMaternoYNombre = this.TipoPersona.Id == ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
            if (actor.Direccion.Any())
            {
                this.Direcciones = DireccionViewModel.Convert(actor.Direccion.ToList());
            }
        }
        public ClienteViewModel(Cliente cliente)
        {
            this.Id = cliente.Id;
            this.IdActor = cliente.ActorDeNegocio.Actor.id;
            this.TipoPersona = new ComboGenericoViewModel(cliente.IdTipoPersona, cliente.TipoPersona());
            this.Codigo = cliente.Codigo;
            this.RazonSocial = cliente.RazonSocial;
            this.ApellidoPaterno = cliente.ApellidoPaterno;
            this.ApellidoMaterno = cliente.ApellidoMaterno;
            this.Nombres = cliente.Nombres;
            this.NombreComercial = cliente.NombreComercial;
            this.NombreCorto = cliente.NombreCorto;
            this.Correo = cliente.Correo();
            this.Telefono = cliente.Telefono();
            this.ClaseActor = new ComboGenericoViewModel(cliente.IdClaseActor, cliente.ClaseActor().nombre);
            this.EstadoLegalActor = new ComboGenericoViewModel(cliente.IdEstadoLegal, cliente.EstadoLegal().nombre);
            this.TipoDocumentoIdentidad = new ComboDocumentoIdentidadViewModel(cliente.IdTipoDocumentoIdentidad, cliente.CodigoTipoDocumentoIdentidad());
            this.NumeroDocumentoIdentidad = cliente.DocumentoIdentidad;
            this.mostrarApellidoPaternoMaternoYNombre = this.TipoPersona.Id == ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
            this.Direcciones = new List<DireccionViewModel>();
            var domicilioFiscal = cliente.DomicilioFiscal();
            if (domicilioFiscal != null)
            {
                Direcciones.Add(new DireccionViewModel(domicilioFiscal));
            }
            this.IdComprobantePredeterminado = cliente.IdComprobantePredeterminado();

        }

    }


}
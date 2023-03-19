using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ProveedorViewModel
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

        public ProveedorViewModel()
        {

        }
        public ProveedorViewModel(Actor actor)
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
        public ProveedorViewModel(Proveedor proveedor)
        {
            this.Id = proveedor.Id;
            this.IdActor = proveedor.ActorDeNegocio.Actor.id;
            this.TipoPersona = new ComboGenericoViewModel(proveedor.IdTipoPersona, proveedor.TipoPersona());
            this.Codigo = proveedor.Codigo;
            this.RazonSocial = proveedor.RazonSocial;
            this.ApellidoPaterno = proveedor.ApellidoPaterno;
            this.ApellidoMaterno = proveedor.ApellidoMaterno;
            this.Nombres = proveedor.Nombres;
            this.NombreComercial = proveedor.NombreComercial;
            this.NombreCorto = proveedor.NombreCorto;
            this.Correo = proveedor.Correo();
            this.Telefono = proveedor.Telefono();
            this.ClaseActor = new ComboGenericoViewModel(proveedor.IdClaseActor, proveedor.ClaseActor().nombre);
            this.EstadoLegalActor = new ComboGenericoViewModel(proveedor.IdEstadoLegal, proveedor.EstadoLegal().nombre);
            this.TipoDocumentoIdentidad = new ComboDocumentoIdentidadViewModel(proveedor.IdTipoDocumentoIdentidad, proveedor.CodigoTipoDocumentoIdentidad());
            this.NumeroDocumentoIdentidad = proveedor.DocumentoIdentidad;
            this.mostrarApellidoPaternoMaternoYNombre = this.TipoPersona.Id == ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;

            this.Direcciones = new List<DireccionViewModel>();
            var domicilioFiscal = proveedor.DomicilioFiscal();
            if (domicilioFiscal != null)
            {
                Direcciones.Add(new DireccionViewModel(domicilioFiscal));
            }
        }
    }

    }
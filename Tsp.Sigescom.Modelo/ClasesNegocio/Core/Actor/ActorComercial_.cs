using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom
{
    public class ActorComercial_
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public ItemGenerico TipoDocumentoIdentidad { get; set; }
        public ItemGenerico TipoPersona { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string NombreORazonSocial { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreComercial { get; set; }
        public string NombreCorto { get; set; }
        public string Alias { get; set; }
        public string CodigoTipoDocumentoIdentidad { get { return this.TipoDocumentoIdentidad.Valor; } }
        public string CodigoSunatTipoDocumentoIdentidad { get { return this.TipoDocumentoIdentidad.Codigo; } }
        public string Codigo { get; set; }
        public bool EsVigente { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string FechaNacimientoString { get => FechaNacimiento.ToString("dd/MM/yyyy"); }
        public string InformacionPublicitaria { get; set; }
        public string Telefono { get; set; }
        //public String DomicilioFiscal { get; set; }
        public Direccion_ DomicilioFiscal { get; set; }
        public ItemGenerico ClaseActor { get; set; }
        public ItemGenerico EstadoLegal { get; set; }
        public ItemGenerico Nacionalidad { get; set; }
        public List<ItemGenerico> Roles { get; set; }
        public bool SeleccionarGrupo { get; set; }
        public bool NingunGrupo { get; set; }
        public ItemGenerico Grupo { get; set; }


        public ActorComercial_()
        { }


        public ActorComercial_(Actor_negocio actorDeNegocio)
        {
            this.Id = actorDeNegocio.id;
            this.IdActor = actorDeNegocio.id_actor;
            this.Codigo = actorDeNegocio.codigo_negocio;
            this.Telefono = actorDeNegocio.Telefono;
            this.NombreComercial = actorDeNegocio.SegundoNombre;
            this.NombreORazonSocial = actorDeNegocio.PrimerNombre.Replace("|", " ");
            this.ApellidoPaterno = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural ? actorDeNegocio.PrimerNombre.Split('|')[0] : "";
            this.ApellidoMaterno = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural && actorDeNegocio.PrimerNombre.Split('|').Count() > 1 ? actorDeNegocio.PrimerNombre.Split('|')[1] : "";
            this.Nombres = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural && actorDeNegocio.PrimerNombre.Split('|').Count() > 2 ? actorDeNegocio.PrimerNombre.Split('|')[2] : "";
            this.NumeroDocumentoIdentidad = actorDeNegocio.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = new ItemGenerico() { Id = actorDeNegocio.IdDocumentoIdentidad, Nombre = actorDeNegocio.Actor.Detalle_maestro.valor, Valor = actorDeNegocio.Actor.Detalle_maestro.valor };
            this.TipoPersona = new ItemGenerico() { Id = actorDeNegocio.Actor.id_tipo_actor, Nombre = actorDeNegocio.Actor.Tipo_actor.nombre };
            this.ClaseActor = new ItemGenerico() { Id = actorDeNegocio.Actor.id_clase_actor, Nombre = actorDeNegocio.Actor.Clase_actor.nombre };
            this.EstadoLegal = new ItemGenerico() { Id = actorDeNegocio.Actor.id_estado_legal , Nombre = actorDeNegocio.Actor.Estado_legal.nombre };
            this.FechaNacimiento = actorDeNegocio.FechaNacimiento;
            this.Nacionalidad = new ItemGenerico() { Id = actorDeNegocio.Actor.Detalle_maestro1.id, Nombre = actorDeNegocio.Actor.Detalle_maestro1.nombre };
            this.DomicilioFiscal = new Direccion_() { Id = actorDeNegocio.Actor.DireccionPrincipal.id, Pais = new ItemGenerico() { Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico(actorDeNegocio.Actor.DireccionPrincipal.Ubigeo.id, actorDeNegocio.Actor.DireccionPrincipal.Ubigeo.descripcion_larga), Detalle = actorDeNegocio.Actor.DireccionPrincipal.detalle };
        }

        public ActorComercial_(int idRol, Actor_negocio actorDeNegocio)
        {
            this.Id = actorDeNegocio.id;
            this.IdActor = actorDeNegocio.id_actor;
            this.Codigo = actorDeNegocio.codigo_negocio;
            this.Telefono = actorDeNegocio.Telefono;
            this.NombreComercial = actorDeNegocio.SegundoNombre;
            this.NombreORazonSocial = actorDeNegocio.PrimerNombre.Replace("|", " ");
            this.ApellidoPaterno = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural ? actorDeNegocio.PrimerNombre.Split('|')[0] : "";
            this.ApellidoMaterno = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural && actorDeNegocio.PrimerNombre.Split('|').Count() > 1 ? actorDeNegocio.PrimerNombre.Split('|')[1] : "";
            this.Nombres = actorDeNegocio.IdTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural && actorDeNegocio.PrimerNombre.Split('|').Count() > 2 ? actorDeNegocio.PrimerNombre.Split('|')[2] : "";
            this.NumeroDocumentoIdentidad = actorDeNegocio.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = new ItemGenerico() { Id = actorDeNegocio.IdDocumentoIdentidad, Nombre = actorDeNegocio.Actor.Detalle_maestro.valor, Valor = actorDeNegocio.Actor.Detalle_maestro.valor };
            this.TipoPersona = new ItemGenerico() { Id = actorDeNegocio.Actor.id_tipo_actor, Nombre = actorDeNegocio.Actor.Tipo_actor.nombre };
            this.ClaseActor = new ItemGenerico() { Id = actorDeNegocio.Actor.id_clase_actor, Nombre = actorDeNegocio.Actor.Clase_actor.nombre };
            this.EstadoLegal = new ItemGenerico() { Id = actorDeNegocio.Actor.id_estado_legal, Nombre = actorDeNegocio.Actor.Estado_legal.nombre };
            this.FechaNacimiento = actorDeNegocio.FechaNacimiento;
            this.Nacionalidad = new ItemGenerico() { Id = actorDeNegocio.Actor.Detalle_maestro1.id, Nombre = actorDeNegocio.Actor.Detalle_maestro1.nombre };
            this.DomicilioFiscal = new Direccion_() { Id = actorDeNegocio.Actor.DireccionPrincipal.id, Pais = new ItemGenerico() { Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico(actorDeNegocio.Actor.DireccionPrincipal.Ubigeo.id, actorDeNegocio.Actor.DireccionPrincipal.Ubigeo.descripcion_larga), Detalle = actorDeNegocio.Actor.DireccionPrincipal.detalle };
            if(idRol == ActorSettings.Default.IdRolEmpleado)
            {
                this.Roles = new List<ItemGenerico>();
                foreach (var rol in actorDeNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente == true).Select(an => an.Rol).ToList())
                {
                    this.Roles.Add(new ItemGenerico(rol.id, rol.nombre));
                }

            }
        }

        public ActorComercial_(Actor actor)
        {
            this.IdActor = actor.id;
            this.Telefono = actor.telefono;
            this.NombreComercial = actor.segundo_nombre;
            this.NombreORazonSocial = actor.primer_nombre.Replace("|", " ");
            this.ApellidoPaterno = actor.id_tipo_actor == ActorSettings.Default.IdTipoActorPersonaNatural ? actor.primer_nombre.Split('|')[0] : "";
            this.ApellidoMaterno = actor.id_tipo_actor == ActorSettings.Default.IdTipoActorPersonaNatural && actor.primer_nombre.Split('|').Count() > 1 ? actor.primer_nombre.Split('|')[1] : "";
            this.Nombres = actor.id_tipo_actor == ActorSettings.Default.IdTipoActorPersonaNatural && actor.primer_nombre.Split('|').Count() > 2 ? actor.primer_nombre.Split('|')[2] : "";
            this.NumeroDocumentoIdentidad = actor.numero_documento_identidad;
            this.TipoDocumentoIdentidad = new ItemGenerico() { Id = actor.id_documento_identidad, Nombre = actor.Detalle_maestro.valor };
            this.TipoPersona = new ItemGenerico() { Id = actor.id_tipo_actor, Nombre = actor.Tipo_actor.nombre };
            this.ClaseActor = new ItemGenerico() { Id = actor.id_clase_actor, Nombre = actor.Clase_actor.nombre };
            this.EstadoLegal = new ItemGenerico() { Id = actor.id_estado_legal, Nombre = actor.Estado_legal.nombre };
            this.FechaNacimiento = actor.fecha_nacimiento;
            this.Nacionalidad = new ItemGenerico() { Id = actor.Detalle_maestro1.id, Nombre = actor.Detalle_maestro1.nombre };
            this.DomicilioFiscal = new Direccion_() { Id = actor.DireccionPrincipal.id, Pais = new ItemGenerico() { Id = actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico(actor.DireccionPrincipal.Ubigeo.id, actor.DireccionPrincipal.Ubigeo.descripcion_larga), Detalle = actor.DireccionPrincipal.detalle };
        }
        public Actor_negocio Convert()
        {
            return new Actor_negocio()
            {
                id = this.Id,
                Actor = new Actor()
                {
                    primer_nombre = this.NombreORazonSocial,
                    segundo_nombre = this.NombreComercial,
                    Detalle_maestro = new Detalle_maestro()
                    {
                        nombre = this.TipoDocumentoIdentidad.Nombre,
                        codigo = this.CodigoSunatTipoDocumentoIdentidad,
                        valor = this.CodigoTipoDocumentoIdentidad
                    },
                    numero_documento_identidad = NumeroDocumentoIdentidad,
                    Direccion = new List<Direccion>()
                    {
                         (VentasSettings.Default.InformacionAMostrarEnDireccionDeCliente == (int)InformacionDireccionEnCliente.SoloDetalle) ? DomicilioFiscal.Convert() : DomicilioFiscal.ConvertirConUbigeo()
                    }
                }
            };
        }
    }
    public class RegistroActorComercial : ActorComercial_
    {
        //public ItemGenerico EstadoLegalActor { get; set; }
        public ItemGenerico ComprobantePredeterminado { get; set; }

        public RegistroActorComercial() : base()
        {
        }


    }
    public class SelectorActorComercial
    {
        private string numeroDocumento;
        private string razonSocial;
        public int Id { get; set; }
        public string NumeroDocumento
        {
            set
            {
                numeroDocumento = value;
            }
        }
        public string RazonSocial
        {
            get
            {
                return numeroDocumento + " - " + razonSocial.Replace("|"," ");
            }
            set
            {
                razonSocial = value;
            }
        }
        public SelectorActorComercial()
        {
        }




    }
}

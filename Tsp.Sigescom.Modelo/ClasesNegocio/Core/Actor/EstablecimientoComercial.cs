using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Actor;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos
{
    public class EstablecimientoComercial
    {
        public EstablecimientoComercial()
        {

        }

        public EstablecimientoComercial(Actor_negocio actorDeNegocio)
        {
            Id = actorDeNegocio.id;
            IdActor = actorDeNegocio.id_actor;
            Codigo = actorDeNegocio.codigo_negocio;
            IdEstadoLegal = actorDeNegocio.Actor.id_estado_legal;
            IdClaseActor = actorDeNegocio.Actor.id_clase_actor;
            IdTipoPersona = actorDeNegocio.Actor.id_tipo_actor;
            ClaseActor = actorDeNegocio.Actor.Clase_actor.nombre;
            TipoPersona = actorDeNegocio.Actor.Tipo_actor.nombre;
            Nombre = actorDeNegocio.Actor.primer_nombre;
            NombreComercial = actorDeNegocio.Actor.segundo_nombre;
            NombreInterno = actorDeNegocio.TercerNombre;
            DocumentoIdentidad = actorDeNegocio.Actor.numero_documento_identidad;


        }
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public string DocumentoIdentidad { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdEstadoLegal { get; set; }
        public int IdClaseActor { get; set; }
        public string ClaseActor { get; set; }


        public string TipoPersona { get; set; }

        /// <summary>
        /// indica el nombre(en el caso de una sucursal) o razon social(en el caso de la sede)
        /// </summary>
        public string Nombre { get; set; }

        public string NombreInterno { get; set; }
        public string NombreComercial { get; set; }

        public Establecimiento ToEstablecimiento()
        {
            return new Establecimiento(Id, NombreInterno);
        }
        public ItemGenerico ToItemGenerico()
        {
            return new ItemGenerico(Id, NombreInterno);
        }

    }

    public class EstablecimientoComercialConLogo : EstablecimientoComercial
    {
        public long IdLogo { get; set; }
        public bool TieneLogo { get { return IdLogo != ActorSettings.Default.IdFotoActorPorDefecto; } }
        public byte[] Logo { get; set; }

        public EstablecimientoComercialConLogo()
        {

        }
    }
    public class EstablecimientoComercialExtendido : EstablecimientoComercial
    {

        public EstablecimientoComercialExtendido()
        {

        }


        public EstablecimientoComercialExtendido(Actor_negocio actorDeNegocio) : base(actorDeNegocio)
        {
            //this.actorNegocio = actorDeNegocio;
            var parametroPrecio = actorDeNegocio.Parametro_actor_negocio.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
            IdCentroDeAtencionParaObtencionDePrecios = parametroPrecio != null ? (int?)Convert.ToInt32(parametroPrecio.valor) : null;
            var parametroStock = actorDeNegocio.Parametro_actor_negocio.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
            IdCentroDeAtencionParaObtencionDeStock = parametroStock != null ? (int?)Convert.ToInt32(parametroStock.valor) : null;
            EsSede = actorDeNegocio.id_rol == ActorSettings.Default.IdRolSede;
            EsSucursal = actorDeNegocio.id_rol == ActorSettings.Default.IdRolSucursal;
            ExtensionJson = actorDeNegocio.extension_json;

            InformacionPublicitaria = actorDeNegocio.Actor.informacion_multiproposito != null ? actorDeNegocio.Actor.informacion_multiproposito : "";
            Telefono = actorDeNegocio.Actor.telefono;
            Correo = actorDeNegocio.Actor.correo;
            if (actorDeNegocio.Actor.Direccion.Count > 0)
            {
                DomicilioFiscal = new Direccion_() { Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().id, Pais = new ItemGenerico() { Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico() { Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga }, Detalle = actorDeNegocio.Actor.Direccion.FirstOrDefault().detalle };
            }
            CodigoSunatTipoDocumentoIdentidad = actorDeNegocio.Actor.Detalle_maestro.codigo;
            CodigoTipoDocumentoIdentidad = actorDeNegocio.Actor.Detalle_maestro.valor;
            NombreTipoDocumento = actorDeNegocio.Actor.Detalle_maestro.nombre;

        }



        public string NombreTipoDocumento { get; set; }

        public string CodigoSunatTipoDocumentoIdentidad { get; set; }

        public string CodigoTipoDocumentoIdentidad { get; set; }

        public string InformacionPublicitaria { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public Direccion_ DomicilioFiscal { get; set; }
        public Direccion Domicilio { get; set; }




        // Es el nombre con el que se conoce al establecimiento comercial, Ejm: Razon social : Comercial ABC EIRL, Nombre comercial: Comercial ABC, Nombre Interno: Tienda Principal Tingo Maria
        public string ExtensionJson { get; set; }

        public int? IdCentroDeAtencionParaObtencionDePrecios { get; set; }


        public int? IdCentroDeAtencionParaObtencionDeStock { get; set; }


        public bool EsSede { get; set; }


        public bool EsSucursal { get; set; }

        public string CodigoEstablecimientoDigemid { get { return ExtensionJson != "" ? JsonConvert.DeserializeObject<JsonEstablecimientoComercial>(ExtensionJson).codigodigemid : ""; } }

        public static List<EstablecimientoComercialExtendido> Convertir(List<Actor_negocio> actoresDeNegocio)
        {
            List<EstablecimientoComercialExtendido> establecimientoComercia = new List<EstablecimientoComercialExtendido>();
            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                establecimientoComercia.Add(new EstablecimientoComercialExtendido(actorDeNegocio));
            }
            return establecimientoComercia;
        }
    }

    public class EstablecimientoComercialExtendidoConLogo : EstablecimientoComercialExtendido
    {
        public EstablecimientoComercialExtendidoConLogo(Actor_negocio actorDeNegocio) : base(actorDeNegocio)
        { }
        public long IdLogo { get; set; }

        public bool TieneLogo { get { return IdLogo != ActorSettings.Default.IdFotoActorPorDefecto; } }


        public byte[] Logo { get; set; }

        public EstablecimientoComercialExtendidoConLogo()
        {

        }
    }
}
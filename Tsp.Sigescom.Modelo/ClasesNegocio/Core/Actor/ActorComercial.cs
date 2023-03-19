using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ActorComercial
    {
        protected Actor_negocio actorDeNegocio;

        public ActorComercial(Actor_negocio actorDeNegocio)
        {
            this.ActorDeNegocio = actorDeNegocio;
        }

        public Actor_negocio ActorDeNegocio
        {
            get { return actorDeNegocio; }
            set { this.actorDeNegocio = value; }
        }

        public int IdTipoPersona
        {
            get { return this.actorDeNegocio.IdTipoActor; }
        }
        public string TipoPersona()
        {
            return this.actorDeNegocio.Actor.Tipo_actor.nombre;
        }
        public int IdEstadoLegal
        {
            get { return this.actorDeNegocio.IdEstadoLegal; }
        }
        public int IdClaseActor
        {
            get { return this.actorDeNegocio.IdClaseActor; }
        }
        public int Id
        {
            get { return this.actorDeNegocio.id; }
        }

        public string RazonSocial
        {
            get { return this.actorDeNegocio.PrimerNombre.Replace("|", " "); }
        }
        public string ApellidoPaterno
        {
            get { return this.IdTipoPersona == ActorSettings.Default.IdTipoActorPersonaNatural ? this.actorDeNegocio.PrimerNombre.Split('|')[0] : ""; }
        }
        public string ApellidoMaterno
        {
            get { return this.IdTipoPersona == ActorSettings.Default.IdTipoActorPersonaNatural && this.actorDeNegocio.PrimerNombre.Split('|').Count() > 1 ? this.actorDeNegocio.PrimerNombre.Split('|')[1] : ""; }
        }
        public string Nombres
        {
            get { return this.IdTipoPersona == ActorSettings.Default.IdTipoActorPersonaNatural && this.actorDeNegocio.PrimerNombre.Split('|').Count() > 2 ? this.actorDeNegocio.PrimerNombre.Split('|')[2] : ""; }
        }
        public string NombreComercial
        {
            get { return this.actorDeNegocio.SegundoNombre; }
        }

        public string NombreCorto
        {
            get { return this.actorDeNegocio.TercerNombre; }
        }

        public string Codigo
        {
            get { return this.actorDeNegocio.codigo_negocio; }
        }

        public int IdTipoDocumentoIdentidad
        {
            get { return this.actorDeNegocio.IdDocumentoIdentidad; }
        }

        //public int LongitudTipoDocumentoIdentidad()
        //{
        //    return this.actorDeNegocio.Actor.Documento_identidad.longitud;
        //}

        public int? IdCondicionPago()
        {
            return this.actorDeNegocio.Parametro_actor_negocio.SingleOrDefault
                (pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroCondicionPago).id_valor_parametro;
        }

        public string CondicionPago()
        {
            return this.actorDeNegocio.Parametro_actor_negocio.SingleOrDefault
                (pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroCondicionPago).Detalle_maestro1.nombre;
        }
        public DetalleGenerico TipoPago()
        {
            return new DetalleGenerico(this.actorDeNegocio.Parametro_actor_negocio.SingleOrDefault
                (pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroCondicionPago).Detalle_maestro1);
        }
        //public int? IdFormaPago()
        //{
        //    return this.actorDeNegocio.Parametro_actor_negocio.SingleOrDefault
        //        (pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroFormaPago).id_valor_parametro; 

        //}

        //public string FormaPago()
        //{
        //    return this.actorDeNegocio.Parametro_actor_negocio.SingleOrDefault
        //        (pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroFormaPago).Detalle_maestro1.nombre; 
        //}

        public int IdActor()
        {
            return this.actorDeNegocio.Actor.id;
        }

        public string CodigoTipoDocumentoIdentidad()
        {
            return this.actorDeNegocio.Actor.Detalle_maestro.valor;
        }
        public string CodigoSunatTipoDocumentoIdentidad()
        {
            return this.actorDeNegocio.Actor.Detalle_maestro.codigo;
        }
        public Clase_actor ClaseActor()
        {
            return this.actorDeNegocio.Actor.Clase_actor;
        }
        public Estado_legal EstadoLegal()
        {
            return this.actorDeNegocio.Actor.Estado_legal;
        }

        //En todos los actores (cliente,proveedor,empleado) se guarda la direccion fiscal
        public Direccion DomicilioFiscal()
        {
            Direccion direccion = this.actorDeNegocio.Actor.Direccion.OrderByDescending(d => d.id_tipo_direccion).FirstOrDefault(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal);
            return direccion;

        }


        public List<Direccion> Direcciones()
        {
            var hayDireccion = this.actorDeNegocio.Actor.Direccion.Any();
            return hayDireccion ? this.actorDeNegocio.Actor.Direccion.ToList() : null;
        }
        public string DocumentoIdentidad
        {
            get { return this.actorDeNegocio.DocumentoIdentidad; }
        }

        public bool EsVigente
        {
            get { return this.actorDeNegocio.es_vigente; }

        }




        public DateTime FechaInicio
        {
            get { return this.actorDeNegocio.fecha_inicio; }
        }

        public DateTime FechaFin
        {
            get { return this.actorDeNegocio.fecha_fin; }

        }
        public string Correo()
        {
            return this.actorDeNegocio.Actor.correo;
        }

        public string InformacionPublicitaria()
        {
            return actorDeNegocio.Actor.informacion_multiproposito != null ? this.actorDeNegocio.Actor.informacion_multiproposito : "";
        }


        public string Telefono()
        {
            return this.actorDeNegocio.Actor.telefono;
        }



        public bool HayLogo()
        {
            return this.actorDeNegocio.Actor.id_foto != ActorSettings.Default.IdFotoActorPorDefecto;
        }

        public byte[] Logo()
        {
            return this.actorDeNegocio.Actor.Foto.imagen;
        }

        public string NombreRolActorNegocio()
        {
            return this.actorDeNegocio.Rol.nombre;
        }

        public string NombreTipoDocumentoActorNegocio()
        {
            return this.actorDeNegocio.Actor.Detalle_maestro.nombre;
        }

        public string CodigoTipoDocumentoActorNegocio()
        {
            return this.actorDeNegocio.Actor.Detalle_maestro.codigo;
        }

        public IEnumerable<Transaccion> TransaccionesDeVenta()
        {
            return this.actorDeNegocio.Transaccion;
        }



        public static List<ActorComercial> convert(List<Actor_negocio> actoresDeNegocio)
        {
            var actorComercial = new List<ActorComercial>();

            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                actorComercial.Add(new Proveedor(actorDeNegocio));
            }
            return actorComercial;
        }

    }


}

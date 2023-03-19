using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CentroDeAtencionExtendido : CentroDeAtencion
    {
        //private readonly Actor_negocio actorDeNegocio;

        public CentroDeAtencionExtendido()
        {
              
        }
        public CentroDeAtencionExtendido(Actor_negocio actorDeNegocio)
        {
            Id = actorDeNegocio.id;
            IdActor = actorDeNegocio.id_actor;
            Codigo = actorDeNegocio.codigo_negocio;
            EstablecimientoComercial = new EstablecimientoComercial
            {
                Id = (int)actorDeNegocio.id_actor_negocio_padre,
                Nombre = actorDeNegocio.Actor_negocio2.Actor.primer_nombre,
                NombreInterno= actorDeNegocio.Actor_negocio2.Actor.tercer_nombre
            };
            Nombre = actorDeNegocio.Actor.primer_nombre;
            ExtensionJson = actorDeNegocio.extension_json;
            DocumentoIdentidad = actorDeNegocio.DocumentoIdentidad;
            RolesHijosVigentes = actorDeNegocio.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre });

            var parametrosDeSuEstablecimiento = actorDeNegocio.Actor_negocio2.Parametro_actor_negocio.ToList();
            var parametroCentroAtencionPrecio = parametrosDeSuEstablecimiento.FirstOrDefault(p => p.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
            var parametroCentroAtencionStock = parametrosDeSuEstablecimiento.FirstOrDefault(p => p.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
            EsCentroAtencionParaObtencioDePrecios = parametroCentroAtencionPrecio != null ? (System.Convert.ToInt32(parametroCentroAtencionPrecio.valor) == Id) : false;
            EsCentroAtencionParaObtencioDeStock = parametroCentroAtencionStock != null ? (System.Convert.ToInt32(parametroCentroAtencionStock.valor) == Id) : false;
        }


        public string DocumentoIdentidad { get; set; }
        public IEnumerable<ItemGenerico> RolesHijosVigentes { get; set; }
        public bool EsCentroAtencionParaObtencioDePrecios { get; set; }
        public bool EsCentroAtencionParaObtencioDeStock { get; set; }

        ///// <summary>
        ///// Devuelve los roles hijos del rol centro de atencion a los que se encuentra asociado el centro de atencion.
        ///// </summary>
        ///// <returns></returns>
        //public List<Rol> ObtenerRolesHijosVigentes()
        //{
        //    return this.actorDeNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an.es_vigente == true).Select(an => an.Rol).ToList();
        //}

        ///// <summary>
        ///// Devuelve si el centro de atencion es para la obtencion de precios
        ///// </summary>
        ///// <returns></returns>
        //public bool EsCentroAtencionParaObtencioDePrecios()
        //{
        //    var centrosAtencionParaObtencioDePrecios = this.actorDeNegocio.Actor_negocio2.Parametro_actor_negocio.Where(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
        //    return centrosAtencionParaObtencioDePrecios != null ? (centrosAtencionParaObtencioDePrecios.SingleOrDefault(cap => System.Convert.ToInt32(cap.valor) == this.actorDeNegocio.id) != null) : false;
        //}
        ///// <summary>
        ///// Devuelve si el centro de atencion es para la obtencion de existencias
        ///// </summary>
        ///// <returns></returns>
        //public bool EsCentroAtencionParaObtencioDeStock()
        //{
        //    var centrosAtencionParaObtencioDeStock = this.actorDeNegocio.Actor_negocio2.Parametro_actor_negocio.Where(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
        //    return centrosAtencionParaObtencioDeStock != null ? (centrosAtencionParaObtencioDeStock.SingleOrDefault(cap => System.Convert.ToInt32(cap.valor) == this.actorDeNegocio.id) != null) : false;
        //}

    }
}

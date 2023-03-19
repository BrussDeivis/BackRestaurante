using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;

namespace Tsp.Sigescom.AccesoDatos.Establecimientos
{
    public partial class Sucursal_Datos : ISucursal_Repositorio
    {
        public IEnumerable<EstablecimientoComercial> ObtenerEstablecimientosComercialesVigentes()
        {
            int[] idsRoles= new int[]{ ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede};
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => idsRoles.Contains(an.id_rol) && an.es_vigente == true).Select(an=> new EstablecimientoComercial() { Id = an.id,
            Nombre = an.Actor.primer_nombre,
            NombreInterno = an.Actor.tercer_nombre,
                });
        }


        /// <summary>
        /// Obtener los actores de negocio de acuerdo a los roles, devuelve items genericos con el id del actor_negocio y el tercer_nombre (nombre interno) del actor.
        /// </summary>
        /// <param name="idsRol"></param>
        /// <returns></returns>
        public IEnumerable<ItemGenerico> ObtenerActoresDeNegocioPorRolVigentesAhoraComosItemGenericos(int[] idsRol)
        {
            SigescomEntities _db = new SigescomEntities();

            return _db.Actor_negocio
                .Where(an => idsRol.Contains(an.id_rol) && an.es_vigente == true).Select(an => new ItemGenerico { Id = an.id, Nombre = an.Actor.tercer_nombre });
        }

        public IEnumerable<ItemGenericoConSubItems> ObtenerActoresDeNegocioPorRolVigentesAhoraComosItemGenericos(int[] idsRolesActoresPadres, int idRolSecundarioActoresHijos, int idRolPrincipalActoresHijos)
        {
            SigescomEntities _db = new SigescomEntities();

            return _db.Actor_negocio
                .Where(an => idsRolesActoresPadres.Contains(an.id_rol) && an.es_vigente == true).Select(an => new ItemGenericoConSubItems
                {
                    Id = an.id,
                    Nombre = an.Actor.primer_nombre,
                    SubItems = an.Actor_negocio1.Where(anh => anh.id_rol == idRolPrincipalActoresHijos && anh.es_vigente && anh.Actor.Actor_negocio.Count(aa => aa.id_rol == idRolSecundarioActoresHijos && aa.es_vigente) > 0)
                    .Select(anh => new ItemGenerico { Id = anh.id, Nombre = anh.Actor.primer_nombre }).ToList()
                });

        }

        public Actor_negocio ObtenerActorDeNegocioInclusiveDireccionesYTipoDocumentoIdentidadYFotoYParametros(int idActorNegocio)
        {
            try
            {
            SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio
                    .Include(an => an.Actor)
                    .Include(an => an.Actor.Detalle_maestro)
                    .Include(an => an.Actor.Direccion)
                    .Include(an => an.Actor.Direccion.Select(d => d.Ubigeo))
                    .Include(an => an.Actor.Foto)
                    .Include(an => an.Parametro_actor_negocio)
                    .SingleOrDefault(an => an.id == idActorNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Parametro_actor_negocio ObtenerParametroCentroDeAtencionParaObtencionPrecios(int idEstablecimientoComencial)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Parametro_actor_negocio.FirstOrDefault(pcn => pcn.id_actor_negocio== idEstablecimientoComencial && pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
        }
        public Parametro_actor_negocio ObtenerParametroCentroDeAtencionParaObtencionDeStock(int idEstablecimientoComencial)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Parametro_actor_negocio.FirstOrDefault(pcn => pcn.id_actor_negocio == idEstablecimientoComencial && pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
        }
    }
}
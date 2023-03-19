using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;

namespace Tsp.Sigescom.AccesoDatos.Establecimientos
{
    public partial class Establecimiento_Datos : IEstablecimiento_Repositorio
    {
        public IEnumerable<EstablecimientoComercial> ObtenerEstablecimientosComercialesVigentes()
        {
            int[] idsRoles= new int[]{ ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede};
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => idsRoles.Contains(an.id_rol) && an.es_vigente == true).Select(an => new EstablecimientoComercial() {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    Nombre = an.Actor.primer_nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre
                });
        }

        public IEnumerable<EstablecimientoComercialExtendido> ObtenerEstablecimientosComercialesExtendidosVigentes()
        {
            int[] idsRoles = new int[] { ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede };
            SigescomEntities _db = new SigescomEntities();
            var establecimientos= _db.Actor_negocio
                .Where(an => idsRoles.Contains(an.id_rol) && an.es_vigente == true).Select(an => new EstablecimientoComercialExtendido()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    Nombre = an.Actor.primer_nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre,
                    EsSede = an.id_rol == ActorSettings.Default.IdRolSede,
                    EsSucursal = an.id_rol == ActorSettings.Default.IdRolSucursal,
                    ExtensionJson = an.extension_json,
                    InformacionPublicitaria = an.Actor.informacion_multiproposito??"",
                    Telefono = an.Actor.telefono,
                    Correo = an.Actor.correo,
                    DomicilioFiscal = new Direccion_() { Id = an.Actor.Direccion.FirstOrDefault().id, Pais = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga }, Detalle = an.Actor.Direccion.FirstOrDefault().detalle },
                    CodigoSunatTipoDocumentoIdentidad = an.Actor.Detalle_maestro.codigo,
                    CodigoTipoDocumentoIdentidad = an.Actor.Detalle_maestro.valor,
                    NombreTipoDocumento = an.Actor.Detalle_maestro.nombre

                });

            EstablecerParametros(establecimientos, _db);
            return establecimientos;
        }
        /// <summary>
        /// Obtener los actores de negocio de acuerdo a los roles, devuelve items genericos con el id del actor_negocio y el tercer_nombre (nombre interno) del actor.
        /// </summary>
        /// <param name="idsRol"></param>
        /// <returns></returns>
        public IEnumerable<ItemGenerico> ObtenerEstablecimientosComercialesVigentesComoItemsGenericos()
        {
            int[] idsRoles = new int[] { ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede };
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => idsRoles.Contains(an.id_rol) && an.es_vigente == true).Select(an => new ItemGenerico { Id = an.id, Nombre = an.Actor.tercer_nombre });
        }

        public EstablecimientoComercial ObtenerEstablecimientoComercial(int id)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => an.id == id).Select(an => new EstablecimientoComercial()
                {

                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    Nombre = an.Actor.primer_nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre,
                }).FirstOrDefault();
        }
        public ItemGenerico ObtenerEstablecimientoComercialComoItemGenerico(int id)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => an.id ==id).Select(an => new ItemGenerico()
                {

                    Id = an.id,
                    Codigo = an.codigo_negocio,
                    Nombre = an.Actor.primer_nombre
                   
                }).FirstOrDefault();
        }

        public EstablecimientoComercialExtendido ObtenerEstablecimientoComercialExtendido(int id)
        {
            SigescomEntities _db = new SigescomEntities();
            var establecimiento= _db.Actor_negocio
                .Where(an => an.id == id).Select(an => new EstablecimientoComercialExtendido()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    Nombre = an.Actor.primer_nombre,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre,
                    EsSede = an.id_rol == ActorSettings.Default.IdRolSede,
                    EsSucursal = an.id_rol == ActorSettings.Default.IdRolSucursal,
                    ExtensionJson= an.extension_json,
                    InformacionPublicitaria = an.Actor.informacion_multiproposito ?? "",
                    Telefono = an.Actor.telefono,
                    Correo = an.Actor.correo,
                    DomicilioFiscal = new Direccion_() { Id = an.Actor.Direccion.FirstOrDefault().id, Pais = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre, Valor = "" }, Ubigeo = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,Valor= an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_corta }, Detalle = an.Actor.Direccion.FirstOrDefault().detalle },
                    CodigoSunatTipoDocumentoIdentidad = an.Actor.Detalle_maestro.codigo,
                    CodigoTipoDocumentoIdentidad = an.Actor.Detalle_maestro.valor,
                    NombreTipoDocumento = an.Actor.Detalle_maestro.nombre
                }).FirstOrDefault();

            EstablecerParametros(establecimiento, _db);
            return establecimiento;
        }

        public EstablecimientoComercialExtendidoConLogo ObtenerEstablecimientoComercialExtendidoConLogo(int id)
        {
            SigescomEntities _db = new SigescomEntities();
            var establecimiento = _db.Actor_negocio
                .Where(an => an.id == id).Select(an => new EstablecimientoComercialExtendidoConLogo()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    Nombre = an.Actor.primer_nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre,
                    EsSede = an.id_rol == ActorSettings.Default.IdRolSede,
                    EsSucursal = an.id_rol == ActorSettings.Default.IdRolSucursal,
                    ExtensionJson = an.extension_json,
                    InformacionPublicitaria = an.Actor.informacion_multiproposito ?? "",
                    Telefono = an.Actor.telefono,
                    Correo = an.Actor.correo,
                    Domicilio = an.Actor.Direccion.FirstOrDefault(),
                    DomicilioFiscal = new Direccion_() { Id = an.Actor.Direccion.FirstOrDefault().id, Pais = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga }, Detalle = an.Actor.Direccion.FirstOrDefault().detalle },
                    CodigoSunatTipoDocumentoIdentidad = an.Actor.Detalle_maestro.codigo,
                    CodigoTipoDocumentoIdentidad = an.Actor.Detalle_maestro.valor,
                    NombreTipoDocumento = an.Actor.Detalle_maestro.nombre,
                    IdLogo = an.Actor.id_foto,
                    Logo = an.Actor.Foto.imagen

                }).FirstOrDefault();

            EstablecerParametros(establecimiento, _db);
            return establecimiento;
        }
        public Modelo.ClasesNegocio.Core.Establecimientos.EstablecimientoComercialConLogo ObtenerEstablecimientoComercialConLogo(int idEstablecimiento)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio.Where(an => an.id == idEstablecimiento).Select(an => new Modelo.ClasesNegocio.Core.Establecimientos.EstablecimientoComercialConLogo()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    IdEstadoLegal = an.Actor.id_estado_legal,
                    IdClaseActor = an.Actor.id_clase_actor,
                    IdTipoPersona = an.Actor.id_tipo_actor,
                    ClaseActor = an.Actor.Clase_actor.nombre,
                    TipoPersona = an.Actor.Tipo_actor.nombre,
                    Nombre = an.Actor.primer_nombre,
                    NombreComercial = an.Actor.segundo_nombre,
                    NombreInterno = an.Actor.tercer_nombre,
                    IdLogo = an.Actor.id_foto,
                    Logo = an.Actor.Foto.imagen
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void EstablecerParametros(EstablecimientoComercialExtendido establecimiento, SigescomEntities _db)
        {
            var parametros = _db.Actor_negocio.FirstOrDefault(an => an.id == establecimiento.Id).Parametro_actor_negocio.ToList();
            EstablecerParametros(parametros, establecimiento);
        }


        public void EstablecerParametros(IEnumerable<EstablecimientoComercialExtendido> establecimientos, SigescomEntities _db)
        {

            var idsEstablecimientos = establecimientos.Select(e => e.Id).Distinct().ToArray();

            var parametros = _db.Actor_negocio.Where(an => idsEstablecimientos.Contains( an.id)).SelectMany(e=> e.Parametro_actor_negocio).ToList();
            foreach (var establecimiento in establecimientos)
            {
                EstablecerParametros(parametros.Where(p=> p.id_actor_negocio== establecimiento.Id).ToList(), establecimiento);
            }
           
        }

        public void EstablecerParametros(List<Parametro_actor_negocio> parametros, EstablecimientoComercialExtendido establecimiento)
        {
            var parametroPrecio = parametros.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);

            establecimiento.IdCentroDeAtencionParaObtencionDePrecios = parametroPrecio != null ? (int?)Convert.ToInt32(parametroPrecio.valor) : null;

            var parametroStock = parametros.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);

            establecimiento.IdCentroDeAtencionParaObtencionDeStock = parametroStock != null ? (int?)Convert.ToInt32(parametroStock.valor) : null;
        }
        public IEnumerable<ItemGenericoConSubItems> ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol(int idRolDeCentroDeAtencion)
        {
            var idsRolesActoresPadres = new int[] { ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede };
            var idRolPrincipalActoresHijos = ActorSettings.Default.IdRolEntidadInterna;
            var idRolSecundarioActoresHijos = idRolDeCentroDeAtencion;
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
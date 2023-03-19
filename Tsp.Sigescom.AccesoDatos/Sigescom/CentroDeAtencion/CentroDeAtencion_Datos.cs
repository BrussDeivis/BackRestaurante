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

namespace Tsp.Sigescom.AccesoDatos.Empleado
{
    public partial class CentroDeAtencion_Datos: ICentroDeAtencion_Repositorio
    {


        public List<CentroDeAtencion> ObtenerCentrosDeAtencionProgramados_(int idEmpleado)
        {
            var fechaActual = DateTimeUtil.FechaActual();
            var tipoVinculo = TipoVinculo.Turno;
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_vinculado == idEmpleado && van.tipo_vinculo == (int)tipoVinculo && van.desde <= fechaActual && van.hasta >= fechaActual && van.es_vigente).Select(van => van.Actor_negocio).Select(an => new CentroDeAtencion { Id = an.id, IdActor= an.id_actor, Codigo = an.codigo_negocio, EstablecimientoComercial = new EstablecimientoComercial { Id = (int)an.id_actor_negocio_padre, Nombre = an.Actor_negocio2.Actor.primer_nombre, NombreInterno=an.Actor_negocio2.Actor.tercer_nombre }, Nombre = an.Actor.primer_nombre, ExtensionJson = an.extension_json }).ToList();
        }
        public Actor_negocio ObtenerActorDeNegocio(int idActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio
                    .Include(an => an.Actor)
                    .Include(an => an.Actor.Detalle_maestro)
                    .Include(an => an.Actor.Direccion)
                    .Include(an => an.Actor.Direccion.Select(d => d.Ubigeo))
                    .SingleOrDefault(an => an.id == idActorNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CentroDeAtencion ObtenerCentroDeAtencion_(int idCentroAtencion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                return _db.Actor_negocio.Where(an => an.id == idCentroAtencion).Select(an => new CentroDeAtencion
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener centro de atencion", e);
            }
        }
        

       
        public IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionVigentes()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;

                var centrosDeAtencion = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente == true).Select(an => new CentroDeAtencion
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json
                }).ToList();
                return centrosDeAtencion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentes()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;

                var centrosDeAtencion= _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente == true).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).ToList();

                EstablecerParametros(centrosDeAtencion, _db);
                return centrosDeAtencion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EstablecerParametros(IEnumerable<CentroDeAtencionExtendido> centrosDeAtencion, SigescomEntities _db)
        {
            var idsEstablecimientos = centrosDeAtencion.Select(ca => ca.EstablecimientoComercial.Id).Distinct().ToArray();
            var parametrosDeLosEstablecimientos = _db.Actor_negocio.Where(an => idsEstablecimientos.Contains(an.id)).SelectMany(e => e.Parametro_actor_negocio).ToList();

            foreach (var centroDeAtencion in centrosDeAtencion)
            {
                var parametrosDeSuEstablecimiento = parametrosDeLosEstablecimientos.Where(p => p.id_actor_negocio == centroDeAtencion.EstablecimientoComercial.Id).ToList();
                EstablecerParametros(parametrosDeSuEstablecimiento, centroDeAtencion);


            }
        }

        public void EstablecerParametros(CentroDeAtencionExtendido centroDeAtencion, SigescomEntities _db)
        {
            var parametrosDeSuEstablecimiento = _db.Actor_negocio.FirstOrDefault(an => an.id == centroDeAtencion.EstablecimientoComercial.Id).Parametro_actor_negocio.ToList();
            EstablecerParametros(parametrosDeSuEstablecimiento, centroDeAtencion);
        }

        public void EstablecerParametros(List<Parametro_actor_negocio> parametrosDeSuEstablecimiento, CentroDeAtencionExtendido centroDeAtencion)
        {
            var parametroCentroAtencionPrecio = parametrosDeSuEstablecimiento.FirstOrDefault(p => p.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
            var parametroCentroAtencionStock = parametrosDeSuEstablecimiento.FirstOrDefault(p => p.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
            centroDeAtencion.EsCentroAtencionParaObtencioDePrecios = parametroCentroAtencionPrecio != null ? (System.Convert.ToInt32(parametroCentroAtencionPrecio.valor) == centroDeAtencion.Id) : false;
            centroDeAtencion.EsCentroAtencionParaObtencioDeStock = parametroCentroAtencionStock != null ? (System.Convert.ToInt32(parametroCentroAtencionStock.valor) == centroDeAtencion.Id) : false;
        }
        public List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionProgramados(int idEmpleado)
        {
            var fechaActual = DateTimeUtil.FechaActual();
            var tipoVinculo = TipoVinculo.Turno;
            SigescomEntities _db = new SigescomEntities();
            int idRol = ActorSettings.Default.IdRolEntidadInterna;

            var resultados = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_vinculado == idEmpleado && van.tipo_vinculo == (int)tipoVinculo && van.desde <= fechaActual && van.hasta >= fechaActual && van.es_vigente).Select(van => van.Actor_negocio).Where(an => an.id_rol == idRol).Select(an => new CentroDeAtencionExtendido
            {
                Id = an.id,
                IdActor = an.id_actor,
                Codigo = an.codigo_negocio,
                EstablecimientoComercial = new EstablecimientoComercial
                {
                    Id = (int)an.id_actor_negocio_padre,
                    Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                },
                Nombre = an.Actor.primer_nombre,
                ExtensionJson = an.extension_json,
                DocumentoIdentidad = an.Actor.numero_documento_identidad,
                RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
            }).ToList();
            return resultados;
        }
        public CentroDeAtencionExtendido _ObtenerCentroDeAtencion(int id)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;

                var centroDeAtencion =_db.Actor_negocio.Where(an => an.id == id && an.id_rol == idRol).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno= an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad= an.Actor.numero_documento_identidad,
                    RolesHijosVigentes= an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r=> new ItemGenerico() {Id= r.id, Nombre= r.nombre }),
                }).FirstOrDefault();

                EstablecerParametros(centroDeAtencion, _db);
                return centroDeAtencion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ObtenerIdDelCentroDeAtencionQueTieneLosPreciosSegunIdCentroDeAtencion(int idCentroAtencion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;

                var parametroPrecio = _db.Actor_negocio.Where(an => an.id == idCentroAtencion && an.id_rol == idRol).FirstOrDefault().Actor_negocio2.Parametro_actor_negocio.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios);
                int IdCentroDeAtencionParaObtencionDePrecios = parametroPrecio != null ? Convert.ToInt32(parametroPrecio.valor) : 0;
                return IdCentroDeAtencionParaObtencionDePrecios;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionConPrecioDeCadaEstablecimientoVigente()
        {
            try
            {
                int[] idsRolesEstablecimiento = new int[] { ActorSettings.Default.IdRolSucursal, ActorSettings.Default.IdRolSede };
                SigescomEntities _db = new SigescomEntities();
                var idsCentrosAtencionConPrecio = _db.Actor_negocio.Where(an => idsRolesEstablecimiento.Contains(an.id_rol) && an.es_vigente == true).SelectMany(an => an.Parametro_actor_negocio).Where(pan => pan.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios).Select(pan => pan.valor).ToList().Select(int.Parse).ToList();
                var centrosAtencionVigentesDeEstablecimientos = _db.Actor_negocio
                    .Where(an => idsCentrosAtencionConPrecio.Contains(an.id)).Select(an => new CentroDeAtencion()
                    {
                        Id = an.id,
                        IdActor = an.id_actor,
                        Codigo = an.codigo_negocio,
                        EstablecimientoComercial = new EstablecimientoComercial
                        {
                            Id = (int)an.id_actor_negocio_padre,
                            Nombre = an.Actor_negocio2.Actor.primer_nombre,
                            NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                        },
                        Nombre = an.Actor.primer_nombre,
                        ExtensionJson = an.extension_json
                    });
                return centrosAtencionVigentesDeEstablecimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ObtenerIdDelCentroDeAtencionQueTieneElStockSegunIdCentroDeAtencion(int idCentroAtencion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;
                var parametroPrecio = _db.Actor_negocio.Where(an => an.id == idCentroAtencion && an.id_rol == idRol).FirstOrDefault().Actor_negocio2.Parametro_actor_negocio.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock);
                int IdCentroDeAtencionParaObtencionDeStock = parametroPrecio != null ? Convert.ToInt32(parametroPrecio.valor) : 0;
                return IdCentroDeAtencionParaObtencionDeStock;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerNombreDeCentroDeAtencion(int id)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;
                return _db.Actor_negocio.SingleOrDefault(an => an.id == id && an.id_rol == idRol).Actor.primer_nombre.Replace("|", " ");
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener primer nombre de actor", e);
            }
        }

        public CentroDeAtencionExtendido ObtenerCentroDeAtencionExtendidosSegunSerieComprobante(int idSerieComprobante)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                int idRol = ActorSettings.Default.IdRolEntidadInterna;
                var centroDeAtencion= _db.Serie_comprobante.Where(sc => sc.id == idSerieComprobante).Select(an => an.Actor_negocio).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).FirstOrDefault();
                EstablecerParametros(centroDeAtencion, _db);
                return centroDeAtencion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesPorEstablecimientoComercial( int idEstablecimiento)
        {
            int idRol = ActorSettings.Default.IdRolEntidadInterna;
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Where(an => an.es_vigente == true && an.id_rol == idRol && an.id_actor_negocio_padre == idEstablecimiento).Select(an => new CentroDeAtencionExtendido
            {
                Id = an.id,
                IdActor = an.id_actor,
                Codigo = an.codigo_negocio,
                EstablecimientoComercial = new EstablecimientoComercial
                {
                    Id = (int)an.id_actor_negocio_padre,
                    Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                },
                Nombre = an.Actor.primer_nombre,
                ExtensionJson = an.extension_json,
                DocumentoIdentidad = an.Actor.numero_documento_identidad,
                RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
            }).ToList();
        }

        
            public IEnumerable<ItemGenerico> ObtenerCentrosDeAtencionComoItemsGenericosSegunRolHijo(int idRolHijo, bool esVigente)
        {
            SigescomEntities _db = new SigescomEntities();
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            var centrosDeAtencion = _db.Actor_negocio
                .Where(an => an.id_rol == idRolHijo && an.es_vigente == esVigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente == esVigente).Select(an => new ItemGenerico
                {
                    Id = an.id,
                    Codigo = an.codigo_negocio,
                    Nombre = an.Actor.primer_nombre,
                }).ToList();
            return centrosDeAtencion;
        }
        public IEnumerable<ItemGenerico> ObtenerCentrosDeAtencionComoItemsGenericosSegunRolesHijos(int[] idsRolesHijo, bool esVigente)
        {
            SigescomEntities _db = new SigescomEntities();
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            var centrosDeAtencion = _db.Actor_negocio
                .Where(an => idsRolesHijo.Contains(an.id_rol) && an.es_vigente == esVigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente == esVigente).Select(an => new ItemGenerico
                {
                    Id = an.id,
                    Codigo = an.codigo_negocio,
                    Nombre = an.Actor.primer_nombre,
                }).ToList();
            return centrosDeAtencion;
        }
        public IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionSegunRolHijo(int idRolHijo, bool esVigente)
        {
            SigescomEntities _db = new SigescomEntities();
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            var centrosDeAtencion = _db.Actor_negocio
                .Where(an => an.id_rol == idRolHijo && an.es_vigente == esVigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente == esVigente).Select(an => new CentroDeAtencion
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json
                }).ToList();
            return centrosDeAtencion;
        }
        public IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosSegunRolHijo(int idRolHijo, bool esVigente)
        {
            SigescomEntities _db = new SigescomEntities();
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            var centrosDeAtencion = _db.Actor_negocio
                .Where(an => an.id_rol == idRolHijo && an.es_vigente == esVigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente == esVigente).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).ToList();

            EstablecerParametros(centrosDeAtencion, _db);

            return centrosDeAtencion;

        }

        public IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(int idRol, int idEstablecimiento)
        {
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            SigescomEntities _db = new SigescomEntities();
            var centrosDeAtencion = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_actor_negocio_padre == idEstablecimiento && an.id_rol == idParentRol && an.es_vigente).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).ToList();

            EstablecerParametros(centrosDeAtencion, _db);

            return centrosDeAtencion;
            
        }

        public int[] ObtenerIdsDeCentrosDeAtencionVigentesSegunRolYEstablecimientoComercial(int idRol, int idActorNegocioPadre)
        {
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio
                .Where(an => an.id_rol == idRol && an.es_vigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_actor_negocio_padre == idActorNegocioPadre && an.id_rol == idParentRol && an.es_vigente).Select(an => an.id).ToArray();
        }

        public IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(int idRol, List<int> idsEstablecimientos)
        {
            var idParentRol = ActorSettings.Default.IdRolEntidadInterna;
            SigescomEntities _db = new SigescomEntities();
            var centrosDeAtencion = _db.Actor_negocio
                .Where(an => an.id_rol == idRol && an.es_vigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => idsEstablecimientos.Contains((int)an.id_actor_negocio_padre) && an.id_rol == idParentRol && an.es_vigente).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).ToList();
            EstablecerParametros(centrosDeAtencion, _db);
            return centrosDeAtencion; ;
        }

        public CentroDeAtencionExtendido ObtenerCentroDeAtencionExtendidoPorIdDeCentroDeAtencionEIdDeEstablecimiento(int idCentroAtencion, int idEstablecimiento)
        {
            try
            {
                var idRol = ActorSettings.Default.IdRolEntidadInterna;
            SigescomEntities _db = new SigescomEntities();
                var centroDeAtencion= _db.Actor_negocio.Include(an => an.Actor_negocio_rol).Include(an => an.Actor).Include(an => an.Actor.Actor_negocio).
                Include(an => an.Actor.Direccion).Where(an => an.id == idCentroAtencion && an.id_rol == idRol && an.id_actor_negocio_padre == idEstablecimiento).Select(an => new CentroDeAtencionExtendido
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.codigo_negocio,
                    EstablecimientoComercial = new EstablecimientoComercial
                    {
                        Id = (int)an.id_actor_negocio_padre,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        NombreInterno = an.Actor_negocio2.Actor.tercer_nombre
                    },
                    Nombre = an.Actor.primer_nombre,
                    ExtensionJson = an.extension_json,
                    DocumentoIdentidad = an.Actor.numero_documento_identidad,
                    RolesHijosVigentes = an.Actor.Actor_negocio.Where(an_ => an_.Rol.id_rol_padre == ActorSettings.Default.IdRolEntidadInterna && an_.es_vigente == true).Select(an__ => an__.Rol).Select(r => new ItemGenerico() { Id = r.id, Nombre = r.nombre }),
                }).FirstOrDefault();
                EstablecerParametros(centroDeAtencion, _db);
                return centroDeAtencion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool TieneInventarioActual(int idCentroDeAtencion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                return _db.Transaccion.Any(t => t.id_actor_negocio_interno == idCentroDeAtencion
                                                && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual
                                                && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transaccion inclusive detalles de transaccion", e);
            }
        }

        public OperationResult ActualizarDocumentoIdentidadDeTodosLosCentrosDeAtencionVigentes(string documentoIdentidad)
        {
            SigescomEntities _db = new SigescomEntities();
            var idRol = ActorSettings.Default.IdRolEntidadInterna;
            var actores = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente == true).Select(an => an.Actor).ToList();
            actores.ForEach(a => a.numero_documento_identidad = documentoIdentidad);
            _db.SaveChanges();
            return new OperationResult(OperationResultEnum.Success);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public class ActorDatos : RepositorioBase, IActorRepositorio
    {
        #region Crear y Actualizar




        public OperationResult CrearActorNegocioActualizandoActor(Actor_negocio actorNegocio)
        {
            try
            {
                List<Actor_negocio> updRoles = actorNegocio.Actor.Actor_negocio.ToList(); ;
                var dbActor = _db.Actor.Include(a => a.Direccion).Single(a => a.id == actorNegocio.id_actor);

                _db.Entry(dbActor).CurrentValues.SetValues(actorNegocio.Actor);

                //direcciones
                List<Direccion> updDirecciones = actorNegocio.Actor.Direccion.ToList();
                List<Direccion> dbDirecciones = dbActor.Direccion.ToList();

                foreach (var dbDireccion in dbDirecciones)
                {
                    //si existe actualizo
                    if (updDirecciones.Any(d => d.id == dbDireccion.id))
                    {
                        var direccion = updDirecciones.Single(d => d.id == dbDireccion.id);
                        _db.Entry(dbDireccion).CurrentValues.SetValues(direccion);
                    }
                    else//sino lo elimino
                    {
                        _db.Direccion.Remove(dbDireccion);
                    }
                }
                //agrego las nuevas direcciones
                foreach (var updDireccion in updDirecciones)
                {
                    if (!dbDirecciones.Any(d => d.id == updDireccion.id))
                    {
                        _db.Direccion.Add(updDireccion);
                    }
                }

                //Agregamos los actores
                actorNegocio.Actor = dbActor;
                _db.Actor_negocio.Add(actorNegocio);

                //agregamos los Roles si lo tiene
                foreach (var updRol in updRoles)
                {
                    _db.Actor_negocio.Add(updRol);
                }
                var result = Save();
                result.data = actorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }






        public int obtenerIdActor(int idActorNegocio)
        {
            return _db.Actor_negocio.Where(an => an.id == idActorNegocio).Max(an => an.id_actor);
        }

        public int obtenerIdDireccion(int idActor)
        {
            var direcciones = _db.Direccion.Where(an => an.id_actor == idActor);
            return direcciones.Count() > 0 ? _db.Direccion.Where(an => an.id_actor == idActor).Max(an => an.id) : 0;
        }


        public bool ExisteNumeroDocumentoIdDetalleMultiPropositoActor(int idDetalleMultiproposito, string numeroDocumento, string primerNombre)
        {
            try
            {
                bool existe = _db.Actor.Where(cn => cn.numero_documento_identidad == numeroDocumento && cn.primer_nombre == primerNombre && cn.id_detalle_multiproposito == idDetalleMultiproposito).Any();
                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el nombre de concepto_negocio", e);
            }
        }

        public bool ExisteNumeroDocumentoIdDetalleMultiPropositoActor(int idActor, int idDetalleMultiproposito, string numeroDocumento, string primerNombre)
        {
            try
            {
                bool existe = _db.Actor.Where(cn => cn.id != idActor && cn.numero_documento_identidad == numeroDocumento && cn.primer_nombre == primerNombre && cn.id_detalle_multiproposito == idDetalleMultiproposito).Any();
                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el nombre de concepto_negocio", e);
            }
        }



        //funcion que actualiza todos los actores de negocio







        //se acualizan los datos escalares del actor de negocio y los datos escalares que contiene updActorNegocio
        public OperationResult actualizarActorNegocioYRoles(Actor_negocio updActorNegocio)
        {

            //traemos el actor comercial de la BD
            var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor.Direccion).Include(an => an.Actor.Actor_negocio).Single(an => an.id == updActorNegocio.id);
            //datos escalares
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);
            //actualizando actor
            _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(updActorNegocio.Actor);

            //direcciones
            int[] idsDireccionesAActualizar = updActorNegocio.Actor.Direccion.Select(d => d.id).ToArray();
            List<Direccion> dbDirecciones = dbActorNegocio.Actor.Direccion.Where(d => idsDireccionesAActualizar.Contains(d.id)).ToList();

            foreach (var dbDireccion in dbDirecciones)
            {
                //actualizando las direcciones
                _db.Entry(dbDireccion).CurrentValues.SetValues(updActorNegocio.Actor.Direccion.SingleOrDefault(d => d.id == dbDireccion.id));
            }

            //Roles
            List<Actor_negocio> dbRoles = dbActorNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == updActorNegocio.id_rol && an.es_vigente == true).ToList();
            List<Actor_negocio> updRoles = updActorNegocio.Actor.Actor_negocio.ToList();

            foreach (var dbRol in dbRoles)
            {
                //si no existe, doy de baja el actor con ese rol
                if (!updRoles.Any(p => p.id_rol == dbRol.id_rol))
                {
                    dbRol.fecha_fin = DateTimeUtil.FechaActual();
                    dbRol.es_vigente = false;
                }
            }
            ///agrego los roles nuevos
            foreach (var updRol in updRoles)
            {
                if (!dbRoles.Any(p => p.id_rol == updRol.id_rol))
                {
                    _db.Actor_negocio.Add(updRol);
                }
            }
            return Save();
        }


        //se acualizan los datos escalares del actor de negocio y los datos escalares que contiene updActorNegocio
        public OperationResult actualizarActorNegocioYDireccion(Actor_negocio updActorNegocio)
        {
            //traemos el actor comercial de la BD
            var dbActorNegocio = _db.Actor_negocio.Include(an => an.Parametro_actor_negocio).Include(an => an.Actor.Direccion).Single(an => an.id == updActorNegocio.id);
            //datos escalares
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);

            //actualizando actor
            _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(updActorNegocio.Actor);


            //direcciones
            int[] idsDireccionesAActualizar = updActorNegocio.Actor.Direccion.Select(d => d.id).ToArray();
            List<Direccion> dbDirecciones = dbActorNegocio.Actor.Direccion.Where(d => idsDireccionesAActualizar.Contains(d.id)).ToList();
            //List<Direccion> udpDirecciones = updActorNegocio.Actor.Direccion.ToList();

            foreach (var dbDireccion in dbDirecciones)
            {
                //actualizando las direcciones
                _db.Entry(dbDireccion).CurrentValues.SetValues(updActorNegocio.Actor.Direccion.SingleOrDefault(d => d.id == dbDireccion.id));
            }

            //parametros
            List<Parametro_actor_negocio> dbParametros = dbActorNegocio.Parametro_actor_negocio.ToList();
            List<Parametro_actor_negocio> updParametros = updActorNegocio.Parametro_actor_negocio.ToList();

            foreach (var dbParametro in dbParametros)
            {
                //si existe, le cambio el valor
                if (updParametros.Any(p => p.id_parametro == dbParametro.id_parametro))
                {
                    dbParametro.id_valor_parametro = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).id_valor_parametro;
                    dbParametro.valor = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).valor;
                }
                else//sino lo elimino
                {
                    _db.Parametro_actor_negocio.Remove(dbParametro);
                }
            }
            ///agrego los nuevos
            foreach (var updParametro in updParametros)
            {
                if (!dbParametros.Any(p => p.id_parametro == updParametro.id_parametro))
                {
                    _db.Parametro_actor_negocio.Add(updParametro);
                }
            }
            return Save();
        }
        public OperationResult actualizarActorNegocio(Actor_negocio newActorNegocio, int[] newIdsActorNegocioRol)
        {
            //ActorDeNegocioRepositorioGenerico.Update(newActorNegocio);
            var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor_negocio_rol).Single(an => an.id == newActorNegocio.id);

            // Update foo (works only for scalar properties)
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(newActorNegocio);

            _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(newActorNegocio.Actor);
            ///resolver los parametros
            ///actualizo o elimino los existentes
            foreach (var dbParametroActorNegocio in dbActorNegocio.Parametro_actor_negocio)
            {
                //si existe, le cambio el valor
                if (newActorNegocio.nuevosParametros.Any(pan => pan.id_parametro == dbParametroActorNegocio.id_parametro))
                {
                    dbParametroActorNegocio.id_valor_parametro = newActorNegocio.nuevosParametros.Single(pan => pan.id_parametro == dbParametroActorNegocio.id_parametro).id_valor_parametro;
                }
                else//sino lo elimino
                {
                    _db.Parametro_actor_negocio.Remove(dbParametroActorNegocio);
                }
            }

            ///agrego los nuevos
            foreach (var newParametroActorNegocio in newActorNegocio.nuevosParametros)
            {
                if (!dbActorNegocio.Parametro_actor_negocio.Any(pan => pan.id_parametro == newParametroActorNegocio.id_parametro))
                {
                    _db.Parametro_actor_negocio.Add(newParametroActorNegocio);
                }
            }

            ///resolver los servicios
            foreach (var dbActorNegocioRol in dbActorNegocio.Actor_negocio_rol.ToList())
            {
                if (!newIdsActorNegocioRol.Any(id => id == dbActorNegocioRol.id_rol))
                {
                    _db.Actor_negocio_rol.Remove(dbActorNegocioRol);
                }
            }


            foreach (var newId in newIdsActorNegocioRol)
            {
                if (!dbActorNegocio.Actor_negocio_rol.Any(anr => anr.id_rol == newId))
                {
                    dbActorNegocio.Actor_negocio_rol.Add(new Actor_negocio_rol(dbActorNegocio.id, newId));
                }
            }
            return Save();
        }



        #endregion

        #region Consultas

        public IEnumerable<Clase_actor> obtenerClasesDeActor(int idTipoActor)
        {
            try
            {
                return _db.Clase_actor.Where(ca => ca.id_tipo_actor == idTipoActor).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Estado_legal> obtenerEstadosLegales(int idTipoActor)
        {
            try
            {
                return _db.Estado_legal.Where(el => el.id_tipo_actor == idTipoActor).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Tipo_actor> obtenerTiposDeActor()
        {
            try
            {
                return _db.Tipo_actor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string obtenerDenominacionClase(int idTipoActor)
        {
            return _db.Tipo_actor.SingleOrDefault(ta => ta.id == idTipoActor).denominacion_de_clase;
        }





        public IEnumerable<Actor_negocio> ObtenerActoresDeNegocio(int[] idsActoresNegocio)
        {
            try
            {
                return _db.Actor_negocio
                    .Include(an => an.Actor)
                    .Include(an => an.Actor.Detalle_maestro)
                    .Include(an => an.Actor.Direccion)
                    .Include(an => an.Actor.Direccion.Select(d => d.Ubigeo))
                    .Where(an => idsActoresNegocio.Contains(an.id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Modelo.Custom.EstablecimientoComercialConLogo_ ObtenerEstablecimiento(int id)
        {
            try
            {
                var resultado = _db.Actor_negocio.Where(an => an.id == id).Select(actorDeNegocio => new
                {
                    Establecimiento = new EstablecimientoComercialConLogo_
                    {
                        Id = actorDeNegocio.id,
                        NombreComercial = actorDeNegocio.Actor.segundo_nombre,
                        IdActor = actorDeNegocio.id_actor,
                        NombreCorto = actorDeNegocio.Actor.tercer_nombre,
                        NombreORazonSocial = actorDeNegocio.Actor.primer_nombre,
                        Codigo = actorDeNegocio.codigo_negocio,
                        NumeroDocumentoIdentidad = actorDeNegocio.Actor.numero_documento_identidad,
                        TipoDocumentoIdentidad = new ItemGenerico() { Id = actorDeNegocio.Actor.id_documento_identidad, Nombre = actorDeNegocio.Actor.Detalle_maestro.nombre, Codigo = actorDeNegocio.Actor.Detalle_maestro.codigo, Valor = actorDeNegocio.Actor.Detalle_maestro.valor },
                        TipoPersona = new ItemGenerico() { Id = actorDeNegocio.Actor.id_tipo_actor, Nombre = actorDeNegocio.Actor.Tipo_actor.nombre, Codigo = "", Valor = "" },
                        DomicilioFiscal = actorDeNegocio.Actor.Direccion.Select(d => new Direccion_() { Ubigeo = (new ItemGenerico() { Id = d.Ubigeo.id, Nombre = d.Ubigeo.descripcion_corta, Codigo = "", Valor = "" }), Id = d.id, Detalle = d.detalle }).FirstOrDefault(),
                        EsVigente = actorDeNegocio.es_vigente,
                        Correo = actorDeNegocio.Actor.correo,
                        InformacionPublicitaria = actorDeNegocio.Actor.informacion_multiproposito != null ? actorDeNegocio.Actor.informacion_multiproposito : "",
                        Telefono = actorDeNegocio.Actor.telefono,
                        NombreInterno = actorDeNegocio.Actor.tercer_nombre,
                        EsSede = actorDeNegocio.id_rol == ActorSettings.Default.IdRolSede,
                        EsSucursal = actorDeNegocio.id_rol == ActorSettings.Default.IdRolSucursal,
                        Logo = actorDeNegocio.Actor.Foto.imagen,
                        JsonExtension = actorDeNegocio.extension_json
                    },
                    ParametroPrecio = actorDeNegocio.Parametro_actor_negocio.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios),
                    ParametroStock = actorDeNegocio.Parametro_actor_negocio.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock),
                    DomicilioFiscal = actorDeNegocio.Actor.Direccion.FirstOrDefault(),
                    IdRol = actorDeNegocio.id_rol

                }).FirstOrDefault();

                var establecimiento = resultado.Establecimiento;
                establecimiento.EsSede = resultado.IdRol == ActorSettings.Default.IdRolSede;
                establecimiento.EsSucursal = resultado.IdRol == ActorSettings.Default.IdRolSucursal;
                establecimiento.IdCentroDeAtencionParaObtencionDePrecios = resultado.ParametroPrecio != null ? (int?)System.Convert.ToInt32(resultado.ParametroPrecio.valor) : null;
                establecimiento.IdCentroDeAtencionParaObtencionDeStock = resultado.ParametroStock != null ? (int?)System.Convert.ToInt32(resultado.ParametroStock.valor) : null;

                return establecimiento;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener establecimiento", e);
            }
        }


        public Actor_negocio ObtenerActorDeNegocioInclusiveDireccionesYTipoDocumentoIdentidadYParametros(int id)
        {
            try
            {
                return _db.Actor_negocio
                    .Include(an => an.Actor)
                    .Include(an => an.Actor.Tipo_actor)
                    .Include(an => an.Actor.Detalle_maestro)
                    .Include(an => an.Actor.Detalle_maestro1)
                    .Include(an => an.Actor.Direccion)
                    .Include(an => an.Actor.Direccion.Select(d => d.Ubigeo))
                    .Include(an => an.Actor.Foto)
                    .Include(an => an.Parametro_actor_negocio)
                    .SingleOrDefault(an => an.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public IEnumerable<Actor_negocio> obtenerActoresDeNegocioPorRolInclusiveActorYRoles(int idRol)
        {
            try
            {
                return _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Detalle_maestro).Include(an => an.Parametro_actor_negocio).
                Include(an => an.Actor_negocio_rol.Select(anr => anr.Rol)).Include(an => an.Rol).Where(an => an.id_rol == idRol);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioPorRolVigentes(int idRol)
        {
            try
            {
                var _dbAsync = new SigescomEntities();
                return (await _dbAsync.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente).ToListAsync());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioPorRolVigentes(int idRol, int idActorNegocioPadre)
        {
            try
            {
                var _dbAsync = new SigescomEntities();
                return (await _dbAsync.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente && an.id_actor_negocio_padre == idActorNegocioPadre).ToListAsync());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Actor_negocio obtenerActorDeNegocio(int idActorNegocio, int idRol)
        {
            try
            {
                return _db.Actor_negocio
                    .Include(an => an.Actor_negocio_rol)
                    .Include(an => an.Actor)
                    .Include(an => an.Actor.Actor_negocio)
                    .Include(an => an.Actor.Direccion)
                    .SingleOrDefault(an => an.id == idActorNegocio && an.id_rol == idRol);
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public Actor_negocio ObtenerActorDeNegocioVigente(int idActorNegocio, int idRol)
        {
            try
            {
                Actor_negocio actorNegocio = _db.Actor_negocio.SingleOrDefault(an => an.id == idActorNegocio);
                return _db.Actor_negocio.SingleOrDefault(an => an.id_actor == actorNegocio.Actor.id && an.id_rol == idRol && an.es_vigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public OperationResult establecerIdUsuario(string idUdsuario, int idActorNegocio)
        {
            try
            {
                var dbActorNegocio = _db.Actor_negocio.Single(an => an.id == idActorNegocio);
                dbActorNegocio.id_usuario = idUdsuario;
                return Save();
            }
            catch (Exception e)
            {
                return new OperationResult(e, "Error al intentar vicular el Usuario con el Empleado");
            }
        }

        public IEnumerable<Actor_negocio> obtenerActorDeNegocioVigentesAhoraEnRol(int idRolActorNegocio, int idRolEn)
        {
            return _db.Actor_negocio_rol.
                Include(anr => anr.Rol).
                Include(anr => anr.Actor_negocio).
                Include(anr => anr.Actor_negocio.Actor).
                Where(anr => anr.Actor_negocio.id_rol == idRolActorNegocio && anr.Actor_negocio.fecha_inicio <= SqlFunctions.GetDate() && anr.Actor_negocio.fecha_fin >= SqlFunctions.GetDate() && anr.id_rol == idRolEn).Select(anr => anr.Actor_negocio);
        }

        public IEnumerable<Actor_negocio> obtenerActorDeNegocioVigentesAhoraEnRoles(int idRolActorNegocio, int[] idsRolesEn)
        {
            return _db.Actor_negocio_rol.
                Include(anr => anr.Rol).
                Include(anr => anr.Actor_negocio).
                Include(anr => anr.Actor_negocio.Actor).
                Where(anr => anr.Actor_negocio.id_rol == idRolActorNegocio && anr.Actor_negocio.fecha_inicio <= SqlFunctions.GetDate() &&
                anr.Actor_negocio.fecha_fin >= SqlFunctions.GetDate() && idsRolesEn.Contains(anr.id_rol)).Select(anr => anr.Actor_negocio);

        }








        public IEnumerable<Actor_negocio> obtenerActorDeNegocioIncluidoActorPorRolVigentesAhora(int idRol)
        {
            return _db.Actor_negocio.
                Include(an => an.Actor).
                Where(an => an.id_rol == idRol && an.es_vigente == true);

        }









        public IEnumerable<Actor_negocio> obtenerActorDeNegocioPorRol(int idRol)
        {
            return _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Detalle_maestro).Include(an => an.Parametro_actor_negocio).
                Include(an => an.Actor_negocio_rol.Select(anr => anr.Rol)).Include(an => an.Rol)
                .Where(an => an.id_rol == idRol);
        }

        public IEnumerable<Actor_negocio> obtenerActorDeNegocioPorRol(int idRol, DateTime fechaVigencia)
        {
            return _db.Actor_negocio
                .Where(an => an.id_rol == idRol && an.fecha_inicio <= fechaVigencia && an.fecha_fin >= fechaVigencia);
        }
        /// <summary>
        ///  Devuelve los actores de negocio con rol <paramref name="idParentRol"/> que tenga ademas el rol <paramref name="idRol"/> y que su estado sea de acuerdo a si <paramref name="esVigente"/>
        /// </summary>
        /// <param name="idParentRol"></param>
        /// <param name="idRol"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        public IEnumerable<Actor_negocio> ObtenerActorDeNegocioPrincipal(int idParentRol, int idRol, bool esVigente)
        {
            return _db.Actor_negocio.Include(an => an.Actor).
                Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Detalle_maestro).
                Include(an => an.Parametro_actor_negocio).
                Include(an => an.Actor_negocio_rol.Select(anr => anr.Rol)).
                Include(an => an.Rol)
                .Where(an => an.id_rol == idRol && an.es_vigente == esVigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente == esVigente);
        }

        public async Task<IEnumerable<ItemGenerico>> ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericos(int idParentRol, int idRol)
        {
            //IEnumerable<ItemGenerico> resultado = null;
            var _dbAsync = new SigescomEntities();
            return await _dbAsync.Actor_negocio.
                Where(an => an.id_rol == idRol && an.es_vigente).Select(an => an.Actor).SelectMany(a => a.Actor_negocio).Where(an => an.id_rol == idParentRol && an.es_vigente).Select(an => new ItemGenerico { Id = an.id, Nombre = an.Actor.primer_nombre }).ToListAsync();
        }
        public async Task<IEnumerable<ItemGenerico>> ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericosPorIdRol(int idRol)
        {
            //IEnumerable<ItemGenerico> resultado = null;
            var _dbAsync = new SigescomEntities();
            return await _dbAsync.Actor_negocio.
                Where(an => an.id_rol == idRol && an.es_vigente).Select(an => new ItemGenerico { Id = an.id, Nombre = an.Actor.primer_nombre }).ToListAsync(); ;
        }





        public int[] ObtenerIdsActorDeNegocioPrincipal(int idParentRol, int idRol)
        {
            return _db.Actor_negocio.Where(an => an.id_rol == idRol).Select(an => an.Actor).SelectMany(a => a.Actor_negocio).Where(an => an.id_rol == idParentRol).Select(an => an.id).ToArray();
        }



        public IEnumerable<Concepto_negocio> obtenerConceptosPorRol(int idRol)
        {
            return null;// _db.Rol.Single(r => r.id == idRol).Concepto_negocio;
        }



        public IEnumerable<Actor_negocio> obtenerActoresDeNegocioPrincipalPorRol(int idRol)
        {
            return _db.Actor_negocio.Where(an => an.id_rol == idRol);
        }
        public List<Actor_negocio> obtenerActoresDeNegocioPrincipalPorRolVigentesAhoraDatosBasicos(int idRol)
        {
            return _db.Actor_negocio.Include(an => an.Actor).Where(an => an.id_rol == idRol && an.es_vigente == true).ToList();
        }

        public int ObtenerIdActor(string documentoIdentidad, int idTipoDocumentoIdentidad)
        {
            int id = 0;
            id = _db.Actor.Where(a => a.numero_documento_identidad == documentoIdentidad && a.id_documento_identidad == idTipoDocumentoIdentidad).Select(a => a.id).FirstOrDefault();
            return id;
        }
        #endregion

        #region Comprobaciones
        public bool ExisteActor(string documentoIdentidad)
        {
            return _db.Actor.Any(a => a.numero_documento_identidad == documentoIdentidad);
        }

        public bool ExisteActorDeNegocio(string documentoIdentidad)
        {
            return _db.Actor_negocio.Any(an => an.Actor.numero_documento_identidad == documentoIdentidad);
        }

        public bool ExistActorDeNegocio(string documentoIdentidad, DateTime fechaVigencia)
        {
            return _db.Actor_negocio.Any(an => an.Actor.numero_documento_identidad == documentoIdentidad && an.fecha_inicio <= fechaVigencia && an.fecha_fin >= fechaVigencia);
        }

        public bool actorDeNegocioEsVigente(int idProveedor)
        {
            return _db.Actor_negocio.Any(an => an.id == idProveedor && an.fecha_inicio <= SqlFunctions.GetDate() && an.fecha_fin >= SqlFunctions.GetDate());
        }


        public Actor_negocio ObtenerActorDeNegocioPorId(int idActorNegocio, int idRol)
        {
            return _db.Actor_negocio.
                Include(an => an.Actor).
                Include(an => an.Actor.Direccion).
                Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Clase_actor).
                Include(an => an.Actor.Detalle_maestro).
                Include(an => an.Actor.Direccion.Select(d => d.Ubigeo)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro1)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro2)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro3)).
                SingleOrDefault(an => an.id == idActorNegocio && an.id_rol == idRol && an.es_vigente);
        }
        public Actor_negocio obtenerActorDeNegocioConRoles(int idActorNegocio, int idRol)
        {
            return _db.Actor_negocio.Include(an => an.Actor.Actor_negocio).SingleOrDefault(an => an.id == idActorNegocio && an.id_rol == idRol);
        }
        public Actor_negocio ObtenerActorDeNegocioVigentePorDocumentoIdentidad(string documentoIdentidad, int idRol)
        {
            return _db.Actor_negocio.
                Include(an => an.Actor).
                Include(an => an.Actor.Direccion).
                Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Clase_actor).
                Include(an => an.Actor.Estado_legal).
                Include(an => an.Actor.Detalle_maestro).
                Include(an => an.Actor.Direccion.Select(d => d.Ubigeo)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro1)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro2)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro3)).
                SingleOrDefault(an => an.Actor.numero_documento_identidad == documentoIdentidad && an.id_rol == idRol && an.es_vigente);
        }

        public IEnumerable<SelectorActorComercial> ObtenerActoresComercialesVigentesPorRolYBusqueda(int idRol, string cadenaBusqueda)
        {
            try
            {
                return _db.Actor_negocio
                               .Where(an => an.id_rol == idRol && an.es_vigente
                                             && (an.Actor.numero_documento_identidad.Contains(cadenaBusqueda) || an.Actor.primer_nombre.Replace("|", " ").Contains(cadenaBusqueda)))
                               .Select(an => new SelectorActorComercial()
                               {
                                   Id = an.id,
                                   NumeroDocumento = an.Actor.numero_documento_identidad,
                                   RazonSocial = an.Actor.primer_nombre,
                               }).OrderBy(cnc => cnc.RazonSocial);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de actor comercial por rol, cadena de busqueda", e);
            }
        }
        public IEnumerable<ItemGenerico> ObtenerActoresComercialesPorCentroDeAtencion(int idCentroDeAtencion)
        {
            try
            {
                return _db.Vinculo_Actor_Negocio.Where(v => v.id_actor_negocio_principal == idCentroDeAtencion && v.tipo_vinculo == (int)TipoVinculo.Turno).Select(v => v.Actor_negocio1).Select(an => 
                new ItemGenerico
                {
                    Id = an.id,
                    Nombre = an.Actor.tercer_nombre
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de actor comercial por rol, cadena de busqueda", e);
            }
        }

        public string obtenerMaximoCodigo(int idRol/*, int idDesde*/)
        {
            try
            {
                int idMaximo = _db.Actor_negocio.Where(an => /*an.id > idDesde &&*/ an.id_rol == idRol).Max(an => an.id);
                return _db.Actor_negocio.Where(an => an.id == idMaximo).Max(an => an.codigo_negocio) == null || _db.Actor_negocio.Where(an => an.id == idMaximo).Max(an => an.codigo_negocio) == "" ? "0" : _db.Actor_negocio.Where(an => an.id == idMaximo).Max(an => an.codigo_negocio);
            }
            catch
            {
                return "0";
            }
        }




        public RespuestaVerificacionActorNegocio verificarActor(int idDocumentoIdentidad, string numeroDocumento, int idRol)
        {
            try
            {

                RespuestaVerificacionActorNegocio respuesta;
                Actor actor = _db.Actor.Include(a => a.Actor_negocio).SingleOrDefault(a => a.id_documento_identidad == idDocumentoIdentidad && a.numero_documento_identidad == numeroDocumento);
                if (actor != null)
                {
                    if (actor.Actor_negocio.Any(an => an.id_rol == idRol && an.es_vigente == true))
                    {
                        Actor_negocio actorNegocio = actor.Actor_negocio.SingleOrDefault(an => an.id_rol == idRol && an.es_vigente == true);
                        respuesta = new RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum.ExisteActorNegocio, actorNegocio);
                    }
                    else
                    {
                        respuesta = new RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum.ExisteSoloActor, actor);
                    }
                }
                else
                {
                    respuesta = new RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum.NoExisteActor);
                }

                return respuesta;

            }
            catch (Exception e)
            {
                return new RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum.NoSePudoVerificar, e.Message);
            }
        }

        public int[] obtenerAccionesPosiblesParaRolesHijos(string idUsuario, int idRolPadre, int idTipoTransaccion)
        {
            int[] idsRolesHijos = _db.Actor_negocio.SingleOrDefault(an => an.id_usuario == idUsuario).Actor.Actor_negocio.
                Where(an => an.Rol.id_rol_padre == idRolPadre && an.es_vigente == true).Select(an => an.id_rol).ToArray();

            //return _db.Rol.Where(r => r.id_rol_padre == idRolPadre)
            //conseguir roles hijos
            //acciones posibles para los roles hijos
            return _db.Rol.Where(r => idsRolesHijos.Contains(r.id)).SelectMany(r => r.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion).Select(apr => apr.id_accion_posible)).Distinct().ToArray();
            /*_db.Rol.Where(r=>r.id_rol_padre== idRolPadre).
                SelectMany(r => r.Accion_por_rol.Where(apr=>apr.id_tipo_transaccion==idTipoTransaccion)).
                Select(apr => apr.id).Intersect().ToArray();
                */
        }

        public int[] obtenerAccionesPosibles(string idUsuario, int idTipoTransaccion)
        {
            throw new NotImplementedException();
        }

        public Actor_negocio obtenerActorDeNegocio(string idUsuario, int idRolEmpleado)
        {
            return _db.Actor_negocio.SingleOrDefault(an => an.id_rol == idRolEmpleado && an.id_usuario == idUsuario);
        }

        public List<Actor_negocio> ObtenerActoresDeNegociosPorTransaccion(int idTransaccion)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Crear, Obtener, Actualizar (Vinculo Actor Negocio)


        public OperationResult CrearVinculosActorNegocio(List<Vinculo_Actor_Negocio> vinculosActoresDeNegocio)
        {
            try
            {
                foreach (var vinculoActorDeNegocio in vinculosActoresDeNegocio)
                {
                    _db.Vinculo_Actor_Negocio.Add(vinculoActorDeNegocio);
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarVinculosActorNegocio(int idActorNegocioPrincipal, List<Vinculo_Actor_Negocio> vinculosActoresDeNegocio, int tipovinculo)
        {
            try
            {
                //actualizamos los vinculos
                List<Vinculo_Actor_Negocio> dbVinculosActoresDeNegocio = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.es_vigente == true && van.tipo_vinculo == tipovinculo).ToList();

                foreach (var dbVinculo in dbVinculosActoresDeNegocio)
                {
                    //si no existe, doy de baja el actor de ese vinculo
                    if (!vinculosActoresDeNegocio.Any(van => van.id_actor_negocio_vinculado == dbVinculo.id_actor_negocio_vinculado))
                    {
                        dbVinculo.hasta = DateTimeUtil.FechaActual();
                        dbVinculo.es_vigente = false;
                    }
                }
                //agrego los nuevos vinculos
                foreach (var vinculo in vinculosActoresDeNegocio)
                {
                    if (!dbVinculosActoresDeNegocio.Any(van => van.id_actor_negocio_vinculado == vinculo.id_actor_negocio_vinculado))
                    {
                        _db.Vinculo_Actor_Negocio.Add(vinculo);
                    }
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Vinculo_Actor_Negocio ObtenerVinculoActorNegocioSegunElTipoDeVinculo(int idActorDeNegocio, int tipoVinculo)
        {
            return _db.Vinculo_Actor_Negocio.SingleOrDefault(van => van.id == idActorDeNegocio && van.tipo_vinculo == tipoVinculo);
        }

        public IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorNegocioSegunElTipoDeVinculo(int tipoVinculo)
        {
            return _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == tipoVinculo);
        }

        public OperationResult ActualizarVinculoActorNegocio(Vinculo_Actor_Negocio updVinculoActorNegocio)
        {
            try
            {
                Vinculo_Actor_Negocio dbVinculoActorNegocio = _db.Vinculo_Actor_Negocio.Single(van => van.id == updVinculoActorNegocio.id);
                _db.Entry(dbVinculoActorNegocio).CurrentValues.SetValues(updVinculoActorNegocio);
                var result = Save();
                result.data = dbVinculoActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Actor_negocio> ObtenerActorDeNegocioIncluidoTransaccion1PorRolVigentesAhora(int idRol)
        {
            return _db.Actor_negocio.
                Include(an => an.Rol).
                Include(an => an.Actor).
                Include(an => an.Actor.Direccion).
                Include(an => an.Actor.Direccion.Select(d => d.Ubigeo)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro1)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro2)).
                Include(an => an.Actor.Direccion.Select(d => d.Detalle_maestro3)).
                Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Detalle_maestro).
                Include(an => an.Actor_negocio_rol).
                Include(an => an.Actor_negocio_rol.Select(anr => anr.Rol)).
                Include(an => an.Transaccion1).
                Include(an => an.Transaccion1.Select(t => t.Cuota)).
                Where(an => an.id_rol == idRol && an.es_vigente == true);

        }

        public IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorDeNegocio(int tipoVinculo)
        {
            var temp = _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == tipoVinculo && van.es_vigente == true);
            return temp;
        }

        public IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorDeNegocio(int idActorNegocioPrincipal, int tipoVinculo)
        {
            return _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.tipo_vinculo == tipoVinculo && van.es_vigente == true);
        }

        public async Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioVigentesPorIdActorNegocioPadre(int idActorNegocioPadre)
        {
            var _dbAsync = new SigescomEntities();
            return await _dbAsync.Actor_negocio.Where(an => an.id_actor_negocio_padre == idActorNegocioPadre && an.es_vigente).ToListAsync();
        }

        public IEnumerable<Actor_negocio> ObtenerActoresDeNegocioSegunActorNegocioPrincipalYElTipoDeVinculoIncluyendoCuotas(int idActorNegocioPrincipal, int tipoVinculo)
        {


            //return _db.Actor_negocio.Where(an => an.Vinculo_Actor_Negocio.
            //                        .Include(an => an.Transaccion1)
            //                        .Include(an => an.Transaccion1.Select(t => t.Cuota.Where(c => c.por_cobrar == true && c.pago_a_cuenta < c.total)))
            //                        .Include(an => an.Transaccion1.Select(t => t.Cuota.Select(c => c.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)))
            //                        .Include(an => an.Transaccion1.Select(t => t.Cuota.Select(c => c.Estado_cuota.Select(ec => ec.Detalle_maestro))))


            //return _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.tipo_vinculo == tipoVinculo)
            //                                .Include(an => an.Actor_negocio1)
            //                                .Include(an => an.Actor_negocio.Transaccion1.Select(c => c.Cuota)
            //                               .Where(c => c.por_cobrar == true && c.pago_a_cuenta < c.total)
            //                               .Where(c => c.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            //                               .Sum(c => c.total - c.pago_a_cuenta));

            return _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.tipo_vinculo == tipoVinculo)
                                   .Select(an => an.Actor_negocio1).Include(an => an.Transaccion1.SelectMany(c => c.Cuota)
                                  .Where(c => c.por_cobrar == true && c.saldo > 0)
                                  .Sum(c => c.saldo));
        }

        public IEnumerable<int> ObtenerIdsActoresDeNegocioPrincipalDeVinculoActorNegocio(int tipoVinculo)
        {

            var idsActoresDeNegocioPrincipal = _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == tipoVinculo).Select(van => van.id_actor_negocio_principal).Distinct();
            return idsActoresDeNegocioPrincipal;
        }

        #endregion

        #region CUENTAS BANCARIAS
        public IEnumerable<CuentaBancaria> ObtenerCuentasBancarias()
        {
            return _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolCuentaBancaria).Select(an => new CuentaBancaria
            {
                Id = an.id,
                IdActor = an.id_actor,
                TipoCuenta = new ItemGenericoBase { Id = an.Actor.id_documento_identidad, Nombre = an.Actor.Detalle_maestro.nombre },
                EntidadFinanciera = new ItemGenericoBase { Id = (int)an.Actor.id_detalle_multiproposito, Nombre = an.Actor.Detalle_maestro1.nombre },
                Titular = an.Actor.segundo_nombre,
                Moneda = new ItemGenericoBase { Id = (int)an.Actor.id_detalle_multiproposito1, Nombre = an.Actor.Detalle_maestro2.nombre },
                NumeroCuenta = an.Actor.numero_documento_identidad,
                NumeroCCI = an.Actor.primer_nombre,
                Ubigeo = an.Actor.informacion_multiproposito,
                EstaActivo = an.es_vigente
            });
        }
        public IEnumerable<CuentaBancaria> ObtenerCuentasBancariasPorEntidadFinanciera(int idEntidadFinanciera)
        {
            return _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolCuentaBancaria && an.Actor.id_detalle_multiproposito == idEntidadFinanciera).Select(an => new CuentaBancaria
            {
                Id = an.id,
                IdActor = an.id_actor,
                TipoCuenta = new ItemGenericoBase { Id = an.Actor.id_documento_identidad, Nombre = an.Actor.Detalle_maestro.nombre },
                EntidadFinanciera = new ItemGenericoBase { Id = (int)an.Actor.id_detalle_multiproposito, Nombre = an.Actor.Detalle_maestro1.nombre },
                Titular = an.Actor.segundo_nombre,
                Moneda = new ItemGenericoBase { Id = (int)an.Actor.id_detalle_multiproposito1, Nombre = an.Actor.Detalle_maestro2.nombre },
                NumeroCuenta = an.Actor.numero_documento_identidad,
                NumeroCCI = an.Actor.primer_nombre,
                Ubigeo = an.Actor.informacion_multiproposito,
                EstaActivo = an.es_vigente
            });
        }

        public IEnumerable<ItemGenerico> ObtenerCuentasBancariasConEntidadFinancieraConMoneda()
        {
            return _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolCuentaBancaria && an.es_vigente).Select(an => new ItemGenerico
            {
                Id = an.id,
                Nombre = an.Actor.Detalle_maestro1.codigo + " | " + an.Actor.Detalle_maestro2.codigo + " | " + an.Actor.numero_documento_identidad,
                Valor = an.Actor.Detalle_maestro1.id.ToString()
            }); ;
        }
        #endregion 
    }
}

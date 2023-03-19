using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;

namespace Tsp.Sigescom.AccesoDatos.Actores
{
    public partial class Actor_Datos : IActor_Repositorio
    {

        public OperationResult DarDeBajaActorNegocioAhora(int idActorNegocio, int idRol)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Damos de baja al rol padre
                var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor.Actor_negocio).Single(a => a.id == idActorNegocio);
                dbActorNegocio.fecha_fin = fechaActual;
                dbActorNegocio.es_vigente = false;
                //Damos de baja a los roles hijos
                List<Actor_negocio> dbRoles = dbActorNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == idRol && an.es_vigente == true).ToList();
                foreach (var dbRol in dbRoles)
                {
                    dbRol.fecha_fin = fechaActual;
                    dbRol.es_vigente = false;
                }
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearParametroActorNegocio(Parametro_actor_negocio parametroActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                _db.Parametro_actor_negocio.Add(parametroActorNegocio);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = parametroActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarParametroActorNegocio(Parametro_actor_negocio parametroActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbParametroActorNegocio = _db.Parametro_actor_negocio.Single(an => an.id == parametroActorNegocio.id);
                _db.Entry(dbParametroActorNegocio).CurrentValues.SetValues(parametroActorNegocio);
                _db.SaveChanges();

                var result = new OperationResult(OperationResultEnum.Success);
                result.data = parametroActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResolverParametrosActorNegocio(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            List<Parametro_actor_negocio> dbParametros = dbActorNegocio.Parametro_actor_negocio.ToList();
            List<Parametro_actor_negocio> updParametros = updActorNegocio.Parametro_actor_negocio.ToList();
            foreach (var dbParametro in dbParametros)
            {
                //Si existe el parametro, lo actualizamos
                if (updParametros.Any(p => p.id_parametro == dbParametro.id_parametro))
                {
                    dbParametro.id_valor_parametro = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).id_valor_parametro;
                    dbParametro.valor = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).valor;
                }
                else//Sino eliminamos el parametro
                {
                    _db.Parametro_actor_negocio.Remove(dbParametro);
                }
            }
            //Agregamos los nuevos parametros
            foreach (var updParametro in updParametros)
            {
                if (!dbParametros.Any(p => p.id_parametro == updParametro.id_parametro))
                {
                    _db.Parametro_actor_negocio.Add(updParametro);
                }
            }
        }

        public void ResolverDireccion(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            List<Direccion> updDirecciones = updActorNegocio.Actor.Direccion.ToList();
            List<Direccion> dbDirecciones = dbActorNegocio.Actor.Direccion.ToList();
            foreach (var dbDireccion in dbDirecciones)
            {
                //Si existe se actualiza la direccion
                if (updDirecciones.Any(d => d.id == dbDireccion.id))
                {
                    var direccion = updDirecciones.Single(d => d.id == dbDireccion.id);
                    _db.Entry(dbDireccion).CurrentValues.SetValues(direccion);
                }
                else//Sino se elimina la direccion
                {
                    _db.Direccion.Remove(dbDireccion);
                }
            }
            //Agregamos las nuevas direcciones
            foreach (var updDireccion in updDirecciones)
            {
                if (!dbDirecciones.Any(d => d.id == updDireccion.id))
                {
                    _db.Direccion.Add(updDireccion);
                }
            }
        }

        public void ResolverRoles(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            List<Actor_negocio> dbRoles = dbActorNegocio.Actor.Actor_negocio.Where(an => an.Rol.id_rol_padre == updActorNegocio.id_rol && an.es_vigente == true).ToList();
            List<Actor_negocio> updRoles = updActorNegocio.Actor.Actor_negocio.ToList();
            foreach (var dbRol in dbRoles)
            {
                //Si no existe el rol, damos de baja el actor negocio con ese rol
                if (!updRoles.Any(p => p.id_rol == dbRol.id_rol))
                {
                    dbRol.fecha_fin = DateTimeUtil.FechaActual();
                    dbRol.es_vigente = false;
                }
            }
            //Agregamos los roles nuevos
            foreach (var updRol in updRoles)
            {
                if (!dbRoles.Any(p => p.id_rol == updRol.id_rol))
                {
                    _db.Actor_negocio.Add(updRol);
                }
            }
        }
        public void ResolverFoto(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            Foto foto_upd = updActorNegocio.Actor.Foto ?? null;
            Foto foto_bd = dbActorNegocio.Actor.Foto ?? null;
            if (foto_upd != null)
            {
                if (foto_bd != null && foto_bd.imagen != foto_upd.imagen)
                {
                    foto_upd.id = foto_bd.id;
                    _db.Entry(foto_bd).CurrentValues.SetValues(foto_upd);
                }
                else
                {
                    _db.Foto.Add(foto_upd);
                }
            }
        }

        public OperationResult CrearActorNegocio(Actor_negocio actorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                _db.Actor_negocio.Add(actorNegocio);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = actorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearActorNegocioActualizandoActor(Actor_negocio actorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                List<Actor_negocio> updRoles = actorNegocio.Actor.Actor_negocio.ToList(); ;
                var dbActor = _db.Actor.Include(a => a.Direccion).Single(a => a.id == actorNegocio.id_actor);

                _db.Entry(dbActor).CurrentValues.SetValues(actorNegocio.Actor);

                List<Direccion> updDirecciones = actorNegocio.Actor.Direccion.ToList();
                List<Direccion> dbDirecciones = dbActor.Direccion.ToList();
                foreach (var dbDireccion in dbDirecciones)
                {
                    //Si existe actualizo la direccion
                    if (updDirecciones.Any(d => d.id == dbDireccion.id))
                    {
                        var direccion = updDirecciones.Single(d => d.id == dbDireccion.id);
                        _db.Entry(dbDireccion).CurrentValues.SetValues(direccion);
                    }
                    else//Sino lo elimino la direccion
                    {
                        _db.Direccion.Remove(dbDireccion);
                    }
                }
                //Agregamos las nuevas direcciones
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
                //Agregamos los Roles si lo tiene
                foreach (var updRol in updRoles)
                {
                    _db.Actor_negocio.Add(updRol);
                }
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = actorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarActorNegocio(Actor_negocio updActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Direccion).
                    Include(an => an.Actor_negocio_rol).Include(an => an.Parametro_actor_negocio).Single(an => an.id == updActorNegocio.id);

                //Resolvemos la parte de actualizacion de los datos del actor de negocio a actualizar
                ResolverActualizacionDeActorNegocio(dbActorNegocio, updActorNegocio, _db);

                ResolverParametrosActorNegocio(dbActorNegocio, updActorNegocio, _db);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = updActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarActorNegocioCreandoActor(Actor_negocio updActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                //Para actualizar el id de actor en actor de negocio, necesito el id del actor que recién se guardará
                _db.Entry(updActorNegocio.Actor).State = EntityState.Added;
                //Traemos el actor comercial de la BD
                var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Direccion).
                    Include(an => an.Actor_negocio_rol).Include(an => an.Parametro_actor_negocio).Single(an => an.id == updActorNegocio.id);

                //Resolvemos la parte de actualizacion de los datos del actor de negocio a actualizar
                ResolverActualizacionDeActorNegocioSinActualizarActor(dbActorNegocio, updActorNegocio, _db);

                ResolverParametrosActorNegocio(dbActorNegocio, updActorNegocio, _db);

                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = updActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResolverActualizacionDeActorNegocioSinActualizarActor(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            updActorNegocio.id_usuario = dbActorNegocio.id_usuario;
            updActorNegocio.fecha_inicio = dbActorNegocio.fecha_inicio;
            updActorNegocio.fecha_fin = dbActorNegocio.fecha_fin;

            //Estos datos no se modifican del actor de negocio 
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);
        }

        public OperationResult ActualizarActorNegocioIncluidoActor(Actor_negocio updActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                Actor_negocio dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor).Single(an => an.id == updActorNegocio.id);
                _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);
                _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(updActorNegocio.Actor);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = updActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarActorNegocioSinTomarEnCuentaAParametros(Actor_negocio updActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor.Actor_negocio).Include(an => an.Actor.Direccion).
                    Include(an => an.Actor_negocio_rol).Include(an => an.Parametro_actor_negocio).Single(an => an.id == updActorNegocio.id);
                //Resolvemos la parte de actualizacion de los datos del actor de negocio a actualizar
                ResolverActualizacionDeActorNegocio(dbActorNegocio, updActorNegocio, _db);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = updActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResolverActualizacionDeActorNegocio(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            //Estos datos no se modifican del actor de negocio 
            updActorNegocio.id_usuario = dbActorNegocio.id_usuario;
            updActorNegocio.fecha_inicio = dbActorNegocio.fecha_inicio;
            updActorNegocio.fecha_fin = dbActorNegocio.fecha_fin;
            updActorNegocio.id_actor_negocio_padre = dbActorNegocio.id_actor_negocio_padre;

            //Datos escalares de Actor de Negocio
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);

            //Datos escalares de Actor
            _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(updActorNegocio.Actor);

            ResolverDireccion(dbActorNegocio, updActorNegocio, _db);
            ResolverRoles(dbActorNegocio, updActorNegocio, _db);
            ResolverFoto(dbActorNegocio, updActorNegocio, _db);
        }

        public OperationResult ActualizarActorNegocioYIdActorNegocioPadre(Actor_negocio updActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocio = _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Direccion).
                    Include(an => an.Actor_negocio_rol).Include(an => an.Parametro_actor_negocio).Single(an => an.id == updActorNegocio.id);
                ResolverActualizacionDeActorNegocioYIdActorNegocioPadre(dbActorNegocio, updActorNegocio, _db);
                ResolverParametrosActorNegocio(dbActorNegocio, updActorNegocio, _db);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = updActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResolverActualizacionDeActorNegocioYIdActorNegocioPadre(Actor_negocio dbActorNegocio, Actor_negocio updActorNegocio, SigescomEntities _db)
        {
            //Estos datos no se modifican del actor de negocio 
            updActorNegocio.id_usuario = dbActorNegocio.id_usuario;
            updActorNegocio.fecha_inicio = dbActorNegocio.fecha_inicio;
            updActorNegocio.fecha_fin = dbActorNegocio.fecha_fin;

            //Datos escalares de Actor de Negocio
            _db.Entry(dbActorNegocio).CurrentValues.SetValues(updActorNegocio);

            //Datos escalares de Actor
            _db.Entry(dbActorNegocio.Actor).CurrentValues.SetValues(updActorNegocio.Actor);

            ResolverDireccion(dbActorNegocio, updActorNegocio, _db);
            ResolverRoles(dbActorNegocio, updActorNegocio, _db);
            ResolverFoto(dbActorNegocio, updActorNegocio, _db);
        }

        public bool ExisteDocumento(int idDocumentoIdentidad, string numeroDocumento)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor.Any(a => a.id_documento_identidad == idDocumentoIdentidad && a.numero_documento_identidad == numeroDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool ExisteActorConElMismoDocumentoYDistintoId(int id, string documentoIdentidad)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor.Any(a => a.numero_documento_identidad == documentoIdentidad && a.id != id);
        }
        public bool ExisteActorComercialConElMismoDocumentoVigente(int idRol, string documentoIdentidad)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Any(an => an.id_rol == idRol && an.Actor.numero_documento_identidad == documentoIdentidad && an.es_vigente);
        }
        public bool ExisteActorComercialConElMismoDocumentoVigente(int idActorComercialDiferente, int idRol, string documentoIdentidad)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Any(an => an.id != idActorComercialDiferente && an.id_rol == idRol && an.Actor.numero_documento_identidad == documentoIdentidad && an.es_vigente);
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
        public IEnumerable<Actor_negocio> ObtenerActorDeNegocioPrincipalVigentes(int idParentRol, int idRol)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Include(an => an.Actor).
                Include(an => an.Actor.Tipo_actor).
                Include(an => an.Actor.Detalle_maestro).
                Include(an => an.Parametro_actor_negocio).
                Include(an => an.Actor_negocio_rol.Select(anr => anr.Rol)).
                Include(an => an.Rol)
                .Where(an => an.id_rol == idRol && an.es_vigente).
                Select(an => an.Actor).SelectMany(a => a.Actor_negocio).
                Where(an => an.id_rol == idParentRol && an.es_vigente);
        }
        public ActorComercial_ ObtenerActorComercial(int id)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var actorComercial = _db.Actor_negocio
                    .Where(an => an.id == id).Select(an => new ActorComercial_
                    {
                        Id = an.id,
                        IdActor = an.id_actor,
                        NombreORazonSocial = an.Actor.primer_nombre,
                        NombreComercial = an.Actor.segundo_nombre,

                        NumeroDocumentoIdentidad = an.Actor.numero_documento_identidad,
                        TipoDocumentoIdentidad = new ItemGenerico() { Id = an.Actor.id_documento_identidad, Nombre = an.Actor.Detalle_maestro.valor },
                        TipoPersona = new ItemGenerico() { Id = an.Actor.id_tipo_actor, Nombre = an.Actor.Tipo_actor.nombre },
                        DomicilioFiscal = new Direccion_() { Id = an.Actor.Direccion.FirstOrDefault().id, Pais = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = an.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre }, Ubigeo = new ItemGenerico() { Id = an.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga }, Detalle = an.Actor.Direccion.FirstOrDefault().detalle }
                    }).FirstOrDefault();
                actorComercial.ApellidoPaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actorComercial.NombreORazonSocial.Split('|')[0] : "";
                actorComercial.ApellidoMaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 1 ? actorComercial.NombreORazonSocial.Split('|')[1] : "";
                actorComercial.Nombres = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 2 ? actorComercial.NombreORazonSocial.Split('|')[2] : "";
                actorComercial.NombreORazonSocial = actorComercial.NombreORazonSocial.Replace("|", " ");
                return actorComercial;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Indica si un actor de negocio participa en alguna transaccion ya sea como actor de negocio interno o externo
        /// </summary>
        /// <param name="idActorComercial"></param>
        /// <returns></returns>
        public bool ActorParticipaEnTransacciones(int idActorComercial)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Transaccion.Any(t => t.id_actor_negocio_interno == idActorComercial || t.id_actor_negocio_externo == idActorComercial || t.id_actor_negocio_interno1 == idActorComercial || t.id_actor_negocio_externo1 == idActorComercial);
        }


        public OperationResult ActualizarActor(Actor actor)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                Actor dbActor = _db.Actor.Single(m => m.id == actor.id);
                _db.Entry(dbActor).CurrentValues.SetValues(actor);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = actor.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public IEnumerable<Actor_negocio> ObtenerActorDeNegocioPorRolVigentesAhora(int idRol)
        {
            SigescomEntities _db = new SigescomEntities();
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
                Where(an => an.id_rol == idRol && an.es_vigente == true);

        }

        public OperationResult InvertirEsVigenteActorNegocio(int idActorNegocio)
        {
            try
            {
                //damos de baja al rol padre
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocio = _db.Actor_negocio.FirstOrDefault(an => an.id == idActorNegocio);
                dbActorNegocio.es_vigente = !dbActorNegocio.es_vigente;
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al invertir Esvigente Actor negocio", e);
            }
        }
        public OperationResult InvertirIndicador1ActorNegocio(int idActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocio = _db.Actor_negocio.FirstOrDefault(an => an.id == idActorNegocio);
                dbActorNegocio.indicador1 = !dbActorNegocio.indicador1;
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al invertir indicador 1 de Actor negocio", e);
            }

        }

        public int[] ObtenerIdsActorDeNegocioVigentePrincipal(int idParentRol, int idRol)
        {

            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente).Select(an => an.Actor).SelectMany(a => a.Actor_negocio).Where(an => an.id_rol == idParentRol).Select(an => an.id).ToArray();
        }

        public string ObtenerExtensionJsonDeActorNegocio(int idActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio.Single(an => an.id == idActorNegocio).extension_json;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener la extension json del actor de negocio", e);
            }
        }

        public IEnumerable<ResumenCliente> ObtenerResumenClientesVigentes()
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolCliente && an.es_vigente == true).Select(an =>
            new ResumenCliente()
            {
                Id = an.id,
                Codigo = an.codigo_negocio,
                RazonSocial = an.Actor.primer_nombre.Replace("|", " "),
                NumeroDocumentoIdentidad = an.Actor.numero_documento_identidad,
                TipoDocumentoIdentidad = an.Actor.Detalle_maestro.valor,
                TipoPersona = an.Actor.Tipo_actor.nombre,
                Telefono = an.Actor.telefono,
                Correo = an.Actor.correo,
                DetalleDireccion = an.Actor.Direccion.FirstOrDefault().detalle,
                UbigeoDireccion = an.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
            });
        }
    }


}
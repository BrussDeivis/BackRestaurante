using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos.Roles

{
    public partial class Roles_Datos: IRoles_Repositorio
    {
        public OperationResult actualizarAccionesPorRolYEstado(List<Accion_por_rol> updRolAcciones, List<Accion_por_estado> updEstadoAcciones, int idTipoTransaccion, int idRol, int idEstadoActual)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();

                List<Accion_por_rol> dbRolAcciones = obtenerAccionesPorTipoTrasaccionYRol(idTipoTransaccion, idRol).ToList();
                List<Accion_por_estado> dbEstadoAcciones = obtenerAccionesPorTipoTrasaccionYEstadoActual(idTipoTransaccion, idEstadoActual).ToList();
                OperationResult result = new OperationResult();

                if (updRolAcciones != null && idRol != 0)
                {
                    //Aqui agregamos los rolAccion que no estan en la BD
                    foreach (var updRolAccion in updRolAcciones)
                    {
                        try
                        {
                            Accion_por_rol rolAccionViejo = dbRolAcciones.SingleOrDefault(ra => ra.id_unidad_negocio == updRolAccion.id_unidad_negocio && ra.id_accion_posible == updRolAccion.id_accion_posible);

                            if (rolAccionViejo == null)
                            {
                                //agregar un nuevo rolAccion que no existe actualmente en la BD
                                _db.Accion_por_rol.Add(updRolAccion);
                            }
                        }
                        catch (Exception e)
                        {
                            result.setResult(OperationResultEnum.Error);
                            result.exceptions.Add(e);
                            break;
                        }
                    }

                    //Ahora eliminamos de la BD aquellos rolAccion que no se encuentran en la lista actualizada
                    foreach (var dbRolAccion in dbRolAcciones)
                    {
                        if (!updRolAcciones.Any(ra => ra.id_accion_posible == dbRolAccion.id_accion_posible && ra.id_unidad_negocio == dbRolAccion.id_unidad_negocio))
                        {
                            _db.Accion_por_rol.Remove(dbRolAccion);
                        }
                    }
                }
                if (updEstadoAcciones != null && idEstadoActual != 0)
                {
                    //Aqui agregamos los estadoAccion que no estan en la BD
                    foreach (var updEstadoAccion in updEstadoAcciones)
                    {
                        try
                        {
                            Accion_por_estado estadoAccionViejo = dbEstadoAcciones.SingleOrDefault(ea => ea.id_accion_posible == updEstadoAccion.id_accion_posible);

                            if (estadoAccionViejo == null)
                            {
                                //agregar un nuevo estadoAccion que no existe actualmente en la BD
                                _db.Accion_por_estado.Add(updEstadoAccion);
                            }
                        }
                        catch (Exception e)
                        {
                            result.setResult(OperationResultEnum.Error);
                            result.exceptions.Add(e);
                            break;
                        }
                    }

                    //Ahora eliminamos de la BD aquellos estadoAccion que no se encuentran en la lista actualizada
                    foreach (var dbEstadoAccion in dbEstadoAcciones)
                    {
                        if (!updEstadoAcciones.Any(ra => ra.id_accion_posible == dbEstadoAccion.id_accion_posible))
                        {
                            _db.Accion_por_estado.Remove(dbEstadoAccion);
                        }
                    }
                }
                _db.SaveChanges();
                var resultadoGuardar = new OperationResult(OperationResultEnum.Success);

                return resultadoGuardar.code_result == OperationResultEnum.Success ? result : resultadoGuardar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Accion_por_rol> obtenerAccionesPorTipoTrasaccionYRol(int idTipoTransaccion, int idRol)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion && apr.id_rol == idRol);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Accion_por_estado> obtenerAccionesPorTipoTrasaccionYEstadoActual(int idTipoTransaccion, int idEstadoOperacion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Accion_por_estado.Where(ape => ape.id_tipo_transaccion == idTipoTransaccion && ape.id_estado_actual == idEstadoOperacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Tipo_transaccion> ObtenerTiposDeTransacciones()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Tipo_transaccion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve la interseccion entre los roles de la accion y los roles vigentes que tiene el actor de negocio
        /// </summary>
        /// <param name="idActorNegocio"></param>
        /// <param name="idRolPadre"></param>
        /// <param name="idAccionPosible"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idUnidadNegocio"></param>
        /// <returns></returns>
        public int ContarInterseccionRolesAccion_RolesVigentesActorNegocio(int idActorNegocio, int idRolPadre, int idAccionPosible, int idTipoTransaccion, int idUnidadNegocio)
        {
            SigescomEntities _db = new SigescomEntities();

            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                var rolesAccion = _db.Accion_por_rol.Where(apr => apr.id_accion_posible == idAccionPosible && apr.id_tipo_transaccion == idTipoTransaccion && apr.id_unidad_negocio == idUnidadNegocio).Select(apr => apr.id_rol).ToArray();
                //traer id de actor 
                int idActor = _db.Actor_negocio.First(an => an.id == idActorNegocio).id_actor;
                //traer los ids de actor de negocio de l
                //_db.Configuration.LazyLoadingEnabled = true;

                var rolesVigentesActorNegocio = _db.Actor_negocio.Where(an => an.id_actor == idActor && an.es_vigente == true).Select(an => an.id_rol).ToArray();

                return rolesAccion.Intersect(rolesVigentesActorNegocio).Count();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }

        }

        public bool ActorNegocioTieneRolVigente(int idActorNegocio, int idRol)
        {
            SigescomEntities _db = new SigescomEntities();
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                //Traer id de actor del actor de negocio
                int idActor = _db.Actor_negocio.First(an => an.id == idActorNegocio).id_actor;
                //Obtener el numero de cuantas veces tiene el IdRol ingresado
                var contadorRolesActorNegocio = _db.Actor_negocio.Where(an => an.id_actor == idActor && an.es_vigente == true).Count(an => an.id_rol == idRol);
                return contadorRolesActorNegocio > 0;
            }
            catch (Exception)
            {
                throw new DatosException("Error al verificar si actor de negocio tiene su rol consultado");
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }

        public List<Rol> ObtenerRoles(int[] idsRoles)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Rol.Where(r => idsRoles.Contains(r.id)).ToList();
        }
        public IEnumerable<RolDeNegocio_> ObtenerRoles(int idActor, int idRolPadre)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Actor_negocio.Where(an => an.id_actor == idActor && an.es_vigente && an.Rol.id_rol_padre == idRolPadre).Select(ann => new RolDeNegocio_ { Id = ann.Rol.id, Nombre = ann.Rol.nombre, AplicaA = (int)ann.Rol.aplica_a });
        }
        public Rol obtenerRol(int idRol)
        {

            SigescomEntities _db = new SigescomEntities();
            return _db.Rol.Single(r => r.id == idRol);
        }

        public IEnumerable<Rol> ObtenerRolesHijos(int idRolPadre)
        {
            try
            {
            SigescomEntities _db = new SigescomEntities();
                return _db.Rol.Where(r => r.id_rol_padre == idRolPadre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
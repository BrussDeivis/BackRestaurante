using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica.Core.Permisos
{
    public class Permisos_Logica: IPermisos_Logica
    {
        protected readonly IRoles_Repositorio _rolesDatos;

        public Permisos_Logica(IRoles_Repositorio rolesDatos)
        {
            _rolesDatos = rolesDatos;
        }

        public void ValidarAccion(int idEmpleado, int idAccion, int idTipoTransaccion, int idUnidadNegocio)
        {
            var intersectedActions = _rolesDatos.ContarInterseccionRolesAccion_RolesVigentesActorNegocio(idEmpleado, ActorSettings.Default.IdRolEmpleado, idAccion, idTipoTransaccion, idUnidadNegocio);
            //AccionOperativa accionAIntentar = new AccionOperativa(maestroRepositorio.obtenerDetalle(idAccion));
            //Empleado empleado = new Empleado(actorRepositorio.obtenerActorDeNegocio(idEmpleado, ActorSettings.Default.IdRolEmpleado));
            if (intersectedActions <= 0)
            {
                throw new LogicaException("No tiene permiso para realizar esta operación");
            }
        }

        public bool ValidarActorNegocioTieneRolVigente(int idEmpleado, int idRol)
        {
            return _rolesDatos.ActorNegocioTieneRolVigente(idEmpleado, idRol);
        }

        public List<Rol> ObtenerRolesTercero()
        {
            try
            {
                int[] idsRoles = { ActorSettings.Default.IdRolCliente, ActorSettings.Default.IdRolProveedor };
                return _rolesDatos.ObtenerRoles(idsRoles).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Tipo_transaccion> ObtenerTiposDeTransaccion()
        {
            try
            {
                return _rolesDatos.ObtenerTiposDeTransacciones().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Accion_por_rol> ObtenerAccionesPosiblesPorTipoTransaccionYRolPersonal(int idTipoTransaccion, int idRolPersonal)
        {
            try
            {
                return _rolesDatos.obtenerAccionesPorTipoTrasaccionYRol(idTipoTransaccion, idRolPersonal).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Accion_por_estado> ObtenerAccionesPosiblesPorTipoTransaccionYEstadoActual(int idTipoTransaccion, int idEstadoActual)
        {
            try
            {
                return _rolesDatos.obtenerAccionesPorTipoTrasaccionYEstadoActual(idTipoTransaccion, idEstadoActual).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarPermisosPorRolYEstado(List<Accion_por_rol> rolAcciones, List<Accion_por_estado> estadoAcciones, int idTipoTransaccion, int idRolPersonal, int idEstadoActual)
        {
            try
            {
                if (rolAcciones != null)
                {
                    List<Accion_por_rol> rolAccionesNuevos = rolAcciones.Where(ra => ra.id <= 0).ToList();

                    foreach (var rolAccionNuevo in rolAccionesNuevos)
                    {
                        if (rolAccionesNuevos.Count(ra => ra.id_tipo_transaccion == rolAccionNuevo.id_tipo_transaccion && ra.id_rol == rolAccionNuevo.id_rol && ra.id_accion_posible == rolAccionNuevo.id_accion_posible && ra.id_unidad_negocio == rolAccionNuevo.id_unidad_negocio) > 1)
                        {
                            return new OperationResult(new Exception("Existen Acciones duplicados para ese Rol, porfavor verifique sus datos"));
                        }
                    }
                }
                if (estadoAcciones != null)
                {
                    List<Accion_por_estado> estadoAccionesNuevos = estadoAcciones.Where(ra => ra.id <= 0).ToList();

                    foreach (var estadoAccionNuevo in estadoAccionesNuevos)
                    {
                        if (estadoAccionesNuevos.Count(ea => ea.id_tipo_transaccion == estadoAccionNuevo.id_tipo_transaccion && ea.id_estado_actual == estadoAccionNuevo.id_estado_actual && ea.id_accion_posible == estadoAccionNuevo.id_accion_posible) > 1)
                        {
                            return new OperationResult(new Exception("Existen Acciones duplicados para este Estado, porfavor verifique sus datos"));
                        }
                    }
                }

                return _rolesDatos.actualizarAccionesPorRolYEstado(rolAcciones, estadoAcciones, idTipoTransaccion, idRolPersonal, idEstadoActual);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

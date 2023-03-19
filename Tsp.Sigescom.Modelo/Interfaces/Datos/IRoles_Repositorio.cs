using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
   public interface IRoles_Repositorio
    {
        int ContarInterseccionRolesAccion_RolesVigentesActorNegocio(int idActorNegocio, int idRolPadre, int idAccionPosible, int idTipoTransaccion, int idUnidadNegocio);
        bool ActorNegocioTieneRolVigente(int idActorNegocio, int idRol);


        /// <summary>
        /// retorna las acciones posibles para un empleado en una transaccion
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idRolPersonal"></param>
        /// <returns></returns>
        IEnumerable<Accion_por_rol> obtenerAccionesPorTipoTrasaccionYRol(int idTipoTransaccion, int idRolPersonal);

        /// <summary>
        /// actualiza las acciones por rol y estado 
        /// </summary>
        /// <param name="rolAcciones"></param>
        /// <param name="estadoAcciones"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idRol"></param>
        /// <param name="idEstadoActual"></param>
        /// <returns></returns>
        OperationResult actualizarAccionesPorRolYEstado(List<Accion_por_rol> rolAcciones, List<Accion_por_estado> estadoAcciones, int idTipoTransaccion, int idRol, int idEstadoActual);

        /// <summary>
        /// retorna las acciones de una transaccion por estado
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idEstadoOperacion"></param>
        /// <returns></returns>
        IEnumerable<Accion_por_estado> obtenerAccionesPorTipoTrasaccionYEstadoActual(int idTipoTransaccion, int idEstadoOperacion);

        /// <summary>
        /// retorna todos los tipos de transaccion 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tipo_transaccion> ObtenerTiposDeTransacciones();

        /// <summary>
        /// devuelve los roles especificos
        /// </summary>
        /// <param name="idsRoles"></param>
        /// <returns></returns>
        List<Rol> ObtenerRoles(int[] idsRoles);

        /// <summary>
        /// devuelve una lista de Roles perteneciente a un Rol Padre
        /// </summary>
        /// <param name="idRolPadre"></param>
        /// <returns></returns>
        IEnumerable<Rol> ObtenerRolesHijos(int idRolPadre);


        IEnumerable<RolDeNegocio_> ObtenerRoles(int idActor, int idRolPadre);

        Rol obtenerRol(int idRol);

    }

}

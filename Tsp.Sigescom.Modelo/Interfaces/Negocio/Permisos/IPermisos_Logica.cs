using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IPermisos_Logica
    {
        /// <summary>
        /// devuelve todos los tipos de transaccion
        /// </summary>
        /// <returns></returns>
        List<Tipo_transaccion> ObtenerTiposDeTransaccion();

        /// <summary>
        /// devuelve una coleccion de acciones posibles para una transaccion en un estado
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        List<Accion_por_estado> ObtenerAccionesPosiblesPorTipoTransaccionYEstadoActual(int idTipoTransaccion, int idEstado);

        /// <summary>
        /// devuelve una coleccion de acciones posibles para un personal en un transaccion 
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idRolPersonal"></param>
        /// <returns></returns>
        List<Accion_por_rol> ObtenerAccionesPosiblesPorTipoTransaccionYRolPersonal(int idTipoTransaccion, int idRolPersonal);

        /// <summary>
        /// actualiza los permisos por rol y estado de una transaccion
        /// </summary>
        /// <param name="rolAcciones"></param>
        /// <param name="estadoAcciones"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idRolPersonal"></param>
        /// <param name="idEstadoActual"></param>
        /// <returns></returns>
        OperationResult ActualizarPermisosPorRolYEstado(List<Accion_por_rol> rolAcciones, List<Accion_por_estado> estadoAcciones, int idTipoTransaccion, int idRolPersonal, int idEstadoActual);


        void ValidarAccion(int idEmpleado, int idAccion, int idTipoTransaccion, int idUnidadNegocio);
        bool ValidarActorNegocioTieneRolVigente(int idEmpleado, int idRol);
    }
}


using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IEmpleado_Logica
    {
        /// <summary>
        /// obtiene un empleado con su id de usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Empleado_ ObtenerEmpleadoInclusiveRoles(string idUsuario);

        Empleado_ ObtenerEmpleadoInclusiveRoles(int id);

        /// <summary>
        /// devuelve el id del empleado que tenga asignado el idUsuario dado. Si no existe, devuelve 0
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        int ObtenerIdEmpleado(string idUsuario);

        /// <summary>
        /// devuelve el nombre del empleado que tenga asignado el idUsuario dado. Si no existe, devuelve ""
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        string ObtenerNombreEmpleado(string idUsuario);

    }
}


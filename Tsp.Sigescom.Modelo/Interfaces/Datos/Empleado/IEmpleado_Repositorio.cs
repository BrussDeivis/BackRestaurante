using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Empleado
{
    public interface IEmpleado_Repositorio
    {
        Empleado_ ObtenerEmpleado(string idUsuario);
        Empleado_ ObtenerEmpleado(int id);
        /// <summary>
        /// devuelve el id del actor de negocio que corresponde al id de usuario 
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        int ObtenerId(string idUsuario);
        /// <summary>
        /// devuelve el nombre del actor de negocio que corresponde al id de usuario 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        string ObtenerNombre(string idUsuario);

    }
}

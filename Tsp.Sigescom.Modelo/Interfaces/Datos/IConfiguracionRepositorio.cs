using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IConfiguracionRepositorio
    {

        #region crear y actualizar
        /// <summary>
        /// crea un nuevo configuracion
        /// </summary>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        OperationResult crearConfiguracion(Configuracion configuracion);

        /// <summary>
        /// actualiza los datos de un configuracion 
        /// </summary>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        OperationResult actualizarConfiguracion(Configuracion configuracion);

        /// <summary>
        /// crea un parametro de un configuracion 
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        OperationResult crearParametroConfiguracion(Parametro_de_configuracion parametro);

        /// <summary>
        /// actualiza los datos de un parametro configuracion 
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        OperationResult actualizarParametroConfiguracion(Parametro_de_configuracion parametro);
        #endregion



        #region consultas
        /// <summary>
        /// devuelve todos los datos de la tabla configuraciones
        /// </summary>
        /// <returns></returns>
        IEnumerable<Configuracion> obtenerConfiguraciones();

        /// <summary>
        /// obtiene un parametro especifico con el id dado
        /// </summary>
        /// <param name="idConfiguracion"></param>
        /// <returns></returns>
        IEnumerable<Parametro_de_configuracion> obtenerParametrosConfiguracion(int idConfiguracion);
        Parametro_de_configuracion ObtenerParametroDeConfiguracion(string nombre);


        Detalle_maestro ObtenerDetalleMaestroIncluidoTipoTransaccionTipoComprobante(int idMaestro, int idTipoDeComprobante);
        IEnumerable<Detalle_maestro> ObtenerDetallesMaestroIncluidoTipoTransaccionTipoComprobante(int idMaestro);
        OperationResult ActualizarDetalleMaestroConTipoTransaccionTipoComprobante(Detalle_maestro detalle);
        #endregion

        #region crear Rol
        OperationResult CrearRol(Rol rol);
        IEnumerable<Rol> ObtenerRoles();
        #endregion

    }

}

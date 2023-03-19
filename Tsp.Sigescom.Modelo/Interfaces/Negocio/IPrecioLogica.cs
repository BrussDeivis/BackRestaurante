using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IPrecioLogica 
    {
        #region crear
        /// <summary>
        /// actualiza los datos de un precio
        /// </summary>
        /// <param name="idPrecio"></param>
        /// <param name="idProducto"></param>
        /// <param name="idTarifa"></param>
        /// <param name="importe"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        OperationResult actualizarPrecio(int idCentroAtencion, int idPrecio, int idProducto, int idTarifa, decimal importe, DateTime fechaDesde, DateTime fechaHasta, string descripcion, int idResponsable);
        /// <summary>
        /// crea un nuevo precio, si existe un precio con un producto y tarifa igual lo caduca y guarda el nuevo
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idTarifa"></param>
        /// <param name="importe"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        OperationResult establecerPrecio(int idCentroAtencion, int idProducto, int idTarifa, decimal importe, DateTime fechaDesde, DateTime fechaHasta, string descripcion, int idResponsable);
        /// <summary>
        /// crea una bonificacion para un producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="porcentaje"></param>
        /// <param name="valor"></param>
        /// <param name="cantidadMinima"></param>
        /// <param name="cantidadMaxima"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idProductoReferencia"></param>
        /// <param name="idResponsable"></param>
        /// <returns></returns>
        OperationResult crearBonificacion(int idCentroAtencion, int idProducto, bool porcentaje, decimal valor, int cantidadMinima, int cantidadMaxima, DateTime fechaDesde, DateTime fechaHasta, int idProductoReferencia, string descripcion, int idResponsable);
        /// <summary>
        /// crea un descuento para un producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="porcentaje"></param>
        /// <param name="valor"></param>
        /// <param name="cantidadMinima"></param>
        /// <param name="cantidadMaxima"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idResponsable"></param>
        /// <returns></returns>
        OperationResult crearDescuento(int idCentroAtencion, int idProducto, bool porcentaje, decimal valor, int cantidadMinima, int cantidadMaxima, DateTime fechaDesde, DateTime fechaHasta, string descripcion, int idResponsable);
        /// <summary>
        /// caduca el precio en la fecha actual
        /// </summary>
        /// <param name="idPrecio"></param>
        /// <returns></returns>
        OperationResult caducarPrecio(int idPrecio);
        #endregion
        #region consultas
        /// <summary>
        /// devuelve un colecion de precios vigentes 
        /// </summary>
        /// <returns></returns>
        List<Precio> obtenerPreciosVigentes();

        List<Precio_Concepto> ObtenerPrecios();

        /// <summary>
        /// devuelve una coleccion de descuentos
        /// </summary>
        /// <returns></returns>
        List<Precio> obtenerDescuentos();

        /// <summary>
        /// obtiene una coleccion de bonificaciones
        /// </summary>
        /// <returns></returns>
        List<Precio> obtenerBonificaciones();

        /// <summary>
        /// obtiene un precio por su id
        /// </summary>
        /// <param name="idPrecio"></param>
        /// <returns></returns>
        Precio obtenerPrecio(int idPrecio);

        
        /// <summary>
        /// Obtiene los precios por su id
        /// </summary>
        /// <param name="idMercaderia"></param>
        /// <returns></returns>
        List<Precio> ObtenerPreciosMercaderiaUnica(int idMercaderia);

        OperationResult establecerPrecios(int idCentroAtencion, int idSede, int idEmpleado, List<Precio> precios, int idMercaderia);
        OperationResult EstablecerPrecio(List<Precio_Compra_Venta_Concepto> precios, int idConcepto, int idEmpleado);

        #endregion

    }
}

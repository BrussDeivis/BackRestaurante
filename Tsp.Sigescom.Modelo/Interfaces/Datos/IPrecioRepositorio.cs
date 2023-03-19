using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IPrecioRepositorio : IRepositorioBase
    {
        /// <summary>
        /// crea un nuevo precio para descuento y bonificacion
        /// </summary>
        /// <param name="precio"></param>
        /// <returns></returns>
        OperationResult CrearPrecio(Precio precio);
        /// <summary>
        /// devuelve una collecion de precios vigentes
        /// </summary>
        /// <returns></returns>
        IEnumerable<Precio> obtenerPreciosVigentes();
        /// <summary>
        /// crea un precio nuevo, si existe un precio con el mismo producto y tarifa lo caduca al precio anterior
        /// </summary>
        /// <param name="precio"></param>
        /// <returns></returns>
        OperationResult EstablecerPrecio(Precio precio);

        /// <summary>
        /// Establce un conjunto de precios. Si existe un precio con el mismo producto y tarifa, lo caduca al precio anterior
        /// </summary>
        /// <param name="precios"></param>
        /// <returns></returns>
        OperationResult establecerPrecios(List<Precio> precios);
        /// <summary>
        /// actualiza los datos de un precio
        /// </summary>
        /// <param name="precio"></param>
        /// <returns></returns>
        OperationResult ActualizarPrecio(Precio precio);
 
        /// <summary>
        /// retorna el precio que correspon al id
        /// </summary>
        /// <param name="idPrecio"></param>
        /// <returns></returns>
        Precio obtenerPrecio(int idPrecio);


        /// <summary>
        /// retorna los precios que correspndan al idconceptonegocio
        /// </summary>
        /// <param name="idTipo"></param>
        /// <param name="idConceptoNegocio"></param>
        /// <returns></returns>
        IEnumerable<Precio> ObtenerPreciosVigentesDeUnConceptoNegocio(int idTipo, int idConceptoNegocio);
        IEnumerable<Precio_Concepto> ObtenerPrecios(int idTipo);
        Precio ObtenerPrecioVigente(int idConceptoNegocio, int idTarifa);
        IEnumerable<Precio> ObtenerPreciosVigentes(int[] idsConceptosNegocio, int idTarifa);
        OperationResult ResolverPrecios(List<Precio> nuevosPrecios, List<Precio> preciosCaducos);
    }
}

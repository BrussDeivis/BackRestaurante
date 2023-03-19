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
   public interface ICodigosTransaccion_Repositorio
    {
        /// <summary>
        /// Devuelve el maximo numero para el codigo de una transaccion 
        /// </summary>
        /// <param name="sufijo"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        int ObtenerMaximoCodigoParaTransaccion(string prefijo, int idTipoTransaccion);
    }

}

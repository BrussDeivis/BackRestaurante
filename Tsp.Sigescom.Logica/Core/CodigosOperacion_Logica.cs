using System;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica.Core.Permisos
{
    public class CodigosOperacion_Logica: ICodigosOperacion_Logica
    {

        protected readonly ICodigosTransaccion_Repositorio _codigosTransaccionDatos;

        public CodigosOperacion_Logica(ICodigosTransaccion_Repositorio codigosTransaccionDatos)
        {
            _codigosTransaccionDatos = codigosTransaccionDatos;
        }

        public string ObtenerSiguienteCodigoParaFacturacion(string prefijo, int idTipoTransaccion)
        {
            try
            {
                int maximo = _codigosTransaccionDatos.ObtenerMaximoCodigoParaTransaccion(prefijo, idTipoTransaccion);
                string siguienteCodigoParaFacturacion = prefijo + (maximo + 1).ToString();

                return siguienteCodigoParaFacturacion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener codigo de transaccion", e);
            }
        }
        public int ObtenerMaximoCodigoParaTransaccion(string prefijo, int idTipoTransaccion)
        {
            return _codigosTransaccionDatos.ObtenerMaximoCodigoParaTransaccion(prefijo, idTipoTransaccion);
        }




    }
}

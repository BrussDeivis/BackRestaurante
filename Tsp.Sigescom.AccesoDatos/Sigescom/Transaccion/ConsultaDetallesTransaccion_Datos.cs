using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;

namespace Tsp.Sigescom.AccesoDatos.Transacciones

{

    public partial class ConsultaDetallesTransaccion_Datos : IConsultaDetalleTransaccion_Repositorio
    {

        public IEnumerable<Detalle_transaccion> ObtenerDetalleTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado, int[] idsConceptos)
        {
            try
            {
            SigescomEntities _db = new SigescomEntities();
                return _db.Detalle_transaccion
                    .Where(dt => dt.Transaccion.id_actor_negocio_interno == idActorNegocioInterno
                    && dt.Transaccion.id_tipo_transaccion == idTipoTransaccion
                    && dt.Transaccion.id_estado_actual == idUltimoEstado
                    && idsConceptos.Contains(dt.id_concepto_negocio));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transaccion inclusive detalles de transaccion", e);
            }
        }

    }
}
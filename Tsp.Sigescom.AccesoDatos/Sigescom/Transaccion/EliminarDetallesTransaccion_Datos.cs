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

    public partial class EliminarDetallesTransaccion_Datos : IEliminarDetalleTransaccion_Repositorio
    {

        public OperationResult EliminarDetallesDeTransaccionMasivo(List<Detalle_transaccion> detallesDeTransaccion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                if (detallesDeTransaccion.Count > 1)
                {
                    List<string> comandosSQL = new List<string>();

                    List<SqlParameter> parametrosSql = new List<SqlParameter>();

                    foreach (var detalle in detallesDeTransaccion)
                    {
                        var comandoActual = ("delete Detalle_transaccion where id = " + detalle.id + ";");
                        comandosSQL.Add(comandoActual);
                    }
                    string comandoAEjecutar = "";
                    foreach (var item in comandosSQL)
                    {
                        comandoAEjecutar += item;
                    }
                    var resultado = _db.Database.ExecuteSqlCommand(comandoAEjecutar, parametrosSql.ToArray());
                }
                var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                return resultadoDetails;
            }
            catch (Exception e)
            {
                throw new DatosException("Error en datos al crear documentos masivos", e);
            }
        }

    }
}
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

    public partial class CrearDetallesTransaccion_Datos : ICrearDetalleTransaccion_Repositorio
    {

        public OperationResult CrearDetallesDeTransaccionMasivo(List<Detalle_transaccion> detallesDeTransaccion)
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
                        var comandoActual = ("insert into Detalle_transaccion (id_transaccion,cantidad,id_concepto_negocio,detalle,precio_unitario,total,cantidad_secundaria,isc,igv,descuento) values (" + detalle.id_transaccion + "," + detalle.cantidad + "," + detalle.id_concepto_negocio + ",'" + detalle.detalle.Replace("'", "''") + "'," + detalle.precio_unitario + "," + detalle.total + "," + detalle.cantidad_secundaria + "," + detalle.isc + "," + detalle.igv + "," + detalle.descuento + ");");
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;

namespace Tsp.Sigescom.AccesoDatos.Transacciones

{

    public partial class CrearTransacciones_Datos: ICrearTransaccion_Repositorio
    {

        public OperationResult CrearTransacciones(List<Transaccion> transacciones)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                foreach (var item in transacciones)
                {
                    _db.Transaccion.Add(item);
                }
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }


        public OperationResult CrearTransaccionesYDetallesTransaccionMasivo(List<Transaccion> transacciones)
        {
            try

            {
                SigescomEntities _db = new SigescomEntities();

                string commandInsert = "insert into Detalle_transaccion values ";
                List<string> comandosSQL = new List<string>();
                List<SqlParameter> parametrosSql = new List<SqlParameter>();
                foreach (var transaccion in transacciones)
                {
                    long idTransaccion;
                    var detalles = transaccion.Detalle_transaccion;
                    transaccion.Detalle_transaccion = null;
                    _db.Transaccion.Add(transaccion);
                    _db.SaveChanges();
                    idTransaccion = transaccion.id;
                    string comandoActual = "";
                    foreach (var detalleTransaccion in detalles)
                    {
                        bool tieneLote = !String.IsNullOrEmpty(detalleTransaccion.lote);
                        comandoActual = (Environment.NewLine + "( " + idTransaccion + ", " + detalleTransaccion.cantidad + ", " + detalleTransaccion.id_concepto_negocio + ", " + "'" + (string.IsNullOrEmpty(detalleTransaccion.detalle) ? "" : detalleTransaccion.detalle.Replace("'", "''")) + " ' " + ", " + detalleTransaccion.precio_unitario + ", " + detalleTransaccion.total + ",  null , " + detalleTransaccion.cantidad_secundaria + " ,  null,  null, " + detalleTransaccion.isc + ", " + detalleTransaccion.igv + ", " + detalleTransaccion.descuento + ", " + (tieneLote ? ("'" + detalleTransaccion.lote + " ' ") : "null") + " , null, null, null),");
                        comandosSQL.Add(comandoActual);
                    }
                }
                string comandoAEjecutar = "";
                var rondas = Math.Ceiling((double)comandosSQL.Count / 1000);
                for (int a = 0; a < rondas; a++)
                {
                    var subListaDeComandosSQL = comandosSQL.Skip(a * 1000).Take(1000);
                    string comando = "";
                    foreach (var item in subListaDeComandosSQL)
                    {
                        comando += item;
                    }
                    comando = comando.Substring(0, comando.Length - 1);
                    comandoAEjecutar += commandInsert + comando;
                }
                var resultado = _db.Database.ExecuteSqlCommand(comandoAEjecutar, parametrosSql.ToArray());
                var result = new OperationResult(OperationResultEnum.Success);
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transacciones y detalles de transacciones", e);
            }
        }

        /// <summary>
        /// Crear una lista de detallle de transaccion para el inventario fisico
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public OperationResult CrearTransaccionYDetallesTransaccionMasivo(Transaccion transaccion, List<Detalle_transaccion> detallesTransaccion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                List<string> comandosSQL = new List<string>();
                List<SqlParameter> parametrosSql = new List<SqlParameter>();
                long idTransaccion;
                //"'" + detalleTransaccion.detalle + " ' "
                _db.Transaccion.Add(transaccion);
                _db.SaveChanges();
                var resultSave = new OperationResult(OperationResultEnum.Success);
                idTransaccion = transaccion.id;

                foreach (var detalleTransaccion in detallesTransaccion)
                {
                    var comandoActual = (Environment.NewLine + "insert into Detalle_transaccion values ( " + idTransaccion + ", " + detalleTransaccion.cantidad + ", " + detalleTransaccion.id_concepto_negocio + ", " + "'" + detalleTransaccion.detalle.Replace("'", "''") + " ' " + ", " + detalleTransaccion.precio_unitario + ", " + detalleTransaccion.total + ",  null , " + detalleTransaccion.cantidad_secundaria + " , null, null, " + detalleTransaccion.isc + ", " + detalleTransaccion.igv + ", " + detalleTransaccion.descuento + ", null, null, null, null, 0);");
                    comandosSQL.Add(comandoActual);
                }
                string comandoAEjecutar = "";
                foreach (var item in comandosSQL)
                {
                    comandoAEjecutar += item;
                }
                var resultado = _db.Database.ExecuteSqlCommand(comandoAEjecutar, parametrosSql.ToArray());

                var result = new OperationResult(OperationResultEnum.Success);
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transaccion y detalles de transaccion", e);

            }
        }
    }
}
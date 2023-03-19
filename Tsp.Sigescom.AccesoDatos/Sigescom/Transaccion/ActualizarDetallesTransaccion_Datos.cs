using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;

namespace Tsp.Sigescom.AccesoDatos.Transacciones

{

    public partial class ActualizarDetallesTransaccion_Datos : IActualizarDetalleTransaccion_Repositorio
    {

        public OperationResult ActualizarValorUnitarioyValorTotal(List<MovimientoAlmacen> detalles)
        {
            SigescomEntities _db = new SigescomEntities();
                try
                {
                detalles.ForEach(d => { d.ImporteUnitario = Math.Round(d.ImporteUnitario, 10); d.ImporteTotal = Math.Round(d.ImporteTotal, 10); });
                    string updateCommand = "update detalle_transaccion set ";

                    List<string> commands = new List<string>();
                    foreach (var detalle in detalles)
                    {
                        
                        commands.Add(Environment.NewLine + updateCommand + "precio_unitario = " + detalle.ImporteUnitario +", total= " + detalle.ImporteTotal + " where id = "+ detalle.IdDetalleTransaccion);
                                //update detalle_transaccion set precio_unitario =1, total=20 where id= 14;
                    }
                    var rondas = Math.Ceiling((double)commands.Count / 1000);
                    for (int a = 0; a < rondas; a++)
                    {
                        var subList = commands.Skip(a * 1000).Take(1000);
                        string com = "";
                        foreach (var item in subList)
                        {
                            com += item;
                        }
                        _db.Database.ExecuteSqlCommand(com);
                    }
                    var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                    return resultadoDetails;
                }
                catch (Exception e)
                {
                    throw new DatosException("Error al actualizar detalles de transaccion", e);
                }
           
        }

    }
}
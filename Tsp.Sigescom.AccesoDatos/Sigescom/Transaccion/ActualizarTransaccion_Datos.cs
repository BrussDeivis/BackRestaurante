using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;

namespace Tsp.Sigescom.AccesoDatos.Transacciones

{

    public partial class ActualizarTransaccion_Datos : IActualizarTransaccion_Repositorio
    {

        public OperationResult ActualizarTransacciones(List<Transaccion> transacciones)
        {
            SigescomEntities _db = new SigescomEntities();
                try
                {
                    foreach (var transaccion in transacciones)
                    {
                        Transaccion dbTransaccion1 = _db.Transaccion.Single(m => m.id == transaccion.id);
                        _db.Entry(dbTransaccion1).CurrentValues.SetValues(transaccion);
                    }
                _db.SaveChanges();
                var resultado = new OperationResult(OperationResultEnum.Success);
                    return resultado;
                }
                catch (Exception e)
                {
                    throw new DatosException("Error al actualizar transacciones", e);
                }
           
        }

    }
}
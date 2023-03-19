using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos.Roles

{
    public partial class CodigosTransaccion_Datos : ICodigosTransaccion_Repositorio
    {


        public int ObtenerMaximoCodigoParaTransaccion(string prefijo, int idTipoTransaccion)
        {
            SigescomEntities _db = new SigescomEntities();

            int longitud = prefijo.Length;
            try
            {
                var ultimoCodigo = _db.Transaccion.
                    Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.codigo.StartsWith(prefijo)).OrderByDescending(c => c.id).FirstOrDefault();
                return ultimoCodigo != null ? Convert.ToInt32(ultimoCodigo.codigo.Remove(0, longitud)) : 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
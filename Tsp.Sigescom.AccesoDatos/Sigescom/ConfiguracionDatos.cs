using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using System.Data.Entity;
namespace Tsp.Sigescom.AccesoDatos
{
    public class ConfiguracionDatos : RepositorioBase, IConfiguracionRepositorio
    {
        public OperationResult crearConfiguracion(Configuracion configuracion)
        {
            try
            {
                _db.Configuracion.Add(configuracion);
                var result = Save();
                result.data = configuracion.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult crearParametroConfiguracion(Parametro_de_configuracion parametro)
        {
            try
            {
                _db.Parametro_de_configuracion.Add(parametro);
                var result = Save();
                result.data = parametro.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult actualizarConfiguracion(Configuracion configuracion)
        {
            try
            {
                Configuracion dbConfiguracion = _db.Configuracion.Single(m => m.id == configuracion.id);
                _db.Entry(dbConfiguracion).CurrentValues.SetValues(configuracion);
                var result = Save();
                result.data = configuracion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult actualizarParametroConfiguracion(Parametro_de_configuracion parametro)
        {
            try
            {
                Parametro_de_configuracion dbDetalle = _db.Parametro_de_configuracion.Single(m => m.id == parametro.id);
                _db.Entry(dbDetalle).CurrentValues.SetValues(parametro);
                var result = Save();
                result.data = parametro.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public IEnumerable<Configuracion> obtenerConfiguraciones()
        {
            try
            {
                return _db.Configuracion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Parametro_de_configuracion> obtenerParametrosConfiguracion(int idConfiguracion)
        {
            try
            {
                return _db.Parametro_de_configuracion.Where(dm => dm.id_configuracion == idConfiguracion);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Parametro_de_configuracion ObtenerParametroDeConfiguracion(string nombre)
        {
            try
            {
                return _db.Parametro_de_configuracion.SingleOrDefault(pc => pc.nombre == nombre);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        #region Obtener, Actualizar Tipo De Comprobante
        public Detalle_maestro ObtenerDetalleMaestroIncluidoTipoTransaccionTipoComprobante(int idMaestro, int idTipoDeComprobante)
        {
            try
            {
                return _db.Detalle_maestro.Include(dm => dm.Tipo_transaccion_tipo_comprobante)
                                          .Include(dm => dm.Tipo_transaccion_tipo_comprobante.Select(tttc => tttc.Tipo_transaccion))
                                          .Where(dm => dm.id_maestro == idMaestro).SingleOrDefault(dm => dm.id == idTipoDeComprobante);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public IEnumerable<Detalle_maestro> ObtenerDetallesMaestroIncluidoTipoTransaccionTipoComprobante(int idMaestro)
        {
            try
            {
                return _db.Detalle_maestro.Include(dm => dm.Tipo_transaccion_tipo_comprobante)
                                           .Include(dm => dm.Tipo_transaccion_tipo_comprobante.Select(tttc => tttc.Tipo_transaccion))
                                           .Where(dm => dm.id_maestro == idMaestro).OrderBy(dm => dm.nombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarDetalleMaestroConTipoTransaccionTipoComprobante(Detalle_maestro detalle)
        {
            try
            {
                Detalle_maestro dbDetalle = _db.Detalle_maestro.Single(m => m.id == detalle.id);
                detalle.fecha_registro = dbDetalle.fecha_registro;
                _db.Entry(dbDetalle).CurrentValues.SetValues(detalle);

                List<Tipo_transaccion_tipo_comprobante> updTipo_transaccion_tipo_comprobantes = detalle.Tipo_transaccion_tipo_comprobante.ToList();
                List<Tipo_transaccion_tipo_comprobante> dbTipo_transaccion_tipo_comprobantes = dbDetalle.Tipo_transaccion_tipo_comprobante.ToList();

                foreach (var dbtttc in dbTipo_transaccion_tipo_comprobantes)
                {
                    if (updTipo_transaccion_tipo_comprobantes.Any(d => d.id == dbtttc.id))
                    {
                        var tttc = updTipo_transaccion_tipo_comprobantes.Single(d => d.id == dbtttc.id);
                        _db.Entry(dbtttc).CurrentValues.SetValues(tttc);
                    }
                    else
                    {
                        _db.Tipo_transaccion_tipo_comprobante.Remove(dbtttc);
                    }
                }
                foreach (var updtttc in updTipo_transaccion_tipo_comprobantes)
                {
                    if (!dbTipo_transaccion_tipo_comprobantes.Any(d => d.id == updtttc.id))
                    {
                        _db.Tipo_transaccion_tipo_comprobante.Add(updtttc);
                    }
                }

                var result = Save();
                result.data = detalle.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        #endregion

        #region (crear rol)
        public OperationResult CrearRol(Rol rol)
        {
            try
            {
                _db.Rol.Add(rol);
                var result = Save();
                result.data = rol.id;

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Rol> ObtenerRoles()

        {

            try
            {
                //throw new NotImplementedException();
                return _db.Rol;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion
    }
}

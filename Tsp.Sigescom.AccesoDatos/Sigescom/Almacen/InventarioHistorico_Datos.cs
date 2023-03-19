using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using System.Data.Entity;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System.Data.SqlClient;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.AccesoDatos.Almacen
{
    public partial class InventarioHistorico_Datos : IInventarioHistorico_Repositorio
    {




        public Transaccion ObtenerTransaccionInclusiveDetalleTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado)
        {
            SigescomEntities _db = new SigescomEntities();

            try
            {

                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Transaccion.Include(t => t.Detalle_transaccion).Include(t => t.Detalle_transaccion.Select(dt => dt.Valor_detalle_maestro_detalle_transaccion))
                    .SingleOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInterno
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transaccion inclusive detalles de transaccion", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }


        public InventarioFisico ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, int idConcepto, DateTime fecha)
        {
            try
            {
                InventarioFisico inventario = null;

                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha))
                {
                    inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha).OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Where(d => d.id_concepto_negocio == idConcepto).Select(dt => new InventarioFisico()
                    {
                        IdTransaccion = dt.id_transaccion,
                        Cantidad = dt.cantidad,
                        CantidadSecundaria = dt.cantidad_secundaria,
                        IdAlmacen = idAlmacen,
                        IdConcepto = idConcepto,
                        Fecha = dt.Transaccion.fecha_inicio
                    }).SingleOrDefault();
                }
                return inventario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public InventarioValorizado ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, int idConcepto, DateTime fecha)
        {
            InventarioValorizado inventario = null;

            SigescomEntities _db = new SigescomEntities();
            if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha))
            {
                inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha).OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Where(d => d.id_concepto_negocio == idConcepto).Select(dt => new InventarioValorizado()
                {
                    IdTransaccion = dt.id_transaccion,
                    Cantidad = dt.cantidad,
                    CantidadSecundaria = dt.cantidad_secundaria,
                    IdAlmacen = idAlmacen,
                    IdConcepto = idConcepto,
                    Fecha = dt.Transaccion.fecha_inicio,
                    ValorUnitario = dt.precio_unitario,
                    ValorTotal = dt.total
                }).SingleOrDefault();
            }
            return inventario;
        }



        public IEnumerable<InventarioFisico> ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA)
        {
            try
            {
                IEnumerable<InventarioFisico> inventario = new List<InventarioFisico>();
                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA))
                {
                    inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA)
                          .OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Select(dt => new InventarioFisico()
                          {
                              IdTransaccion = dt.id_transaccion,
                              Cantidad = dt.cantidad,
                              CantidadSecundaria = dt.cantidad_secundaria,
                              IdAlmacen = idAlmacen,
                              IdConcepto = dt.id_concepto_negocio,
                              Lote = dt.lote,
                              Fecha = dt.Transaccion.fecha_inicio
                          });
                }
                return inventario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<InventarioHistorico> ObtenerInventariosHistoricosDespuesDe(int idAlmacen, DateTime despuesDe)
        {
            try
            {
                IEnumerable<InventarioHistorico> inventario = new List<InventarioHistorico>();
                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio > despuesDe))
                {
                    inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio > despuesDe).SelectMany(t => t.Detalle_transaccion).Select(dt => new InventarioHistorico()
                    {
                        IdDetalleTransaccion = dt.id,
                        Cantidad = dt.cantidad,
                        IdConcepto = dt.id_concepto_negocio,
                        Descripcion = dt.detalle,
                        ValorUnitario = dt.precio_unitario,
                        ValorTotal = dt.total,
                        CantidadSecundaria = dt.cantidad_secundaria,
                        Lote = dt.lote,
                    });
                }
                return inventario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<InventarioValorizado> ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA)
        {
            try
            {
                IEnumerable<InventarioValorizado> inventario = new List<InventarioValorizado>();
                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA))
                {
                    inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA)
                          .OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Select(dt => new InventarioValorizado()
                          {
                              IdTransaccion = dt.id_transaccion,
                              Cantidad = dt.cantidad,
                              CantidadSecundaria = dt.cantidad_secundaria,
                              IdAlmacen = idAlmacen,
                              IdConcepto = dt.id_concepto_negocio,
                              Lote = dt.lote,
                              Fecha = dt.Transaccion.fecha_inicio,
                              ValorUnitario = dt.precio_unitario,
                              ValorTotal = dt.total
                          });
                }
                return inventario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<InventarioFisico> ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA, int[] familias)
        {
            try
            {
                IEnumerable<InventarioFisico> inventario = new List<InventarioFisico>();

                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA))
                {
                    inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA)
                          .OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioFisico()
                          {
                              IdTransaccion = dt.id_transaccion,
                              Cantidad = dt.cantidad,
                              CantidadSecundaria = dt.cantidad_secundaria,
                              IdAlmacen = idAlmacen,
                              IdConcepto = dt.id_concepto_negocio,
                              Lote = dt.lote,
                              Fecha = dt.Transaccion.fecha_inicio
                          });
                }
                return inventario;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<InventarioValorizado> ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA, int[] familias)
        {
            try
            {
                IEnumerable<InventarioValorizado> inventario = new List<InventarioValorizado>();

                SigescomEntities _db = new SigescomEntities();
                if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA))
                {
                    return _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < anteriorA)
                          .OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.Where(dt => familias.Contains(dt.Concepto_negocio.id_concepto_basico)).Select(dt => new InventarioValorizado()
                          {
                              IdTransaccion = dt.id_transaccion,
                              Cantidad = dt.cantidad,
                              CantidadSecundaria = dt.cantidad_secundaria,
                              IdAlmacen = idAlmacen,
                              IdConcepto = dt.id_concepto_negocio,
                              Lote = dt.lote,
                              Fecha = dt.Transaccion.fecha_inicio,
                              ValorUnitario = dt.precio_unitario,
                              ValorTotal = dt.total
                          });
                }
                return inventario;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Transaccion ObtenerUltimoInventarioHistorico(int idAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion
                    .Include(t => t.Detalle_transaccion)
                          .Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico)
                          .OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public InventarioConceptoNegocio ObtenerUltimoInventarioHistoricoAntesDe(int idAlmacen, int idConcepto, string lote, DateTime fecha)
        {
            InventarioConceptoNegocio inventario = null;
            SigescomEntities _db = new SigescomEntities();
            if (_db.Transaccion.Any(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha))
            {
                inventario = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico && t.fecha_inicio < fecha).OrderByDescending(t => t.fecha_inicio).FirstOrDefault().Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto && dt.lote == lote).Select(inv => new InventarioConceptoNegocio()
                {
                    IdConceptoNegocio = idConcepto,
                    Lote = lote,
                    Fecha = fecha,
                    CostoUnitario = inv.precio_unitario,
                    CostoTotal = inv.total,
                    CantidadPrincipal = inv.cantidad,
                    CantidadSecundaria = inv.cantidad_secundaria
                }).SingleOrDefault();

            }
            return inventario;

        }





    }
}
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
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Comprobante;

namespace Tsp.Sigescom.AccesoDatos.Almacen
{
    public partial class OrdenAlmacen_Datos : IOrdenAlmacen_Repositorio
    {
        public IEnumerable<OrdenAlmacenResumen> ObtenerOrdenesAlmacen(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, int[] idsModoEntrega, int[] idsEstados, int[] idsAlmacenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Tipo_transaccion.Where(tt => tt.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && antt.valor == porIngresar).Select(at => at.id_tipo_transaccion).Contains(tt.id) && !tt.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && antt.valor).Select(at => at.id_tipo_transaccion).Intersect(tt.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && !antt.valor).Select(at => at.id_tipo_transaccion)).Contains(tt.id)).SelectMany(tt => tt.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsAlmacenes.Contains((int)t.id_actor_negocio_interno1) && idsModoEntrega.Contains(t.enum1) && idsEstados.Contains((int)t.id_estado_actual_1))
                    .Select(t => new OrdenAlmacenResumen()
                    {
                        Id = t.id,
                        Fecha = t.fecha_inicio,
                        EstablecimientoActorInterno = t.Actor_negocio4.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorInterno = t.Actor_negocio4.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumento = t.Comprobante.Detalle_maestro.valor,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre,
                        Ordenante = t.Actor_negocio.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|"," "),
                        NombreActorExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorExterno = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        DocumentoActorExterno = t.Actor_negocio1.Actor.numero_documento_identidad,
                        AliasOrigenDestino = t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente),
                        IdModoEntrega = t.enum1,
                        IdEstado = (int)t.id_estado_actual_1,
                        Estado = t.Detalle_maestro2.nombre,
                    }).OrderByDescending(t => t.Fecha);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener las ordenes de almacen", e);
            }
        }
        public IEnumerable<OrdenAlmacenResumen> ObtenerOrdenesAlmacenBidireccional(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, int[] idsModoEntrega, int[] idsEstados, int[] idsAlmacenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Tipo_transaccion.Where(tt => tt.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && antt.valor).Select(at => at.id_tipo_transaccion).Intersect(tt.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && !antt.valor).Select(at => at.id_tipo_transaccion)).Contains(tt.id)).SelectMany(tt => tt.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && (porIngresar ? idsAlmacenes.Contains(t.id_actor_negocio_externo) : idsAlmacenes.Contains(t.id_actor_negocio_interno)) && idsModoEntrega.Contains(t.enum1) && idsEstados.Contains((int)t.id_estado_actual_1))
                    .Select(t => new OrdenAlmacenResumen()
                    {
                        Id = t.id,
                        Fecha = t.fecha_inicio,
                        EstablecimientoActorInterno = t.Actor_negocio2.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorInterno = t.Actor_negocio2.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorInterno = t.Actor_negocio2.Actor.Detalle_maestro.valor,
                        DocumentoActorInterno = t.Actor_negocio2.Actor.numero_documento_identidad,
                        TipoDocumento = t.Comprobante.Detalle_maestro.valor,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre,
                        Ordenante = t.Actor_negocio.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                        EstablecimientoActorExterno = t.Actor_negocio1.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorExterno = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        DocumentoActorExterno = t.Actor_negocio1.Actor.numero_documento_identidad,
                        IdModoEntrega = t.enum1,
                        IdEstado = (int)t.id_estado_actual_1,
                        Estado = t.Detalle_maestro2.nombre,
                        EsBidireccional = true, 
                        PorIngresar = porIngresar,
                    }).OrderByDescending(t => t.Fecha);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener las ordenes de almacen", e);
            }
        }
        public bool VerificarOrdenAlmacenBidireccional(long idOrdenAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var accionesNegocioTipoTransaccion = _db.Transaccion.FirstOrDefault(t => t.id == idOrdenAlmacen).Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen).ToList();
                return accionesNegocioTipoTransaccion.SingleOrDefault(a => a.valor) != null && accionesNegocioTipoTransaccion.SingleOrDefault(a => !a.valor) != null;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de orden de almacen", e);
            }
        }
        public OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionCompromisoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => t.id == idOrdenAlmacen)
                    .Select(t => new OrdenAlmacen()
                    {
                        Id = t.id,
                        Fecha = t.fecha_inicio,
                        IdActorInterno = (int)t.id_actor_negocio_interno1,
                        EstablecimientoActorInterno = t.Actor_negocio4.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorInterno = t.Actor_negocio4.Actor.primer_nombre,
                        TipoDocumento = t.Comprobante.Detalle_maestro.valor,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdTipoOperacion = t.id_tipo_transaccion,
                        TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre.ToUpper(),
                        Ordenante = t.Actor_negocio.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                        IdActorExterno = t.id_actor_negocio_externo,
                        NombreActorExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorExterno = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        DocumentoActorExterno = t.Actor_negocio1.Actor.numero_documento_identidad,
                        AliasOrigenDestino = t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente),
                        EsBidireccional = false,
                        IdModoEntrega = t.enum1,
                        IdEstado = (int)t.id_estado_actual_1,
                        Estado = t.Detalle_maestro2.nombre,
                        IdsOrdenes = t.Transaccion11.Where(tt => tt.enum1 == (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial && idsTipoTransaccionCompromisoAlmacen.Contains(tt.id_tipo_transaccion)).Select(tt => tt.id).ToList(),
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Ordenado = dt.cantidad,
                            Revocado = dt.cantidad_1
                        }).ToList()
                    }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener la orden de almacen", e);
            }
        }
        public OrdenAlmacen ObtenerOrdenAlmacenBireccional(long idOrdenAlmacen, bool porIngresar)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionCompromisoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => t.id == idOrdenAlmacen)
                    .Select(t => new OrdenAlmacen()
                    {
                        Id = t.id,
                        Fecha = t.fecha_inicio,
                        IdActorInterno = (int)t.id_actor_negocio_interno,
                        EstablecimientoActorInterno = t.Actor_negocio2.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorInterno = t.Actor_negocio2.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorInterno = t.Actor_negocio2.Actor.Detalle_maestro.valor,
                        DocumentoActorInterno = t.Actor_negocio2.Actor.numero_documento_identidad,
                        TipoDocumento = t.Comprobante.Detalle_maestro.valor,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdTipoOperacion = t.id_tipo_transaccion,
                        TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre.ToUpper(),
                        Ordenante = t.Actor_negocio.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                        IdActorExterno = t.id_actor_negocio_externo,
                        EstablecimientoActorExterno = t.Actor_negocio1.Actor_negocio2.Actor.tercer_nombre,
                        NombreActorExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoActorExterno = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        DocumentoActorExterno = t.Actor_negocio1.Actor.numero_documento_identidad,
                        AliasOrigenDestino = t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente),
                        EsBidireccional = true,
                        PorIngresar = porIngresar,
                        IdModoEntrega = t.enum1,
                        IdEstado = (int)t.id_estado_actual_1,
                        Estado = t.Detalle_maestro2.nombre,
                        IdsOrdenes = t.Transaccion11.Where(tt => tt.enum1 == (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial && idsTipoTransaccionCompromisoAlmacen.Contains(tt.id_tipo_transaccion)).Select(tt => tt.id).ToList(),
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Ordenado = dt.cantidad,
                            Revocado = dt.cantidad_1
                        }).ToList()
                    }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener la orden de almacen", e);
            }
        }
        public IEnumerable<OrdenDeOrdenAlmacen> ObtenerOrdenesDeOrdenAlmacen(long[] IdsOrdenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion.Where(t => IdsOrdenes.Contains(t.id))
                    .Select(t => new OrdenDeOrdenAlmacen()
                    {
                        Id = t.id,
                        Fecha = t.fecha_inicio,
                        IdTipoTransaccion = t.id_tipo_transaccion,
                        TipoDocumento = t.Comprobante.Detalle_maestro.nombre,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre.ToUpper()
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de orden de almacen", e);
            }
        }

        public IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] IdsOrdenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionMovimientoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => IdsOrdenes.Contains((long)t.id_transaccion_referencia) && idsTipoTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion))
                    .Select(t => new MovimientoDeOrdenAlmacen()
                    {
                        Id = t.id,
                        IdOrden = (long)t.id_transaccion_referencia,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        Fecha = t.fecha_inicio,
                        Destinatario = t.Actor_negocio1.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio1.Actor.numero_documento_identidad + " - " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdEstado = (int)t.id_estado_actual,
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Cantidad = dt.cantidad
                        }).ToList(),
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }

        public IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] IdsOrdenes, bool porIngresar)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionMovimientoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == porIngresar).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => IdsOrdenes.Contains((long)t.id_transaccion_referencia) && idsTipoTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion))
                    .Select(t => new MovimientoDeOrdenAlmacen()
                    {
                        Id = t.id,
                        IdOrden = (long)t.id_transaccion_referencia,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        Fecha = t.fecha_inicio,
                        Destinatario = t.Actor_negocio1.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio1.Actor.numero_documento_identidad + " - " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdEstado = (int)t.id_estado_actual,
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Cantidad = dt.cantidad
                        }).ToList(),
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }

        public IEnumerable<MovimientoDeOrdenAlmacen> ObtenerNotasCreditoDeOrdenAlmacen(long idOrdenAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Transaccion.Where(t => t.id_transaccion_referencia == idOrdenAlmacen && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta)
                    .Select(t => new MovimientoDeOrdenAlmacen()
                    {
                        Id = t.id,
                        IdOrden = (long)t.id_transaccion_referencia,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        Fecha = t.fecha_inicio,
                        Destinatario = t.Actor_negocio1.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio1.Actor.numero_documento_identidad + " - " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdEstado = (int)t.id_estado_actual,
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Cantidad = dt.cantidad
                        }).ToList(),
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }

        public IEnumerable<MovimientoDeOrdenAlmacen> ObtenerMovimientosConfirmadosDeOrdenAlmacen(long[] IdsOrdenes)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionMovimientoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => IdsOrdenes.Contains((long)t.id_transaccion_referencia) && idsTipoTransaccionMovimientoAlmacen.Contains(t.id_tipo_transaccion) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .Select(t => new MovimientoDeOrdenAlmacen()
                    {
                        Id = t.id,
                        IdOrden = (long)t.id_transaccion_referencia,
                        IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                        Fecha = t.fecha_inicio,
                        Destinatario = t.Actor_negocio1.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio1.Actor.numero_documento_identidad + " - " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        IdEstado = (int)t.id_estado_actual,
                        Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Concepto = dt.Concepto_negocio.nombre,
                            Cantidad = dt.cantidad
                        }).ToList(),
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }

        public OrdenAlmacen ObtenerOrdenAlmacenConIdMovimientoOrdenAlmacen(long IdMovimientoOrdenAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var idsTipoTransaccionCompromisoAlmacen = _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen).Select(antt => antt.id_tipo_transaccion).ToList();
                return _db.Transaccion.Where(t => t.id == IdMovimientoOrdenAlmacen).Select(t => t.Transaccion3)
                   .Select(t => new OrdenAlmacen()
                   {
                       Id = t.id,
                       Fecha = t.fecha_inicio,
                       EstablecimientoActorInterno = t.Actor_negocio4.Actor_negocio2.Actor.tercer_nombre,
                       IdActorInterno = (int)t.id_actor_negocio_interno1,
                       NombreActorInterno = t.Actor_negocio4.Actor.primer_nombre,
                       TipoDocumento = t.Comprobante.Detalle_maestro.valor,
                       SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       IdTipoOperacion = t.id_tipo_transaccion,
                       TipoOperacion = t.Tipo_transaccion.Tipo_transaccion2.nombre.ToUpper(),
                       Ordenante = t.Actor_negocio.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                       IdActorExterno = t.id_actor_negocio_externo,
                       DocumentoActorExterno = t.Actor_negocio1.Actor.Detalle_maestro.valor + ": " + t.Actor_negocio1.Actor.numero_documento_identidad,
                       NombreActorExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       AliasOrigenDestino = t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente),
                       IdModoEntrega = t.enum1,
                       IdEstado = (int)t.id_estado_actual_1,
                       Estado = t.Detalle_maestro.nombre,
                       IdsOrdenes = t.Transaccion11.Where(tt => tt.enum1 == (int)IndicadorImpactoAlmacen.NoImpactaPorQueRevocaAOperacionInicial && idsTipoTransaccionCompromisoAlmacen.Contains(tt.id_tipo_transaccion)).Select(tt => tt.id).ToList(),
                       Detalles = t.Detalle_transaccion.Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor == "1").Select(dt => new DetalleDeOrdenAlmacen()
                       {
                           IdConcepto = dt.id_concepto_negocio,
                           Concepto = dt.Concepto_negocio.nombre,
                           Ordenado = dt.cantidad,
                           Revocado = dt.cantidad_1
                       }).ToList()
                   }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }
        public IEnumerable<DetalleDeOrdenAlmacen> ObtenerStockActualDetallesOrdenAlmacen(int[] idsConceptos,int idAlmacen)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities(); 
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual && t.id_actor_negocio_interno == idAlmacen).SelectMany(t => t.Detalle_transaccion).Where(dt => idsConceptos.Contains(dt.id_concepto_negocio))
                    .Select(dt => new DetalleDeOrdenAlmacen()
                    {
                        IdConcepto = dt.id_concepto_negocio,
                        StockActual = dt.cantidad
                    });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }
        
    }
}
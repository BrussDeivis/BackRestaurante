using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class FinanzaReporte_Datos : IFinanzaReporte_Repositorio
    {
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresos(int idCaja, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja && antt.valor == esIngreso)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorOperaciones(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && operaciones.Contains(t.id_tipo_transaccion))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso, int[] mediosPago)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja && antt.valor == esIngreso)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total,
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosPorOperacionesYMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones, int[] mediosPago)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && operaciones.Contains(t.id_tipo_transaccion) && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total,
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancaria(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja && antt.valor == esIngreso)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && operaciones.Contains(t.id_tipo_transaccion))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, bool esIngreso, int[] mediosPago)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja && antt.valor == esIngreso)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion)
                                .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total,
                                }
                                );
            return ingresosEgresos;
        }
        public IEnumerable<IngresoEgreso> ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] operaciones, int[] mediosPago)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresosEgresos = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && operaciones.Contains(t.id_tipo_transaccion) && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                                .Select(t => new IngresoEgreso()
                                {
                                    Fecha = t.fecha_inicio,
                                    IdOperacion = t.id_tipo_transaccion,
                                    Operacion = t.Tipo_transaccion.nombre_corto,
                                    IdMedioPago = t.Traza_pago.FirstOrDefault().id_medio_pago,
                                    MedioPago = t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre,
                                    Informacion = t.Traza_pago.FirstOrDefault().traza,
                                    TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                                    SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                                    Importe = t.importe_total,
                                }
                                );
            return ingresosEgresos;
        }

        public IEnumerable<DetalleFlujo> ObtenerIngresosEgresos(int idCaja, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                   .Select(t => new DetalleFlujo()
                   {
                       Fecha = t.fecha_inicio,
                       IdOperacion = t.id_tipo_transaccion,
                       Operacion = t.Tipo_transaccion.nombre,
                       TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Importe = t.importe_total,
                       EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja).valor
                   });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        public IEnumerable<DetalleFlujo> ObtenerIngresosEgresosPorMediosPago(int idCaja, DateTime fechaDesde, DateTime fechaHasta, int[] mediosPago)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCaja && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                   .Select(t => new DetalleFlujo()
                   {
                       Fecha = t.fecha_inicio,
                       IdOperacion = t.id_tipo_transaccion,
                       Operacion = t.Tipo_transaccion.nombre,
                       TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Importe = t.importe_total,
                       EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja).valor
                   });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        public IEnumerable<DetalleFlujo> ObtenerIngresosEgresosEnCuentaBancaria(int idCuenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                   .Select(t => new DetalleFlujo()
                   {
                       Fecha = t.fecha_inicio,
                       IdOperacion = t.id_tipo_transaccion,
                       Operacion = t.Tipo_transaccion.nombre,
                       TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Importe = t.importe_total,
                       EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja).valor
                   });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        public IEnumerable<DetalleFlujo> ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(int idCuenta, DateTime fechaDesde, DateTime fechaHasta, int[] mediosPago)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno1 == idCuenta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && mediosPago.Contains(t.Traza_pago.FirstOrDefault().id_medio_pago))
                   .Select(t => new DetalleFlujo()
                   {
                       Fecha = t.fecha_inicio,
                       IdOperacion = t.id_tipo_transaccion,
                       Operacion = t.Tipo_transaccion.nombre,
                       TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Importe = t.importe_total,
                       EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja).valor
                   });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        public IEnumerable<OperacionGrupo> ObtenerCuentasPorCobarGrupos(int[] idsGrupos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Cuota.Where(c => c.por_cobrar
                                                  && c.saldo > 0
                                                  && Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(c.Transaccion.id_tipo_transaccion)
                                                  && idsGrupos.Contains((int)c.Transaccion.id_actor_negocio_externo1))
                   .Select(c => new OperacionGrupo()
                   {
                       IdGrupo = (int)c.Transaccion.id_actor_negocio_externo1,
                       IdTipoTransaccion = c.Transaccion.id_tipo_transaccion,
                       NombreGrupo = c.Transaccion.Actor_negocio3.Actor.primer_nombre,
                       DocumentoResponsable = c.Transaccion.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)c.Transaccion.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.numero_documento_identidad,
                       NombreResponsable = c.Transaccion.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)c.Transaccion.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                       IdCliente = c.Transaccion.id_actor_negocio_externo,
                       NombreCliente = c.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       IdComprobante = c.Transaccion.id_comprobante,
                       InfoComprobante = "[(" + c.Transaccion.Comprobante.Detalle_maestro.valor + ") " + c.Transaccion.Comprobante.numero_serie + "-" + c.Transaccion.Comprobante.numero + "]",
                       Importe = c.total,
                       Revocado = c.revocado,
                       ACuenta = c.pago_a_cuenta,
                       Saldo = c.saldo,
                   });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de cuentas por cobrar en grupos", e);
            }
        }

        public IEnumerable<OperacionGrupoDetallado> ObtenerCuentasPorCobarGrupoDetallado(int idGrupo)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Cuota.Where(c => c.por_cobrar
                                                  && c.saldo > 0
                                                  && Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(c.Transaccion.id_tipo_transaccion)
                                                  && (int)c.Transaccion.id_actor_negocio_externo1 == idGrupo)
                   .Select(c => new OperacionGrupoDetallado()
                   {
                       Id = c.Transaccion.id,
                       IdTipoTransaccion = c.Transaccion.id_tipo_transaccion,
                       NombreResponsable = c.Transaccion.Actor_negocio3.Vinculo_Actor_Negocio1.FirstOrDefault(van => van.id_actor_negocio_vinculado == (int)c.Transaccion.id_actor_negocio_externo1 && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo).Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                       DocumentoCliente = c.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                       NombreCliente = c.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       Emision = c.Transaccion.fecha_inicio,
                       TipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.valor,
                       IdComprobante = c.Transaccion.id_comprobante,
                       SerieYNumeroComprobante = c.Transaccion.Comprobante.numero_serie + "-" + c.Transaccion.Comprobante.numero,
                       Importe = c.total,
                       Revocado = c.revocado,
                       ACuenta = c.pago_a_cuenta,
                       Saldo = c.saldo,
                   });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de cuentas por cobrar en grupo detallado", e);
            }
        }




    }
}
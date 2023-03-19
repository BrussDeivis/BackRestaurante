using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Hotel;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public class HotelReporte_Datos : IHotelReporte_Repositorio
    {
        public IEnumerable<RegistroHuesped> ObtenerRegistroHuespedes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var registroHuespedes = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && (et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).SelectMany(et => et.Transaccion.Actor_negocio_por_transaccion)
                .Select(ant => new RegistroHuesped()
                {
                    IdAtencion = ant.Transaccion.id,
                    Huesped = ant.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                    EsMasculino = ant.Actor_negocio.Actor.id_clase_actor == ActorSettings.Default.IdClaseActorMasculino,
                    IdPais = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().id_nacion,
                    Nacionalidad = ant.Actor_negocio.Actor.Detalle_maestro1.valor,
                    Pais = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre,
                    Ubigeo = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
                    TipoDocumentoCliente = ant.Actor_negocio.Actor.Detalle_maestro.valor,
                    NumeroDocumentoCliente = ant.Actor_negocio.Actor.numero_documento_identidad,
                    EstadoIngresoReferencia = ant.Transaccion.Transaccion3.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                    EstadoIngreso = ant.Transaccion.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                    EstadoSalida = ant.Transaccion.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut),
                    FechaFinAtencion = ant.Transaccion.fecha_fin,
                    IdTipoHabitacion = ant.Transaccion.Actor_negocio2.Concepto_negocio.id,
                    TipoHabitacion = ant.Transaccion.Actor_negocio2.Concepto_negocio.sufijo,
                    CodigoHabitacion = ant.Transaccion.Actor_negocio2.Actor.numero_documento_identidad,
                    ImporteTotal = ant.Transaccion.importe_total,
                    Noches = (int)ant.Transaccion.cantidad1,
                    IdMotivoViaje = (int)ant.id_detalle_maestro
                });
            return registroHuespedes;
        }

        public IEnumerable<RegistroHuesped> ObtenerRegistroHuespedesCompleto(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var registroHuespedes = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && (et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).SelectMany(et => et.Transaccion.Actor_negocio_por_transaccion)
                .Select(ant => new RegistroHuesped()
                {
                    IdAtencion = ant.Transaccion.id,
                    Huesped = ant.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                    EsMasculino = ant.Actor_negocio.Actor.id_clase_actor == ActorSettings.Default.IdClaseActorMasculino,
                    Nacionalidad = ant.Actor_negocio.Actor.Detalle_maestro1.valor,
                    IdPais = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().id_nacion,
                    IdUbigeo = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().id_ubigeo,
                    //Pais = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre,
                    //Ubigeo = ant.Actor_negocio.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
                    TipoDocumentoCliente = ant.Actor_negocio.Actor.Detalle_maestro.valor,
                    NumeroDocumentoCliente = ant.Actor_negocio.Actor.numero_documento_identidad,
                    EstadoIngresoReferencia = ant.Transaccion.Transaccion3.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                    EstadoIngreso = ant.Transaccion.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                    EstadoSalida = ant.Transaccion.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut),
                    FechaFinAtencion = ant.Transaccion.fecha_fin,
                    IdTipoHabitacion = ant.Transaccion.Actor_negocio2.Concepto_negocio.id,
                    TipoHabitacion = ant.Transaccion.Actor_negocio2.Concepto_negocio.sufijo,
                    CodigoHabitacion = ant.Transaccion.Actor_negocio2.Actor.numero_documento_identidad,
                    ImporteTotal = ant.Transaccion.importe_total,
                    Noches = (int)ant.Transaccion.cantidad1,
                    IdMotivoViaje = (int)ant.id_detalle_maestro
                }).ToList();
            var idUbigeosHuespedes = registroHuespedes.Select(rh => rh.IdUbigeo).ToList();
            var ubigeos = _db.Ubigeo.Where(u => idUbigeosHuespedes.Contains(u.id)).Select(u => new
            {
                Id = u.id,
                IdRegion = u.id_region,
                IdProvincia = u.id_provincia,
                Nombre = u.descripcion_larga
            }).ToList();
            var idPaisesHuespedes = registroHuespedes.Select(rh => rh.IdPais).ToList();
            var paisesContinentes = _db.Detalle_maestro.Where(dm => idPaisesHuespedes.Contains(dm.id)).Select(dm => new
            {
                IdPais = dm.id,
                NombrePais = dm.nombre,
                IdContinente = dm.Detalle_detalle_maestro1.FirstOrDefault().Detalle_maestro.id,
                Continente = dm.Detalle_detalle_maestro1.FirstOrDefault().Detalle_maestro.nombre,
            }).ToList();
            registroHuespedes.ForEach(rh =>
            {
                rh.IdRegionUbigeo = ubigeos.Single(u => u.Id == rh.IdUbigeo).IdRegion;
                rh.IdProvinciaUbigeo = ubigeos.Single(u => u.Id == rh.IdUbigeo).IdProvincia;
                rh.Ubigeo = ubigeos.Single(u => u.Id == rh.IdUbigeo).Nombre;
                rh.Pais = paisesContinentes.Single(pc => pc.IdPais == rh.IdPais).NombrePais;
                rh.IdContinente = paisesContinentes.Single(pc => pc.IdPais == rh.IdPais).IdContinente;
                rh.Continente = paisesContinentes.Single(pc => pc.IdPais == rh.IdPais).Continente;
            });
            return registroHuespedes;
        }

        public IEnumerable<Ingreso> ObtenerIngresos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var ingresos = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && (et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(et => et.Transaccion)
                             .Select(t => new Ingreso()
                             {
                                 Fecha = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado).fecha,
                                 Codigo = t.codigo,
                                 Huespedes = t.Actor_negocio_por_transaccion.Select(anpt => new Huesped()
                                 {
                                     Nombre = anpt.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                     ExtensionJson = anpt.extension_json,
                                 }),
                                 TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                                 CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                                 Noches = (int)t.cantidad1
                             }
                             );
            return ingresos;
        }
        public IEnumerable<Salida> ObtenerSalidas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var salidas = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(et => et.Transaccion)
                             .Select(t => new Salida()
                             {
                                 EstadoIngresoReferencia = t.Transaccion3.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                                 EstadoIngreso = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn),
                                 FechaSalida = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut).fecha,
                                 Codigo = t.codigo,
                                 Huespedes = t.Actor_negocio_por_transaccion.Select(anpt => new Huesped()
                                 {
                                     Nombre = anpt.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                     ExtensionJson = anpt.extension_json,
                                 }),
                                 TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                                 CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                                 Noches = (int)t.cantidad1,
                                 Importe = t.importe_total

                             }
                             );
            return salidas;
        }
        public IEnumerable<Anulada> ObtenerAnuladas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var anuladas = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(et => et.Transaccion)
                             .Select(t => new Anulada()
                             {
                                 FechaRegistro = t.fecha_registro_sistema,
                                 Fecha = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado).fecha,
                                 Codigo = t.codigo,
                                 TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                                 CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                                 Empleado = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado).Actor_negocio.Actor.tercer_nombre,
                             }
                             );
            return anuladas;
        }
        public IEnumerable<Facturada> ObtenerFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var eventosTransaccion = _db.Evento_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && et.id_evento == MaestroSettings.Default.IdDetalleMaestroEstadoFacturado && (et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion || et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel) && (et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento || et.Transaccion.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento))
             .Select(et => new EventoAtencion
             {
                 Id = et.id,
                 Fecha = et.fecha,
                 ModoFacturacion = et.Transaccion.enum1,
                 IdAtencion = et.Transaccion.id,
             }).ToList();
            var idsAtencionesOrdenVenta = eventosTransaccion.Select(et => et.IdAtencion);
            var ordenesVentas = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta && idsAtencionesOrdenVenta.Contains((long)t.id_transaccion_referencia)).Select(t => new
            {
                Id = t.id,
                IdReferencia = t.id_transaccion_referencia,
                TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                Cliente = t.Actor_negocio1.Actor.numero_documento_identidad + " | " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                FechaOperacion = t.fecha_inicio,
                Importe = t.importe_total,
            }).ToList();
            var idsOrdenVenta = ordenesVentas.Select(et => et.Id);
            var notasCredito = _db.Transaccion.Where(t => idsOrdenVenta.Contains((long)t.id_transaccion_referencia)).Select(t => new
            {
                IdReferencia = t.id_transaccion_referencia,
                TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                Cliente = t.Actor_negocio1.Actor.numero_documento_identidad + " | " + t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                FechaOperacion = t.fecha_inicio,
                Importe = t.importe_total,
            }).ToList();
            var atencionesHabitacion = ObtenerAtencionesHabitacion(_db, eventosTransaccion);
            var facturadas = ordenesVentas.Select(ov => new Facturada()
            {
                IdEventoFacturado = eventosTransaccion.First(et => et.IdAtencion == ov.IdReferencia).Id,
                Facturacion = eventosTransaccion.First(et => et.IdAtencion == ov.IdReferencia).ModoFacturacion == (int)ModoFacturacionHotel.Global ? "General" : "Por Habitacion",
                Fecha = eventosTransaccion.First(et => et.IdAtencion == ov.IdReferencia).Fecha,
                Codigo = atencionesHabitacion.First(ai => ai.Id == eventosTransaccion.First(et => et.IdAtencion == ov.IdReferencia).IdAtencion).Codigo,
                TipoComprobante = ov.TipoComprobante,
                SerieYNumeroComprobante = ov.SerieYNumeroComprobante,
                Cliente = ov.Cliente,
                FechaOperacion = ov.FechaOperacion,
                Importe = ov.Importe,
                Habitacion = atencionesHabitacion.First(ai => ai.Id == eventosTransaccion.First(et => et.IdAtencion == ov.IdReferencia).IdAtencion).Habitacion,
            }).ToList();
            foreach (var notaCredito in notasCredito)
            {
                facturadas.Add(new Facturada()
                {
                    IdEventoFacturado = eventosTransaccion.First(et => et.IdAtencion == ordenesVentas.First(ov => ov.Id == notaCredito.IdReferencia).IdReferencia).Id,
                    Facturacion = eventosTransaccion.First(et => et.IdAtencion == ordenesVentas.First(ov => ov.Id == notaCredito.IdReferencia).IdReferencia).ModoFacturacion == (int)ModoFacturacionHotel.Global ? "General" : "Por Habitacion",
                    Fecha = eventosTransaccion.First(et => et.IdAtencion == ordenesVentas.First(ov => ov.Id == notaCredito.IdReferencia).IdReferencia).Fecha,
                    Codigo = atencionesHabitacion.First(ai => ai.Id == eventosTransaccion.First(et => et.IdAtencion == ordenesVentas.First(ov => ov.Id == notaCredito.IdReferencia).IdReferencia).IdAtencion).Codigo,
                    TipoComprobante = notaCredito.TipoComprobante,
                    SerieYNumeroComprobante = notaCredito.SerieYNumeroComprobante,
                    Cliente = notaCredito.Cliente,
                    FechaOperacion = notaCredito.FechaOperacion,
                    Importe = notaCredito.Importe * -1,
                    Habitacion = atencionesHabitacion.First(ai => ai.Id == eventosTransaccion.First(et => et.IdAtencion == ordenesVentas.First(ov => ov.Id == notaCredito.IdReferencia).IdReferencia).IdAtencion).Habitacion,
                });
            }
            return facturadas;
        }
        public IEnumerable<NoFacturada> ObtenerNoFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var noFacturadas = _db.Transaccion.Where(t => t.fecha_registro_sistema >= fechaDesde && t.fecha_registro_sistema <= fechaHasta && t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && !t.indicador2 && t.importe_total > 0)/*!t.Evento_transaccion.Select(et => et.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado)*/
                             .Select(t => new NoFacturada()
                             {
                                 FechaRegistro = t.fecha_registro_sistema,
                                 Codigo = t.codigo,
                                 Huespedes = t.Actor_negocio_por_transaccion.Select(anpt => new Huesped()
                                 {
                                     Nombre = anpt.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                     ExtensionJson = anpt.extension_json,
                                 }),
                                 TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                                 CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                                 Importe = t.importe_total,
                                 Estado = t.Estado_transaccion.OrderByDescending(et => et.fecha).FirstOrDefault().Detalle_maestro.nombre
                             }
                             );
            return noFacturadas;
        }
        public IEnumerable<Incidente> ObtenerIncidentes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var eventosTransaccion = _db.Evento_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && et.id_evento == MaestroSettings.Default.IdDetalleMaestroEstadoIncidente && (et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion || et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel) && (et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento || et.Transaccion.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento))
             .Select(et => new EventoAtencion
             {
                 Id = et.id,
                 Fecha = et.fecha,
                 Justificacion = et.comentario,
                 Empleado = et.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                 ModoFacturacion = et.Transaccion.enum1,
                 IdAtencion = et.Transaccion.id,
             }).ToList();
            var idsEventos = eventosTransaccion.Select(et => et.Id);
            var notasCredito = _db.Transaccion.Where(t => idsEventos.Contains((int)t.id_evento_referencia)).Select(t => new
            {
                IdEventoReferencia = t.id_evento_referencia,
                IdTransaccionReferencia = t.id_transaccion_referencia,
                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                Observacion = t.comentario,
                Importe = t.importe_total
            }).ToList();
            var idsAtencionesOrdenVenta = eventosTransaccion.Select(et => et.IdAtencion).ToList();
            var idsAtencionesOrdenVentaReferencia = _db.Transaccion.Where(t =>  idsAtencionesOrdenVenta.Contains((long)t.id)).Select(t => t.Transaccion3).Where(t => t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado).Select(t => t.id).ToList();
            if (idsAtencionesOrdenVentaReferencia != null) idsAtencionesOrdenVenta.AddRange(idsAtencionesOrdenVentaReferencia);
           var ordenesVentas = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta && idsAtencionesOrdenVenta.Contains((long)t.id_transaccion_referencia)).Where(t => t.Detalle_transaccion.Any(dt => dt.Concepto_negocio.id_concepto_basico == HotelSettings.Default.IdDetalleMaestroFamiliaHabitacion)).Select(t => new
            {
                Id = t.id,
                IdReferencia = t.id_transaccion_referencia,
                TipoComprobante = t.Comprobante.Detalle_maestro.valor,
                SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                Importe = t.importe_total,
                MontoHospedaje = t.Detalle_transaccion.Where(d => d.Concepto_negocio.id_concepto_basico == HotelSettings.Default.IdDetalleMaestroFamiliaHabitacion).Sum(d => d.total)
            }).ToList();
            var atencionesIncidente = ObtenerAtencionesHabitacion(_db, eventosTransaccion);
            var incidentes = notasCredito.Select(nc => new Incidente()
            {
                Fecha = eventosTransaccion.First(et => et.Id == nc.IdEventoReferencia).Fecha,
                Codigo = atencionesIncidente.First(ai => ai.Id  == eventosTransaccion.First(et => et.Id == nc.IdEventoReferencia).IdAtencion).Codigo,
                TipoComprobante = ordenesVentas.First(ov => ov.Id == nc.IdTransaccionReferencia).TipoComprobante,
                SerieYNumeroComprobante = ordenesVentas.First(ov => ov.Id == nc.IdTransaccionReferencia).SerieYNumeroComprobante,
                Importe = ordenesVentas.First(ov => ov.Id == nc.IdTransaccionReferencia).Importe,
                MontoHospedaje = ordenesVentas.First(ov => ov.Id == nc.IdTransaccionReferencia).MontoHospedaje,
                Devuelto = nc.Importe,
                Habitacion = atencionesIncidente.First(ai => ai.Id == eventosTransaccion.First(et => et.Id == nc.IdEventoReferencia).IdAtencion).Habitacion,
                TipoComprobanteDescuento = nc.TipoComprobante,
                SerieYNumeroComprobanteDescuento = nc.SerieYNumeroComprobante,
                Solucion = nc.Observacion,
                Empleado = eventosTransaccion.First(et => et.Id == nc.IdEventoReferencia).Empleado,
                Justificacion = eventosTransaccion.First(et => et.Id == nc.IdEventoReferencia).Justificacion,
            }).ToList();
            return incidentes;
        }
        public List<AtencionHabitacion> ObtenerAtencionesHabitacion(SigescomEntities _db, List<EventoAtencion> eventosTransaccion)
        {
            var idsAtencionHabitacion = eventosTransaccion.Where(et => et.ModoFacturacion == (int)ModoFacturacionHotel.Individual).Select(et => et.IdAtencion);
            var atencionesHabitacion = _db.Transaccion.Where(t => idsAtencionHabitacion.Contains(t.id)).Select(t => new AtencionHabitacion
            {
                Id = t.id,
                Codigo = t.codigo,
                Habitacion = t.Actor_negocio2.Concepto_negocio.sufijo + " " + t.Actor_negocio2.Actor.numero_documento_identidad,
            }).ToList();
            var idsAtencionHotel = eventosTransaccion.Where(et => et.ModoFacturacion == (int)ModoFacturacionHotel.Global).Select(et => et.IdAtencion);
            var atencionesHotelConHabitaciones = _db.Transaccion.Where(t => idsAtencionHotel.Contains(t.id)).Select(t => new
            {
                Id = t.id,
                Codigo = t.codigo,
                Habitaciones = t.Transaccion1.Where(t1 => t1.importe_total > 0).Select(t1 => new
                {
                    IdTipoHabitacion = t1.Actor_negocio2.id_concepto_negocio,
                    TipoHabitacion = t1.Actor_negocio2.Concepto_negocio.sufijo,
                    CodigoHabitacion = t1.Actor_negocio2.Actor.numero_documento_identidad,
                }).ToList()
            }).ToList();
            var atencionesHotel = new List<AtencionHabitacion>();
            for (int i = 0; i < atencionesHotelConHabitaciones.Count; i++)
            {
                string habitacion = "";
                var idTiposHabitacionDistintos = atencionesHotelConHabitaciones[i].Habitaciones.Select(h => h.IdTipoHabitacion).Distinct();
                foreach (var idTipoHabitacion in idTiposHabitacionDistintos)
                {
                    var codigosHabitacion = atencionesHotelConHabitaciones[i].Habitaciones.Where(h => h.IdTipoHabitacion == idTipoHabitacion).Select(h => h.CodigoHabitacion).ToList();
                    habitacion += atencionesHotelConHabitaciones[i].Habitaciones.First(h => h.IdTipoHabitacion == idTipoHabitacion).TipoHabitacion + " " + String.Join(",", codigosHabitacion) + " | ";
                }
                habitacion = habitacion.Substring(0, habitacion.Length - 2);
                atencionesHotel.Add(new AtencionHabitacion()
                {
                    Id = atencionesHotelConHabitaciones[i].Id,
                    Codigo = atencionesHotelConHabitaciones[i].Codigo,
                    Habitacion = habitacion
                });
            }
            atencionesHabitacion.AddRange(atencionesHotel);
            return atencionesHabitacion;
        }
        public IEnumerable<Reserva> ObtenerReservasConfirmadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            SigescomEntities _db = new SigescomEntities();
            var reservas = _db.Estado_transaccion.Where(et => et.Transaccion.fecha_inicio >= fechaDesde && et.Transaccion.fecha_inicio <= fechaHasta && et.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(et => et.Transaccion)
                .Select(t => new Reserva()
                {
                    Codigo = t.codigo,
                    FechaIngreso = t.fecha_inicio,
                    FechaSalida = t.fecha_fin,
                    Noches = (int)t.cantidad1,
                    TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                    CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                    Importe = t.importe_total,
                    FechaRegistro = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).fecha,
                    Responsable = t.Actor_negocio3.Actor.primer_nombre.Replace("|", " "),
                    ModoFacturacion = t.enum1,
                });
            return reservas;
        }
        public IEnumerable<Reserva> ObtenerReservasConfirmadasPorTipoHabitacion(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposHabitacion)
        {
            SigescomEntities _db = new SigescomEntities();
            var reservas = _db.Estado_transaccion.Where(et => et.fecha >= fechaDesde && et.fecha <= fechaHasta && et.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && et.Transaccion.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && et.Transaccion.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && idsTiposHabitacion.Contains((int)et.Transaccion.Actor_negocio2.id_concepto_negocio)).Select(et => et.Transaccion)
                .Select(t => new Reserva()
                {
                    Codigo = t.codigo,
                    FechaIngreso = t.fecha_inicio,
                    FechaSalida = t.fecha_fin,
                    Noches = (int)t.cantidad1,
                    TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                    CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                    Importe = t.importe_total,
                    FechaRegistro = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).fecha,
                    Responsable = t.Actor_negocio3.Actor.primer_nombre.Replace("|", " "),
                    ModoFacturacion = t.enum1,
                });
            return reservas;
        }
    }
}
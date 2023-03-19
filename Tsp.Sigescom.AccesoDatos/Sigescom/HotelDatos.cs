using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Newtonsoft.Json;

namespace Tsp.Sigescom.AccesoDatos.Sigescom
{
    public class HotelDatos : RepositorioBase, IHotelRepositorio
    {
        #region AMBIENTE DE HABITACION
        public IEnumerable<ItemGenerico> ObtenerAmbientes()
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolAmbienteHotel).Select(an => new ItemGenerico()
                {
                    Id = an.id,
                    Nombre = an.Actor.primer_nombre,
                });
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener ambiente hotel", e);
            }
        }
        public IEnumerable<AmbienteHotel> ObtenerAmbientesHotelPorEstablecimiento(int idEstablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolAmbienteHotel && an.id_actor_negocio_padre == idEstablecimiento).Select(an => new AmbienteHotel()
                {
                    Id = an.id,
                    IdActor = an.Actor.id,
                    Codigo = an.Actor.numero_documento_identidad,
                    Nombre = an.Actor.primer_nombre,
                    EsVigente = an.es_vigente

                });
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener ambiente hotel", e);
            }
        }
        public IEnumerable<ItemGenerico> ObtenerAmbientesVigentesPorEstablecimientoSimplificado(int idEstablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolAmbienteHotel && an.id_actor_negocio_padre == idEstablecimiento && an.es_vigente).Select(an => new ItemGenerico()
                {
                    Id = an.id,
                    Nombre = an.Actor.primer_nombre,
                });
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener ambiente hotel", e);
            }
        }

        public bool ExisteNombreAmbienteEnEstablecimiento(string nombreAmbiente, int idEstablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_actor_negocio_padre == idEstablecimiento && an.Actor.primer_nombre == nombreAmbiente && an.id_rol == HotelSettings.Default.IdRolAmbienteHotel).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el nombre de ambiente", e);
            }
        }
        public bool ExisteNombreAmbienteEnEstablecimientoExceptoAmbiente(string nombreAmbiente, int idEstablecimiento, int idAmbienteExcluir)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_actor_negocio_padre == idEstablecimiento && an.Actor.primer_nombre == nombreAmbiente && an.id_rol == HotelSettings.Default.IdRolAmbienteHotel && an.id != idAmbienteExcluir).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el nombre de ambiente", e);
            }
        }
        #endregion

        #region TIPO HABITACION
        public IEnumerable<ItemGenerico> ObtenerTiposHabitacionVigentesSimplificado()
        {
            try
            {
                return _db.Concepto_negocio.Where(cn => cn.id_concepto_basico == HotelSettings.Default.IdDetalleMaestroFamiliaHabitacion && cn.es_vigente).Select(cn => new ItemGenerico()
                {
                    Id = cn.id,
                    Nombre = cn.sufijo,
                });
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener tipo habitacion", e);
            }
        }
        public bool ExisteNombreTipoHabitacion(string nombreTipoHabitacion)
        {
            try
            {
                return _db.Concepto_negocio_rol.Where(cnr => cnr.Concepto_negocio.nombre == nombreTipoHabitacion && cnr.id_rol == HotelSettings.Default.IdRolConceptoHotel).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el nombre de tipo de habitacion", e);
            }
        }
        public bool ExisteNombreTipoHabitacionExceptoTipoHabitacion(string nombreTipoHabitacion, int idTipoHabitacionExcluir)
        {
            try
            {
                return _db.Concepto_negocio_rol.Where(cnr => cnr.Concepto_negocio.nombre == nombreTipoHabitacion && cnr.id_rol == HotelSettings.Default.IdRolConceptoHotel && cnr.Concepto_negocio.id != idTipoHabitacionExcluir).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el nombre de tipo de habitacion", e);
            }
        }

        #endregion

        #region CAMAS
        public IEnumerable<ItemGenerico> ObtenerTiposCama()
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == HotelSettings.Default.IdMaestroTipoCama).Select(dm => new ItemGenerico()
                {
                    Id = dm.id,
                    Nombre = dm.nombre,
                });

            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener tipo camas", e);
            }
        }
        #endregion

        #region COMPLEMENTOS
        public IEnumerable<Complemento> ObtenerComplementos()
        {
            //return _db.Detalle_maestro.Where(dm => dm.id_maestro == 4542).Select(dm => new ItemGenerico()//MaestroSettings.Default.IdRolAmbienteHotel
            //{
            //    Id = dm.id,
            //    Nombre = dm.nombre,
            //});
            return null;
        }
        #endregion

        #region HABITACIONES
        public bool ExisteCodigoHabitacionEnEstablecimiento(string codigoHabitacion, int idEstablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && an.Actor.numero_documento_identidad == codigoHabitacion && an.id_rol == HotelSettings.Default.IdRolHabitacion).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el codigo de habitación", e);
            }
        }
        public bool ExisteCodigoHabitacionEnEstablecimientoExceptoHabitacion(string codigoHabitacion, int idEstablecimiento, int idHabitacionExcluir)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && an.Actor.numero_documento_identidad == codigoHabitacion && an.id != idHabitacionExcluir && an.id_rol == HotelSettings.Default.IdRolHabitacion).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de verificar si exite el codigo de habitación", e);
            }
        }
        public bool ExisteHabitacionesVigentesTipoHabitacion(int idTipoHabitacion)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolHabitacion && an.Concepto_negocio.id == idTipoHabitacion && an.es_vigente).Any();
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener tipo habitacion", e);
            }
        }
        public bool ExisteHabitacionesVigentesAmbiente(int idAmbiente)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolHabitacion && an.Actor_negocio2.id == idAmbiente && an.es_vigente).Any();
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener tipo habitacion", e);
            }
        }
        public bool ExisteAtencionesHabitacion(int idHabitacion)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_actor_negocio_interno == idHabitacion && (t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado || t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut || t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado)).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }
        public Habitacion ObtenerHabitacion(int id)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id == id && an.id_rol == HotelSettings.Default.IdRolHabitacion).Select(an => new Habitacion
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = new AmbienteHotel
                    {
                        Id = an.Actor_negocio2.id,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                        Establecimiento = new ItemGenerico
                        {
                            Id = an.Actor_negocio2.Actor_negocio2.id,
                            Nombre = an.Actor_negocio2.Actor_negocio2.Actor.primer_nombre
                        }
                    },
                    TipoHabitacion = new Concepto_Negocio_Comercial_
                    {
                        Id = an.Concepto_negocio.id,
                        Nombre = an.Concepto_negocio.sufijo
                    },
                    InformacionCamas = an.Actor.segundo_nombre,
                    //Aforo = an.Actor.informacion_multiproposito,
                    Anexo = an.Actor.telefono,
                    EsVigente = an.es_vigente

                }).SingleOrDefault();

            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener habitacion", e);
            }
        }

        public IEnumerable<HabitacionBandeja> ObtenerHabitacionesBandeja(int idestablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_rol == HotelSettings.Default.IdRolHabitacion && an.Actor_negocio2.Actor_negocio2.id == idestablecimiento).Select(an => new HabitacionBandeja()
                {
                    Id = an.id,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = an.Actor_negocio2.Actor.primer_nombre,
                    TipoHabitacion = an.Concepto_negocio.sufijo,
                    Camas = an.Actor.segundo_nombre,
                    Aforo = an.Actor.informacion_multiproposito,
                    AnexoTelefono = an.Actor.telefono,
                    EsVigente = an.es_vigente
                });

            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener habitaciones", e);
            }

        }

        public IEnumerable<Habitacion> ObtenerHabitacionDisponibles(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idAmbiente, int idActorNegocioQueTienePrecios)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_concepto_negocio == idTipoHabitacion && an.id_actor_negocio_padre == idAmbiente && an.es_vigente).Except(_db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado && ((t.fecha_fin > fechaDesde && t.fecha_inicio < fechaHasta) && ((t.fecha_inicio >= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio >= fechaDesde && t.fecha_fin >= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin >= fechaHasta)))).Select(t => t.Actor_negocio2).Where(an => an.id_concepto_negocio == idTipoHabitacion && an.id_actor_negocio_padre == idAmbiente).Distinct()).Select(an => new Habitacion()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = new AmbienteHotel
                    {
                        Id = an.Actor_negocio2.id,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    },
                    TipoHabitacion = new Concepto_Negocio_Comercial_
                    {
                        Id = an.Concepto_negocio.id,
                        Nombre = an.Concepto_negocio.sufijo,
                        Precios = an.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                    },
                    InformacionCamas = an.Actor.segundo_nombre,
                    InformacionAforo = "Adultos:" + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoAdultos).Valor_caracteristica.valor + " - Niños: " + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoNinos).Valor_caracteristica.valor,
                    Anexo = an.Actor.telefono,
                    EsVigente = an.es_vigente
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }
        public bool ObtenerDisponibilidadHabitacion(long idAtencionAEvitar, int idHabitacion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id == idHabitacion && an.es_vigente).Except(_db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut && t.id != idAtencionAEvitar && ((t.fecha_fin > fechaDesde && t.fecha_inicio < fechaHasta) && ((t.fecha_inicio >= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio >= fechaDesde && t.fecha_fin >= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin >= fechaHasta)))).Select(t => t.Actor_negocio2).Where(an => an.id == idHabitacion).Distinct()).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }
        public IEnumerable<Habitacion> ObtenerHabitacionDisponiblesPorEstablecimientoConPrecio(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idEstablecimiento, int idActorNegocioQueTienePrecios)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_concepto_negocio == idTipoHabitacion && an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && an.es_vigente).Except(_db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut && ((t.fecha_fin > fechaDesde && t.fecha_inicio < fechaHasta) && ((t.fecha_inicio >= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio >= fechaDesde && t.fecha_fin >= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin >= fechaHasta)))).Select(t => t.Actor_negocio2).Where(an => an.id_concepto_negocio == idTipoHabitacion && an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Distinct()).Select(an => new Habitacion()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = new AmbienteHotel
                    {
                        Id = an.Actor_negocio2.id,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    },
                    TipoHabitacion = new Concepto_Negocio_Comercial_
                    {
                        Id = an.Concepto_negocio.id,
                        Nombre = an.Concepto_negocio.sufijo,
                        Precios = an.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                    },
                    InformacionCamas = an.Actor.segundo_nombre,
                    InformacionAforo = "Adultos:" + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoAdultos).Valor_caracteristica.valor + " - Niños: " + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoNinos).Valor_caracteristica.valor,
                    Anexo = an.Actor.telefono,
                    EsVigente = an.es_vigente
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }

        public IEnumerable<Habitacion> ObtenerHabitacionDisponiblesPorEstablecimiento(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idEstablecimiento)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id_concepto_negocio == idTipoHabitacion && an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && an.es_vigente).Except(_db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroCodigoEstadoItemAnulado && ((t.fecha_fin > fechaDesde && t.fecha_inicio < fechaHasta) && ((t.fecha_inicio >= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio >= fechaDesde && t.fecha_fin >= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin <= fechaHasta) || (t.fecha_inicio <= fechaDesde && t.fecha_fin >= fechaHasta)))).Select(t => t.Actor_negocio2).Where(an => an.id_concepto_negocio == idTipoHabitacion && an.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Distinct()).Select(an => new Habitacion()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = new AmbienteHotel
                    {
                        Id = an.Actor_negocio2.id,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    },
                    TipoHabitacion = new Concepto_Negocio_Comercial_
                    {
                        Id = an.Concepto_negocio.id,
                        Nombre = an.Concepto_negocio.sufijo
                    },
                    InformacionCamas = an.Actor.segundo_nombre,
                    InformacionAforo = "Adultos:" + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoAdultos).Valor_caracteristica.valor + " - Niños: " + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoNinos).Valor_caracteristica.valor,
                    Anexo = an.Actor.telefono,
                    EsVigente = an.es_vigente
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }







        public Habitacion ObtenerHabitacionDisponible(int idHabitacion, int idActorNegocioQueTienePrecios)
        {
            try
            {
                return _db.Actor_negocio.Where(an => an.id == idHabitacion && an.es_vigente).Select(an => new Habitacion()
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = new AmbienteHotel
                    {
                        Id = an.Actor_negocio2.id,
                        Nombre = an.Actor_negocio2.Actor.primer_nombre,
                    },
                    TipoHabitacion = new Concepto_Negocio_Comercial_
                    {
                        Id = an.Concepto_negocio.id,
                        Nombre = an.Concepto_negocio.sufijo,
                        Precios = an.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                    },
                    InformacionCamas = an.Actor.segundo_nombre,
                    InformacionAforo = "Adultos:" + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoAdultos).Valor_caracteristica.valor + " - Niños: " + an.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(v => v.Valor_caracteristica.id_caracteristica == HotelSettings.Default.IdCaracteristicaAforoNinos).Valor_caracteristica.valor,
                    Anexo = an.Actor.telefono,
                    EsVigente = an.es_vigente
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener habitaciones disponibles", e);
            }
        }
        #endregion
        //public ReportePlanificador ObtenerReportePlanificador(int[] idsHabitaciones, DateTime fechaActual)
        //{
        //    try
        //    {
        //        fechaActual = fechaActual.Date;
        //        var fechaSiguiente = fechaActual.AddDays(1).Date;
        //        ReportePlanificador reportePlanificador = new ReportePlanificador();
        //        reportePlanificador.Ocupadas = _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && idsHabitaciones.Contains(t.id_actor_negocio_interno) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado && t.fecha_fin > fechaActual && t.fecha_inicio < fechaSiguiente && ((t.fecha_inicio >= fechaActual && t.fecha_fin <= fechaSiguiente) || (t.fecha_inicio >= fechaActual && t.fecha_fin >= fechaSiguiente) || (t.fecha_inicio <= fechaActual && t.fecha_fin <= fechaSiguiente) || (t.fecha_inicio <= fechaActual && t.fecha_fin >= fechaSiguiente))).Select(t => t.Actor_negocio2).Distinct().Count();
        //        reportePlanificador.Disponibles = _db.Actor_negocio.Where(an => idsHabitaciones.Contains(an.id)).Except(_db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado && t.id_estado_actual != MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut && t.fecha_fin > fechaActual && t.fecha_inicio < fechaSiguiente && ((t.fecha_inicio >= fechaActual && t.fecha_fin <= fechaSiguiente) || (t.fecha_inicio >= fechaActual && t.fecha_fin >= fechaSiguiente) || (t.fecha_inicio <= fechaActual && t.fecha_fin <= fechaSiguiente) || (t.fecha_inicio <= fechaActual && t.fecha_fin >= fechaSiguiente))).Select(t => t.Actor_negocio2)).Distinct().Count();
        //        reportePlanificador.PorIngresar = _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && idsHabitaciones.Contains(t.id_actor_negocio_interno) && t.fecha_inicio <= fechaActual && t.fecha_fin >= fechaActual && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && !t.Estado_transaccion.Select(et => et.id_estado).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn)).Count();
        //        reportePlanificador.PorSalir = _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && idsHabitaciones.Contains(t.id_actor_negocio_interno) && t.fecha_fin <= fechaActual && (t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && !t.Estado_transaccion.Select(et => et.id_estado).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut)).Count();
        //        return reportePlanificador;
        //            }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al intentar de obtener el reporte de planificador", e);
        //    }
        //}

        public IEnumerable<HabitacionEnPlanificador> ObtenerHabitacionesPlanificador(int[] idsAmbientes, int[] idsTiposHabitaciones, int idActorNegocioQueTienePrecios, DateTime fechaActual)
        {
            try
            {
                return _db.Actor_negocio.Where(an => idsTiposHabitaciones.Contains((int)an.id_concepto_negocio) && idsAmbientes.Contains((int)an.id_actor_negocio_padre) && an.es_vigente).Select(an => new HabitacionEnPlanificador()
                {
                    Id = an.id,
                    CodigoHabitacion = an.Actor.numero_documento_identidad,
                    Ambiente = an.Actor_negocio2.Actor.primer_nombre,
                    TipoHabitacion = an.Concepto_negocio.sufijo,
                    Precios = an.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente).ToList(),
                    EnLimpieza = an.indicador1,
                    Disponible = !an.Transaccion2.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && (t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado || t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado) && t.fecha_fin > fechaActual && t.fecha_inicio <= fechaActual).Any(),
                    Ocupada = an.Transaccion2.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && (t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && t.fecha_fin > fechaActual && t.fecha_inicio <= fechaActual).Any(),
                    PorIngresar = an.Transaccion2.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.fecha_inicio <= fechaActual && (t.fecha_fin >= fechaActual || (t.fecha_fin == fechaActual.Date && fechaActual.Hour < 12)) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && !t.Estado_transaccion.Select(et => et.id_estado).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn)).Any(),
                    PorSalir = an.Transaccion2.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.fecha_fin <= fechaActual && (t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado) && !t.Estado_transaccion.Select(et => et.id_estado).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut)).Any()
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar de obtener el planificador", e);
            }
        }
        public EstadoHabitacionEnPlanificador ObtenerEstadoHabitacionPlanificador(DateTime fechaConsulta, int idHabitacion, DateTime fechaActual)
        {
            try
            {
                EstadoHabitacionEnPlanificador estadoHabitacion = new EstadoHabitacionEnPlanificador();
                bool existeAtencion = _db.Transaccion.Where(t => t.fecha_inicio <= fechaConsulta && fechaConsulta < t.fecha_fin && t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_actor_negocio_interno == idHabitacion).Any();
                if (existeAtencion)
                {
                    var atencionHabitacion = _db.Transaccion.Include(t => t.Estado_transaccion).Include(t => t.Actor_negocio2).Where(t => t.fecha_inicio <= fechaConsulta && fechaConsulta < t.fecha_fin && t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id_actor_negocio_interno == idHabitacion).OrderByDescending(t => t.id).FirstOrDefault();
                    estadoHabitacion.IdEstado = atencionHabitacion.Estado_transaccion.OrderByDescending(e => e.fecha).FirstOrDefault().id_estado;
                    estadoHabitacion.IdAtencion = atencionHabitacion.id;
                    estadoHabitacion.IdAtencionMacro = (long)atencionHabitacion.id_transaccion_padre;
                    estadoHabitacion.Fecha = fechaConsulta.Date;
                    estadoHabitacion.EsFechaAtencion = (fechaConsulta.AddDays(1).Date.AddHours(12) - fechaActual).TotalMinutes - HotelSettings.Default.ToleranciaEnMinutosParaChecking > 0;
                }
                else
                {
                    estadoHabitacion.IdEstado = MaestroSettings.Default.IdDetalleMaestroEstadoDisponible;
                    estadoHabitacion.Fecha = fechaConsulta.Date;
                    var cd = (fechaConsulta.AddDays(1).Date.AddHours(12) - fechaActual).TotalMinutes;
                    estadoHabitacion.EsFechaAtencion = (fechaConsulta.AddDays(1).Date.AddHours(12) - fechaActual).TotalMinutes - HotelSettings.Default.ToleranciaEnMinutosParaChecking > 0;
                }
                return estadoHabitacion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener estado habitacion planificador", e);
            }
        }
        #region RESERVA
        public IEnumerable<ReservaBandeja> ObtenerReservaBandeja(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento)
                    .Select(t => new ReservaBandeja()
                    {
                        IdAtencion = t.id,
                        IdAtencionMacro = (long)t.id_transaccion_padre,
                        Codigo = t.codigo,
                        Responsable = t.Transaccion2.Actor_negocio3.Actor.primer_nombre,
                        Ambiente = t.Actor_negocio2.Actor_negocio2.Actor.primer_nombre,
                        TipoHabitacion = t.Actor_negocio2.Concepto_negocio.sufijo,
                        CodigoHabitacion = t.Actor_negocio2.Actor.numero_documento_identidad,
                        Ingreso = t.fecha_inicio,
                        Salida = t.fecha_fin,
                        Noches = (int)t.cantidad1,
                        Importe = t.importe_total,
                        Estado = t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().Detalle_maestro.nombre,
                        Facturado = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener reservas", e);
            }
        }

        public ItemGenerico ObtenerUltimoMotivoViajeHuesped(int idHuesded)
        {
            try
            {
                return _db.Actor_negocio_por_transaccion.Where(t => t.id_rol == HotelSettings.Default.IdRolHuesped && t.id_actor_negocio == idHuesded).OrderByDescending(t => t.id)
                    .Select(t => new ItemGenerico()
                    {
                        Id = t.Detalle_maestro.id,
                        Nombre = t.Detalle_maestro.nombre
                    }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener el ultimo motivo de viaje", e);
            }
        }
        public OperationResult CrearActorNegocioPorTransaccion(Actor_negocio_por_transaccion actorNegocioPorTransaccion)
        {
            try
            {
                _db.Actor_negocio_por_transaccion.Add(actorNegocioPorTransaccion);
                var result = Save();
                result.data = actorNegocioPorTransaccion.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult CambiarTitularHuesped(int idHuespedCambiado, int idHuespedNuevoTitular)
        {
            try
            {
                var dbHuespedCambiado = _db.Actor_negocio_por_transaccion.Single(at => at.id == idHuespedCambiado);
                var dbHuespedTitular = _db.Actor_negocio_por_transaccion.Single(at => at.id == idHuespedNuevoTitular);
                var extensionJsonTitular = dbHuespedCambiado.extension_json;
                dbHuespedCambiado.extension_json = dbHuespedTitular.extension_json;
                dbHuespedTitular.extension_json = extensionJsonTitular;
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarActorNegocioExterno1DeTransacciones(List<long> idsTransaccion, int idActorNegocioExterno1)
        {
            try
            {
                foreach (var idTransaccion in idsTransaccion)
                {
                    Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == idTransaccion);
                    dbTransaccion.id_actor_negocio_externo1 = idActorNegocioExterno1;
                }
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult EliminarActorNegocioPorTransaccion(int idActorNegocioPorTransaccion)
        {
            try
            {
                Actor_negocio_por_transaccion dbActorNegocioPorTransaccion = _db.Actor_negocio_por_transaccion.Single(m => m.id == idActorNegocioPorTransaccion);
                _db.Actor_negocio_por_transaccion.Remove(dbActorNegocioPorTransaccion);
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public AtencionMacroHotel ObtenerAtencionMacro(long idAtencionMacro)
        {
            try
            {
                AtencionMacroHotel atencionMacro = _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel && t.id == idAtencionMacro)
                    .Select(t => new AtencionMacroHotel()
                    {
                        Id = t.id,
                        Codigo = t.codigo,
                        Responsable = new ActorComercial_
                        {
                            Id = t.Actor_negocio3.id,
                            TipoDocumentoIdentidad = new ItemGenerico() { Id = t.Actor_negocio3.Actor.id_documento_identidad, Nombre = t.Actor_negocio3.Actor.Detalle_maestro.nombre },
                            NombreORazonSocial = t.Actor_negocio3.Actor.primer_nombre.Replace("|", " ")
                        },
                        Total = t.importe_total,
                        FacturadoGlobal = t.enum1 == (int)ModoFacturacionHotel.NoEspecificado || t.enum1 == (int)ModoFacturacionHotel.Global,
                        TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                        FechaRegistro = t.fecha_registro_sistema,
                        HayImagenVoucherExtranet = t.indicador1,
                        Eventos = t.Evento_transaccion.Select(et => new ItemEstado
                        {
                            Id = et.Detalle_maestro.id,
                            Nombre = et.Detalle_maestro.nombre,
                            Fecha = et.fecha,
                            Observacion = et.comentario
                        }).ToList(),
                        Atenciones = t.Transaccion1.Select(tt => new AtencionHotel
                        {
                            Id = tt.id,
                            Habitacion = new Habitacion
                            {
                                Id = tt.Actor_negocio2.id,
                                CodigoHabitacion = tt.Actor_negocio2.Actor.numero_documento_identidad,
                                TipoHabitacion = new Concepto_Negocio_Comercial_
                                {
                                    Id = tt.Actor_negocio2.Concepto_negocio.id,
                                    Nombre = tt.Actor_negocio2.Concepto_negocio.sufijo
                                }
                            },
                            FechaIngreso = tt.fecha_inicio,
                            FechaSalida = tt.fecha_fin,
                            Noches = (int)tt.cantidad1,
                            PrecioUnitario = tt.importe1,
                            Importe = tt.importe_total,
                            Facturado = tt.indicador1,
                            TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                            FacturadoGlobal = t.enum1 == (int)ModoFacturacionHotel.NoEspecificado || t.enum1 == (int)ModoFacturacionHotel.Global,
                            AnotacionesJson = tt.comentario,
                            Huespedes = tt.Actor_negocio_por_transaccion.Select(at => new Huesped
                            {
                                IdHuesped = at.id,
                                Id = at.Actor_negocio.id,
                                NumeroDocumentoIdentidad = at.Actor_negocio.Actor.numero_documento_identidad,
                                TipoDocumentoIdentidad = new ItemGenerico { Id = at.Actor_negocio.Actor.id_documento_identidad, Nombre = at.Actor_negocio.Actor.Detalle_maestro.nombre },
                                NombreORazonSocial = at.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                                ClaseActor = new ItemGenerico { Id = at.Actor_negocio.Actor.id_clase_actor, Nombre = at.Actor_negocio.Actor.Clase_actor.nombre },
                                FechaNacimiento = at.Actor_negocio.Actor.fecha_nacimiento,
                                Nacionalidad = new ItemGenerico { Id = at.Actor_negocio.Actor.Detalle_maestro1.id, Nombre = at.Actor_negocio.Actor.Detalle_maestro1.nombre },
                                MotivoDeViaje = new ItemGenerico { Id = at.Detalle_maestro.id, Nombre = at.Detalle_maestro.nombre },
                                JsonHuesdep = at.extension_json,
                                DomicilioFiscal = new Direccion_
                                {
                                    Id = at.Actor_negocio.Actor.Direccion.FirstOrDefault().id,
                                    Pais = new ItemGenerico { Id = at.Actor_negocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id, Nombre = at.Actor_negocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre },
                                    Ubigeo = new ItemGenerico { Id = at.Actor_negocio.Actor.Direccion.FirstOrDefault().Ubigeo.id, Nombre = at.Actor_negocio.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga },
                                    Detalle = at.Actor_negocio.Actor.Direccion.FirstOrDefault().detalle
                                }
                            }).ToList(),
                            Estados = tt.Estado_transaccion.Select(et => new ItemEstado
                            {
                                Id = et.Detalle_maestro.id,
                                Nombre = et.Detalle_maestro.nombre,
                                Fecha = et.fecha,
                                Observacion = et.comentario
                            }).ToList().Union(tt.Evento_transaccion.Select(et => new ItemEstado
                            {
                                Id = et.Detalle_maestro.id,
                                Nombre = et.Detalle_maestro.nombre,
                                Fecha = et.fecha,
                                Observacion = et.comentario
                            }).ToList()).ToList(),
                        }).OrderBy(to => to.FechaIngreso).ToList()
                    }).SingleOrDefault();
                return atencionMacro;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener atencion macro", e);
            }
        }

        public Atencion ObtenerAtencionDesdeAtencionMacro(long idAtencionMacro)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel && t.id == idAtencionMacro)
                    .Select(t => new Atencion()
                    {
                        Id = t.id,
                        Codigo = t.codigo,
                        TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                        ComprobantesOrdenPrincipal = t.Transaccion11.Where(tr => tr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        ComprobantesReferenciaPrincipal = t.Transaccion11.SelectMany(ttr => ttr.Transaccion11).Where(ttr => (ttr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta && ttr.Comprobante.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna) || Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebitoExceptoAnulacionDeVenta.Contains(ttr.id_tipo_transaccion)).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        OrdenPrincipal = new OrdenAtencion
                        {
                            Id = t.id,
                            Codigo = t.codigo,
                            FechaRegistro = t.fecha_inicio,
                            Importe = t.importe_total,
                            TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                            Detalles = t.Transaccion1.Select(th => new DetalleOrdenAtencion
                            {
                                Id = th.id,
                                IdConcepto = (int)th.Actor_negocio2.id_concepto_negocio,
                                IdFamilia = th.Actor_negocio2.Concepto_negocio.id_concepto_basico,
                                NombreConcepto = th.Actor_negocio2.Concepto_negocio.nombre + " - " + th.Actor_negocio2.Actor.numero_documento_identidad,
                                Cantidad = th.cantidad1,
                                PrecioUnitario = th.importe1,
                                Importe = th.importe_total,
                                IdEstadoActual = (int)th.id_estado_actual
                            })
                        },
                        Ordenes = t.Transaccion1.SelectMany(tr => tr.Transaccion11).Where(_tr => _tr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion).Where(trr => trr.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(tr => new OrdenAtencion
                        {
                            Id = tr.id,
                            Codigo = tr.Actor_negocio1.Actor.primer_nombre,
                            FechaRegistro = tr.fecha_inicio,
                            Importe = tr.importe_total,
                            TieneFacturacion = tr.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado),
                            Detalles = tr.Detalle_transaccion.Distinct().Select(dtr => new DetalleOrdenAtencion
                            {
                                Id = dtr.id,
                                IdConcepto = dtr.id_concepto_negocio,
                                IdFamilia = dtr.Concepto_negocio.id_concepto_basico,
                                NombreConcepto = dtr.Concepto_negocio.nombre,
                                Cantidad = dtr.cantidad,
                                PrecioUnitario = dtr.precio_unitario,
                                Importe = dtr.total,
                                IdEstadoActual = 0
                            })
                        }).ToList()
                    }).SingleOrDefault();

            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener atencion desde atencion macro", e);
            }
        }

        public Atencion ObtenerAtencionDesdeAtencion(long idAtencion)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id == idAtencion)
                    .Select(t => new Atencion()
                    {
                        Id = t.id,
                        Codigo = t.codigo,
                        TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                        ComprobantesOrdenPrincipal = t.Transaccion11.Where(tr => tr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        ComprobantesOrdenSecundario = t.Transaccion3.Transaccion11.Where(tr => tr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        ComprobantesReferenciaPrincipal = t.Transaccion11.SelectMany(ttr => ttr.Transaccion11).Where(ttr => (ttr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta && ttr.Comprobante.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna) || Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebitoExceptoAnulacionDeVenta.Contains(ttr.id_tipo_transaccion)).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        ComprobantesReferenciaSecundario = t.Transaccion3.Transaccion11.SelectMany(ttr => ttr.Transaccion11).Where(ttr => (ttr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta && ttr.Comprobante.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna) || Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebitoExceptoAnulacionDeVenta.Contains(ttr.id_tipo_transaccion)).Select(tr => new ComprobanteFacturado
                        {
                            IdOrden = tr.id,
                        }),
                        OrdenPrincipal = new OrdenAtencion
                        {
                            Id = t.id,
                            Codigo = t.codigo,
                            FechaRegistro = t.fecha_inicio,
                            Importe = t.importe_total,
                            TieneFacturacion = t.enum1 != (int)ModoFacturacionHotel.NoEspecificado,
                            Detalles = new List<DetalleOrdenAtencion> { new DetalleOrdenAtencion
                            {
                                Id = t.id,
                                IdConcepto = (int)t.Actor_negocio2.id_concepto_negocio,
                                IdFamilia = t.Actor_negocio2.Concepto_negocio.id_concepto_basico,
                                NombreConcepto = t.Actor_negocio2.Concepto_negocio.nombre + " - " + t.Actor_negocio2.Actor.numero_documento_identidad,
                                Cantidad = t.cantidad1,
                                PrecioUnitario = t.importe1,
                                Importe = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual
                            } }
                        },
                        Ordenes = t.Transaccion11.Where(trr => trr.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion && trr.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(tr => new OrdenAtencion
                        {
                            Id = tr.id,
                            Codigo = tr.Actor_negocio1.Actor.primer_nombre,
                            FechaRegistro = tr.fecha_inicio,
                            Importe = tr.importe_total,
                            TieneFacturacion = tr.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado),
                            Detalles = tr.Detalle_transaccion.Distinct().Select(dtr => new DetalleOrdenAtencion
                            {
                                Id = dtr.id,
                                IdConcepto = dtr.id_concepto_negocio,
                                IdFamilia = dtr.Concepto_negocio.id_concepto_basico,
                                NombreConcepto = dtr.Concepto_negocio.nombre,
                                Cantidad = dtr.cantidad,
                                PrecioUnitario = dtr.precio_unitario,
                                Importe = dtr.total,
                                IdEstadoActual = 0
                            })
                        }).ToList()
                    }).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener atencion desde atencion", e);
            }
        }
        public OperationResult ActualizarComentarioTransaccion(long idTransaccion, string comentario)
        {
            try
            {
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == idTransaccion);
                dbTransaccion.comentario = comentario;
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar el comentario de transaccion", e);
            }
        }
        public Transaccion ObtenerTransaccionAtencionMacro(long idAtencionMacro)
        {
            try
            {
                return _db.Transaccion.Single(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel && t.id == idAtencionMacro);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener la transaccion atencion", e);
            }
        }
        public Transaccion ObtenerTransaccionAtencion(long idAtencion)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Estado_transaccion).Include(t => t.Detalle_maestro).Single(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id == idAtencion);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener la transaccion atencion", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransaccionesHijasDeAtencionMacro(long idAtencionMacro)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Transaccion1).Include(t => t.Estado_transaccion).Include(t => t.Detalle_maestro).Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHotel && t.id == idAtencionMacro).SelectMany(t => t.Transaccion1);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones hijas de la atencion macro", e);
            }
        }
        public Transaccion ObtenerTransaccionPadreDeAtencion(long idAtencion)
        {
            try
            {
                return _db.Transaccion.Single(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && t.id == idAtencion).Transaccion2;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener los ids de las atenciones", e);
            }
        }

        #endregion
        #region EXTRANETWEB
        public IEnumerable<RoomType> ObtenerRoomType()
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == 4541).Select(dm => new RoomType()//MaestroSettings.Default.IdRolAmbienteHotel
                {
                    Id = dm.id,
                    Name = dm.nombre,
                });
            }
            catch (Exception e)
            {

                throw new DatosException("Error al tratar de obtener tipo habitacion", e);
            }
        }
        #endregion

        #region CONSUMO
        public IEnumerable<Consumo> ObtenerConsumos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion && t.Actor_negocio4.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta)
                    .Select(t => new Consumo()
                    {
                        Id = t.id,
                        CodigoHabitacion = t.Actor_negocio4.Actor.numero_documento_identidad,
                        Facturado = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado),
                        Fecha = t.fecha_inicio,
                        IdEstado = (int)t.id_estado_actual,
                        Huesped = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        Importe = t.importe_total,
                        TipoHabitacion = t.Actor_negocio4.Concepto_negocio.sufijo,
                        DetallesConsumo = t.Detalle_transaccion.Select(dt => new DetalleConsumo
                        {
                            Id = dt.id,
                            Nombre = dt.Concepto_negocio.nombre,
                            Cantidad = dt.cantidad,
                            PrecioUnitario = dt.precio_unitario,
                            Importe = dt.total,
                        }).ToList()
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener los consumos", e);
            }
        }

        public ConsumoHabitacion ObtenerConsumoHabitacion(int idAtencion)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id == idAtencion)
                    .Select(t => new ConsumoHabitacion()
                    {
                        IdAtencion = t.id,
                        IdHabitacion = t.id_actor_negocio_interno,
                        FechaDesde = t.fecha_inicio,
                        FechaHasta = t.fecha_fin,
                        Huespedes = t.Actor_negocio_por_transaccion.Select(at => new ItemGenerico
                        {
                            Id = at.id_actor_negocio,
                            Nombre = at.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                            Valor = at.extension_json
                        }).ToList(),
                        Consumos = t.Transaccion11.Where(tt => tt.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion && tt.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(tt => new ConsumoSimple
                        {
                            Id = tt.id,
                            Fecha = tt.fecha_inicio,
                            Huesped = tt.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                            Importe = tt.importe_total,
                            DetallesConsumo = tt.Detalle_transaccion.Select(dt => new DetalleConsumo
                            {
                                Id = dt.id,
                                Nombre = dt.Concepto_negocio.nombre,
                                Cantidad = dt.cantidad,
                                PrecioUnitario = dt.precio_unitario,
                                Importe = dt.total,
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener el consumo de habitacion", e);
            }
        }

        public IEnumerable<ItemGenerico> ObtenerAtencionesEnCheckedInComoHabitaciones(int idEstablecimiento)
        {
            try
            {
                var idEstadosEnCheckin = new int[] { MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn, MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado };
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == HotelSettings.Default.IdTipoTransaccionAtencionDeHabitacion && idEstadosEnCheckin.Contains((int)t.id_estado_actual) && t.Actor_negocio2.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento)
                    .Select(t => new ItemGenerico()
                    {
                        Id = (int)t.id,
                        Nombre = t.Actor_negocio2.Concepto_negocio.sufijo + " - " + t.Actor_negocio2.Actor.numero_documento_identidad
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener las atenciones en checkedin", e);
            }
        }

        #endregion
    }
}

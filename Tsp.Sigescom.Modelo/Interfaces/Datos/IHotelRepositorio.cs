using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos
{
    public interface IHotelRepositorio
    {

        #region AMBIENTE DE HABITACION
        IEnumerable<ItemGenerico> ObtenerAmbientes();
        IEnumerable<AmbienteHotel> ObtenerAmbientesHotelPorEstablecimiento(int idEstablecimiento);
        IEnumerable<ItemGenerico> ObtenerAmbientesVigentesPorEstablecimientoSimplificado(int idEstablecimiento);
        bool ExisteNombreAmbienteEnEstablecimiento(string nombreAmbiente, int idEstablecimiento);
        bool ExisteNombreAmbienteEnEstablecimientoExceptoAmbiente(string nombreAmbiente, int idEstablecimiento, int idAmbienteExcluir);
        #endregion

        #region TIPO HABITACION
        IEnumerable<ItemGenerico> ObtenerTiposHabitacionVigentesSimplificado();
        bool ExisteNombreTipoHabitacion(string nombreTipoHabitacion);
        bool ExisteNombreTipoHabitacionExceptoTipoHabitacion(string nombreTipoHabitacion, int idTipoHabitacionExcluir);
        #endregion

        #region CAMAS
        IEnumerable<ItemGenerico> ObtenerTiposCama();
        #endregion

        #region COMPLEMENTOS
        IEnumerable<Complemento> ObtenerComplementos();
        #endregion

        #region HABITACIONES
        bool ExisteCodigoHabitacionEnEstablecimiento(string codigoHabitacion, int idEstablecimiento);
        bool ExisteCodigoHabitacionEnEstablecimientoExceptoHabitacion(string codigoHabitacion, int idEstablecimiento, int idHabitacionExcluir);
        bool ExisteHabitacionesVigentesAmbiente(int idAmbiente);
        bool ExisteHabitacionesVigentesTipoHabitacion(int idTipoHabitacion);
        bool ExisteAtencionesHabitacion(int idHabitacion);
        Habitacion ObtenerHabitacion(int id);
        IEnumerable<HabitacionBandeja> ObtenerHabitacionesBandeja(int idestablecimiento);
        IEnumerable<Habitacion> ObtenerHabitacionDisponibles(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idAmbiente, int idActorNegocioQueTienePrecios);
        Habitacion ObtenerHabitacionDisponible(int idHabitacion, int idActorNegocioQueTienePrecios);
        IEnumerable<Habitacion> ObtenerHabitacionDisponiblesPorEstablecimientoConPrecio(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idEstablecimiento, int idActorNegocioQueTienePrecios);
        IEnumerable<Habitacion> ObtenerHabitacionDisponiblesPorEstablecimiento(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idEstablecimiento);
        bool ObtenerDisponibilidadHabitacion(long idAtencionAEvitar, int idHabitacion, DateTime fechaDesde, DateTime fechaHasta);
        #endregion
        //ReportePlanificador ObtenerReportePlanificador(int[] idsHabitaciones, DateTime fechaActual);
        IEnumerable<HabitacionEnPlanificador> ObtenerHabitacionesPlanificador(int[] idsAmbientes, int[] idsTiposHabitaciones, int idActorNegocioQueTienePrecios, DateTime fechaActual);
        EstadoHabitacionEnPlanificador ObtenerEstadoHabitacionPlanificador(DateTime fechaConsulta, int idHabitacion, DateTime fechaActual);
        //void CompletarEstadosHabitacionPlanificadorDeFechaActual(DateTime fechaActual, DateTime fechaActualFinal, List<HabitacionEnPlanificador> habitacionesEnPlanificador);
        #region RESERVAS
        IEnumerable<ReservaBandeja> ObtenerReservaBandeja(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        ItemGenerico ObtenerUltimoMotivoViajeHuesped(int idHuesded);
        OperationResult CrearActorNegocioPorTransaccion(Actor_negocio_por_transaccion actorNegocioPorTransaccion);
        OperationResult CambiarTitularHuesped(int idHuespedCambiado, int idHuespedNuevoTitular);
        OperationResult ActualizarActorNegocioExterno1DeTransacciones(List<long> idsTransaccion, int idActorNegocioExterno1);
        OperationResult EliminarActorNegocioPorTransaccion(int idActorNegocioPorTransaccion);
        AtencionMacroHotel ObtenerAtencionMacro(long idAtencionMacro);
        OperationResult ActualizarComentarioTransaccion(long idTransaccion, string comentario);
        Atencion ObtenerAtencionDesdeAtencionMacro(long idAtencionMacro);
        Atencion ObtenerAtencionDesdeAtencion(long idAtencion);
        #endregion
        #region EXTRANETWEB
        IEnumerable<RoomType> ObtenerRoomType();
        #endregion

        #region CONSUMO
        IEnumerable<Consumo> ObtenerConsumos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        ConsumoHabitacion ObtenerConsumoHabitacion(int idAtencion);
        IEnumerable<ItemGenerico> ObtenerAtencionesEnCheckedInComoHabitaciones(int idEstablecimiento);
        #endregion
    }
}

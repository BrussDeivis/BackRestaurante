using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio
{
    public interface IHotelLogica
    {

        List<ItemGenerico> ObtenerTiposHabitacionVigentesSimplificado(); 
        List<ItemGenerico> ObtenerTipoCamas();

        OperationResult ActualizarComplemento(Complemento complemento);
        OperationResult GuardarComplemento(Complemento complemento);
        OperationResult EditarHabitacion(Habitacion complemento);
        OperationResult CrearHabitacion(Habitacion complemento);
        List<HabitacionBandeja> ObtenerHabitacionesBandeja(int idestablecimiento);
        OperationResult CambiarEsVigenteHabitacion(int id);
        List<Habitacion> ObtenerHabitacionesDisponibles(int idTipoHabitacion, DateTime fechaDesde, DateTime fechaHasta, int idEstablecimiento, int idAmbiente, int idActorNegocioQueTienePrecios);
        Habitacion ObtenerHabitacionDisponible(int idHabitacion, int idActorNegocioQueTienePrecios);
        ReportePlanificador ObtenerReportePlanificador(int idEstablecimiento, int idActorNegocioQueTienePrecios);
        Planificador ObtenerPlanificadorHabitaciones(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int idAmbiente, int idTipoHabitacion, int idActorNegocioQueTienePrecios);
        OperationResult CambiarEnLimpiezaDeHabitacion(int idHabitacion);
        OperationResult EditarTipoHabitacion(TipoHabitacion tipoHabitacion, UserProfileSessionData sesionDeUsuario);
        OperationResult CrearTipoHabitacion(TipoHabitacion tipoHabitacion, UserProfileSessionData sesionDeUsuario);
        List<TipoHabitacionesBandeja> ObtenerTipoHabitaciones();
        TipoHabitacion ObtenerTipoHabitacion(int id, UserProfileSessionData sesionDeUsuario);
        Habitacion ObtenerHabitacion(int id);
        OperationResult CambiarVigenciaDelTipoHabitacion(int id);

        List<ItemGenerico> ObtenerAmbientesVigentesPorEstablecimientoSimplificado(int idEstablecimiento);
        List<AmbienteHotel> ObtenerAmbientesHotelPorEstablecimiento(int idEstablecimiento);
        OperationResult CrearAmbiente(AmbienteHotel ambienteHotel);
        OperationResult EditarAmbiente(AmbienteHotel ambiente);
        OperationResult CambiarVigenciaDelAmbienteHotel(int idAmbiente);

        //reserva

        List<ReservaBandeja> ObtenerReservaBandeja(int idestablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        OperationResult ConfirmarReserva(AtencionMacroHotel reserva, UserProfileSessionData sesionUsuario);
        OperationResult CheckInReserva(AtencionMacroHotel reserva, UserProfileSessionData sesionUsuario);
        
        ItemGenerico ObtenerUltimoMotivoViajeHuesped(int idHuesped);
        OperationResult AgregarHuesped(long idAtencion, int idActorComercial, int idMotivoViaje, bool esTitular);
        OperationResult EliminarHuesped(int idHuesped);
        OperationResult CambiarTitularHuesped(int idHuespedCambiado, int idHuespedNuevoTitular);
        AtencionMacroHotel ObtenerAtencionMacro(long idAtencionMacro, UserProfileSessionData sesionUsuario);
        OperationResult EditarResponsableAtencionMacro(long idAtencionMacro, int idResponsable);
        OperationResult EditarFechaAtencion(AtencionHotel atencion);
        OperationResult CambiarHabitacionAtencion(AtencionHotel atencion, UserProfileSessionData sesionUsuario);
        OperationResult GuardarAnotacion(AtencionHotel atencion);
        OperationResult ConfirmarAtencionMacro(long idAtencionMacro, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult ConfirmarAtencion(long idAtencion, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult CheckInAtencionMacro(long idAtencionMacro, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult CheckInAtencion(long idAtencion, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult CheckOutAtencionMacro(long idAtencionMacro, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult CheckOutAtencion(long idAtencion, string observacion, UserProfileSessionData sesionUsuario);
        List<ComprobanteAtencion> ObtenerComprobantesDeAtencionMacro(long idAtencionMacro);
        OperationResult AnularAtencionMacro(long idAtencionMacro, List<ComprobanteAtencion> comprobantes, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult RegistrarIncidenteAtencionMacro(long idAtencionMacro, bool esDevolucion, List<ComprobanteAtencion> comprobantes, string observacion, UserProfileSessionData sesionUsuario);
        List<ComprobanteAtencion> ObtenerComprobantesDeAtencion(long idAtencionMacro, long idAtencion);
        OperationResult AnularAtencion(long idAtencion, long idAtencionMacro, List<ComprobanteAtencion> comprobantes, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult RegistrarIncidenteAtencion(long idAtencion, long idAtencionMacro, bool esDevolucion, List<ComprobanteAtencion> comprobantes, string observacion, UserProfileSessionData sesionUsuario);
        Atencion ObtenerAtencionDesdeAtencionMacro(long idAtencionMacro);
        Atencion ObtenerAtencionDesdeAtencion(long idAtencion);
        OperationResult FacturarAtencionMacro(Atencion atencion, UserProfileSessionData sesionUsuario);
        OperationResult FacturarAtencion(Atencion atencion, UserProfileSessionData sesionUsuario);
        List<Consumo> ObtenerConsumos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<ItemGenerico> ObtenerAtencionesEnCheckedInComoHabitaciones(int idEstablecimiento);
        ConsumoHabitacion ObtenerConsumoHabitacion(int idAtencion);
        OperationResult ConfirmarConsumo(ConsumoHabitacion consumoHabitacion, UserProfileSessionData sesionUsuario);
        OperationResult InvalidarConsumo(long idConsumo, UserProfileSessionData sesionUsuario);
        #region EXTRANETWEB
        List<RoomType> ObtenerRoomType();
        OperationResult RegistrarBooking(Booking booking, UserProfileSessionData sesion);
        List<RoomType> ObtenerRoomTypesDisponibles(DateBooking dateBooking);
        #endregion
    }
}

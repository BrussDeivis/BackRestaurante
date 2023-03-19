using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.Report;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio
{
    public interface IHotelReporte_Logica
    {
        PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData);
        List<RegistroHuesped> ObtenerRegistroHuespedes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Ingreso> ObtenerIngresos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Salida> ObtenerSalidas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Anulada> ObtenerAnuladas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        FormularioT1 ObtenerFormularioT1(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Facturada> ObtenerFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<NoFacturada> ObtenerNoFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Incidente> ObtenerIncidentes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        List<Reserva> ObtenerReservasConfirmadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool todosTiposHabitacion, int[] idsTiposHabitacion);
    }
}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Hotel

{
    public interface IHotelReporte_Repositorio
    {
        IEnumerable<RegistroHuesped> ObtenerRegistroHuespedes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<RegistroHuesped> ObtenerRegistroHuespedesCompleto(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Ingreso> ObtenerIngresos(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Salida> ObtenerSalidas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Anulada> ObtenerAnuladas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Facturada> ObtenerFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<NoFacturada> ObtenerNoFacturadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Incidente> ObtenerIncidentes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Reserva> ObtenerReservasConfirmadas(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Reserva> ObtenerReservasConfirmadasPorTipoHabitacion(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposHabitacion);
    }
}

using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ICotizacionLogica
    {
        OperationResult AgregarCotizacion(int idEmpleado, int idCentroAtencion, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, bool gravaIgv, DateTime fechaVencimiento, string observacion, List<DetalleDeOperacion> detalles, decimal flete);
        OperationResult EditarCotizacion(long id, long idOrden, long idComprobante, int idEstado, int idEmpleado, int idCentroAtencion, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, bool gravaIgv, DateTime fechaVencimiento, string observacion, List<DetalleDeOperacion> detalles, decimal flete);

        List<OrdenDeCotizacion> ObtenerOrdenesDeCotizacion(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);

        OrdenDeCotizacion ObtenerOrdenDeCotizacion(long idOrdenDeCotizacion);

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCotizacion(int idCentroAtencion);

        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeCotizacion ordenDeCotizacion);

        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeCotizacion ordenDeCotizacion);
    }
}

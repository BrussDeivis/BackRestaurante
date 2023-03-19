using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {


        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCompra(int idEmpleado, int idCentroAtencion);

        List<OrdenDeCompra> ObtenerOrdenesDeCompra(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        List<OperacionDeCompra> ObtenerOperacionesDeCompra(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        List<OrdenDeCompra> ObtenerOrdenesDeCompraConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Compra> ObtenerResumenesCompraDeTipoNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int todoComprobante);
        List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int todoComprobante);
        List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int todoComprobante);
        List<Resumen_Compra> ObtenerResumenesCompraDeTipoGravadasDestinadasAVentasGravadasYNoGravadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int todoComprobante);
        List<OperacionDeCompra> ObtenerOrdenesYNotasDeCreditoYDebitoDeComprasTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        OperationResult ConfirmarCompra(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);
        
        OperationResult ConfirmarCompraAlCreditoRapido(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompraAlCredito(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, UserProfileSessionData sesionUsuario);
        /*
        OperationResult ConfirmarOrdenCompra(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idAlmacen, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraYPago(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idProveedor, int tipoCompra, int idAlmacen, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraEIngresoMercaderia(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string serieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompra(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraAlCreditoRapido(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idAlmacen, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompraAlCreditoRapido(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraAlCredito(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idAlmacen, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraYPagoAlCredito(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idProveedor, int tipoCompra, int idAlmacen, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarOrdenCompraEIngresoMercaderiaAlCredito(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompraAlCredito(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);

        OperationResult RegistrarCompraAlContado(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult RegistrarCompraAlCreditoRapido(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult RegistrarCompraAlCredito(int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, UserProfileSessionData sesionUsuario);

        OperationResult EditarCompraAlContado(long idCompra, int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult EditarCompraAlCreditoRapido(long idCompra, int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, UserProfileSessionData sesionUsuario);

        OperationResult EditarCompraAlCredito(long idCompra, int idComprador, int idPuntoDeCompra, int idProveedor, int tipoCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string observacion, DateTime fechaRegistro, List<DetalleDeOperacion> detalles, decimal flete, List<Cuota> cuotas, UserProfileSessionData sesionUsuario);
        */
        bool ExisteNumeroDeComprobante(int idProveedor, int idTipoComprobante, string numeroDeSerie, int numeroComprobante, int idEstadoActual);

        Compra ObtenerCompra(long data);

        OrdenDeCompra ObtenerOrdenDeCompra(long data);

        OperationResult InvalidarCompra(long idOrden, int idEmpleado, int idEntidadInternaSeleccionada, string observacion, UserProfileSessionData sesionUsuario);

        //OperationResult AnularCompra(long idOrdenDeCompra, int idEmpleado, int idCentroAtencion, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieComprobante, int numeroComprobante, string observaciones);

        //OperationResult InvalidarAnulacionDeCompra(long idOrdenAnulacionDeCompra, int idEmpleado, int idCentroAtencion, string observaciones);
        OperationResult RegistrarComprobanteCompraCorporativa(int idEmpleado, int idCentroAtencion, long idCompra, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante);
        /*OperationResult ConfirmarCompraCorporativa(int idComprador, int idPuntoDeCompra, int idAlmacen, long idCompra);
        OperationResult ConfirmarCompraCorporativaYPago(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacen, long idCompra, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompraCorporativaYMovimientoMercaderia(int idComprador, int idPuntoDeCompra, int idAlmacenero, int idAlmacen, long idCompra, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, UserProfileSessionData sesionUsuario);

        OperationResult ConfirmarCompraCorporativaPagoYMovimientoMercaderia(int idComprador, int idPuntoDeCompra, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, long idCompra, int idTipoComprobanteIngresoMercaderia, int idSerieComprobanteIngresoMercaderia, bool esPropioIngresoMercaderia, string serieDeComprobanteIngresoMercaderia, int numeroDeComprobanteIngresoMercaderia, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string observacionIngresoMercaderia, List<DetalleDeOperacion> detallesIngresoMercaderia, bool ingresoTotalOrden, bool usaComprobanteDeOrden, int idMedioPago, int idEntidadFinanciera, string informacionDePago, UserProfileSessionData sesionUsuario);
        */
        OperationResult GuardarNotaDeDebitoDeCompra(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario);

        OperationResult GuardarNotaDeCreditoDeCompra(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario );

        List<string> ObtenerFechaIncioyFinParaReporteCompra();

    }
}


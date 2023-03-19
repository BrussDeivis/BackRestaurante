using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaMovimientoMercaderia(int idCentroAtencion);

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAlmacen(int idEmpleado, int idCentroAtencion);

        List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteGuiaDeRemision(int idEmpleado, int idCentroAtencion);

        List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteNotaDeAlmacen(int idEmpleado, int idCentroAtencion);

        List<TipoDeComprobanteParaTransaccion> ObtenerTipoDeComprobanteOrdenDeAlmacen(int idEmpleado, int idCentroAtencion);

        OperationResult ConfirmarMovimientoInternoMercaderiaIntegrado(int idEmpleado, int idCentroAtencion, int idAlmacenDestino, int idResponsableDestino, int idTipoComprobante,
            int idSerieComprobante, bool esPropio, string serieDeComprobante, int numeroDeComprobante, string observacion, List<Detalle_transaccion> detalles, UserProfileSessionData sesionDeUsuario);

        List<OrdenDeTrasladoInterno> ObtenerOrdenesIngresoInternoMercaderia(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);

        List<OrdenDeTrasladoInterno> ObtenerOrdenesSalidaInternoMercaderia(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);


        OrdenDeTrasladoInterno ObtenerOrdenMovimiento(long idOrdenDesplazamiento);

        decimal ObtenerStockDeProducto(int idProducto, int idCentroAtencion);

        TrasladoInterno ObtenerMovimiento(long idDesplazamiento);

        List<Orden_Recibir_Entregar> ObtenerOrdenesDeAlmacenPorRecibir(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Orden_Recibir_Entregar> ObtenerOrdenesDeAlmacenPorEntregar(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Entrada_Salida_Almacen> ObtenerEntradasDeAlmacen(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Entrada_Salida_Almacen> ObtenerSalidasDeAlmacen(int idEmpleado, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);


        OperationResult GuardarOrdenDeAlmacen(long idOrdenDeOperacion, int idEmpleado, int idCentroAtencion, int idSerieComprobante, int idAlmacen, DateTime fechaTransporte, string observacion, List<DetalleDeOperacion> detalles, bool generacionTotalOrden);
        /// <summary>
        /// Confirma el omvimiento de mercaderia de acuerdo si es un ingreso o salida de mercaderia solo se cambiara el tipo de ingreso de mercaderia de acuerdo al diccionario
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <param name="idOrden"></param>
        /// <param name="idTipoComprobante"></param>
        /// <param name="idSerieComprobante"></param>
        /// <param name="esPropio"></param>
        /// <param name="serieDeComprobante"></param>
        /// <param name="numeroDeComprobante"></param>
        /// <param name="fechaInicioTransporte"></param>
        /// <param name="idTransportista"></param>
        /// <param name="placaYMarcaTransporte"></param>
        /// <param name="numeroLicenciaTransporte"></param>
        /// <param name="idModalidadTransaporte"></param>
        /// <param name="idMotivoTransaporte"></param>
        /// <param name="direccionOrigenTraslado"></param>
        /// <param name="direccionDestinoTraslado"></param>
        /// <param name="observacion"></param>
        /// <param name="detalles"></param>
        /// <param name="ingresoTotalOrden"></param>
        /// <returns></returns>
       // OperationResult GuardarMovimientoDeAlmacen(long idOrden, int idEmpleado, int idCentroAtencion, bool esPropio, int idTipoComprobante, int idSerieComprobante, string serieDeComprobante, int numeroDeComprobante, DateTime fechaInicioTransporte, int idTransportista, string placaYMarcaTransporte, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string comprobanteReferencia, string observacion, List<Detalle_transaccion> detalles, bool ingresoTotalOrden, UserProfileSessionData sesionUsuario);



        OrdenDeMovimientoDeAlmacen ObtenerOrdenDeMovimientoDeAlmacen(long idOrdenDeAlmacen);




        List<Reporte_Concepto_Basico> ObtenerReporteDeSalidasDeAlcohol(DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosDeVenta);

        List<string> ObtenerFechaIncioyFinParaReporteAlmacen();


        List<MovimientoDeAlmacen> ObtenerGuiasRemision(int[] idsCentroAtencion, DateTime desde, DateTime hasta);
        MovimientoDeAlmacen ObtenerGuiaRemision(long idGuiaRemision);
        OperationResult GuardarGuiaRemision(int idTercero, bool esPropio, int idTipoComprobante, int idSerieComprobante, string serieDeComprobante, int numeroDeComprobante, DateTime fechaInicioTransporte, int idTransportista, string placaTransporte, int idConductor, string numeroLicenciaTransporte, int idModalidadTransaporte, int idMotivoTransaporte, string descripcionMotivo, decimal pesoBrutoTotal, int numeroBultos, string direccionOrigenTraslado, int idUbigeoOrigenTraslado, string direccionDestinoTraslado, int idUbigeoDestinoTraslado, string comprobanteReferencia, string observacion, List<Detalle_transaccion> detalles, UserProfileSessionData sesionUsuario);
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaGuiaRemision(int idEmpleado, int idCentroAtencion);
        List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteGuiaRemisionNotaAlmacen(int idEmpleado, int idCentroAtencion);
        OperationResult InvalidarGuiaRemision(long idGuiaRemision, int idEmpleado, int idCentroAtencion, string observacion);
        OperationResult AfectarInventarioFisicoYGuardarOperacion(OperacionIntegrada operacion, UserProfileSessionData sesionDeUsuario);
        OperationResult AfectarInventarioFisicoYGuardarOperacion(OperacionModificatoria operacion, UserProfileSessionData sesionDeUsuario);
        OperationResult AfectarInventarioFisicoYGuardarInventarioEstadosTransacciones(Transaccion movimientoBienes, List<Estado_transaccion> estadosTransacciones, UserProfileSessionData sesionDeUsuario);
        OperationResult GuardarMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen, UserProfileSessionData sesionUsuario);
    }
}

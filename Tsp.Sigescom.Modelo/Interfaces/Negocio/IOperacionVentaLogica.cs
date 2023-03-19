using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {


        OperationResult ConfirmarVentaIntegrada(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada datosVentaIntegrada);
        OperationResult ConfirmarVentasIntegradas(ModoOperacionEnum tipoDeVenta, UserProfileSessionData sesionDeUsuario, List<DatosVentaIntegrada> datosVentasIntegradas);
        //OperationResult ConfirmarVentaAlContado(ModoOperacionEnum tipoDeVenta, int idVendedor, int idPuntoDeVenta, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, int numeroDeComprobante, bool esVentaPasada, DateTime fechaEmision, decimal tipoDeCambio, int idMedioDePago, int idEntidadFinanciera, string informacionDePago, bool gravaIgv, bool detalleUnificado, List<DetalleDeOperacion> detallesDeVenta, string observacion, decimal flete, int numeroBolsasDePlastico, decimal icbper, bool haySalidaDeMercaderia, List<MovimientoDeAlmacen> salidasDeMercaderia, long idInventarioFisico);

        //OperationResult ConfirmarVentaAlCreditoRapido(ModoOperacionEnum tipoDeVenta, int idVendedor, int idPuntoDeVenta, int idAlmacenero, int idAlmacen, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, int numeroDeComprobante, bool esVentaPasada, DateTime fechaEmision, decimal tipoDeCambio, bool gravaIgv, bool detalleUnificado, List<DetalleDeOperacion> detallesDeVenta, string observacion, decimal flete, int numeroBolsasDePlastico, decimal icbper, bool haySalidaDeMercaderia, List<MovimientoDeAlmacen> salidasDeMercaderia, long idInventarioFisico);

        //OperationResult ConfirmarVentaAlCredito(ModoOperacionEnum tipoDeVenta, int idVendedor, int idPuntoDeVenta, int idCajero, int idCaja, int idAlmacenero, int idAlmacen, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, int numeroDeComprobante, bool esVentaPasada, DateTime fechaEmision, decimal tipoDeCambio, int idMedioDePago, int idEntidadFinanciera, string informacionDePago, bool gravaIgv, bool detalleUnificado, List<DetalleDeOperacion> detallesDeVenta, string observacion, decimal flete, int numeroBolsasDePlastico, decimal icbper, List<Cuota> cuotas, decimal montoInicial, bool haySalidaDeMercaderia, List<MovimientoDeAlmacen> salidasDeMercaderia, long idInventarioFisico);

        PuntosDeCliente ObtenerPuntosDeCliente(int idCliente);
        OperationResult InvalidarOrdenVentaRegistrada(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, string observaciones);

        void validarAccionSoberOrdenVenta(OrdenDeVenta ordenDeVenta, AccionOperativa accionAIntentar, int idEmpleado);

        //OperationResult anularVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, int idSerie, string observaciones);

        OperationResult RegistrarOrdenVenta(int idEmpleado, int idCliente, int idCentroAtencion, int idSerieDeComprobante, DateTime fechaRegistro, string observacion, List<Detalle_transaccion> detalles);

        OperationResult ConfirmarYPagarOrdenVenta(int idEmpleado, int idCentroAtencion, long idVenta, int idSerieDeComprobante, DateTime fechaConfirmacion, int idMedioDePago, int idEntidadFinanciera, string informacionBancaria, string observacion);

        //OperationResult anularOperacionesDeVenta(int idEmpleado, int idCentroAtencion, int idCliente, int idSerie, DateTime fechaEmision, DateTime fechaVencimiento, string observaciones, long[] idsOperacionesDeVenta);

        //OperationResult descontarOperacionesDeVenta(int idEmpleado, int idCentroAtencion, int idCliente, int idSerie, DateTime fechaEmision, DateTime fechaVencimiento, string observaciones, long[] idsOperacionesDeVenta, List<Detalle_transaccion> detalles,
        //    List<OrdenDeVentaCompraParaDescuentoDebito> ordenesDescuento);

        //OperationResult debitoOperacionesDeVenta(int idEmpleado, int idCentroAtencion, int idCliente, int idSerie, DateTime fechaEmision, DateTime fechaVencimiento, string observaciones, long[] idsOperacionesDeVenta, List<Detalle_transaccion> detalles,
        //    List<OrdenDeVentaCompraParaDescuentoDebito> ordenesDebito);

        OperationResult cancelarOrdenDeVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion);

        OrdenDeVenta ObtenerOrdenDeVenta(int idEmpleado, int idCentroAtencion, long idOrdenVenta);

        Transaccion ObtenerVentaYOrdenVenta(int idEmpleado, int idCentroAtencion, long idVenta);
        //List<OperacionDeVenta> ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasAnuladasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<OperacionDeVenta> ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        //List<OperacionDeVenta> ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYAnuladasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        /// <summary>
        /// solo debe importar que sean tributables, por lo que su ultimo estado debe ser transmitido
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        /// <summary>
        /// Devuelve las operaciones de venta validas para SUNAT, incluyendo los conceptos de negocio
        /// este metodo sirve para el reporte de productos controlados por SUNAT (insumos quimicos)
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributablesIncluyendoConceptos(int idEmpleado,
            DateTime fechaDesde, DateTime fechaHasta);
        List<OperacionDeVenta> ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributablesSinConcepto(int idEmpleado, DateTime fechaDesde,
            DateTime fechaHasta);
        List<ReporteVentaDetalladoSinConcepto> ObtenerReporteVentaDetalladoSinConcepto(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        List<Venta_Cliente> ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        List<Venta_Cliente> ObtenerVentasClienteQueSeanConNotasDeCreditoYDebito(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        /// <summary>
        /// Obtiene la ventas confirmadas segun los puntos de venta
        /// </summary>
        /// <param name="idsPuntosDeVentas"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        List<Venta_Cliente> ObtenerVentasClientesQueSeanConComprobantesTributablesConfirmadas(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        /// <summary>
        /// Obtienes las ventas invalidadas segun los puntos de venta
        /// </summary>
        /// <param name="idsPuntosDeVentas"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        List<Venta_Cliente> ObtenerVentasClientesQueSeanConComprobantesTributablesInvalidadas(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);


        List<Venta_Cliente> ObtenerVentasClientesConfirmadasConIcbper(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);
        List<Venta_Cliente> ObtenerVentasClientesInvalidadasConIcbper(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<Venta_Cliente> ConsolidarRegistroDeVentas(Periodo periodo, int idEmpleado);

        /// <summary>
        /// devuelve de manera consolidada como una sola operacion de venta, todas aquellas ventas correspondientes a la serie indicada en el periodo de tiempo indicado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idSerie"></param>
        /// <param name="fechaInicioDelDia"></param>
        /// <param name="fechaFinDelDia"></param>
        /// <returns></returns>
        Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadas(int idEmpleado, int idSerie, DateTime fechaInicioDelDia, DateTime fechaFinDelDia);
        Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMenorA(int idEmpleado, int idSerie, DateTime fechaInicioDelDia, DateTime fechaFinDelDia, long idMaximo);
        Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(int idEmpleado, int idSerie, DateTime fechaInicioDelDia, DateTime fechaFinDelDia, long idMinimo, long idMaximo);
        List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(int idEmpleado, int idSerie, long idMinimo, long idMaximo);
        List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorA(int idEmpleado, int idSerie, long numeroDeComprobanteMinimo, long numeroDeComprobanteMaximo);
        List<Transaccion_consolidada> ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorAFechaDesdeHasta(int idEmpleado, int idSerie, long numeroDeComprobanteMinimo, long numeroDeComprobanteMaximo, DateTime fechaDesde, DateTime fechaHasta);

        List<DatosVentaIntegrada> ObtenerOrdenesDeVenta_SegunOperacionOrigen(long idOperacionOrigen);

        long ObtenerIdDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);
        long ObtenerIdDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);

        long ObtenerNumeroDeComprobanteDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);
        long ObtenerNumeroDeComprobanteDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);

        Transaccion_consolidada ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorA(int idEmpleado, int idSerie, DateTime fechaInicioDelDia, DateTime fechaFinDelDia, long idMinimo);

        List<int> ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta();


        OperationResult CrearEstadosDeTransacciones(List<Estado_transaccion> estadosDeTransacciones);
        OperationResult CrearEventosDeTransacciones(List<Evento_transaccion> eventosDeTransacciones);
        OperationResult CrearEventoTransaccion(Evento_transaccion eventoTransaccion);
        OperationResult CrearEventoTransaccionInformacionTransaccion(Evento_transaccion eventoTransaccion, string informacionTransaccion);

        List<OrdenDeVenta> ObtenerOrdenesDeVenta(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);

        List<OperacionDeVenta> ObtenerOperacionesDeVenta(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Venta> ObtenerResumenesVentas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int? idCliente, string comprobante);

        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        //List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasOTransmitidas_(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); //y44
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasYTransmitidas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); // confirmadas y47
        List<OrdenDeVenta> ObtenerOrdenesDeVentaInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); //y45
        List<OrdenDeVenta> ObtenerOrdenesDeVentaAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); //y46
        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaConfirmadasYTransmitidasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); // confirmado  XY4.1 
        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaInvalidadasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); //invalidadas XY 4.2
        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaAnuladasPorConceptoBasicoYDeNegocio(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta); //XY 4.3

        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasEnIntervaloDeFecha(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta);
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta); //y22

        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta); //XY1.1
        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoInvalidadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta); //XY1.2
        List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoAnuldada(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta); //XY1.3

        // c1 reporte venta de conceoto por vendedor -> ir a implementacion
        List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptConfirmadas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta); // confirmada C1.1
        List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptoInvalidadas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta); //Invalidada C1.2
        List<Resumen_transaccion_Venta_PorConcepto> ObtenerOrdenesDeVentaPorConceptoAnuladas(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta); //anulada C1.3

        // c2 reporte de venta de concepto por vendedor - administrador  -> ir a implementación
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasOTransferidasDeVendedorAdministrador(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta); // c2
        List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerReporteVentasConSerieYConceptoConfirmadaOTransmitida(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);// y66  cod:XY5
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadaOTransmitida_(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);//   y77  cod:XY5
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadaOTransmitida(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);//cod:XY5 y55

        List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioConfirmada(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoConfirmadaYTransmitida(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);

        List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenVentasConSerieYConceptoNegocioAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);// XY5.5
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenVentasConSerieYConceptoBasicoAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);// XY5.6


        List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorConfirmadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.1
        List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorInvalidadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.2
        List<ResumenTransaccionPorVendedor> ObtenerResumenDeVentasPorVendedorAnuladas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.3

        List<DetalleTransaccionPorVendedor> ObtenerDetalleDeVentasPorVendedorConfirmadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.4
        List<DetalleTransaccionPorVendedor> ObtenerDetallaDeVentasPorVendedorInvalidadas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.5
        List<DetalleTransaccionPorVendedor> ObtenerDetallaDeVentasPorVendedorAnuladas(int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);//a14.6

        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoConfirmadasYTransmitidas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);//cod:XY6.1
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoInvalidadas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);//cod:XY6.2
        List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerReporteVentasConSerieYConceptoBasicoAnuladas(int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);//cod:XY6.3

        List<Resumen_Transaccion_Consolidado> ObtenerReporteVentaCompraPagoCobroYGastos(DateTime fechaDesde, DateTime fechaHasta);//cod:XY7  Y77
        List<Cuota> ObtenerReporteDeudasAProveedor();//xy10
        List<Cuota> ObtenerReporteDeudasDeCliente(); //C2

        List<OrdenDeVenta> ObtenerOrdenesDeVenta(long[] idsOperacionesDeVentas);

        OrdenDeVenta ObtenerOrdenDeVenta(long data);

        OrdenDeVenta ObtenerOrdenDeVentaConsultaComprobante(ConsultaComprobanteParameter consultaComprobante);
        OrdenDeVenta ObtenerOrdenDeVentaParaImprimir(OrdenDeVenta ordenDeVenta);
        List<MovimientoDeAlmacen> ObtenerSalidaDeMercaderiaDeVenta(long idVenta);
        MovimientoDeAlmacen ObtenerMovimientoDeMercaderia(long idMovimiento);
        List<long> ObtenerIdsGuiaRemisionPorEnviarSunat(DateTime fechaDesde, DateTime fechaHasta);
        Task<List<Detalle_maestro>> ObtenerMediosDePagoVenta();

        List<OrdenDeVenta> ObtenerOrdenesDeVentaHoy(int IdEmpleado);
        //devuelve las ventas realizadas en el centro de atencion indicado, a la fecha actual
        List<OrdenDeVenta> ObtenerOrdenesDeVentaHoyConfirmadas(int IdEmpleado, int idCentroAtencion);
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int idSerie);
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConfirmadasOTransmitidas(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta, int idSerie); //y11
        List<ResumenDeTransaccionVenta> ObtenerResumenDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idSerie);
        List<OrdenDeVenta> ObtenerOrdenesDeVentaTransmitidasYConfirmadas(DateTime fechaDesde, DateTime fechaHasta, int idSerie); //XY2.1
        List<OrdenDeVenta> ObtenerOrdenesDeVentaInvalidadas(DateTime fechaDesde, DateTime fechaHasta, int idSerie); //XY2.2
        List<OrdenDeVenta> ObtenerOrdenesDeVentaAnuladas(DateTime fechaDesde, DateTime fechaHasta, int idSerie); //XY2.3

        List<DateTime> ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();

        OperationResult InvalidarVenta(InvalidarVenta invalidarVenta, UserProfileSessionData sesionUsuario);
        OperationResult AnularVenta(long idOrdenDeVenta, int idEmpleado, int idCentroAtencion, int idSerieComprobante, string observaciones);
        OperationResult InvalidarAnulacionDeVenta(long idOrdenAnulacionDeCompra, int idEmpleado, int idCentroAtencion, string observaciones);
        OrdenDeVenta ObtenerOrdenDeVentaConIdOrden(long data);
        long obtenerIdOrdenDeVenta(long idVenta);
        long ObtenerIdOrdenDeVenta(long idVenta);
        List<long> ObtenerIdOrdenesDeVentaConBoletaYFacturaConfirmadasEInvalidadas(int idEmpleado);

        List<OrdenDeVenta> ObtenerOrdenesDeVentaConBoletaConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int cantidadAObtener);
        List<OrdenDeVenta> ObtenerOrdenesDeVentaConFacturaConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual);
        List<OperacionDeVenta> ObtenerOperacionesConNotaDeCreditoConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int idTipoComprobanteReferencia);
        List<OperacionDeVenta> ObtenerOperacionesConNotaDeDebitoConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual, int idTipoComprobanteReferencia);
        List<MovimientoDeAlmacen> ObtenerGuiasDeRemisionConfirmadasEInvalidadas(int idEmpleado, DateTime fechaActual);

        List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSerie(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSerie(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerResumenDeComprobantePorSerieAnulada(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);

        List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobanteVentaPorSerieYConceptoBasicoConfirmado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);
        List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobantePorSerieYConceptoBasicoInvalidado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);
        List<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerComprobantePorSerieYConceptoBasicoAnulado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);


        #region VENTAS MASIVAS 
        OperationResult ConfirmarVentaMasiva(UserProfileSessionData sesionDeUsuario, VentaMasiva ventaMasiva);
        OperationResult ConfirmarVentasAlCreditoYCobranzasCarteraCliente(UserProfileSessionData sesionDeUsuario, VentaYCobranzaCarteraDeClientes ventasCobranzasMasiva);
        //List<Cuota> ObtenerCuotasConSaldo(int[] idsClientes);
        List<OrdenDeVenta> ObtenerVentasMasivasPorFecha(DateTime fecha);
        List<MovimientoEconomico> ObtenerCobrosMasivosPorFecha(DateTime fecha);
        List<Deuda_Actor_Negocio> ObtenerDeudasMasivasDeCarteraDeClientes(int idCentroDeAtencion, DateTime fecha);
        List<VentaYCobranzaCarteraDeClientes> ObtenerVentasYCobranzasMasivas(DateTime fechaDesde, DateTime fechaHasta);
        //VentaYCobranzaCarteraDeClientes ObtenerVentasYCobranzasMasiva(DateTime fechaDeVenta);
        VentaYCobranzaCarteraDeClientes ObtenerVentasYCobranzasMasiva(int idTransaccion);

        //OperationResult ReconstruirFacturasRechazadasEnFacturacionElectronica(long[] idDocumentos);

        EstadoCuentaCliente_VentaCobro ObtenerDeudasMasivasDeCarteraDeClientes(int idPuntoDeVenta, int idCliente, DateTime fechaDesde, DateTime fechaHasta);

        #endregion


        List<OperacionDeVenta> ObtenerOperacionesDeVentaConfirmadasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta);
        List<OperacionDeVenta> ObtenerOperacionesDeVentaAnuladasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta);
        List<OperacionDeVenta> ObtenerOperacionesDeVentaInvalidadasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta);
        EstadoDeCuenta ObtenerEstadoDeCuentaCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta);

        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeVenta ordenDeVenta);
        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeVenta ordenDeVenta, string host, List<LinkedResource> resources);
        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoDeAlmacen movimientoDeAlmacen);
        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoDeAlmacen movimientoDeAlmacen);
        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeMovimientoDeAlmacen ordenDeAlmacen);
        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeMovimientoDeAlmacen ordenDeAlmacen);
        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoEconomico ordenDeAlmacen);
        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, MovimientoEconomico ordenDeAlmacen);
        string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OperacionTipoTransaccionTipoComprobante operacion);
        string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OperacionTipoTransaccionTipoComprobante operacion, string host, List<LinkedResource> resources);
        List<DateTime> ObtenerFechaIncioyFinBasadoEnFechaActual();

        OperationResult GuardarNotaDeDebitoDeVenta(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario);
        //OperationResult InvalidarOperacionVenta(long idOrdenVenta, string observacion, UserProfileSessionData sesionUsuario);
        OperationResult DescuentoGlobalOperacionVenta(long idOrdenVenta, decimal importe, string observacion, int idEventoReferencia, UserProfileSessionData sesionUsuario);
        OperationResult AnularOperacionVenta(long idOrdenVenta, bool darDeBaja, string observacion, int idEventoReferencia, UserProfileSessionData sesionUsuario);
        OperationResult GuardarNotaDeCreditoDeVenta(long idOrdenDeOperacion, int idTipoNota, string motivo, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, string valorDeNota, int idEventoReferencia, List<DetalleOrdenDeNota> detalles, UserProfileSessionData sesionUsuario);
        OperationResult GuardarNotaVenta(long idOrdenVenta, int idTipoNota, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerie, int numeroComprobante, decimal montoNota, int idEventoReferencia, List<DetalleOrdenDeNota> detalles, bool esDebito, bool esDiferida, string observacion, DatosPago datosPago, UserProfileSessionData sesionUsuario);
        DateTime FechaActual();
        List<MovimientoEconomico> ObtenerCobranzasPorCliente(int idCliente, DateTime fechaDesde, DateTime fechaHasta);


        #region REPORTES DE VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO
        List<Resumen_Transaccion_Por_Modalidad> ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Transaccion_Por_Modalidad> ObtenerResumenesDeVentasPorModalidadYPorVendedorInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioConfirmadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Detalle_Transaccion_Por_Modalidad> ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaYPrecioUnitarioInvalidadas(string[] modalidades, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        #endregion


        #region REPORTES POR CONCEPTOS Y VENDEDOR

        List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorPrecioUnitarioPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoNegocioPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorConceptoBasicoPorVendedor(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDeVentasAgrupadasPorFamilia(DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDeVentasAgrupadasPorFamiliaYVendedor(DateTime fechaDesde, DateTime fechaHasta);
        List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> ObtenerResumenVentasPorConceptoPorVendedorContadoCredito(DateTime fechaDesde, DateTime fechaHasta);

        #endregion



        #region METODOS PARA REPORTE DE DEUDA Y PAGO PROVEEDOR Y CLIENTE

        List<Reporte_Deuda> ObtenerDeudasAProveedores(bool todosLosProveedores, int[] idsProveedores);
        List<Reporte_Deuda> ObtenerDeudasDeClientes(bool todosLosClientes, int[] idsClientes);

        List<Reporte_Pago> ObtenerPagosAProveedores(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, bool todosLosProveedores, int[] idsProveedores);
        List<Reporte_Pago> ObtenerPagosDeClientes(DateTime fechaDesde, DateTime fechaHasta, bool todasLasCajas, int[] idsCajas, bool todosLosClientes, int[] idsClientes);

        #endregion

        #region FECHAS PARA REPORTES
        List<string> ObtenerFechaIncioyFinConPrecisionDeMilisegundosParaReporteVentaPuntoDeVenta();
        #endregion

        OperationResult RegistrarCanjeDeComprobante(int idEmpleado, int idCentroAtencion, List<long> idsOrdenes, int idTipoComprobante, int idSerieComprobante, int numeroDeComprobante);

        List<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion);
        List<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion, int[] idsConceptosBasicos);
        List<Reporte_Puntos_Canjeados> ObtenerReporteDePuntosCanjeados(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion);
        List<Reporte_Puntos_Pendientes> ObtenerReporteDePuntosPendientes();

        List<ItemConGrupoOperacionComercial> ObtenerVentasConfirmadasPorCaracteristicaYConcepto(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idCaracteristica);
        List<ItemDetalladoOperacionComercial> ObtenerVentasDetalladasPorConceptoCaracteristicasModoPago(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta, int[] idsCaracteristicas);
        List<VentaConceptoCliente> ObtenerVentasPorFamiliaCaracteristica(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idFamilia, int idCaracteristica, int idValorCaracteristica);
        List<VentaConceptoCliente> ObtenerVentasPorCaracteristica(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idCaracteristica, int idValorCaracteristica);
        List<VentaConceptoCliente> ObtenerVentasPorConcepto(DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion, int idConcepto);
        List<ResumenDeTransaccionGeneral> ObtenerInvalidacionesDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta);
        List<ResumenDeTransaccionGeneral> ObtenerNotasCreditoDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta);
        List<ResumenDeTransaccionGeneral> ObtenerNotasDebitoDeOperacionesDeVenta(DateTime fechaDesde, DateTime fechaHasta, int idPuntoVenta);
    }
}

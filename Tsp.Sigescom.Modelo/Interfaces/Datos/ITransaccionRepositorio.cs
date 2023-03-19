using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;//
using Comprobante = Tsp.Sigescom.Modelo.Entidades.Comprobante;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface ITransaccionRepositorio : IRepositorioBase
    {
        #region crear
        /// <summary>
        /// Crea una transaccion 
        /// </summary>
        /// <param name="transaccion"></param>
        /// <returns></returns>
        OperationResult CrearTransaccion(Transaccion transaccion);
        OperationResult CrearTransaccionYModificarActorDeNegocio(Transaccion orden, Actor_negocio actorAModificar);
        OperationResult CrearTransaccionYCrearActorDeNegocio(Transaccion orden, Actor_negocio mesa);
        OperationResult CrearTransacciones(Transaccion transaccion, List<Transaccion> transacciones1);
        OperationResult ActualizarTransaccion(Transaccion transaccion);
        OperationResult ActualizarTransacciones(List<Transaccion> transacciones);
        OperationResult ActualizarTransaccionTransaccion1DetallesParametro(Transaccion transaccion);
        OperationResult CrearTransacciones(Transaccion transaccion, List<Transaccion> transacciones1, List<Transaccion> transacciones2);
        OperationResult CrearEstadoDeTransaccionAhora(Estado_transaccion estadoTransaccion);
        OperationResult CrearEstadoDeTransaccionAhoraActualizarTransaccion(Estado_transaccion estadoTransaccion, Transaccion transaccion);
        OperationResult CrearEstadoTransaccionActualizarActorNegocio(Estado_transaccion estadoTransaccion, Actor_negocio actorNegocio);
        OperationResult CrearEstadosDeTransaccionesAhora(List<Estado_transaccion> estadosDetransacciones);
        OperationResult CrearEstadosDeTransaccionesAhoraActualizarTransaccion(List<Estado_transaccion> estadosDeTransacciones, Transaccion transaccion);
        OperationResult CrearTransaccionYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias);
        OperationResult CrearTransaccionAgregarEstadoYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias, Estado_transaccion estado);
        OperationResult CrearEstadosMasivosDeTransacciones(List<Estado_transaccion> stadosDeTransacciones);
        OperationResult CrearEventoTransaccion(Evento_transaccion eventoTransaccion);
        OperationResult CrearEventoTransaccionInformacionTransaccion(Evento_transaccion eventoTransaccion, string informacionTransaccion);
        OperationResult CrearEventosTransacciones(List<Evento_transaccion> eventosTransacciones);
        OperationResult CrearEventosMasivosDeTransacciones(List<Evento_transaccion> eventosDeTransacciones);
        OperationResult CrearTransaccionAgregarEstadoYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias, Estado_transaccion estadoTransaccion, List<Estado_cuota> estadosCuotas);
        OperationResult CrearTipoDeCambio(DateTime fecha, int idMoneda, decimal compra, decimal venta);
        OperationResult ActualizarTipoDeCambio(Tipo_cambio tipo_cambio);
        OperationResult CrearTransacciones(List<Transaccion> transacciones);
        OperationResult ActualizarCoutas(List<Cuota> cuotas);
        OperationResult ActualizarTransaccionYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias);
        OperationResult CrearEstadoTransaccionYCrearEstadoCuota(List<Estado_transaccion> estadoTransacciones, List<Estado_cuota> estadoCuotas);
        OperationResult CrearTransaccionYCrearEstadoTransaccion(Transaccion transaccion, Estado_transaccion estadoTransaccion);
        OperationResult ActualizarYCrear(Transaccion transaccionAActualizar, Transaccion transaccionACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear);
        OperationResult CrearTransacionesYEstados(List<Transaccion> transaccionesACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear);
        OperationResult CrearTransacionYEstadoTransaccionYEstadosCuota(Transaccion transaccionACrear, Estado_transaccion estadoTransaccionACrear, List<Estado_cuota> estadosCuotasACrear);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int idUltimoEstado, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int[] idsEntidadInterna, int idTipoComprobante, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int[] idsEntidadInterna, DateTime fechaDesde, DateTime fechaHasta);
        //IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int idEstadoAnteriorOActual, int idEstadoActualSiExiste, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta, int idParametroDeTransaccion, string valorParametroDeTransaccion);
        Transaccion ObtenerTransaccionInclusiveActorDeNegocio1Transaccion11(long idTransaccion);
        PuntosDeCliente ObtenerPuntosDeCliente(DateTime fechaDesdeParaObtencionPuntos, int idCliente);
        IEnumerable<Transaccion> ObtenerTransaccionesParaCanjePuntos(DateTime fechaDesdeParaObtencionPuntos, int idCliente); 
        IEnumerable<Reporte_Puntos_Canjeados> ObtenerReportePuntosCanjeados(int idAccionDeNegocioMovimientoEnCaja, DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion);
        IEnumerable<Reporte_Puntos_Pendientes> ObtenerReportePuntosPendientes(DateTime fechaDesdeParaObtencionPuntos);
        IEnumerable<Entrada_Salida_Almacen> ObtenerEntradasOSalidasDeAlmacen(bool esEntrada, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Orden_Recibir_Entregar> ObtenerOrdenesPorRecibirOPorEntregarDeAlmacen(bool porRecibir, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta);
        #endregion
        #region consultas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idEstadoActualSiExiste"> es el estado actual(último) de la transaccion, en caso exista.</param>
        /// <param name="idEstadoAnteriorOActual">es el estado anterior de la transaccion (penultimo estado). Si el estado @idEstadoActualSiExiste existe, entonces @idEstadoAnteriorOActual es el anterior, de lo contrario es el Actual (ultimo)</param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="idSerie"></param>
        /// <param name="idMinimo"></param>
        /// <returns></returns>
        Transaccion_consolidada ObtenerResumenTransaccionesDespuesDe(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie, long idMinimo);

        Transaccion_consolidada ObtenerResumenTransaccionesEntre(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie, long idMinimo, long idMaximo);

        IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntre(int idTipoTransaccion, int idaUltimoEstado, int idSerie, long idMinimo, long idMaximo);

        IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntreNumeroDeComprobante(int idTipoTransaccion, int idUltimoEstado, int idSerie, long numeroMinimo, long numeroMaximo);
        IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntreNumeroDeComprobantefechaHasta(int idTipoTransaccion, int idUltimoEstado, int idSerie, long numeroMinimo, long numeroMaximo, DateTime fechaDesde, DateTime fechaHasta);
        Transaccion_consolidada ObtenerResumenTransaccionesAntesDe(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie, long idMaximo);

        Transaccion_consolidada ObtenerResumenTransacciones(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie);
        long ObtenerIdTipoTransaccion(long idOperacionVenta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEstadoQueDebeContener, DateTime fechaDesde, DateTime fechaHasta);



        IEnumerable<Transaccion> ObtenerTransacciones(long[] idsTransacciones);

        Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccion);

        Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccionConsultaComprobante(ConsultaComprobanteParameter consultaComprobante);
        List<Transaccion> ObtenerTransaccionesSegunOrigen_InclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionOrigen, int idTipoTransaccion);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionPadre, int idTipoTransaccion);

        IEnumerable<Transaccion> ObtenerTransacciones(long[] idsOperacionesDeVentas, int idTipoTransaccionVenta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Resumen_Compra> ObtenerResumenCompraPorTipoCompra(int[] idsTiposTransaccion, int[] idsTiposComprobante, int idEstado, int idParametroTransaccion, TipoOperacionCompra valorParametroTransaccion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta, int cantidadAObtener);

        IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransacciones(int[] idsTiposTransacciones, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idUltimoEstado);
        

        /// <summary>
        /// DEvuelve las transsacciones par alos tipos de comprobantes y estados indicados
        /// ademas se incluye como carga anciosa la transaccion de referencia y los conceptos de negocio
        /// </summary>
        /// <param name="idsTiposTransaccion"></param>
        /// <param name="idsTiposComprobantes"></param>
        /// <param name="idsEstados"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYConceptoNegocio(int[] idsTiposTransaccion,
            int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta);
        bool ExisteTransaccion(int idTipoTransaccion, int idActorNegocioInterno, int idActorNegocioExterno1, int idEstadoActual);


        IEnumerable<Venta_Cliente> ObtenerVentasCliente(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Venta_Cliente> ObtenerVentasClienteConOperacionDeReferenciaSegunElUltimoEstado(int[] idsTiposComprobantes, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);

        /// <summary>
        /// Retorna las ventas segun el esado que debe de tener
        /// </summary>
        /// <param name="idsTiposComprobantes"></param>
        /// <param name="idsTiposTransaccion"></param>
        /// <param name="idEstadoQueDebeTener"></param>
        /// <param name="idEstadoActualNoPosible"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        IEnumerable<Venta_Cliente> ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta);


        IEnumerable<Venta_Cliente> ObtenerVentasClienteSegunElEstadoQueDebeTener(int[] idsTiposComprobantes, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);

            IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoSinConcepto(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<ReporteVentaDetalladoSinConcepto> ObtenerReporteVentaDetalladoSinConcepto(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion,
            int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion,
            int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta, int idCliente);
        IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta, int idCliente);
        IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, string comprobante);
        IEnumerable<long> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEstado);




         
        /// <summary>
        /// Obtener ultima transaccion segun id_actor_negocio_interno y id_tipo_transaccion
        /// </summary>
        /// <param name="idActorNegocioInterno"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        Transaccion ObtenerUltimaTransaccion(int idActorNegocioInterno, int idTipoTransaccion);

        long[] ObtenerIdsDeTransaccionesPosteriores(int idActorNegocioInterno, int idTipoTransaccion, DateTime fecha);

        /// <summary>
        /// Obtiene la ultima transaccion segun el id_actor_negocio vigente y segun el tipo de transaccion antes de la fechaInicio
        /// </summary>
        /// <param name="idActorNegocioInterno"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="fechaInicio"></param>
        /// <returns></returns>
        Transaccion ObtenerUltimaTransaccionAntesDe(int idActorNegocioInterno, int idTipoTransaccion, DateTime fechaInicio);
        Transaccion ObtenerUltimaTransaccionAntesDe(int idTipoTransaccion, DateTime fechaInicio);
        /// <summary>
        /// Devuelva un detalle transaccion segun el id concepto negocio de la ultima transaccion segun el tipo de transaccion
        /// </summary>
        /// <param name="idActorNegocioInterno"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idConceptoNegocio"></param>
        /// <returns></returns>
        Detalle_transaccion ObtenerDetalleTransaccionPorConceptoNegocio(int idActorNegocioInterno, int idTipoTransaccion, int idConceptoNegocio);


        /// <summary>
        /// Devuelve la ultima transaccion vigente segun su tipo de transaccion
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        Transaccion ObtenerUltimaTransaccion(int idTipoTransaccion);


        //Asegurar que la existencia del concepto coincida con el stock deteerminado en el inventario lógico

        /// <summary>
        /// Devuelve el tipo de cambio en la fecha dada
        /// </summary>
        /// <param name="fechaActual"></param>
        /// <returns></returns>
        Tipo_cambio ObtenerTipoDeCambio(DateTime fecha);

        /// <summary>
        /// Retorna una coleccion de transacciones, segun el tipo de transaccion y el rango de fechas ingresados
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenTransaccionesSerieYConcepto(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);// y66  cod:XY5
        IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesPorSerieYConeptoBasico(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);// y55 cod:XY5

        IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idCentroAtencion, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);
        IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idCentroAtencion, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);// XY6.3


        IEnumerable<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerTransaccionesAgrupadasPorSerie(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSerie_(int[] idsPuntosDeVentas, int[] idTipoTransaccion, int idEstadoQueDebeTener, DateTime fechaDesde, DateTime fechaHasta);// a11

        IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario_(int[] idsPuntosDeVentas, int[] idTipoTransaccion, int idEstadoQueDebeTener, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSerie(int[] idsPuntosDeVentas, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//a12

        IEnumerable<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);
        IEnumerable<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(int[] idsPuntosDeVentas, int[] idTipoTransaccion, int idEstadoQueDebeContener, DateTime fechaDesde, DateTime fechaHasta);//a13.2

        IEnumerable<Resumen_Transaccion_Consolidado> ObtenerResumenTransacciones(int[] idsTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idAccionNegocio);//cod:XY7  Y77

        IEnumerable<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerResumenTransaccionesDeGastosPorConcepto(int idTransaccion, int idEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerResumenTransaccionesDeGastosPorConcepto(int idTransaccion, int idEstado, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        Transaccion ObtenerTransaccion(long id);
        Detalle_transaccion ObtenerDetalleTransaccion(long id);
        Transaccion ObtenerTransaccionInclusiveEstadoTransaccionDetalleMaestro(long id);
        Transaccion ObtenerTransaccionInclusiveEstadoTransaccionYDetalleTransaccion(long id);
        IEnumerable<Transaccion> ObtenerTransacciones1DeTransaccionInclusiveEstadoTransaccionDetalleMaestro(long id);
        IEnumerable<Transaccion> ObtenerTransacciones11DeTransaccion(long id);
        IEnumerable<Transaccion> ObtenerTransacciones11DeTransaccion3DeTransaccion(long id);
        IEnumerable<Transaccion> ObtenerTransacciones11DeTransacciones(long[] ids);
        Transaccion ObtenerTransaccionPadre(long id);
        Transaccion ObtenerTransaccionInclusiveEstados(long id);
        Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(long id);

        //Transaccion generarTransaccionEntradaSalidaBienes();//InventarioLogicoOmar

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int idTipoTransaccion, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado, int idSerie);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(int idSerieComprobante, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);

        IEnumerable<ResumenDeTransaccionVenta> ObtenerResumenDeTransaccion(int idSerieComprobante, int[] idsTiposTransaccion, int[] idEstadosPosibles, int idEstidEstadoAIgnorar, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(int idSerieComprobante, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, int idEntidadInterna, DateTime date, DateTime dateTime);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int puntoDeVenta, int idTipoTransaccion, DateTime date, int idEstadoConfirmado, DateTime datetime); //y45 ;47
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int puntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime date, DateTime dateTime); //y46
        //IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados_(int idPuntoDeVenta, int idTipoTransaccion, int idEstadoActualSiExiste, int idEstadoAnteriorOActual, DateTime fechaDesde, DateTime fechaHasta);//  XY4.1     XY4.2 
        //IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados_(int idPuntoDeVenta, int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta);//anulado XY4.3 

        IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstados(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//invalidos cod.XY5.3 //corfirmados cod.XY5.1 

        IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstados(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//invalidos  cod.XY5.4 //corfirmados cod.XY5.2 

        IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//anulado XY5.5 

        IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//anulado XY 5.6

        IEnumerable<ResumenTransaccionPorVendedor> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);//a14.1 a14.2 

        IEnumerable<ResumenTransaccionPorVendedor> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idEmpleado, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//a14.3 anulado

        IEnumerable<DetalleTransaccionPorVendedor> ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);//a14.4   a14.5 

        IEnumerable<DetalleTransaccionPorVendedor> ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//anuladas a14.6 

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int[] idEstado, int idEntidadInterna, DateTime date, DateTime dateTime);//Y33

        // c1 reporte de venta de concepto por vendedor -> ir a implementacion
        IEnumerable<Resumen_transaccion_Venta_PorConcepto> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(int idEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado); //conirmado C1.1  anulado c1.2

        IEnumerable<Resumen_transaccion_Venta_PorConcepto> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(int idEmpleado, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta);//c1.3 anulado

        // c2 reporte de venta de concepto por vendedor - administrador 
        //IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedorAdministrador(int idTipoTransaccion, int[] idEstado, int idEntidadInterna, DateTime date, DateTime dateTime); //C2
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, int[] idsEntidadInterna, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int[] idsEntidadInterna, DateTime fechaDesde, int idUltimoEstado, DateTime fechaHasta); //y22

        IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(int[] idPuntoDeVenta, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado);

        IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(int[] idsPuntoDeVenta, int[] idsTiposTransaccion, int idEstadoQueDebeContener, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorVendedor(int[] idsTransaccion, int idEstadoActual, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorVendedores(int[] idsTransaccion, int idEstadoActual, int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> ObtenerConsolidadoPorVendedoresPorModoPagoPorConcepto(int[] idsTransaccion, int idEstadoActual, int[] idsEmpleado, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta);
        Serie_comprobante ObtenerPrimeraSerieDeComprobanteAutonumerable(int idDetalleMaestroComprobanteBoleta, int idPuntoDeVenta);
        bool ExisteSerieDeComprobanteSegunTipoDeComprobante(int idTipoComprobante, string numeroDeSerie);

        IEnumerable<Cobro_Pago> ObtenerCobrosOPagos(bool esCobro, DateTime fechaDesde, DateTime fechaHasta);


        /// <summary>
        /// Devuelve el maximo numero para el codigo de una cuota 
        /// </summary>
        /// <param name="empiezaEn"></param>
        /// <returns></returns>
        int ObtenerMaximoCodigoCuota(string empiezaEn);

        /// <summary>
        /// devuelve una collecion de tipo de comprobante por transaccion
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        Task<IEnumerable<Tipo_transaccion_tipo_comprobante>> ObtenerTipoComprobantePorTipoDeTransaccion(int idTipoTransaccion);

        Task<IEnumerable<Tipo_transaccion_tipo_comprobante>> ObtenerTipoComprobantePorTipoDeTransaccion(int[] idsTipoTransaccion);


        /// <summary>
        /// devuelve una coleccion de cuotas
        /// </summary>
        /// <param name="idEstado"></param>
        /// <param name="porCobrar"></param>
        /// <param name="vencimientoDesde"></param>
        /// <param name="vencimientoHasta"></param>
        /// <returns></returns>
        //IEnumerable<Cuota> ObtenerCuotas(int idEstado, bool porCobrar, DateTime vencimientoDesde, DateTime vencimientoHasta);


        IEnumerable<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarOPagar(bool porCobra);
        IEnumerable<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarOPagarPorGrupos(bool porCobrar, int?[] idsGrupos);

        /// <summary>
        /// valida el comprobante en compras
        /// </summary>
        /// <param name="idActorNegocio"></param>
        /// <param name="idTipoComprobante"></param>
        /// <param name="numeroDeSerie"></param>
        /// <param name="numeroComprobante"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        bool ExisteComprobante(int idActorNegocio, int idTipoComprobante, string numeroDeSerie, int numeroComprobante, int idTipoTransaccion, int idEstadoActual);
        OperationResult CrearTransaccionesYActualizarExistenciasYCuotas(Transaccion ventasAGuardar, List<Existencia> existencias, List<Cuota> cuotas);
        OperationResult CrearEstadoTransaccionActualizarCuotas(Estado_transaccion estadoTransaccion, List<Cuota> cuotas);
        OperationResult CrearTransaccionYActualizarParametroTransaccion(Transaccion transaccion, Parametro_transaccion parametroTransaccion);
        OperationResult ActualizarParametroTransaccion(Parametro_transaccion parametroTransaccion);
        List<Existencia> ObtenerExistencias(int[] v, int idAreaInterna);
        Existencia ObtenerExistencia(int idConceptoNegocio, int idPuntoAtencion);
        Detalle_transaccion ObtenerDetalleTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idEstadoTransaccion, int idConceptoNegocio, string loteConceptoNegocio);
        List<Detalle_maestro> ObtenerTipoComprobante(int idMaestro);
        List<Tipo_transaccion_tipo_comprobante> obtenerTipoComprobantePorTipoDeTransaccion(int idTipoTransaccion, int idTipoComprobante);
        List<Tipo_cambio> ObtenerTipoDeCambio(DateTime desde, DateTime hasta);
        Tipo_cambio ObtenerTipoDeCambioPorFecha(DateTime fecha);
        Tipo_cambio ObtenerTipoDeCambio();
        List<Cuota> ObtenerCuotas(int[] idsCuotas);
        Cuota ObtenerCuota(int idCuota);
        Cuota ObtenerCuota(long idCuota);
        Comprobante ObtenerComprobanteCero(int idSerieComprobante);
        Comprobante ObtenerComprobanteDeTransaccion(long idTransaccion);
        Cuota ObtenerCuotaIncluidoOperacion(long idCuota);
        #endregion

        void MarcarSerieComoModificada(Serie_comprobante serie);

        #region obtenerCuotas
        IEnumerable<Cuota> ObtenerCuotasConSaldo(bool porCobrar, int[] idsTiposTransaccion);//cod xy10




        /// <summary>
        /// 
        /// </summary>
        /// <param name="porCobrar"></param>
        /// <param name="idDetalleMaestroEstadoConfirmado"></param>
        /// <param name="idsActoresDeNegocioExternos">actores de la transaccion</param>
        /// <param name="idTipoTransaccionOrdenDeVenta"></param>
        /// <returns></returns>
        IEnumerable<Cuota> ObtenerCuotasConSaldo(bool porCobrar, int idEstado, int[] idsActoresDeNegocioExternos, int idTipoTransaccion);


        #endregion

        #region Crear, Obtener, Actualizar (Serie_Comprobante)

        OperationResult CrearSerieComprobante(Serie_comprobante serie);
        IEnumerable<Serie_comprobante> ObtenerSeriesComprobante();
        /// <summary>
        /// Devuelve la serie del comprobante con su id
        /// </summary>
        /// <param name="idSerieDeComprobante"></param>
        /// <returns></returns>
        Serie_comprobante ObtenerSerieDeComprobante(int idSerieDeComprobante);
        /// <summary>
        /// Devuelve la serie del comprobante par el centro de atencion y el tipo de comprobante
        /// </summary>
        /// <param name="idSerieDeComprobante"></param>
        /// <returns></returns>
        Serie_comprobante ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(int idTipoComprobante, int IdCentroAtencion);
        Serie_comprobante ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie(int idTipoComprobante, int idCentroAtencion, string prefijoSerie);
        OperationResult ActualizarSerieComprobante(Serie_comprobante serie);

        #endregion

        #region Crear, Obtener, Actualizar (Tipo_Transaccion)
        OperationResult CrearTipoTransaccion(Tipo_transaccion tipoTransaccion);
        IEnumerable<Tipo_transaccion> ObtenerTiposDeTransaccionIncluidoAccionDeNegocio();
        Tipo_transaccion ObtenerTipoDeTransaccionIncluidoAccionDeNegocio(int idTipoDeTransaccion);
        OperationResult ActualizarTipoTransaccionConAccionNegocio(Tipo_transaccion tipoTransaccion);
        IEnumerable<ItemGenerico> ObtenerTipoDeTransaccionPorAccionDeNegocio(int idAccionNegocio, bool valor);


        #endregion

        #region Obtener (Accion_Negocio)
        IEnumerable<Accion_de_negocio> ObtenerAccionesDeNegocio();
        Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionPadre, long idTipoTransaccion);
        IEnumerable<Transaccion_consolidada> ObtenerResumenTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion_consolidada> ObtenerResumenTransacciones(int idTipoTransaccion, int idTipoComprobante, int idUltimoEstado, long idMinimo, long idMaximo);
        long ObtenerUnicoIdTransaccion(long idTransaccionPadre, int idTipoTransaccionADevolver);
        long ObtenerUnicoIdTransaccion(long idTransaccionPadre, int idTipoTransaccionADevolver, int idUltimoEstado);
        /// <summary>
        /// Devuelve el id de las series de comprobantes
        /// </summary>
        /// <param name="idsTipoComprobante"></param>
        /// <param name="estado">true: activas, false: inactivas</param>
        /// <returns></returns>
        IEnumerable<int> ObtenerIdsSeriesComprobantes(int[] idsTipoComprobante, bool estado);
        /// <summary>
        /// Devuele los ids series de comprobante activas o no
        /// </summary>
        /// <param name="idsTipoComprobante"></param>
        /// <returns></returns>
        IEnumerable<int> ObtenerIdsSeriesComprobantes(int[] idsTipoComprobante);

        IEnumerable<long> ObtenerIdTransacciones(int idTipoTransaccion, int[] idsTiposComprobantes, int[] idsEstados);
        IEnumerable<long> ObtenerIdTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEstado);

        long ObtenerIdTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado);

        long ObtenerIdTransaccion(int idActorNegocioInterno, int idTipoTransaccion);

        long ObtenerIdPrimeraTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idEstadoActualSiExiste, int idEstadoAnteriorOActual, DateTime fechaDesde, DateTime fechaHasta, int idSerie);

        long ObtenerIdUltimaTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idEstadoActualSiExiste, int idEstadoAnteriorOActual, DateTime fechaDesde, DateTime fechaHasta, int idSerie);


        long ObtenerNumeroDeComprobantePrimeraTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);
        long ObtenerNumeroDeComprobanteUltimaTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie);

        //Transaccion_consolidada ObtenerResumenTransaccionesEntre(int idTipoTransaccion,  int idEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie, long idMinimo, long idMaximo);



        #endregion

 


        // IEnumerable<Cuota> ObtenerCuotasPorTipoDeVinculoActorNegocio(int idActorDeNegocioPrincipal, int tipoVinculo, bool porCobrar, int idEstado);

        IEnumerable<Deuda_Actor_Negocio> ObtenerDeudasActorNegocioPorVinculoActorNegocio(int idTipoDeTransaccion, int idActorNegocioPrincipal, int tipoDeVinculo, DateTime fecha, int idParametroComprobantePredeterminado);



       


        DateTime? ObtenerFechaPrimeraTransaccion(int idEntidadInterna);
        DateTime? ObtenerFechaPrimeraTransaccion();

        DateTime? ObtenerFechaPrimeraTransaccionGenerica(int idTipoTransaccionGenerica);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fechaMaxima"></param>
        /// <param name="idDetalleMaestroParametro"></param>
        /// <param name="ventaMasiva"></param>
        /// <returns></returns>
        DateTime? ObtenerFechaInicioUltimaTransaccion(int idActorNegocioInterno, int idTipoDeTransaccion, int idUltimoEstado, DateTime fechaMaxima, int idDetalleMaestroParametroTransaccion, string valorParametroTransaccion);

        DateTime? ObtenerFechaUltimaTransaccionDeAlgunoDeLosActoresVinculadosSegunTransaccion1(int idActorNEgocioPrincipal, int idTipoTransaccionCobranzaFacturasClientes, DateTime dateTime);

        OperationResult CrearTransaccionesYActualizarExistencias(List<Transaccion> transacciones, List<Existencia> existencias);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fecha);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, int idTransaccion);

        Transaccion ObtenerTransaccionConCuotasInclusiveTransaccion2ConComprobante(long id);

        IEnumerable<Transaccion> ObtenerTransaccionesPorActorDeNegocio(int idTipoDeTransaccion, int idActorDeNegocio, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoFiltradoPorEmpleadoYActorInterno(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta);
        Deuda_Actor_Negocio ObtenerDeudaActorNegocio( int idActorNegocio, DateTime fecha);
        Transaccion ObtenerPrimeraTransaccion(int idActorNegocio, int idTipoTransaccion);
        DateTime? ObtenerFechaInicioUltimaTransaccion(int idActorNegocioExterno, int idTipoDeTransaccion, int idUltimoEstado, DateTime fechaMaxima);

        DateTime? ObtenerFechaUltimaTransaccionActorNegocioExterno(int idActorNegocio, int idTipoTransaccionCobranzaFacturasClientes, DateTime fechaMaxima);
        IEnumerable<Tipo_transaccion_tipo_comprobante> ObtenerTipoComprobantePorTipoDeComprobante(int idTipoComprobante);
        IEnumerable<Tipo_transaccion_tipo_comprobante> ObtenerTipoComprobantePorTipoDeComprobante(int[] idsTipoComprobante);
        #region VENTAS Y COBROS MASIVOS
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<DetalleTransaccionVentaCobro> ObtenerDetallesEstadoCuentaClienteVenta(int idTransaccion, int idEstadoActual, int idActor, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<DetalleTransaccionVentaCobro> ObtenerDetallesEstadoCuentaClienteCobro(int idTransaccion, int idActor, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionPorActorDeNegocio(int idTipoTransaccion, int idEstadoActual, int idActorNegocio, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fecha);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fechaDesde, DateTime fechaHasta);

        IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, int idTransaccion);
        #endregion



        #region    VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO Y PRECIO UNITARIO
        IEnumerable<Resumen_Transaccion_Por_Modalidad> ObtenerResumenTransaccionesInclusiveParametroTransaccionActoresYDetalleMaestroYEstado(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros);

        IEnumerable<Detalle_Transaccion_Por_Modalidad> ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocio(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros);

        IEnumerable<Detalle_Transaccion_Por_Modalidad> ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocioYPrecioUnitario(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros);
        #endregion



        #region REPORTE DEUDA Y PAGO
        IEnumerable<Reporte_Deuda> ObtenerDeudas(bool porCobrar, int[] idsTiposTransaccion, int[] idsActorNegocioExterno);
        IEnumerable<Reporte_Deuda> ObtenerDeudas(bool porCobrar, int[] idsTiposTransaccion);

        IEnumerable<Reporte_Pago> ObtenerPagos(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioInterno, int[] idsActorNegocioExterno);
        IEnumerable<Reporte_Pago> ObtenerPagosExterno(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioExterno);
        IEnumerable<Reporte_Pago> ObtenerPagosInterno(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioInterno);
        IEnumerable<Reporte_Pago> ObtenerPagos(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion);

        #endregion



        #region REPORTE DE CONCEPTO BASICO
        IEnumerable<Reporte_Concepto_Basico> ObtenerReportePorConceptoBasico(DateTime fechaDesde, DateTime fechaHasta, int idConceptoBasico, int[] idsTiposTransaccion, int[] idsTiposComprobante, int[] idsActorNegocioInterno);
        #endregion

        #region DETALLES DE TRANSACCION 
        OperationResult ActualizarDetallesTransaccion(List<Detalle_transaccion> detallesTransaccion);

        OperationResult CrearActualizarDetallesTransaccion(List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar);

        #endregion

        #region INVENTARIO FISICO


        int ObtenerNumeroDeTransaccionesExistentes(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado);
        OperationResult CrearTransaccionActualizarTransaccionCrearEstadoTransaccion(Transaccion transaccionCrear, Transaccion transaccionActualizar, Estado_transaccion estadoTransaccionCrear);
        OperationResult CrearTransaccionCrearActualizarDetallesTransaccion(Transaccion transaccion, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, Transaccion transaccionActualizar);
        OperationResult CrearTransaccionesCrearEstadosTransaccionCrearEstadosCuotaCrearActualizarDetallesTransaccion(List<Transaccion> transaccionesParaCrear, List<Estado_transaccion> estadosDeTransaccionParaCrear, List<Estado_cuota> estadosDeCuotaParaCrear, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, List<Actor_negocio> actoresNegocioParaActualizar);
        OperationResult CrearTransaccionCrearActualizarDetallesTransaccionCrearEstadoTransaccionCrearEstadosCuota(Transaccion transaccion, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, Estado_transaccion estadoDeTransaccion, List<Estado_cuota> estadosDeCuota);

        OperationResult CrearTransaccionActualizarDetalleTransaccionExistente(Transaccion transaccion, List<Detalle_transaccion> updDetallesTransaccion, int idActorNegocioInternoTransaccionExistenteOrigen, int idTipoTransaccionExistenteOrigen, int idUltimoEstadoTransaccionExsitenteOrigen, int idActorNegocioInternoTransaccionExistenteDestino, int idTipoTransaccionExistenteDestino, int idUltimoEstadoTransaccionExsitenteDestino);

        OperationResult CrearTransacionesYEstados(List<Transaccion> transaccionesACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear, List<Detalle_transaccion> updDetallesTransaccion, int idActorNegocioInternoTransaccionExistente, int idTipoTransaccionExistente, int idUltimoEstadoTransaccionExsitente, bool debeAumentarCantidad);
        OperationResult RegistrarTransacciones(RegistroTransacciones registro);

        IEnumerable<Detalle_transaccion> ObtenerDetallesTransaccion(long idTransaccion, IEnumerable<int> idsConceptoNegocio);
        IEnumerable<Detalle_transaccion> ObtenerDetallesTransaccion(long idTransaccion);
        int ObtenerNumeroDetallesDeTransaccionConCantidadMayorA0(long idConceptoBasico, int idTipoTransaccion);
        #endregion

        #region OBTENER IDS DE TRANSACCIONES PARA LA EMISION MASIVA DE NOTAS DE CREDITO 

        IEnumerable<long> ObtenerIdsTransacciones(int idSerie, decimal montoMinimo, decimal montoMaximo, DateTime desde, DateTime hasta, int idCliente, int idTipoTransaccion, int idEstado);
        IEnumerable<MontoNotaDeCredito> ObtenerMontoAEmitirNotaCredito(int idSerieB002, int idSerieB006, int idSerieB007, decimal montoMinimo, decimal montoMaximo, DateTime desdeB002, DateTime hastaB002, DateTime desdeB006, DateTime hastaB006, DateTime desdeB007, DateTime hastaB007, int idCliente, int idTipoTransaccion, int idEstado);

        #endregion
        IEnumerable<Transaccion> ObtenerTransaccionesPorTipoYContenganIdTransaccionPadre(long[] idsTransaccionesPadre, int idTipoTransaccion);
        IEnumerable<long> ObtenerIdsGuiaRemisionPorEnviar(DateTime fechaDesde, DateTime fechaHasta);

        #region REPORTE DE UTILIDAD DE VENTAS
        IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsConceptoBasico, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsCentroAtencion, int[] idsConceptoBasico, DateTime fechaDesde, DateTime fechaHasta);
        #endregion

        IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<ItemConGrupoOperacionComercial> ObtenerItemOperacionPorCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idCaracteristica);
        IEnumerable<ItemDetalladoOperacionComercial> ObtenerItemsDetalladoDeVentaConMedioPago(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsCaracteristicas, int idPuntoVenta);
        Transaccion ObtenerTransaccionDeUltimoComprobante(int idSerieComprobante);
        IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorFamiliaCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idFamilia, int idCaracteristica, int idValorCaracteristica);
        IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idCaracteristica, int idValorCaracteristica);
        IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorConcepto(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idConcepto);
        List<Periodo> ObtenerPeriodos();
        Periodo ObtenerPeriodo(int idPeriodo);
        Periodo ObtenerPeriodo(string nombrePeriodo);
        IEnumerable<ResumenDeTransaccionGeneral> ObtenerResumenDeTransacciones(int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion);
        IEnumerable<ResumenDeTransaccionGeneral> ObtenerResumenDeTransacciones(int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion, int idParametroParaTipoOperacion);

        string ObtenerUltimoRegistroDeDetalleTransaccionDeTransaccionOrdenVentaDeUnCliente(int idCliente);
        IEnumerable<DetalleCuotaPago> ObtenerDetallesCuotaPagoDeOperacion(long idOperacion);
        OperacionTipoTransaccionTipoComprobante ObtenerTipoTransaccionTipoComprobanteOperacion(long idOperacion);
    }

}

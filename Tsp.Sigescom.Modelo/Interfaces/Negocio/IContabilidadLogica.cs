
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{

    public interface IContabilidadLogica
    {
        List<Operacion> obtenerRegistroDeVentas();
        //OperationResult guardarSerieDeComprobante(int idTipoDeComprobante, string numeroSerie, int idSede, bool autonumerico, string numeroSiguiente, bool vigente);
       // OperationResult actualizarSerieDeComprobante(int id, int idTipoDeComprobante, string numeroSerie, int idSede, bool autonumerico, string numeroSiguiente, bool vigente);
        List<TipoDeComprobanteParaTransaccion> obtenerComprobantesParaCompra(int idTipoSubTransaccion);
        List<TipoDeComprobanteParaTransaccion> obtenerComprobantesParaVenta(int idTipoSubTransaccion);
        List<TipoDeComprobanteParaTransaccion> obtenerComprobantesParaOrdenVenta();
        List<Detalle_maestro> obtenerComprobantes();
        //List<ConceptoComercial> obtenerConceptosContablesParaVenta();
        //List<ConceptoComercial> obtenerConceptosContablesParaCompra();       
        List<SerieDeComprobante> obtenerSeriesDeComprobante();
        Tipo_cambio obtenerTipoDeCambioActual();




        OperationResult crearTipoDeCambio(DateTime fecha, decimal compra, decimal venta);

        /// <summary>
        /// solo se puede actualizar el tipo de cambio correspondiente a a fecha actual.
        /// </summary>
        /// <param name="compra"></param>
        /// <param name="venta"></param>
        /// <returns></returns>
       // OperationResult actualizarTipoDeCambio(int id, DateTime fecha,decimal compra, decimal venta);


        //List<Tipo_cambio> obtenerTipoDeCambio(DateTime desde, DateTime hasta);
        //Tipo_cambio obtenerTipoDeCambio(DateTime desde);
        //Tipo_cambio obtenerTipoDeCambioActual();
        decimal obtenerTipoDeCambioCompraActualOPorDefecto();
        Tipo_cambio obtenerTipoDeCambioPorFecha(DateTime fecha);
        decimal obtenerTipoDeCambioCompraPorFechaOPorDefecto(DateTime fecha);
        //List<ConceptoComercial> obtenerConceptosContablesParaCompraAdministrativa();
        OperationResult actualizarSerieDeComprobante(int id, int idTipoDeComprobante, string numeroSerie, int idSede, bool autonumerico, int numeroSiguiente, bool vigente);
        OperationResult guardarSerieDeComprobante(int idTipoDeComprobante, string numeroSerie, int idSede, bool autonumerico, int numeroSiguiente, bool vigente);
        //OperationResult actualizarTipoDeCambio(int id, DateTime fecha, decimal compra, decimal venta);
    }

}

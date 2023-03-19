using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IRestauranteRepositorio: IRepositorioBase
    {
        OperationResult CalcularAtencion(long idAtencion);

        //IEnumerable<Complemento> ObtenerComplementosIncluidoDetalles();
        IEnumerable<Complemento> ObtenerComplementosPorFamilia(int idFamilia);
        Transaccion ObtenerTransaccionDeAtencionDeMesaOcupada(int idMesa);
        IEnumerable<Complemento> ObtenerComplementos();
        OperationResult ActualizarComplemento(Complemento complemento);
        IEnumerable<DetalleOrden> ObtenerDetallesDeOrden(int idOrden);
        Task<IEnumerable<ItemRestaurante>> ObtenerItemsDeRestaurante();
        Task<IEnumerable<ItemJerarquico>> ObtenerDetallesJerarquicos(int idMaestro);
        IEnumerable<ItemRestaurante> ObtenerItemsDeRestaurantePorCategorias(List<int> idsCategoria);
        ItemRestaurante ObtenerItemDeRestauranteConPrecio(int idItem);
        Orden_Atencion ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(long idOrden);
        IEnumerable<Orden_Atencion> ObtenerOrdenesDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(long id);
        IEnumerable<Familia> ObtenerFamiliasSuperficial();
        OperationResult ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(int idDetalleDeOrden, EstadoDeDetalleDeOrden estado);
        IEnumerable<AtencionRestaurante> ObtenerAtencionesPorEstado(int IdEstado);
        IEnumerable<AtencionSinMesa> ObtenerAtencionesSinMesaPorEstados(int[] idsEstados, int idCentroAtencion);
        OperationResult ActualizarEstadosDeDetallesDeOrdenes(long[] IdsDetallesDeOrdenes, EstadoDeDetalleDeOrden nuevoEstado);
        OperationResult ActualizarEstadosDeDetallesDeOrden_CalcularOrden_CalcularAtencion(long idOrden, EstadoDeDetalleDeOrden nuevoEstado);

        IEnumerable<Orden_Atencion> ObtenerOrdenesPorEstado(int NumEstado);
        IEnumerable<Orden_Atencion> ObtenerOrdenesPorEstadoDeUnAmbiente(int NumEstado, int IdAmbiente);
        OperationResult ActualizarEstadoDeOrdenes(long[] ids, int estado);
        long[] ObtenerIdsDeItemsDeRestaurantePorCategoria(int idCategoria);
        IEnumerable<Concepto_negocio> ObtenerConceptoDeNegocioIncluyendoDetallesDeTransaccion();
        IEnumerable<ResumenOrden_Consulta> ObtenerReporteOrdenesIncluyendoMozo(DateTime desde, DateTime hasta);
        IEnumerable<ResumenOrdenesPorMozo_Consulta> ObtenerReporteMozoIncluyendoCantidadDeOrdenes(DateTime desde, DateTime hasta);
        IEnumerable<ItemRestaurante_Consulta> ObtenerOrdenesInclusiveItemsYDetallesDeOrden(DateTime desde, DateTime hasta);
        IEnumerable<DetalleOrden_Consulta> ObtenerOrdenesDetalladasIncluyendoMozoyDetalleDeOrden(DateTime desde, DateTime hasta, EstadoDeDetalleDeOrden estadoDeDetalle);

        IEnumerable<ResumenAtencion> ObtenerResumenDeAtencionesPorEstado(DateTime fechaDesde, DateTime fechaHasta, int estadoCerradoId, int[] idsCentrosDeAtencion);
        AtencionRestaurante ObtenerAtencionEspecifica(long id);
        IEnumerable<long> ObtenerIdsTransaccionDeTransaccionReferencia(long idTransaccion);
        OperationResult ActualizarDetalleDeDetalleTransaccion(long idDetalle, string detalleString);
        IEnumerable<OrdenAtencion_Consulta> ObtenerOrdenesAtencion(DateTime desde, DateTime hasta, int idEstablecimiento);
        IEnumerable<OrdenPorModoAtencion_Consulta> ObtenerOrdenesPorModoAtencion(DateTime desde, DateTime hasta, int idEstablecimiento);
        Orden_Atencion ObtenerOrdenDeAtencionDeDetalleOrden(long idDetalleOrden);
        Orden_Atencion ObtenerOrdenDeAtencion(long idOrden);
    }
}

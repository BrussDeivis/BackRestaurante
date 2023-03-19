using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Negocio.Restaurant
{
    public interface IRestauranteLogica
    {
        Task<List<ItemJerarquico>> ObtenerCategorias();
        List<ItemJerarquico> ObtenerCategoriasRestaurante();
        Task<List<Mesa>> ObtenerMesasDeAmbiente(int idAmbiente);
        Task<List<Ambiente>> ObtenerAmbientes();
        Task<List<Ambiente>> ObtenerAmbientes(int IdEstablecimiento);
        List<CentroAtencionRestaurante> ObtenerPuntoDeVeliveryPuntoAlPasoVigentes(int IdEstablecimiento);
        AtencionRestaurante ObtenerAtencionDeMesa(int mesaId);
        List<Complemento> ObtenerComplementosDeFamilia(int idFamilia);
        List<DetalleOrden> ObtenerDetallesDeOrden(int idOrden);
        Task<OperationResult> CrearAmbiente(Ambiente ambiente);
        Task<OperationResult> ActualizarAmbiente(Ambiente ambiente);
        Task<OperationResult> EliminarAmbiente(int idAmbiente);
        OperationResult CrearMesa(Mesa mesa);
        Task <List<ItemRestaurante>> ObtenerItemsDeRestaurante();
        OperationResult ActualizarMesa(Mesa mesa);
        OperationResult EliminarMesa(int idMesa);
        Task<List<ItemRestaurante>> ObtenerItemsDeRestaurantePorCategoria(int idCategoria);
        Task<SesionRestaurante> ObtenerSesion(UserProfileSessionData userProfileSessionData);
        OperationResult AgregarOrdenDeAtencion(Orden_Atencion orden, SesionRestaurante sesion);
        OperationResult CrearAtencionConOrden(AtencionRestaurante atencion, SesionRestaurante sesion);
        Task<List<ItemGenerico>> ObtenerMozosVigentes();
        //OperationResult CambiarEstadoDeDetalleDeOrden(int idDetalleDeOrden, int estado);
        OperationResult FinalizarAtencionCocina(long idAtencion);
        List<AtencionRestaurante> ObtenerAtencionesConfirmadas();
        List<AtencionSinMesa> ObtenerAtencionesSinMesa(int idCentroAtencion);
        //OperationResult ActualizarEstadoDeDetallesDeOrden(long[] ids, int numEstado);
        List<Orden_Atencion> ObtenerOrdenesPorEstado(int numEstado);
        List<Orden_Atencion> ObtenerOrdenesPorEstadoDesdeUnAmbiente(int numEstado, int idAmbiente);
        OperationResult CambiarEstadoDeOrden(long idOrden, int estado);
         
        #region Cambio de estado: Detalle de Orden
        OperationResult AtenderDetalleDeOrden(int idDetalleDeOrden);
        OperationResult ServirDetalleDeOrden(int idDetalleDeOrden);
        OperationResult PrepararDetalleDeOrden(int idDetalleDeOrden);
        OperationResult DevolverDetalleDeOrden(int idDetalleDeOrden);
        OperationResult AnularDetalleDeOrden(int idDetalleDeOrden);
        OperationResult ObservarDetalleDeOrden(int idDetalleDeOrden);
        OperationResult ReanudarDetalleDeOrden(int idDetalleDeOrden);

        OperationResult ReanudarTodosLosDetallesDeOrden(long idOrden);
        OperationResult AnularTodosLosDetallesDeOrden(long idOrden);
        OperationResult AtenderTodosLosDetallesDeOrden(long idOrden);
        OperationResult ServirDetallesDeOrdenes(long[] idsDetallesDeOrdenes);
        OperationResult PrepararDetallesDeOrdenes(long[] idsDetallesDeOrdenes);
        OperationResult CerrarOrden(long idOrden);
        #endregion
        OperationResult ActualizarImportesDeTransaccion(List<ItemGenerico> nuevosImportes);
        OperationResult CerrarAtencion(long id, SesionRestaurante sesion);
        OperationResult AnularAtencion(long id);
        OperationResult CambiarEstadoDeOrdenes(long[] ids, int estado);
        long[] ObtenerIdsDeitemsPorCategoria(long idCategoria);
        OperationResult ConfirmarFacturacion(AtencionRestaurante atencion, SesionRestaurante sesionRestaurante);
        OperationResult ConfirmarPagoAtencion(long idAtencion, SesionRestaurante sesionRestaurante);

        List<ResumenOrden_Consulta> ObtenerReporteDeResumenDeOrdenes(DateTime desde, DateTime hasta);
        List<ResumenOrdenesPorMozo_Consulta> ObtenerReporteDeResumenDeMozos(DateTime desde, DateTime hasta);
        List<DetalleOrden_Consulta> ObtenerReporteDetallesAtendidosEnOrdenes(DateTime desde, DateTime hasta);
        List<DetalleOrden_Consulta> ObtenerReporteDetallesDevueltosEnOrdenes(DateTime desde, DateTime hasta);

        List<ItemRestaurante_Consulta> ObtenerReporteDeItemsDeRestaurante(DateTime desde, DateTime hasta);

        List<ResumenAtencion> ObtenerResumenAtencionesCerradas(DateTime fechaDesde, DateTime fechaHasta, SesionRestaurante sesion);
        AtencionRestaurante ObtenerAtencionEspecifica(long id);
        Dictionary<long, long> ObtenerIdsOrdenDeVentaDeAtencion(long idAtencion);
        ItemRestaurante ObtenerItemDeRestauranteIncluyendoComplementosDeFamilia(int idItem);
        OperationResult ActualizarJsonDetalleDeDetalleOrden(long idDetalle, string stringJsonDetalle);
        ComprobanteCuentaAtencion ObtenerComprobanteCuentaAtencion(long idAtención);
        ComprobanteOrden ObtenerComprobanteOrdenSinItemsAnulados(long idOrden);
        List<OrdenAtencion_Consulta> ObtenerOrdenesAtencion(DateTime desde, DateTime hasta, int idEstablecimiento);
        List<OrdenPorModoAtencion_Consulta> ObtenerOrdenesPorModoAtencion(DateTime desde, DateTime hasta, int idEstablecimiento);
        List<Complemento> ObtenerComplementos();
        OperationResult ActualizarComplemento(Complemento complemento);
    }
}

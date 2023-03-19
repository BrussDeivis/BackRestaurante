using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;


namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IConceptoLogica
    {
        /// <summary>
        /// guarda un nuevo concepto basico 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="idsCategoria"></param>
        /// <param name="idsCaracteristica"></param>
        /// <returns></returns>
        OperationResult GuardarConceptoBasico(string nombre, string valor, int[] idsCategoria, int[] idsCaracteristica);

        /// <summary>
        /// actuliza un nuevo concepto basico
        /// </summary>
        /// <param name="idConcepto"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="idsCategoria"></param>
        /// <param name="idsCaracteristica"></param>
        /// <returns></returns>
        OperationResult ActualizarFamilia(int idConcepto, string nombre, string valor, int[] idsCategoria, int[] idsCaracteristica);


        /// <summary>
        /// Da de baja un concepto basico
        /// </summary>
        /// <param name="idConcepto"></param>
        /// <returns></returns>
        OperationResult CambiarEsVigenteFamilia(int idConcepto, bool esVigente);

        /// <summary>
        /// crea una nueva caracteristica y agrega sus valores 
        /// </summary>
        /// <param name="idMaestroCaracteristica"></param>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        OperationResult GuardarCarcateristica(int idMaestroCaracteristica, string codigo, string nombre, string descripcion, List<Valor_caracteristica> valores);


        /// <summary>
        /// devuelve una coleccion de caracteristicas
        /// </summary>
        /// <returns></returns>
        List<Detalle_maestro> ObtenerCaracteristicas();

        List<ItemGenerico> ObtenerCaracteristicasComunes();

        List<Detalle_maestro> ObtenerCaracteristicasIncluyendoValores();
        List<Detalle_maestro> ObtenerCaracteristicasVigentesIncluyendoValores();
        /// <summary>
        /// actualiza una caracteristica y sus valores
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMaestroCaracteristica"></param>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        OperationResult ActualizarCaracteristica(int id, int idMaestroCaracteristica, string codigo, string nombre, string valor, List<Valor_caracteristica> valores, bool esVigente);
        bool ExisteCaracteristicaEnConceptosVigentes(int idCaracteristica);
        bool ExisteCaracteristicaEnFamiliasVigentes(int idCaracteristica);

        /// <summary>
        /// obtiene la caracteristica segun el id dado
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerCaracteristica(int idCaracteristica);
        List<ItemGenerico> ObtenerCaracteristicasVigentesPorFamilia(int idFamilia);
        /// <summary>
        /// obtiene una coleccion de conceptos basicos
        /// </summary>
        /// <returns></returns>
        List<Detalle_maestro> ObtenerConceptosBasicos();

        /// <summary>
        /// devuelve el concepto segun el id dado
        /// </summary>
        /// <param name="idConcepto"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerConceptoBasicoVigente(int idConcepto);

        Detalle_maestro ObtenerConceptoBasicoVigenteIncluyendonCaracteristicasYValoresCaracteristicas(int idConcepto);
        /// <summary>
        /// Obtener valor caracteristica segun el id
        /// </summary>
        /// <param name="idValorCaracteristica"></param>
        /// <returns></returns>
        Valor_caracteristica ObtenerValorCaracteristica(int idValorCaracteristica);


        /// <summary>
        /// obtiene los valores de una caracteristica 
        /// </summary>
        /// <returns></returns>
        List<Valor_caracteristica> ObtenerValoresDeCaracteristica(int idCaracteristica);





        /// <summary>
        /// actualiza el valor de una caracteristica
        /// </summary>
        /// <param name="idValorCaracteristica"></param>
        /// <param name="idCaracteristica"></param>
        /// <param name="valorCaracteristica"></param>
        /// <returns></returns>
        OperationResult ActualizarValorCaracteristica(int idValorCaracteristica, int idCaracteristica, string valorCaracteristica);

        /// <summary>
        /// crea un nuevo valor para un acaracteristica
        /// </summary>
        /// <param name="idCaracteristica"></param>
        /// <param name="valorCaracteristica"></param>
        /// <returns></returns>
        OperationResult GuardarValorCarcateristica(int idCaracteristica, string valorCaracteristica);


        ///// <summary>
        ///// retorna una lista de productos con stock
        ///// </summary>
        ///// <returns></returns>
        //List<Producto> obtenerProductosConStock();

        /// <summary>
        /// retorna los productos como subcontenidos 
        /// </summary>
        /// <param name="idModelo"></param>
        /// <returns></returns>
        List<ConceptoDeNegocio> obtenerSubContenidos(int idModelo);




        OperationResult GuardarProducto(string codigoBarra, string nombre, string codigo, string codigoDigemid, string sufijo, int idConceptoBasico, bool esBien, int idUnidadDeMedidaCom, int idUnidadDeMedidaRef, int[] idCaracteristicas, int[] modulosAdicionales, int idPresentacion, decimal cantidadPresentacion, int idUnidadDeMedidaPre, int? idPresentacionSubContenido, byte[] foto, bool hayFoto, List<Precio_Compra_Venta_Concepto> precios, decimal? stock, int idEmpleado, int idCentroAtencion);


        OperationResult DarDeBajaMercaderia(int idMercaderia);

        string ObtenerSiguienteCodigoParaMercaderia(int idRol, int idConceptoBasico);
        void AgregarCaracteristicas(int[] idsCaracteristicas, Concepto_negocio producto);
        List<Precio> GenerarPrecios(List<Precio_Compra_Venta_Concepto> precios, DateTime fecha, int idProducto, int idEmpleado);
        /// <summary>
        /// actualiza datos de producto
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="codigo"></param>
        /// <param name="sufijo"></param>
        /// <param name="idUnidadDeMedidaCom"></param>
        /// <param name="idUnidadDeMedidaRef"></param>
        /// <param name="idCaracteristicas"></param>
        /// <param name="idPresentacion"></param>
        /// <param name="cantidadPresentacion"></param>
        /// <param name="idUnidadDeMedidaPre"></param>
        /// <param name="idPresentacionSubContenido"></param>
        /// <param name="foto"></param>
        /// <param name="hayFoto"></param>
        /// <returns></returns>
        OperationResult ActualizarProducto(int id, string codigoBarra, string nombre, string codigo, string codigoDigemid, string sufijo, int idConceptoBasico, bool esBien, int idUnidadDeMedidaCom, int idUnidadDeMedidaRef, int[] idCaracteristicas, int[] modulosAdicionales, int idPresentacion, decimal cantidadPresentacion, int idUnidadDeMedidaPre, int? idPresentacionSubContenido, byte[] foto, bool hayFoto, List<Precio_Compra_Venta_Concepto> precios, decimal? stockMinimo, int idEmpleado, int idCentroAtencion);

        OperationResult GuardarConceptoServicio(string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion);
        /// <summary>
        /// retorna un producto ingresando su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ConceptoDeNegocio obtenerProducto(int id);

        List<Precio_Compra_Venta_Concepto> ObtenerPreciosCompraVentaDeConceptoNegocio(int idConceptoDeNegocio);

        decimal ObtenerCostoUnitarioDelIcbperALaFecha();


        List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoStockYPrecios(int idConceptoBasico, int idCentroAtencion);

        List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoStockYPrecios();

        /// <summary>
        /// Retorna las mercaderias de un centro de atención que tienen por lo menos un precio
        /// </summary>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        List<ConceptoDeNegocio> ObtenerMercaderiasPorCentroAtencionIncluyendoStockYPrecios(int idCentroAtencion);
        List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios(int idConceptoBasico);


        /// <summary>
        /// obtine los productos que tienen precios para una venta
        /// </summary>
        /// <returns></returns>
        List<ConceptoDeNegocio> obtenerMercaderiasConPrecio();
        List<ConceptoDeNegocio> obtenerMercaderias();//PROCESO X0
        ConceptoDeNegocio ObtenerMercaderia(int idMercaderia);
        ConceptoDeNegocio ObtenerConcepto(int idConcepto);
        List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasico(int idConceptoBasico);

        List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoExistencias();

        List<Reporte_Stock_General> ObtenerReporteStockGeneral(int[] idsEntidadInterna);
     
        ConceptoDeNegocio ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPreciosConUnPrecioComoMinimo(int idCentroAtencion, string codigoBarra);
        ConceptoDeNegocio ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios(string codigoBarra);
        List<Stock> ObtenerExistencias(int idAlmacen);

        List<DetalleGenerico> ObtenerConceptosVigentesGasto();
        OperationResult GuardarConceptoGasto(string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion);

        OperationResult ActualizarConceptoGasto(int id, string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion);
        List<Detalle_maestro> ObtenerConceptosBasicosDeGasto();
        List<RolDeNegocio> ObtenerRolesQueAplicaAConceptosDeNegocioExceptoMercaderiaYServicios();

        List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoIncluyendoStockPreciosYCaracteristicas(int idConceptoBasico);
        List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoYCaracteristicasIncluyendoStockPreciosYCaracteristicas(int idConceptoBasico, int[] idValoresCaracteristicas);
        List<ConceptoDeNegocio> ObtenerMercaderiasPorCaracteristicasIncluyendoStockPreciosYCaracteristicas(int[] idsValoresDeCaracteristicas);



        /// <summary>
        /// Obtiene el primer concepto de negocio por nombre que sea vigente
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        ConceptoDeNegocio ObtenerConceptoDeNegocioPorNombre(string nombre);
        

        /// <summary>
        /// Obtiene una lista de conceptos incluyendo su categoria
        /// </summary>
        /// <returns></returns>
        List<Detalle_maestro> ObtenerConceptosBasicosVigentesIncluyendoCategoriaConcepto();
        List<Detalle_maestro> ObtenerTodosLasFamilia();
        #region CONCEPTOS NEGOCIOS COMERCIALES SIN STOCK Y PRECIOS

        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicas(int[] idsValoresDeCaracteristicas);
        List<Concepto_Negocio_Comercial> ObtenerConceptosNegociosComerciales(int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas);

        List<Concepto_Negocio_Comercial> ObtenerConceptosNegociosComercialesConStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas);

        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas(int idConceptoBasico, int[] idsValoresDeCaracteristicas);
        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico(int idConceptoBasico);
        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRoles();
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES CON STOCK Y PRECIOS

        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicasIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int[] idsValoresDeCaracteristicas);

        #endregion

        #region CONCEPTOS NEGOCIO COMERCIALES PARA OPERACIONES
        List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComerciales(int modoSeleccionTipoFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario);
        List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorBusquedaConcepto(string cadenaBusqueda, int modoSeleccionTipoFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario);
        List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorFamilia(int idFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorCodigoBarra(UserProfileSessionData sesionDeUsuario, string codigoBarra, bool complementoStock, bool complementoPrecio, int modoSeleccionTipoFamilia);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorIdConcepto(UserProfileSessionData sesionDeUsuario, int idConceptoNegocio, bool complementoStock, bool complementoPrecio);
        List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialParaVentaPorNombre(int idCentroAtencionQueTieneLosPrecios, string nombre);


        //List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesParaCompra(int idConceptoBasico);
        //Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaCompraPorCodigoBarra(string codigoBarra);
        //List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesParaVenta(int idCentroAtencionQueTieneLosPrecios, int idConceptoBasico);
        //List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComerciales(int idCentroAtencionQueTieneLosPrecios);
        //Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorCodigoBarra(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneStockIntegrado, string codigoBarra);
        //Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorId(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneLosPrecios, int id);
        //List<Complemento_Concepto_Negocio_Comercial> ObtenerComplementoConceptoDeNegocioComercialParaVenta(long idTransaccionInventarioFisico, int idConceptoNegocio, bool esBien);
        //Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorIdConceptoNegocio(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneLosPrecios, int idConceptoNegocio, bool esBien);
        #endregion


        #region CONCEPTOS BASICOS
        List<Concepto_Basico> ObtenerConceptosaBasicosVigentesIncluyendoCaracteristicas();
        #endregion


        #region VALIDACIONES
        //Producto
        bool ExisteCodigoDeBarraDeProducto(string codigoBarra);

        bool ExisteNombreConceptoComercial(string codigoBarra);

        //Valor caracteristica
        bool ExisteNombreDeValorCaracteristica(int idCaracteristica, string valor);
        #endregion


        List<ReporteDigemid> ObtenerReporteConceptosDigemid(UserProfileSessionData sesionUsuario);



    }
}

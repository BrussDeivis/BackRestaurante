using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IConceptoRepositorio : IRepositorioBase
    {
        #region crear y actualizar
        /// <summary>
        /// crea un concepto de negocio
        /// </summary>
        /// <param name="ConceptoDeNegocio"></param>
        /// <returns></returns>
        OperationResult CrearConceptoDeNegocio(Concepto_negocio ConceptoDeNegocio);

        /// <summary>
        /// actualizar concepto de negocio
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        OperationResult ActualizarConceptoNegocio(Concepto_negocio producto);
        OperationResult ActualizarNombreConceptosNegocio(List<Concepto_negocio> conceptosNegocio);

        OperationResult DarDeBajaConceptoNegocio(int idConceptoNegocio);
        bool TieneStockConceptoNegocio(int idConceptoNegocio);
        OperationResult InvertirEsVigenteConceptoNegocio(int idConceptoNegocio);
        OperationResult CrearConceptoDeNegocioRol(Concepto_negocio_rol mercaderia);


        /// <summary>
        /// actualiza datos de un concepto basico
        /// </summary>
        /// <param name="concepto"></param>
        /// <returns></returns>
        OperationResult ActualizarConcepto(Detalle_maestro concepto);


        /// <summary>
        /// actualiza los datos de la caracteristica y sus valores si los tuviese
        /// </summary>
        /// <param name="caracteristica"></param>
        /// <returns></returns>
        OperationResult ActualizarCaracteristica(Detalle_maestro caracteristica);
        IEnumerable<Concepto_negocio> ConceptosNegocioVigentesConCaracteristica(int idCaracteristica);
        IEnumerable<Detalle_maestro> FamiliasVigentesConCaracteristica(int idCaracteristica);
        /// <summary>
        ///  crea un nuevo valor de una caracteristica
        /// </summary>
        /// <param name="valorCaracteristica"></param>
        /// <returns></returns>
        OperationResult GuardarValorCaracteristica(Valor_caracteristica valorCaracteristica);

        /// <summary>
        /// actualiza el valor de una caracteristica
        /// </summary>
        /// <param name="valorCaracteristica"></param>
        /// <returns></returns>
        OperationResult ActualizarValorCaracteristica(Valor_caracteristica valorCaracteristica);
        #endregion

        #region consultas
        /// <summary>
        /// devuelve el concepto de negocio correspondiente al id 
        /// </summary>
        /// <param name="idConcepto"></param>
        /// <returns></returns>
        Concepto_negocio ObtenerConceptoNegocioIncluyendoValorCaracteristicaConceptoNegocioYDetalleMaestroYCaracteristicaConcepto(int idConcepto);

        /// <summary>
        /// Devuelve una colleccion de conceptos de negocio con respecto a un rol
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(int idRol);

        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(int idRol, int idConceptoBasico);

        IEnumerable<Precio_Compra_Venta_Concepto> ObtenerPreciosCompraVentaDeConceptoNegocio(int idConceptoNegocio);

        decimal ObtenerPrecioPublicoDelIcbper(DateTime fecha);


        Concepto_negocio ObtenerConceptosDeNegocioPorRolYCodigoBarra(int idRol, string codigoBarra);
        ///// <summary>
        ///// Retorna una colleccion de conceptos de negocio con stock 
        ///// </summary>
        ///// <param name="idRol"></param>
        ///// <returns></returns>
        //IEnumerable<Concepto_negocio> obtenerConceptosDeNegocioPorRolYConStock(int idRol);

        /// <summary>
        /// Retorna una colleccion de conceptos de negocio con precios 
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimo(int idRol);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRol(int idRol); //EN PROCESO X0
        Concepto_negocio ObtenerConceptoDeNegocioPorRolYIdConceptoNegocio(int idRol, int idConceptoNegocio);
        Concepto_negocio ObtenerConceptoDeNegocioPorIdConceptoNegocio(int idConceptoNegocio);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioIncluyendoDetalleMaestro4(int[] idsConceptoNegocio);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(int idActorNegocio, int idRol);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoPorActorNegocio(int idActorNegocio, int idRol, int idConceptoBasico);
        /// <summary>
        /// devuelve el maximo codigo para concepto de negocio
        /// </summary>
        /// <param name="prefijo"></param>
        /// <param name="idRolConcepto"></param>
        /// <param name="idConceptoBasico"></param>
        /// <returns></returns>
        int ObtenerMaximoCodigoParaConceptoNegocio(string prefijo, int idRolConcepto, int idConceptoBasico);


        /// <summary>
        /// obtiene los valores de una caracteristica
        /// </summary>
        /// <param name="idCaracteristica"></param>
        /// <returns></returns>
        IEnumerable<Valor_caracteristica> ObtenerValoresDeCaracteristica(int idCaracteristica);

        /// <summary>
        /// Obtiene un valor caracteristica segun el id
        /// </summary>
        /// <param name="idValorCaracteristica"></param>
        /// <returns></returns>
        Valor_caracteristica ObtenerValorCaracteristica(int idValorCaracteristica);

        IEnumerable<Concepto_negocio> ObtenerConceptoPorRol(int idRol);

        Concepto_negocio ObtenerConceptoDeNegocioPorCodigoBarraIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(int idActorNegocio, int idRol, string codigoBarra);
        bool ExisteCodigoBarraEnConceptoVigente(int idRol, string codigoBarra);
        bool ExisteCodigoBarraEnConceptoVigenteAlActuaizar(int idConcepto, int idRol, string codigoBarra);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistencia(int idRol);
        /// <summary>
        /// Metodo usado para el reporte stock general, devuelve los conceptos de negocio, el stock el nombre del centro de atencion
        /// </summary>
        /// <param name="idTipoTransaccionExistente"></param>
        /// <param name="idUltimoEstadoTransaccionExsitente"></param>
        /// <returns></returns>
        IEnumerable<Reporte_Stock_General> ObtenerReporteStockGeneral(int[] idsActorNegocioInterno, int idTipoTransaccionExistente, int idUltimoEstadoTransaccionExsitente);

        IEnumerable<Existencia> ObtenerExistenciasIncluyendoConceptoNegocioYActorNegocio(int idEntidadInterna);


        IEnumerable<Rol> ObtenerRolesDeConceptosExceptoMercaderiaYServicios(int aplicaA, int[] idsAExcluir);
        IEnumerable<Detalle_maestro> ObtenerDetalleMaestro4DeConceptoNegocioConRol(int idRol);

        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRol, int idConceptoBasico);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRol, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRolMercaderia, int idConceptoBasico, int[] idsValoresDeCaracteristicas);

        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolesIncluyendoExistenciasYPrecios(int idRol);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolesIncluyendoExistenciasYPrecios(int iddRol, bool esVigente);
        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolYPorConceptoBasico(int idRol, int idConceptoBasico);

        IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioConPreciosPorFamilia(int idFamilia);
        #endregion

        /// <summary>
        /// Obtiene el primer concepto_negocio por nombre que sea vigente
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        Concepto_negocio ObtenerConceptoNegocioPorNombre(string nombre);


        #region CONCEPTOS NEGOCIOS COMERCIALES SIN STOCK Y PRECIOS
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicas(int[] idsRoles, int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas(int[] idsRoles, int idConceptoBasico, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicas(int[] idsRoles, int idCategoria, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicas(int[] idsRoles, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoria(int[] idsRoles, int idConceptoBasico, int idCategoria);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico(int[] idsRoles, int idConceptoBasico);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoria(int[] idsRoles, int idCategoria);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRoles(int[] idsRoles);
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES CON STOCK Y PRECIOS
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idCategoria, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int[] idsValoresDeCaracteristicas);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int idCategoria);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idCategoria);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion);
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES PARA OPERACIONES
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRol(int idRol);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYTipoFamilia(int idRol, string valorTipoFamilia);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string valorTipoFamilia);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConcepto(int idRol, string cadenaBusqueda);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string cadenaBusqueda);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConceptoYTipoFamilia(int idRol, string cadenaBusqueda, string valorTipoFamilia);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string cadenaBusqueda, string valorTipoFamilia);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYFamilia(int idRol, int idFamilia);
        IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idFamilia);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(int idRol, string codigoBarra);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(int idRol, string codigoBarra, string valorTipoFamilia);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(long idTransaccionInventario, int idRol, string codigoBarra);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(long idTransaccionInventario, int idRol, string codigoBarra, string valorTipoFamilia);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(int idActorNegocioQueTienePrecios, int idRol, string codigoBarra);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(int idActorNegocioQueTienePrecios, int idRol, string codigoBarra, string valorTipoFamilia);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, string codigoBarra);
        IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, string codigoBarra, string valorTipoFamilia);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorRolesEIdConcepto(int idRol, int idConceptoNegocio);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConStockPorRolesEIdConcepto(long idTransaccionInventario, int idRol, int idConceptoNegocio);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConPrecioPorRolesEIdConcepto(int idActorNegocioQueTienePrecios, int idRol, int idConceptoNegocio);
        Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConStockPrecioPorRolesEIdConcepto(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, int idConceptoNegocio);

        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string nombre);
        IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreInclyendoStockPreciosYStock(int idActorNegocioInterno, int idTipoTransaccion, int idTarifa, int idUltimoEstado, int[] idsRoles, string nombre);



        //IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoBasico);
        //IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles);
        //IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string nombre);
        //IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreInclyendoStockPreciosYStock(int idActorNegocioInterno, int idTipoTransaccion, int idTarifa, int idUltimoEstado, int[] idsRoles, string nombre);
        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string codigoBarra);
        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorIdIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int id);
        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecioInclyendoComplementos(long idActorNegocioInternoExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string codigoBarra);
        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorIdIncluyendoPrecioInclyendoComplementos(long idActorNegocioInternoExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int id);


        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoNegocio);
        //Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecioInclyendoComplementos(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoNegocio);


        #endregion


        #region CONCEPTOS BASICOS
        IEnumerable<Concepto_Basico> ObtenerConceptosBasicosIncluyendodoCaracteristicas(int idMaestro, bool esVigente);
        #endregion

        #region VALIDACIONES

        /// <summary>
        /// Comprueba si hay un registro con el mismo codigo de barra en la tabla concepto_negocio
        /// </summary>
        /// <param name="codigoDeBarra"></param>
        /// <returns></returns>
        bool ExisteCodigoDeBarraConceptoNegocio(string codigoDeBarra);

        /// <summary>
        /// Comprueba si hay un registro con el mismo nombre en la tabla concepto_negocio
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        bool ExisteNombreConceptoNegocio(string nombre);

        /// <summary>
        /// Comprueba si esta asociado  con la tabla concepto_negocio
        /// </summary>
        /// <param name="idDetalleMaestro"></param>
        /// <param name="esVigente"></param>
        bool TieneConceptosDeNegocio(int idDetalleMaestro, bool esVigente);

        /// <summary>
        /// Comprueba si hay un registro con el mismo nombre en la tabla valor_caracteristica
        /// </summary>
        /// <param name="idCaracteristica"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        bool ExisteNombreDeValorCaracteristica(int idCaracteristica, string valor);

        bool EsBien(string codigoBarra);
        bool EsBien(int id);
        #endregion
        IEnumerable<ReporteDigemid> ObtenerReporteConceptosDigemid(int idRol, int idActorNegocioInternoQueTieneLosPrecios, int idTarifaUnitariaDigemid);
    }
}

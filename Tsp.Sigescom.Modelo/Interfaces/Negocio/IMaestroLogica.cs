using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IMaestroLogica
    {
        #region Crear, Actualizar y eliminar un Maestro
        /// <summary>
        /// guarda un maestro con los datos de codigo y nombre ingresados como parametros
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        OperationResult guardarMaestro(string codigo, string nombre);

        /// <summary>
        /// actualiza el codigo y el nombre de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        OperationResult actualizarMaestro(int id, string codigo, string nombre);
        #endregion

        #region Crear, Actualizar y Eliminar un Detalle de un Maestro
        /// <summary>
        /// guarda un detalle de maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        OperationResult guardarDetalleMaestro(int idMaestro, string codigo, string nombre, string valor);
        OperationResult GuardarDetalleDetalleMaestro(int idMaestro, string codigo, string nombre, string valor, int idMaestroPadre);
        /// <summary>
        /// actualiza un detalle de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMaestro"></param>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        OperationResult actualizarDetalleMaestro(int id, int idMaestro, string codigo, string nombre, string valor);
        OperationResult ActualizarDetalleDetalleMaestro(int id, int idMaestro, string codigo, string nombre, string valor, int idMaestroPadre);
        #endregion

        #region consultas
        /// <summary>
        /// Devuelve un detale de maestro conteniendo la moneda por defecto
        /// </summary>
        /// <returns></returns>
        Detalle_maestro ObtenerMonedaPorDefecto();

        /// <summary>
        /// devuele una lista de todos los maestros
        /// </summary>
        /// <returns></returns>
        List<Maestro> obtenerMaestros();

        Detalle_maestro ObtenerDetalleMaestroPorIdMaestroYNombre(int idMaestro, string nombre);

        /// <summary>
        /// devuelve los detalles de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerDetallesMaestrosAsync(int idMaestro);

        List<Detalle_maestro> ObtenerDetallesMaestros(int idMaestro);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDetalleMaestro"></param>
        /// <returns></returns>
        Detalle_maestro obtenerDetalleMaestro(int idDetalleMaestro);

        /// <summary>
        /// devuelve una coleccion del maestro medio de pagos
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerMediosDePago();



        /// <summary>
        /// devuelve los ubigeos  de los distritos de peru
        /// </summary>
        /// <returns></returns>
        List<Ubigeo> obtenerUbigeoDistrito();
        List<Ubigeo> obtenerUbigeo(int[] idUbigeos);
        /// <summary>
        /// devuelve un listado de documentos de identidad: pasaporte, DNI, RUC, etc.
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposDeDocumentosDeIdentidad();

        /// <summary>
        /// devuelve un listado de categorias.
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerCategorias();


        /// <summary>
        /// devuelve una lista de vias: calle, avenida, etc
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposDeVia();

        /// <summary>
        /// devuelve una lista de Zonas: parques, colegios, etc
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposDeZona();

        /// <summary>
        /// devuelve una lista de naciones: Peru, Brasil, etc
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerNaciones();


        /// <summary>
        /// devuelve un listado de tipos de direccion: principal, secundario, etc
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposDeDireccion();


        /// <summary>
        /// devuelve un listado de tarifas para precio
        /// </summary>
        /// <returns></returns>
        List<Detalle_maestro> obtenerTarifas();

        /// <summary>
        /// obtiene los conceptos
        /// </summary>
        /// <returns></returns>
        List<Detalle_maestro> ObtenerConceptosVigentes();
        List<Familia_Concepto_Comercial> ObtenerFamiliasVigentes();
        List<Familia_Concepto_Comercial> ObtenerFamiliasVigentes(int modoSeleccionTipoFamilia);
        List<Familia_Concepto_Comercial> ObtenerFamiliasConceptosComercialesVigentes();
        List<Familia_Concepto_Comercial> ObtenerFamiliasConceptosComercialesVigentes(int modoSeleccionTipoFamilia);
        List<Detalle_maestro> ObtenerConceptosVigentesDeCompraVenta();
        List<Detalle_maestro> ObtenerConceptosServicioVigentes();

        /// <summary>
        ///  obtiene los conceptos basicos de pago al empleado
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerConceptosPagoEmpleados();
        /// <summary>
        /// Obtiene todos los tipos de servicio e impuesto
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposServicioImpuesto();
        /// <summary>
        /// Obtiene todos los tipos de productos para la compra
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposProductoDeCompra();
        /// <summary>
        /// Obtiene los tipos de bienes que pueda tener cada producto 
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerTiposBien();

        /// <summary>
        /// retorna las marcas asociadas a un articulo
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <returns></returns>
        List<Detalle_maestro> obtenerMarcas(int idArticulo);

        /// <summary>
        /// devuelve una coleccion de unidades de medida
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerUnidadesDeMedida();

        /// <summary>
        /// retorna las presentaciones para un producto
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerPresentaciones();

        /// <summary>
        /// devuelve los valores de las caracteristicas
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <returns></returns>
        List<Valor_caracteristica_concepto> obtenercaracteristicasConceptoValor(int idArticulo);

        /// <summary>
        /// devuelve los estados para una transaccion
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerEstadosTransaccion();

        /// <summary>
        /// devuelve los estados para una transaccion
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerAccionesTransaccion();

        /// <summary>
        /// devuelve llos tipos de tarjetas
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerOperadoresDeTarjeta();

        /// <summary>
        /// devuelve todas las unidades de negocio
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerUnidadesDeNegocio();

        /// <summary>
        /// devuelve una lista para el menu
        /// </summary>
        /// <returns></returns>
        List<Menu_aplicacion> obtenerMenus();

        /// <summary>
        /// devuelve las modalidades de traslado de la mercaderia
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerModalidadesTrasladoAsync();

        List<Detalle_maestro> ObtenerModalidadesTraslado();

        /// <summary>
        /// devuelve los motivos de trasladod e mercaderia
        /// </summary>
        /// <returns></returns>
        Task<List<Detalle_maestro>> ObtenerMotivosTrasladoAsync();
        Task<List<Detalle_maestro>> ObtenerMotivosTrasladoVigentesAsync();

        List<Detalle_maestro> ObtenerMotivosTraslado();
        Task<List<Detalle_maestro>> ObtenerMotivosDeViajeAsync();
        Task<List<Detalle_maestro>> ObtenerTiposDeComprobante(int idMaestro);
        Task<List<Detalle_maestro>> ObtenerEntidadesFinancieras();
        Task<List<Detalle_maestro>> ObtenerMonedas();
        Task<List<Detalle_maestro>> ObtenerTiposCuentaBancaria();
        Task<List<Detalle_maestro>> ObtenerTiposDeNotaDeDebito();
        Task<List<Detalle_maestro>> ObtenerTiposDeNotaDeCredito();

        /// <summary>
        /// devuelve el detalle del catalogo de documentos (factura, boleta, etc) sunat correspondiente al codigo
        /// </summary>
        /// <param name="codigoDetalle"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerDetalleMaestroDeDocumento(string codigoDetalle);

        /// <summary>
        /// devuelve el detalle del catalogo de documentos de identidad sunat correspondiente al codigo
        /// </summary>
        /// <param name="codigoDetalle"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerDetalleMaestroDeDocumentoIdentidad(string codigoDetalle);

        #endregion

        #region VALIDACIONES
        /// <summary>
        /// Comprueba si existe un detalle_maestro con el mismo nombre segun el id_maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="idDetalleMaestro"></param>
        /// <param name="nombre"></param>
        void ComprobarSiYaExisteNombreDeDetalleMaestro(int idMaestro, int idDetalleMaestro, string nombre);

        bool ComprobarSiYaExisteNombreDeDetalleMaestro(int idDetalleMaesro, string nombreDetalleMaestro, Detalle_maestro detalleMaestro);
        #endregion
        List<ItemGenerico> ObtenerDetalleMaestroLibroElectronico();
        ItemGenerico ObtenerLibroElectronico(int idLibroElectronico);
        Tipo_cambio ObtenerTipoCambioDolarActual();
        OperationResult GuardarTipoCambioDolarActual(Tipo_cambio tipoCambio);
        Task<List<ItemGenerico>> ObtenerTiposGrupoClientes();
        Task<List<ItemGenerico>> ObtenerClasificacionesGrupoClientes();
    }
}

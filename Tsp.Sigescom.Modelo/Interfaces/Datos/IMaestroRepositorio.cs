using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IMaestroRepositorio: IRepositorioBase
    {

        #region crear, actualizar, dar de baja
        /// <summary>
        /// crea un nuevo maestro
        /// </summary>
        /// <param name="maestro"></param>
        /// <returns></returns>
        OperationResult crearMaestro(Maestro maestro);
            
        /// <summary>
        /// actualiza los datos de un maestro 
        /// </summary>
        /// <param name="maestro"></param>
        /// <returns></returns>
        OperationResult actualizarMaestro(Maestro maestro);

        /// <summary>
        /// crea un detalle de un maestro 
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        OperationResult crearDetalleMaestro(Detalle_maestro detalle);
        OperationResult CrearDetalleDetalleMaestro(Detalle_detalle_maestro detalle);

        /// <summary>
        /// Actualiza la columna 'valor' con un valor de  0 
        /// </summary>
        /// <param name="idDetalleMaestro"></param>
        /// <param name="idMaestro"></param>
        /// <returns></returns>
        OperationResult DarDeBajaDetalleMaestro(int idDetalleMaestro, int idMaestro);
        OperationResult DarDeAltaDetalleMaestro(int idDetalleMaestro, int idMaestro);


        /// <summary>
        /// actualiza los datos de un detalle maestro 
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        OperationResult actualizarDetalleMaestro(Detalle_maestro detalle);
        OperationResult ActualizarDetalleDetalleMaestro(Detalle_detalle_maestro detalle);
        #endregion



        #region consultas
        /// <summary>
        /// devuelve todos los datos de la tabla maestros
        /// </summary>
        /// <returns></returns>
        IEnumerable<Maestro> obtenerMaestros();

        /// <summary>
        /// devuelve un listado de documentos de identidad: pasaporte, DNI, RUC, etc.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Documento_identidad> obtenerTiposDeDocumentosDeIdentidad();

        /// <summary>
        /// devuelve una coleccion de ubiegos de peru
        /// </summary>
        /// <returns></returns>
        IEnumerable<Ubigeo> obtenerUbigeos(int idPais);
        IEnumerable<Ubigeo> obtenerUbigeos(int[] idUbigeos);
        IEnumerable<Ubigeo> ObtenerRegiones();
        /// <summary>
        /// obtiene un detalle especifico con el id dado
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerDetalle(int idDetalle);
        Detalle_maestro ObtenerDetalleMaestroPorIdMaestroYNombre(int idMaestro, string nombre);

        /// <summary>
        /// Obtiene un detalle_maestro vigente
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerDetalle(int idDetalle, bool esVigente);

        /// <summary>
        /// obtiene un detalle especifico con el codigo dado y el maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="codigoDetalle"></param>
        /// <returns></returns>
        Detalle_maestro obtenerDetalle(long idMaestro,string codigoDetalle);

        /// <summary>
        /// Obtiene un detalle_maestro segun el id_maestro, id, esVigente
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="idDetalleMaestro"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        Detalle_maestro ObtenerDetalleMaestro(int idMaestro, int idDetalleMaestro, bool esVigente);

        /// <summary>
        /// obtiene una coleccion de conceptos basicos segun la Categoria
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> obtenerDetallesConcepto(int idDetalleMaestro);
        
        /// <summary>
        /// devuelva una coleccion de marcas para un articulo
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> obtenerDetallesMarca(int idDetalleMaestro);
        
        /// <summary>
        /// devuelve una coleccion de valores para un concepto
        /// </summary>
        /// <param name="idDetalleMaestro"></param>
        /// <returns></returns>
        IEnumerable<Valor_caracteristica_concepto> obtenerValorCaracteristicaConcepto(int idDetalleMaestro);

        Detalle_maestro ObtenerDetalleMaestroInclusiveCaracteristicaConceptoConDetalleMaestroYValorCaracteristica(int idDetalle, bool esVigente);
        IEnumerable<ItemGenerico> ObtenerDetallesComoItemsGenericos(int idMaestro);
        IEnumerable<Detalle_maestro> ObtenerDetalles(int idMaestro);


        Detalle_maestro ObtenerDetalleInclusiveValorCaracteristica(int idDetalle);
        IEnumerable<Detalle_maestro> ObtenerDetallesInclusiveValorCaracteristica(int[] idsMaestro);
        IEnumerable<Detalle_maestro> ObtenerDetallesVigentesInclusiveValorCaracteristica(int[] idsMaestro);
        /// <summary>
        /// devuelve una lista de los menus
        /// </summary>
        /// <returns></returns>
        IEnumerable<Menu_aplicacion> obtenerMenus();

        IEnumerable<Detalle_maestro> obtenerDetallesMaestros(int idMaestro);
        Task<IEnumerable<Detalle_maestro>> ObtenerDetallesEspecificos(int[] idsDetalleMaestro);
        
       
        Tipo_cambio obtenerTipoDeCambio(DateTime date);
        Maestro obtenerMaestro(int idMaestro);

        /// <summary>
        /// devuelve los detalles que corresponden a los ids de maestros indicados
        /// </summary>
        /// <param name="idsMaestro"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> obtenerDetalles(int[] idsMaestro);
        /// <summary>
        /// devuelve todos los detalles de un maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <returns></returns>
        /// 

        IEnumerable<Detalle_maestro> obtenerDetalles(int idMaestro, string filtroDeBusqueda);


        /// <summary>
        /// devuelve todos los detalles de un maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <returns></returns>
        Task<IEnumerable<Detalle_maestro>> ObtenerDetallesAsync(int idMaestro);
        Task<IEnumerable<Detalle_maestro>> ObtenerDetallesVigentesAsync(int idMaestro);


        /// <summary>
        /// Obtiene la lista de detalle_maestro vigentes
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> ObtenerDetalles(int idMaestro, bool esVigente);
        IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercial(int idMaestro, bool esVigente);
        IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercial(int idMaestro, string valor, bool esVigente);
        IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercialPorRol(int idRol, bool esVigente);
        IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercialPorRol(int idRol, string valor, bool esVigente);
        IEnumerable<Detalle_maestro> ObtenerDetallesVigentesPorValor(int idMaestro, bool esVigente, string valor);


        /// <summary>
        /// Obtiene la lista de detalle_maestro vigentes segun idsMaestros
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> ObtenerDetalles(int[] idMaestro, bool esVigente);



        /// <summary>
        /// devuelve todos los detalles de un maestro hijos de un detalle maestro padre
        /// </summary>
        /// <param name="idDetalleMaestro"></param>
        /// <returns></returns>
        IEnumerable<Detalle_detalle_maestro> obtenerDetalleDetalles(int idDetalleMaestro, string filtroDeBusqueda);


        /// <summary>
        /// Devuelve la lista de detalles de maestro incluyendo el detalle de maestro padre.
        /// Por ejemplo se utiliza, para categorias que utilizan otras categorias:
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <returns></returns>
        Task<IEnumerable<ItemJerarquico>> ObtenerDetallesJerarquicos(int idMaestro);

        IEnumerable<ItemJerarquico> ObtenerDetallesJerarquicosPorRolConceptoNegocio(int idRol);

        #endregion

        #region otros



        List<Ubigeo> obtenerUbigeoDistrito();
        int obtenerNumeroMaximoDetalle(int idMaestroConfiguracionDeSistema);
        List<Detalle_maestro> obtenerDetallesEstados(int[] idsEstados);
        List<Detalle_maestro> obtenerDetallesVariedad(int idDetalle);



        #endregion

        /// <summary>
        /// Obtiene una lista de detale_maestro incluyendo categoria_concepto
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> ObtenerDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro);


        /// <summary>
        /// Obtiene la lista de detalle_maestro vigentes incluyendo categoria_concepto
        /// </summary>
        /// <param name="IdMaestro"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        IEnumerable<Detalle_maestro> ObtenerDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro, bool esVigente);
        IEnumerable<Detalle_maestro> ObtenerTodoLosDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro);

        #region VALIDACIONES
        /// <summary>
        /// Comprueba si existe un detalle_maestro con el mismo nombre segun el id_maestro
        /// </summary>
        /// <param name="idMaestro"></param>
        /// <param name="nombre"></param>
        /// <param name="esVigente"></param>
        /// <returns></returns>
        bool ExisteNombreDeDetalleMaestro(int idMaestro, string nombre, bool esVigente);
        int ObtenerIdDetalleMaestro(int idMaestro, string codigo, bool esVigente);

        /// <summary>
        /// Devuelve el turno al que corresponde una fecha y hora entre todos los turnos del tipo @idTipoTurno
        /// </summary>
        /// <param name="fechaHora"></param>
        /// <param name="idTipoTurno"></param>
        /// <returns></returns>
        Turno ObtenerTurno(DateTime fechaHora, int idTipoTurno);
        List<Turno> ObtenerTurnos(int idTipoDeTurno);

        #endregion
        Tipo_cambio ObtenerTipoCambio(int idMoneda, DateTime fechaActual);
        OperationResult GuardarTipoCambio(Tipo_cambio tipoCambio);
        IEnumerable<Detalle_maestro> ObtenerDetallesPorFamilia(int idFamilia);
    }
}

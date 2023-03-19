using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IConfiguracionLogica
    {
        #region Crear, Actualizar una Configuracion
        /// <summary>
        /// guarda un maestro con los datos de codigo y nombre ingresados como parametros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        OperationResult guardarConfiguracion(string nombre, string descripcion);

        /// <summary>
        /// actualiza el codigo y el nombre de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="decripcion"></param>
        /// <returns></returns>
        OperationResult actualizarConfiguracion(int id, string nombre, string decripcion);
        #endregion

        #region Crear, Actualizar un Parametro de un Configuracion
        /// <summary>
        /// guarda un detalle de maestro
        /// </summary>
        /// <param name="idConfiguracion"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="descripcion"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        OperationResult guardarParametroConfiguracion(int idConfiguracion, string nombre, string tipo, string descripcion, string valor);

        /// <summary>
        /// actualiza un detalle de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idConfiguracion"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="descripcion"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        OperationResult actualizarParametroConfiguracion(int id, int idConfiguracion, string nombre, string tipo, string descripcion, string valor);
        #endregion

        #region consultas

        /// <summary>
        /// devuele una lista de todos los maestros
        /// </summary>
        /// <returns></returns>
        List<Configuracion> obtenerConfiguraciones();

        /// <summary>
        /// devuelve los detalles de un maestro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Parametro_de_configuracion> obtenerParametrosConfiguracion(int idConfiguracion);

        #endregion




        #region Obtener, Crear, Actualizar (Tipo De Comprobante)

        OperationResult CrearTipoDeComprobante(string nombre, string nombreCorto, string codigoSunat, int [] idsTransaccionEmisionPropia, int []idsTransaccionEmisionTerceros);
        TipoDeComprobante ObtenerTipoDeComprobante(int idTipoDeComprobante);
        List<TipoDeComprobante> ObtenerTiposDeComprobante();
        OperationResult ActualizarTipoComprobante(int id,string nombre, string nombreCorto, string codigoSunat,List<TipoDeComprobanteParaTransaccion> tiposDeTransaccionConEmisionPropia, List<TipoDeComprobanteParaTransaccion> tiposDeTransaccionConEmisionTerceros);
        Task<List<ItemGenerico>> ObtenerSeriesConTipoComprobante(TipoComprobantePara tipoOperacion, UserProfileSessionData sesionUsuario);
        Task<List<SelectorTipoDeComprobante>> ObtenerSelectorDeComprobante(TipoComprobantePara tipoOperacion, UserProfileSessionData sesionUsuario);

        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVenta();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVentasYSusNotas();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVenta(int idEmpleado, int idCentroAtencion);
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVentasPorContingencia(int idEmpleado, int idCentroAtencion);
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAnularVenta();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaDescuentoSobreVenta();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaRecargoSobreVenta();
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAnulacionVenta(int idEmpleado, int idCentroAtencion);

        #endregion

        #region Obtener,Crear, Actualizar (Serie De Comprobante)

        OperationResult CrearSerieDeComprobante(int idCentroDeAtencion, int idTipoDeComprobante, string numeroDeSerie, int numeroDeComprobanteSiguiente, bool autonumerica, bool vigente);
        List<SerieDeComprobante> ObtenerSeriesDeComprobante();
        SerieDeComprobante ObtenerSerieDeComprobante(int idSerieDeComprobante);
        OperationResult ActualizarSerieDeComprobante(int id, int idCentroDeAtencion, int idTipoDeComprobante, string numeroDeSerie, int numeroDeComprobanteSiguiente, bool autonumerica, bool vigente);
        bool ExisteNumeroDeSerieDeComprobanteSegunTipoDeComprobante(int idTipoDeComprobante, string numeroDeSerie);
        #endregion

        #region Obtener, Crear, Actualizar (Tipos De Transaccion)
        List<TipoDeTransaccion> ObtenerTiposDeTransaccion();
        OperationResult CrearTipoDeTransaccion(string nombre, string descripcion, int idTransaccionMaestro, List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion);
        TipoDeTransaccion ObtenerTipoDeTransaccion(int idTipoDeTransaccion);
        OperationResult ActualizarTipoDeTransaccion(int id, string nombre, string descripcion, int idTransaccionMaestro, List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion);

        List<AccionDeNegocio> ObtenerAccionesDeNegocio();

        #endregion

        #region Obtener, Crear, Actualizar (Rol)
        OperationResult CrearRol(string nombre, string descripcion, int rolPadre, int aplica);
        List<RolDeNegocio> ObtenerRolesDeNegocio();
        #endregion
        OperacionTipoTransaccionTipoComprobante ObtenerTipoTransaccionTipoComprobanteOperacion(long idOperacion);
    }
}

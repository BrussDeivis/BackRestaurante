using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Logica
{
    public class ConfiguracionLogica : IConfiguracionLogica
    {
        private readonly IConfiguracionRepositorio _repositorioConfiguracion;
        private readonly IMaestroRepositorio _repositorioMaestro;
        private readonly ITransaccionRepositorio _repositorioTransaccion;

        public ConfiguracionLogica(IConfiguracionRepositorio repositorioConfiguracion, IMaestroRepositorio repositorioMaestro, ITransaccionRepositorio repositorioTransaccion)
        {
            _repositorioConfiguracion = repositorioConfiguracion;
            _repositorioMaestro = repositorioMaestro;
            _repositorioTransaccion = repositorioTransaccion;

        }
        public OperationResult guardarConfiguracion(string nombre, string descripcion)
        {
            try
            {
                Configuracion nuevoConfiguracion = new Configuracion();
                nuevoConfiguracion.nombre = nombre;
                nuevoConfiguracion.descripcion = descripcion;
                return _repositorioConfiguracion.crearConfiguracion(nuevoConfiguracion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult actualizarConfiguracion(int id, string nombre, string descripcion)
        {
            try
            {
                Configuracion Configuracion = new Configuracion();
                Configuracion.id = id;
                Configuracion.nombre = nombre;
                Configuracion.descripcion = descripcion;
                return _repositorioConfiguracion.actualizarConfiguracion(Configuracion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult guardarParametroConfiguracion(int idConfiguracion, string nombre, string tipo, string descripcion, string valor)
        {
            try
            {
                Parametro_de_configuracion parametroConfiguracion = new Parametro_de_configuracion();
                parametroConfiguracion.id_configuracion = idConfiguracion;
                parametroConfiguracion.nombre = nombre;
                parametroConfiguracion.tipo = tipo;
                parametroConfiguracion.descripcion = descripcion;
                parametroConfiguracion.valor = valor;

                return _repositorioConfiguracion.crearParametroConfiguracion(parametroConfiguracion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult actualizarParametroConfiguracion(int id, int idConfiguracion, string nombre, string tipo, string descripcion, string valor)
        {
            try
            {
                Parametro_de_configuracion parametroConfiguracion = new Parametro_de_configuracion();
                parametroConfiguracion.id = id;
                parametroConfiguracion.id_configuracion = idConfiguracion;
                parametroConfiguracion.nombre = nombre;
                parametroConfiguracion.tipo = tipo;
                parametroConfiguracion.descripcion = descripcion;
                parametroConfiguracion.valor = valor;
                return _repositorioConfiguracion.actualizarParametroConfiguracion(parametroConfiguracion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Configuracion> obtenerConfiguraciones()
        {
            try { return _repositorioConfiguracion.obtenerConfiguraciones().ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Parametro_de_configuracion> obtenerParametrosConfiguracion(int idConfiguracion)
        {
            try { return _repositorioConfiguracion.obtenerParametrosConfiguracion(idConfiguracion).ToList(); }
            catch (Exception e) { throw e; }
        }





        #region Crear, Obtener, Actualizar (Tipo De Comprobante)
        public OperationResult CrearTipoDeComprobante(string nombre, string nombreCorto, string codigoSunat, int[] idsTransaccionEmisionPropia, int[] idsTransaccionEmisionTerceros)
        {
            try
            {
                Detalle_maestro tipoDeComprobante = new Detalle_maestro();
                tipoDeComprobante.nombre = nombre;
                tipoDeComprobante.valor = nombreCorto;
                tipoDeComprobante.codigo = codigoSunat;
                tipoDeComprobante.id_maestro = MaestroSettings.Default.IdMaestroDocumento;
                tipoDeComprobante.fecha_registro = DateTimeUtil.FechaActual();
                tipoDeComprobante.es_vigente = true;

                foreach (var item in idsTransaccionEmisionPropia)
                {
                    Tipo_transaccion_tipo_comprobante tttc = new Tipo_transaccion_tipo_comprobante();
                    tttc.id_tipo_transaccion = item;
                    tttc.es_propio = true;
                    tttc.Detalle_maestro = tipoDeComprobante;
                    tipoDeComprobante.Tipo_transaccion_tipo_comprobante.Add(tttc);
                }
                foreach (var item in idsTransaccionEmisionTerceros)
                {
                    Tipo_transaccion_tipo_comprobante tttc = new Tipo_transaccion_tipo_comprobante();
                    tttc.id_tipo_transaccion = item;
                    tttc.es_propio = false;
                    tttc.Detalle_maestro = tipoDeComprobante;
                    tipoDeComprobante.Tipo_transaccion_tipo_comprobante.Add(tttc);
                }
                return _repositorioMaestro.crearDetalleMaestro(tipoDeComprobante);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public TipoDeComprobante ObtenerTipoDeComprobante(int idTipoDeComprobante)
        {
            try
            {
                return new TipoDeComprobante(_repositorioConfiguracion.ObtenerDetalleMaestroIncluidoTipoTransaccionTipoComprobante(MaestroSettings.Default.IdMaestroDocumento, idTipoDeComprobante));

            }
            catch (Exception e) { throw e; }
        }
        public List<TipoDeComprobante> ObtenerTiposDeComprobante()
        {
            try
            {
                return TipoDeComprobante.Convert(_repositorioConfiguracion.ObtenerDetallesMaestroIncluidoTipoTransaccionTipoComprobante(MaestroSettings.Default.IdMaestroDocumento).ToList());

            }
            catch (Exception e) { throw e; }
        }
        public OperationResult ActualizarTipoComprobante(int id, string nombre, string nombreCorto, string codigoSunat, List<TipoDeComprobanteParaTransaccion> tiposDeTransaccionConEmisionPropia, List<TipoDeComprobanteParaTransaccion> tiposDeTransaccionConEmisionTerceros)
        {
            try
            {
                Detalle_maestro tipoDeComprobante = new Detalle_maestro();
                tipoDeComprobante.id = id;
                tipoDeComprobante.nombre = nombre;
                tipoDeComprobante.valor = nombreCorto;
                tipoDeComprobante.codigo = codigoSunat;
                tipoDeComprobante.id_maestro = MaestroSettings.Default.IdMaestroDocumento;
                tipoDeComprobante.fecha_registro = DateTimeUtil.FechaActual();

                foreach (var item in tiposDeTransaccionConEmisionPropia)
                {
                    Tipo_transaccion_tipo_comprobante tttc = new Tipo_transaccion_tipo_comprobante();
                    tttc.id = item.Id;
                    tttc.id_tipo_transaccion = item.IdTipoTransaccion;
                    tttc.es_propio = true;
                    tttc.id_tipo_comprobante = tipoDeComprobante.id;
                    tipoDeComprobante.Tipo_transaccion_tipo_comprobante.Add(tttc);
                }
                foreach (var item in tiposDeTransaccionConEmisionTerceros)
                {
                    Tipo_transaccion_tipo_comprobante tttc = new Tipo_transaccion_tipo_comprobante();
                    tttc.id = item.Id;
                    tttc.id_tipo_transaccion = item.IdTipoTransaccion;
                    tttc.es_propio = false;
                    tttc.id_tipo_comprobante = tipoDeComprobante.id;
                    tipoDeComprobante.Tipo_transaccion_tipo_comprobante.Add(tttc);
                }
                return _repositorioConfiguracion.ActualizarDetalleMaestroConTipoTransaccionTipoComprobante(tipoDeComprobante);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Crear, Obtener, Actualizar (Serie De Comprobante)
        public OperationResult CrearSerieDeComprobante(int idCentroDeAtencion, int idTipoDeComprobante, string numeroDeSerie, int numeroDeComprobanteSiguiente, bool autonumerica, bool vigente)
        {
            try
            {

                Serie_comprobante serieDeComprobante = new Serie_comprobante();
                serieDeComprobante.id_propietario = idCentroDeAtencion;
                serieDeComprobante.id_tipo_comprobante = idTipoDeComprobante;
                serieDeComprobante.numero = numeroDeSerie;
                serieDeComprobante.proximo_numero = numeroDeComprobanteSiguiente;
                serieDeComprobante.es_autonumerable = autonumerica;
                serieDeComprobante.es_vigente = vigente;
                //El campo id_modelo_comprobante en futuro se usara 
                serieDeComprobante.id_modelo_comprobante = 1;
                return _repositorioTransaccion.CrearSerieComprobante(serieDeComprobante);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<SerieDeComprobante> ObtenerSeriesDeComprobante()
        {
            try
            {
                return SerieDeComprobante.Convert(_repositorioTransaccion.ObtenerSeriesComprobante().ToList());
            }
            catch (Exception e) { throw e; }

        }
        public SerieDeComprobante ObtenerSerieDeComprobante(int idSerieDeComprobante)
        {
            try
            {
                return new SerieDeComprobante(_repositorioTransaccion.ObtenerSerieDeComprobante(idSerieDeComprobante));
            }
            catch (Exception e) { throw e; }

        }

        public async Task<List<ItemGenerico>> ObtenerSeriesConTipoComprobante(TipoComprobantePara tipoOperacion, UserProfileSessionData sesionUsuario)
        {
            try
            {
                List<SelectorTipoDeComprobante> tiposComprobante = await ObtenerSelectorDeComprobante(tipoOperacion, sesionUsuario);
                List<ItemGenerico> seriesConComprobantes = new List<ItemGenerico>();
                foreach (var tipoDeComprobante in tiposComprobante)
                {
                    foreach (var serie in tipoDeComprobante.Series)
                    {
                        seriesConComprobantes.Add(new ItemGenerico()
                        {
                            Id = serie.Id,
                            Nombre = tipoDeComprobante.TipoComprobante.Codigo + ": " + serie.Nombre
                        });
                    }
                }
                return seriesConComprobantes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }

        public async Task<List<SelectorTipoDeComprobante>> ObtenerSelectorDeComprobante(TipoComprobantePara tipoOperacion, UserProfileSessionData sesionUsuario)
        {
            try
            {
                List<SelectorTipoDeComprobante> tiposComprobante= null;
                if (tipoOperacion== TipoComprobantePara.Venta)
                { 
                var resultados = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? await ObtenerTiposDeComprobanteParaVenta() :
                    await ObtenerTiposDeComprobanteParaVenta(sesionUsuario.Empleado.Id, sesionUsuario.IdCentroDeAtencionSeleccionado);
                tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados) :
                    SelectorTipoDeComprobante.Convert(resultados, sesionUsuario.IdCentroDeAtencionSeleccionado);
                }
                else if(tipoOperacion == TipoComprobantePara.VentasPorContingencia)
                {
                    var resultados = await ObtenerTiposDeComprobanteParaVentasPorContingencia(sesionUsuario.Empleado.Id, sesionUsuario.IdCentroDeAtencionSeleccionado);
                    tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados) :
                        SelectorTipoDeComprobante.Convert(resultados, sesionUsuario.IdCentroDeAtencionSeleccionado);
                }
                else if (tipoOperacion == TipoComprobantePara.ReporteDeVenta)
                {
                    var resultados = await ObtenerTiposDeComprobanteParaVenta();
                    tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados) :
                        SelectorTipoDeComprobante.Convert(resultados);
                }
                else if (tipoOperacion == TipoComprobantePara.VentasYSusNotas)
                {
                    var resultados = await ObtenerTiposDeComprobanteParaVentasYSusNotas();
                    tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados) :
                        SelectorTipoDeComprobante.Convert(resultados);
                }
                else if (tipoOperacion == TipoComprobantePara.SeriesAutonumericasParaVenta)
                {
                    var resultados = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id? await ObtenerTiposDeComprobanteParaVenta() : await ObtenerTiposDeComprobanteParaVenta(sesionUsuario.Empleado.Id, sesionUsuario.IdCentroDeAtencionSeleccionado);
                     tiposComprobante =
                        sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados, true):SelectorTipoDeComprobante.Convert(resultados, sesionUsuario.IdCentroDeAtencionSeleccionado, true);
                }
                else if (tipoOperacion == TipoComprobantePara.SeriesAutonumericasParaVentaExcluidoFactura)
                {
                        var resultados = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? (await ObtenerTiposDeComprobanteParaVenta ()).Where(c => c.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList() :
                            (await ObtenerTiposDeComprobanteParaVenta (sesionUsuario.Empleado.Id, sesionUsuario.IdCentroDeAtencionSeleccionado)).Where(c => c.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList();
                    tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados, true) :
                            SelectorTipoDeComprobante.Convert(resultados, sesionUsuario.IdCentroDeAtencionSeleccionado, true);
                }else
                if (tipoOperacion == TipoComprobantePara.Pedido)
                {
                    var resultados = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? await ObtenerTiposDeComprobanteParaPedido() :
                        await ObtenerTiposDeComprobanteParaPedido(sesionUsuario.Empleado.Id, sesionUsuario.IdCentroDeAtencionSeleccionado);
                    tiposComprobante = sesionUsuario.IdCentroDeAtencionSeleccionado == sesionUsuario.Sede.Id ? SelectorTipoDeComprobante.Convert(resultados) :
                        SelectorTipoDeComprobante.Convert(resultados, sesionUsuario.IdCentroDeAtencionSeleccionado);
                }
                return tiposComprobante;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }


        /// <summary>
        /// Devuelve los tipos de comprobantes incluyendo solo sus series vigentes, devolvera solo las series que empiezen en letra y las que tengan como propietario al <paramref name="idCentroAtencion"/>
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVenta(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && char.IsLetter(sc.numero.FirstOrDefault()) && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaPedido(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(PedidoSettings.Default.IdTipoTransaccionOrdenPedido)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para pedido", e);
            }
        }
        /// <summary>
        /// Devuelve los tipos de comprobante incluyendo solo sus series vigentes, solo devuelve las series que empiezen con una letra (no toma las series de contingencia)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVenta()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => char.IsLetter(sc.numero.FirstOrDefault()) && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaPedido()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(PedidoSettings.Default.IdTipoTransaccionOrdenPedido)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }

        /// <summary>
        /// Devuelve los tipos de comprobantes incluyendo todas sus series (Vigentes y no vigentes)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVentasYSusNotas()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto)
                    .GroupBy(r => new { r.id_tipo_comprobante, r.es_propio }).Select(r => r.FirstOrDefault())
                    .ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }

        /// <summary>
        /// Devuelve los tipos de comprobante incluyendo solo sus series vigentes, solo devuelve las series que empiezen con un numero (series de contingencia), que tengan el propietario a <paramref name="idCentroAtencion"/>
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaVentasPorContingencia(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => char.IsNumber(sc.numero.FirstOrDefault()) && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para ventas por contingencia", e);
            }
        }
       

        //Verificar si se usa este metodo
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAnularVenta()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto).ToList();
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener tipos de comprobantes para anulación de venta", e);
            }
        }

        //Verificar si el metodo todavia se usa
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaAnulacionVenta(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto);
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener tipos de comprobantes para anulación de venta", e);
            }
        }
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaDescuentoSobreVenta()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionDescuentoSobreOrdenDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto).ToList();
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener tipos de comprobantes para descuentos sobre venta", e);
            }
        }

        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaRecargoSobreVenta()
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionDebitoOrdenDeVenta)).Where(r => r.id_tipo_comprobante != MaestroSettings.Default.IdDetalleMaestroComprobantePorDefecto).ToList();
                return TipoDeComprobanteParaTransaccion.Convert(resultado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener tipos de comprobantes para recargos sobre venta", e);

            }
        }






        public OperationResult ActualizarSerieDeComprobante(int id, int idCentroDeAtencion, int idTipoDeComprobante, string numeroDeSerie, int numeroDeComprobanteSiguiente, bool autonumerica, bool vigente)
        {
            try
            {
          
                Serie_comprobante serieDeComprobante = new Serie_comprobante();
                serieDeComprobante.id = id;
                serieDeComprobante.id_propietario = idCentroDeAtencion;
                serieDeComprobante.id_tipo_comprobante = idTipoDeComprobante;
                serieDeComprobante.numero = numeroDeSerie;
                serieDeComprobante.proximo_numero = numeroDeComprobanteSiguiente;
                serieDeComprobante.es_autonumerable = autonumerica;
                serieDeComprobante.es_vigente = vigente;
                serieDeComprobante.id_modelo_comprobante = 1;
                return _repositorioTransaccion.ActualizarSerieComprobante(serieDeComprobante);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExisteNumeroDeSerieDeComprobanteSegunTipoDeComprobante(int idTipoDeComprobante ,string numeroDeSerie)
        {
            return _repositorioTransaccion.ExisteSerieDeComprobanteSegunTipoDeComprobante(idTipoDeComprobante, numeroDeSerie);
        }



        #endregion

        #region Crear, Obtener, Actualizar Tipos De Transaccion


        public List<AccionDeNegocio> ObtenerAccionesDeNegocio()
        {
            try { return AccionDeNegocio.Convert(_repositorioTransaccion.ObtenerAccionesDeNegocio().ToList()); }
            catch (Exception e) { throw e; }
        }


        public List<TipoDeTransaccion> ObtenerTiposDeTransaccion()
        {
            try { return TipoDeTransaccion.Convert(_repositorioTransaccion.ObtenerTiposDeTransaccionIncluidoAccionDeNegocio().ToList()); }
            catch (Exception e) { throw e; }
        }

        public OperationResult CrearTipoDeTransaccion(string nombre, string descripcion, int idTransaccionMaestro, List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion)
        {
            try
            {
                Tipo_transaccion tipoDeTransaccion = new Tipo_transaccion();
                tipoDeTransaccion.nombre = nombre;
                tipoDeTransaccion.descripcion = descripcion;
                tipoDeTransaccion.id_tipo_transaccion_padre = (idTransaccionMaestro == 0) ? (int?)null : idTransaccionMaestro;
                foreach (var item in accionesDeNegocioPorTipoTransaccion)
                {
                    Accion_de_negocio_por_tipo_transaccion adnptt = new Accion_de_negocio_por_tipo_transaccion();
                    adnptt.Tipo_transaccion = tipoDeTransaccion;
                    adnptt.id_accion_de_negocio = item.IdAccionDeNegocio;
                    adnptt.valor = item.Valor;

                    tipoDeTransaccion.Accion_de_negocio_por_tipo_transaccion.Add(adnptt);
                }
                return _repositorioTransaccion.CrearTipoTransaccion(tipoDeTransaccion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TipoDeTransaccion ObtenerTipoDeTransaccion(int IdTipoDeTransaccion)
        {
            try
            {
                return new TipoDeTransaccion(_repositorioTransaccion.ObtenerTipoDeTransaccionIncluidoAccionDeNegocio(IdTipoDeTransaccion));
            }
            catch (Exception e) { throw e; }
        }

        public OperationResult ActualizarTipoDeTransaccion(int Id, string Nombre, string Descripcion, int IdTransaccionMaestro, List<AccionDeNegocioPorTipoTransaccion> AccionesDeNegocioPorTipoTransaccion)
        {
            try
            {
                Tipo_transaccion tipoDeTransaccion = new Tipo_transaccion();
                tipoDeTransaccion.id = Id;
                tipoDeTransaccion.nombre = Nombre;
                tipoDeTransaccion.descripcion = Descripcion;
                tipoDeTransaccion.id_tipo_transaccion_padre = (IdTransaccionMaestro == 0) ? (int?)null : IdTransaccionMaestro;
                foreach (var item in AccionesDeNegocioPorTipoTransaccion)
                {
                    Accion_de_negocio_por_tipo_transaccion adnptt = new Accion_de_negocio_por_tipo_transaccion();
                    adnptt.id = item.Id;
                    adnptt.id_tipo_transaccion = tipoDeTransaccion.id;
                    adnptt.id_accion_de_negocio = item.IdAccionDeNegocio;
                    adnptt.valor = item.Valor;

                    tipoDeTransaccion.Accion_de_negocio_por_tipo_transaccion.Add(adnptt);
                }
                return _repositorioTransaccion.ActualizarTipoTransaccionConAccionNegocio(tipoDeTransaccion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Crear, Obtener  (roles)
        public OperationResult CrearRol(string nombre, string descripcion, int idRolPadre, int aplica)
        {
            try
            {
                Rol rol = new Rol();
                rol.nombre = nombre;
                rol.descripcion = descripcion;
                rol.id_rol_padre = (idRolPadre == 0) ? (int?)null : idRolPadre;
                rol.aplica_a = aplica;
                return _repositorioConfiguracion.CrearRol(rol);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<RolDeNegocio> ObtenerRolesDeNegocio()
        {
            try
            {
                return RolDeNegocio.Convert_(_repositorioConfiguracion.ObtenerRoles().ToList());
            }
            //(_repositorioConfiguracion.ObtenerRoles().ToList()); }
            catch (Exception e) { throw e; }
        }
        #endregion

        public OperacionTipoTransaccionTipoComprobante ObtenerTipoTransaccionTipoComprobanteOperacion(long idOperacion)
        {
            try
            {
                var resultado = _repositorioTransaccion.ObtenerTipoTransaccionTipoComprobanteOperacion(idOperacion);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener tipos de comprobantes para descuentos sobre venta", e);
            }
        }

    }
}

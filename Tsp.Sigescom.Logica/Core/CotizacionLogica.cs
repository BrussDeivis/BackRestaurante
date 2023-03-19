using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica
{
    public class CotizacionLogica : ICotizacionLogica
    {
        private readonly ITransaccionRepositorio _repositorioTransaccion;
        private readonly IPermisos_Logica _permisos_Logica;
        private readonly ICodigosOperacion_Logica _codigosOperacion_Logica;


        public CotizacionLogica()
        {
        }

        public CotizacionLogica(ITransaccionRepositorio repositorioTransaccion, IPermisos_Logica permisos_Logica, ICodigosOperacion_Logica codigosOperacion_Logica)
        {
            _repositorioTransaccion = repositorioTransaccion;
            _permisos_Logica = permisos_Logica;
            _codigosOperacion_Logica = codigosOperacion_Logica;
        }

        //Genera una transaccion cotizacion
        private Transaccion GenerarCotizacion(int idEmpleado, int idTipoDeComprobante, DateTime fechaEmision, DateTime fechaRegistro, DateTime fechaVencimiento, string sufijoCodigo, int idTipoTransaccion, int accionARealizar, int idTipoTransaccionValidar, decimal importe, string observacion, int idCliente, int idCentroAtencion)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1; //_repositorioTransaccion.obtenerTipoDeCambio(fechaRegistro).valorCotizacion;
                //Validamos la accion a realizar
                _permisos_Logica.ValidarAccion(idEmpleado, accionARealizar, idTipoTransaccionValidar, idUnidadNegocio);
                //obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(_repositorioTransaccion.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //obtenemos el codigo
                string codigo = _codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(sufijoCodigo, idTipoTransaccion);
                //crear una cotizacion
                Transaccion cotizacion = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaEmision, fechaVencimiento, observacion, fechaEmision, idEmpleado, importe, idCentroAtencion, idMoneda, tipoDeCambio, null, idCliente);
                return cotizacion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar la cotizacion", e);
            }
        }

        //Genera una transaccion orden de cotizacion 
        private Transaccion GenerarOrdenDeCotizacion(Transaccion cotizacion, int idEmpleado, int idCentroAtencion, DateTime fechaEmision, DateTime fechaRegistro, DateTime fechaVencimiento, string sufijoCodigo, int idOrdenTransaccion, string observacion, int idCliente, string aliasCliente, List<Detalle_transaccion> detalles)
        {
            var gravada = detalles.Sum(d => d.igv) > 0 ? detalles.Sum(d => d.total) - detalles.Sum(d => d.igv) : 0;
            var exonerada = detalles.Sum(d => d.igv) == 0 ? detalles.Sum(d => d.total) : 0;
            var igv = detalles.Sum(d => d.igv);
            //crear una orden de cotizacion
            Transaccion ordenDeCotizacion = new Transaccion(_codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(cotizacion.codigo + "_O" + sufijoCodigo, idOrdenTransaccion), null, fechaRegistro, idOrdenTransaccion, cotizacion.id_unidad_negocio, true, fechaEmision, fechaVencimiento, observacion, fechaEmision, idEmpleado, cotizacion.importe_total, idCentroAtencion, cotizacion.id_moneda, cotizacion.tipo_cambio, null, idCliente, 0, 0, 0, gravada, exonerada, 0, 0, igv, 0, 0, 0, 0);
            //agregamos los detalles
            ordenDeCotizacion.AgregarDetalles(detalles);
            //agregamos el parametro Alias si lo tiene
            if (!String.IsNullOrEmpty(aliasCliente) && !String.IsNullOrWhiteSpace(aliasCliente) && idCliente == ActorSettings.Default.IdClienteGenerico)
            {
                ordenDeCotizacion.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, aliasCliente));
            }
            return ordenDeCotizacion;
        }

        public List<DetalleDeOperacion> ResolverDetalle(List<DetalleDeOperacion> detalles, bool gravaIgv, decimal flete)
        {
            //Si tiene flete se agrega a los detalles de la venta
            if (flete > 0)
            {
                detalles.Add(new DetalleDeOperacion(ConceptoSettings.Default.IdConceptoNegocioFlete, 1, flete, flete, 0, 0, 0, null, null, null, null, false, "110", null));
            }
            //Calculamos el igv 
            foreach (var item in detalles)
            {
                if (item.Cantidad <= 0)
                {
                    throw new LogicaException("No es posible realizar una venta con cantidad 0 en alguno de sus detalles");
                }

                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad))
                {
                    item.Cantidad = item.Importe / item.PrecioUnitario;
                }
                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario))
                {
                    item.PrecioUnitario = item.Importe / item.Cantidad;
                }
                if (VerificarCalculadoMascaraDeCalculo(item.MascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe))
                {
                    item.Importe = item.Cantidad * item.PrecioUnitario;
                }
                //la primera condicion del if es para donde no aplica la ley de la amazonia y la segunda condicion es para la parte de la amazonia las dos solo se graba el igv cuando el documento sea difernte a la nota interna de venta
                if (gravaIgv)
                {
                    item.Igv = item.Importe - (item.Importe / (1 + TransaccionSettings.Default.TasaIGV));
                }
            }
            return detalles;
        }

        private bool VerificarCalculadoMascaraDeCalculo(string mascaraDeCalculo, ElementoDeCalculoEnVentasEnum orden)
        {
            List<int> mascaraDeCalculoArray = mascaraDeCalculo.Select(digito => int.Parse(digito.ToString())).ToList();
            //Retornamos si el valor de mascara es igual a 1
            return !Convert.ToBoolean(mascaraDeCalculoArray[(int)orden]);
        }

        public Comprobante GenerarComprobantePropioAutonumerable(int idSerieComprobante)
        {
            Serie_comprobante serie = _repositorioTransaccion.ObtenerSerieDeComprobante(idSerieComprobante);
            Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
            serie.proximo_numero++;
            _repositorioTransaccion.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
            return comprobante;
        }




        public OperationResult AgregarCotizacion(int idEmpleado, int idCentroAtencion, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, bool gravaIgv, DateTime fechaVencimiento, string observacion, List<DetalleDeOperacion> detalles, decimal flete)
        {
            try
            {
                OperationResult result;
                //Resolvemos los detalles de la cotizacion
                var detallesDeCotizacion = ResolverDetalle(detalles, gravaIgv, flete);
                var detalles_transaccion = detallesDeCotizacion.Select(d => d.DetalleTransaccion()).ToList();
                var fechaActual = DateTimeUtil.FechaActual();
                //Generamos la cotizacion 
                Transaccion cotizacion = GenerarCotizacion(idEmpleado, idTipoComprobante, fechaActual, fechaActual, fechaVencimiento, "CZ", TransaccionSettings.Default.IdTipoTransaccionCotizacion, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion, detalles_transaccion.Sum(d => d.total), observacion, idCliente, idCentroAtencion);
                cotizacion.Comprobante = GenerarComprobantePropioAutonumerable(idSerieComprobante);
                //Generamos la orden de comra
                Transaccion ordenDeCotizacion = GenerarOrdenDeCotizacion(cotizacion, idEmpleado, idCentroAtencion, fechaActual, fechaActual, fechaVencimiento, "CZ", TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion, observacion, idCliente, aliasCliente, detalles_transaccion);
                ordenDeCotizacion.Comprobante = cotizacion.Comprobante;
                //agregamos el estado de la orden por defecto
                Estado_transaccion estadoDeLaOrdenDeCotizacion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al confirmar una cotizacion");
                ordenDeCotizacion.Estado_transaccion.Add(estadoDeLaOrdenDeCotizacion);
                //Agregamos la orden de cotizacion en la cotizacion
                cotizacion.Transaccion1.Add(ordenDeCotizacion);
                //Crear la transaccion
                result = _repositorioTransaccion.CrearTransaccion(cotizacion);
                result.information = new Cotizacion(cotizacion).OrdenDeCotizacion().Id;
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la cotizacion", e);
            }
        }

        public OperationResult EditarCotizacion(long id, long idOrden, long idComprobante, int idEstado, int idEmpleado, int idCentroAtencion, int idCliente, string aliasCliente, int idTipoComprobante, int idSerieComprobante, bool gravaIgv, DateTime fechaVencimiento, string observacion, List<DetalleDeOperacion> detalles, decimal flete)
        {
            try
            {
                OperationResult result;
                //Resolvemos los detalles de la cotizacion
                var detallesDeCotizacion = ResolverDetalle(detalles, gravaIgv, flete);
                var detalles_transaccion = detallesDeCotizacion.Select(d => d.DetalleTransaccion()).ToList();
                var fechaActual = DateTimeUtil.FechaActual();
                //Generamos la cotizacion 
                Transaccion dbCotizacion = _repositorioTransaccion.ObtenerTransaccion(id);
                Transaccion cotizacion = GenerarCotizacion(idEmpleado, idTipoComprobante, fechaActual, fechaActual, fechaVencimiento, "CZ", TransaccionSettings.Default.IdTipoTransaccionCotizacion, MaestroSettings.Default.IdDetalleMaestroAccionConfirmar, TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion, detalles_transaccion.Sum(d => d.total), observacion, idCliente, idCentroAtencion);
                cotizacion.id = id;
                cotizacion.id_comprobante = idComprobante;
                cotizacion.codigo = dbCotizacion.codigo;
                //Generamos la orden de comra
                Transaccion dbOrdenCotizacion = _repositorioTransaccion.ObtenerTransaccion(idOrden);
                Transaccion ordenCotizacion = GenerarOrdenDeCotizacion(cotizacion, idEmpleado, idCentroAtencion, fechaActual, fechaActual, fechaVencimiento, "CZ", TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion, observacion, idCliente, aliasCliente, detalles_transaccion);
                ordenCotizacion.id = idOrden;
                ordenCotizacion.id_transaccion_padre = id;
                ordenCotizacion.id_comprobante = idComprobante;
                ordenCotizacion.id_estado_actual = idEstado;
                ordenCotizacion.codigo = dbOrdenCotizacion.codigo;
                //Agregamos a los detalles de la orden el id de transaccion de la orden a actualizar
                ordenCotizacion.Detalle_transaccion.ToList().ForEach(d => d.id_transaccion = idOrden);
                //Agregamos la orden de cotizacion en la cotizacion
                cotizacion.Transaccion1.Add(ordenCotizacion);
                //Editamos la trnasaccion
                result = _repositorioTransaccion.ActualizarTransaccionTransaccion1DetallesParametro(cotizacion);
                result.information = new Cotizacion(cotizacion).OrdenDeCotizacion().Id;
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al confirmar la cotizacion", e);
            }
        }

        public List<OrdenDeCotizacion> ObtenerOrdenesDeCotizacion(int idEmpleado, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return OrdenDeCotizacion.Convert(_repositorioTransaccion.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion, fechaDesde, fechaHasta).OrderByDescending(t => t.fecha_inicio).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de cotizacion", e);
            }
        }

        public OrdenDeCotizacion ObtenerOrdenDeCotizacion(long idOrdenDeCotizacion)
        {
            try
            {
                return new OrdenDeCotizacion(_repositorioTransaccion.ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(idOrdenDeCotizacion));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Devulve todos los tipos de comprobante incluyendo sus respectivos series vigentes y que tengan como propietario al <paramref name="idCentroAtencion"/>
        /// </summary>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        public async Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaCotizacion(int idCentroAtencion)
        {
            try
            {
                var resultado = (await _repositorioTransaccion.ObtenerTipoComprobantePorTipoDeTransaccion(TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion)).ToList();
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtner tipos de comprobantes para venta", e);
            }
        }

        public string ObtenerAsuntoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeCotizacion ordenDeCotizacion)
        {
            string asunto = sede.Nombre + ", " + ordenDeCotizacion.Comprobante().NombreTipo + " : " + ordenDeCotizacion.Comprobante().NumeroDeSerie + " - " + ordenDeCotizacion.Comprobante().NumeroDeComprobante;
            return asunto;
        }
        public string ObtenerCuerpoDeCorreoElectronico(EstablecimientoComercial sede, OrdenDeCotizacion ordenDeCotizacion)
        {
            string cuerpo = @"<html>
                      <body>
                      <p>" + sede.Nombre + @"</p>
                      <p>" + ordenDeCotizacion.Comprobante().NombreTipo + " : " + ordenDeCotizacion.Comprobante().NumeroDeSerie + " - " + ordenDeCotizacion.Comprobante().NumeroDeComprobante + @" Adjuntado,</p>
                      <p>Saludos.</p>
                      <p>SIGES (Sistema de Gestion Comercial), Un producto de Tech Solutions Perú<br>www.siges.tsolperu.com</br></p>
                      </body>
                      </html>";
            return cuerpo;
        }

    }
}

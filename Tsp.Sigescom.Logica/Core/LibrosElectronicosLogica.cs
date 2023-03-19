using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
//using Tsp.Sigescom.Modelo.Properties;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;

namespace Tsp.Sigescom.Logica
{
    public class LibrosElectronicosLogica : ILibrosElectronicosLogica
    {
        #region building

        private readonly IOperacionLogica _facturacionOperacion;
        private readonly IActorNegocioLogica _actorLogica;
        private readonly ITransaccionRepositorio _transaccionRepositorio;


        public LibrosElectronicosLogica(IOperacionLogica facturacionLogica, IActorNegocioLogica actorLogica, ITransaccionRepositorio transaccionRepositorio)
        {
            _facturacionOperacion = facturacionLogica;
            _actorLogica = actorLogica;
            _transaccionRepositorio = transaccionRepositorio;
        }


        //public OperationResult GenerarLibrosElectronicosRegimenEspecial(int idEmpleado, int idPeriodo)
        //{
        //    var periodo = _eBookRepositorio.ObtenerPeriodo(idPeriodo);
        //    //En caso ya existan registros generados, se debera mostrar un mensaje de error.
        //    var contribuyente = _actorLogica.ObtenerSede();

        //    try
        //    {
        //        ///comprobando contribuyente
        //        var idContribuyente = _eBookRepositorio.ObtenerIdContribuyente(contribuyente.DocumentoIdentidad);
        //        if (idContribuyente < 1)
        //        {
        //            throw new LogicaException("Error al obtener contribuyente. Es posible que el contribuyente no se encuentre registrado en la base de datos de libros electronicos");
        //        }
        //        ///comprobando existencia del libro compras
        //        var idLibroCompras = _eBookRepositorio.ObtenerIdLibro(idContribuyente, LibrosElectronicosSettings.Default.IdTipoEbookCompras);
        //        if (idLibroCompras < 1)
        //        {
        //            throw new LogicaException("Error al obtener el Registro de Compras. Es posible que el contribuyente no tiene configurado el libro en la base de datos de libros electrónicos");
        //        }
        //        ///comprobando existencia del libro ventas
        //        var idLibroVentas = _eBookRepositorio.ObtenerIdLibro(idContribuyente, LibrosElectronicosSettings.Default.IdTipoEBookVentasIngresos);
        //        if (idLibroVentas < 1)
        //        {
        //            throw new LogicaException("Error al obtener el Registro de Ventas. Es posible que el contribuyente no tiene configurado el libro en la base de datos de libros electrónicos");
        //        }

        //        if (_eBookRepositorio.ExistenRegistros(idLibroCompras, idPeriodo))
        //        {
        //            string mensaje = "";
        //            mensaje = "Existen registros de compras para el periodo indicado. Estos deben eliminarse para poder generar los nuevos registros";
        //            if (_eBookRepositorio.ExistenRegistros(idLibroVentas, idPeriodo))
        //            {
        //                mensaje += Environment.NewLine + "Existen registros de ventas para el periodo indicado. Estos deben eliminarse para poder generar los nuevos registros";

        //            }
        //            throw new LogicaException(mensaje);
        //        }
        //        //Compras
        //        //Obtener compras del periodo
        //        List<OperacionDeCompra> operacionesDeCompras = _facturacionOperacion.ObtenerOrdenesYNotasDeCreditoYDebitoDeComprasTributables(idEmpleado, periodo.FechaDesde, periodo.FechaHasta).OrderBy(oc => oc.FechaEmision).ToList();
        //        //Mapear con el viewmodel de compras
        //        List<EbookComprasModel> registrosCompras = EbookComprasModel.Convert(operacionesDeCompras, periodo);
        //        //Convertir a Book
        //        List<BookLog> logsCompras = EbookComprasModel.convert(registrosCompras, periodo.id, idLibroCompras);
        //        //ventas
        //        List<EBookVentasIngresosModel> registrosVentas = null;
        //        if (LibrosElectronicosSettings.Default.ConsolidarBoletasEnRegistroDeVentas)
        //        {
        //            //Mapear con el viewmodel de ventas
        //            registrosVentas = ConsolidarRegistroDeVentas(periodo, idEmpleado);
        //        }
        //        else
        //        {
        //            registrosVentas = EBookVentasIngresosModel.Convert(_facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(idEmpleado, periodo.FechaDesde, periodo.FechaHasta), periodo);
        //        }
        //        //Convertir a Book
        //        List<BookLog> logsVentas = EBookVentasIngresosModel.convert(registrosVentas, periodo.id, idLibroVentas);
        //        logsCompras.AddRange(logsVentas);
        //        //guardar los registros
        //        var guardarLogResult = _eBookRepositorio.GuardarLogs(logsCompras);
        //        ///cambiar el estado de las ordenes a contabilizadas
        //        return guardarLogResult;

        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al intentar generar libros electronicos", e);

        //    }
        //}
        //metodo grande
        //metodo que coonslide las operaciones anteriores
        private List<EBookVentasIngresosModel> ConsolidarRegistroDeVentas(Periodo periodo, int idEmpleado)
        {
            DateTime fechaDesde = new DateTime(Convert.ToInt32(periodo.anio), Convert.ToInt32(periodo.mes), 1);
            DateTime fechaHasta = new DateTime(Convert.ToInt32(periodo.anio), Convert.ToInt32(periodo.mes), DateTime.DaysInMonth(Convert.ToInt32(periodo.anio), Convert.ToInt32(periodo.mes))).AddDays(1).AddMilliseconds(-1);
            //inicializamos la lista de registros
            List<EBookVentasIngresosModel> registros = new List<EBookVentasIngresosModel>();
            //Todo: Conseguir las operacioens regiatradas en este perido que las fecha, pero fecha de inicio corresponda a un periodo anterior 
            //Se consigue las operaciones anuladas del periodo
            List<OperacionDeVenta> operacionesInvalidadas = _facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributables(idEmpleado, fechaDesde, fechaHasta);
            //Se consiguen las operaciones con factura y notas de credito y debito
            List<OperacionDeVenta> operacionesDeVentasSinBoleta = _facturacionOperacion.ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta);
            var idsSeries = _facturacionOperacion.ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta();
            int correlativo = 1;
            var anyo = Convert.ToInt32(periodo.anio);
            var mes = Convert.ToInt32(periodo.mes);
            //agregamos todas las operaciones sin boleta... el correlativo ya no es importante... se tiene que recrear al final
            registros.AddRange(EBookVentasIngresosModel.Convert(operacionesDeVentasSinBoleta, periodo, correlativo));
            //agregamos todas las operaciones sin boleta que hayan sido invalidadas
            registros.AddRange(EBookVentasIngresosModel.Convert(operacionesInvalidadas.Where(oi => oi.Comprobante().IdTipo != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta).ToList(), periodo, correlativo));
            foreach (var idSerie in idsSeries)
            {
                Int64 primerNumeroDeComprobanteDelPeriodo = _facturacionOperacion.ObtenerNumeroDeComprobanteDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta, idSerie);//inicializamos con el numero de comprobante de la ultima transaccion del periodo anterior.
                Int64 maximoNumeroDeComprobanteDelPeriodo = _facturacionOperacion.ObtenerNumeroDeComprobanteDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta, idSerie);//inicializamos con el mumero de comprobante de la ultima transaccion del periodo anterior.
                
                var invalidadasDeLaSerie = operacionesInvalidadas.Where(id => id.Comprobante().IdSerie == idSerie).ToList();
                Int64 ultimoNumeroDeComprobanteRegistrado = primerNumeroDeComprobanteDelPeriodo - 1;//inicializamos con el numero de comprobante de la ultima transaccion del periodo anterior.
                if (invalidadasDeLaSerie.Count() > 0)
                {
                    for (int j = 0; j < invalidadasDeLaSerie.Count(); j++)
                    {
                        var invalidadaAnterior = j > 0 ? invalidadasDeLaSerie.ElementAt(j - 1) : null;
                        var invalidadaActual = invalidadasDeLaSerie.ElementAt(j);
                        var invalidadaSiguiente = (j + 1) < invalidadasDeLaSerie.Count() ? invalidadasDeLaSerie.ElementAt(j + 1) : null;
                        var fechaDeLaInvalidadaAnterior = invalidadaAnterior != null ? (DateTime?)invalidadaAnterior.FechaEmision : null;
                        //registrar las anteriores a la anulada
                        var ventasConfirmadasDeLaSerieAntesDeLaInvalidada = _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorAFechaDesdeHasta(idEmpleado, idSerie, ultimoNumeroDeComprobanteRegistrado, invalidadaActual.Comprobante().NumeroDeComprobante, fechaDesde, fechaHasta);
                        //: _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado,invalidadaActual.Id);
                        if (ventasConfirmadasDeLaSerieAntesDeLaInvalidada != null && ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Count > 0)
                        {
                            registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadasDeLaSerieAntesDeLaInvalidada, periodo, correlativo));
                            ultimoNumeroDeComprobanteRegistrado = ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Last().NumeroFinal;
                            correlativo += ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Count();
                        }
                        //registrar la anulada 
                        registros.Add(new EBookVentasIngresosModel(invalidadaActual, periodo, correlativo));
                        ultimoNumeroDeComprobanteRegistrado = invalidadaActual.Comprobante().NumeroDeComprobante;
                        correlativo++;
                        //registrar las posteriores a la anulada
                        var ventasConfirmadaDeLaSerieDespuesDeLaInvalidada =
                            invalidadaSiguiente != null ?
                            _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorA(idEmpleado, idSerie, ultimoNumeroDeComprobanteRegistrado, invalidadaSiguiente.Comprobante().NumeroDeComprobante)
                            : _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorA(idEmpleado, idSerie, ultimoNumeroDeComprobanteRegistrado, maximoNumeroDeComprobanteDelPeriodo + 1);
                        if (ventasConfirmadaDeLaSerieDespuesDeLaInvalidada != null && ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Count > 0)
                        {
                            registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadaDeLaSerieDespuesDeLaInvalidada, periodo, correlativo));
                            ultimoNumeroDeComprobanteRegistrado = ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Last().NumeroFinal;
                            correlativo += ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Count();
                        }
                    }
                }
                else
                {
                    var ventasConfirmadasDeLaSerie = _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConNumeroDeComprobanteMayorAYMenorA(idEmpleado, idSerie, ultimoNumeroDeComprobanteRegistrado, maximoNumeroDeComprobanteDelPeriodo + 1);
                    if (ventasConfirmadasDeLaSerie != null && ventasConfirmadasDeLaSerie.Count > 0)
                    {
                        registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadasDeLaSerie, periodo, correlativo));
                        ultimoNumeroDeComprobanteRegistrado = ventasConfirmadasDeLaSerie.Last().NumeroFinal;
                        correlativo += ventasConfirmadasDeLaSerie.Count();
                    }
                }
            }
            registros = registros.OrderBy(r => r.FechaEmisionComprobantePago).ThenBy(r => r.NumeroSerieComprobantePagoODocumento).ToList();
            correlativo = 1;
            registros.ForEach(r=> r.Correlativo= correlativo++);
            return registros;
        }

        public List<ReporteVentaCliente> ObtenerRegistrosDeVenta(Periodo periodo, int idEmpleado)
        {
            try
            {
                List<EBookVentasIngresosModel> registrosVentas = null;
                if (LibrosElectronicosSettings.Default.ConsolidarBoletasEnRegistroDeVentas)
                {
                    registrosVentas = ConsolidarRegistroDeVentas(periodo, idEmpleado);
                }
                else
                {
                    registrosVentas = EBookVentasIngresosModel.Convert(_facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(idEmpleado, periodo.FechaDesde, periodo.FechaHasta), periodo);
                    //Modificar los metodos 
                }
                List<ReporteVentaCliente> resultado = ReporteVentaCliente.Convert(registrosVentas);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar libros electronicos", e);
            }
        }

        public List<DetalleLibroVentasIngresos> ObtenerLibroElectronicoDeVentasEIngresos(Periodo periodo, int idEmpleado)
        {
            try
            {
                List<DetalleLibroVentasIngresos> resultado = DetalleLibroVentasIngresos.Convert(_facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(idEmpleado, periodo.FechaDesde, periodo.FechaHasta), periodo); 
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar libros electronicos de ventas e ingresos", e);

            }
        }

       
        //private List<EBookVentasIngresosModel> ConsolidarRegistroDeVentas(Period periodo, int idEmpleado)
        //{
        //    DateTime fechaDesde = new DateTime(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month), 1);
        //    DateTime fechaHasta = new DateTime(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month),
        //        DateTime.DaysInMonth(Convert.ToInt32(periodo.year),
        //        Convert.ToInt32(periodo.month))).AddDays(1).AddMilliseconds(-1);

        //    //inicializamos la lista de registros
        //    List<EBookVentasIngresosModel> registros = new List<EBookVentasIngresosModel>();

        //    //Se consigue las operaciones anuladas del periodo
        //    List<OperacionDeVenta> operacionesInvalidadas = _facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributables(idEmpleado, fechaDesde, fechaHasta);
        //    //Se consiguen las operaciones con factura y notas de credito y debito
        //    List<OperacionDeVenta> operacionesDeVentasSinBoleta = _facturacionOperacion.ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta);
        //    var idsSeries = _facturacionOperacion.ObtenerIdsDeSeriesDeComprobantesActivasParaBoletasDeVenta();

        //    int correlativo = 1;
        //    var anyo = Convert.ToInt32(periodo.year);
        //    var mes = Convert.ToInt32(periodo.month);

        //    //agregamos todas las operaciones sin boleta... el correlativo ya no es importante... se tiene que recrear al final
        //    registros.AddRange(EBookVentasIngresosModel.Convert(operacionesDeVentasSinBoleta, periodo, correlativo));

        //    foreach (var idSerie in idsSeries)
        //    {
        //        Int64 primerIdDelPeriodo = _facturacionOperacion.ObtenerIdDePrimeraOrdenConBoletaDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta, idSerie);//inicializamos con el id de la ultima transaccion del periodo anterior.
        //        Int64 maximoIdDelPeriodo = _facturacionOperacion.ObtenerIdDeUltimaOrdenConBoletaDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta, idSerie); ;//inicializamos con el id de la ultima transaccion del periodo anterior.

        //        var invalidadasDeLaSerie = operacionesInvalidadas.Where(id => id.Comprobante().IdSerie == idSerie);
        //        Int64 ultimoIdRegistrado = primerIdDelPeriodo - 1;//inicializamos con el id de la ultima transaccion del periodo anterior.

        //        if (invalidadasDeLaSerie.Count() > 0)
        //        {
        //            for (int j = 0; j < invalidadasDeLaSerie.Count(); j++)
        //            {
        //                var invalidadaAnterior = j > 0 ? invalidadasDeLaSerie.ElementAt(j - 1) : null;
        //                var invalidadaActual = invalidadasDeLaSerie.ElementAt(j);
        //                var invalidadaSiguiente = (j + 1) < invalidadasDeLaSerie.Count() ? invalidadasDeLaSerie.ElementAt(j + 1) : null;
        //                var fechaDeLaInvalidadaAnterior = invalidadaAnterior != null ? (DateTime?)invalidadaAnterior.FechaEmision : null;


        //                //registrar las anteriores a la anulada
        //                var ventasConfirmadasDeLaSerieAntesDeLaInvalidada =

        //                    _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado, invalidadaActual.Id);
        //                //: _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado,invalidadaActual.Id);
        //                if (ventasConfirmadasDeLaSerieAntesDeLaInvalidada != null && ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Count > 0)
        //                {
        //                    registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadasDeLaSerieAntesDeLaInvalidada, periodo, correlativo));
        //                    ultimoIdRegistrado = ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Last().UltimoId;
        //                    correlativo += ventasConfirmadasDeLaSerieAntesDeLaInvalidada.Count();
        //                }
        //                //registrar la anulada
        //                registros.Add(new EBookVentasIngresosModel(invalidadaActual, periodo, correlativo));
        //                ultimoIdRegistrado = invalidadaActual.Id;
        //                correlativo++;
        //                //registrar las posteriores a la anulada
        //                var ventasConfirmadaDeLaSerieDespuesDeLaInvalidada =
        //                    invalidadaSiguiente != null ?
        //                    _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado, invalidadaSiguiente.Id)
        //                    : _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado, maximoIdDelPeriodo + 1);
        //                if (ventasConfirmadaDeLaSerieDespuesDeLaInvalidada != null && ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Count > 0)
        //                {
        //                    registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadaDeLaSerieDespuesDeLaInvalidada, periodo, correlativo));
        //                    ultimoIdRegistrado = ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Last().UltimoId;
        //                    correlativo += ventasConfirmadaDeLaSerieDespuesDeLaInvalidada.Count();

        //                }

        //            }
        //        }
        //        else
        //        {
        //            var ventasConfirmadasDeLaSerie =

        //                   _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado, maximoIdDelPeriodo + 1);
        //            //: _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, ultimoIdRegistrado,invalidadaActual.Id);
        //            if (ventasConfirmadasDeLaSerie != null && ventasConfirmadasDeLaSerie.Count > 0)
        //            {
        //                registros.AddRange(EBookVentasIngresosModel.Convert(ventasConfirmadasDeLaSerie, periodo, correlativo));
        //                ultimoIdRegistrado = ventasConfirmadasDeLaSerie.Last().UltimoId;
        //                correlativo += ventasConfirmadasDeLaSerie.Count();
        //            }
        //        }
        //    }
        //    registros = registros.OrderBy(r => r.FechaEmisionComprobantePago).ThenBy(r => r.NumeroSerieComprobantePagoODocumento).ToList();
        //    correlativo = 1;
        //    foreach (var item in registros)
        //    {
        //        item.Correlativo = correlativo++;
        //    }

        //    return registros;

        //}


        //private List<EBookVentasIngresosModel> ConsolidarRegistroDeVentas_(Period periodo, int idEmpleado)
        //{
        //    DateTime fechaDesde = new DateTime(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month), 1);
        //    DateTime fechaHasta = new DateTime(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month),
        //        DateTime.DaysInMonth(Convert.ToInt32(periodo.year),
        //        Convert.ToInt32(periodo.month))).AddDays(1).AddMilliseconds(-1);

        //    //inicializamos la lista de registros
        //    List<EBookVentasIngresosModel> registros = new List<EBookVentasIngresosModel>();

        //    //Se consigue las operaciones anuladas del periodo
        //    List<OperacionDeVenta> operacionesInvalidadas = _facturacionOperacion.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasInvalidadasYTributables(idEmpleado, fechaDesde, fechaHasta);
        //    //Se consiguen las operaciones con factura y notas de credito y debito
        //    List<OperacionDeVenta> operacionesDeVentasSinBoleta = _facturacionOperacion.ObtenerOrdenesConFacturaYNotasDeCreditoYDebitoDeVentasConfirmadasYTributables(idEmpleado, fechaDesde, fechaHasta);
        //    var idsSeries = _facturacionOperacion.ObtenerIdsDeSeriesDeComprobantesActivasParaBoletasDeVenta();
        //    Int64 ultimoId = 0;

        //    int correlativo = 1;
        //    var anyo = Convert.ToInt32(periodo.year);
        //    var mes = Convert.ToInt32(periodo.month);

        //    for (int i = 1; i <= DateTime.DaysInMonth(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month)); i++)
        //    {
        //        //conseguimos las operaciones sin boleta confirmadas
        //        var sinBoletaDelDia = operacionesDeVentasSinBoleta.Where(ov => ov.FechaEmision.Day == i).ToList();
        //        //le añadimos las invalidadas 
        //        sinBoletaDelDia.AddRange(operacionesInvalidadas.Where(ov => ov.FechaEmision.Day == i));
        //        //ordenamos por fecha
        //        sinBoletaDelDia = sinBoletaDelDia.OrderBy(ov => ov.Comprobante().NumeroDeSerie).OrderBy(ov => ov.Comprobante().NumeroDeComprobante).ToList();
        //        //registramos facturas y notas del dia i
        //        registros.AddRange(EBookVentasIngresosModel.Convert(sinBoletaDelDia, periodo, correlativo));
        //        //ultimoIdTransaccionRegistrado = sinBoletaDelDia.Max(b => b.Id);
        //        correlativo += sinBoletaDelDia.Count;
        //        //registramos boletas de forma consolidada:
        //        //conseguir todas las operaciones invalidadas para el dia i.  Agrupadas por serie
        //        var invalidadasEnElDia = operacionesInvalidadas.Where(ov => ov.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && ov.FechaEmision.Day == i).OrderBy(ov => ov.Comprobante().Id).ToArray();
        //        // siguiente anulada por registrar

        //        var fechaInicioDelDia = new DateTime(Convert.ToInt32(periodo.year), Convert.ToInt32(periodo.month), i);
        //        var fechaFinDelDia = fechaInicioDelDia.AddDays(1).AddMilliseconds(-1);

        //        //    por cada serie
        //        foreach (var idSerie in idsSeries)
        //        {
        //            //      conseguimos las invalidadas de la serie
        //            var invalidadasDeLaSerie = invalidadasEnElDia.Where(id => id.Comprobante().IdSerie == idSerie);

        //            ///si hay invalidadas, proceso por bloques
        //            ///Bloque 1: confirmadas antes de la anulada
        //            ///BLoque2: anulada
        //            ///Bloque 3: confirmadas despues de la anulada
        //            if (invalidadasDeLaSerie.Count() > 0)
        //            {
        //                //por cada anulada de esa serie
        //                for (int j = 0; j < invalidadasDeLaSerie.Count(); j++)
        //                {
        //                    var invalidadaAnterior = j > 0 ? invalidadasDeLaSerie.ElementAt(j - 1) : null;
        //                    var invalidadaActual = invalidadasDeLaSerie.ElementAt(j);
        //                    var invalidadaSiguiente = (j + 1) < invalidadasDeLaSerie.Count() ? invalidadasDeLaSerie.ElementAt(j + 1) : null;

        //                    //registrar las anteriores a la anulada
        //                    var ventaConfirmadasDeLaSerieAntesDeLaInvalidada =
        //                        invalidadaAnterior != null ?
        //                        _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia, invalidadaAnterior.Id, invalidadaActual.Id)
        //                        : _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMenorA(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia, invalidadaActual.Id);
        //                    if (ventaConfirmadasDeLaSerieAntesDeLaInvalidada != null)
        //                    {
        //                        registros.Add(new EBookVentasIngresosModel(ventaConfirmadasDeLaSerieAntesDeLaInvalidada, periodo, correlativo));
        //                        ultimoId = ventaConfirmadasDeLaSerieAntesDeLaInvalidada.UltimoId;
        //                        correlativo++;
        //                    }
        //                    //registrar la anulada
        //                    registros.Add(new EBookVentasIngresosModel(invalidadaActual, periodo, correlativo));
        //                    ultimoId = invalidadaActual.Id;
        //                    correlativo++;
        //                    //registrar las posteriores a la anulada
        //                    var ventaConfirmadaDeLaSerieDespuesDeLaInvalidada =
        //                        invalidadaSiguiente != null ?
        //                        _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorAYMenorA(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia, invalidadaActual.Id, invalidadaSiguiente.Id)
        //                        : _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorA(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia, invalidadaActual.Id);
        //                    if (ventaConfirmadaDeLaSerieDespuesDeLaInvalidada != null)
        //                    {
        //                        registros.Add(new EBookVentasIngresosModel(ventaConfirmadaDeLaSerieDespuesDeLaInvalidada, periodo, correlativo));
        //                        ultimoId = ventaConfirmadaDeLaSerieDespuesDeLaInvalidada.UltimoId;
        //                        correlativo++;
        //                    }
        //                }
        //            }
        //            else// si no hay invalidadas, proceso las confirmadas en un solo registro
        //            {
        //                //var ventasConfirmadasDelDia = _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadas(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia);
        //                var ventasConfirmadasDelDia = _facturacionOperacion.ObtenerOrdenesDeVentaConfirmadasYTributablesConsolidadasConIdMayorA(idEmpleado, idSerie, fechaInicioDelDia, fechaFinDelDia, ultimoId);

        //                if (ventasConfirmadasDelDia != null)
        //                {
        //                    registros.Add(new EBookVentasIngresosModel(ventasConfirmadasDelDia, periodo, correlativo));
        //                    ultimoId = ventasConfirmadasDelDia.UltimoId;
        //                    correlativo++;
        //                }
        //            }
        //        }
        //    }
        //    return registros;

        //}

        //private int AgregarTransaccionesConsolidadasARegistros(List<Transaccion_consolidada> transaccionesConsolidadas, List<EBookVentasIngresosModel> registros, Periodo periodo, int correlativo)
        //{
        //    foreach (var tc in transaccionesConsolidadas)
        //    {
        //        registros.Add(new EBookVentasIngresosModel(tc, periodo, correlativo));
        //        correlativo++;
        //    }
        //    return correlativo;
        //}



        //public List<MonthlyBookReport> ObtenerLibrosElectronicos__(int idEmpleado, int idPeriodo)
        //{
        //    ///todo: validar si el empleado tiene los permisos para obtener los libros electrónicos
        //    var periodo= _eBookRepositorio.ObtenerPeriodo(idPeriodo);

        //    var contribuyente = _actorLogica.ObtenerSede();
        //    //Obtener todos los tipos de libros a los que esta obligado el contribuyente
        //    var tiposLibrosAPresentar= _eBookRepositorio.ObtenerTiposDeLibros(contribuyente.DocumentoIdentidad);
        //    // Creamos una coleccion de reportes mensuales que serán presentados a SUNAT.
        //    List<MonthlyBookReport> reportesAPresentar = new List<MonthlyBookReport>();
        //    ///Por cada tipo de libro...

        //        var reporteMensual = new MonthlyBookReport
        //        {
        //            BookType = tiposLibrosAPresentar.SingleOrDefault(tl=>tl.id== LibrosElectronicosSettings.Default.IdTipoEBookVentasIngresos),
        //            Period = periodo,
        //            Ruc = contribuyente.DocumentoIdentidad,
        //            //se obtienen los registros correspondientes
        //            Logs = this.GenerarLibrosElectronicosRegimenEspecial_(idEmpleado, idPeriodo).ToList() 
        //        };
        //        reportesAPresentar.Add(reporteMensual);



        //public List<MonthlyBookReport> ObtenerLibrosElectronicos(int idEmpleado, int idPeriodo)
        //{
        //    var periodo = _eBookRepositorio.ObtenerPeriodo(idPeriodo);

        //    var contribuyente = _actorLogica.ObtenerSede();
        //    //Obtener todos los tipos de libros a los que esta obligado el contribuyente
        //    var tiposLibrosAPresentar = _eBookRepositorio.ObtenerTiposDeLibros(contribuyente.DocumentoIdentidad);
        //    // Creamos una coleccion de reportes mensuales que serán presentados a SUNAT.
        //    List<MonthlyBookReport> reportesAPresentar = new List<MonthlyBookReport>();
        //    ///Por cada tipo de libro...
        //    foreach (var tipoLibro in tiposLibrosAPresentar)
        //    {
        //        ///se crea un reporte mensual
        //        var reporteMensual = new MonthlyBookReport
        //        {
        //            BookType = tipoLibro,
        //            Period = periodo,
        //            Ruc = contribuyente.DocumentoIdentidad,
        //            //se obtienen los registros correspondientes
        //            Logs = _eBookRepositorio.ObtenerLogs(contribuyente.DocumentoIdentidad, tipoLibro.id, idPeriodo).ToList()
        //        };
        //        reportesAPresentar.Add(reporteMensual);
        //    }

        //    return reportesAPresentar;
        //}

        #endregion


        public List<Periodo> ObtenerPeriodos()
        {
            try
            {
                return _transaccionRepositorio.ObtenerPeriodos();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Periodo ObtenerPeriodo(int idPeriodo)
        {
            try
            {
                return _transaccionRepositorio.ObtenerPeriodo(idPeriodo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Periodo ObtenerPeriodo(string nombrePeriodo)
        {
            try
            {
                return _transaccionRepositorio.ObtenerPeriodo(nombrePeriodo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}

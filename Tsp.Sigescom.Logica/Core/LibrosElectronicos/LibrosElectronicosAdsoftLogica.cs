using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;

namespace Tsp.Sigescom.Logica
{
    public class LibrosElectronicosAdsoftLogica : ILibrosElectronicosAdsoftLogica
    {
        protected readonly ILibrosElectronicosAdsoftRepositorio _librosElectronicosAdsoftDatos;
        protected readonly ITransaccionRepositorio _transaccionRepositorio;
        public LibrosElectronicosAdsoftLogica(ILibrosElectronicosAdsoftRepositorio librosElectronicosAdsoftDatos, ITransaccionRepositorio transaccionRepositorio)
        {
            _librosElectronicosAdsoftDatos = librosElectronicosAdsoftDatos;
            _transaccionRepositorio = transaccionRepositorio;
        }

        public List<ReporteVentaClienteAdsoft> ObtenerLibrosElectronicosAdsoft(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<VentaClienteAdsoft> registros = new List<VentaClienteAdsoft>();
                List<VentaClienteAdsoft> ventasDelPeriodo = ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(fechaDesde, fechaHasta).OrderBy(vp => vp.FechaEmision).ThenBy(vp => vp.NumeroSerie).ThenBy(vp => vp.NumeroComprobante).ToList();
                List<VentaClienteAdsoft> ventasDelPeriodoConBoleta = ventasDelPeriodo.Where(vp => vp.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta).OrderBy(vp => vp.FechaEmision).ThenBy(vp => vp.NumeroSerie).ThenBy(vp => vp.NumeroComprobante).ToList();
                List<int> idsSeries = ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta();
                foreach (var idSerie in idsSeries)
                {
                    List<VentaClienteAdsoft> ventasDeLaSerie = ventasDelPeriodoConBoleta.Where(vi => vi.IdSerie == idSerie).OrderBy(vp => vp.FechaEmision).ThenBy(vp => vp.NumeroSerie).ThenBy(vp => vp.NumeroComprobante).ToList();

                    if (ventasDeLaSerie.Count() > 0)
                    {
                        int contVentasDeLaSerie = 0;
                        int cantidadDeVentasDeLaSerie = ventasDeLaSerie.Count();
                        bool terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha = false;
                        int cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = 0;

                        do
                        {
                            List<VentaClienteAdsoft> ventasAgrupadasPorTipoAgrupamiento = new List<VentaClienteAdsoft>();
                            List<VentaClienteAdsoft> itemVentasDeLaSerie = null;
                            itemVentasDeLaSerie = ventasDeLaSerie.Skip(contVentasDeLaSerie).Take(1).ToList();
                            List<VentaClienteAdsoft> ventasDeLaSeriePorFecha = ventasDeLaSerie.Where(vsf => vsf.FechaEmision == itemVentasDeLaSerie.First().FechaEmision).ToList();

                            int cantidadDeVentasDeLaSeriePorFecha = ventasDeLaSeriePorFecha.Count();
                            int contVentasDeLaSeriePorFecha = terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha ? 0 : cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha;
                            ventasAgrupadasPorTipoAgrupamiento.Add(itemVentasDeLaSerie.First());
                            List<VentaClienteAdsoft> element = ventasDeLaSeriePorFecha.Where(vs => vs.NumeroComprobante == 353).ToList();

                            bool consolido = false;
                            while (contVentasDeLaSeriePorFecha < cantidadDeVentasDeLaSeriePorFecha - 1)
                            {
                                var item = ventasDeLaSeriePorFecha.Skip(contVentasDeLaSeriePorFecha + 1).Take(1).ToList();
                                if (itemVentasDeLaSerie.First().IdActorNegocioExterno == item.First().IdActorNegocioExterno && itemVentasDeLaSerie.First().TipoAgrupamiento == item.First().TipoAgrupamiento)
                                {
                                    ventasAgrupadasPorTipoAgrupamiento.Add(item.First());
                                    contVentasDeLaSeriePorFecha++;
                                    contVentasDeLaSerie++;
                                }
                                else
                                {
                                    terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha = contVentasDeLaSeriePorFecha == cantidadDeVentasDeLaSeriePorFecha - 1;
                                    contVentasDeLaSeriePorFecha++;
                                    contVentasDeLaSerie++;
                                    List<VentaClienteAdsoft> ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(ventasAgrupadasPorTipoAgrupamiento);
                                    consolido = true;
                                    registros.AddRange(ventasAgrupadasConsolidadas);
                                    break;
                                }
                            }
                            if (contVentasDeLaSeriePorFecha == cantidadDeVentasDeLaSeriePorFecha - 1)
                            {
                                terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha = true;
                                contVentasDeLaSerie++;
                                cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = contVentasDeLaSeriePorFecha;
                                List<VentaClienteAdsoft> ventasAgrupadasConsolidadas;
                                if (consolido)
                                {
                                    var item = ventasDeLaSeriePorFecha.Skip(contVentasDeLaSeriePorFecha).Take(1).ToList();
                                    ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(item);
                                }
                                else
                                {
                                    ventasAgrupadasConsolidadas = ConsolidadarVentasCliente(ventasAgrupadasPorTipoAgrupamiento);
                                }
                                registros.AddRange(ventasAgrupadasConsolidadas);
                            }
                            else
                            {
                                cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha = contVentasDeLaSeriePorFecha;
                            }
                        } while (contVentasDeLaSerie < cantidadDeVentasDeLaSerie);
                    }
                }
                List<VentaClienteAdsoft> ventasConFactura = ventasDelPeriodo.Where(v => !v.EsInvalidada && v.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList();
                registros.AddRange(ventasConFactura);
                List<VentaClienteAdsoft> ventasInvalidadasConFactura = ventasDelPeriodo.Where(v => v.EsInvalidada && v.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura).ToList();
                registros.AddRange(ventasInvalidadasConFactura);
                List<VentaClienteAdsoft> ventasConNotasDeCreditoYDebito = ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas(fechaDesde, fechaHasta);
                registros.AddRange(ventasConNotasDeCreditoYDebito);
                registros = registros.OrderBy(r => r.NumeroSerie).ThenBy(r => r.NumeroComprobante).ThenBy(r => r.FechaEmision).ThenBy(r => r.IdTipoComprobante).ToList();
                var reporteVentaClienteAdsoft = ReporteVentaClienteAdsoft.Convert(registros);
                return reporteVentaClienteAdsoft;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo consolidar registro de ventas", e);
            }
        }

        private List<int> ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta()
        {
            return _transaccionRepositorio.ObtenerIdsSeriesComprobantes(new int[] { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta }).ToList();
        }

        private List<VentaClienteAdsoft> ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = _librosElectronicosAdsoftDatos.ObtenerVentasCliente(Diccionario.TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, fechaDesde, fechaHasta).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas", e);
            }
        }

        private List<VentaClienteAdsoft> ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = _librosElectronicosAdsoftDatos.ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito, fechaDesde, fechaHasta).OrderBy(t => t.FechaEmision).ThenBy(t => t.NumeroSerie).ThenBy(t => t.Id).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener ventas invalidadas que sean con notas de credito y debito ", e);
            }
        }

        private List<VentaClienteAdsoft> ConsolidadarVentasCliente(List<VentaClienteAdsoft> ventasAgrupadas)
        {

            List<VentaClienteAdsoft> ventasConsolidadas = ventasAgrupadas
                .GroupBy(vcs => new
                {
                    Fecha = new { y = vcs.FechaEmision.Year, m = vcs.FechaEmision.Month, d = vcs.FechaEmision.Day },
                    vcs.IdSerie,
                    vcs.NumeroSerie,
                    vcs.CodigoComprobante,
                    vcs.IdTipoComprobante,
                    vcs.CodigoMoneda,
                })
                .Select(vcc => new VentaClienteAdsoft()
                {
                    Anyo = vcc.Key.Fecha.y,
                    Mes = vcc.Key.Fecha.m,
                    Dia = vcc.Key.Fecha.d,
                    IdTipoComprobante = vcc.Key.IdTipoComprobante,
                    CodigoComprobante = vcc.Key.CodigoComprobante,
                    IdSerie = vcc.Key.IdSerie,
                    NumeroSerie = vcc.Key.NumeroSerie,
                    NumeroComprobante = (int)vcc.Min(vc => vc.NumeroComprobante),
                    NumeroInicial = (int)vcc.Min(vc => vc.NumeroComprobante),
                    NumeroFinal = (int)vcc.Max(vc => vc.NumeroComprobante),
                    ValorIgv = vcc.Sum(vc => (decimal)vc.Igv),
                    ValorVenta = vcc.Sum(vc => vc.ValorDeVenta),
                    Total = vcc.Sum(vc => vc.ImporteTotal),
                    ValorIcbper = vcc.Sum(vc => vc.ValorIcbper),
                    PrimerNombre = vcc.FirstOrDefault().PrimerNombre,
                    IdTipoDocumento = vcc.FirstOrDefault().IdTipoDocumento,
                    NumeroDocumento = vcc.FirstOrDefault().NumeroDocumento,
                    CodigoMoneda = vcc.Key.CodigoMoneda,
                    IdEstadoActual = vcc.FirstOrDefault().IdEstadoActual,
                    IdEstadoAnteriorAlActual = vcc.FirstOrDefault().IdEstadoAnteriorAlActual
                })
                .OrderBy(t => t.FechaEmision).ThenBy(t => t.NumeroSerie).ThenBy(t => t.NumeroComprobante).ToList();
            return ventasConsolidadas;
        }
    }
}

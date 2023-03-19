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
    public class LibrosElectronicosConcarLogica : ILibrosElectronicosConcarLogica
    {
        protected readonly ILibrosElectronicosConcarRepositorio _librosElectronicosConcarDatos;
        public LibrosElectronicosConcarLogica(ILibrosElectronicosConcarRepositorio librosElectronicosConcarDatos)
        {
            _librosElectronicosConcarDatos = librosElectronicosConcarDatos;
        }

        public static Dictionary<int, string> MapeoComprobanteSigesVsComprobanteConcar = new Dictionary<int, string> {
            {MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, ConcarSettings.Default.TipoDocumentoBoletaVenta },
            {MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, ConcarSettings.Default.TipoDocumentoFactura },
            {MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito, ConcarSettings.Default.TipoDocumentoNotaCredito },
            {MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito, ConcarSettings.Default.TipoDocumentoNotaDebito },
        };

        private DetalleAsientoContableConcar GenerarDetalleAsientoContableConcar(OperacionVenta operacionVenta, string subDiario, bool esDebe, string correlativo, bool esNotaCreditoDebito, string cuentaContable, decimal total)
        {
            var detalleAsiento = new DetalleAsientoContableConcar
            {
                SubDiario = subDiario,
                NumeroComprobante = correlativo,
                FechaComprobante = operacionVenta.FechaEmision.ToString("dd/MM/yyyy"),
                CodigoMoneda = operacionVenta.IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaSoles ? ConcarSettings.Default.MonedaSoles : operacionVenta.IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaDolares ? ConcarSettings.Default.MonedaDolarUsa : "",
                GlosaPrincipal = MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobante).Value + " " + operacionVenta.SerieComprobante + "-" + operacionVenta.NumeroComprobante.ToString().ToString().PadLeft(8, '0') + " " + operacionVenta.NombreCliente,
                TipoCambio = operacionVenta.TipoCambio,
                TipoConversion = ConcarSettings.Default.TipoConversionVenta,
                FlagConversionMoneda = ConcarSettings.Default.FlagConversionMonedaSiSeConvierte,
                FechaTipoCambio = "",
                CuentaContable = cuentaContable,
                CodigoAnexo = operacionVenta.NumeroDocumentoCliente,
                CodigoCentroCosto = "",
                DebeHaber = esDebe ? ConcarSettings.Default.Debe : ConcarSettings.Default.Haber,
                ImporteOriginal = total,
                ImporteDolares = 0,
                ImporteSoles = 0,
                TipoDocumento = MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobante).Value,
                NumeroDocumento = operacionVenta.SerieComprobante + "-" + operacionVenta.NumeroComprobante.ToString().ToString().PadLeft(8, '0'),
                FechaDocumento = operacionVenta.FechaDocumento.ToString("dd/MM/yyyy"),
                FechaVencimiento = operacionVenta.FechaVencimiento.ToString("dd/MM/yyyy"),
                CodigoArea = "",
                GlosaDetalle = operacionVenta.NombreTipoTransaccionWrapper,
                CodigoAnexoAuxiliar = "",
                MedioPago = "",
                TipoDocumentoReferencia = esNotaCreditoDebito ? MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobanteReferencia).Value : "",
                NumeroDocumentoReferencia = esNotaCreditoDebito ? operacionVenta.SerieComprobanteReferencia + "-" + operacionVenta.NumeroComprobanteReferencia.ToString().PadLeft(8, '0') : "",
                FechaDocumentoReferencia = esNotaCreditoDebito ? operacionVenta.FechaEmisionReferencia.ToString("dd/MM/yyyy") : "",
                NroMaqRegistradoraTipoDocRef = "",
                BaseImponibleDocumentoReferencia = esNotaCreditoDebito ? Math.Round(operacionVenta.BaseImponibleReferencia, 2) : 0,
                IGVDocumentoProvision = esNotaCreditoDebito ? (operacionVenta.IgvReferencia == 0 ? 0 : Math.Round(operacionVenta.IgvReferencia, 2)) : 0,
                TipoReferenciaenestadoMQ = "",
                NumeroSerieCajaRegistradora = "",
                FechaOperacion = "",
                TipoTasa = "",
                TasaDetraccionPercepcion = 0,
                ImporteBaseDetraccionPercepcionDolares = 0,
                ImporteBaseDetraccionPercepcionSoles = 0,
                TipoCambioparaF = "",
                ImporteIGVSinDerechoCreditoFiscal = 0
            };
            return detalleAsiento;
        }

        private DetalleAsientoContableConcar GenerarDetalleAsientoContableConcarRegistroNotas(OperacionVenta operacionVenta, bool esDebe, string correlativo, bool esNotaCreditoDebito, bool esReferencia, decimal total)
        {
            var detalleAsiento = new DetalleAsientoContableConcar
            {
                SubDiario = ConcarSettings.Default.SubDiarioProvisionesVarias,
                NumeroComprobante = correlativo,
                FechaComprobante = operacionVenta.FechaDocumento.ToString("dd/MM/yyyy"),
                CodigoMoneda = operacionVenta.IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaSoles ? ConcarSettings.Default.MonedaSoles : operacionVenta.IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaDolares ? ConcarSettings.Default.MonedaDolarUsa : "",
                GlosaPrincipal = "APLIC." + MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobante).Value + " " + operacionVenta.SerieComprobante + "-" + operacionVenta.NumeroComprobante.ToString().ToString().PadLeft(8, '0') + " / " + MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobanteReferencia).Value + " " + operacionVenta.SerieComprobanteReferencia + "-" + operacionVenta.NumeroComprobanteReferencia.ToString().ToString().PadLeft(8, '0'),
                TipoCambio = operacionVenta.TipoCambio,
                TipoConversion = ConcarSettings.Default.TipoConversionVenta,
                FlagConversionMoneda = ConcarSettings.Default.FlagConversionMonedaSiSeConvierte,
                FechaTipoCambio = "",
                CuentaContable = ConcarSettings.Default.CuentaContableFacturasPorCobrar,
                CodigoAnexo = operacionVenta.NumeroDocumentoCliente,
                CodigoCentroCosto = "",
                DebeHaber = esDebe ? ConcarSettings.Default.Debe : ConcarSettings.Default.Haber,
                ImporteOriginal = total,
                ImporteDolares = 0,
                ImporteSoles = 0,
                TipoDocumento = esReferencia ? MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobanteReferencia).Value : MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobante).Value,
                NumeroDocumento = esReferencia ? operacionVenta.SerieComprobanteReferencia + "-" + operacionVenta.NumeroComprobanteReferencia.ToString().ToString().PadLeft(8, '0') : operacionVenta.SerieComprobante + "-" + operacionVenta.NumeroComprobante.ToString().ToString().PadLeft(8, '0'),
                FechaDocumento = operacionVenta.FechaDocumento.ToString("dd/MM/yyyy"),
                FechaVencimiento = operacionVenta.FechaVencimiento.ToString("dd/MM/yyyy"),
                CodigoArea = "",
                GlosaDetalle = esReferencia ? operacionVenta.NombreTipoTransaccionReferenciaWrapper : operacionVenta.NombreTipoTransaccionWrapper,
                CodigoAnexoAuxiliar = "",
                MedioPago = "",
                TipoDocumentoReferencia = esNotaCreditoDebito ? MapeoComprobanteSigesVsComprobanteConcar.Single(m => m.Key == operacionVenta.IdTipoComprobanteReferencia).Value : "",
                NumeroDocumentoReferencia = esNotaCreditoDebito ? operacionVenta.SerieComprobanteReferencia + "-" + operacionVenta.NumeroComprobanteReferencia.ToString().PadLeft(8, '0') : "",
                FechaDocumentoReferencia = esNotaCreditoDebito ? operacionVenta.FechaEmisionReferencia.ToString("dd/MM/yyyy") : "",
                NroMaqRegistradoraTipoDocRef = "",
                BaseImponibleDocumentoReferencia = esNotaCreditoDebito ? Math.Round(operacionVenta.BaseImponibleReferencia, 2) : 0,
                IGVDocumentoProvision = esNotaCreditoDebito ? (operacionVenta.IgvReferencia == 0 ? 0 : Math.Round(operacionVenta.IgvReferencia, 2)) : 0,
                TipoReferenciaenestadoMQ = "",
                NumeroSerieCajaRegistradora = "",
                FechaOperacion = "",
                TipoTasa = "",
                TasaDetraccionPercepcion = 0,
                ImporteBaseDetraccionPercepcionDolares = 0,
                ImporteBaseDetraccionPercepcionSoles = 0,
                TipoCambioparaF = "",
                ImporteIGVSinDerechoCreditoFiscal = 0
            };
            return detalleAsiento;
        }

        private List<DetalleAsientoContableConcar> ObtenerRegistroCobranzas(Periodo periodo, List<OperacionVenta> operacionesVenta)
        {
            try
            {
                List<DetalleAsientoContableConcar> SubDiarioRegistroCobranzas = new List<DetalleAsientoContableConcar>();
                int correlativo = 1;
                foreach (var operacionVenta in operacionesVenta)
                {
                    bool esDebeOperacionPrincipal = operacionVenta.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                    //bool esNotaCreditoDebito = Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito.Contains(operacionVenta.IdTipoComprobante);
                    string correlativoDetalle = periodo.mes + correlativo.ToString().PadLeft(4, '0');
                    //Operacion principal
                    SubDiarioRegistroCobranzas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioPlanillaDeCobranza, esDebeOperacionPrincipal, correlativoDetalle, false, ConcarSettings.Default.CuentaContableCaja, operacionVenta.MontoTotalPago));
                    //Operacion complementaria
                    if (!operacionVenta.EstaInvalidado)
                    {
                        SubDiarioRegistroCobranzas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioPlanillaDeCobranza, !esDebeOperacionPrincipal, correlativoDetalle, false, ConcarSettings.Default.CuentaContableFacturasPorCobrar, operacionVenta.MontoTotalPago));
                    }
                    correlativo++;
                }
                return SubDiarioRegistroCobranzas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el registro de cobranza", e);
            }
        }

        private List<DetalleAsientoContableConcar> ObtenerRegistroVentas(Periodo periodo, List<OperacionVenta> operacionesVenta)
        {
            try
            {
                List<DetalleAsientoContableConcar> SubDiarioRegistroVentas = new List<DetalleAsientoContableConcar>();
                int correlativo = 1;
                foreach (var operacionVenta in operacionesVenta)
                {
                    bool esDebeOperacionPrincipal = operacionVenta.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                    bool esNotaCreditoDebito = Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito.Contains(operacionVenta.IdTipoComprobante);
                    string correlativoDetalle = periodo.mes + correlativo.ToString().PadLeft(4, '0');
                    //Operacion principal
                    SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioRegistroVentas, esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, ConcarSettings.Default.CuentaContableFacturasPorCobrar, operacionVenta.ImporteTotal));
                    //Operacion complementaria
                    if (!operacionVenta.EstaInvalidado)
                    {
                        if (operacionVenta.TotalIgv > 0)
                            SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioRegistroVentas, !esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, ConcarSettings.Default.CuentaContableIgv, (decimal)operacionVenta.TotalIgv));
                        if (operacionVenta.Icbper > 0)
                            SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioRegistroVentas, !esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, ConcarSettings.Default.CuentaContableIcbper, (decimal)operacionVenta.Icbper));
                        if (operacionVenta.TotalBien > 0)
                            SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioRegistroVentas, !esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, ConcarSettings.Default.CuentaContableVentaMercaderia, (decimal)operacionVenta.BaseImponibleBien));
                        if (operacionVenta.TotalServicio > 0)
                            SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcar(operacionVenta, ConcarSettings.Default.SubDiarioRegistroVentas, !esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, ConcarSettings.Default.CuentaContableVentaServicio, (decimal)operacionVenta.BaseImponibleServicio));
                    }
                    correlativo++;
                }
                return SubDiarioRegistroVentas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el registro de ventas", e);
            }
        }

        private List<DetalleAsientoContableConcar> ObtenerRegistroNotas(Periodo periodo, List<OperacionVenta> operacionesVenta)
        {
            try
            {
                List<DetalleAsientoContableConcar> SubDiarioRegistroVentas = new List<DetalleAsientoContableConcar>();
                int correlativo = 1;
                foreach (var operacionVenta in operacionesVenta)
                {
                    bool esDebeOperacionPrincipal = operacionVenta.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
                    bool esNotaCreditoDebito = Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito.Contains(operacionVenta.IdTipoComprobante);
                    string correlativoDetalle = periodo.mes + correlativo.ToString().PadLeft(4, '0');
                    //Operacion principal
                    SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcarRegistroNotas(operacionVenta, !esDebeOperacionPrincipal, correlativoDetalle, esNotaCreditoDebito, false, operacionVenta.ImporteTotal));
                    //Operacion complementaria
                    SubDiarioRegistroVentas.Add(GenerarDetalleAsientoContableConcarRegistroNotas(operacionVenta, esDebeOperacionPrincipal, correlativoDetalle, !esNotaCreditoDebito, true, operacionVenta.ImporteTotal));
                    correlativo++;
                }
                return SubDiarioRegistroVentas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el registro de notas de credito y debito", e);
            }
        }

        private List<RegistroClienteConcar> ObtenerRegistroClientes(Periodo periodo)
        {
            try
            {
                var registrosCliente = _librosElectronicosConcarDatos.ObtenerRegistroClientes(Diccionario.TiposDeComprobanteTributables, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, periodo.FechaDesde, periodo.FechaHasta).ToList();
                registrosCliente.Remove(registrosCliente.SingleOrDefault(rc => rc.Id == ActorSettings.Default.IdClienteGenerico));
                registrosCliente.ForEach(m => { var nombreClienteArray = m.Nombre.Split('|'); m.Nombre = nombreClienteArray.Length == 3 ? (nombreClienteArray[2] + " " + nombreClienteArray[0] + " " + nombreClienteArray[1]) : nombreClienteArray[0]; });
                registrosCliente = registrosCliente.OrderBy(c => c.NumeroDocumento).ToList();
                List<RegistroClienteConcar> RegistroClientesConcar = new List<RegistroClienteConcar>();
                foreach (var registroCliente in registrosCliente)
                {
                    RegistroClientesConcar.Add(new RegistroClienteConcar
                    {
                        Avanexo = ConcarSettings.Default.RegistroClientesConcarAvanexoC,
                        Acodane = registroCliente.NumeroDocumento,
                        Adesane = registroCliente.Nombre,
                        Arefane = (string.IsNullOrEmpty(registroCliente.Direccion) || registroCliente.Direccion == "-") ? ConcarSettings.Default.RegistorClientesConcarArefaneSinDireccion : registroCliente.Direccion,
                        Aruc = registroCliente.NumeroDocumento,
                        Acodmon = ConcarSettings.Default.MonedaSoles,
                        Aestado = ConcarSettings.Default.RegistorClientesConcarAestadoV
                    });
                }
                return RegistroClientesConcar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el registro de clientes", e);
            }
        }
        public LibroElectronicoConcar ObtenerLibrosElectronicosConcar(Periodo periodo)
        {
            try
            {
                LibroElectronicoConcar libroElectronicoConcar = new LibroElectronicoConcar();
                var operacionesVenta = _librosElectronicosConcarDatos.ObtenerOperacionesVenta(Diccionario.TiposDeComprobanteTributables, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, periodo.FechaDesde, periodo.FechaHasta).OrderBy(ov => ov.FechaEmision).ToList();
                operacionesVenta.ForEach(m => { var nombreClienteArray = m.NombreCliente.Split('|'); m.NombreCliente = m.IdCliente == ActorSettings.Default.IdClienteGenerico ? nombreClienteArray[2] : nombreClienteArray.Length == 3 ? (nombreClienteArray[2] + " " + nombreClienteArray[0] + " " + nombreClienteArray[1]) : nombreClienteArray[0]; });
                var cobranzasVenta = _librosElectronicosConcarDatos.ObtenerOperacionesIngresoDineroPorVenta(Diccionario.TiposDeComprobanteDeIngresoDineroPorOperacionesDeVenta, Diccionario.TiposDeTransaccionDeIngresoDeDineroPorOperacionesDeVenta, periodo.FechaDesde, periodo.FechaHasta).OrderBy(ov => ov.FechaEmision).ToList();
                cobranzasVenta.ForEach(m => { var nombreClienteArray = m.NombreCliente.Split('|'); m.NombreCliente = m.IdCliente == ActorSettings.Default.IdClienteGenerico ? nombreClienteArray[2] : nombreClienteArray.Length == 3 ? (nombreClienteArray[2] + " " + nombreClienteArray[0] + " " + nombreClienteArray[1]) : nombreClienteArray[0]; });
                libroElectronicoConcar.RegistroCobranzas = ObtenerRegistroCobranzas(periodo, cobranzasVenta);
                libroElectronicoConcar.RegistroVentas = ObtenerRegistroVentas(periodo, operacionesVenta);
                var operacionesNotasCreditoDebito = operacionesVenta.Where(o => Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito.Contains(o.IdTipoComprobante)).ToList();
                libroElectronicoConcar.RegistroNotas = ObtenerRegistroNotas(periodo, operacionesNotasCreditoDebito);
                libroElectronicoConcar.RegistroClientes = ObtenerRegistroClientes(periodo);
                return libroElectronicoConcar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el libro electronico concar", e);
            }
        }
    }
}

using Tsp.Sigescom.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using System.Text.RegularExpressions;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionInvalidacion
    {
        public Venta Venta { get; set; }
        public OrdenDeVenta OrdenVenta { get; set; }
        public List<Detalle_transaccion> DetallesOperacion { get; set; }
        public List<Detalle_transaccion> DetallesBienesOperacion { get; set; }
        public List<Detalle_transaccion> DetallesMovimientoAlmacenOperacion { get; set; }
        public bool EsDiferidaOperacion { get; set; }
        public bool HaySeccionEntregaAlmacenOperacion { get => VentasSettings.Default.MostrarSeccionEntregaEnVenta && (EsOrdenOrigenCompletada || EsOrdenOrigenParcial); }
        public bool HayMovimientoAlmacenOperacion { get => (!VentasSettings.Default.MostrarSeccionEntregaEnVenta && EsOrdenOrigenCompletada) || (HaySeccionEntregaAlmacenOperacion && !EsDiferidaOperacion); }
        public string ObservacionOperacion { get; set; }
        public DatosPago PagoOperacion { get; set; }
        public List<Cuota> Cuotas { get; set; }
        public bool EsPendienteEstadoCuotas { get => Cuotas.Sum(c => c.saldo) == Cuotas.Sum(c => c.total); }
        public bool EsParcialEstadoCuotas { get => Cuotas.Sum(c => c.saldo) != 0 && Cuotas.Sum(c => c.saldo) != Cuotas.Sum(c => c.total); }
        public bool EsCompletoEstadoCuotas { get => Cuotas.Sum(c => c.saldo) == 0; }
        public DateTime FechaActual { get; set; }
        public int IdMoneda { get; set; }
        public decimal TipoDeCambio { get; set; }
        public int IdUnidadDeNegocio { get; set; }
        public int IdCliente { get; set; }
        public string AliasCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCaja { get; set; }
        public int IdAlmacen { get; set; }
        public int IdCentroAtencion { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImportePagoTotal { get; set; }
        public decimal DescuentoGlobal { get; set; }
        public decimal DescuentoPorItem { get; set; }
        public decimal Anticipo { get; set; }
        public decimal Gravada { get; set; }
        public decimal Exonerada { get; set; }
        public decimal Inafecta { get; set; }
        public decimal Gratuita { get; set; }
        public decimal Igv { get; set; }
        public bool GravaIgv { get => Igv > 0; }
        public decimal Isc { get; set; }
        public decimal Icbper { get; set; }
        public decimal NumeroBolsasPlastico { get; set; }
        public decimal OtrosCargos { get; set; }
        public decimal OtrosTributos { get; set; }
        public bool EsOrdenOrigenDiferida { get => OrdenVenta.IndicadorImpactoAlmacen == IndicadorImpactoAlmacen.Diferida; }
        public bool EsOrdenOrigenPendiente { get => OrdenVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoPendiente; }
        public bool EsOrdenOrigenParcial { get => OrdenVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoParcial; }
        public bool EsOrdenOrigenCompletada { get => OrdenVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoCompletada; }

        public OperacionInvalidacion()
        { }
        public OperacionInvalidacion(Venta venta, OrdenDeVenta ordenVenta, DateTime fechaActual, bool esDiferida, string observacion, DatosPago pago, UserProfileSessionData sesionUsuario)
        {
            Venta = venta;
            OrdenVenta = ordenVenta;
            FechaActual = fechaActual;
            DetallesOperacion = ordenVenta.Detalles().Select(dv => dv.DetalleTransaccion()).ToList();
            DetallesBienesOperacion = ordenVenta.Detalles().Where(d => d.Producto.EsBien).Select(d => d.DetalleTransaccion()).ToList();
            EsDiferidaOperacion = esDiferida;
            ObservacionOperacion = observacion = string.IsNullOrEmpty(observacion) ? "NINGUNO" : Regex.Replace(observacion, @"\s+", " "); ;
            PagoOperacion = pago;
            PagoOperacion.Traza.MedioDePago = PagoOperacion.Traza.MedioDePago ?? new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo);
            PagoOperacion.Traza.Info = PagoOperacion.Traza.Info ?? new InfoPago();
            PagoOperacion.Traza.Info.EntidadFinanciera = (PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos || PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta) ? new ItemGenerico { Id = int.Parse(PagoOperacion.Traza.Info.CuentaBancaria.Valor) } : PagoOperacion.Traza.Info.EntidadFinanciera;
            PagoOperacion.Traza.Info.EntidadFinanciera = Diccionario.IdsMediosDePagoQueTienenEntidadBancaria.Contains(PagoOperacion.Traza.MedioDePago.Id) ? PagoOperacion.Traza.Info.EntidadFinanciera : new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto);
            PagoOperacion.Traza.Info.OperadorTarjeta = PagoOperacion.Traza.MedioDePago.Id == MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito ? PagoOperacion.Traza.Info.OperadorTarjeta : new ItemGenerico();
            PagoOperacion.Traza.Info.Observacion = string.IsNullOrEmpty(PagoOperacion.Traza.Info.Observacion) ? "NINGUNO" : PagoOperacion.Traza.Info.Observacion;
            Cuotas = ordenVenta.Cuotas();
            IdUnidadDeNegocio = ordenVenta.IdUnidadDeNegocio;
            IdMoneda = ordenVenta.IdMoneda;
            TipoDeCambio = ordenVenta.TipoDeCambio;
            IdCliente = ordenVenta.IdCliente;
            AliasCliente = ordenVenta.AliasCliente();
            IdEmpleado = sesionUsuario.Empleado.Id;
            IdCaja = venta.ObtenerPagos().Count() > 0 ? venta.ObtenerPagos().First().Transaccion().id_actor_negocio_interno : sesionUsuario.CentroDeAtencionSeleccionado.Id;
            IdAlmacen = sesionUsuario.IdCentroAtencionQueTieneElStockIntegrada;
            IdCentroAtencion = sesionUsuario.CentroDeAtencionSeleccionado.Id;
            ImporteTotal = DetallesOperacion.Sum(d => d.total) + ordenVenta.Icbper();
            ImportePagoTotal = Cuotas.Sum(c => c.pago_a_cuenta);
            DescuentoGlobal = 0;
            DescuentoPorItem = 0;
            Anticipo = 0;
            Gravada = DetallesOperacion.Sum(d => d.igv) > 0 ? DetallesOperacion.Sum(d => d.total - d.igv) : 0;
            Exonerada = DetallesOperacion.Sum(d => d.igv) > 0 ? 0 : DetallesOperacion.Sum(d => d.total);
            Inafecta = 0;
            Gratuita = 0;
            Igv = DetallesOperacion.Sum(d => d.igv) > 0 ? DetallesOperacion.Sum(d => d.igv) : 0;
            Isc = 0;
            Icbper = ordenVenta.Icbper();
            NumeroBolsasPlastico = ordenVenta.NumeroBolsasDePlastico();
            OtrosCargos = 0;
            OtrosTributos = 0;
        }

    }
    public class OperacionNota : OperacionInvalidacion
    {
        public bool EsDebito { get; set; }
        public int IdTipoNota { get; set; }
        public decimal MontoNota { get; set; }
        public bool EsPropio { get; set; }
        public int IdSerieComprobante { get; set; }
        public int IdTipoComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public int NumeroComprobante { get; set; }
        public List<DetalleOrdenDeNota> DetallesNota { get; set; }
        public int IndicadorImpactoAlmacenNota { get; set; }
        public bool HayMovimientoAlmacenNota { get; set; }
        public string SufijoCodigo { get; set; }
        public int IdTipoTransaccion { get; set; }
        public int IdTipoTransaccionOrden { get; set; }
        public int IdTipoTransaccionPago { get; set; }
        public int IdTipoTransaccionAlmacen { get; set; }
        public bool NuevoComprobanteParaMovimientoAlmacen { get; set; }

        public OperacionNota(Venta venta, OrdenDeVenta ordenVenta, DateTime fechaActual, int idTipoNota, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerie, int numeroComprobante, decimal montoNota, List<DetalleOrdenDeNota> detalles, bool esDebito, bool esDiferida, string observacion, DatosPago pago, UserProfileSessionData sesionUsuario) : base(venta, ordenVenta, fechaActual, esDiferida, observacion, pago, sesionUsuario)
        {
            IdTipoNota = idTipoNota;
            MontoNota = montoNota;
            EsPropio = esPropio;
            IdSerieComprobante = idSerieComprobante;
            IdTipoComprobante = idTipoComprobante;
            NumeroSerie = numeroSerie;
            NumeroComprobante = numeroComprobante;
            DetallesNota = detalles;
            IndicadorImpactoAlmacenNota = (int)IndicadorImpactoAlmacen.NoImpactaNoBienes;
            HayMovimientoAlmacenNota = false;//Por defecto sin movimiento de almacen
            EsDebito = esDebito;
            SufijoCodigo = esDebito ? "ND" : "NC";
            IdTipoTransaccion = Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value;
            IdTipoTransaccionOrden = Diccionario.MapeoWraperVsOrden.Single(m => m.Key == Diccionario.MapeoDetalleMaestroVsTipoTransaccionParaVenta.Single(n => n.Key == idTipoNota).Value).Value;
            IdTipoTransaccionPago = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == IdTipoTransaccionOrden).Value;
            IdTipoTransaccionAlmacen = Diccionario.MapeoOrdenVsMovimientoDeAlmacen.SingleOrDefault(m => m.Key == IdTipoTransaccionOrden).Value;
            NuevoComprobanteParaMovimientoAlmacen = EsOrdenOrigenParcial && (idTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion || idTipoNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal);
        }
    }

}

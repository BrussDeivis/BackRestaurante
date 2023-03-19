using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenAlmacen : OrdenAlmacenResumen
    {
        public List<long> IdsOrdenes { get; set; }
        public bool EstaPendiente { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoPendiente; }
        public bool EstaParcial { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoParcial; }
        public bool EstaCompletada { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoCompletada; }
        public List<OrdenDeOrdenAlmacen> Ordenes { get; set; }
        public List<DetalleDeOrdenAlmacen> Detalles { get; set; }
        public List<MovimientoDeOrdenAlmacen> Movimientos { get; set; }
    }
    public class OrdenDeOrdenAlmacen
    {
        public long Id { get; set; }
        public int IdTipoTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaEmision { get => Fecha.ToString("dd/MM/yyyy hh:mm tt"); }
        public string TipoDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public string TipoOperacion { get; set; }
        public ComprobanteDeAlmacen Comprobante { get; set; }
    }

    public class MovimientoDeOrdenAlmacen
    {
        public long Id { get; set; }
        public long IdOrden { get; set; }
        public int IdTipoComprobante { get; set; }
        public int IdTipoTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaEmision { get => Fecha.ToString("dd/MM/yyyy hh:mm tt"); }
        public string Destinatario { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public int IdEstado { get; set; }
        public bool EsVigente { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado; }
        public List<DetalleDeOrdenAlmacen> Detalles { get; set; }
        public ComprobanteDeAlmacen Comprobante { get; set; }
    }

    public class DetalleDeOrdenAlmacen
    {
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Ordenado { get; set; }
        public decimal Revocado { get; set; }
        public decimal Entregado { get; set; }
        public decimal Pendiente { get; set; }
        public decimal StockActual { get; set; }
        public decimal Devuelto { get; set; }
    }
    public class ComprobanteDeAlmacen
    {
        public long Id { get; set; }
        public long IdOrden { get => Id; }
        public string CadenaHtmlDeComprobante80 { get; set; }
        public string CadenaHtmlDeComprobanteA4 { get; set; }
    }
}

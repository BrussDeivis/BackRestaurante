using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class OperacionVenta
    {
        public long Id { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdMoneda { get; set; }
        public int IdTipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public int IdCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal? TotalBien { get; set; }
        public decimal? IgvBien { get; set; }
        public decimal? TotalServicio { get; set; }
        public decimal? IgvServicio { get; set; }
        public decimal Icbper { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string NombreTipoTransaccionWrapper { get; set; }
        public int IdEstado { get; set; }
        public bool EstaInvalidado { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; }
        public decimal BaseImponibleBien { get => (TotalBien == null ? 0 : (decimal)TotalBien) - (IgvBien == null ? 0 : (decimal)IgvBien); }
        public decimal BaseImponibleServicio { get => (TotalServicio == null ? 0 : (decimal)TotalServicio) - (IgvServicio == null ? 0 : (decimal)IgvServicio); }
        public decimal BaseImponibleTotal { get => BaseImponibleBien + BaseImponibleServicio; }
        public decimal Total { get => (TotalBien == null ? 0 : (decimal)TotalBien) + (TotalServicio == null ? 0 : (decimal)TotalServicio) + Icbper; }
        public decimal ImporteTotal { get => EstaInvalidado ? 0 : Total; }
        public decimal TotalIgv { get => (IgvBien == null ? 0 : (decimal)IgvBien) + (IgvServicio == null ? 0 : (decimal)IgvServicio); }

        public DateTime FechaEmision { get; set; }
        public decimal MontoTotalPago { get; set; }


        public int IdTipoComprobanteReferencia { get; set; }
        public string SerieComprobanteReferencia { get; set; }
        public int NumeroComprobanteReferencia { get; set; }
        public DateTime FechaEmisionReferencia { get; set; }
        public DateTime FechaVencimientoReferencia { get; set; }
        public string NombreTipoTransaccionReferenciaWrapper { get; set; }
        public decimal ImporteTotalReferencia { get; set; }
        public decimal IgvReferencia { get; set; }
        public decimal BaseImponibleReferencia { get => ImporteTotalReferencia - IgvReferencia; }

        public List<OperacionVenta> Convert()
        {
            return new List<OperacionVenta>();
        }

    }
    public class OperacionCuotaPago
    {
        public long IdOperacion { get; set; }
        public decimal MontoPago { get; set; }
        public DateTime FechaEmision { get; set; }
    }
   
}

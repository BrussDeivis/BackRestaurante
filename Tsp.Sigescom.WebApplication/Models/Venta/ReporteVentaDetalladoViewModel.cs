using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ReporteVentaDetalladoViewModel // ReporteVentaViewModel 
    {
        public string CUO { get; set; } //CUO = Codigo Unico de la Operacion
        public DateTime FechaEmisionComprobante { get; set; }
        public DateTime FechaVencimientoComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public string TipoDocumentoIdentidadCliente { get; set; }
        public string NumeroDocumentoIdentidadCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal CantidadProducto { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public decimal ValorExportacion { get; set; }
        public decimal BaseImponibleGravada { get; set; }
        public decimal ImporteTotalExonerada { get; set; }
        public decimal ImporteTotalInafecta { get; set; }
        public decimal ISC { get; set; }
        public decimal IGVoIPM { get; set; }
        public decimal Icbper { get; set; }
        public decimal TributosYCargosNoImponible { get; set; }
        public decimal ImporteTotalComprobante { get; set; }
        public decimal TipoCambio { get; set; }
        public DateTime? FechaComprobanteModifica { get; set; }
        public string TipoComprobanteModifica { get; set; }
        public string SerieComprobanteModifica { get; set; }
        public int NumeroComprobanteModifica { get; set; }

        public ReporteVentaDetalladoViewModel()
        {
        }

        public ReporteVentaDetalladoViewModel(DetalleDeOperacion detalle, OperacionDeVenta orden)
        {
            var esNotaDeCredito = orden.Comprobante().IdTipo == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
            //var hayIGV = orden.Igv > 0;

            CUO = "";
            FechaEmisionComprobante = orden.FechaEmision;
            FechaVencimientoComprobante = orden.FechaVencimiento;
            TipoComprobante = orden.Comprobante().CodigoTipo;
            SerieComprobante = orden.Comprobante().NumeroDeSerie;
            NumeroComprobante = orden.Comprobante().NumeroDeComprobante;
            TipoDocumentoIdentidadCliente = orden.CodigoSunatTipoDocumentoIdentidadCliente;
            NumeroDocumentoIdentidadCliente = orden.NumeroDocumentoIdentidadCliente;
            RazonSocialCliente = orden.ApellidosYNombres;
            CodigoProducto = detalle.Producto.CodigoBarra;
            DescripcionProducto = detalle.Producto.Nombre;
            CantidadProducto = orden.EsInvalidada ? 0m : detalle.Cantidad;
            PrecioUnitarioProducto = orden.EsInvalidada ? 0m : detalle.PrecioUnitario;
            ValorExportacion = 0;
            BaseImponibleGravada = orden.EsInvalidada ? 0 : detalle.Igv > 0 ? esNotaDeCredito ? (-1) * detalle.Importe : detalle.Importe : 0;
            ImporteTotalExonerada = orden.EsInvalidada ? 0 : detalle.Igv <= 0 ? esNotaDeCredito ? (-1) * detalle.Importe : detalle.Importe : 0;
            ImporteTotalInafecta = 0;
            ISC = orden.EsInvalidada ? 0 : esNotaDeCredito ? (-1) * detalle.Isc : detalle.Isc;
            IGVoIPM = orden.EsInvalidada ? 0 : esNotaDeCredito ? (-1) * detalle.Igv : detalle.Igv;
            Icbper = orden.EsInvalidada ? 0 : esNotaDeCredito ? (-1) * ((MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica == detalle.Producto.IdConceptoBasico) ? detalle.Cantidad * orden.ValorIcbper() : 0) : ((MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica == detalle.Producto.IdConceptoBasico) ? detalle.Cantidad * orden.ValorIcbper() : 0);
            TributosYCargosNoImponible = 0;
            ImporteTotalComprobante = orden.EsInvalidada ? 0 : esNotaDeCredito ? (-1) * detalle.Importe : detalle.Importe;
            TipoCambio = orden.EsInvalidada ? 0 : orden.TipoDeCambio;
            FechaComprobanteModifica = orden.FechaEmisionComprobantePagoQueSeModifica;
            TipoComprobanteModifica = orden.TipoComprobantePagoQueSeModifica;
            SerieComprobanteModifica = orden.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera;
            NumeroComprobanteModifica = orden.NumeroComprobantePagoQueSeModificaNúmeroDUA;
        }


        public static List<ReporteVentaDetalladoViewModel> Convert(List<OperacionDeVenta> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteVentaDetalladoViewModel>();
            foreach (var ordenVenta in ordenes)
            {
                reporteVentaViewModels.AddRange(ReporteVentaDetalladoViewModel.Convert(ordenVenta));
            }

            return reporteVentaViewModels;
        }

        public static List<ReporteVentaDetalladoViewModel> Convert(OperacionDeVenta orden)
        {
            var reporteVentaViewModels = new List<ReporteVentaDetalladoViewModel>();
            foreach (var detalle in orden.Detalles())
            {
                reporteVentaViewModels.Add(new ReporteVentaDetalladoViewModel(detalle, orden));
            }

            return reporteVentaViewModels;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    //Reporte de Venta Detallada sin concepto
    [Serializable]
    public class ReporteVentaDetalladoSinConceptoViewModel // ReporteVentaViewModel 
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
        public decimal ValorExportacion { get; set; }
        public decimal BaseImponibleGravada { get; set; }
        public decimal ImporteTotalExonerada { get; set; }
        public decimal ImporteTotalInafecta { get; set; }
        public decimal ISC { get; set; }
        public decimal IGVoIPM { get; set; }
        public decimal TributosYCargosNoImponible { get; set; }
        public decimal ImporteTotalComprobante { get; set; }
        public decimal TipoCambio { get; set; }
        public DateTime? FechaComprobanteModifica { get; set; }
        public string TipoComprobanteModifica { get; set; }
        public string SerieComprobanteModifica { get; set; }
        public int NumeroComprobanteModifica { get; set; }

        public ReporteVentaDetalladoSinConceptoViewModel()
        {
        }
        
        public ReporteVentaDetalladoSinConceptoViewModel(OperacionDeVenta orden)
        {
            

            CUO = "";
            FechaEmisionComprobante = orden.FechaEmision;
            FechaVencimientoComprobante = orden.FechaVencimiento;
            TipoComprobante = orden.Comprobante().CodigoTipo;
            SerieComprobante = orden.Comprobante().NumeroDeSerie;
            NumeroComprobante = orden.Comprobante().NumeroDeComprobante;
            TipoDocumentoIdentidadCliente = orden.CodigoSunatTipoDocumentoIdentidadCliente;
            NumeroDocumentoIdentidadCliente = orden.NumeroDocumentoIdentidadCliente;
            RazonSocialCliente = orden.ApellidosYNombres;
            ValorExportacion = 0;
            BaseImponibleGravada = orden.EsInvalidada ? 0 : orden.BaseImponibleOperacionGravadaConSigno;
            ImporteTotalExonerada = orden.EsInvalidada ? 0 : orden.ImporteTotalOperacionExoneradaConSigno;
            ImporteTotalInafecta = orden.ImporteTotalOperacionInafecta;
            ISC = orden.EsInvalidada ? 0 : orden.ImpuestoSelectivoConsumo;
            IGVoIPM = orden.EsInvalidada ? 0 : orden.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal;
            TributosYCargosNoImponible = 0;
            ImporteTotalComprobante = orden.EsInvalidada ? 0 : orden.ImporteTotalComprobantePago;
            TipoCambio = orden.EsInvalidada ? 0 : orden.TipoDeCambio;
            FechaComprobanteModifica = orden.FechaEmisionComprobantePagoQueSeModifica;
            TipoComprobanteModifica = orden.TipoComprobantePagoQueSeModifica;
            SerieComprobanteModifica = orden.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera;
            NumeroComprobanteModifica = orden.NumeroComprobantePagoQueSeModificaNúmeroDUA;
        }

       

        public static List<ReporteVentaDetalladoSinConceptoViewModel> Convert(List<OperacionDeVenta> ordenes)
        {
            var reporteVentaViewModels = new List<ReporteVentaDetalladoSinConceptoViewModel>();
            foreach (var ordenVenta in ordenes)
            {
                reporteVentaViewModels.Add(new ReporteVentaDetalladoSinConceptoViewModel(ordenVenta));

            }
            return reporteVentaViewModels;
        }

       
    }
}
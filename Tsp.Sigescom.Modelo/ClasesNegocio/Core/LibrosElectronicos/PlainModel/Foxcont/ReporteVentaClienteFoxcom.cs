using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class ReporteVentaClienteFoxcom
    {
        public string Fecha { get; set; }
        public string CodigoComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public string NumeroComprobante { get; set; }
        public string CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public decimal BaseImponibleGravada { get; set; }
        public decimal IGVoIPM { get; set; }
        public decimal Icbper { get; set; }
        public decimal ImporteTotalExonerada { get; set; }
        public decimal Retencion { get; set; }
        public decimal ImporteTotalComprobante { get; set; }
        public string NumeroSerieReferencia { get; set; }
        public string NumeroComprobanteReferencia { get; set; }
        public string FechaEmisionReferencia { get; set; }
        public string CodigoComprobanteReferencia { get; set; }

        public ReporteVentaClienteFoxcom()
        { }

        public ReporteVentaClienteFoxcom(VentaClienteFoxcom ventaFoxcont)
        {
            this.Fecha = ventaFoxcont.FechaEmision.ToString("dd/MM/yyyy");
            this.CodigoComprobante = ventaFoxcont.CodigoComprobante;
            this.NumeroSerie = ventaFoxcont.NumeroSerie;
            this.NumeroComprobante = ventaFoxcont.NumeroComprobante.ToString().PadLeft(7, '0');
            this.CodigoDocumento = ventaFoxcont.CodigoDocumento;
            this.NumeroDocumento = ventaFoxcont.NumeroDocumento;
            this.RazonSocial = ventaFoxcont.RazonSocial;

            this.BaseImponibleGravada = ventaFoxcont.EsInvalidada ? 0 : ventaFoxcont.ImporteBaseImponibleOperacionGravada;
            this.IGVoIPM = ventaFoxcont.EsInvalidada ? 0 : ventaFoxcont.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal;
            this.Icbper = ventaFoxcont.EsInvalidada ? 0 : ventaFoxcont.Icbper;
            this.ImporteTotalExonerada = ventaFoxcont.EsInvalidada ? 0 : ventaFoxcont.ImporteTotalOperacionExonerada;
            this.Retencion = 0;
            this.ImporteTotalComprobante = ventaFoxcont.EsInvalidada ? 0 : ventaFoxcont.ImporteTotal;

            this.NumeroSerieReferencia = ventaFoxcont.EsNota ? ventaFoxcont.NumeroSerieReferencia : "";
            this.NumeroComprobanteReferencia = ventaFoxcont.EsNota ? ventaFoxcont.NumeroComprobanteReferencia.ToString() : "";
            this.FechaEmisionReferencia = ventaFoxcont.EsNota ? ventaFoxcont.FechaEmisionReferencia.ToString("dd/MM/yyyy") : "";
            this.CodigoComprobanteReferencia = ventaFoxcont.EsNota ? ventaFoxcont.CodigoComprobanteReferencia : "";
        }

        public static List<ReporteVentaClienteFoxcom> Convert(List<VentaClienteFoxcom> ventasFoxcont)
        {
            List<ReporteVentaClienteFoxcom> reporteVentasFoxcont = new List<ReporteVentaClienteFoxcom>();
            foreach (var ventaFoxcont in ventasFoxcont)
            {
                reporteVentasFoxcont.Add(new ReporteVentaClienteFoxcom(ventaFoxcont));
            }
            return reporteVentasFoxcont;
        }
    }
}


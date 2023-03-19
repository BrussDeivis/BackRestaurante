using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class GastoPorComprobanteViewModel
    {
        public long IdComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public int NumeroComprobante { get; set; }
        public decimal Importe { get; set; }
        public string Cliente { get; set; }

        public GastoPorComprobanteViewModel()
        {

        }

        public GastoPorComprobanteViewModel( long idComprobante,DateTime fecha, string tipoDocumento, string serie, int numero, decimal importe, string razonSocial)
        {
            IdComprobante = idComprobante;
            Fecha = fecha;
            TipoDocumento = tipoDocumento;
            Serie = serie;
            NumeroComprobante = numero;
            Importe = importe;
            Cliente = razonSocial;
        }
        public GastoPorComprobanteViewModel(Reporte_Transaccion_Gasto_Por_Comprobante gasto)
        {
            this.IdComprobante = gasto.IdTipoComprobante;
            this.TipoDocumento = gasto.NombreCorteComprobante;
            this.Serie =gasto.Serie;
            this.NumeroComprobante = gasto.NumeroComprobante;
            this.Importe = gasto.Importe;
            this.Cliente = gasto.Cliente;
            this.Fecha = gasto.Fecha;
        }


        public static List<GastoPorComprobanteViewModel> Convert(List<Reporte_Transaccion_Gasto_Por_Comprobante> gastos)
        {
            var reporteGastoViewModels = new List<GastoPorComprobanteViewModel>();
            foreach (var gasto in gastos)
            {
                reporteGastoViewModels.Add(new GastoPorComprobanteViewModel(gasto));
            }

            return reporteGastoViewModels;
        }

        internal static List<GastoPorComprobanteViewModel> Resumen(List<GastoPorComprobanteViewModel> reporteVentaViewModelDetalles)
        {
            List<GastoPorComprobanteViewModel> reporteVentaViewModelResumenes = new List<GastoPorComprobanteViewModel>();
            foreach (var item in reporteVentaViewModelDetalles.Select(d => new { tipo = d.TipoDocumento, serie = d.Serie }).Distinct())
            {
                reporteVentaViewModelResumenes.Add(new GastoPorComprobanteViewModel(0,DateTime.Now, item.tipo, item.serie, 0, reporteVentaViewModelDetalles.Where(d => d.TipoDocumento == item.tipo && d.Serie == item.serie).Sum(d => d.Importe), ""));
            }
           
            return reporteVentaViewModelResumenes;
        }
    }
}
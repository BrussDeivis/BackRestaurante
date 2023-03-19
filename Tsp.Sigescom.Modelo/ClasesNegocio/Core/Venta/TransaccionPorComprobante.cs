using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    [Serializable]
    public class ResumenDeTransaccionVenta
    {
        private string cliente;

        public long Id { get; set; }
        public int IdTipoTransaccion { get; set; }

        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string OrdenVenta { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public decimal Importe { get; set; }
        public string PrimerNombre { get; set; }
        public string Cliente { get => PrimerNombre.Replace("|", " "); set => cliente = value; }

        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string CentroAtencion { get; set; }
        public string NumeroDocumento { get; set; }



        public ResumenDeTransaccionVenta()
        {

        }
        public ResumenDeTransaccionVenta(OrdenDeVenta ordenVenta)
        {
            this.Id = ordenVenta.Id;
            this.IdTipoTransaccion = ordenVenta.IdTipoTransaccion;
            this.Fecha = ordenVenta.FechaEmision;
            this.TipoDocumento = ordenVenta.Comprobante().NombreCortoTipo;
            this.OrdenVenta = ordenVenta.Codigo;
            this.Serie = ordenVenta.Comprobante().NumeroDeSerie;
            this.Numero = (int)ordenVenta.Comprobante().NumeroDeComprobante;
            this.Importe = ordenVenta.Total;
            this.PrimerNombre = ordenVenta.Cliente().RazonSocial;
            this.Cliente = ordenVenta.Cliente().RazonSocial;


        }

        public ResumenDeTransaccionVenta(long id, DateTime fecha, string tipoDocumento, string ordenVenta, string serie, int numero, decimal importe, string razonSocial)
        {
            Id = id;
            Fecha = fecha;
            TipoDocumento = tipoDocumento;
            OrdenVenta = ordenVenta;
            Serie = serie;
            Numero = numero;
            Importe = importe;
            Cliente = razonSocial;
        }

        public static List<ResumenDeTransaccionVenta> Convert(List<OrdenDeVenta> ordenes)
        {
            var reporteVentaViewModels = new List<ResumenDeTransaccionVenta>();
            foreach (var ordenVenta in ordenes)
            {
                reporteVentaViewModels.Add(new ResumenDeTransaccionVenta(ordenVenta));
            }

            return reporteVentaViewModels;
        }

        public static List<ResumenDeTransaccionVenta> Resumen(List<ResumenDeTransaccionVenta> reporteVentaViewModelDetalles)
        {
            List<ResumenDeTransaccionVenta> reporteVentaViewModelResumenes = new List<ResumenDeTransaccionVenta>();
            foreach (var item in reporteVentaViewModelDetalles.Select(d => new { tipo = d.TipoDocumento, serie = d.Serie }).Distinct())
            {
                reporteVentaViewModelResumenes.Add(new ResumenDeTransaccionVenta(0, DateTime.Now, item.tipo, "", item.serie, 0, reporteVentaViewModelDetalles.Where(d => d.TipoDocumento == item.tipo && d.Serie == item.serie).Sum(d => d.Importe), ""));
            }
            return reporteVentaViewModelResumenes;
        }
    }
}
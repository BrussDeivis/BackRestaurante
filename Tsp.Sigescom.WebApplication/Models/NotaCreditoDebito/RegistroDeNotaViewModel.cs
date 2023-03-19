using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroDeNotaViewModel
    {
        public long IdOrdenDeOperacion { get; set; }
        public ItemGenerico TipoNota { get; set; }
        public SelectorTipoDeComprobante Comprobante { get; set; }
        public string Observacion { get; set; }
        public decimal MontoNota { get; set; }
        public decimal Igv { get; set; }
        public decimal Isc { get; set; }
        public IEnumerable<RegistroDetalleDeNotaViewModel> Detalles { get; set; }
        public bool EsDebito { get; set; }
        public bool EsDiferida { get; set; }
        public DatosPago Pago { get; set; }

    }


    public class RegistroDetalleDeNotaViewModel
    {
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public decimal Igv { get; set; }
        public decimal Descuento { get; set; }
        public string Observacion { get; set; }
        public decimal MontoDetalle { get; set; }
        public decimal MontoRevocado { get; set; }
        public decimal MontoDevuelto { get; set; }
    }
}

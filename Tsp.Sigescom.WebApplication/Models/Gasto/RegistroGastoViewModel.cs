using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroGastoViewModel
    {
        public ComboActorComercialViewModel Proveedor { get; set; }
        public int TipoGasto { get; set; }
        public int TipoCompra { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observacion { get; set; }
        public IEnumerable<RegistroDetalleGastoViewModel> Detalles { get; set; }
        public bool EsCompraACredito { get; set; }
        public bool EsCreditoRapido { get; set; }
        public IEnumerable<RegistroDetalleFinanciamientoViewModel> Cuotas { get; set; }
        public decimal Inicial { get; set; }
        public decimal Flete { get; set; }
        public bool PagarInicialAlConfimar { get; set; }

        public RegistroGastoViewModel()
        {

        }
    }

    public class RegistroDetalleGastoViewModel
    {
        public RegsitroProductoParaFacturacionViewModels Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public string VersionFila { get; set; }
        public decimal Igv { get; set; }
        public decimal Descuento { get; set; }
        //public string Lote { get; set; }
        //public DateTime? Vencimiento { get; set; }
        //public string Registro { get; set; }
        public string Observacion { get; set; }

        public RegistroDetalleGastoViewModel()
        {

        }
    }

    public class RegistroGastoViewModel_
    {
        public ComboActorComercialViewModel Proveedor { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<RegistroDetalleFinanciamientoViewModel> Cuotas { get; set; }
        public DateTime FechaEmision { get; set; }
        public ComboGenerico Concepto { get; set; }
        public string Detalle { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public bool EsGastoACredito { get; set; }
        public bool EsCreditoRapido { get; set; }
        public string Observacion { get; set; }

    }
}

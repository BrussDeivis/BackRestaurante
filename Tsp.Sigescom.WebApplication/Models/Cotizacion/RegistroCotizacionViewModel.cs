using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroCotizacionViewModel
    {
        public long Id { get; set; }
        public long IdOrden { get; set; }
        public int IdEstado { get; set; }
        public long IdComprobante { get; set; }
        public bool GrabaIgv { get; set; }
        public string Alias { get; set; }
        public string Observacion { get; set; }
        public decimal Flete { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public ComboActorComercialViewModel Cliente { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<RegistroDetalleVentaViewModel> Detalles { get; set; }

        public RegistroCotizacionViewModel()
        {

        }

        public RegistroCotizacionViewModel(OrdenDeCotizacion ordenDeCotizacion, List<SelectorTipoDeComprobante> tiposComprobantes)
        {
            Id = ordenDeCotizacion.IdCotizacion;
            IdOrden = ordenDeCotizacion.Id;
            IdEstado = ordenDeCotizacion.IdEstadoActual;
            IdComprobante = ordenDeCotizacion.Comprobante().Id;
            GrabaIgv = ordenDeCotizacion.Detalles().Sum(d => d.Igv) > 0;
            Alias = ordenDeCotizacion.AliasCliente().Replace("(","").Replace(")","");
            Observacion = ordenDeCotizacion.Observacion();
            Flete = ordenDeCotizacion.Flete;
            FechaVencimiento = ordenDeCotizacion.FechaVencimiento;
            Cliente = new ComboActorComercialViewModel(ordenDeCotizacion.Cliente().Id, "nn", "nn");
            TipoDeComprobante = tiposComprobantes.SingleOrDefault(sc => sc.TipoComprobante.Id == ordenDeCotizacion.Comprobante().IdTipo);
            if (TipoDeComprobante.Series.Count() > 1) { TipoDeComprobante.SerieSeleccionada = (int)ordenDeCotizacion.Comprobante().IdSerie; }
            else { TipoDeComprobante.SerieSeleccionada = 0; }
            Detalles = RegistroDetalleVentaViewModel.Convert_(ordenDeCotizacion.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());

        }
    }


    public class RegistroDetalleCotizacionViewModel
    {
        public long IdDetalle { get; set; }
        public int IdPrecioUnitario { get; set; }
        public ProductoParaVentaViewModel Producto { get; set; }
        public bool PrecioCalculadoVenta { get; set; }
        public string VersionFila { get; set; }
        public string Observacion { get; set; }
        public decimal Stock { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal Igv { get; set; }
        public decimal Descuento { get; set; }

        public RegistroDetalleCotizacionViewModel()
        {

        }

    }
}
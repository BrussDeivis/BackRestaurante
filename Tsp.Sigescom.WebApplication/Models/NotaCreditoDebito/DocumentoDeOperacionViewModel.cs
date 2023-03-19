using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class DocumentoDeOperacionViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdOperacion { get; set; }
        public string IdEncriptado { get; set; }
        public long IdOrden { get; set; }
        public int IdCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string EtiquetaDocumentoIdentidadTercero { get; set; }
        public string DocumentoIdentidadTercero { get; set; }
        public string EtiquetaTercero { get; set; }
        public string NombreTercero { get; set; }
        public string DireccionTercero { get; set; }
        public string EtiquetaNombreDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
        public string HayRegistroFlete { get; set; }
        public decimal Flete { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public bool AccionInvalidar { get; set; }
        public bool AccionEmitirNota { get; set; }
        public string CadenaHtmlDeComprobante80 { get; set; }
        public string CadenaHtmlDeComprobanteA4 { get; set; }
        public bool EsOrden { get; set; }
        public bool TieneGuiaDeRemision { get; set; }
        public List<string> CadenasHtmlDeGuiaDeRemision80 { get; set; }
        public List<string> CadenasHtmlDeGuiaDeRemisionA4 { get; set; }
        public string Formato { get; set; }
        public bool HayMovimientoEconomico { get => Total == TotalMovimientoEconomico; }
        public decimal TotalMovimientoEconomico { get; set; }
        public bool MostrarEntregaAlmacen { get; set; }
        public bool HayMovimientoAlmacen { get; set; }
        public bool EstadoAlmacenPendiente { get; set; }
        public bool EstadoAlmacenParcial { get; set; }
        public bool EstadoAlmacenCompletada { get; set; }
        public List<DetalleDeOrdenAlmacen> DetallesOrdenAlmacen { get; set; }
        public bool EstaPagadoConPuntos { get; set; }

        public DocumentoDeOperacionViewModel()
        {
        }

        public DocumentoDeOperacionViewModel(OrdenDeCompra ordenDeCompra, string cadenaHtmlDeComprobante80,/* string cadenaHtmlDeComprobanteA4,*/ bool tieneGuiaDeRemision, List<string> cadenasHtmlDeGuiaDeRemision80/*, List<string> cadenasHtmlDeGuiaDeRemisionA4*/)
        {
            this.IdOperacion = ordenDeCompra.IdCompra;
            this.IdOrden = ordenDeCompra.Id;
            this.EtiquetaDocumentoIdentidadTercero = ordenDeCompra.Proveedor().CodigoTipoDocumentoIdentidad();
            this.DocumentoIdentidadTercero = ordenDeCompra.Proveedor().DocumentoIdentidad;
            this.EtiquetaTercero = ordenDeCompra.Proveedor().NombreRolActorNegocio();
            this.NombreTercero = ordenDeCompra.Proveedor().RazonSocial;
            this.DireccionTercero = (ordenDeCompra.Proveedor().DomicilioFiscal() != null) ? (ordenDeCompra.Proveedor().DomicilioFiscal().detalle + " , " + ordenDeCompra.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "-";
            this.EtiquetaNombreDocumento = ordenDeCompra.Comprobante().Tipo().Nombre;
            this.SerieNumeroDocumento = ordenDeCompra.Comprobante().NumeroDeComprobante > 0 ? ordenDeCompra.Comprobante().NumeroDeSerie + " - " + ordenDeCompra.Comprobante().NumeroDeComprobante : ordenDeCompra.Comprobante().NombreCortoTipo;
            this.Fecha = ordenDeCompra.FechaOrden().ToString("dd/MM/yyyy");
            this.Estado = ordenDeCompra.EstadoActual().nombre;
            this.HayRegistroFlete = System.Convert.ToString(ordenDeCompra.Flete != 0);
            this.Flete = ordenDeCompra.Flete;
            this.SubTotal = ordenDeCompra.Total - ordenDeCompra.Igv();
            this.Igv = ordenDeCompra.Igv();
            this.Total = ordenDeCompra.Total;
            this.TotalMovimientoEconomico = ordenDeCompra.Cuotas().Sum(c => c.pago_a_cuenta);
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(ordenDeCompra.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            this.AccionInvalidar = ordenDeCompra.esAnulableConNotaInterna();
            this.AccionEmitirNota = ordenDeCompra.esAnulableConNotaDeCredito();
            this.CadenaHtmlDeComprobante80 = cadenaHtmlDeComprobante80;
            //this.CadenaHtmlDeComprobanteA4 = cadenaHtmlDeComprobanteA4;
            this.TieneGuiaDeRemision = tieneGuiaDeRemision;
            this.CadenasHtmlDeGuiaDeRemision80 = cadenasHtmlDeGuiaDeRemision80;
            //this.CadenasHtmlDeGuiaDeRemisionA4 = cadenasHtmlDeGuiaDeRemisionA4;
            this.Formato = "210mm";
        }

        public DocumentoDeOperacionViewModel(OrdenDeVenta ordenDeVenta, string cadenaHtmlDeComprobante80, string cadenaHtmlDeComprobanteA4, bool tieneGuiaDeRemision, List<string> cadenasHtmlDeGuiaDeRemision80, List<string> cadenasHtmlDeGuiaDeRemisionA4, string idEncriptado)
        {
            this.IdOperacion = ordenDeVenta.IdVenta;
            this.IdOrden = ordenDeVenta.Id; 
            this.IdEncriptado = idEncriptado; 
            this.IdCliente = ordenDeVenta.IdCliente;
            this.TelefonoCliente = ordenDeVenta.Cliente().Telefono();
            this.SerieNumeroDocumento = ordenDeVenta.Comprobante().NumeroDeComprobante > 0 ? ordenDeVenta.Comprobante().NumeroDeSerie + "-" + ordenDeVenta.Comprobante().NumeroDeComprobante : ordenDeVenta.Comprobante().NombreCortoTipo;
            this.Fecha = ordenDeVenta.FechaOrden().ToString("dd/MM/yyyy");
            this.SubTotal = ordenDeVenta.Total - ordenDeVenta.Igv();
            this.Igv = ordenDeVenta.Igv();
            this.Total = ordenDeVenta.Total;
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(ordenDeVenta.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList(), ordenDeVenta.ValorIcbper());
            this.AccionInvalidar = ordenDeVenta.EsAnulableConNotaInterna();
            this.AccionEmitirNota = ordenDeVenta.EsAnulableConNotaDeCredito();
            this.CadenaHtmlDeComprobante80 = cadenaHtmlDeComprobante80;
            this.CadenaHtmlDeComprobanteA4 = cadenaHtmlDeComprobanteA4;
            this.EsOrden = ordenDeVenta.EsOrdenDeVenta();
            this.TieneGuiaDeRemision = tieneGuiaDeRemision;
            this.TotalMovimientoEconomico = ordenDeVenta.Cuotas().Sum(c => c.pago_a_cuenta);
            this.MostrarEntregaAlmacen = VentasSettings.Default.MostrarSeccionEntregaEnVenta;
            this.HayMovimientoAlmacen = ordenDeVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoCompletada || ordenDeVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoParcial;
            this.EstadoAlmacenPendiente = ordenDeVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoPendiente;
            this.EstadoAlmacenParcial = ordenDeVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoParcial;
            this.EstadoAlmacenCompletada = ordenDeVenta.IdEstadoActualOrdenAlmacen == MaestroSettings.Default.IdDetalleMaestroEstadoCompletada;
            this.CadenasHtmlDeGuiaDeRemision80 = cadenasHtmlDeGuiaDeRemision80;
            this.CadenasHtmlDeGuiaDeRemisionA4 = cadenasHtmlDeGuiaDeRemisionA4;
            this.EstaPagadoConPuntos = ordenDeVenta.Venta().ObtenerPagos().FirstOrDefault(p => p.TrazaDePago().MedioDePago().id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos) != null;
        }
    }
    public class CadenasHtmlVentaYGuia
    {
        public string CadenaHtmlDeComprobante { get; set; }
        public List<string> CadenasHtmlDeGuiaDeRemision { get; set; }

        public CadenasHtmlVentaYGuia()
        {
        }

        public CadenasHtmlVentaYGuia(string cadenaHtmlDeComprobante, List<string> cadenasHtmlDeGuiaDeRemision)
        {
            CadenaHtmlDeComprobante = cadenaHtmlDeComprobante;
            CadenasHtmlDeGuiaDeRemision = cadenasHtmlDeGuiaDeRemision;
        }
    }

    public class DocumentoComprobanteOrdenVenta
    {
        public long Id { get; set; }
        public long IdOrden { get; set; }
        public string CadenaHtmlDeComprobante80 { get; set; }
        public string CadenaHtmlDeComprobanteA4 { get; set; }

        public DocumentoComprobanteOrdenVenta()
        {
        }

    }
}

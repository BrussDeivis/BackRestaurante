using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class CotizacionViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public long IdCotizacion { get; set; }
        //public string Fecha { get; set; }
        ////public long IdTipoDocumento { get; set; }
        //public string EtiquetaNombreDocumento { get; set; }
        //public string EtiquetaDocumentoIdentidad { get; set; }
        //public string DocumentoIdentidadProveedor { get; set; }
        //public string NombreDocumento { get; set; }
        //public string SerieNumeroDocumento { get; set; }
        //public int IdProveedor { get; set; }
        //public string Proveedor { get; set; }
        //public string DireccionProveedor { get; set; }
        //public string TelefonoProveedor { get; set; }
        //public string Empleado { get; set; }
        //public string SubTotal { get; set; }
        //public string Igv { get; set; }
        //public string Total { get; set; }
        //public string Estado { get; set; }
        //public string Flete { get; set; }
        //public List<DetalleOrdenVentaCompraGastoAlmacenViewModel> Detalles { get; set; }
        //public bool AccionInvalidar { get; set; }
        //public bool AccionAnular { get; set; }
        public string CadenaHtmlDeDocumento { get; set; }

        public CotizacionViewModel(OrdenDeCotizacion ordenDeCotizacion)
        {
            this.IdOrden = ordenDeCotizacion.Id;
            this.IdCotizacion = ordenDeCotizacion.IdCotizacion;
            //this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            ////this.IdTipoDocumento = orden.Comprobante().Serie().id;
            //this.NombreDocumento = orden.Comprobante().Tipo().Nombre;
            //this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante != "0" ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            //this.IdProveedor = orden.Proveedor().Id;
            //this.Proveedor = orden.Proveedor().RazonSocial;
            //this.DocumentoIdentidadProveedor = orden.Proveedor().DocumentoIdentidad;
            //this.DireccionProveedor = (orden.Proveedor().DomicilioFiscal() != null) ? (orden.Proveedor().DomicilioFiscal().detalle + " , " + orden.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            //this.TelefonoProveedor = orden.Proveedor().Telefono() ?? "";
            //this.EtiquetaNombreDocumento = orden.Comprobante().Tipo().Nombre;
            //this.EtiquetaDocumentoIdentidad = orden.Proveedor().CodigoTipoDocumentoIdentidad();
            //this.Empleado = orden.Empleado().Nombres;
            //this.Igv = orden.Igv().ToString("0.00");
            //this.Total = orden.Total.ToString("0.00");
            //this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("0.00");
            //this.Estado = orden.EstadoActual.nombre;
            //this.Flete = orden.Flete.ToString("0.00");
            //this.Detalles = DetalleOrdenVentaCompraGastoAlmacenViewModel.convert(orden.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            //this.AccionInvalidar = orden.esAnulableConNotaInterna();
            //this.AccionAnular = orden.esAnulableConNotaDeCredito();
        }

        public CotizacionViewModel(OrdenDeCotizacion ordenDeCotizacion, string cadenaHtmlDeDocumento)
        {
            this.IdOrden = ordenDeCotizacion.Id;
            this.IdCotizacion = ordenDeCotizacion.IdCotizacion;
            //this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            ////this.IdTipoDocumento = orden.Comprobante().Serie().id;
            //this.NombreDocumento = orden.Comprobante().Tipo().Nombre;
            //this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante != "0" ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            //this.IdProveedor = orden.Proveedor().Id;
            //this.Proveedor = orden.Proveedor().RazonSocial;
            //this.DocumentoIdentidadProveedor = orden.Proveedor().DocumentoIdentidad;
            //this.DireccionProveedor = (orden.Proveedor().DomicilioFiscal() != null) ? (orden.Proveedor().DomicilioFiscal().detalle + " , " + orden.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            //this.TelefonoProveedor = orden.Proveedor().Telefono() ?? "";
            //this.EtiquetaNombreDocumento = orden.Comprobante().Tipo().Nombre;
            //this.EtiquetaDocumentoIdentidad = orden.Proveedor().CodigoTipoDocumentoIdentidad();
            //this.Empleado = orden.Empleado().Nombres;
            //this.Igv = orden.Igv().ToString("0.00");
            //this.Total = orden.Total.ToString("0.00");
            //this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("0.00");
            //this.Estado = orden.EstadoActual.nombre;
            //this.Flete = orden.Flete.ToString("0.00");
            //this.Detalles = DetalleOrdenVentaCompraGastoAlmacenViewModel.convert(orden.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            //this.AccionInvalidar = orden.esAnulableConNotaInterna();
            //this.AccionAnular = orden.esAnulableConNotaDeCredito();
            this.CadenaHtmlDeDocumento = cadenaHtmlDeDocumento;
        }

        public static List<CotizacionViewModel> Convert(List<OrdenDeCotizacion> ordenesDeCotizacion)
        {
            List<CotizacionViewModel> ordenes = new List<CotizacionViewModel>();
            foreach (var ordenDeCotizacion in ordenesDeCotizacion)
            {
                ordenes.Add(new CotizacionViewModel(ordenDeCotizacion));
            }
            return ordenes;
        }
    }
}
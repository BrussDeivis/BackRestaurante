using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class DetalleCuotaOperacionViewModel
    {
        public string Codigo { get; set; }
        public string Emision { get; set; }
        public string Vencimiento { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Total { get; set; }
        public string CadenaHtmlDeComprobante { get; set; }
        public OperacionViewModel Operacion { get; set; }

        public DetalleCuotaOperacionViewModel(){}

        public DetalleCuotaOperacionViewModel(CuentaPorCobrarPagar cuenta, string cadenaHtml)
        {
            this.Codigo = cuenta.Codigo;
            this.Emision = cuenta.FechaDeEmision.ToString("dd/MM/yyyy");
            this.Vencimiento = cuenta.FechaDeVencimiento.ToString("dd/MM/yyyy");
            this.Capital = cuenta.Capital();
            this.Interes = cuenta.Interes();
            this.Total = cuenta.Total();
            //this.Operacion = new OperacionViewModel();
            //this.Operacion.Id = cuenta.IdOperacionBase;
            //this.Operacion.Fecha = cuenta.OperacionBase().FechaEmision.ToString("dd/MM/yyyy");
            //this.Operacion.EtiquetaNombreTercero = cuenta.OperacionBase().Tercero().NombreRolActorNegocio();
            //this.Operacion.EtiquetaDocumentoIdentidad = cuenta.OperacionBase().Tercero().CodigoTipoDocumentoIdentidad();
            //this.Operacion.NombreDocumento = cuenta.OperacionBase().Comprobante().Tipo().Nombre;
            //this.Operacion.SerieNumeroDocumento = cuenta.OperacionBase().Comprobante().NumeroDeComprobante >0 ? cuenta.OperacionBase().Comprobante().NumeroDeSerie + "-" + cuenta.OperacionBase().Comprobante().NumeroDeComprobante : cuenta.OperacionBase().Comprobante().NombreCortoTipo;
            //this.Operacion.IdProveedor = cuenta.OperacionBase().Tercero().Id;
            //this.Operacion.Proveedor = cuenta.OperacionBase().Tercero().RazonSocial;
            //this.Operacion.DocumentoIdentidadProveedor = cuenta.OperacionBase().Tercero().DocumentoIdentidad;
            //this.Operacion.DireccionProveedor = (cuenta.OperacionBase().Tercero().DomicilioFiscal() != null) ? (cuenta.OperacionBase().Tercero().DomicilioFiscal().detalle + " , " + cuenta.OperacionBase().Tercero().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            //this.Operacion.TelefonoProveedor = (cuenta.OperacionBase().Tercero().Telefono() != null) ? cuenta.OperacionBase().Tercero().Telefono() : "";
            //this.Operacion.Empleado = cuenta.OperacionBase().Empleado().Nombres;
            //this.Operacion.Igv = cuenta.OperacionBase().Igv().ToString("N2");
            //this.Operacion.Total = cuenta.OperacionBase().Total.ToString("N2");
            //this.Operacion.SubTotal = (System.Convert.ToDecimal(Operacion.Total) - System.Convert.ToDecimal(Operacion.Igv)).ToString("N2");
            //this.Operacion.Estado = cuenta.OperacionBase().EstadoActual().nombre;
            //this.Operacion.Flete = cuenta.OperacionBase().Flete.ToString("N2");
            //this.Operacion.Detalles = DetalleOrdenDeOperacionViewModel.Convert(DetalleDeOperacion.Convert(cuenta.OperacionBase().DetalleTransaccion().Where(d => d.id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList()));
            this.CadenaHtmlDeComprobante = cadenaHtml;
        }
    }
    public class OperacionViewModel
    {
        public long Id { get; set; }
        public long IdOperacion { get; set; }
        public string Fecha { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public string EtiquetaNombreTercero { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string EtiquetaDocumentoIdentidad { get; set; }
        public string DocumentoIdentidadProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string Empleado { get; set; }
        public string SubTotal { get; set; }
        public string Igv { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public string Flete { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }

        public OperacionViewModel() { }
    }
}
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
    public class CompraConDetallesViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdCompra { get; set; }
        public string Fecha { get; set; }
        //public long IdTipoDocumento { get; set; }
        public string EtiquetaNombreDocumento { get; set; }
        public string EtiquetaDocumentoIdentidad { get; set; }
        public string DocumentoIdentidadProveedor { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string Empleado { get; set; }
        public string SubTotal { get; set; }
        public string Igv { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public string Flete { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public bool AccionInvalidar { get; set; }
        public bool AccionAnular { get; set; }

        public CompraConDetallesViewModel(OrdenDeCompra orden)
        {
            this.Id = orden.Id;
            this.IdCompra = orden.IdCompra;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            //this.IdTipoDocumento = orden.Comprobante().Serie().id;
            this.NombreDocumento = orden.Comprobante().Tipo().Nombre;
            this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante >0 ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            this.IdProveedor = orden.Proveedor().Id;
            this.Proveedor = orden.Proveedor().RazonSocial;
            this.DocumentoIdentidadProveedor = orden.Proveedor().DocumentoIdentidad;
            this.DireccionProveedor = (orden.Proveedor().DomicilioFiscal() != null) ? (orden.Proveedor().DomicilioFiscal().detalle + " , " + orden.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            this.TelefonoProveedor = orden.Proveedor().Telefono() ?? "";
            this.EtiquetaNombreDocumento = orden.Comprobante().Tipo().Nombre;
            this.EtiquetaDocumentoIdentidad = orden.Proveedor().CodigoTipoDocumentoIdentidad();
            this.Empleado = orden.Empleado().Nombres;
            this.Igv = orden.Igv().ToString("N2");
            this.Total = orden.Total.ToString("N2");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.Flete = orden.Flete.ToString("N2");
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            this.AccionInvalidar = orden.esAnulableConNotaInterna();
            this.AccionAnular = orden.esAnulableConNotaDeCredito();
        }

        public static List<CompraConDetallesViewModel> Convert(List<OrdenDeCompra> ordenDeCompras)
        {
            List<CompraConDetallesViewModel> ordenes = new List<CompraConDetallesViewModel>();
            foreach (var ordenDeCompra in ordenDeCompras)
            {
                ordenes.Add(new CompraConDetallesViewModel(ordenDeCompra));
            }
            return ordenes;
        }

    }
}
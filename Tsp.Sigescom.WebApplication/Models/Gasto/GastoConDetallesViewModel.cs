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
    public class GastoConDetallesViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdGasto { get; set; }
        public string Fecha { get; set; }
        //public long IdTipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string DocumentoIdentidadProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string Empleado { get; set; }
        public string SubTotal { get; set; }
        public string Igv { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string Flete { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }


        public GastoConDetallesViewModel(OrdenDeGasto orden)
        {
            this.Id = orden.Id;
            this.IdGasto = orden.IdGasto;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            //this.IdTipoDocumento = orden.Comprobante().Serie().id;
            this.NombreDocumento = orden.Comprobante().Tipo().Nombre;
            this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante >0 ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            this.IdProveedor = orden.proveedor().Id;
            this.Proveedor = orden.proveedor().RazonSocial;
            this.DocumentoIdentidadProveedor = orden.proveedor().DocumentoIdentidad;
            this.DireccionProveedor = (orden.proveedor().DomicilioFiscal() != null) ? (orden.proveedor().DomicilioFiscal().detalle + " , " + orden.proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            this.TelefonoProveedor = (orden.proveedor().Telefono() != null) ? orden.proveedor().Telefono() : "";
            this.Empleado = orden.empleado().Nombres;
            this.Igv = orden.Igv().ToString("N2");
            this.Total = orden.Total.ToString("N2");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.Observacion = orden.Comentario;
            this.Flete = orden.Flete.ToString("N2");
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());

        }

        public static List<GastoConDetallesViewModel> Convert(List<OrdenDeGasto> ordenDeGastos)
        {
            List<GastoConDetallesViewModel> ordenes = new List<GastoConDetallesViewModel>();
            foreach (var ordenDeGasto in ordenDeGastos)
            {
                ordenes.Add(new GastoConDetallesViewModel(ordenDeGasto));
            }
            return ordenes;
        }

    }
}
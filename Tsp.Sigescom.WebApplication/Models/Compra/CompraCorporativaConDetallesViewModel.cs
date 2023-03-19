using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{

    [Serializable]
    [DataContract]
    public class CompraCorporativaViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public long IdCompra { get; set; }
        public bool AccionEditar { get; set; }
        public bool AccionConfirmar { get; set; }
        public bool AccionInvalidar { get; set; }
        public bool AccionAnular { get; set; }
        public bool AccionModificarComprobante { get; set; }
        public bool AccionGenerarOrdenDeAlmacen { get; set; }
        public string CadenaHtmlDeDocumento { get; set; }

        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public long Id { get; set; }
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

        public CompraCorporativaViewModel()
        {

        }

        public CompraCorporativaViewModel(OrdenDeCompra ordenDeCompra, string cadenaHtmlDeDocumento)
        { 
            this.IdOrden = ordenDeCompra.Id; 
            this.IdCompra = ordenDeCompra.IdCompra;
            this.AccionEditar = ordenDeCompra.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            this.AccionConfirmar = ordenDeCompra.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            this.AccionInvalidar = ordenDeCompra.esAnulableConNotaInterna();
            this.AccionAnular = ordenDeCompra.esAnulableConNotaDeCredito();
            this.AccionModificarComprobante = ordenDeCompra.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            this.AccionGenerarOrdenDeAlmacen = !(bool)ordenDeCompra.SeGeneroOrdenDeAlmacenTotalMente();
            this.CadenaHtmlDeDocumento = cadenaHtmlDeDocumento;
            //todo: verificar los datos que esta recibiendo la confirmacion de la compra corporaativa, fue un cambio por el momomento, verificar
            this.Detalles = DetalleOrdenDeOperacionViewModel.ConvertirABienes(ordenDeCompra.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            this.Id = ordenDeCompra.Id;
            this.Fecha = ordenDeCompra.FechaOrden().ToString("dd/MM/yyyy");
            //this.IdTipoDocumento = orden.Comprobante().Serie().id;
            this.NombreDocumento = ordenDeCompra.Comprobante().Tipo().Nombre;
            this.SerieNumeroDocumento = ordenDeCompra.Comprobante().NumeroDeComprobante > 0 ? ordenDeCompra.Comprobante().NumeroDeSerie + "-" + ordenDeCompra.Comprobante().NumeroDeComprobante : ordenDeCompra.Comprobante().NombreCortoTipo;
            this.IdProveedor = ordenDeCompra.Proveedor().Id;
            this.Proveedor = ordenDeCompra.Proveedor().RazonSocial;
            this.DocumentoIdentidadProveedor = ordenDeCompra.Proveedor().DocumentoIdentidad;
            this.DireccionProveedor = (ordenDeCompra.Proveedor().DomicilioFiscal() != null) ? (ordenDeCompra.Proveedor().DomicilioFiscal().detalle + " , " + ordenDeCompra.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            this.TelefonoProveedor = ordenDeCompra.Proveedor().Telefono() ?? "";
            this.EtiquetaNombreDocumento = ordenDeCompra.Comprobante().Tipo().Nombre;
            this.EtiquetaDocumentoIdentidad = ordenDeCompra.Proveedor().CodigoTipoDocumentoIdentidad();
            this.Empleado = ordenDeCompra.Empleado().Nombres;
            this.Igv = ordenDeCompra.Igv().ToString("N2");
            this.Total = ordenDeCompra.Total.ToString("N2");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("N2");
            this.Estado = ordenDeCompra.EstadoActual().nombre;
            this.Flete = ordenDeCompra.Flete.ToString("N2");
        }
    }


    /*
    [Serializable]
    [DataContract]
    public class CompraCorporativaConDetallesViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public long IdCompra { get; set; }
        public string Fecha { get; set; }
        public string EtiquetaNombreDocumento { get; set; }
        public string EtiquetaDocumentoIdentidad { get; set; }
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
        public string Flete { get; set; }
        public string Inicial { get; set; }
        public int ModoDePago { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }
        public bool AccionEditar { get; set; }
        public bool AccionConfirmar { get; set; }
        public bool AccionInvalidar { get; set; }
        public bool AccionAnular { get; set; }
        public bool AccionModificarComprobante { get; set; }
        public bool AccionGenerarOrdenDeAlmacen { get; set; }

        public CompraCorporativaConDetallesViewModel(OrdenDeCompra orden)
        {
            this.IdOrden = orden.Id;
            this.IdCompra = orden.IdCompra;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.EtiquetaNombreDocumento = orden.Comprobante().Tipo().Nombre;
            this.EtiquetaDocumentoIdentidad = orden.Proveedor().CodigoTipoDocumentoIdentidad();
            this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante >0 ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
            this.IdProveedor = orden.Proveedor().Id;
            this.Proveedor = orden.Proveedor().RazonSocial;
            this.DocumentoIdentidadProveedor = orden.Proveedor().DocumentoIdentidad;
            this.DireccionProveedor = (orden.Proveedor().DomicilioFiscal() != null) ? (orden.Proveedor().DomicilioFiscal().detalle + " , " + orden.Proveedor().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
            this.TelefonoProveedor = (orden.Proveedor().Telefono() != null) ? orden.Proveedor().Telefono() : "";
            this.Empleado = orden.Empleado().Nombres;
            this.Igv = orden.Igv().ToString("0.00");
            this.Total = orden.Total.ToString("0.00");
            this.SubTotal = (System.Convert.ToDecimal(Total) - System.Convert.ToDecimal(Igv)).ToString("0.00");
            this.Estado = orden.EstadoActual.nombre;
            this.Inicial = orden.Cuotas().SingleOrDefault(c => c.cuota_inicial == true) != null ? orden.Cuotas().SingleOrDefault(c => c.cuota_inicial == true).Saldo().ToString("0.00") : "";
            this.ModoDePago = System.Convert.ToInt32(orden.ModoDePago());
            this.Flete = orden.Flete.ToString("0.00");
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
            this.AccionEditar = orden.EstadoActual.id == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            this.AccionConfirmar = orden.EstadoActual.id == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            this.AccionInvalidar = orden.esAnulableConNotaInterna();
            this.AccionAnular = orden.esAnulableConNotaDeCredito();
            this.AccionModificarComprobante = orden.EstadoActual.id == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            this.AccionGenerarOrdenDeAlmacen = !(bool)orden.SeGeneroOrdenDeAlmacenTotalMente();
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
    */
    public class ConfirmacionCompraViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public bool HayRegistroTrazaPago { get; set; }
        public bool HayRegistroMovimientoDeAlmacen { get; set; }
        public bool UsaComprobanteOrden { get; set; }

        public ConfirmacionCompraViewModel()
        {
            
        }
    }
}
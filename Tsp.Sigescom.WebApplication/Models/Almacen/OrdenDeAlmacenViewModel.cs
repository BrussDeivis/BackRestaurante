using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class OrdenDeAlmacenViewModel
    {
        [DataMember]
        public long IdOrdenDeAlmacen;
        public string CadenaHtmlDeOrdenDeAlmacen { get; set; }

        public OrdenDeAlmacenViewModel()
        {
        }

        public OrdenDeAlmacenViewModel(long idOrdenDeAlmacen, string cadenaHtmlDeOrdenDeAlmacen)
        {
            IdOrdenDeAlmacen = idOrdenDeAlmacen;
            CadenaHtmlDeOrdenDeAlmacen = cadenaHtmlDeOrdenDeAlmacen;
        }
    }

    public class OrdenDeAlmacenParaMovimientoDeAlmacenViewModel
    {
        public long IdOrdenDeAlmacen;
        public string EtiquetaDocumentoIdentidadTercero { get; set; }
        public string DocumentoIdentidadTercero { get; set; }
        public string EtiquetaNombreTercero { get; set; }
        public int IdTercero { get; set; }
        public string Tercero { get; set; }
        public int IdUbigeoTercero { get; set; }
        public string DireccionTercero { get; set; }
        public string UbigeoTercero { get; set; }
        public List<DetalleMovimientoDeAlmacenViewModel> Detalles;

        public OrdenDeAlmacenParaMovimientoDeAlmacenViewModel()
        {
        }

        public OrdenDeAlmacenParaMovimientoDeAlmacenViewModel(OrdenDeMovimientoDeAlmacen ordenDeMovimiento)
        {
            IdOrdenDeAlmacen = ordenDeMovimiento.Id;
            this.EtiquetaDocumentoIdentidadTercero = ordenDeMovimiento.Tercero().CodigoTipoDocumentoIdentidad();
            this.DocumentoIdentidadTercero = ordenDeMovimiento.Tercero().DocumentoIdentidad;
            this.EtiquetaNombreTercero = ordenDeMovimiento.Tercero().NombreRolActorNegocio();
            this.IdTercero = ordenDeMovimiento.Tercero().Id;
            this.Tercero = ordenDeMovimiento.Tercero().RazonSocial;
            this.IdUbigeoTercero = (ordenDeMovimiento.Tercero().DomicilioFiscal() != null) ? ordenDeMovimiento.Tercero().DomicilioFiscal().Ubigeo.id : 0;
            this.UbigeoTercero = (ordenDeMovimiento.Tercero().DomicilioFiscal() != null) ? ordenDeMovimiento.Tercero().DomicilioFiscal().Ubigeo.descripcion_larga : "" ;
            this.DireccionTercero = (ordenDeMovimiento.Tercero().DomicilioFiscal() != null) ? ordenDeMovimiento.Tercero().DomicilioFiscal().detalle : "";
            Detalles = DetalleMovimientoDeAlmacenViewModel.Convertir(DetalleDeOperacion.Convert(ordenDeMovimiento.DetalleTransaccion()), ordenDeMovimiento.MovimientosDeAlmacen());
        }
    }

    //public class OrdenDeAlmacenParaMovimientoDeAlmacenViewModel
    //{
    //    public long IdOrden { get; set; }
    //    public long IdOperacion { get; set; }
    //    public string NombreTipoDocumento { get; set; }
    //    public string SerieNumeroDocumento { get; set; }
    //    public int IdTercero { get; set; }
    //    public string EtiquetaDocumentoIdentidad { get; set; }
    //    public string DocumentoIdentidadTercero { get; set; }
    //    public string EtiquetaNombreTercero { get; set; }
    //    public string Tercero { get; set; }
    //    public string DireccionTercero { get; set; }
    //    public string TelefonoTercero { get; set; }
    //    public string Empleado { get; set; }
    //    public string Fecha { get; set; }
    //    public string Estado { get; set; }
    //    public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }


    //    public OrdenDeAlmacenParaMovimientoDeAlmacenViewModel(OrdenDeTrasladoInterno orden)
    //    {
    //        this.IdOrden = orden.Id;
    //        this.IdOperacion = orden.IdDesplazamiento(); 
    //        this.NombreTipoDocumento = orden.Comprobante().Tipo().Nombre;
    //        this.SerieNumeroDocumento = orden.Comprobante().SerieYNumero();
    //        this.IdTercero = orden.Tercero().Id;
    //        this.EtiquetaDocumentoIdentidad = orden.Tercero().CodigoTipoDocumentoIdentidad();
    //        this.DocumentoIdentidadTercero = orden.Tercero().DocumentoIdentidad;
    //        this.EtiquetaNombreTercero = orden.Tercero().NombreRolActorNegocio();
    //        this.Tercero = orden.Tercero().RazonSocial;
    //        this.DireccionTercero = (orden.Tercero().DomicilioFiscal() != null) ? (orden.Tercero().DomicilioFiscal().detalle + ", " + orden.Tercero().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
    //        this.TelefonoTercero = orden.Tercero().Telefono() ?? "";
    //        this.NombreTipoDocumento = orden.Comprobante().Tipo().Nombre;
    //        this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante >0 ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
    //        this.Empleado = orden.Empleado().Nombres;
    //        this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
    //        this.Estado = orden.EstadoActual.nombre;
    //        this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
    //    }


    //    public OrdenDeAlmacenParaMovimientoDeAlmacenViewModel(OrdenDeMovimientoDeAlmacen orden)
    //    {
    //        this.IdOrden = orden.Id;
    //        this.NombreTipoDocumento = orden.Comprobante().Tipo().Nombre;
    //        this.SerieNumeroDocumento = orden.Comprobante().SerieYNumero();
    //        this.IdTercero = orden.Tercero().Id;
    //        this.EtiquetaDocumentoIdentidad = orden.Tercero().CodigoTipoDocumentoIdentidad();
    //        this.DocumentoIdentidadTercero = orden.Tercero().DocumentoIdentidad;
    //        this.EtiquetaNombreTercero = orden.Tercero().NombreRolActorNegocio();
    //        this.Tercero = orden.Tercero().RazonSocial;
    //        this.DireccionTercero = (orden.Tercero().DomicilioFiscal() != null) ? (orden.Tercero().DomicilioFiscal().detalle + ", " + orden.Tercero().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
    //        this.TelefonoTercero = orden.Tercero().Telefono() ?? "";
    //        this.NombreTipoDocumento = orden.Comprobante().Tipo().Nombre;
    //        this.SerieNumeroDocumento = orden.Comprobante().NumeroDeComprobante >0 ? orden.Comprobante().NumeroDeSerie + "-" + orden.Comprobante().NumeroDeComprobante : orden.Comprobante().NombreCortoTipo;
    //        this.Empleado = orden.Empleado().Nombres;
    //        this.Fecha = orden.FechaEmision.ToString("dd/MM/yyyy");
    //        this.Estado = orden.EstadoActual.nombre;
    //        this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
    //    }

    //    public static List<OrdenDeAlmacenParaMovimientoDeAlmacenViewModel> Convert(List<OrdenDeTrasladoInterno> ordenDeDesplazminetos)
    //    {
    //        List<OrdenDeAlmacenParaMovimientoDeAlmacenViewModel> ordenes = new List<OrdenDeAlmacenParaMovimientoDeAlmacenViewModel>();
    //        foreach (var ordenDeDesplazamiento in ordenDeDesplazminetos)
    //        {
    //            ordenes.Add(new OrdenDeAlmacenParaMovimientoDeAlmacenViewModel(ordenDeDesplazamiento));
    //        }
    //        return ordenes;
    //    }
    //}
}
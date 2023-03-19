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
    public class MovimientoDeAlmacenViewModel
    {
        [DataMember]
        public long IdMovimiento;
        public long IdOrden;
        public string CadenaHtmlDeMovimientoDeAlmacen { get; set; }
        public string CadenaHtmlDeOrdenDeMovimiento { get; set; }
        public MovimientoDeAlmacenViewModel()
        {
        }

        public MovimientoDeAlmacenViewModel(long idMovimiento, string cadenaHtmlDeMovimientoDeAlmacen, string cadenaHtmlDeOrdenDeMovimiento)
        {
            IdMovimiento = idMovimiento;
            CadenaHtmlDeMovimientoDeAlmacen = cadenaHtmlDeMovimientoDeAlmacen;
            CadenaHtmlDeOrdenDeMovimiento = cadenaHtmlDeOrdenDeMovimiento;
        }
    }

    //public OperacionConDetalleDeTrasladoDeMercaderia(OrdenDeMovimientoDeAlmacen ordenDeMovimiento)
    //{
    //    IdOperacion = (long)ordenDeMovimiento.Transaccion().id_transaccion_padre;
    //    IdOrden = ordenDeMovimiento.Id;
    //    this.EtiquetaDocumentoIdentidad = ordenDeMovimiento.Tercero().CodigoTipoDocumentoIdentidad();
    //    this.DocumentoIdentidadTercero = ordenDeMovimiento.Tercero().DocumentoIdentidad;
    //    this.EtiquetaNombreTercero = ordenDeMovimiento.Tercero().NombreRolActorNegocio();
    //    this.Tercero = ordenDeMovimiento.Tercero().RazonSocial;
    //    this.DireccionTercero = (ordenDeMovimiento.Tercero().DomicilioFiscal() != null) ? (ordenDeMovimiento.Tercero().DomicilioFiscal().detalle + ", " + ordenDeMovimiento.Tercero().DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper() : "";
    //    Detalles = DetalleTrasladoDeMercaderiaViewModel.Convertir(DetalleOrdenDeOperacion.Convert_(ordenDeMovimiento.DetalleTransaccion()), ordenDeMovimiento.MovimientosDeMercaderia());
    //}


    //public class MovimientoDeMercaderiaViewModel
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
    //    public List<DetalleOrdenVentaCompraGastoAlmacenViewModel> Detalles { get; set; }


    //    public MovimientoDeMercaderiaViewModel(OrdenDeTrasladoInterno orden)
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
    //        this.Empleado = orden.Empleado().Nombres;
    //        this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
    //        this.Estado = orden.EstadoActual.nombre;
    //        this.Detalles = DetalleOrdenVentaCompraGastoAlmacenViewModel.convert(orden.Detalles());
    //    }

    //    public static List<MovimientoDeMercaderiaViewModel> Convert_(List<OrdenDeTrasladoInterno> ordenDeDesplazminetos)
    //    {
    //        List<MovimientoDeMercaderiaViewModel> ordenes = new List<MovimientoDeMercaderiaViewModel>();
    //        foreach (var ordenDeDesplazamiento in ordenDeDesplazminetos)
    //        {
    //            ordenes.Add(new MovimientoDeMercaderiaViewModel(ordenDeDesplazamiento));
    //        }
    //        return ordenes;
    //    }
    //}
    

  
    
}
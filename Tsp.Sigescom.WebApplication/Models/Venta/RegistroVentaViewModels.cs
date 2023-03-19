using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroVentaViewModel
    {
        public long IdVenta { get; set; }
        public bool GrabaIgv { get; set; }
        public bool DetalleUnificado { get; set; }
        public bool EsVentaPasada { get; set; }
        public bool EsVentaACredito { get; set; }
        public bool EsCreditoRapido { get; set; }
        public bool HayRegistroTrazaPago { get; set; }
        public bool HayRegistroMovimientoMercaderia { get; set; }
        public bool UsaComprobanteOrden { get; set; }
        public string Alias { get; set; }
        public string Observacion { get; set; }
        public decimal Inicial { get; set; }
        public decimal Flete { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ComboActorComercialViewModel Cliente { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<RegistroDetalleFinanciamientoViewModel> Cuotas { get; set; }
        public IEnumerable<RegistroDetalleVentaViewModel> Detalles { get; set; }
        //Informacion de pago de la venta
        public int IdMedioDePago { get; set; }
        public string InformacionDeMedioPago { get; set; }
        public ComboGenericoViewModel EntidadFinanciera { get; set; }
        public ComboGenericoViewModel TipoTarjeta { get; set; }
        public bool HaySalidaDeMercaderia { get; set; }
        public IEnumerable<RegistroMovimientoDeAlmacenViewModel> SalidasDeMercaderia { get; set; }
        public int NumeroBolsasDePlastico { get; set; }
        public decimal Icbper { get; set; }


        public ComboGenericoViewModel PuntoDeVenta { get; set; }
        public ComboGenericoViewModel Almacen { get; set; }
        public ComboGenericoViewModel Caja { get; set; }
        public ComboGenericoViewModel Vendedor { get; set; }
        public ComboGenericoViewModel Cajero { get; set; }
        public ComboGenericoViewModel Almacenero { get; set; }


        public RegistroVentaViewModel()
        {
        }

        public RegistroVentaViewModel(OrdenDeVenta ordenDeVenta, List<SelectorTipoDeComprobante> selectorComprobante)
        {
            this.IdVenta = ordenDeVenta.IdVenta;
            this.Cliente = new ComboActorComercialViewModel(ordenDeVenta.Cliente().Id, ordenDeVenta.Cliente().RazonSocial, ordenDeVenta.Cliente().DocumentoIdentidad);
            this.Alias = ordenDeVenta.AliasCliente();
            this.TipoDeComprobante = selectorComprobante.SingleOrDefault(sc => sc.TipoComprobante.Id == ordenDeVenta.Comprobante().IdTipo);
            if (ordenDeVenta.Comprobante().IdSerie == null)
            {
                TipoDeComprobante.SerieIngresada = ordenDeVenta.Comprobante().NumeroDeSerie;
                TipoDeComprobante.NumeroIngresado = ordenDeVenta.Comprobante().NumeroDeComprobante;
            }
            else
            {
                if (TipoDeComprobante.Series.Count() > 1) { TipoDeComprobante.SerieSeleccionada = (int)ordenDeVenta.Comprobante().IdSerie; }
                else { TipoDeComprobante.SerieSeleccionada = 0; }
            }
            this.TipoDeComprobante.IdComprobante = ordenDeVenta.Comprobante().Id;
            this.FechaRegistro = ordenDeVenta.FechaEmision;
            this.Observacion = ordenDeVenta.Comentario;
            this.GrabaIgv = (ordenDeVenta.Igv() > 0) ? true : false;
            this.DetalleUnificado = true;//ordenDeVenta.DetalleUnificado().Equals("1") ? true : false;
            this.EsVentaACredito = Convert.ToInt32(ordenDeVenta.ModoDePago()) == 1 ? false : true;
            if (EsVentaACredito) { this.EsCreditoRapido = Convert.ToInt32(ordenDeVenta.ModoDePago()) == 3 ? false : true; }
            this.Cuotas = RegistroDetalleFinanciamientoViewModel.Convert_(ordenDeVenta.Cuotas());
            this.Inicial = ordenDeVenta.Cuotas().SingleOrDefault(c => c.cuota_inicial == true) == null ? 0 : ordenDeVenta.Cuotas().SingleOrDefault(c => c.cuota_inicial == true).saldo;
            this.Flete = ordenDeVenta.Flete;
            this.Detalles = RegistroDetalleVentaViewModel.Convert_(ordenDeVenta.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
        }
    }

    public class RegistroDetalleVentaViewModel
    {
        public long IdDetalle { get; set; }
        public int IdPrecioUnitario { get; set; }
        public ProductoParaVentaViewModel Producto { get; set; }
        public bool PrecioCalculadoVenta { get; set; }
        public string VersionFila { get; set; }
        public string Observacion { get; set; }
        public string Lote { get; set; }
        public string Registro { get; set; }
        public decimal Stock { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal Igv { get; set; }
        public decimal Descuento { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string MascaraDeCalculo { get; set; }
        public Concepto_Negocio_Comercial_ ConceptoCompleto { get; set; }

        public RegistroDetalleVentaViewModel()
        {
        }

        public RegistroDetalleVentaViewModel(DetalleDeOperacion detalle)
        {
            IdDetalle = detalle.Id;
            ConceptoDeNegocio producto = new ConceptoDeNegocio(detalle.DetalleTransaccion().Concepto_negocio);
            this.Producto = new ProductoParaVentaViewModel()
            {
                Id = detalle.Producto.Id,
                Nombre = detalle.Producto.Nombre,
                EsBien = detalle.Producto.EsBien
            };
            this.PrecioUnitario = detalle.PrecioUnitario;
            this.Cantidad = detalle.Cantidad;
            this.Importe = detalle.Importe;
            //this.VersionFila = producto.VersionFila().ToString();
            this.Igv = detalle.Igv;
            this.Descuento = detalle.Descuento;
            this.Lote = detalle.Lote;
            this.Observacion = "";
            this.Vencimiento = detalle.Vencimiento;
            this.Registro = detalle.Registro;
        }

        public static List<RegistroDetalleCompraViewModel> Convert(List<DetalleDeOperacion> detalles)
        {
            List<RegistroDetalleCompraViewModel> detalles_ = new List<RegistroDetalleCompraViewModel>();
            foreach (var item in detalles)
            {
                detalles_.Add(new RegistroDetalleCompraViewModel(item));
            }
            return detalles_;
        }

        public static List<RegistroDetalleVentaViewModel> Convert_(List<DetalleDeOperacion> detalles)
        {
            List<RegistroDetalleVentaViewModel> detalles_ = new List<RegistroDetalleVentaViewModel>();
            foreach (var item in detalles)
            {
                detalles_.Add(new RegistroDetalleVentaViewModel(item));
            }
            return detalles_;
        }

        public static DetalleDeOperacion Convert(RegistroDetalleVentaViewModel detalle)
        {
            return new DetalleDeOperacion(
                                        detalle.IdDetalle, 
                                        detalle.Producto.Id,
                                        detalle.Producto.Nombre,
                                        detalle.Cantidad,
                                        detalle.PrecioUnitario,
                                        detalle.Importe,
                                        0, 0,
                                        detalle.Descuento,  
                                        detalle.Observacion,
                                        detalle.Lote,
                                        detalle.Vencimiento,
                                        detalle.Registro,
                                        detalle.Producto.EsBien,
                                        detalle.MascaraDeCalculo,
                                        null);
        }

        public static List<DetalleDeOperacion> Convert(List<RegistroDetalleVentaViewModel> detalles)
        {
            List<DetalleDeOperacion> detalles_ = new List<DetalleDeOperacion>();
            foreach (var detalle in detalles)
            {
                detalles_.Add(Convert(detalle));
            }
            return detalles_;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroCompraViewModel
    {
        public long IdCompra { get; set; }
        public ComboActorComercialViewModel Proveedor { get; set; }
        public int TipoCompra { get; set; }//1 : No grabado //2: de grabado a grabado //3: de grabado a no grabado ,//4:  de grabado a ambas
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Observacion { get; set; }
        public IEnumerable<RegistroDetalleCompraViewModel> Detalles { get; set; }
        public bool EsCompraACredito { get; set; }
        public bool EsCreditoRapido { get; set; }
        public IEnumerable<RegistroDetalleFinanciamientoViewModel> Cuotas { get; set; }
        public decimal Inicial { get; set; }
        public decimal Flete { get; set; }
        public bool HayRegistroTrazaPago { get; set; }
        public bool HayRegistroMovimientoDeAlmacen { get; set; }
        public bool UsaComprobanteOrden { get; set; }

        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public ComboGenericoViewModel Comprador { get; set; }

        public RegistroCompraViewModel()
        {
        }

        public RegistroCompraViewModel(OrdenDeCompra ordenDeCompra, List<SelectorTipoDeComprobante> selectorComprobante)
        {
            this.IdCompra = ordenDeCompra.IdCompra;
            this.Proveedor = new ComboActorComercialViewModel(ordenDeCompra.Proveedor().Id, ordenDeCompra.Proveedor().RazonSocial, ordenDeCompra.Proveedor().DocumentoIdentidad);
            this.TipoCompra = Convert.ToInt32(ordenDeCompra.TipoDeOperacionDeCompra());
            this.TipoDeComprobante = selectorComprobante.SingleOrDefault(sc => sc.TipoComprobante.Id == ordenDeCompra.Comprobante().IdTipo);
            if (ordenDeCompra.Comprobante().IdSerie == null)
            {
                TipoDeComprobante.SerieIngresada = ordenDeCompra.Comprobante().NumeroDeSerie;
                TipoDeComprobante.NumeroIngresado = ordenDeCompra.Comprobante().NumeroDeComprobante;
            }
            else
            {
                if (TipoDeComprobante.Series.Count() > 1) { TipoDeComprobante.SerieSeleccionada = (int)ordenDeCompra.Comprobante().IdSerie; }
                else { TipoDeComprobante.SerieSeleccionada = 0; }
            }
            this.TipoDeComprobante.IdComprobante = ordenDeCompra.Comprobante().Id;
            this.FechaRegistro = ordenDeCompra.FechaEmision;
            this.Observacion = ordenDeCompra.Comentario;
            this.EsCompraACredito = Convert.ToInt32(ordenDeCompra.ModoDePago()) == 1 ? false : true;
            if (EsCompraACredito) { this.EsCreditoRapido = Convert.ToInt32(ordenDeCompra.ModoDePago()) == 3 ? false : true; }
            this.Cuotas = RegistroDetalleFinanciamientoViewModel.Convert_(ordenDeCompra.Cuotas());
            this.Inicial = ordenDeCompra.Cuotas().SingleOrDefault(c => c.cuota_inicial == true).saldo;
            this.Flete = ordenDeCompra.Flete;
            this.Detalles = RegistroDetalleCompraViewModel.Convert_(ordenDeCompra.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
        }
    }

    public class RegistroDetalleCompraViewModel
    {
        public long IdDetalle { get; set; }
        public RegsitroProductoParaFacturacionViewModels Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public string VersionFila { get; set; }
        public bool Igv { get; set; }
        public decimal Descuento { get; set; }
        public string Observacion { get; set; }
        public string Lote { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string Registro { get; set; }

        public RegistroDetalleCompraViewModel()
        {
        }

        public RegistroDetalleCompraViewModel(DetalleDeOperacion detalle)
        {
            IdDetalle = detalle.Id;
            ConceptoDeNegocio producto = new ConceptoDeNegocio(detalle.DetalleTransaccion().Concepto_negocio);
            this.Producto = new RegsitroProductoParaFacturacionViewModels(detalle.Producto.Id, detalle.Producto.Nombre, detalle.PrecioUnitario, producto.VersionFila(), producto.Stock());
            this.PrecioUnitario = detalle.PrecioUnitario;
            this.Cantidad = detalle.Cantidad;
            this.Importe = detalle.Importe;
            this.VersionFila = producto.VersionFila().ToString();
            this.Igv = 0 < detalle.Igv;
            this.Descuento = detalle.Descuento;
            this.Lote = detalle.Lote;
            this.Observacion = "";
            this.Vencimiento = detalle.Vencimiento;
            this.Registro = detalle.Registro;
        }

        public static List<RegistroDetalleCompraViewModel> Convert_(List<DetalleDeOperacion> detalles)
        {
            List<RegistroDetalleCompraViewModel> nuevosDetalles = new List<RegistroDetalleCompraViewModel>();
            foreach (var item in detalles)
            {
                nuevosDetalles.Add(new RegistroDetalleCompraViewModel(item));
            }
            return nuevosDetalles;
        }

        public static DetalleDeOperacion Convert(RegistroDetalleCompraViewModel detalle)
        {
            return new DetalleDeOperacion(
                                        detalle.IdDetalle,
                                        detalle.Producto.Id,
                                        detalle.Producto.Nombre,
                                        detalle.Cantidad,
                                        detalle.PrecioUnitario,
                                        detalle.Importe,
                                        0, 0,
                                        detalle.Descuento, detalle.Observacion,
                                        detalle.Lote,
                                        detalle.Vencimiento,
                                        detalle.Registro, 
                                        detalle.Producto.EsBien,
                                        null,
                                        detalle.Producto.ConceptoBasico == null ? null: CaracteristicaPropiaViewModel.Convert(detalle.Producto.ConceptoBasico.CaracteristicasPropias));
        }

        public static List<DetalleDeOperacion> Convert(List<RegistroDetalleCompraViewModel> detalles)
        {
            List<DetalleDeOperacion> detalles_ = new List<DetalleDeOperacion>();
            foreach (var detalle in detalles)
            {
                detalles_.Add(Convert(detalle));
            }
            return detalles_;
        }
    }

        public class TrazaDePagoViewModel
        {
            public ComboGenericoViewModel MedioDePago { get; set; }
            public ComboGenericoViewModel EntidadFinanciera { get; set; }
            public ComboGenericoViewModel CuentaBancaria { get; set; }
            public ComboGenericoViewModel OperadorTarjeta { get; set; }
            public string InformacionDePago { get; set; }
            public ComboGenericoViewModel CentroDeAtencion { get; set; }
            public ComboGenericoViewModel Cajero { get; set; }

            public TrazaDePagoViewModel()
            {
            }
        }
    }
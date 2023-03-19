using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroMovimientoDeAlmacenViewModel
    {
        public long IdOrdenDeAlmacen { get; set; }
        public long IdOperacion { get; set; }
        public DateTime FechaInicioTraslado { get; set; }
        public RegistroTransporteViewModel Transportista { get; set; }
        public RegistroConductorViewModel Conductor { get; set; }
        public ComboGenericoViewModel ModalidadTransporte { get; set; }
        public ComboGenericoViewModel MotivoTraslado { get; set; }
        public string DescripcionMotivo { get; set; }
        public decimal PesoBrutoTotal { get; set; }
        public int NumeroBultos { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<DetalleMovimientoDeAlmacenViewModel> Detalles { get; set; }
        public string Observacion { get; set; }
        public bool EsTrasladoTotal { get; set; }
        public ComboGenericoViewModel UbigeoOrigen { get; set; }
        public string DireccionOrigen { get; set; }
        public ComboGenericoViewModel UbigeoDestino { get; set; }
        public string DireccionDestino { get; set; }
        public ActorComercial_ Tercero { get; set; }
        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public ComboGenericoViewModel Almacenero { get; set; }
        public string DocumentoReferencia { get; set; }

        public RegistroMovimientoDeAlmacenViewModel()
        {
            this.TipoDeComprobante = new SelectorTipoDeComprobante();
            this.Transportista = new RegistroTransporteViewModel();
            this.Conductor = new RegistroConductorViewModel();
        }

    }

    public class DetalleMovimientoDeAlmacenViewModel
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal StockActual { get; set; }
        public decimal Ordenado { get; set; }
        public decimal RecibidoEntregado { get; set; }
        public decimal IngresoSalidaActual { get; set; }
        public string Lote { get; set; }
        public bool EsBien { get; set; }

        public DetalleMovimientoDeAlmacenViewModel()
        {
        }

        public DetalleMovimientoDeAlmacenViewModel(DetalleDeOperacion detalle, string lote, decimal cantidadEntregadaRecibida)
        {
            IdProducto = detalle.Producto.Id;
            Descripcion = detalle.Producto.Nombre;
            Ordenado = detalle.Cantidad;
            RecibidoEntregado = cantidadEntregadaRecibida;
            IngresoSalidaActual = Ordenado - RecibidoEntregado;
            Lote = lote;
            EsBien = detalle.Producto.EsBien;
        }

        public static List<DetalleMovimientoDeAlmacenViewModel> Convertir(List<DetalleDeOperacion> detalles, List<MovimientoDeAlmacen> ordenes)
        {
            List<DetalleMovimientoDeAlmacenViewModel> detallesTrasladoDeMercaderia = new List<DetalleMovimientoDeAlmacenViewModel>();
            foreach (var detalle in detalles)
            {
                var idConcepto = detalle.Producto.Id;
                decimal cantidadEntregadaRecibida = 0;
                if (ordenes != null)
                {
                    foreach (var orden in ordenes)
                    {
                        cantidadEntregadaRecibida += orden.DetalleTransaccion().Single(dt => dt.id_concepto_negocio == idConcepto).cantidad;
                    }
                }
                detallesTrasladoDeMercaderia.Add(new DetalleMovimientoDeAlmacenViewModel(detalle, detalle.Lote, cantidadEntregadaRecibida));
            }
            return detallesTrasladoDeMercaderia;
        }
    }

    public class RegistroTransporteViewModel
    {
        public ComboGenericoViewModel Transportista { get; set; }
        public string Placa { get; set; }

        public RegistroTransporteViewModel()
        {
        }
    }
    public class RegistroConductorViewModel
    {
        public ComboGenericoViewModel Conductor { get; set; }
        public string NumeroLicencia { get; set; }

        public RegistroConductorViewModel()
        {
        }
    }
}












//    int[] idsTransaccionesIngresoMercaderia = { TransaccionSettings.Default.IdTipoTransaccionIngresaBienOservicio, TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra,
//            TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno, TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta,
//            TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta, TransaccionSettings.Default.IdTipoTransaccionIngresoMercadInvalidacionDeAnulacionCompra };

//int[] idsTransaccionesSalidaMercaderia = { TransaccionSettings.Default.IdTipoTransaccionSaleBienOservicio, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta,
//            TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno, TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra,
//            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra, TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta };

//List<DetalleOrdenDeVentaCompraGasto> detalles = orden.Desplazamiento().transaccion.Transaccion1.Where(t => t.Tipo_transaccion.id == 2).Select(t =>t.Detalle_transaccion).ToList();

//List<DetalleOrdenVentaCompraGastoAlmacenViewModel> detallesOrden = DetalleOrdenVentaCompraGastoAlmacenViewModel.convert(DetalleOrdenDeVentaCompraGasto.Convert_());
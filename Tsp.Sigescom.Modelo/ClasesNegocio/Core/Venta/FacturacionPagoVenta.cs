using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class DatosVentaIntegrada
    {
        public long Id { get; set; }
        public DatosOrdenVenta Orden { get; set; }
        public DatosPago Pago { get; set; }
        public DatosMovimientoDeAlmacen MovimientoAlmacen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Transaccion TransaccionOrigen { get; set; }
        public List<Transaccion> TransaccionesModificar { get; set; }
        public Transaccion TransaccionCreacion { get; set; }
        public Actor_negocio ActorReferencia { get; set; }
        public Estado_transaccion NuevoEstado { get; set; }
        public bool EsVentaModoCaja { get; set; }
        public DatosVentaIntegrada() { }
        public DatosVentaIntegrada(RegistroDeVenta registroDeVenta)
        {
            Orden = new DatosOrdenVenta(registroDeVenta);
            Pago = new DatosPago(registroDeVenta);
            MovimientoAlmacen = new DatosMovimientoDeAlmacen(registroDeVenta);
            FechaRegistro = registroDeVenta.FechaRegistro;
        }

        public static DatosVentaIntegrada Convert(Transaccion ordenDeVenta)
        {

            DatosVentaIntegrada datosVenta = new DatosVentaIntegrada()
            {
                Orden = new DatosOrdenVenta(new OrdenDeVenta(ordenDeVenta)),
                Pago = null,
                MovimientoAlmacen = null,
                FechaRegistro = ordenDeVenta.fecha_registro_sistema,
                EsVentaModoCaja = false
            };
            return datosVenta;
        }
    }
}

public partial class DatosOrdenVenta
{
    public long Id { get; set; }
    public ItemGenerico PuntoDeVenta { get; set; }
    public ItemGenerico Vendedor { get; set; }
    public ActorComercial_ Cliente { get; set; }
    public ComprobanteDeNegocio_ Comprobante { get; set; }
    public List<DetalleDeOperacion> Detalles { get; set; }
    public bool AplicarIGVCuandoEsAmazonia { get; set; }
    public bool UnificarDetalles { get; set; }
    public string ValorDetalleUnificado { get; set; }
    public string Observacion { get; set; }
    public decimal Total { get; set; }
    public decimal Flete { get; set; }
    public int NumeroBolsasDePlastico { get; set; }
    public decimal Icbper { get; set; }
    public string Placa { get; set; }
    public string Informacion { get; set; }
    public bool EsVentaPasada { get; set; }
    public DateTime FechaEmision { get; set; }
    public int IdEstado { get; set; }
    public int IdTransaccionPadre { get; set; }
    public int IdTipoComprobanteaEmitir { get; set; }
    public bool EsOperacionPreGenerada { get; set; }
    public long IdOperacionPreGenerada { get; set; }
    public DatosOrdenVenta() { }
    public DatosOrdenVenta(OrdenDeVenta orden)
    {

        PuntoDeVenta = new ItemGenerico(orden.IdPuntoDeVenta, "");
        Vendedor = new ItemGenerico(orden.IdVendedor, "");
        Cliente = new ActorComercial_(orden.Cliente().ActorDeNegocio);
        Comprobante = new ComprobanteDeNegocio_()
        {
            Tipo = new ItemGenerico(orden.Comprobante().IdTipo, orden.Comprobante().Tipo().Nombre),
            Serie = new SerieComprobante_((int)orden.Comprobante().IdSerie, orden.Comprobante().NombreTipo, orden.Comprobante().Tipo().EsPropio),
            Numero = orden.Comprobante().NumeroDeComprobante
        };
        Detalles = orden.Detalles();
        AplicarIGVCuandoEsAmazonia = orden.AplicaIGVCuandoAplicaLeyAmazonia;
        UnificarDetalles = orden.TieneLosDetallesUnificados();
        Observacion = orden.Observacion();
        Flete = orden.Flete;
        EsVentaPasada = orden.EsVentaRegistradaConFechaPasada;
        FechaEmision = orden.FechaEmision;
        NumeroBolsasDePlastico = orden.NumeroBolsasDePlastico();
        Icbper = orden.Icbper();
        Placa = VentasSettings.Default.PermitirRegistroDePlacaEnVenta ? orden.Detalles().First().Registro : null;
    }

    public DatosOrdenVenta(RegistroDeVenta registroDeVenta)
    {
        PuntoDeVenta = registroDeVenta.PuntoDeVenta;
        Vendedor = registroDeVenta.Vendedor;
        Cliente = registroDeVenta.Cliente;
        if (!String.IsNullOrEmpty(registroDeVenta.Alias)) Cliente.Alias = registroDeVenta.Alias;
        Comprobante = new ComprobanteDeNegocio_()
        {
            Tipo = new ItemGenerico(registroDeVenta.TipoDeComprobante.TipoComprobante.Id, registroDeVenta.TipoDeComprobante.TipoComprobante.Nombre),
            Serie = new SerieComprobante_(registroDeVenta.TipoDeComprobante.SerieSeleccionada, registroDeVenta.TipoDeComprobante.SerieIngresada, registroDeVenta.TipoDeComprobante.EsPropio),
            Numero = registroDeVenta.TipoDeComprobante.NumeroIngresado
        };
        Detalles = registroDeVenta.Detalles.ToList();
        AplicarIGVCuandoEsAmazonia = registroDeVenta.GrabaIgv;
        UnificarDetalles = registroDeVenta.DetalleUnificado;
        Observacion = registroDeVenta.Observacion;
        Flete = registroDeVenta.Flete;
        EsVentaPasada = registroDeVenta.EsVentaPasada;
        FechaEmision = registroDeVenta.FechaEmision;
        NumeroBolsasDePlastico = registroDeVenta.NumeroBolsasDePlastico;
        Icbper = registroDeVenta.Icbper;
        Placa = registroDeVenta.Placa;
    }
    public bool VentaGravaIgv
    {
        get
        {
            //La primera condicion del if es para donde no aplica la ley de la amazonia y la segunda condicion es para la parte de la amazonia las dos solo se graba el igv cuando el documento sea difernte a la nota interna de venta
            return (!TransaccionSettings.Default.AplicaLeyAmazonia && Comprobante.Tipo.Id != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna) || (TransaccionSettings.Default.AplicaLeyAmazonia && Comprobante.Tipo.Id != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna && AplicarIGVCuandoEsAmazonia);
        }
    }
    public decimal ImporteTotal
    {
        get
        {
            return Detalles != null ? Detalles.Sum(d => d.Importe) + Icbper : 0;
        }
    }
    public List<Detalle_transaccion> DetallesDeVenta()
    {
        return Detalles.Select(d => d.DetalleTransaccion()).ToList();
    }
    public bool HayBienesEnLosDetalles()
    {
        return Detalles.Count(d => d.Producto.EsBien) > 0;
    }
    public List<Detalle_transaccion> DetallesDeVentaQueSonBienes()
    {
        return Detalles.Where(d => d.Producto.EsBien).Select(dt => dt.DetalleTransaccion()).ToList();
    }
    public decimal DescuentoGlobal { get; set; }
    public decimal DescuentoPorItem
    {
        get
        {
            return Detalles != null ? Detalles.Sum(d => d.Descuento) : 0;
        }
    }
    public decimal Anticipo
    {
        get
        {
            return 0;
        }
    }
    public decimal Gravada
    {
        get
        {
            return Detalles != null ? VentaGravaIgv ? Detalles.Sum(d => d.Importe - d.Igv) : 0 : 0;
        }
    }
    public decimal Exonerada
    {
        get
        {
            return Detalles != null ? VentaGravaIgv ? 0 : Detalles.Sum(d => d.Importe) : 0;
        }
    }
    public decimal Inafecta
    {
        get
        {
            return 0;
        }
    }
    public decimal Gratuita
    {
        get
        {
            return 0;
        }
    }
    public decimal Igv
    {
        get
        {
            return Detalles != null ? Detalles.Sum(d => d.Igv) : 0;
        }
    }
    public decimal Isc
    {
        get
        {
            return Detalles != null ? Detalles.Sum(d => d.Isc) : 0;
        }
    }
    public decimal OtrosCargos
    {
        get
        {
            return 0;
        }
    }
    public decimal OtrosTributos
    {
        get
        {
            return 0;
        }
    }
}

public partial class DatosPago
{
    public ItemGenerico Caja { get; set; }
    public ItemGenerico Cajero { get; set; }
    public TrazaDePago_ Traza { get; set; }
    public ModoPago ModoDePago { get; set; }
    public decimal Inicial { get; set; }
    public List<RegistroDetalleDeFinanciamiento> Cuotas { get; set; }
    public DatosPago() { }
    public DatosPago(RegistroDeVenta registroDeVenta)
    {
        Caja = registroDeVenta.Caja;
        Cajero = registroDeVenta.Cajero;
        Traza = registroDeVenta.TrazaDePago;
        ModoDePago = registroDeVenta.ModoDePago; //registroDeVenta.EsVentaACredito ? registroDeVenta.EsCreditoRapido ? ModoPago.CreditoRapido : ModoPago.CreditoConfigurado : ModoPago.Contado;
        Inicial = registroDeVenta.Inicial;
        Cuotas = registroDeVenta.Cuotas?.ToList();
    }
    public List<Cuota> CuotasDeVenta()
    {
        return ModoDePago == ModoPago.CreditoConfigurado ? RegistroDetalleDeFinanciamiento.Convert_(Cuotas.ToList()) : null;
    }
    public bool HayIngresoDinero
    {
        get
        {
            return (ModoDePago == ModoPago.Contado || Inicial > 0);
        }
    }
}

public partial class DatosMovimientoDeAlmacen
{
    public ItemGenerico Almacen { get; set; }
    public ItemGenerico Almacenero { get; set; }
    public bool EntregaDiferida { get; set; }
    public bool HayComprobanteDeSalidaDeMercaderia { get; set; }
    public IEnumerable<RegistroMovimientoAlmacen> RegistroDeMovimientosDeAlmacen { get; set; }
    public DatosMovimientoDeAlmacen() { }
    public DatosMovimientoDeAlmacen(RegistroDeVenta registroDeVenta)
    {
        Almacen = registroDeVenta.Almacen;
        Almacenero = registroDeVenta.Almacenero;
        HayComprobanteDeSalidaDeMercaderia = registroDeVenta.HaySalidaDeMercaderia;
        RegistroDeMovimientosDeAlmacen = registroDeVenta.SalidasDeMercaderia;

    }
    public List<MovimientoDeAlmacen> ComprobantesDeSalidasDeMercaderia
    {
        get
        {
            return HayComprobanteDeSalidaDeMercaderia ? RegistroMovimientoAlmacen.Convert_(RegistroDeMovimientosDeAlmacen.ToList()) : null;
        }
    }
}


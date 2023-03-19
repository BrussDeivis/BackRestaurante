using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class RegistroDeVenta
    {
        public long IdVenta { get; set; }
        public bool GrabaIgv { get; set; }//
        public bool DetalleUnificado { get; set; }//
        public bool EsVentaPasada { get; set; }//
        public int ModoPago { get; set; }//
        public bool HayRegistroTrazaPago { get; set; }
        public bool HayRegistroMovimientoMercaderia { get; set; }
        public bool UsaComprobanteOrden { get; set; }
        public string Alias { get; set; }//
        public string Observacion { get; set; }//
        public decimal Inicial { get; set; }//
        public decimal Flete { get; set; }//
        public string Placa { get; set; }//
        public DateTime FechaEmision { get; set; }//
        public ActorComercial_ Cliente { get; set; }//
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }//
        public IEnumerable<RegistroDetalleDeFinanciamiento> Cuotas { get; set; }//
        public IEnumerable<DetalleDeOperacion> Detalles { get; set; }//

        //public int IdMedioDePago { get; set; }
        //public string InformacionDeMedioPago { get; set; }
        //public ItemGenerico EntidadFinanciera { get; set; }
        //public ItemGenerico TipoTarjeta { get; set; }
        public TrazaDePago_ TrazaDePago { get; set; }//
        public DatosOrdenVenta Facturacion { get; set; }

        public bool HaySalidaDeMercaderia { get; set; }//
        public IEnumerable<RegistroMovimientoAlmacen> SalidasDeMercaderia { get; set; }//
        public int NumeroBolsasDePlastico { get; set; }//
        public decimal Icbper { get; set; }//

        public ItemGenerico PuntoDeVenta { get; set; }//
        public ItemGenerico Almacen { get; set; }//
        public ItemGenerico Caja { get; set; }//
        public ItemGenerico Vendedor { get; set; }//
        public ItemGenerico Cajero { get; set; }//
        public ItemGenerico Almacenero { get; set; }//

        public RegistroDeVenta()
        {
        }

        //public RegistroDeVenta(OrdenDeVenta ordenDeVenta, List<SelectorTipoDeComprobante> selectorComprobante)
        //{
        //    this.IdVenta = ordenDeVenta.IdVenta;
        //    this.Cliente = new ComboActorComercial(ordenDeVenta.Cliente().Id, ordenDeVenta.Cliente().RazonSocial, ordenDeVenta.Cliente().DocumentoIdentidad);
        //    this.Alias = "";
        //    this.TipoDeComprobante = selectorComprobante.SingleOrDefault(sc => sc.TipoComprobante.Id == ordenDeVenta.Comprobante().IdTipo);
        //    if (ordenDeVenta.Comprobante().IdSerie == null)
        //    {
        //        TipoDeComprobante.SerieIngresada = ordenDeVenta.Comprobante().NumeroDeSerie;
        //        TipoDeComprobante.NumeroIngresado = ordenDeVenta.Comprobante().NumeroDeComprobante;
        //    }
        //    else
        //    {
        //        if (TipoDeComprobante.Series.Count() > 1) { TipoDeComprobante.SerieSeleccionada = (int)ordenDeVenta.Comprobante().IdSerie; }
        //        else { TipoDeComprobante.SerieSeleccionada = 0; }
        //    }
        //    //this.TipoDeComprobante.IdComprobante = ordenDeVenta.Comprobante().Id;
        //    this.FechaEmision = ordenDeVenta.FechaEmision;
        //    this.Observacion = ordenDeVenta.Comentario;
        //    this.GrabaIgv = (ordenDeVenta.Igv() > 0) ? true : false;
        //    this.DetalleUnificado = true;//ordenDeVenta.DetalleUnificado().Equals("1") ? true : false;
        //    this.EsVentaACredito = Convert.ToInt32(ordenDeVenta.ModoDePago()) == 1 ? false : true;
        //    if (EsVentaACredito) { this.EsCreditoRapido = Convert.ToInt32(ordenDeVenta.ModoDePago()) == 3 ? false : true; }
        //    this.Cuotas = RegistroDetalleDeFinanciamiento.Convert_(ordenDeVenta.Cuotas());
        //    this.Inicial = ordenDeVenta.Cuotas().SingleOrDefault(c => c.cuota_inicial == true).Saldo();
        //    this.Flete = ordenDeVenta.Flete;
        //    this.Detalles = null;// RegistroDetalleCompraViewModel.Convert_(ordenDeVenta.Detalles().Where(d => d.DetalleTransaccion().id_concepto_negocio != ConceptoSettings.Default.IdConceptoNegocioFlete).ToList());
        //}
        public RegistroDeVenta(VentaMasiva ventaMasiva, List<VentaMonoDetalle> ventasMonoDetalle)
        {
            Vendedor = new ItemGenerico(ventaMasiva.IdVendedor);
            PuntoDeVenta = new ItemGenerico(ventaMasiva.IdPuntoDeVenta);
            Cajero = new ItemGenerico(ventaMasiva.IdCajero);
            Caja = new ItemGenerico(ventaMasiva.IdCaja);
            Almacenero = new ItemGenerico(ventaMasiva.IdAlmacenero);
            Almacen = new ItemGenerico(ventaMasiva.IdAlmacen);
            Cliente = new ActorComercial_() { Id = ventasMonoDetalle.First().IdCliente };
            Alias = "";
            TipoDeComprobante = new SelectorTipoDeComprobante()
            {
                EsPropio = true,
                TipoComprobante = new ItemGenerico(ventaMasiva.IdTipoDeComprobante),
                SerieSeleccionada = ventaMasiva.IdSerieDeComprobante
            };
            EsVentaPasada = true;
            FechaEmision = ventaMasiva.FechaEmision;
            GrabaIgv = !TransaccionSettings.Default.AplicaLeyAmazonia;
            DetalleUnificado = false;
            Observacion = "";
            Cuotas = null;
            Inicial = 0;
            Flete = 0;
            NumeroBolsasDePlastico = 0;
            Icbper = 0;
            TrazaDePago = new TrazaDePago_();
            HayRegistroTrazaPago = false;
            HayRegistroMovimientoMercaderia = false;
            HaySalidaDeMercaderia = false;
            SalidasDeMercaderia = null;
            UsaComprobanteOrden = false;
            Detalles = DetalleDeOperacion.Convert(ventasMonoDetalle);
        }
        public static RegistroDeVenta ConvertVentaCobroPorVendedor(VentaMasiva ventaMasiva, List<VentaMonoDetalle> ventasMonoDetalle)
        {
            return new RegistroDeVenta(ventaMasiva, ventasMonoDetalle)
            {
                ModoPago = (int)Entidades.ModoPago.CreditoRapido,
                TrazaDePago = new TrazaDePago_()
            };
        }

        public static RegistroDeVenta ConvertVentaMasiva(VentaMasiva ventaMasiva, VentaMonoDetalle ventaMonoDetalle)
        {
            return new RegistroDeVenta(ventaMasiva, new List<VentaMonoDetalle> { ventaMonoDetalle })
            {
                ModoPago = (int)Entidades.ModoPago.Contado,
                TrazaDePago = new TrazaDePago_()
                {
                    MedioDePago = new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo)
                }
            };
        }

        //Propertys generados en base a los datos del registro de venta
        public List<DetalleDeOperacion> DetallesDeOperacion { get; set; }
        public DateTime FechaRegistro { get; set; }

        public ModoPago ModoDePago
        {
            get
            {
                return (Entidades.ModoPago)ModoPago;
            }
        }

        public bool HayIngresoDinero
        {
            get
            {
                return (ModoDePago == Entidades.ModoPago.Contado || Inicial > 0);
            }
        }

        public List<Cuota> CuotasDeVenta
        {
            get
            {
                return ModoDePago == Entidades.ModoPago.CreditoConfigurado ? RegistroDetalleDeFinanciamiento.Convert_(Cuotas.ToList()) : null;
            }
        }

        public List<MovimientoDeAlmacen> ComprobantesDeSalidaDeMercaderia
        {
            get
            {
                return HaySalidaDeMercaderia ? RegistroMovimientoAlmacen.Convert_(SalidasDeMercaderia.ToList()) : null;
            }
        }

        public bool VentaGravaIgv
        {
            get
            {
                //La primera condicion del if es para donde no aplica la ley de la amazonia y la segunda condicion es para la parte de la amazonia las dos solo se graba el igv cuando el documento sea difernte a la nota interna de venta
                return (!TransaccionSettings.Default.AplicaLeyAmazonia && TipoDeComprobante.TipoComprobante.Id != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna) || (TransaccionSettings.Default.AplicaLeyAmazonia && TipoDeComprobante.TipoComprobante.Id != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna && GrabaIgv);
            }
        }

        public decimal ImporteTotal
        {
            get
            {
                return DetallesDeOperacion != null ? DetallesDeOperacion.Sum(d => d.Importe) + Icbper : 0;
            }
        }

        public List<Detalle_transaccion> DetallesDeVenta
        {
            get
            {
                return DetallesDeOperacion.Select(d => d.DetalleTransaccion()).ToList();
            }
        }

        public List<Detalle_transaccion> DetallesDeVentaQueSonBienes
        {
            get
            {
                return DetallesDeOperacion.Where(d => d.Producto.EsBien).Select(dt => dt.DetalleTransaccion()).ToList();
            }
        }

    }
    public class OperacionModificatoria : OperacionIntegrada
    {
        /// <summary>
        /// estados de transacciones que debe crearse para la orden modificada
        /// </summary>
        public List<Estado_transaccion> NuevosEstadosTransaccionesModificadas { get; set; }
        /// <summary>
        /// estados de cuotas que deben crearse para la orden modificada, por ejemplo, al confirmarse o invalidarse
        /// </summary>
        public List<Estado_cuota> NuevosEstadosParaCuotasTransaccionesModificadas { get; set; }
        public List<Cuota> CuotasModificadas { get; set; }
        public List<Detalle_transaccion> DetallesTransaccionModificados { get; set; }
    }

    public class OperacionSoloOrden
    {
        public Transaccion Operacion { get; set; }
        public Transaccion OrdenDeOperacion { get; set; }
        public Transaccion OperacionOrigen { get; set; }

        public void EnlazarTransacciones()
        {
            if (OrdenDeOperacion != null)
            {
                OrdenDeOperacion.Transaccion2 = Operacion;//la transaccion wrapper de la ordem
                Operacion.Transaccion1.Add(OrdenDeOperacion);//Operacion viene a ser la transaccion padre de orden de operacion
            }

            if (OperacionOrigen != null)
            {
                OrdenDeOperacion.Transaccion3 = OperacionOrigen;//la operacion de referencia de la orden viene a ser la operacion origen, por ejemplo la atención en un restaurante, o el movimiento de cochera en un negocio de parking, etc.
            }
        }

        public void AsignarComprobante(Comprobante comprobante)
        {
            if (Operacion != null) Operacion.Comprobante = Operacion.Comprobante ?? comprobante;
            if (OrdenDeOperacion != null) OrdenDeOperacion.Comprobante = OrdenDeOperacion.Comprobante ?? comprobante;
        }

        public OperacionSoloOrden(Transaccion operacion, Transaccion ordenDeOperacion, Transaccion operacionOrigen)
        {
            Operacion = operacion;
            OrdenDeOperacion = ordenDeOperacion;
            OperacionOrigen = operacionOrigen;
            this.EnlazarTransacciones();
        }
    }
    public class OperacionIntegrada
    {
        public long Id { get; set; }
        public Transaccion Operacion { get; set; }
        public Transaccion OrdenDeOperacion { get; set; }
        public Transaccion MovimientoEconomico { get; set; }
        public List<Transaccion> MovimientosBienes { get; set; }

        /// <summary>
        /// Transaccion origen que da lugar a la Venta Por ejemplo: un movimiento de cochera, un servicio técnico, etc.
        /// Se usa cuando existen modulos a medida de algun dominio de negocio. 
        /// Esta transacción tendra como transaccion de referencia a la Orden de la venta.
        /// </summary>
        public Transaccion OperacionOrigen { get; set; }
        /// <summary>
        /// Transacciones que fueron modificadas solo a nivel de la entidad transaccion no de sus objetos relacionados
        /// </summary>
        public List<Transaccion> TransaccionesModificadas { get; set; }
        /// <summary>
        /// Es una transaccion que se debe de agregar, como nueva en base de datos, ejemplo en hotel al momento de confirmar una reserva y me pide facturar se crea el comprobante y se tiene que agregar la operacion de creacion que sera la reserva y que se tiene que agregfar como nueva transaccion
        /// </summary>
        public Transaccion OperacionCreacion { get; set; }
        public List<Actor_negocio> ActoresNegocioModificados { get; set; }
        public List<Estado_transaccion> NuevosEstadosTransaccion { get; set; }


        public OperacionIntegrada()
        {
        }

        //public List<Detalle_transaccion> DetallesDeSalidas
        //{
        //    get
        //    {
        //        return MovimientoBienes.SelectMany(sm => sm.Detalle_transaccion).ToList();
        //    }
        //}

        /// <summary>
        /// Armar la venta tal como se guardara con Orden de venta, cobro y salidas de mercaderia como transacciones hijas de venta y las saldas de mercaderia referenciando a la Orden de venta
        /// </summary>
        /// <returns></returns>
        public void EnlazarTransacciones()
        {
            if (OrdenDeOperacion != null)
            {
                OrdenDeOperacion.Transaccion2 = Operacion;//la transaccion wrapper de la ordem
                Operacion.Transaccion1.Add(OrdenDeOperacion);//Operacion viene a ser la transaccion padre de orden de operacion
            }
            if (MovimientoEconomico != null)
            {
                MovimientoEconomico.Transaccion2 = Operacion; //la transaccion wrapper del pago
                Operacion.Transaccion1.Add(MovimientoEconomico);//Operacion viene a ser la transaccion padre de movimiento economico
            }
            foreach (var salidaDeMercaderia in MovimientosBienes)
            {
                salidaDeMercaderia.Transaccion2 = Operacion;
                salidaDeMercaderia.Transaccion3 = OrdenDeOperacion;//la orden será la transaccion de referencia para la salida de mercaderia
                Operacion.Transaccion1.Add(salidaDeMercaderia);//Operacion viene a ser la transaccion padre de salida de mercaderia
            }
            if (OperacionOrigen != null)
            {
                OrdenDeOperacion.Transaccion3 = OperacionOrigen;//la operacion de referencia de la orden viene a ser la operacion origen, por ejemplo la atención en un restaurante, o el movimiento de cochera en un negocio de parking, etc.
            }
            if (OperacionCreacion != null)
            {
                OrdenDeOperacion.Transaccion3 = OperacionCreacion;//La operacion de referencia de la orden viene a ser la operacion de creacion cuando esta no es una operacion ya creada, sino se crea al momento de crear la operacion se crea su referencia, por ejemplo en registrar reserva y esta se factura
            }
        }
        /// <summary>
        /// Se asigna el comprobante que viene por parametro a la operacion, orden de operacion, movimiento economico, Salida de mrcaderia siempre en cuando no tengan un comprobante
        /// Hay caso de algunas operaciones ya tienen su comprobante, como ejemplo: las salidas de mercaderia pueden tener una guia de remision de comprobante
        /// </summary>
        /// <param name="comprobante"></param>
        public void AsignarComprobante(Comprobante comprobante)
        {
            if (Operacion != null) Operacion.Comprobante = Operacion.Comprobante ?? comprobante;
            if (OrdenDeOperacion != null) OrdenDeOperacion.Comprobante = OrdenDeOperacion.Comprobante ?? comprobante;
            if (MovimientoEconomico != null) MovimientoEconomico.Comprobante = MovimientoEconomico.Comprobante ?? comprobante;
            foreach (var salidaDeMercaderia in MovimientosBienes)
            {
                salidaDeMercaderia.Comprobante = salidaDeMercaderia.Comprobante ?? comprobante;
            }
        }

        /// <summary>
        /// Se reemplaza el comprobante que viene por parametro a la operacion, orden de operacion, movimiento economico, salida de mrcaderia
        /// </summary>
        /// <param name="comprobante"></param>
        public void ReemplazarComprobante(Comprobante comprobante)
        {
            if (Operacion != null) Operacion.Comprobante = comprobante;
            if (OrdenDeOperacion != null) OrdenDeOperacion.Comprobante = comprobante;
            if (MovimientoEconomico != null) MovimientoEconomico.Comprobante = comprobante;
            foreach (var salidaDeMercaderia in MovimientosBienes)
            {
                salidaDeMercaderia.Comprobante = comprobante;
            }
        }


        public OperacionIntegrada(Transaccion operacion, Transaccion ordenDeOperacion, Transaccion cobro, List<Transaccion> salidasDeMercaderia, Transaccion operacionOrigen, Transaccion operacionCreacion)
        {
            Operacion = operacion;
            OrdenDeOperacion = ordenDeOperacion;
            MovimientoEconomico = cobro;
            MovimientosBienes = salidasDeMercaderia;
            OperacionOrigen = operacionOrigen;
            OperacionCreacion = operacionCreacion;
        }
    }
    public class OperacionIntegradaSerie
    {
        public OperacionIntegradaSerie(OperacionIntegrada operacionIntegrada, int idSerieComprobante)
        {
            OperacionIntegrada = operacionIntegrada;
            IdSerieComprobante = idSerieComprobante;
        }

        public OperacionIntegrada OperacionIntegrada { get; set; }
        public int IdSerieComprobante { get; set; }

    }

    public class OperacionCorporativa : OperacionModificatoria
    {
        public Transaccion OrdenDeAlmacen { get; set; }
    }

    public class OperacionVentaCobroCarteraCliente
    {
        public Transaccion OperacionWrapper { get; set; }
        /// <summary>
        /// Cada venta contiene el wrapper de la venta, la orden de venta y los movimientos de bienes
        /// </summary>
        public List<OperacionIntegrada> Ventas { get; set; }
        /// <summary>
        /// Cobros realizados a un cliente de la caartera. En este caso un cobro no corresponde a una ventaa especifica, por eso se maneja fuera de las operaciones integradas
        /// </summary>
        public List<Transaccion> Cobros { get; set; }

    }
}
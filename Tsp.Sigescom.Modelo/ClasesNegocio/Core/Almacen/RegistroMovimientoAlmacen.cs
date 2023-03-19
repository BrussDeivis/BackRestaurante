using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class RegistroMovimientoAlmacen
    {
        public long IdOrdenDeAlmacen { get; set; }
        public long IdOperacion { get; set; }
        public ActorComercial_ Tercero { get; set; }
        public DateTime FechaInicioTraslado { get; set; }
        public RegistroTransporte Transportista { get; set; }
        public RegistroConductor Conductor { get; set; }
        public ItemGenerico ModalidadTransporte { get; set; }
        public ItemGenerico MotivoTraslado { get; set; }
        public string DescripcionMotivo { get; set; }
        public decimal PesoBrutoTotal { get; set; }
        public int NumeroBultos { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public List<DetalleMovimientoDeAlmacen> Detalles { get; set; }
        public string Observacion { get; set; }
        public ItemGenerico UbigeoOrigen { get; set; }
        public string DireccionOrigen { get; set; }
        public ItemGenerico UbigeoDestino { get; set; }
        public string DireccionDestino { get; set; }
        public ItemGenerico Almacen { get; set; }
        public ItemGenerico Almacenero { get; set; }
        public string DocumentoReferencia { get; set; }
        public bool EsTrasladoTotal { get; set; }
        public int IdTercero { get; set; }

        public RegistroMovimientoAlmacen()
        {
            this.Tercero = new ActorComercial_()
            {
                TipoDocumentoIdentidad = new ItemGenerico()
            };
            this.Transportista = new RegistroTransporte();
            this.Conductor = new RegistroConductor();
            this.TipoDeComprobante = new SelectorTipoDeComprobante();
            this.Detalles = new List<DetalleMovimientoDeAlmacen>();
        }
        public static List<MovimientoDeAlmacen> Convert_(List<RegistroMovimientoAlmacen> registros)
        {
            List<MovimientoDeAlmacen> salidasDeMercaderia = new List<MovimientoDeAlmacen>();
            foreach (var registro in registros)
            {
                salidasDeMercaderia.Add(new MovimientoDeAlmacen(new Transaccion()
                {
                    Detalle_transaccion = ConstruirDetalleMovimientoMercaderia(registro),
                    comentario = registro.Observacion,
                })
                {
                    IdTercero = registro.Tercero.Id,
                    IdComprobanteSeleccionado = registro.TipoDeComprobante.TipoComprobante.Id,
                    IdSerieSeleccionada = registro.TipoDeComprobante.SerieSeleccionada,
                    EsPropio = registro.TipoDeComprobante.EsPropio,
                    SerieIngresada = registro.TipoDeComprobante.SerieIngresada,
                    NumeroIngresado = registro.TipoDeComprobante.NumeroIngresado,
                    FechaInicioTraslado = registro.FechaInicioTraslado,
                    IdTransportista = registro.Transportista.Transportista.Id,
                    Placa = registro.Transportista.Placa,
                    IdConductor = registro.Conductor.Conductor.Id,
                    NumeroLicencia = registro.Conductor.NumeroLicencia,
                    IdModalidadTransporte = registro.ModalidadTransporte.Id,
                    IdMotivoTraslado = registro.MotivoTraslado.Id,
                    DescripcionMotivo = registro.DescripcionMotivo,
                    PesoBrutoTotal = registro.PesoBrutoTotal,
                    NumeroBultos = registro.NumeroBultos,
                    DireccionOrigen = registro.DireccionOrigen + " - " + registro.UbigeoOrigen.Nombre,
                    DireccionDestino = registro.DireccionDestino + " - " + registro.UbigeoDestino.Nombre,
                    IdUbigeoOrigen = registro.UbigeoOrigen.Id,
                    IdUbigeoDestino = registro.UbigeoDestino.Id
                });
            }
            return salidasDeMercaderia;
        }

        public static List<Detalle_transaccion> ConstruirDetalleMovimientoMercaderia(RegistroMovimientoAlmacen salidaMercaderia)
        {
            List<Detalle_transaccion> detallesConstruidos = new List<Detalle_transaccion>();
            foreach (var item in salidaMercaderia.Detalles)
            {
                detallesConstruidos.Add(new Detalle_transaccion(item.IngresoSalidaActual, item.IdProducto, null, 1, 1, null, 0, null, null, 0, 0, 0, item.Lote, null, null)
                {
                    Concepto_negocio = new Concepto_negocio()
                    {
                        id = item.IdProducto,
                        nombre = item.Descripcion,
                        Detalle_maestro4 = new Detalle_maestro()
                        {
                            valor = item.EsBien ? "1" : "0"
                        }
                    }
                }) ;
            }
            return detallesConstruidos;
        }
    }

    public class DetalleMovimientoDeAlmacen
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal StockActual { get; set; }
        public decimal Pendiente { get; set; }
        public decimal Ordenado { get; set; }
        public decimal RecibidoEntregado { get; set; }
        public decimal IngresoSalidaActual { get; set; }
        public string Lote { get; set; }
        public bool EsBien { get; set; }

        public DetalleMovimientoDeAlmacen()
        {
        }

        public DetalleMovimientoDeAlmacen(DetalleDeOperacion detalle, string lote, decimal cantidadEntregadaRecibida)
        {
            IdProducto = detalle.Producto.Id;
            Descripcion = detalle.Producto.NombreConcepto;
            Ordenado = detalle.Cantidad;
            RecibidoEntregado = cantidadEntregadaRecibida;
            IngresoSalidaActual = Ordenado - RecibidoEntregado;
            Lote = lote;
            EsBien = detalle.Producto.EsBien;
        }

        public static List<DetalleMovimientoDeAlmacen> Convertir(List<DetalleDeOperacion> detalles, List<MovimientoDeAlmacen> ordenes)
        {
            List<DetalleMovimientoDeAlmacen> detallesTrasladoDeMercaderia = new List<DetalleMovimientoDeAlmacen>();
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
                detallesTrasladoDeMercaderia.Add(new DetalleMovimientoDeAlmacen(detalle, detalle.Lote, cantidadEntregadaRecibida));
            }
            return detallesTrasladoDeMercaderia;
        }

        
    }

    public class RegistroTransporte
    {
        public ItemGenerico Transportista { get; set; }
        public string Placa { get; set; }

        public RegistroTransporte()
        {
        }
    }

    public class RegistroConductor
    {
        public ItemGenerico Conductor { get; set; }
        public string NumeroLicencia { get; set; }

        public RegistroConductor()
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
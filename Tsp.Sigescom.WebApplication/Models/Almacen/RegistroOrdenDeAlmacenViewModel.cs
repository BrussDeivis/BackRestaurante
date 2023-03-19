using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroOrdenDeAlmacenViewModel
    {
        public long IdOrdenDeOperacion { get; set; }
        public DateTime FechaTraslado { get; set; }
        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<DetalleMovimientoDeAlmacenViewModel> Detalles { get; set; }
        public string Observacion { get; set; }
        public bool EsGeneracionTotal { get; set; }

        public RegistroOrdenDeAlmacenViewModel()
        {
            this.TipoDeComprobante = new SelectorTipoDeComprobante();
        }
    }

    public class DetalleOrdenDeAlmacenViewModel
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Ordenado { get; set; }
        public decimal RecibidoEntregado { get; set; }
        public decimal IngresoSalidaActual { get; set; }
        public bool EsBien { get; set; }

        public DetalleOrdenDeAlmacenViewModel()
        {
        }

        public DetalleOrdenDeAlmacenViewModel(DetalleDeOperacion detalle, decimal cantidadEntregadaRecibida)
        {
            IdProducto = detalle.Producto.Id;
            Descripcion = detalle.Producto.Nombre;
            Ordenado = detalle.Cantidad;
            RecibidoEntregado = cantidadEntregadaRecibida;
            IngresoSalidaActual = Ordenado - RecibidoEntregado;
            EsBien = detalle.Producto.EsBien;
        }

        public static List<DetalleOrdenDeAlmacenViewModel> Convertir(List<DetalleDeOperacion> detalles, List<OrdenDeMovimientoDeAlmacen> ordenes)
        {
            List<DetalleOrdenDeAlmacenViewModel> detallesTrasladoDeMercaderia = new List<DetalleOrdenDeAlmacenViewModel>();
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
                detallesTrasladoDeMercaderia.Add(new DetalleOrdenDeAlmacenViewModel(detalle, cantidadEntregadaRecibida));
            }
            return detallesTrasladoDeMercaderia;
        }
    }
}
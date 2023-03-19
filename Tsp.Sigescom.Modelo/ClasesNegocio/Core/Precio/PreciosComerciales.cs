using Tsp.Sigescom.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class PrecioComercial
    {

        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public string CentroAtencion { get; set; }
        public string Tarifa { get; set; }
        public int IdConcepto { get; set; }

        public PrecioComercial(decimal precio, DateTime fecha, string centroAtencion, string tarifa)
        {
            this.Precio = precio;
            this.Fecha = fecha;
            this.CentroAtencion = centroAtencion;
            this.Tarifa = tarifa;
        }

        public PrecioComercial()
        { }
    }
    public class PrecioDeCompra : PrecioComercial
    {
        public string Proveedor { get; set; }


        public PrecioDeCompra(Detalle_transaccion detalleTransaccion) : base(detalleTransaccion.precio_unitario,detalleTransaccion.Transaccion.fecha_inicio,new OrdenDeCompra(detalleTransaccion.Transaccion).CentroDeAtencion().Nombre, "")
        {
            this.Proveedor = new OrdenDeCompra(detalleTransaccion.Transaccion).Proveedor().NombreComercial;
        }

        public PrecioDeCompra() : base()
        {

        }

        public static List<PrecioDeCompra> obtener(List<Detalle_transaccion> detallesTransaccion)
        {
            List<PrecioDeCompra> precios = new List<PrecioDeCompra>();
            foreach (var detalle in detallesTransaccion)
            {
                precios.Add(new PrecioDeCompra(detalle));
            }
            return precios;
        }


    }

    public class PrecioDeVenta : PrecioComercial
    {
        public PrecioDeVenta(Precio precio) : base(precio.valor, precio.fecha_inicio, new ActorComercial(precio.Actor_negocio).NombreComercial,precio.Detalle_maestro3.nombre)
        {
        }

        public PrecioDeVenta() : base()
        {

        }

        public static List<PrecioDeVenta> obtener(List<Precio> precios)
        {
            List<PrecioDeVenta> preciosDeVenta = new List<PrecioDeVenta>();
            foreach (var precio in precios)
            {
                preciosDeVenta.Add(new PrecioDeVenta(precio));
            }
            return preciosDeVenta;
        }


    }

}

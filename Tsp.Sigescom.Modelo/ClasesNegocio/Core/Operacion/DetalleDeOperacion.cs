using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleDeOperacion
    {
        public long Id { get; set; }
        public Concepto_Negocio_Comercial Producto { get; set; }//Todo: cambiaar el nombre a concepto
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }//Verificar el nombre importe o total
        public decimal Neto { get; set; }
        public decimal Igv { get; set; }
        public decimal Isc { get; set; }
        public decimal Descuento { get; set; }
        public string Detalle { get; set; }
        public string Lote { get; set; }
        public string Registro { get; set; }
        public DateTime? Vencimiento { get; set; }
        public string MascaraDeCalculo { get; set; }
        public decimal Cantidad1 { get; set; }

        public DetalleDeOperacion()
        {
        }

        public DetalleDeOperacion(Detalle_transaccion detalleTransaccion)
        {
            this.Producto = new Concepto_Negocio_Comercial()
            {
                Id = detalleTransaccion.Concepto_negocio.id,
                EsBien = detalleTransaccion.Concepto_negocio.EsBien
            };
            this.PrecioUnitario = detalleTransaccion.precio_unitario;
            this.Cantidad = detalleTransaccion.cantidad;
            this.Importe = detalleTransaccion.total;
            this.Igv = (decimal)detalleTransaccion.igv;
            this.Isc = (decimal)detalleTransaccion.isc;
            this.Descuento = (decimal)detalleTransaccion.descuento;
            this.Lote = detalleTransaccion.lote;
            this.Detalle = detalleTransaccion.detalle;
            this.Vencimiento = detalleTransaccion.vencimiento;
            this.Registro = detalleTransaccion.registro;
        }

        public DetalleDeOperacion(Detalle_transaccion detalleTransaccion, string mascaraDeCalculo)
        {
            this.Producto = new Concepto_Negocio_Comercial()
            {
                Id = detalleTransaccion.Concepto_negocio.id,
                EsBien = detalleTransaccion.Concepto_negocio.EsBien
            };
            this.PrecioUnitario = detalleTransaccion.precio_unitario;
            this.Cantidad = detalleTransaccion.cantidad;
            this.Importe = detalleTransaccion.total;
            this.Igv = (decimal)detalleTransaccion.igv;
            this.Isc = (decimal)detalleTransaccion.isc;
            this.Descuento = (decimal)detalleTransaccion.descuento;
            this.Lote = detalleTransaccion.lote;
            this.Detalle = detalleTransaccion.detalle;
            this.Vencimiento = detalleTransaccion.vencimiento;
            this.Registro = detalleTransaccion.registro;
            this.MascaraDeCalculo = mascaraDeCalculo;
        }

        public DetalleDeOperacion(VentaMonoDetalle detalle)
        {
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = detalle.IdConcepto,
                EsBien = detalle.EsBien
            };
            Cantidad = detalle.Cantidad;
            PrecioUnitario = detalle.PrecioUnitario;
            Importe = detalle.Importe;
            Igv = 0;
            Isc = 0;
            Descuento = 0;
            Lote = null;
            Detalle = "";
            Vencimiento = null;
            Registro = null;
            MascaraDeCalculo = detalle.MascaraDeCalculo;
        }
        public static List<DetalleDeOperacion> Convert(List<VentaMonoDetalle> ventasMonoDetalle)
        {
            List<DetalleDeOperacion> detallesDeOperacion = new List<DetalleDeOperacion>();
            foreach (var ventaMonoDetalle in ventasMonoDetalle)
            {
                detallesDeOperacion.Add(new DetalleDeOperacion(ventaMonoDetalle));
            }
            return detallesDeOperacion;
        }

        public DetalleDeOperacion(long id, int idConceptoNegocio, string nombre, decimal cantidad, decimal precioUnitario, decimal total, decimal igv, decimal isc, decimal descuento, string comentario, string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            Id = id;
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = idConceptoNegocio,
                Nombre = nombre,
                EsBien = esBien,
            };
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Importe = total;
            Igv = igv;
            Isc = isc;
            Descuento = descuento;
            Lote = lote;
            Detalle = comentario;
            Vencimiento = vencimiento;
            Registro = registro;
            MascaraDeCalculo = mascaraDeCalculo;
        }
        public DetalleDeOperacion(long id, int idConceptoNegocio, string codigo, string nombre, decimal cantidad, decimal precioUnitario, decimal total, decimal igv, decimal isc, decimal descuento, string comentario, string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            Id = id;
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = idConceptoNegocio,
                Codigo = codigo,
                Nombre = nombre,
                EsBien = esBien,
            };
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Importe = total;
            Igv = igv;
            Isc = isc;
            Descuento = descuento;
            Lote = lote;
            Detalle = comentario;
            Vencimiento = vencimiento;
            Registro = registro;
            MascaraDeCalculo = mascaraDeCalculo;
        }
        public DetalleDeOperacion(long id, int idConceptoNegocio, int idConceptoBasico, string codigo, string nombre, decimal cantidad, decimal precioUnitario, decimal total, decimal igv, decimal isc, decimal descuento, string comentario, string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores, decimal cantidad1)
        {
            Id = id;
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = idConceptoNegocio,
                Codigo = codigo,
                Nombre = nombre,
                EsBien = esBien,
                IdConceptoBasico = idConceptoBasico,
            };
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Importe = total;
            Igv = igv;
            Isc = isc;
            Descuento = descuento;
            Lote = lote;
            Detalle = comentario;
            Vencimiento = vencimiento;
            Registro = registro;
            MascaraDeCalculo = mascaraDeCalculo;
            Cantidad1 = cantidad1;
        }
        public DetalleDeOperacion(long id, int idConceptoNegocio, decimal cantidad, decimal precioUnitario, decimal total, decimal igv, decimal isc, decimal descuento, string comentario, string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            Id = id;
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = idConceptoNegocio,
                EsBien = esBien,
            };
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
            Importe = total;
            Igv = igv;
            Isc = isc;
            Descuento = descuento;
            Lote = lote;
            Detalle = comentario;
            Vencimiento = vencimiento;
            Registro = registro;
            MascaraDeCalculo = mascaraDeCalculo;
        }
        public DetalleDeOperacion(int idConceptoNegocio, decimal cantidad, decimal precioUnitario, decimal total, decimal igv, decimal isc, decimal descuento, string comentario, string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            Producto = new Concepto_Negocio_Comercial()
            {
                Id = idConceptoNegocio,
                EsBien = esBien,
            };
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
            Importe = total;
            Igv = igv;
            Isc = isc;
            Descuento = descuento;
            Lote = lote;
            Detalle = comentario;
            Vencimiento = vencimiento;
            Registro = registro;
            MascaraDeCalculo = mascaraDeCalculo;
        }

        public Detalle_transaccion DetalleTransaccion()
        {
            return new Detalle_transaccion(Id, Cantidad, Producto.Id, Detalle, PrecioUnitario, Importe, null, 0, null, null, Isc, Igv, Descuento, Lote, Vencimiento, Registro, Cantidad1);
        }

        public DetalleDeOperacion Clone()
        {
            return new DetalleDeOperacion(Producto.Id, Cantidad, PrecioUnitario, Importe, Igv, Isc, Descuento, Detalle, Lote, Vencimiento, Registro, Producto.EsBien, MascaraDeCalculo, null);
        }

        public static List<DetalleDeOperacion> Clone(List<DetalleDeOperacion> detallesDeOperacionAClonar)
        {
            List<DetalleDeOperacion> detallesDeOperacionClonados = new List<DetalleDeOperacion>();
            foreach (var detalleDeOperacionAClonar in detallesDeOperacionAClonar)
            {
                detallesDeOperacionClonados.Add(detalleDeOperacionAClonar.Clone());
            }
            return detallesDeOperacionClonados;
        }

        public static Detalle_transaccion Convert(DetalleDeOperacion detalleDeOperacion)
        {
            return new Detalle_transaccion(detalleDeOperacion.Id, detalleDeOperacion.Cantidad, detalleDeOperacion.Producto.Id, detalleDeOperacion.Detalle, detalleDeOperacion.PrecioUnitario, detalleDeOperacion.Importe, null, 0, null, null, 0, 0, detalleDeOperacion.Descuento, detalleDeOperacion.Lote, detalleDeOperacion.Vencimiento, detalleDeOperacion.Registro);
        }

        public static DetalleDeOperacion Convert(Detalle_transaccion detalleTransaccion)
        {
            return new DetalleDeOperacion(detalleTransaccion.id, detalleTransaccion.id_concepto_negocio, detalleTransaccion.Concepto_negocio.id_concepto_basico, detalleTransaccion.Concepto_negocio.codigo, detalleTransaccion.Concepto_negocio.nombre, detalleTransaccion.cantidad, detalleTransaccion.precio_unitario, detalleTransaccion.total, (decimal)detalleTransaccion.igv, (decimal)detalleTransaccion.isc, (decimal)detalleTransaccion.descuento, detalleTransaccion.detalle, detalleTransaccion.lote, detalleTransaccion.vencimiento, detalleTransaccion.registro, detalleTransaccion.Concepto_negocio.EsBien, VentasSettings.Default.MascaraDeCalculoDeNingunValorCalculado, null, detalleTransaccion.cantidad_1);
        }

        public static List<DetalleDeOperacion> Convert(List<Detalle_transaccion> detallesTransaccion)
        {
            List<DetalleDeOperacion> detallesDeOperacion = new List<DetalleDeOperacion>();
            foreach (var detalleTransaccion in detallesTransaccion)
            {
                detallesDeOperacion.Add(Convert(detalleTransaccion));
            }
            return detallesDeOperacion;
        }

        public static List<Detalle_transaccion> Convert(List<DetalleDeOperacion> detallesDeOperacion)
        {
            List<Detalle_transaccion> detallesTransaccion = new List<Detalle_transaccion>();
            foreach (var detalleDeOperacion in detallesDeOperacion)
            {
                detallesTransaccion.Add(Convert(detalleDeOperacion));
            }
            return detallesTransaccion;
        }

        public OperacionGenericaNivel3 Operacion()
        {
            return new OperacionGenericaNivel3();
        }
    }
}

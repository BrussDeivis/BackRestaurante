using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleEstadoCuentaCliente_VentaCobro
    {
        public long IdOperacion { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleDeVenta> DetallesDeVenta { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal Cobro { get; set; }
        public decimal Saldo { get; set; }

        public DetalleEstadoCuentaCliente_VentaCobro()
        { }

        public static List<DetalleEstadoCuentaCliente_VentaCobro> Convert()
        {
            return new List<DetalleEstadoCuentaCliente_VentaCobro>();
        }
    }

    public class DetalleDeVenta
    {
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Cantidad != 0 ? Importe / Cantidad : 0; }
        }
        public DetalleDeVenta()
        { }

        public static List<DetalleDeVenta> Convert()
        {
            return new List<DetalleDeVenta>();
        }
    }

    public class DetalleTransaccionVentaCobro
    {
        public long IdOperacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Cantidad != 0 ? Importe / Cantidad : 0; }
        }
        public decimal Cobro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class EstadoDeCuenta
    {
        private decimal saldoAnterior;
        private decimal entregas;
        private decimal pagos;
        private decimal saldoFinal;
        private List<DetalleEstadoDeCuenta> detalle;

        public EstadoDeCuenta()
        {

        }

        public EstadoDeCuenta(decimal saldoAnterior, decimal entregas, decimal pagos, decimal saldoFinal, List<DetalleEstadoDeCuenta> detalle)
        {
            this.saldoAnterior = saldoAnterior;
            this.entregas = entregas;
            this.pagos = pagos;
            this.saldoFinal = saldoFinal;
            this.detalle = detalle;
        }

        public decimal SaldoAnterior { get => saldoAnterior; set => saldoAnterior = value; }
        public decimal Entregas { get => entregas; set => entregas = value; }
        public decimal Pagos { get => pagos; set => pagos = value; }
        public decimal SaldoFinal { get => saldoFinal; set => saldoFinal = value; }
        public List<DetalleEstadoDeCuenta> Detalle { get => detalle; set => detalle = value; }




        public class DetalleEstadoDeCuenta
        {
            private DateTime fecha;
            private string nombreOperacion;
            private decimal cantidad;
            private string concepto;
            private decimal precioUnitario;
            private decimal importe;
            private decimal saldo;



            public DetalleEstadoDeCuenta()
            {

            }

            public DetalleEstadoDeCuenta(DateTime fecha, string nombreOperacion, decimal cantidad, string concepto, decimal precioUnitario, decimal importe, decimal saldo)
            {
                this.fecha = fecha;
                this.nombreOperacion = nombreOperacion;
                this.cantidad = cantidad;
                this.concepto = concepto;
                this.precioUnitario = precioUnitario;
                this.importe = importe;
                this.saldo = saldo;
            }


            public DateTime Fecha { get => fecha; set => fecha = value; }
            public string NombreOperacion { get => nombreOperacion; set => nombreOperacion = value; }
            public decimal Cantidad { get => cantidad; set => cantidad = value; }
            public string Concepto { get => concepto; set => concepto = value; }
            public decimal PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
            public decimal Importe { get => importe; set => importe = value; }
            public decimal Saldo { get => saldo; set => saldo = value; }
 

            public List<DetalleEstadoDeCuenta> Detalle()
            {
                return null;
            }

        }

    }


}
     
   

    


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Transaccion_Deuda_A_Proveedor
    {
        public string NombreProveedor;
        /// <summary>
        /// monto total que se adeuda al proveedor
        /// </summary>
        public decimal Total;
        /// <summary>
        /// el acumulado lo que voy pagando
        /// </summary>
        public decimal Acuenta;
        /// <summary>
        /// deuda actual
        /// </summary>
        public decimal Deuda
        {
            get
            { return Total - Acuenta; }
        }
        /// <summary>
        /// devuelve la serie y el numero de comprobante de la transaccion de la que hace referencia la cuota
        /// </summary>
        public string Comprobante;

        
    }
}
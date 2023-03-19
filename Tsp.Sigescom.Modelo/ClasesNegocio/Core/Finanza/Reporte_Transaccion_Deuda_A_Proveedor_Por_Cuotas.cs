using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Transaccion_Deuda_A_Proveedor_Por_Cuotas
    {
        public DateTime fechaVencimiento;
        public int Cuota;
        public string NombreCortoComprobante; public string Comprobante;
        public string NombreProveedor;
        public decimal Total;
        public decimal Acuenta;
        public decimal Deuda
        { get  { return Total - Acuenta; }  }
    }
}
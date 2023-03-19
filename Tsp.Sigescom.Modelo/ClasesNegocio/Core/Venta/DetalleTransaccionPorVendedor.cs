using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class DetalleTransaccionPorVendedor
    {


        public string DNI { get; set; }
        public string Empleado { get; set; }
        public int IdConceptoNegocio { get; set; }
        public string CodigoConceptoNegocio { get; set; }
        public string NombreConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public DetalleTransaccionPorVendedor()
        {}

        public static List<DetalleTransaccionPorVendedor> Convert()
        {
            return new List<DetalleTransaccionPorVendedor>();
        }
    }
}

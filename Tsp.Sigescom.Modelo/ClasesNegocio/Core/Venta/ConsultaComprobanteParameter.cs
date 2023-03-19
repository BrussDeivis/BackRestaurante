using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta
{
    public class ConsultaComprobanteParameter
    {
        public string Comprobante { get;set;}
        public string Serie { get;set;}
        public string Numero { get;set;}
        public DateTime FechaEmision { get; set; }
        public decimal Importe { get; set; }    

    }
}

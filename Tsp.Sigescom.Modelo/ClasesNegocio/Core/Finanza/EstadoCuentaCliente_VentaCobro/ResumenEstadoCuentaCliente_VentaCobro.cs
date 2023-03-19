using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ResumenEstadoCuentaCliente_VentaCobro
    {
        public decimal SaldoAnterior { get; set; }
        public decimal EntregaTotal { get; set; }
        public decimal CobroTotal { get; set; }
        public decimal SaldoFinal { get; set; }

        public ResumenEstadoCuentaCliente_VentaCobro()
        { }

        public static List<ResumenEstadoCuentaCliente_VentaCobro> Convert()
        {
            return new List<ResumenEstadoCuentaCliente_VentaCobro>();
        }
    }
}

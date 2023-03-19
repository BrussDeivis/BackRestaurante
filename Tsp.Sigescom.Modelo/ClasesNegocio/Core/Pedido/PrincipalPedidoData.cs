using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido
{
    public class PrincipalPedidoData
    {
        public DateTime FechaActual { get; set; }
        public bool TieneRolVendedorDeNegocio { get; set; }
        public bool TieneRolCajeroDeNegocio { get;set ; }

        public int ComprobanteParaPedido { get;set; }
    }
}

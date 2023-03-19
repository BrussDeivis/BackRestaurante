using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Configuraciones
{
    public class ConfiguracionDePedido
    {
        public readonly bool MostrarSeccionEntregaEnPedido = PedidoSettings.Default.MostrarSeccionEntregaEnPedido;
        public string FechaActual;

    }
}

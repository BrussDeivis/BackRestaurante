using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteOrden
    {
        private static readonly ConfiguracionRestauranteOrden defaultInstance = new ConfiguracionRestauranteOrden();
        public static ConfiguracionRestauranteOrden Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public readonly int EstadoConfirmado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
        public readonly int EstadoCerrado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado;
        public readonly ConfiguracionRestauranteDetalleOrden ConfiguracionDetallesDeOrden = ConfiguracionRestauranteDetalleOrden.Default;

    }
}

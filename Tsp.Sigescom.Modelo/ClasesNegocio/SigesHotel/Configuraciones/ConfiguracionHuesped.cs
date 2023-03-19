using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public sealed class ConfiguracionHuesped
    {
        private static readonly ConfiguracionHuesped defaultInstance = new ConfiguracionHuesped();
        public static ConfiguracionHuesped Default
        {
            get
            {
                return defaultInstance;
            }
        }

        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;
        public readonly int TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
        public readonly int MinimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
        public readonly int IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
        public readonly int IdTipoPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
        public readonly string MascaraDeVisualizacionValidacionRegistroHuesped = HotelSettings.Default.MascaraDeVisualizacionValidacionRegistroHuesped;
    }
}


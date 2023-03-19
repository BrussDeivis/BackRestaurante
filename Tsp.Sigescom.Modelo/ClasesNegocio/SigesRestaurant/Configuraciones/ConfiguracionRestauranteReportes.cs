using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteReportes
    {
        private static readonly ConfiguracionRestauranteReportes defaultInstance = new ConfiguracionRestauranteReportes();
        public static ConfiguracionRestauranteReportes Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public readonly int MaximoDiasAtenciones = RestauranteSettings.Default.MaximoDiasConsultaAtenciones;
        public readonly int MaximoDiasOrdenesPorConcepto = RestauranteSettings.Default.MaximoDiasConsultaOrdenesPorConcepto;
        public readonly int MaximoDiasOrdenesPorMozo = RestauranteSettings.Default.MaximoDiasConsultasOrdenesPorMozo;
        public readonly int MaximoDiasOrdenesDetalladas = RestauranteSettings.Default.MaximoDiasConsultasOrdenesDetalladas;
        public readonly int MaximoDiasDevolucionesEnOrdenes = RestauranteSettings.Default.MaximoDiasConsultasDevolucionesEnOrdenes;
        public readonly int MaximoDiasPorModoAtenciones = RestauranteSettings.Default.MaximoDiasConsultaPorModoAtenciones;



    }
}

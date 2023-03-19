using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.Configuraciones
{
    public sealed class ConfiguracionEstadoHabitacion
    {
        private static readonly ConfiguracionEstadoHabitacion defaultInstance = new ConfiguracionEstadoHabitacion();
        public static ConfiguracionEstadoHabitacion Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public readonly int Todo = (int)EstadoHabitacionEnum.Todo;
        public readonly int EstadoDisponible = (int)EstadoHabitacionEnum.Disponible;
        public readonly int EstadoOcupado = (int)EstadoHabitacionEnum.Ocupado;
        public readonly int EstadoReservado = (int)EstadoHabitacionEnum.Reservado;
        public readonly int EstadoOcupadoDisponible = (int)EstadoHabitacionEnum.OcupadoDisponible; 

    }
}

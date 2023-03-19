using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Planificador
    {
        public List<HabitacionEnPlanificador> HabitacionesEnPlanificador { get; set; }
        public List<string> FechasPlanificador { get => HabitacionesEnPlanificador.SelectMany(h => h.EstadosHabitacion).Select(h => h.Fecha.Date).Distinct().OrderBy(h => h).Select(h => h.ToString("dd/MM/yyyy")).ToList(); }
        public Planificador() 
        {
            HabitacionesEnPlanificador = new List<HabitacionEnPlanificador>();
        }
    }


}

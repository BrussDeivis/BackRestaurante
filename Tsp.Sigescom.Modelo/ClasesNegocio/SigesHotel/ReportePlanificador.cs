using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class ReportePlanificador
    {
        public int Disponibles { get; set; }
        public int Ocupadas { get; set; }
        public int PorIngresar { get; set; }
        public int PorSalir { get; set; }
        public int EnLimpieza { get; set; }
        public DateTime FechaActual { get; set; }
        public string FechaActualString { get => FechaActual.Date.ToLongDateString(); }
        public string HoraActualString { get => FechaActual.ToString("hh:mm tt"); }
        public ReportePlanificador() 
        {

        }
    }


}

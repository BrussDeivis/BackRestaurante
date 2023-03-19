using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    /// <summary>
    /// Un turno representa un intervalo de tiempo para la realización de actividades: horario de trabajo de un funcionario, el horario de atención de un establecimiento, etc.
    /// </summary>
    public partial class Turno
    {
        /// <summary>
        /// Horario de inicio del turno
        /// </summary>
        /// <param name="fechaReferencia"></param>
        /// <returns></returns>
        public DateTime Inicio(DateTime fechaReferencia)
        {
            return fechaReferencia.Date.Add(this.hora_inicio);
        }
        public DateTime Fin(DateTime fechaReferencia)
        {
            return fechaReferencia.Date.Add(this.hora_inicio).AddHours((double)this.duracion_horas);
        }
    }
}

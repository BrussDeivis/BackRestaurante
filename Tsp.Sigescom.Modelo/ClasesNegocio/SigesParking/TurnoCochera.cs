using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class TurnoCochera
    {
        public int Id { get; set; }
        public int IdTipoDeTurno { get; set; }
        public string Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public decimal DuracionEnHoras { get; set; }
        public ConfiguracionTurnoCochera Configuracion { get; set; }
        public string FechaInicioString { get; set; }
        public string HoraInicioString { get; set; }
        public string FechaFinString { get; set; }
        public string HoraFinString { get; set; }

        public DateTime Inicio(DateTime date)
        {
            return date.Date.Add(HoraInicio);
        }

        public DateTime Fin(DateTime date)
        {
            return Inicio(date.Date).AddHours((double)DuracionEnHoras);
        }
        public TurnoCochera() { }
        public TurnoCochera(Turno turno_)
        {
            Id = turno_.id;
            IdTipoDeTurno = turno_.id_tipo;
            Nombre = turno_.nombre;
            DuracionEnHoras = turno_.duracion_horas;
            HoraInicio = turno_.hora_inicio;
            Configuracion = JsonConvert.DeserializeObject<ConfiguracionTurnoCochera>(turno_.extension_json);
        }

        public static List<TurnoCochera> Convert(List<Turno> turnos)
        {
            List<TurnoCochera> turnosCochera = new List<TurnoCochera>();
            foreach (var turno_ in turnos)
            {
                turnosCochera.Add( new TurnoCochera (turno_));
            }
            return turnosCochera;
        }
    }
}

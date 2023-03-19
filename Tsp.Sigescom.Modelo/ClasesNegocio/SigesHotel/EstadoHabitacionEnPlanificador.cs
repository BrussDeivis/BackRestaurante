using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{

    public class EstadoHabitacionEnPlanificador
    {
        public DateTime Fecha { get; set; }
        public string FechaString { get => Fecha.ToString("dd/MM/yyyy"); }
        public string FechaSiguienteString { get => Fecha.AddDays(1).ToString("dd/MM/yyyy"); }
        public long IdAtencionMacro { get; set; }
        public long IdAtencion { get; set; }
        public int IdEstado { get; set; }
        public bool EstaDisponible { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoDisponible || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado; }
        public bool EstaOcupado { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut; }
        public bool EstaOcupadoDisponible { get => (IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut) && EsFechaAtencion; }
        public bool EstaReservado { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado; }
        public int EstadoHabitacion { get => EstaOcupadoDisponible ? (int)EstadoHabitacionEnum.OcupadoDisponible : (EstaOcupado ? (int)EstadoHabitacionEnum.Ocupado : (EstaReservado ? (int)EstadoHabitacionEnum.Reservado : (int)EstadoHabitacionEnum.Disponible)); }
        public bool PuedeHacerConsumo { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn && EsFechaAtencion; }
        public bool EsFechaAtencion { get; set; }
        public EstadoHabitacionEnPlanificador() { }
    }
}

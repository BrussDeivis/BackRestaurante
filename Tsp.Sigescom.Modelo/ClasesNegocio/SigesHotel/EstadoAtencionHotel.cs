using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class EstadoAtencionHotel
    {
        /// <summary>
        /// Cuando se registra estados de atencion y se necesita los ids
        /// </summary>
        public long IdAuxiliar { get; set; }
        public bool Facturado { get; set; }
        /// <summary>
        /// Atributo que nos dice si es que la atencion ya tiene alguna Facturacion realizada
        /// </summary>
        public bool TieneFacturacion { get; set; }
        /// <summary>
        /// Atributo que muestra si el facturado de manera global (sino seria individual)
        /// </summary>
        public bool FacturadoGlobal { get; set; }
        public ItemEstado EstadoActual { get => Estados == null ? new ItemEstado() : Estados.Except(Estados.Where(e => e.Id == MaestroSettings.Default.IdDetalleMaestroEstadoFacturado || e.Id == MaestroSettings.Default.IdDetalleMaestroEstadoIncidente)).Last(); }
        public ItemEstado EstadoEventoFinal { get => Estados == null ? new ItemEstado() : Estados.Last(); }
        public List<ItemEstado> Estados { get; set; }
        public bool PuedeFacturar { get => EstadoActual.Id != MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado && EstadoActual.Id != MaestroSettings.Default.IdDetalleMaestroEstadoAnulado; }
        public bool PuedeConfirmar { get => EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado; }
        public bool PuedeCheckIn { get => EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado; }
        public bool PuedeCheckOut { get => EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado; }
        public bool PuedeAnular { get => EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado || EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado; }
        public bool PuedeCambiarHabitacion { get => EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado || EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn || EstadoActual.Id == MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado; }

        public EstadoAtencionHotel() { }
    }
}

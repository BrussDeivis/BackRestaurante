using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class ReservaBandeja
    {
        public long IdAtencion { get; set; }
        public long IdAtencionMacro { get; set; }
        public string Codigo { get; set; }
        public string Responsable { get; set; }
        public string Ambiente { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public DateTime Ingreso { get; set; }
        public string FechaIngreso { get => Ingreso.ToString("dd/MM/yyyy"); }
        public DateTime Salida { get; set; }
        public string FechaSalida { get => Salida.ToString("dd/MM/yyyy"); }
        public int Noches { get; set; }
        public decimal Importe { get; set; }
        public string Total { get => Importe.ToString("N2"); }
        public string Estado { get; set; }
        public bool Facturado { get; set; }
        public string EstaFacturado { get => Facturado ? "SI" : "NO"; }
    }
}

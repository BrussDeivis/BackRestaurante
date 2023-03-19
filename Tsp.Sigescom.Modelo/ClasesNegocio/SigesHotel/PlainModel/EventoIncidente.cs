using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class EventoAtencion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Empleado { get; set; }
        public string Justificacion { get; set; }
        public int ModoFacturacion { get; set; }
        public long IdAtencion { get; set; }
    }

    public class AtencionHabitacion
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Habitacion { get; set; }
        public decimal Importe { get; set; }
    }

    //public class AtencionIncidenteHabitacion
    //{
    //    private IEnumerable<Detalle_transaccion> detalles;
    //    private IEnumerable<Transaccion> transacciones;
    //    public long? IdReferencia { get; set; }
    //    public string TipoComprobante { get; set; }
    //    public string SerieYNumeroComprobante { get; set; }
    //    public decimal Importe { get; set; }
    //    public IEnumerable<Detalle_transaccion> Detalles { set => detalles = value; }
    //    public IEnumerable<Transaccion> Transacciones { set => transacciones = value; }
    //    public decimal MontoHospedaje { get => detalles == null ? transacciones.SelectMany(tt => tt.Detalle_transaccion).Sum(dt => dt.total) : detalles.Sum(dt => dt.total); }
    //}
}

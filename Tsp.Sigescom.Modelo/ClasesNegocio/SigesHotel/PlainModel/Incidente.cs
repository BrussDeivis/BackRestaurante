using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Incidente
    {
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }  
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public decimal Importe { get; set; }
        public decimal MontoHospedaje { get; set; }
        public decimal Devuelto { get; set; }
        public string Habitacion { get; set; }
        public string TipoComprobanteDescuento { get; set; }
        public string SerieYNumeroComprobanteDescuento { get; set; }
        public string Solucion { get; set; }
        public string Empleado { get; set; }
        public string Justificacion { get; set; }
        public List<Incidente> Convert()
        {
            return new List<Incidente>();
        }
    }
}

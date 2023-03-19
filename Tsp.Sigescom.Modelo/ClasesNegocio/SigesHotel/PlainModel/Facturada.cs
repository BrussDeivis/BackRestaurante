using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Facturada
    {
        public int IdEventoFacturado { get; set; }
        public string Facturacion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string Codigo { get; set; }
        public string Habitacion { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public string Cliente { get; set; }
        public decimal Importe { get; set; }

        public List<Facturada> Convert()
        {
            return new List<Facturada>();
        }
    }
}

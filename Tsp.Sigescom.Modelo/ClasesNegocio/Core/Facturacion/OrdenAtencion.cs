using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion
{
    public class OrdenAtencion
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<DetalleOrdenAtencion> Detalles { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroString { get => FechaRegistro.ToString("dd/MM/yyyy"); }
        public string FechaHoraRegistroString { get => FechaRegistro.ToString("dd/MM/yyyy hh:mm tt"); }
        public bool TieneFacturacion { get; set; }

        public OrdenAtencion() { }

    }
}

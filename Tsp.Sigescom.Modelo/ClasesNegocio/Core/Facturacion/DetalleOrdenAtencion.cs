using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion
{
    public class DetalleOrdenAtencion
    {
        public long Id { get; set; }
        public int IdConcepto { get; set; }
        public int IdFamilia { get; set; }
        public string NombreConcepto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public int IdEstadoActual { get; set; }
        public bool EstaAnulado { get => IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado; }

        public DetalleOrdenAtencion() { }
    }
}

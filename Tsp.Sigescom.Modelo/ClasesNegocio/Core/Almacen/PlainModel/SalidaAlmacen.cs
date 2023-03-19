using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class SalidaAlmacen
    {
        public DateTime Fecha { get; set; }
        public string Operacion { get; set; }
        public string Destino { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public string Empleado { get; set; }
        public Decimal Cantidad { get; set; }
        public string Concepto { get; set; }

        public List<SalidaAlmacen> Convert()
        {
            return new List<SalidaAlmacen>();
        }
    }
}

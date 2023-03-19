using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Custom.SigesParking;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class AfectacionInventarioActual
    {
        public List<Detalle_transaccion> Detalles_modificados{ get; set; }
        public List<Detalle_transaccion> Detalles_nuevos { get; set; }
    }
}

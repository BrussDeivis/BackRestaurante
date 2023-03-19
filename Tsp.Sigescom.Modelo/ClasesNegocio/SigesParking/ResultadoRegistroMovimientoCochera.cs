using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Custom.SigesParking;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ResultadoRegistroMovimientoCochera
    {
        public MovimientoCochera Movimiento{ get; set; }
        public OrdenDeVenta OrdenDeVenta { get; set; }
    }
}

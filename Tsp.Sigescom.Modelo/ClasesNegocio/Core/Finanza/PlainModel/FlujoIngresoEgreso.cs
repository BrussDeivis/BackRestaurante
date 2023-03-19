using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{

    public class FlujoIngresoEgreso
    {
        public ResumenFlujo Resumen { get; set; }
        public List<DetalleFlujo> Detalles { get; set; }
        public List<FlujoIngresoEgreso> Convert()
        {
            return new List<FlujoIngresoEgreso>();
        }
    } 
}

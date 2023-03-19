using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioVencimiento:InventarioFisico
    {
        public DateTime? FechaVencimiento { get; set; }
        

        public InventarioVencimiento()
        {

        }
        new public List<InventarioVencimiento> Convert()
        {
            return new List<InventarioVencimiento>();
        }

        

    }
}

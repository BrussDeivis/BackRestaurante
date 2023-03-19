using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioEnAlmacen : InventarioFisico
    {
        public string Almacen { get; set; }
       

        public InventarioEnAlmacen()
        {

        }
        public List<InventarioEnAlmacen> Convert()
        {
            return new List<InventarioEnAlmacen>();
        }

        

    }
}

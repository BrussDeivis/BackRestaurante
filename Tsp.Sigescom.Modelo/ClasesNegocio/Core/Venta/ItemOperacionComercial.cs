using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class ItemOperacionComercial
    {
        public int IdItem { get; set; }
        public string NombreItem { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitarioPromedio
        {
            get
            { return Importe / Cantidad; }
        }
        
        public ItemOperacionComercial()
        { }

        public static List<ItemOperacionComercial> Convert()
        {
            return new List<ItemOperacionComercial>();
        }
    }
}

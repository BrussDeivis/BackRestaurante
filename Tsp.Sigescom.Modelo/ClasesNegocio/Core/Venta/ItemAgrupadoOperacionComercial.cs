using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    /// <summary>
    /// por ejemplo, se agrupa por caracteristica
    /// </summary>
    public class ItemConGrupoOperacionComercial
    {
        public long? IdItem { get; set; }
        public string NombreItem { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitarioPromedio
        {
            get
            { return Importe / Cantidad; }
        }
        
        public ItemConGrupoOperacionComercial()
        { }

        public static List<ItemConGrupoOperacionComercial> Convert()
        {
            return new List<ItemConGrupoOperacionComercial>();
        }

        
    }
}

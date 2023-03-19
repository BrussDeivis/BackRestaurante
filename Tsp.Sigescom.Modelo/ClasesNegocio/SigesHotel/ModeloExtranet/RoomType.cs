using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImagen { get; set; }
        public string[] BedArray { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string Description { get; set; }
        public decimal PriceValue { get; set; }
        public int AvailabilityAmount { get; set; } //
    }
}

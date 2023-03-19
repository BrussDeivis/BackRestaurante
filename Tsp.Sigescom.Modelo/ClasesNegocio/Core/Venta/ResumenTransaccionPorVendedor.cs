using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class ResumenTransaccionPorVendedor
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public decimal Importe { get; set; }
        public IEnumerable<string> Icbpers { get; set; }
        public decimal Icbper { get => (Icbpers != null) && Icbpers.Count() > 0 ? Icbpers.Sum(i => Decimal.Parse(i)) : 0; }
        public decimal ImporteSinIcbper { get => Importe - Icbper; }


        public ResumenTransaccionPorVendedor()
        {}

        public static List<ResumenTransaccionPorVendedor> Convert()
        {
            return new List<ResumenTransaccionPorVendedor>();
        }
    }

}

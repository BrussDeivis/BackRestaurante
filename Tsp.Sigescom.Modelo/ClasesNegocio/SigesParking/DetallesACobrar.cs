using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class DetallesACobrar
    {
        public decimal Principal { get; set; }
        public decimal Exceso { get; set; }
        public decimal Ticket { get; set; }
        public decimal Total { get { return (Principal + Exceso + Ticket); } }
    }
}

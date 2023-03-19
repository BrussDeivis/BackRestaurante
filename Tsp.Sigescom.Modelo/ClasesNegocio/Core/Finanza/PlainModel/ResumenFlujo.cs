using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class ResumenFlujo
    {
        public decimal SaldoInicial { get; set; }
        public decimal Ingresos { get; set; }
        public decimal Egresos { get; set; }
        public decimal SaldoFinal { get; set; }

        public List<ResumenFlujo> Convert()
        {
            return new List<ResumenFlujo>();
        }
    }
}

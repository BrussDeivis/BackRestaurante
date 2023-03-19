using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto
{
    public class Concepto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public string Familia { get; set; }
        public string UnidadMedida { get; set; }
        public decimal StockMinimo { get; set; }


        public Concepto()
        {
        }


       
    }
}
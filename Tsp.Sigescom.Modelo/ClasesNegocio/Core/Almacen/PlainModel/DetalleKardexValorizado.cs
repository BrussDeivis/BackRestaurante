using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class DetalleKardexValorizado: DetalleKardexFisico
    {
        public decimal ImporteUnitarioEntrada { get; set; }
        public decimal ImporteTotalEntrada { get; set; }
        public decimal ImporteUnitarioSalida { get; set; }
        public decimal ImporteTotalSalida { get; set; }
        public decimal ImporteUnitarioSaldo { get; set; }
        public decimal ImporteTotalSaldo { get; set; }

        public DetalleKardexValorizado()
        { }

        new public static List<DetalleKardexValorizado> Convert()
        {
            return new List<DetalleKardexValorizado>();
        }
    }
}

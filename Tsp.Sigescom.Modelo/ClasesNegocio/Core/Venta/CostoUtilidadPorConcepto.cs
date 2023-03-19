using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class CostoUtilidadPorConcepto
    {
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad
        {
            get
            { return Importe - Costo; }
        }
        public decimal PrecioUnitarioPromedio
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal CostoUnitarioPromedio
        {
            get
            {
                return Costo / Cantidad;
            }

        }
        public decimal UtilidadPromedio
        {
            get
            { return Utilidad / Cantidad; }
        }


        public CostoUtilidadPorConcepto()
        { }

        public static List<CostoUtilidadPorConcepto> Convert()
        {
            return new List<CostoUtilidadPorConcepto>();
        }
    }
}

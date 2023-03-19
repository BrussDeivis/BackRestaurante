using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CuentaPorPagar
    {
        public string proveedores { get; set; }
        public decimal current { get; set; }
        public decimal _0_7Days { get; set; }
        public decimal _8_30Days { get; set; }
        public decimal _31_59Days { get; set; }
        public decimal _60_90Days { get; set; }
        public decimal _91_180Days { get; set; }
        public decimal _181_365Days { get; set; }
        public decimal total_general {get; set;}

        public CuentaPorPagar()
        {

        }


        public CuentaPorPagar(string proveedores, decimal current,decimal _0_7days, decimal _8_30Days,
            decimal _31_59Days, decimal _60_90Days, decimal _91_180Days, decimal _181_365Days, decimal total_general)
        {
            this.proveedores = proveedores;
            this.current = current;
            this._0_7Days = _0_7days;
            this._8_30Days = _8_30Days;
            this._31_59Days = _31_59Days;
            this._60_90Days = _60_90Days;
            this._91_180Days = _91_180Days;
            this._181_365Days = _181_365Days;
            this.total_general = total_general;
        }


    }

}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleCuotaPago
    {
        public long Id { get; set; }
        public long IdOperacion { get; set; }
        public string Codigo { get; set; }
        public decimal Total { get; set; }
        public decimal Pagado { get; set; }
        public decimal Revocado { get; set; }
        public decimal Saldo { get; set; }

    }


}

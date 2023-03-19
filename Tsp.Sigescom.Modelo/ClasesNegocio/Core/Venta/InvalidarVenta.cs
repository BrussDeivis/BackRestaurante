using Tsp.Sigescom.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class InvalidarVenta
    {
        public long Id { get; set; }
        public bool EsDiferida { get; set; }
        public string Observacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public DatosPago Pago { get; set; }

        public InvalidarVenta()
        { }
    }
}
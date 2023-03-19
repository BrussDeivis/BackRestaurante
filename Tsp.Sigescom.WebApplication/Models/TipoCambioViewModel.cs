using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class TipoCambioViewModel
    {
        public string Fecha { get; set; }
        public string Dia { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }

        public TipoCambioViewModel() { }
        
        public TipoCambioViewModel(string fecha,string dia , double compra, double venta)
        {
            this.Fecha = fecha;
            this.Dia = dia;
            this.Compra = compra;
            this.Venta = venta;
        }
    }
}
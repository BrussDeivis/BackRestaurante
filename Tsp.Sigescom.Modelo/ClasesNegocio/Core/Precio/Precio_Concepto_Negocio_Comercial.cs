using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{

    public class Precio_Concepto_Negocio_Comercial
    {
        public int Id { get; set; }
        public int IdTarifa { get; set; }
        public string Tarifa { get; set; }
        public decimal Valor { get; set; }
        public string ValorString { get => Valor.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio); }
        public string Codigo { get; set; }

    }
}

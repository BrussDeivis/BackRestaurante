using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class InventarioConceptoNegocio
    {

        public int IdConceptoNegocio { get; set; }

        public DateTime Fecha { get; set; }
        public decimal CantidadPrincipal { get; set; }
        public decimal CantidadSecundaria { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal CostoTotal { get; set; }

        public string Lote { get; set; }

        public InventarioConceptoNegocio()
        {

        }

        


       
    }

}

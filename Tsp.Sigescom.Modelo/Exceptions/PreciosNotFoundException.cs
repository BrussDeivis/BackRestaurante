using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class PreciosNotFoundException : Exception
    {
        public PreciosNotFoundException() : base("No se ha encontrado precios para el concepto")
        {
        }
        public PreciosNotFoundException(string concepto) : base("No se ha encontrado precios para el concepto "+ concepto)
        {
        }
    }
}

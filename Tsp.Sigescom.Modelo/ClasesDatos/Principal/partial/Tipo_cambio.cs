using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Tipo_cambio
    {
        public string FechaString { get => fecha.ToString("dd/MM/yyyy"); }
    }
}

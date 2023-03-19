using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{

    public enum NivelStockSemaforoEnum
    {
        [Description("Contado")]
        Indeterminado = 0,
        [Description("Bajo")]
        Bajo = 1,
        [Description("Normal")]
        Normal = 2,
        [Description("Alto")]
        Alto = 3,
    }




}

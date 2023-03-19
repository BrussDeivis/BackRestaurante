using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Periodo
    {
        public DateTime FechaDesde
        {
            get { return new DateTime(Convert.ToInt32(this.anio), Convert.ToInt32(this.mes), 1); }
        }
        public DateTime FechaHasta
        {
            get
            {
                return new DateTime(Convert.ToInt32(this.anio),
                Convert.ToInt32(this.mes), DateTime.DaysInMonth(Convert.ToInt32(this.anio),
                Convert.ToInt32(this.mes))).AddDays(1).AddMilliseconds(-1);
            }
        }
    }
}

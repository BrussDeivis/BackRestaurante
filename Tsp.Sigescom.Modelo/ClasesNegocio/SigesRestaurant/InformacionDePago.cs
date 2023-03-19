using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class InformacionPago
    {
        public long IdAtencion { get; set; }
        public List<DatosVentaIntegrada> datos {get;set;}

    }
}

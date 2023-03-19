using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Deuda_Actor_Negocio
    {
        public Actor Actor { get; set; }
        public Actor_negocio ActorNegocio { get; set; }
        public decimal? TotalOrden { get; set; }
        public decimal? TotalPagoCuota { get; set; }
        public string IdTipoComprobantePredeterminado { get; set; }


        public decimal Deuda()
        {
            TotalOrden = TotalOrden != null ? TotalOrden : 0;
            TotalPagoCuota = TotalPagoCuota != null ? TotalPagoCuota : 0;
            return (decimal)TotalOrden - (decimal)TotalPagoCuota;
        }


        public int IdTipoComprobantePredeterminadoEntero
        {
            get
            {
                return Convert.ToInt32(this.IdTipoComprobantePredeterminado);
            }
        }

        public Deuda_Actor_Negocio()
        {
            //FullArtefactos;4pm raymondi 393
            //Repuestos  ;
        }
    }
}

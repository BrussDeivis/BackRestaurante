using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Concepto_Negocio_Proximo_A_Vencer_Por_Almacen
    {
        public int IdConcepto { get; set; }
        public string CodigoBarra { get; set; }
        public string NombreConcepto { get; set; }
        public string Lote { get; set; }
        public decimal Stock { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }


        public static List<Reporte_Concepto_Negocio_Proximo_A_Vencer_Por_Almacen> Convert()
        {
            return new List<Reporte_Concepto_Negocio_Proximo_A_Vencer_Por_Almacen>();
        }
    }

   

}

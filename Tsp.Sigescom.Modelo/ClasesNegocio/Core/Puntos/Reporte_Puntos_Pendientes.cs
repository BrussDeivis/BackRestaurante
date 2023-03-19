using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Puntos_Pendientes
    {
        public string NumeroDocumentoCliente { get; set; }
        public string Cliente { get; set; }
        public int PuntosPendientes { get; set; }
        public decimal ValorPuntosPendientes { get => decimal.Round((PuntosPendientes * VentasSettings.Default.ValorDeUnPuntoComoMedioDePago), 2); }

        public Reporte_Puntos_Pendientes()
        {

        }

        public static List<Reporte_Puntos_Pendientes> Convert()
        {
            return new List<Reporte_Puntos_Pendientes>();
        }
    }
}

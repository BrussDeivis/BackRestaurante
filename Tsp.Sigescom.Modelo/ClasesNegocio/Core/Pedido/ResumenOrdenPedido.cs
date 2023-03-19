using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido
{
    public class ResumenOrdenPedido
    {
        public long Id { get; set; } 
        public DateTime FechaInicio { get; set; } 
        public string FechaEmision { get=>FechaInicio.ToString("dd/MM/yyyy hh:mm:ss tt"); }
        public string TipoComprobante { get; set; }
        public string Comprobante { get;set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Cliente { get => DocumentoCliente + " | " + (Alias == null ?  NombreCliente : Alias); }
        public string Alias { get; set; }
        public string Vendedor { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
      
        public int IdEstado { get; set; } 
        public bool EstaInvalidado { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; }

        public static List<ResumenOrdenPedido> Convert()
        {
            return new List<ResumenOrdenPedido>();
        }
    }

}

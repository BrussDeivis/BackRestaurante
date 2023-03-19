using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel
{
    public class PedidosInvalidados
    {
        public long Id { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Comprobante { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Cliente { get => DocumentoCliente + " | " + (Alias == null ? NombreCliente : Alias); }
        public string Alias { get; set; }
        public string Vendedor { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaInvalidacion { get; set; }
        public string Observacion { get; set; }
        public static List<PedidosInvalidados> Convert()
        {
            return new List<PedidosInvalidados>();
        }
    }
}

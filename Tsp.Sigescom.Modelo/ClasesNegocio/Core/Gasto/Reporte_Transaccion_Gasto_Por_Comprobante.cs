using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Transaccion_Gasto_Por_Comprobante
    {
        public long IdTipoComprobante;       
        public string NombreCorteComprobante;
        public int NumeroComprobante;
        public string Serie;
        public decimal Importe;
        public string Cliente;
        public DateTime Fecha;
    }
}
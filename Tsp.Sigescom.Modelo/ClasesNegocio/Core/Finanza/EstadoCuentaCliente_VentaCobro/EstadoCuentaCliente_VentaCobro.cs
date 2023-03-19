using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class EstadoCuentaCliente_VentaCobro
    {
        public decimal IdCliente { get; set; }
        public string Cliente { get; set; }
        public ResumenEstadoCuentaCliente_VentaCobro Resumen { get; set; }
        public List<DetalleEstadoCuentaCliente_VentaCobro> Detalles { get; set; }

        public EstadoCuentaCliente_VentaCobro()
        { }

        public static List<EstadoCuentaCliente_VentaCobro> Convert()
        {
            return new List<EstadoCuentaCliente_VentaCobro>();
        }
    }
}

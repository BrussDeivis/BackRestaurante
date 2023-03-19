using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ResumenCliente
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DetalleDireccion { get; set; }
        public string UbigeoDireccion { get; set; }
        public string Direccion { get => DetalleDireccion + " , " + UbigeoDireccion; }

        public ResumenCliente() { }

    }
}

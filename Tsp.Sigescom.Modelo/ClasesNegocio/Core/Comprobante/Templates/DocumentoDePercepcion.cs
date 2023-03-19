using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class DocumentoDePercepcion : DocumentoElectronicoImpreso
    {
        public override string NombreTipo { get; set; } = "COTIZACIÓN";
        public DateTime FechaVencimiento { get; set; }


    }
}

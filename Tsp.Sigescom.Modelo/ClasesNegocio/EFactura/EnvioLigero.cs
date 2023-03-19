using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.FacturacionElectronica.Modelo
{
    public class EnvioSimplificado
    {
        public long Id { get; set; }
        public string NumeroTicket { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoEnvio { get; set; }
    }

}

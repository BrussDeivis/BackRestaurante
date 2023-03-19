using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ResumenDeTransaccionGeneral
    {

        public ResumenDeTransaccionGeneral()
        {
        }

        public long Id { get; set; }
        public string CodigoTipoOperacionSunat { get; set; }
        public string NombreTipoOperacionSunat { get; set; }

        public DateTime Fecha { get; set; }
        public DateTime FechaOperacionReferencia { get; set; }
        public string Comprobante { get; set; }
        public string ComprobanteOperacionReferencia { get; set; }
        public string Empleado { get; set; }
        public string ActorNegocioExterno { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Observacion { get; set; }
        public List<ResumenDeTransaccionGeneral> Convert()
        {
            return null;
        }
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models.FacturacionElectronica
{
    public class RegistroDocumentoViewModel
    {
        public long IdSigescom { get; set; }
        public DateTime FechaEmision { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string Estado { get; set; }
    }
}
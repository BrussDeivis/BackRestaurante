using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroIngresoYEgresoVariosViewModel
    {
        public ComboActorComercialViewModel Emisor { get; set; }
        public ComboActorComercialViewModel PagadorBeneficiario { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public string Observacion { get; set; }
        public decimal Importe { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models 
{
    public class RegistroCobroYPagoCuotaViewModel
    {
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public DateTime FechaDePago { get; set; }
        public string Observacion { get; set; }
        public int IdTipoTransaccion { get; set; }
        public int IdActorComercial { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<CuotaSaldoPagoViewModel> CuotaSaldoPago { get; set; }
        public TrazaDePagoViewModel trazaPago { get; set; }
        public RegistroCobroYPagoCuotaViewModel()
        {
        }
    }

    public class CuotaSaldoPagoViewModel
    {
        public int IdCuota { get; set; }
        public string CodigoCuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal MontoPago { get; set; }

        public CuotaSaldoPagoViewModel()
        {
        }
    }
}
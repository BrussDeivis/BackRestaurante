using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ConsolidadoViewModel
    {
        public int IdTipoTransaccion { get; set; }
        public string NombreTransaccion { get; set; }
        public int IdEntidad { get; set; }
        public string EntidadInterna { get; set; }
        public decimal Importe { get; set; }
        public bool EntradaSalida { get; set; }
        public decimal SaldoAnteterior { get; set; }
        public decimal SaldoTotal { get; set; }


        public ConsolidadoViewModel()
        { }

        public ConsolidadoViewModel(Resumen_Transaccion_Consolidado consolidado)
        {
            consolidado.Importe = consolidado.EntradaSalida == true ? consolidado.Importe * 1 : consolidado.Importe * -1;
            this.IdTipoTransaccion = consolidado.IdTipoTransaccion;
            this.NombreTransaccion = consolidado.NombreTransaccion;
            this.IdEntidad = consolidado.IdEntidad;
            this.EntidadInterna = consolidado.EntidadInterna;
            this.Importe = consolidado.Importe;
            this.EntradaSalida = consolidado.EntradaSalida;
            this.SaldoTotal = SaldoTotal + this.Importe;
        }

        public static List<ConsolidadoViewModel> Convert(List<Resumen_Transaccion_Consolidado> consolidados)
        {
            var reporteConsolidadoViewModels = new List<ConsolidadoViewModel>();
            //this.Saldo=consolidados.
            foreach (var consolidado in consolidados)
            {
                reporteConsolidadoViewModels.Add(new ConsolidadoViewModel(consolidado));
            }

            return reporteConsolidadoViewModels;
        }


    }
}
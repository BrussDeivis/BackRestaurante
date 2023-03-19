using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models//Tsp.Sigescom.WebApplication.Models.concepto
  {
    [Serializable]
  
    public class ReporteDeConceptoBasicoAlcoholViewModel
    {
        public string Fecha { get; set; }
        public int IdConceptoNegocio { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Denominacion { get; set; }
        public string CodigoTipoComprobante  { get; set; }
        public string SerieYNumeroDeComprobante { get; set; }
        public string Concentracion { get; set; }
        public string UnidadMedida { get; set; }
        public string PresentacionNumeroDeEnvases { get; set; }
        public decimal Envasado { get; set; }
        public string AGranel { get; set; }
        public string SalidaDelPais { get; set; }
        public decimal Merma { get; set; }
        public decimal Perdida { get; set; }
        public decimal Otros { get; set; }
        public string Direccion { get; set; }
        public string CodigoUbigeo { get; set; }
        public string NombreCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string LugarEntrega { get; set; }
        public string UbigeoDelLugarDeEntrega { get; set; }
        public string Obersavaciones { get; set; }

        public decimal ContenidoCalculado { get; set; }
        public string NombreUnidadMedidaCalculada { get; set; }



        public ReporteDeConceptoBasicoAlcoholViewModel()
        {
            
        }

        public ReporteDeConceptoBasicoAlcoholViewModel(Reporte_Concepto_Basico reporteConceptoBasico)
        {

            this.IdConceptoNegocio = reporteConceptoBasico.IdConceptoNegocio;
            this.IdTipoComprobante = reporteConceptoBasico.IdTipoComprobante;
            this.Fecha = reporteConceptoBasico.FechaInicio.ToString("dd/MM/yyyy");
            this.Denominacion = reporteConceptoBasico.NombreConceptoBasico;
            this.CodigoTipoComprobante = reporteConceptoBasico.CodigoTipoComprobante;
            this.SerieYNumeroDeComprobante = reporteConceptoBasico.NumeroSerieComprobante + "-"+ reporteConceptoBasico.NumeroComprobante;
            this.Concentracion = reporteConceptoBasico.Sufijo;

            this.UnidadMedida = reporteConceptoBasico.ValorUnidadMedida == "ML" ? "L" : reporteConceptoBasico.ValorUnidadMedida;
            this.ContenidoCalculado = reporteConceptoBasico.ValorUnidadMedida == "ML" ? reporteConceptoBasico.Contenido/1000  : reporteConceptoBasico.Contenido;
            this.NombreUnidadMedidaCalculada = reporteConceptoBasico.ValorUnidadMedida == "ML" ? "LITROS" : reporteConceptoBasico.NombreUnidadMedida;
            this.PresentacionNumeroDeEnvases = reporteConceptoBasico.CantidadVendida.ToString("0.00") + " " + reporteConceptoBasico.NombrePresentacion + " X " + ContenidoCalculado.ToString("0.000") + " "+ NombreUnidadMedidaCalculada;

            this.Envasado = reporteConceptoBasico.ValorUnidadMedida == "ML" ? (reporteConceptoBasico.Contenido / 1000) * reporteConceptoBasico.CantidadVendida  : reporteConceptoBasico.CantidadVendida * ContenidoCalculado;

            this.AGranel = "";
            this.SalidaDelPais = "";
            this.Merma = 0.0m;
            this.Perdida = 0.0m;
            this.Otros = 0.0m;
            this.Direccion = reporteConceptoBasico.DireccionCentroDeAtencion;
            this.CodigoUbigeo = reporteConceptoBasico.CodigoUbigeo.ToString();
            this.NombreCliente = reporteConceptoBasico.NombreCliente.Replace("|", " ");
            this.NumeroDocumentoCliente = reporteConceptoBasico.NumeroDocumentoCliente;
            this.LugarEntrega = reporteConceptoBasico.DireccionCentroDeAtencion;
            this.UbigeoDelLugarDeEntrega = reporteConceptoBasico.CodigoUbigeo.ToString();
            this.Obersavaciones = "";

        }

        public static List<ReporteDeConceptoBasicoAlcoholViewModel> ConvertirConceptosBasicosALitros(List<Reporte_Concepto_Basico> reporteConceptosBasicos)
        {
            var reporteConceptosBasicosViewModel = new List<ReporteDeConceptoBasicoAlcoholViewModel>();

            foreach (var item in reporteConceptosBasicos)
            {
                reporteConceptosBasicosViewModel.Add(new ReporteDeConceptoBasicoAlcoholViewModel(item));
            }
            return reporteConceptosBasicosViewModel;
        }


    }
}
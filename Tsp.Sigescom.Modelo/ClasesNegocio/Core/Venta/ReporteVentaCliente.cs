using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ReporteVentaCliente
    {


        public string CodigoOperacion { get; set; }
        public string FechaEmision { get; set; }
        public string FechaVencimiento { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string Serie { get; set; }
        public string NumeroInicial { get; set; }
        public string NumeroFinal { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public decimal ValorFacturadoExportacion { get; set; }
        public decimal BaseImponibleGravada { get; set; }
        public decimal ImporteTotalExonerado { get; set; }
        public decimal ImporteTotalInafecta { get; set; }
        public decimal IGV { get; set; }
        public decimal ISC { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal TipoCambio { get; set; }

        public string FechaEmisionComprobantePagoQueSeModifica { get; set; }
        public string TipoComprobantePagoQueSeModifica { get; set; }
        public string NumeroSerieComprobantePagoQueSeModifica { get; set; }
        public string NumeroComprobantePagoQueSeModifica { get; set; }

        public decimal BaseImponibleIVAP { get; set; }
        public decimal ImporteIVAP { get; set; }
        public string CuentaContable { get; set; }
        public string FormaDePago { get; set; }
        public string CodigoMedioPago { get; set; }
        public string EstadoLibroElectronico { get; set; }

        public string NumeroDocumento
        {
            get
            {
                return (string.IsNullOrEmpty(NumeroFinal) || string.IsNullOrWhiteSpace(NumeroFinal)) ? NumeroInicial : NumeroInicial + "-" + NumeroFinal;
            }
        }

        public ReporteVentaCliente()
        {

        }

        public ReporteVentaCliente(EBookVentasIngresosModel ventasIngresosModel)
        {
            CodigoOperacion = ventasIngresosModel.CUO;
            FechaEmision = ventasIngresosModel.FechaEmisionComprobantePago;
            FechaVencimiento = ventasIngresosModel.FechaVencimientoOFechaPago;
            CodigoTipoComprobante = ventasIngresosModel.TipoComprobantePagoODocumento;
            Serie = ventasIngresosModel.NumeroSerieComprobantePagoODocumento;
            NumeroInicial = ventasIngresosModel.NumeroComprobantePagoODocumento;
            NumeroFinal = ventasIngresosModel.NumeroFinal;
            TipoDocumentoIdentidad = ventasIngresosModel.TipoDocumentoIdentidadCliente;
            NumeroDocumentoIdentidad = ventasIngresosModel.NumeroDocumentoIdentidadCliente;
            RazonSocial = ventasIngresosModel.ApellidosYNombres;
            ValorFacturadoExportacion = string.IsNullOrEmpty(ventasIngresosModel.ValorFacturadoExportacion) ? 0 : decimal.Parse(ventasIngresosModel.ValorFacturadoExportacion);
            BaseImponibleGravada = (string.IsNullOrEmpty(ventasIngresosModel.BaseImponibleOperacionGravada) && string.IsNullOrEmpty(ventasIngresosModel.DescuentoBaseImponible)) ? 0 : ((string.IsNullOrEmpty(ventasIngresosModel.BaseImponibleOperacionGravada) ? 0 : decimal.Parse(ventasIngresosModel.BaseImponibleOperacionGravada)) + (string.IsNullOrEmpty(ventasIngresosModel.DescuentoBaseImponible) ? 0 : decimal.Parse(ventasIngresosModel.DescuentoBaseImponible)));
            ImporteTotalExonerado = string.IsNullOrEmpty(ventasIngresosModel.ImporteTotalOperacionExonerada) ? 0 :  decimal.Parse(ventasIngresosModel.ImporteTotalOperacionExonerada);
            ImporteTotalInafecta = string.IsNullOrEmpty(ventasIngresosModel.ImporteTotalOperacionInafecta) ? 0 :  decimal.Parse(ventasIngresosModel.ImporteTotalOperacionInafecta);
            IGV = (string.IsNullOrEmpty(ventasIngresosModel.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal) && string.IsNullOrEmpty(ventasIngresosModel.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal)) ? 0 : ((string.IsNullOrEmpty(ventasIngresosModel.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal) ? 0 : decimal.Parse(ventasIngresosModel.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal)) + (string.IsNullOrEmpty(ventasIngresosModel.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal) ? 0 : decimal.Parse(ventasIngresosModel.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal)));
            ISC = string.IsNullOrEmpty(ventasIngresosModel.ImpuestoSelectivoConsumo) ? 0 :  decimal.Parse(ventasIngresosModel.ImpuestoSelectivoConsumo);
            OtrosTributos = string.IsNullOrEmpty(ventasIngresosModel.OtrosConceptosTributosCargosNoFormanParteBaseImponible) ? 0 : decimal.Parse(ventasIngresosModel.OtrosConceptosTributosCargosNoFormanParteBaseImponible);
            ImporteTotal = string.IsNullOrEmpty(ventasIngresosModel.ImporteTotalComprobantePago) ? 0 :  decimal.Parse(ventasIngresosModel.ImporteTotalComprobantePago);
            TipoCambio = string.IsNullOrEmpty(ventasIngresosModel.TipoCambio) ? 0 :  decimal.Parse(ventasIngresosModel.TipoCambio);
            FechaEmisionComprobantePagoQueSeModifica = ventasIngresosModel.FechaEmisionComprobantePagoQueSeModifica;
            TipoComprobantePagoQueSeModifica = ventasIngresosModel.TipoComprobantePagoQueSeModifica;
            NumeroSerieComprobantePagoQueSeModifica = ventasIngresosModel.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera;
            NumeroComprobantePagoQueSeModifica = ventasIngresosModel.NumeroComprobantePagoQueSeModificaNúmeroDUA;
            BaseImponibleIVAP = string.IsNullOrEmpty(ventasIngresosModel.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado) ? 0 :  decimal.Parse(ventasIngresosModel.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado);
            ImporteIVAP = string.IsNullOrEmpty(ventasIngresosModel.ImpuestoVentasArrozPilado) ? 0 :  decimal.Parse(ventasIngresosModel.ImpuestoVentasArrozPilado);
            CuentaContable = "";
            FormaDePago = "";
            CodigoMedioPago = "";
            EstadoLibroElectronico = ventasIngresosModel.EstadoIdentificaOportunidadAnotaciónIndicación;
        }

        public static List<ReporteVentaCliente> Convert(List<EBookVentasIngresosModel> registrosVentaIngreso)
        {
            List<ReporteVentaCliente> resultado = new List<ReporteVentaCliente>();

            foreach (var registro in registrosVentaIngreso)
            {
                resultado.Add(new ReporteVentaCliente(registro));
            }
            return resultado;
        }

    }
}

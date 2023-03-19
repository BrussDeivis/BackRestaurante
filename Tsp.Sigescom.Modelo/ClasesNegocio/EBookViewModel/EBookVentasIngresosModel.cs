using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class EBookVentasIngresosModel
    {
        

        public int Correlativo
        {
            set
            {
                this.CUO = "R" + value.ToString();
                this.NumeroCorrelativo = "M" + (value).ToString("0000");
            }
        }
        /// <summary>
        /// crea un item del registro de ventas en base a una consolidacion de varios comprobantes
        /// </summary>
        /// <param name="operacionVenta"></param>
        /// <param name="periodo"></param>
        /// <param name="correlativo"></param>
        public EBookVentasIngresosModel(Transaccion_consolidada operacionVenta, Periodo periodo, int correlativo)
        {
            this.Periodo = periodo.anio + periodo.mes + "00";
            this.CUO = "R" + (correlativo).ToString();
            this.NumeroCorrelativo = "M" + (correlativo).ToString("0000");
            this.FechaEmisionComprobantePago = operacionVenta.FechaEmision.ToString("dd/MM/yyyy");
            this.FechaVencimientoOFechaPago = operacionVenta.FechaEmision.ToString("dd/MM/yyyy");
            this.TipoComprobantePagoODocumento = operacionVenta.CodigoTipoComprobante;


            this.NumeroSerieComprobantePagoODocumento = operacionVenta.Serie.PadLeft(4, '0'); ;
            this.NumeroComprobantePagoODocumento = operacionVenta.NumeroInicial.ToString();
            this.NumeroFinal = operacionVenta.NumeroFinal.ToString();

            this.TipoDocumentoIdentidadCliente = "";
            this.NumeroDocumentoIdentidadCliente = "";
            this.ApellidosYNombres = "CLIENTES VARIOS";
            this.ValorFacturadoExportacion = "";
            this.BaseImponibleOperacionGravada = operacionVenta.GravaIgv ? operacionVenta.ValorDeVenta.ToString("0.00") : "";
            this.DescuentoBaseImponible = "";
            this.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal = operacionVenta.GravaIgv ? operacionVenta.IGV.ToString("0.00") : "";
            this.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal = "";
            this.ImporteTotalOperacionExonerada = !operacionVenta.GravaIgv ? operacionVenta.Total.ToString("0.00") : "";
            this.ImporteTotalOperacionInafecta = "";
            this.ImpuestoSelectivoConsumo = "";
            this.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado = "";
            this.ImpuestoVentasArrozPilado = "";
            this.OtrosConceptosTributosCargosNoFormanParteBaseImponible = "";
            //this.ImporteTotalComprobantePago = (operacionVenta.Total + operacionVenta.Icbper).ToString("0.00");//operacionVenta.Icbper
            this.ImporteTotalComprobantePago = (operacionVenta.Total).ToString("0.00");//operacionVenta.Icbper
            this.CodigoMoneda = String.IsNullOrEmpty(operacionVenta.CodigoMoneda) ? "PEN" : operacionVenta.CodigoMoneda;
            this.TipoCambio = operacionVenta.TipoCambio.ToString("0.000");
            this.FechaEmisionComprobantePagoQueSeModifica = "";
            this.TipoComprobantePagoQueSeModifica = "";
            this.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera = "";
            this.NumeroComprobantePagoQueSeModificaNúmeroDUA = "";
            this.IdentificacionContratoColaboracionEmpresarial = "";
            this.ErrorTipo1 = "";
            this.IndicadorComprobantesPagoCanceladosMediosPago = "";
            this.EstadoIdentificaOportunidadAnotaciónIndicación = 1.ToString();
        }
        /// <summary>
        /// crea un item del registro de ventas para una operacion de forma individual
        /// </summary>
        /// <param name="operacionVenta"></param>
        /// <param name="periodo"></param>
        /// <param name="correlativo"></param>
        public EBookVentasIngresosModel(OperacionDeVenta operacionVenta, Periodo periodo, int correlativo)
        {
            this.Periodo = periodo.anio + periodo.mes + "00";
            this.CUO = "R" + (correlativo).ToString();
            this.NumeroCorrelativo = "M" + (correlativo).ToString("0000");
            this.FechaEmisionComprobantePago = operacionVenta.FechaEmision.ToString("dd/MM/yyyy");
            this.FechaVencimientoOFechaPago = operacionVenta.FechaVencimiento.ToString("dd/MM/yyyy");
            this.TipoComprobantePagoODocumento = operacionVenta.Comprobante().CodigoTipo;
            this.NumeroSerieComprobantePagoODocumento = operacionVenta.Comprobante().NumeroDeSerie;
            this.NumeroComprobantePagoODocumento = operacionVenta.Comprobante().NumeroDeComprobante.ToString();
            //todo: analizar registro en bloque de boletas de venta
            this.NumeroFinal = "";
            this.TipoDocumentoIdentidadCliente = operacionVenta.CodigoSunatTipoDocumentoIdentidadCliente; // Desde aqui modifico CERNA
            this.NumeroDocumentoIdentidadCliente = operacionVenta.NumeroDocumentoIdentidadCliente;
            this.ApellidosYNombres = operacionVenta.ApellidosYNombres == "" ? "CLIENTES VARIOS" : operacionVenta.ApellidosYNombres;
            this.ValorFacturadoExportacion = "";
            this.BaseImponibleOperacionGravada = operacionVenta.BaseImponibleOperacionGravadaConSigno != 0 && !operacionVenta.EsInvalidada ? operacionVenta.BaseImponibleOperacionGravadaConSigno.ToString("0.00") : "";
            this.DescuentoBaseImponible = operacionVenta.DescuentoBaseImponible != 0 && !operacionVenta.EsInvalidada ? operacionVenta.DescuentoBaseImponible.ToString("0.00") : "";
            this.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal = operacionVenta.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal.ToString("0.00") : "";
            this.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal = operacionVenta.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal != 0 && !operacionVenta.EsInvalidada ? operacionVenta.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal.ToString("0.00") : "";
            //todo: analizar casuistica Ley Amazonia
            this.ImporteTotalOperacionExonerada = operacionVenta.ImporteTotalOperacionExoneradaConSigno != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImporteTotalOperacionExoneradaConSigno.ToString("0.00") : "";
            this.ImporteTotalOperacionInafecta = operacionVenta.ImporteTotalOperacionInafecta != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImporteTotalOperacionInafecta.ToString("0.00") : "";
            this.ImpuestoSelectivoConsumo = (operacionVenta.ImpuestoSelectivoConsumo != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImpuestoSelectivoConsumo.ToString("0.00") : "");
            this.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado = operacionVenta.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado != 0 && !operacionVenta.EsInvalidada ? operacionVenta.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado.ToString("0.00") : "";
            this.ImpuestoVentasArrozPilado = operacionVenta.ImpuestoVentasArrozPilado != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImpuestoVentasArrozPilado.ToString("0.00") : "";
            this.OtrosConceptosTributosCargosNoFormanParteBaseImponible = operacionVenta.OtrosConceptosTributosCargosNoFormanParteBaseImponible != 0 && !operacionVenta.EsInvalidada ? operacionVenta.OtrosConceptosTributosCargosNoFormanParteBaseImponible.ToString("0.00") : "";
            this.ImporteTotalComprobantePago =
                (operacionVenta.ImporteTotalComprobantePago != 0 && !operacionVenta.EsInvalidada ? operacionVenta.ImporteTotalComprobantePago.ToString("0.00") : "");
            this.CodigoMoneda = operacionVenta.CodigoMoneda();
            this.TipoCambio = operacionVenta.TipoDeCambio.ToString("0.000");
            //todo: Comprobante Modifica
            this.FechaEmisionComprobantePagoQueSeModifica = (operacionVenta.FechaEmisionComprobantePagoQueSeModifica != null ? ((DateTime)operacionVenta.FechaEmisionComprobantePagoQueSeModifica).ToString("dd/MM/yyyy") : "");
            this.TipoComprobantePagoQueSeModifica = operacionVenta.TipoComprobantePagoQueSeModifica;
            this.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera = operacionVenta.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera;
            this.NumeroComprobantePagoQueSeModificaNúmeroDUA = operacionVenta.NumeroComprobantePagoQueSeModificaNúmeroDUA > 0 ? operacionVenta.NumeroComprobantePagoQueSeModificaNúmeroDUA.ToString() : "";
            this.IdentificacionContratoColaboracionEmpresarial = "";
            this.ErrorTipo1 = "";
            this.IndicadorComprobantesPagoCanceladosMediosPago = "";
            this.EstadoIdentificaOportunidadAnotaciónIndicación = operacionVenta.EsInvalidada ? 2.ToString() : 1.ToString();

        }

        public static List<EBookVentasIngresosModel> Convert(List<OperacionDeVenta> operacionesDeVentas, Periodo period)
        {
            List<EBookVentasIngresosModel> ebookVentasModels = new List<EBookVentasIngresosModel>();
            int correlativo = 0;
            foreach (var item in operacionesDeVentas)
            {
                ebookVentasModels.Add(new EBookVentasIngresosModel(item, period, ++correlativo));

            }
            return ebookVentasModels;
        }

        public static List<EBookVentasIngresosModel> Convert(List<OperacionDeVenta> operacionesDeVentas, Periodo period, int correlativo)
        {
            List<EBookVentasIngresosModel> ebookVentasModels = new List<EBookVentasIngresosModel>();

            foreach (var item in operacionesDeVentas)
            {
                ebookVentasModels.Add(new EBookVentasIngresosModel(item, period, ++correlativo));
            }
            return ebookVentasModels;
        }

        public static List<EBookVentasIngresosModel> Convert(List<Transaccion_consolidada> operacionesDeVentasConsolidades, Periodo period, int correlativo)
        {
            List<EBookVentasIngresosModel> ebookVentasModels = new List<EBookVentasIngresosModel>();

            foreach (var item in operacionesDeVentasConsolidades)
            {
                ebookVentasModels.Add(new EBookVentasIngresosModel(item, period, ++correlativo));
            }
            return ebookVentasModels;
        }
    }
}

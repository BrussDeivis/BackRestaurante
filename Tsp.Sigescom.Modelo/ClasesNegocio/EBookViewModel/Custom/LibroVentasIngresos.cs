using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom
{
    public class DetalleLibroVentasIngresos : LibroElectronico
    {
        public string Periodo { get; set; }
        public string CUO { get; set; }
        public string NumeroCorrelativo { get; set; }
        public string FechaEmisionComprobantePago { get; set; }
        public string FechaVencimientoOFechaPago { get; set; }
        public string TipoComprobantePagoODocumento { get; set; }
        public string NumeroSerieComprobantePagoODocumento { get; set; }
        public string NumeroComprobantePagoODocumento { get; set; }
        public string NumeroFinal { get; set; }
        public string TipoDocumentoIdentidadCliente { get; set; }
        public string NumeroDocumentoIdentidadCliente { get; set; }
        public string ApellidosYNombres { get; set; }
        public string ValorFacturadoExportacion { get; set; }
        public string BaseImponibleOperacionGravada { get; set; }
        public string ImporteTotalOperacionExonerada { get; set; }
        public string ImporteTotalOperacionInafecta { get; set; }
        public string ImpuestoSelectivoConsumo { get; set; }
        public string ImpuestoGeneralVentasYOImpuestoPromocionMunicipal { get; set; }
        public string BaseImponibleOperacionGravadaImpuestoVentasArrozPilado { get; set; }
        public string ImpuestoVentasArrozPilado { get; set; }
        public string OtrosConceptosTributosCargosNoFormanParteBaseImponible { get; set; }
        public string ImporteTotalComprobantePago { get; set; }
        public string TipoCambio { get; set; }
        public string FechaEmisionComprobantePagoQueSeModifica { get; set; }
        public string TipoComprobantePagoQueSeModifica { get; set; }
        public string NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera { get; set; }
        public string NumeroComprobantePagoQueSeModificaNúmeroDUA { get; set; }
        public string EstadoIdentificaOportunidadAnotaciónIndicación { get; set; }
        public string DescuentoBaseImponible { get; set; }
        public string DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal { get; set; }
        public string CodigoMoneda { get; set; }
        public string IdentificacionContratoColaboracionEmpresarial { get; set; }
        public string ErrorTipo1 { get; set; }
        public string IndicadorComprobantesPagoCanceladosMediosPago { get; set; }
        public string Icbper { get; set; }
        //public IEnumerable<string> Icbpers { get; set; }
        //public decimal Icbper { get => (Icbpers != null) && Icbpers.Count() > 0 ? Icbpers.Sum(i => System.Convert.ToDecimal(i)) : 0; }
        public DetalleLibroVentasIngresos()
        {
        }

        public DetalleLibroVentasIngresos(OperacionDeVenta operacionVenta, Periodo periodo, int correlativo)
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
            this.Icbper = operacionVenta.Icbper().ToString("0.00");
        }

        public static List<DetalleLibroVentasIngresos> Convert(List<OperacionDeVenta> operacionesDeVentas, Periodo periodo)
        {
            List<DetalleLibroVentasIngresos> ebookVentasIngresos = new List<DetalleLibroVentasIngresos>();
            int correlativo = 0;
            foreach (var item in operacionesDeVentas)
            {
                ebookVentasIngresos.Add(new DetalleLibroVentasIngresos(item, periodo, ++correlativo));

            }
            return ebookVentasIngresos;
        }

        public static string ContenidoFormatoPLE(List<DetalleLibroVentasIngresos> logs, string separador)
        {
            string lines = "";
            foreach (var log in logs)
            {
                lines += log.Periodo + separador
                    + log.CUO + separador
                    + log.NumeroCorrelativo + separador
                    + log.FechaEmisionComprobantePago + separador
                    + log.FechaVencimientoOFechaPago + separador + log.TipoComprobantePagoODocumento + separador
                    + log.NumeroSerieComprobantePagoODocumento + separador
                    + log.NumeroComprobantePagoODocumento + separador
                    + log.NumeroFinal + separador
                    + log.TipoDocumentoIdentidadCliente + separador
                    + log.NumeroDocumentoIdentidadCliente + separador
                    + log.ApellidosYNombres + separador
                    + log.ValorFacturadoExportacion + separador
                    + log.BaseImponibleOperacionGravada + separador
                    + log.DescuentoBaseImponible + separador
                    + log.ImpuestoGeneralVentasYOImpuestoPromocionMunicipal + separador
                    + log.DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal + separador
                    + log.ImporteTotalOperacionExonerada + separador
                    + log.ImporteTotalOperacionInafecta + separador
                    + log.ImpuestoSelectivoConsumo + separador
                    + log.BaseImponibleOperacionGravadaImpuestoVentasArrozPilado + separador
                    + log.ImpuestoVentasArrozPilado + separador
                    + log.Icbper + separador
                    + log.OtrosConceptosTributosCargosNoFormanParteBaseImponible + separador
                    + log.ImporteTotalComprobantePago + separador
                    + log.CodigoMoneda + separador
                    + log.TipoCambio + separador
                    + log.FechaEmisionComprobantePagoQueSeModifica + separador
                    + log.TipoComprobantePagoQueSeModifica + separador
                    + log.NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera + separador
                    + log.NumeroComprobantePagoQueSeModificaNúmeroDUA + separador
                    + log.IdentificacionContratoColaboracionEmpresarial + separador
                    + log.ErrorTipo1 + separador
                    + log.IndicadorComprobantesPagoCanceladosMediosPago + separador
                    + log.EstadoIdentificaOportunidadAnotaciónIndicación;
                if (separador.Equals("|")) lines += separador;
                lines += Environment.NewLine;
            }
            return lines;
        }

        public static string ContenidoTXT(List<DetalleLibroVentasIngresos> logs)
        {
            string lines = ContenidoFormatoPLE(logs, "|");
            return lines;
        }
        public static string NombreArchivoTXT(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<DetalleLibroVentasIngresos> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.txt");
        }

        public static string NombreArchivoCSV(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<DetalleLibroVentasIngresos> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.csv");
        }
        public static string CabeceraArchivoCSV
        {
            get => "Periodo;CUO;N° Correlativo;Fecha de emisión del;Fecha de Vencimiento;Tipo de Comprobante ;N° serie del comprob;N° comprobante de pa;N° final;Tipo de Documento de;Número de Documento ;Apellidos y nombres;Valor facturado de l;Base imponible de la;Descuento de la Base;Impuesto General a l;Descuento del Impues;Importe total de la ;Importe total de la ;Impuesto Selectivo a;Base imponible de la;Impuesto a las Venta;Icbper;Otros conceptos, tri;Importe total del co;Código de la Moneda ;Tipo de cambio (5);Fecha de emisión del;Tipo del comprobante;N° de serie del comp;N° del comprobante d;Identificación del C;Error tipo 1: incons;Indicador de Comprob;Estado que identific";
        }

        public static string ContenidoCSV(List<DetalleLibroVentasIngresos> logs)
        {
            string lines = "";

            lines += CabeceraArchivoCSV;
            lines += Environment.NewLine;

            foreach (var log in logs)
            {
                var existeDetailDocumentoIdentidadCliente = log.NumeroDocumentoIdentidadCliente != "";
                if (existeDetailDocumentoIdentidadCliente)
                {
                    log.NumeroDocumentoIdentidadCliente = "\t" + log.NumeroDocumentoIdentidadCliente;
                }
            }
            lines += ContenidoFormatoPLE(logs, ";");
            return lines;
        }
    }
}


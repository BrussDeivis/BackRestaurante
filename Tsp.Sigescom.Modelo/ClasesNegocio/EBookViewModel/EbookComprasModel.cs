using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class EbookComprasModel
    {
        /*
        public EbookComprasModel(OperacionDeCompra operacionCompra, Period periodo, int correlativo)
        {
            bool esComprobanteQueSeModifica = false;

            this.IdPeriodo = periodo.id;
            esComprobanteQueSeModifica = operacionCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito|| operacionCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito;
            this.Periodo = periodo.year + periodo.month+ "00";
            this.CUO = "R" + (correlativo).ToString();
            this.Correlativo = "M" + (correlativo).ToString("0000");
            this.FechaEmisiónComprobantePagoODocumento = operacionCompra.FechaEmision.ToString("dd/MM/yyyy");
            this.FechaVencimientoOFechaPago = "";
            this.AnyoEmisionDeclaracionAduaneraMercancias = operacionCompra.FechaVencimiento.ToString("dd/MM/yyyy");
            this.TipoComprobantePagoODocumento = operacionCompra.Comprobante().CodigoTipo;
            this.SerieComprobantePagoODocumento = operacionCompra.Comprobante().NumeroDeSerie;
            this.AnyoEmisionDeclaracionAduaneraMercancias = "";
            this.NumeroComprobantePago = operacionCompra.Comprobante().NumeroDeComprobante.ToString();
//todo: implementar modo resumen: numero inicial, numero final
            this.NumeroFinalComprobante = "";
            this.TipoDocumentoIdentidadProveedor = operacionCompra.Proveedor().CodigoSunatTipoDocumentoIdentidad();
            this.NumeroRUCProveedorONumeroDocumentoIdentidad = operacionCompra.Proveedor().DocumentoIdentidad;
            this.ApellidosNombresDenominacionORazonSocialProveedor = operacionCompra.Proveedor().RazonSocial;
            //GRAVADAS DESTINADAS A VENTAS GRAVADAS
            this.BaseImponibleAdquisicionesGravadasDestinadasAGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasGravadas) ?
                operacionCompra.ValorDeVenta.ToString("0.##") : "";
            this.MontoImpuestoGeneralVentasDestinadasAGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasGravadas) ? 
                operacionCompra.Igv().ToString("0.##"):"";
            //GRAVADAS DESTINADAS A VENTAS GRAVADAS Y NO GRAVADAS
            this.BaseImponibleAdquisicionesGravadasDestinadasGravadasYNoGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasGravadasYNoGravadas) ?
                operacionCompra.ValorDeVenta.ToString("0.##") : "";
            this.MontoImpuestoGeneralVentasDestinadasAGravadasYNoGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasGravadasYNoGravadas) ?
                operacionCompra.Igv().ToString("0.##") : ""; 

            // GRAVADAS DESTINADAS AVENTAS NO GRAVADAS
            this.BaseImponibleAdquisicionesGravadasDestinadasANoGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasNoGravadas) ?
                operacionCompra.ValorDeVenta.ToString("0.##") : "";
            this.MontoImpuestoGeneralVentasDestinadasANoGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.GravadaDestinadaAVentasNoGravadas) ?
                operacionCompra.Igv().ToString("0.##") : "";
            //NO GRAVADAS
            this.ValorAdquisicionesNoGravadas = (operacionCompra.TipoDeOperacionDeCompra() == TipoOperacionCompra.NoGravada) ?
                operacionCompra.ValorDeVenta.ToString("0.##") : "";

            this.MontoImpuestoSelectivoConsumo = operacionCompra.Isc().ToString("0.##");
            this.OtrosConceptosTributosCargosNoFormanParteBaseImponible = "";
            this.ImporteTotalAdquisicionesRegistradasSegunComprobantePago = operacionCompra.Total.ToString("0.##");
            this.CodigoMoneda = operacionCompra.CodigoMoneda();
            this.TipoCambio = (operacionCompra.IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaDolares ? operacionCompra.TipoDeCambio.ToString("0.###") : "");


            this.FechaEmisionComprobantePagoQueSeModifica = esComprobanteQueSeModifica ? operacionCompra.OperacionDeReferencia().FechaEmision.ToString("dd/MM/yyyy") : "";
            this.TipoComprobantePagoQueSeModifica = esComprobanteQueSeModifica ? operacionCompra.OperacionDeReferencia().Comprobante().CodigoTipo : ""; ;
            this.NumeroSerieComprobantePagoQueSeModifica = esComprobanteQueSeModifica ? operacionCompra.OperacionDeReferencia().Comprobante().NumeroDeSerie : "";
            this.CodigoDependenciaAduaneraDeclaraciónUnicaAduanasDUA = "";
            this.NumeroComprobantePagoQueSeModifica = esComprobanteQueSeModifica ? operacionCompra.OperacionDeReferencia().Comprobante().NumeroDeComprobante.ToString() : "";
            this.NumeroComprobantePagoEmitidoSujetoNoDomiciliado = "";
            this.FechaEmisionConstanciaDepositoDetraccion = "";
            this.NumeroConstanciaDepositoDetraccion = "";
            this.MarcaComprobantePagoSujetoARetencion = "";
            this.ClasificacionBienesServiciosAdquiridos = "";
            this.IdentificacionContratoProyecto = "";
            this.ErrorTipo1 = "";
            this.ErrorTipo2 = "";
            this.ErrorTipo3 = "";
            this.ErrorTipo4 = "";
            this.IndicadorComprobantesPagoCanceladosConMedioPago = "";
            this.EstadoQueIdentificaOportunidadAnotaciónOIndicacionSiCorrespondeAAjuste = 1.ToString();
        }

        public static List<EbookComprasModel> Convert(List<OperacionDeCompra> operacionesDeCompras, Period period)
        {
            List<EbookComprasModel> ebookComprasModels= new List<EbookComprasModel>();
            int correlativo = 0;
            foreach (var item in operacionesDeCompras)
            {
                ebookComprasModels.Add(new 
                    EbookComprasModel(item,period,++correlativo));
            }
            return ebookComprasModels;
        }

        */
    }
}

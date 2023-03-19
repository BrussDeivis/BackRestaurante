using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class FinanzaController : BaseController
    {
        private readonly IOperacionLogica operacionLogica;
        private readonly IContabilidadLogica contabilidadLogica;
        private readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        private readonly IMaestroLogica maestroLogica;
        private readonly IBarCodeUtil barCodeUtil;
        private readonly IMailer mailer;

        public FinanzaController()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            contabilidadLogica = Dependencia.Resolve<IContabilidadLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            mailer = Dependencia.Resolve<IMailer>();
        }

        #region GETS

        public ActionResult CobrarYPagar()
        {
            ViewBag.idDetalleMaestroMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.idDetalleMaestroEntidadBancariaNinguna = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
            ViewBag.idDetalleMaestroMedioDePagoDepositoCuenta = MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta;
            ViewBag.idDetalleMaestroMedioDePagoTransferenciaDeFondos = MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos;
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteAlmacen();
            ViewBag.fechaInicio = fechas[0];
            ViewBag.fechaFin = fechas[1];
            ViewBag.permitirGruposEnCuentasPorCobrarPagar = AplicacionSettings.Default.PermitirGruposEnCuentasPorCobrarPagar;
            ViewBag.nombreCajero = ProfileData().Empleado.NombresYApellidos;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
               Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
               Url.Action("PrintFile", "Finanza", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        public ActionResult CobrosYPagos()
        {

            ViewBag.fechaInicio = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            ViewBag.fechaFin = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.nombreCajero = ProfileData().Empleado.NombresYApellidos;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.IdTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.idComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.idRolProveedor = ActorSettings.Default.IdRolProveedor;
            ViewBag.idRolEmpleado = ActorSettings.Default.IdRolEmpleado;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            ViewBag.mascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
            ViewBag.mascaraDeVisualizacionValidacionRegistroEmpleado = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroEmpleado;
            return View();
        }


        public ActionResult ReporteCuentasCobrar()
        {
            return View();
        }

        public ActionResult ReporteCuentasPagar()
        {
            return View();
        }
        [Authorize(Roles = "Gerente,AdministradorTI")]
        public ActionResult InicializarCaja()
        {
            return View();
        }

        #endregion

        #region CUENTAS POR COBRAR / PAGAR

        public JsonResult ObtenerCuentasPorCobrarOPagar(bool porCobrar)
        {
            try
            {
                List<Cuenta_Cobrar_Pagar> resultado = porCobrar ? operacionLogica.ObtenerCuentasPorCobrar() : operacionLogica.ObtenerCuentasPorPagar();
                var jsonResult = Json(resultado, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerCuentasPorCobrarOPagarPorGrupos(bool porCobrar, bool todosLosGrupos, int?[] idsGrupos)
        {
            try
            {
                List<Cuenta_Cobrar_Pagar> resultado = porCobrar ? operacionLogica.ObtenerCuentasPorCobrarPorGrupos(todosLosGrupos, idsGrupos) : operacionLogica.ObtenerCuentasPorPagarPorGrupos(todosLosGrupos, idsGrupos);
                var jsonResult = Json(resultado, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CobrarPagarCuota(MovimientoEconomico_ movimiento)
        {
            try
            {
                OperationResult result = operacionLogica.GuardarMovimientoEconomico(movimiento,ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "Error al cobrar / pagar las cuota(s)");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al cobrar / pagar las cuota(s)", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaCobrarPagar(bool porCobrar, int idTipoTransaccion)
        {
            try
            {
                var resultados = porCobrar ? await operacionLogica.ObtenerTiposDeComprobanteParaCobrar(idTipoTransaccion, ProfileData().IdCentroDeAtencionSeleccionado) : await operacionLogica.ObtenerTiposDeComprobanteParaPagar(idTipoTransaccion, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public  JsonResult ObtenerCuotaOperacionDetalle(long idCuota)
        {
            try
            {
                CuentaPorCobrarPagar cuenta = operacionLogica.ObtenerCuentaIncluidoOperacion(idCuota);
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(cuenta.IdOperacionBase);
                var sede = ObtenerSede();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                string htmlStringA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                if (string.IsNullOrEmpty(htmlStringA4))
                {
                    MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(cuenta.IdOperacionBase + 1);
                    htmlStringA4 = CoreHtmlStringBuilder.ObtenerHtmlString(movimiento, FormatoImpresion.A4, sede, this);
                }
                return Json(new DetalleCuotaOperacionViewModel(cuenta, htmlStringA4));
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener cuota", e)), HttpStatusCode.InternalServerError);
            }
        }
        public void PrintFile(long idOperacion)
        {
            MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(idOperacion);
            var sede = ObtenerSede();
            var PDFventa = ObtenerPdfMovimiento(movimiento, sede, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto);
            PrintFilePDF file = null;
            file = new PrintFilePDF(PDFventa.Save(), movimiento.Id + ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = 1;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }
        public PdfDocument ObtenerPdfMovimiento(MovimientoEconomico movimientoEconomico, EstablecimientoComercialExtendidoConLogo sede, FormatoImpresion formato)
        {
            string result = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoEconomico, formato, sede, this);
            var Renderer = new SelectPdf.HtmlToPdf();
            //Renderer.Options.WebPageFixedSize = true;
            if (formato == FormatoImpresion._80mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.WebPageWidth = 1024;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(204, 756);
            }
            else if (formato == FormatoImpresion.A4)
            {
                Renderer.Options.PdfPageSize = PdfPageSize.A4;
            }
            else if (formato == FormatoImpresion._56mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                Renderer.Options.WebPageWidth = 661;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(159, 756);
            }
            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 0;
            Renderer.Options.MarginLeft = 0;
            Renderer.Options.MarginRight = 0;
            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;
            return Renderer.ConvertHtmlString(result);
        }
        #endregion

        #region COBROS / PAGOS

        public JsonResult ObtenerCobrosPagos(bool esCobro, string desde, string hasta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            try
            {
                List<Cobro_Pago> resultado = esCobro ? operacionLogica.ObtenerCobros(fechaDesde, fechaHasta)
                                                     : operacionLogica.ObtenerPagos(fechaDesde, fechaHasta);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerDocumentoIngresoEgreso(long idOperacion)
        {
            try
            {
                MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(idOperacion);
                var sede = ObtenerSede();
                var htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(movimiento, FormatoImpresion._80mm, sede, this);
                return Json(new { htmlString, accionInvalidar = movimiento.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerHtmlIngresoEgreso(long idOperacion, int formato)
        {
            MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(idOperacion);
            var sede = ObtenerSede();
            var htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(movimiento, (FormatoImpresion)formato, sede, this);
            return Json(htmlString);
        }

        public JsonResult EnviarCorreoElectronicoConDocumentoIngresoEgreso(long idOperacion, int formato, List<string> correosElectronicos)
        {
            try
            {
                MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(idOperacion);
                var sede = ObtenerSede();
                PdfDocument pdfVenta = ObtenerPdfIngresoEgreso(movimiento, sede, (FormatoImpresion)formato);
               
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, movimiento);
                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, movimiento);
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, new List<Attachment>() { new Attachment(new MemoryStream(pdfVenta.Save()), movimiento.Comprobante().NumeroDeSerie + " - " + movimiento.Comprobante().NumeroDeComprobante + " - " + movimiento.Tercero().RazonSocial + ".pdf", "application/pdf") });
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public ActionResult DescargarDocumentoIngresoEgreso(long idOperacion, int formato)
        {
            try
            {
                MovimientoEconomico movimiento = operacionLogica.ObtenerMovimientoEconomico(idOperacion);
                var sede = ObtenerSede();
                PdfDocument pdfVenta = ObtenerPdfIngresoEgreso(movimiento, sede, (FormatoImpresion)formato);
             
                byte[] fileBytes = pdfVenta.Save();
                string fileName = movimiento.Comprobante().NumeroDeSerie + " - " + movimiento.Comprobante().NumeroDeComprobante + " - " + movimiento.Tercero().RazonSocial + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener Registro de VEntas e Ingresos en Excel", e)), HttpStatusCode.InternalServerError);
            }
        }

        public PdfDocument ObtenerPdfIngresoEgreso(MovimientoEconomico movimiento, EstablecimientoComercialExtendidoConLogo sede, FormatoImpresion formato)
        {
            string result = CoreHtmlStringBuilder.ObtenerHtmlString(movimiento, formato, sede, this);
            var Renderer = new SelectPdf.HtmlToPdf();

            //Renderer.Options.WebPageFixedSize = true;

            if (formato == FormatoImpresion._80mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.WebPageWidth = 1024;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(204, 756);

            }
            else if (formato == FormatoImpresion.A4)
            {
                Renderer.Options.PdfPageSize = PdfPageSize.A4;
            }
            else if (formato == FormatoImpresion._56mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                Renderer.Options.WebPageWidth = 661;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(159, 756);
            }

            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 0;
            Renderer.Options.MarginLeft = 0;
            Renderer.Options.MarginRight = 0;
            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;
            return Renderer.ConvertHtmlString(result);
        }

        public JsonResult InvalidarMovimientoEconomico(long idOperacion, string observacion)
        {
            try
            {
                OperationResult result;
                result = operacionLogica.InvalidarMovimientoEconomico(idOperacion, observacion, ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INVALIDAR EL MOVIMIENTO ECONOMICO");
                return Json(new { result.code_result, result_description = result.title });
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(ex), HttpStatusCode.InternalServerError);
            }
        }
       
        #endregion

        #region INGRESOS / EGRESOS VARIOS        

        public JsonResult GuardarIngresoEgresoVarios(bool esIngreso, RegistroIngresoYEgresoVariosViewModel modelo)
        {
            try
            {
                OperationResult result = null;
                if (esIngreso)
                {
                    result = operacionLogica.GuardarIngresoVarios(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, modelo.Importe, modelo.Emisor.Id, modelo.PagadorBeneficiario.Id, modelo.TipoDeComprobante.SerieSeleccionada, modelo.Observacion);
                }
                else
                {
                    result = operacionLogica.GuardarEgresoVarios(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, modelo.Importe, modelo.Emisor.Id, modelo.PagadorBeneficiario.Id, modelo.TipoDeComprobante.SerieSeleccionada, modelo.Observacion);
                }
                Util.ManageIfResultIsNotSuccess(result, "Error al guardar el ingreso / egreso varios");
                return Json(new { codigo = result.code_result, result_description = result.title });
                
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaIngresoEgresoVarios(bool esIngreso)
        {
            try
            {
                var resultados = new List<TipoDeComprobanteParaTransaccion>();
                if (esIngreso)
                {
                    resultados =
                   ProfileData().IdCentroDeAtencionSeleccionado == ObtenerSede().Id ?
                   await operacionLogica.ObtenerTiposDeComprobanteParaIngresoVarios() :
                   await operacionLogica.ObtenerTiposDeComprobanteParaIngresoVarios(ProfileData().IdCentroDeAtencionSeleccionado);
                }
                else
                {
                    resultados = 
                   ProfileData().IdCentroDeAtencionSeleccionado == ObtenerSede().Id ?
                   await operacionLogica.ObtenerTiposDeComprobanteParaEgresoVarios() :
                   await operacionLogica .ObtenerTiposDeComprobanteParaEgresoVarios(ProfileData().IdCentroDeAtencionSeleccionado);
                }


                //esIngreso ? _logicaOperacion.ObtenerTiposDeComprobanteParaIngresoVarios() : _logicaOperacion.ObtenerTiposDeComprobanteParaEgresoVarios();
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region CUENTAS BANCARIAS
        public ActionResult CuentasBancarias()
        {
            ViewBag.idEntidadBancariaNinguna = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
            return View();
        }

        public JsonResult GuardarCuentaBancaria(CuentaBancaria cuentaBancaria)
        {
            try
            {
                OperationResult resultado;
                if (cuentaBancaria.Id != 0)
                {
                    resultado = operacionLogica.ActualizarCuentaBancaria(cuentaBancaria);
                }
                else
                {
                    resultado = operacionLogica.CrearCuentaBancaria(cuentaBancaria);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "Error al intentar guardar la cuenta bancaria");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);


            }
        }

        public JsonResult ObtenerCuentasBancarias()
        {
            try
            {
                var cuentasBancarias = operacionLogica.ObtenerCuentasBancarias();
                return Json(cuentasBancarias);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCuentasBancariasPorEntidadFinanciera(int idEntidadFinanciera)
        {
            try
            {
                var cuentasBancarias = operacionLogica.ObtenerCuentasBancariasPorEntidadFinanciera(idEntidadFinanciera);
                List<ComboGenericoViewModel> comboCuentasBancarias = ComboGenericoViewModel.Convert(cuentasBancarias);
                return Json(comboCuentasBancarias);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCuentasBancariasConEntidadFinancieraConMoneda()
        {
            try
            {
                var cuentasBancarias = operacionLogica.ObtenerCuentasBancariasConEntidadFinancieraConMoneda();
                return Json(cuentasBancarias);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region INICIALIZAR CAJA

        public JsonResult ObtenerInicializarCaja()
        {
            try
            {
                var cajasAInicializar = operacionLogica.ObtenerInicializarCaja();
                return Json(cajasAInicializar);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GuardarInicializarCaja(List<CajaInicializar> cajasAInicializar)
        {
            try
            {
                var resultado = operacionLogica.GuardarInicializarCaja(ProfileData().Empleado.Id, cajasAInicializar);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al intentar inicializar las cajas");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }



        #endregion

        #region ARQUEO DE CAJA

        public JsonResult GenerarArqueoDeCaja()
        {
            try
            {
                var resultado = operacionLogica.GenerarArqueoDeCaja(ProfileData().Empleado.Id);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al generar el arqueo de caja");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GenerarArqueoCajaAutomaticamente()
        {
            try
            {
                var resultado = operacionLogica.GenerarArqueoDeCaja(7);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al generar el arqueo de caja");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }

            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al generar el arqueo de caja", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        
    }
}
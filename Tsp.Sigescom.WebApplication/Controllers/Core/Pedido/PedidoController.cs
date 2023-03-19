using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Utilitarios;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Configuraciones;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.WebApplication.Models.Comprobante;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly IConceptoLogica conceptoLogica;
        protected readonly IMailer mailer;
        protected readonly IVentaUtilitarioLogica ventaUtil;
        protected readonly IPdfUtil pdfUtil;
        protected readonly IPedido_Logica pedidoLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IBarCodeUtil barCodeUtil;
        private readonly IConfiguracionLogica _configuracionLogica;
        public PedidoController()
        {
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            ventaUtil = Dependencia.Resolve<IVentaUtilitarioLogica>();
            pdfUtil = Dependencia.Resolve<IPdfUtil>();
            pedidoLogica = Dependencia.Resolve<IPedido_Logica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            _configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();

        }

        public ActionResult Index()
        {
            ViewBag.Data = pedidoLogica.ObetenerDatosParaPedidos(ProfileData());
            ViewBag.FechaInicio = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.FechaFin = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            var modoVenta = (int)ModoVenta.VentaNormal;
            ViewBag.TasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto;
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.IdTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.IdTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.IdTipoComprobantePedido = PedidoSettings.Default.IdTipoComprobantePedido;
            ViewBag.IdTipoComprobanteEmitirPorDefecto = PedidoSettings.Default.IdTipoComprobanteEmitirPorDefecto;
            ViewBag.ImprimirPedido = PedidoSettings.Default.ImprimirPedido;
            ViewBag.EsPregeneracionPedido = false;
            ViewBag.IdOrdenAPregenerar = 0;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Pedido", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }
        [ActionName("PregeneracionPedido")]
        public ActionResult Index(long idOrden)
        {
            ViewBag.Data = pedidoLogica.ObetenerDatosParaPedidos(ProfileData());
            ViewBag.FechaInicio = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.FechaFin = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            var modoVenta = (int)ModoVenta.VentaNormal;
            ViewBag.TasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto;
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.IdTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.IdTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.IdTipoComprobantePedido = PedidoSettings.Default.IdTipoComprobantePedido;
            ViewBag.IdTipoComprobanteEmitirPorDefecto = PedidoSettings.Default.IdTipoComprobanteEmitirPorDefecto;
            ViewBag.ImprimirPedido = PedidoSettings.Default.ImprimirPedido;
            ViewBag.EsPregeneracionPedido = true;
            ViewBag.IdOrdenAPregenerar = idOrden;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Pedido", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View("Index");
        }

        public JsonResult ObtenerPedidos(string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTime.Parse(desde);
                DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
                List<ResumenOrdenPedido> ordenPedidos = pedidoLogica.ObtenerOrdenesDePedido(fechaDesde, fechaHasta);
                return Json(ordenPedidos);
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(ex), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerParametrosDePedidos()
        {
            try
            {
                var parametrosConfiguracion = new ConfiguracionDePedido
                {
                    FechaActual = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy hh:mm:ss")
                };
                return Json(new { parametrosConfiguracion });
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(ex), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GuardarPedido(DatosVentaIntegrada pedido)
        {
            try
            {
                OperationResult result;
                PrinterBuilder printerBuilder = new PrinterBuilder();
                if (pedido.Orden.Id > 0)
                {
                    result = pedidoLogica.EditarPedido(pedido, ProfileData());
                }
                else
                {
                    result = pedidoLogica.AgregarPedido(pedido, ProfileData());
                }
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INTENTAR GUARDAR PEDIDO");
                //Armar orden de pedido para guardar para la impresion
                var ordenPedido = printerBuilder.ArmarOrdenPedidoParaImprimir((OrdenDePedido)result.objeto, pedido, ProfileData());
                //Guardar la orden de pedido en una variable de aplicacion
                printerBuilder.GuardarOrdenDePedidoParaImprimirEnAplicacion(ordenPedido);

                Util.ManageIfResultIsNotSuccess(result, "ERROR AL REGISTRAR EL PEDIDO");
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });
            }
            catch (LogicaException LogicException)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(LogicException), HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR EL PEDIDO", ex)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult InvalidarPedido(int IdOrdenPedido, string Observacion)
        {
            try
            {
                OperationResult result;
                result = pedidoLogica.InvalidarPedido(IdOrdenPedido, Observacion, ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INVALIDAR EL PEDIDO");
                return Json(new { result.code_result, result_description = result.title });
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(ex), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerPedidoParaEditar(int IdOrdenPedido)
        {
            try
            {
                var OrdenPedido = pedidoLogica.ObtenerOrdenDePedido(IdOrdenPedido);
                List<Concepto_Negocio_Comercial_> Conceptos = new List<Concepto_Negocio_Comercial_>();
                foreach (var item in OrdenPedido.Orden.Detalles)
                {

                    Conceptos.Add(conceptoLogica.ObtenerConceptoDeNegocioComercialPorIdConcepto(ProfileData(), item.Producto.Id, true, true));

                }
                return Json(new { OrdenPedido = OrdenPedido, Conceptos = Conceptos });
            }
            catch (Exception ex)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(ex), HttpStatusCode.InternalServerError);
            }
        }

        #region GenerarPDF
        public JsonResult ObtenerDocumentoDePedido(long idOrdenDePedido)
        {
            try
            {
                OrdenDePedido ordenDePedido = pedidoLogica.ObtenerOrdenDePedidoComprobante(idOrdenDePedido);
                var sede = ObtenerSede();
                var htmlString = ObtenerCadenaHtmlDePedido(ordenDePedido, (FormatoImpresion)1, null, sede);
                var respuesta = new PedidoViewModel(ordenDePedido, htmlString);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public PdfDocument ObtenerPdfPedido(OrdenDePedido ordenDePedido, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {
            string htmlString = ObtenerCadenaHtmlDePedido(ordenDePedido, formato, qrBytes, sede);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }

        public string ObtenerCadenaHtmlDePedido(OrdenDePedido ordenDeCotizacion, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendidoConLogo sede)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";

            nombreVista = formato == FormatoImpresion._80mm ? "DocumentoDePedido80" : "DocumentoDePedidoA4";
            result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new DocumentoDePedido(ordenDeCotizacion, sede, new EstablecimientoComercialExtendido(ordenDeCotizacion.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), this);

            return result;
        }

        public JsonResult ObtenerHtmlDocumentoDePedido(long idOrden, int formato)
        {
            OrdenDePedido ordenDePedido = pedidoLogica.ObtenerOrdenDePedidoComprobante(idOrden);
            var sede = ObtenerSede();
            var htmlString = ObtenerCadenaHtmlDePedido(ordenDePedido, (FormatoImpresion)formato, null, sede);
            return Json(htmlString);
        }

        public JsonResult ObtenerFormatosDeImpresion()
        {
            var valoresEnum = Enum.GetValues(typeof(FormatoImpresion));
            List<ComboGenericoViewModel> formatos = new List<ComboGenericoViewModel>();
            foreach (var item in valoresEnum)
            {
                if ((int)item != (int)FormatoImpresion._56mm)
                    formatos.Add(new ComboGenericoViewModel((int)item, ((FormatoImpresion)Convert.ToInt32(item)).ToString()));
            }
            return Json(formatos);
        }
        #endregion

        public JsonResult ObtenerTipoDeComprobante(int idTipoDeComprobante)
        {
            try
            {
                TipoDeComprobante tipoDeComprobante = _configuracionLogica.ObtenerTipoDeComprobante(idTipoDeComprobante);
                RegistroTipoComprobanteViewModel tipoDeComprobanteViewModel = new RegistroTipoComprobanteViewModel(tipoDeComprobante);
                return Json(tipoDeComprobanteViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #region Print File
        public void PrintFile(long idOperacion, int tipoOperacion)
        {
            PrinterBuilder printerBuilder = new PrinterBuilder();
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            printerBuilder.ImprimirOperacion(idOperacion, tipoOperacion, sede, this, pedidoLogica, operacionLogica, actorNegocioLogica, maestroLogica, facturacionElectronicaLogica, barCodeUtil, pdfUtil);
        }
        #endregion

        #region Confirmar Pedido    
        public JsonResult ConfirmarPedido(DatosVentaIntegrada pedido)
        {
            try
            {
                PrinterBuilder printerBuilder = new PrinterBuilder();
                OperationResult result = pedidoLogica.ConfirmarPedido(ModoOperacionEnum.PorMostrador, ProfileData(), pedido);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INTENTAR VALIDAR EL PEDIDO");
                //Armar orden de venta para guardar para la impresion
                var ordenVenta = printerBuilder.ArmarOrdenVentaParaImprimir((OrdenDeVenta)result.objeto, pedido, ProfileData());
                //Guardar la orden de venta en una variable de aplicacion
                printerBuilder.GuardarOrdenDeVentaParaImprimirEnAplicacion(ordenVenta);
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });
            }
            catch (LogicaException le)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(le), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR LA VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

    }
}
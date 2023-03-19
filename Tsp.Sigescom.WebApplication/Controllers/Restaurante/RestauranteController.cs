using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class RestauranteController : BaseController
    {
        private readonly IRestauranteLogica restauranteLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IAtencion_Logica atencionLogica;
        protected readonly ICaja_Logica cajaLogica;

        protected readonly IMaestroLogica maestroLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IPdfUtil pdfUtil;
        protected readonly IBarCodeUtil barCodeUtil;
        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;

        public RestauranteController() : base()
        {
            restauranteLogica = Dependencia.Resolve<IRestauranteLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            pdfUtil = Dependencia.Resolve<IPdfUtil>();
            atencionLogica = Dependencia.Resolve<IAtencion_Logica>();
            cajaLogica = Dependencia.Resolve<ICaja_Logica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
        }
        SesionRestaurante SesionRestaurante
        {
            get
            {
                return (SesionRestaurante)this.Session["SesionRestaurante"];
            }
            set
            {
                this.Session["SesionRestaurante"] = value;
            }
        }
        public async Task<ActionResult> Index()
        {
            SesionRestaurante = await restauranteLogica.ObtenerSesion(ProfileData());
            ViewBag.ParametrosDeConfiguracion = atencionLogica.ObtenerConfiguracionParaAtencion(SesionRestaurante);
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
               Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
               Url.Action("Imprimir", "Restaurante", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        public async Task<ActionResult> Caja()
        {
            SesionRestaurante = await restauranteLogica.ObtenerSesion(ProfileData());
            ViewBag.Configuracion = cajaLogica.ObtenerConfiguracionParaCaja(SesionRestaurante);
           
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
               Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
               Url.Action("Imprimir", "Restaurante", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);

            return View();
        }
        public async Task<ActionResult> Preparacion()
        {
            SesionRestaurante = await restauranteLogica.ObtenerSesion(ProfileData());
            ViewBag.ParametrosDeConfiguracion = ConfiguracionRestauranteOrden.Default;
            ViewBag.ParametrosDeConfiguracionAtencion = atencionLogica.ObtenerConfiguracionParaAtencion(SesionRestaurante);
            return View();
        }

        public ActionResult Complementos()
        {
            return View();
        }

        #region API RESTAURANTE
        public JsonResult ActualizarMesa(Mesa mesa)
        {
            try
            {
                OperationResult result = restauranteLogica.ActualizarMesa(mesa);
                return Json(result.data);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ActualizarAmbiente(Ambiente ambiente)
        {
            try
            {
                OperationResult result = await restauranteLogica.ActualizarAmbiente(ambiente);
                return Json(result.data);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> AgregarAmbiente(Ambiente ambiente)
        {
            try
            {
                var result = await restauranteLogica.CrearAmbiente(ambiente);
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> EliminarAmbiente(int idAmbiente)
        {
            try
            {
                OperationResult result = await restauranteLogica.EliminarAmbiente(idAmbiente);
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });

            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public JsonResult AgregarMesa(Mesa mesa)
        {
            try
            {
                var result = restauranteLogica.CrearMesa(mesa);
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });

            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public JsonResult EliminarMesa(int idMesa)
        {
            try
            {
                OperationResult result = restauranteLogica.EliminarMesa(idMesa);
                return Json(new { result.code_result, data = result.information, result_description = result.title, result.objeto });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public JsonResult ObtenerCategorias()
        {
            try
            {
                List<ItemJerarquico> categorias = restauranteLogica.ObtenerCategoriasRestaurante();
                return Json(categorias);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerIdsDeItemsPorCategoria(long IdCategoria)
        {
            try
            {
                long[] Ids = restauranteLogica.ObtenerIdsDeitemsPorCategoria(IdCategoria);
                return Json(Ids);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public async Task<JsonResult> ObtenerMesasDeAmbiente(int idAmbiente)
        {
            try
            {
                List<Mesa> mesas = await restauranteLogica.ObtenerMesasDeAmbiente(idAmbiente);
                return Json(mesas);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ObtenerTodosLosAmbientes()
        {
            try
            {
                List<Ambiente> ambientes = (await restauranteLogica.ObtenerAmbientes());
                return Json(ambientes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ObtenerAmbientes(int IdEstablecimiento)
        {
            try
            {
                List<Ambiente> ambientes = (await restauranteLogica.ObtenerAmbientes(IdEstablecimiento));
                return Json(ambientes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerCentrosAtencionRestaurante(int idEstablecimiento)
        {
            try
            {
                List<CentroAtencionRestaurante> centrosAtencion = restauranteLogica.ObtenerPuntoDeVeliveryPuntoAlPasoVigentes(idEstablecimiento);
                return Json(centrosAtencion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ObtenerItemsSuperficial()
        {
            try
            {
                List<ItemRestaurante> items = await restauranteLogica.ObtenerItemsDeRestaurante();
                return Json(items);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionDeMesa(int IdMesa)
        {
            try
            {
                AtencionRestaurante atencion = restauranteLogica.ObtenerAtencionDeMesa(IdMesa);
                return Json(atencion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ObtenerItemsDeCategoria(int IdCategoria)
        {
            try
            {
                List<ItemRestaurante> items = await restauranteLogica.ObtenerItemsDeRestaurantePorCategoria(IdCategoria);
                return Json(items);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> ObtenerMozos()
        {
            try
            {
                var mozos = await restauranteLogica.ObtenerMozosVigentes();
                return Json(mozos);
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener los mozos", e);
            }
        }
        #endregion

        #region Metodos de Atencion de restaurante

        
        public JsonResult CrearAtencionConOrden(AtencionRestaurante atencion)
        {
            try
            {
                OperationResult result = restauranteLogica.CrearAtencionConOrden(atencion, SesionRestaurante);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
       
        public JsonResult FinalizarAtencionCocina(long IdAtencion)
        {
            try
            {
                OperationResult result = restauranteLogica.FinalizarAtencionCocina(IdAtencion);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CerrarAtencion(long Id)
        {
            try
            {
                OperationResult result = atencionLogica.CerrarAtencionYSusOrdenesAtendiendoDetalles(Id, SesionRestaurante.SesionDeUsuario.Empleado.Id);
                return Json(new { result.code_result, data = result.information, result_description = result.title, result.objeto });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public JsonResult ObtenerAtencionesCerradas(string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(desde);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(hasta);
                List<ResumenAtencion> resumenes = restauranteLogica.ObtenerResumenAtencionesCerradas(fechaDesde, fechaHasta, SesionRestaurante);
                var jsonResult = Json(resumenes, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER RESUMENES DE VENTAS", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionesConfirmadas()
        {
            try
            {
                List<AtencionRestaurante> atenciones = restauranteLogica.ObtenerAtencionesConfirmadas().ToList();
                return Json(atenciones);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionesSinMesa(int idCentroAtencion)
        {
            try
            {
                List<AtencionSinMesa> atenciones = restauranteLogica.ObtenerAtencionesSinMesa(idCentroAtencion).ToList();
                return Json(atenciones);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionEspecifica(long id)
        {
            try
            {
                AtencionRestaurante atencion = restauranteLogica.ObtenerAtencionEspecifica(id);
                return Json(atencion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        /// <summary>
        /// Metodo para facturar la atencion de restarurante (creacion de orden, pago, movimiento de almacen) de acuerdo al tipo de pago (simple, dividida simple, dividida diferenciada)
        /// </summary>
        /// <param name="atencion"></param>
        /// <returns></returns>
        public JsonResult FacturarAtencion(AtencionRestaurante atencion)
        {
            try
            {
                var result = restauranteLogica.ConfirmarFacturacion(atencion, SesionRestaurante);
                OperationResult resultado = GenerarDocumentosAtencion(atencion.Id, (Dictionary<long, long>)result.objeto);
                return Json(new { resultado.code_result, resultado.information, result_description = resultado.title, resultado.objeto });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Metodo para confirmar el pago de la atencion de restaurante
        /// </summary>
        /// <param name="atencion"></param>
        /// <returns></returns>
        public JsonResult ConfirmarPagoAtencion(long idAtencion)
        {
            try
            {
                OperationResult result = restauranteLogica.ConfirmarPagoAtencion(idAtencion, SesionRestaurante);
                return Json(result);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public OperationResult GenerarDocumentosAtencion(long idAtencion, Dictionary<long, long> diccionarioIdOrdenVenta)
        {
            try
            {
                OperationResult resultado = new OperationResult();
                List<DocumentoComprobanteOrdenVenta> documentos = new List<DocumentoComprobanteOrdenVenta>();
                var sede = ObtenerSede();

                foreach (var item in diccionarioIdOrdenVenta)
                {
                    OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(item.Value);
                    string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                    byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                    var htmlString80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytes, sede, this, maestroLogica);
                    var htmlStringA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                    documentos.Add(new DocumentoComprobanteOrdenVenta()
                    {
                        Id = item.Key,
                        IdOrden = item.Value,
                        CadenaHtmlDeComprobante80 = htmlString80,
                        CadenaHtmlDeComprobanteA4 = htmlStringA4
                    });
                }
                var comprobanteAtencion = restauranteLogica.ObtenerComprobanteCuentaAtencion(idAtencion);
                string htmlString =HtmlStringBuilder.RenderRazorViewToString("../Restaurante/Comprobantes/ComprobanteCuentaAtencion80mm", comprobanteAtencion, this);
                resultado.title = OperationResultSettings.Default.OperationResultSuccessDescription;
                resultado.objeto = htmlString;
                resultado.information = documentos;
                return resultado;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar generar los documentos de atencion", e);
            }
        }

        public JsonResult ObtenerDocumentosAtencion(long idAtencion)
        {
            try
            {
                var diccionarioIdOrdenVenta = restauranteLogica.ObtenerIdsOrdenDeVentaDeAtencion(idAtencion);
                var resultado = GenerarDocumentosAtencion(idAtencion,diccionarioIdOrdenVenta);
                return Json(new { resultado.code_result, resultado.information, result_description = resultado.title, resultado.objeto });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }


        #endregion

        #region Metodos de Orden de restaurante
        public JsonResult AgregarOrdenDeAtencion(Orden_Atencion Orden)
        {
            try
            {
                var result = restauranteLogica.AgregarOrdenDeAtencion(Orden, SesionRestaurante);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarEstadoDeOrden(long IdOrden, int Estado)
        {
            try
            {
                OperationResult result = restauranteLogica.CambiarEstadoDeOrden(IdOrden, Estado);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarEstadoDeOrdenes(long[] Ids, int Estado)
        {
            try
            {
                OperationResult result = restauranteLogica.CambiarEstadoDeOrdenes(Ids, Estado);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerOrdenesPorEstado(int NumEstado)
        {
            try
            {
                List<Orden_Atencion> ordenes = new List<Orden_Atencion>();
                ordenes = restauranteLogica.ObtenerOrdenesPorEstado(NumEstado);
                return Json(ordenes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerOrdenesConfirmadas()
        {
            try
            {
                List<Orden_Atencion> ordenes = new List<Orden_Atencion>();
                ordenes = restauranteLogica.ObtenerOrdenesPorEstado(MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
                return Json(ordenes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerOrdenesPorEstadoDesdeUnAmbiente(int NumEstado, int IdAmbiente)
        {
            try
            {
                List<Orden_Atencion> ordenes = new List<Orden_Atencion>();

                ordenes = restauranteLogica.ObtenerOrdenesPorEstadoDesdeUnAmbiente(NumEstado, IdAmbiente);

                return Json(ordenes);

            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CerrarOrden(long idOrden)
        {
            try
            {
                OperationResult result = restauranteLogica.CerrarOrden(idOrden);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }


        #endregion


        public ActionResult ObtenerOrdenPDF(long idOrden)
        {
            var sede = ObtenerSede();
            var comprobanteOrden = restauranteLogica.ObtenerComprobanteOrdenSinItemsAnulados(idOrden);
            var PDForden = ObtenerPdfOrden(comprobanteOrden, sede, FormatoImpresion._80mm);
            PDForden.ViewerPreferences.FitWindow = false;
            return base.File(PDForden.Save(), "application/pdf");
        }

        public ActionResult ObtenerAtencionPDF(long idAtencion)
        {
            var sede = ObtenerSede();
            var comprobanteAtencion = restauranteLogica.ObtenerComprobanteCuentaAtencion(idAtencion);
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(comprobanteAtencion, FormatoImpresion._80mm, sede, this);

            
            var documentoPdf = pdfUtil.ObtenerPdfDocumento(htmlString, FormatoImpresion._80mm);
            documentoPdf.ViewerPreferences.FitWindow = false;
            return base.File(documentoPdf.Save(), "application/pdf");
        }

        public string ObtenerAtencionPDFString(long idAtencion)
        {
            var sede = ObtenerSede();
            var comprobanteAtencion = restauranteLogica.ObtenerComprobanteCuentaAtencion(idAtencion);
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(comprobanteAtencion, FormatoImpresion._80mm, sede, this);

            var documentoPdf = pdfUtil.ObtenerPdfDocumento(htmlString, FormatoImpresion._80mm);
            byte[] fileBytes = documentoPdf.Save();
            return Convert.ToBase64String(fileBytes);
        }

        public ActionResult ObtenerOrdenHtml(long idOrden)
        {

            var sede = ObtenerSede();
            var comprobanteOrden = restauranteLogica.ObtenerComprobanteOrdenSinItemsAnulados(idOrden);
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(comprobanteOrden, FormatoImpresion._80mm, sede, this);
            return Json(htmlString);
        }

        public ActionResult ObtenerAtencionHtml(long idAtencion)
        {
            var sede = ObtenerSede();
            var comprobanteAtencion = restauranteLogica.ObtenerComprobanteCuentaAtencion(idAtencion);
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(comprobanteAtencion, FormatoImpresion._80mm, sede, this);
            return Json(htmlString);
        }


        ///
        public void Imprimir(int tipoDocumento, int idDocumento)
        {
            switch (tipoDocumento)
            {
                case 1:ImprimirOrden(idDocumento);break;
                case 2: ImprimirCuenta(idDocumento); break;
                default:
                    break;
            }
        }
        public PdfDocument ObtenerPdfOrden(ComprobanteOrden orden, EstablecimientoComercialExtendidoConLogo sede, FormatoImpresion formato)
        {
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(orden, formato, sede, this);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }
        public void ImprimirOrden(long idOrden)
        {
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            var orden = restauranteLogica.ObtenerComprobanteOrdenSinItemsAnulados(idOrden);
            var PDForden = ObtenerPdfOrden(orden, sede, FormatoImpresion._80mm);
            PrintFilePDF file = null;
            file = new PrintFilePDF(PDForden.Save(), orden.Orden.Codigo + ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = 1;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }
        public PdfDocument ObtenerPdfCuenta(ComprobanteCuentaAtencion cuenta, EstablecimientoComercialExtendidoConLogo sede, FormatoImpresion formato)
        {
            string htmlString = RestaurantHtmlStringBuilder.ObtenerHtmlString(cuenta, formato, sede, this);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }


        public void ImprimirCuenta(long idAtencion)
        {
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            var cuenta = restauranteLogica.ObtenerComprobanteCuentaAtencion(idAtencion);
            var PDFcuenta = ObtenerPdfCuenta(cuenta, sede, FormatoImpresion._80mm);
            PrintFilePDF file = null;
            file = new PrintFilePDF(PDFcuenta.Save(), cuenta.Atencion.Id + ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = 1;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }
       

        #region Cambiar Estado Detalle de Orden
        public JsonResult AtenderDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.AtenderDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AnularDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.AnularDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult DevolverDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.DevolverDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ServirDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ServirDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult PrepararDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.PrepararDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObservarDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ObservarDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ReanudarDetalleDeOrden(int id)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ReanudarDetalleDeOrden(id);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ReanudarTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ReanudarTodosLosDetallesDeOrden(idOrden);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AtenderTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.AtenderTodosLosDetallesDeOrden(idOrden);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult PrepararDetallesDeOrdenes(long[] idsDetallesDeOrdenes)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.PrepararDetallesDeOrdenes(idsDetallesDeOrdenes);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ServirDetallesDeOrdenes(long[] idsDetallesDeOrdenes)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ServirDetallesDeOrdenes(idsDetallesDeOrdenes);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AnularTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.AnularTodosLosDetallesDeOrden(idOrden);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion
        public JsonResult ObtenerConfiguracionRestauranteFacturacion()
        {
            try
            {
                return Json(new { data = ConfiguracionRestauranteFacturacion.Default });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ActualizarImportesDeTransaccion(List<ItemGenerico> NuevosImportes)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = restauranteLogica.ActualizarImportesDeTransaccion(NuevosImportes);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerItemDeRestauranteIncluyendoComplementosDeFamilia(int id)
        {
            try
            {
                ItemRestaurante item = restauranteLogica.ObtenerItemDeRestauranteIncluyendoComplementosDeFamilia(id);
                return Json(item);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ActualizarJsonDetalleDeDetalleOrden(long idDetalle, string jsonString)
        {
            try
            {
                OperationResult result = restauranteLogica.ActualizarJsonDetalleDeDetalleOrden(idDetalle,jsonString);
                return Json(result.data);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        #region COMPLEMENTO

        public JsonResult ObtenerComplementos()
        {
            try
            {
                List<Complemento> complementos = restauranteLogica.ObtenerComplementos().ToList();
                return Json(complementos);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener complementos", e);
            }
        }

        public JsonResult ActualizarComplemento(Complemento complemento)
        {
            try
            {
                OperationResult result = restauranteLogica.ActualizarComplemento(complemento);
                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar complemento", e);
            }
        }

        #endregion
    }
}
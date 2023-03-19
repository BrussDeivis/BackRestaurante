using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class AlmacenController : BaseController
    {
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly ISede_Logica sedeLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IInventarioHistorico_Logica inventarioHistoricoLogica;
        protected readonly IConfiguracionLogica configuracionLogica;
        protected readonly IGeneracionArchivosLogica generacionArchivosLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IMailer mailer;
        protected readonly IBarCodeUtil barCodeUtil;

        public AlmacenController() : base()
        {
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            generacionArchivosLogica = Dependencia.Resolve<IGeneracionArchivosLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            inventarioHistoricoLogica = Dependencia.Resolve<IInventarioHistorico_Logica>();

        }

        #region GETS
        public ActionResult OrdenesDeAlmacen()
        {
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteAlmacen();
            ViewBag.fechaInicio = fechas[0];
            ViewBag.fechaFin = fechas[1];
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.IdTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : " ";
            ViewBag.idUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : 0;
            ViewBag.idModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
            ViewBag.idMotivoTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
            ViewBag.idTransportistaPorDefecto = AplicacionSettings.Default.IdTransportistaPorDefectoEnSalidaDeMercaderia;
            ViewBag.idTipoDeComprobantePorDefecto = AplicacionSettings.Default.IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia;
            ViewBag.WCPScript = WebClientPrint.CreateScript(
            Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
            Url.Action("PrintFile", "Almacen", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        public ActionResult TrasladosInternos()
        {
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteAlmacen();
            ViewBag.fechaInicio = fechas[0];
            ViewBag.fechaFin = fechas[1];
            ViewBag.NombreEmpleadoDeSesion = ProfileData().Empleado.NombresYApellidos;
            ViewBag.NombreCentroDeAtencion = ProfileData().CentroDeAtencionSeleccionado.Nombre;
            ViewBag.idCentroDeAtencionSeleccionado = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.mostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnTrasladoMercaderia;
            ViewBag.modoDeSeleccionDeConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnTrasladoMercaderia;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnTrasladoMercaderia;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.salidaBienesSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.informacionSelectorConcepto = (int)InformacionSelectorConcepto.Nombre;

            ViewBag.WCPScript = WebClientPrint.CreateScript(
            Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
            Url.Action("PrintFile", "Almacen", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        public ActionResult MovimientosDeAlmacen()
        {
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteAlmacen();
            ViewBag.FechaInicio = fechas[0];
            ViewBag.FechaFin = fechas[1];
            ViewBag.IdEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.IdCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.TieneRolAdministradorDeNegocio = ProfileData().Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);

            ViewBag.WCPScript = WebClientPrint.CreateScript(
            Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
            Url.Action("PrintFile", "Almacen", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }


        public ActionResult InventarioHistorico()
        {
            return View();
        }
        [Authorize(Roles = "Almacenero,AdministradorNegocio,Gerente")]

        #endregion

        #region ORDENES DE ALMACEN
        public JsonResult ObtenerOrdenesDeAlmacen(bool porRecibir, int[] idsCentrosDeAtencion, string desde, string hasta)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            try
            {
                List<Orden_Recibir_Entregar> resultado = porRecibir ? operacionLogica.ObtenerOrdenesDeAlmacenPorRecibir(ProfileData().Empleado.Id, idsCentrosDeAtencion.ToList(), fechaDesde, fechaHasta) : operacionLogica.ObtenerOrdenesDeAlmacenPorEntregar(ProfileData().Empleado.Id, idsCentrosDeAtencion.ToList(), fechaDesde, fechaHasta);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerOrdenDeAlmacen(long idOrdenDeAlmacen, int formato)
        {
            try
            {
                OrdenDeAlmacenViewModel respuesta = new OrdenDeAlmacenViewModel();
                OrdenDeMovimientoDeAlmacen ordenDeMovimientoDeAlmacen = operacionLogica.ObtenerOrdenDeMovimientoDeAlmacen(idOrdenDeAlmacen);
                var sede = ObtenerSede();
                string htmlStringDeOrdenDeAlmacen = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeMovimientoDeAlmacen, (FormatoImpresion)formato, null, sede, this);

                respuesta = new OrdenDeAlmacenViewModel(ordenDeMovimientoDeAlmacen.Id, htmlStringDeOrdenDeAlmacen);

                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerOrdenDeAlmacenParaMovimientoDeAlmacen(long idOrdenDeAlmacen)
        {
            try
            {
                OrdenDeMovimientoDeAlmacen ordenDeAlmacen = operacionLogica.ObtenerOrdenDeMovimientoDeAlmacen(idOrdenDeAlmacen);
                OrdenDeAlmacenParaMovimientoDeAlmacenViewModel ordenDeAlmacenViewModel = new OrdenDeAlmacenParaMovimientoDeAlmacenViewModel(ordenDeAlmacen);
                return Json(ordenDeAlmacenViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerStockDeProducto(int idProducto)
        {
            try
            {
                return Json(operacionLogica.ObtenerStockDeProducto(idProducto, ProfileData().IdCentroAtencionQueTieneElStockIntegrada));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        //public JsonResult GuardarMovimientoDeAlmacen(RegistroMovimientoDeAlmacenViewModel movimientoDeAlmacen, bool esIngresoMercaderia)
        //{
        //    try
        //    {
        //        OperationResult result;
        //        //Convertimos los detalles
        //        List<Detalle_transaccion> detalles = ConstruirDetalleMovimientoMercaderia(movimientoDeAlmacen);
        //        //Confimamos el movimiento de la mercaderia  
        //        result = operacionLogica.GuardarMovimientoDeAlmacen(movimientoDeAlmacen.IdOrdenDeAlmacen, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, movimientoDeAlmacen.TipoDeComprobante.EsPropio, movimientoDeAlmacen.TipoDeComprobante.TipoComprobante == null ? 0 : movimientoDeAlmacen.TipoDeComprobante.TipoComprobante.Id, movimientoDeAlmacen.TipoDeComprobante.SerieSeleccionada, movimientoDeAlmacen.TipoDeComprobante.SerieIngresada, movimientoDeAlmacen.TipoDeComprobante.NumeroIngresado, movimientoDeAlmacen.FechaInicioTraslado, movimientoDeAlmacen.Transporte.Transportista == null ? 0 : movimientoDeAlmacen.Transporte.Transportista.Id, movimientoDeAlmacen.Transporte.MarcaYPlaca, movimientoDeAlmacen.Transporte.NumeroLicencia, movimientoDeAlmacen.ModalidadTransporte == null ? 0 : movimientoDeAlmacen.ModalidadTransporte.Id, movimientoDeAlmacen.MotivoTraslado == null ? 0 : movimientoDeAlmacen.MotivoTraslado.Id, movimientoDeAlmacen.DireccionOrigen, movimientoDeAlmacen.UbigeoOrigen == null ? 0 : movimientoDeAlmacen.UbigeoOrigen.Id, movimientoDeAlmacen.DireccionDestino, movimientoDeAlmacen.UbigeoDestino == null ? 0 : movimientoDeAlmacen.UbigeoDestino.Id, movimientoDeAlmacen.DocumentoReferencia, movimientoDeAlmacen.Observacion, detalles, movimientoDeAlmacen.EsTrasladoTotal, ProfileData());
        //        //Verificamos el resultado
        //        Util.ManageIfResultIsNotSuccess(result, "");
        //        return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title }, HttpStatusCode.OK);
        //    }
        //    catch (LogicaException oe)
        //    {
        //        return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR EL MOVIMIENTO", e)), HttpStatusCode.InternalServerError);
        //    }
        //}

        public List<Detalle_transaccion> ConstruirDetalleMovimientoMercaderia(RegistroMovimientoDeAlmacenViewModel ingresoMercaderia)
        {
            List<Detalle_transaccion> detallesConstruidos = new List<Detalle_transaccion>();
            foreach (var item in ingresoMercaderia.Detalles)
            {
                detallesConstruidos.Add(new Detalle_transaccion(item.IngresoSalidaActual, item.IdProducto, "", 1, 1, null, 0, null, null, 0, 0, 0, item.Lote, null, null));
            }
            return detallesConstruidos;
        }

        [AllowAnonymous]
        public async Task PrintFile(long idMovimiento)
        {
            MovimientoDeAlmacen movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
            var sede = ObtenerSede();
            var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
            var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
            var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
            byte[] QrBytes = (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent);
            var PDFventa = ObtenerPdfMovimientoDeAlmacen(movimientoDeAlmacen, sede, QrBytes, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto, proveedores, modalidadesDeTraslado, motivosDeTraslado);
            PrintFilePDF file = null;

            file = new PrintFilePDF(PDFventa.Save(), idMovimiento + ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();

            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        public PdfDocument ObtenerPdfOrdenDeAlmacen(OrdenDeMovimientoDeAlmacen ordenDeAlmacen, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {
            string result = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeAlmacen, formato, qrBytes, sede, this);
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

        public JsonResult EnviarCorreoElectronicoDeOrdenDeAlmacen(long idOrden, int formato, List<string> correosElectronicos)
        {
            try
            {
                OrdenDeMovimientoDeAlmacen ordenDeAlmacen = operacionLogica.ObtenerOrdenDeMovimientoDeAlmacen(idOrden);
                var sede = ObtenerSede();
                PdfDocument pdfMovimiento = ObtenerPdfOrdenDeAlmacen(ordenDeAlmacen, sede, null, (FormatoImpresion)formato);
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, ordenDeAlmacen);
                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, ordenDeAlmacen);
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, new List<Attachment>() { new Attachment(new MemoryStream(pdfMovimiento.Save()), ordenDeAlmacen.Comprobante().NumeroDeSerie + " - " + ordenDeAlmacen.Comprobante().NumeroDeComprobante + " - " + ordenDeAlmacen.Tercero().RazonSocial + ".pdf", "application/pdf") });
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public ActionResult DescargarOrdenDeAlmacen(long idOrden, int formato)
        {
            try
            {
                OrdenDeMovimientoDeAlmacen ordenDeAlmacen = operacionLogica.ObtenerOrdenDeMovimientoDeAlmacen(idOrden);
                var sede = ObtenerSede();
                PdfDocument pdfMovimiento = ObtenerPdfOrdenDeAlmacen(ordenDeAlmacen, sede, null, (FormatoImpresion)formato);
                byte[] fileBytes = pdfMovimiento.Save();
                string fileName = ordenDeAlmacen.Comprobante().NumeroDeSerie + " - " + ordenDeAlmacen.Comprobante().NumeroDeComprobante + " - " + ordenDeAlmacen.Tercero().RazonSocial + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener Registro de VEntas e Ingresos en Excel", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerDetalleDeCompraParaOrdenDeAlmacen(long idOrdenDeCompra)
        {
            try
            {
                OrdenDeCompra ordenDeCompra = operacionLogica.ObtenerOrdenDeCompra(idOrdenDeCompra);
                List<DetalleOrdenDeAlmacenViewModel> detalles = DetalleOrdenDeAlmacenViewModel.Convertir(ordenDeCompra.Detalles(), ordenDeCompra.ObtenerOrdenesDeAlmacen());
                return Json(detalles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarOrdenDeAlmacen(RegistroOrdenDeAlmacenViewModel ordenDeAlmacen)
        {
            try
            {
                OperationResult result;
                //Convertimos los detalles
                List<DetalleDeOperacion> detalles = ConstruirDetalleOrdenDeAlmacen(ordenDeAlmacen);
                //Confimamos el movimiento de la mercaderia  
                result = operacionLogica.GuardarOrdenDeAlmacen(ordenDeAlmacen.IdOrdenDeOperacion, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ordenDeAlmacen.TipoDeComprobante.SerieSeleccionada, ordenDeAlmacen.CentroDeAtencion.Id, ordenDeAlmacen.FechaTraslado, ordenDeAlmacen.Observacion, detalles, ordenDeAlmacen.EsGeneracionTotal);
                //Verificamos el resultado
                Util.ManageIfResultIsNotSuccess(result, "");
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR EL ORDEN DE ALMACEN", e)), HttpStatusCode.InternalServerError);
            }
        }

        public List<DetalleDeOperacion> ConstruirDetalleOrdenDeAlmacen(RegistroOrdenDeAlmacenViewModel ordenDeAlmacen)
        {
            List<DetalleDeOperacion> detallesConstruidos = new List<DetalleDeOperacion>();
            foreach (var item in ordenDeAlmacen.Detalles)
            {
                detallesConstruidos.Add(new DetalleDeOperacion(item.IdProducto, item.IngresoSalidaActual, 1, 1, 0, 0, 0, null, null, null, null, item.EsBien, null, null));
            }
            return detallesConstruidos;
        }

        #endregion

        #region ENTRADA Y SALIDA DE MERCADERIA
        public JsonResult ObtenerMovimientosDeAlmacen(bool esEntrada, int[] idsCentrosDeAtencion, string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);

                List<Entrada_Salida_Almacen> resultado = esEntrada ? operacionLogica.ObtenerEntradasDeAlmacen(ProfileData().Empleado.Id, idsCentrosDeAtencion.ToList(), fechaDesde, fechaHasta) : operacionLogica.ObtenerSalidasDeAlmacen(ProfileData().Empleado.Id, idsCentrosDeAtencion.ToList(), fechaDesde, fechaHasta);

                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerMovimientoDeAlmacen(long idMovimiento, int formato)
        {
            try
            {
                MovimientoDeAlmacen movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
                MovimientoDeAlmacenViewModel respuesta = new MovimientoDeAlmacenViewModel();
                var sede = ObtenerSede();
                string htmlStringMovimiento = "";
                string htmlStringOrden = "";
                bool comprobanteMovimiento = false;

                if (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna || movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
                {
                    var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                    var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                    var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                    var QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                    var QrBytes = (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent);
                    comprobanteMovimiento = true;
                    htmlStringMovimiento = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, (FormatoImpresion)formato, QrBytes, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, this);
                }
                else
                {
                    comprobanteMovimiento = false;
                    htmlStringMovimiento = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, (FormatoImpresion)formato, null, sede, null, null, null, this);
                }

                if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(movimientoDeAlmacen.OrdenDeOperacion().TipoTransaccion().Id))
                {
                    OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(movimientoDeAlmacen.IdOperacionReferencia());
                    var QrContentOrden = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                    var QrBytesOrden = barCodeUtil.ObtenerCodigoQR(QrContentOrden);
                    htmlStringOrden = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, (FormatoImpresion)formato, QrBytesOrden, sede, this, maestroLogica);
                }
                else if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras.Contains(movimientoDeAlmacen.OrdenDeOperacion().TipoTransaccion().Id))
                {
                    OrdenDeCompra ordenDecompra = operacionLogica.ObtenerOrdenDeCompra(movimientoDeAlmacen.IdOperacionReferencia());
                    htmlStringOrden = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDecompra, (FormatoImpresion)formato, null, sede, this, maestroLogica);
                }
                //else if (TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen == movimientoDeAlmacen.OrdenDeOperacion().TipoTransaccion().Id)
                //{
                //    OrdenDeMovimientoDeAlmacen ordenDeMovimiento = operacionLogica.ObtenerOrdenDeMovimientoDeAlmacen(movimientoDeAlmacen.IdOperacionReferencia());
                //    var QrContentOrden = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                //    var QrBytesOrden = barCodeUtil.ObtenerCodigoQR(QrContentOrden);
                //    htmlStringOrden = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeMovimiento, (FormatoImpresion)formato, QrBytesOrden, sede, this);
                //}

                if (htmlStringOrden == "")
                    htmlStringOrden = htmlStringMovimiento;
                if (!comprobanteMovimiento)
                    htmlStringMovimiento = htmlStringOrden;

                //htmlStringMovimiento = htmlStringMovimiento == "" ? htmlStringOrden : htmlStringMovimiento;
                //htmlStringOrden = htmlStringOrden == "" ? htmlStringMovimiento : htmlStringOrden;
                respuesta = new MovimientoDeAlmacenViewModel(movimientoDeAlmacen.Id, htmlStringMovimiento, htmlStringOrden);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public PdfDocument ObtenerPdfMovimientoDeAlmacen(MovimientoDeAlmacen movimientoDeAlmacen, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato, List<Proveedor> proveedores, List<Detalle_maestro> modalidadesDeTraslado, List<Detalle_maestro> motivosDeTraslado)
        {
            string result = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, formato, qrBytes, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, this);
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

        public async Task<JsonResult> EnviarCorreoElectronicoDeMovimientoDeAlmacen(long idMovimiento, int formato, List<string> correosElectronicos)
        {
            try
            {
                MovimientoDeAlmacen movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
                var sede = ObtenerSede();
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                byte[] QrBytes = (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent);
                PdfDocument pdfMovimiento = ObtenerPdfMovimientoDeAlmacen(movimientoDeAlmacen, sede, QrBytes, (FormatoImpresion)formato, proveedores, modalidadesDeTraslado, motivosDeTraslado);
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, movimientoDeAlmacen);
                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, movimientoDeAlmacen);
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, new List<Attachment>() { new Attachment(new MemoryStream(pdfMovimiento.Save()), movimientoDeAlmacen.Comprobante().NumeroDeSerie + " - " + movimientoDeAlmacen.Comprobante().NumeroDeComprobante + " - " + movimientoDeAlmacen.Tercero().RazonSocial + ".pdf", "application/pdf") });
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public async Task<ActionResult> DescargarMovimientoDeAlmacen(long idMovimiento, int formato)
        {
            try
            {
                PdfDocument pdfMovimiento;
                var sede = ObtenerSede();
                MovimientoDeAlmacen movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                byte[] QrBytes = (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent);
                pdfMovimiento = ObtenerPdfMovimientoDeAlmacen(movimientoDeAlmacen, sede, QrBytes, (FormatoImpresion)formato, proveedores, modalidadesDeTraslado, motivosDeTraslado);
                byte[] fileBytes = pdfMovimiento.Save();
                string fileName = movimientoDeAlmacen.Comprobante().NumeroDeSerie + " - " + movimientoDeAlmacen.Comprobante().NumeroDeComprobante + " - " + movimientoDeAlmacen.Tercero().RazonSocial + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar obtener Registro de VEntas e Ingresos en Excel", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region MOVIMIENTO INTERNO DE MERCADERIA
        public JsonResult ObtenerOrdenesMovimientoInternoMercaderia(bool tipoEntradaSalida, string desde, string hasta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            try
            {
                //OBTENER ORDENES DE MOVIMIENTO INTERNO
                List<OrdenDeTrasladoInterno> ordenesDeMovimiento = tipoEntradaSalida ? operacionLogica.ObtenerOrdenesIngresoInternoMercaderia(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta) :
                                                                                        operacionLogica.ObtenerOrdenesSalidaInternoMercaderia(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                List<BandejaMovimientoInternoMercaderiaViewModel> respuesta = BandejaMovimientoInternoMercaderiaViewModel.Convert(ordenesDeMovimiento);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerMovimientoInternoMercaderiaIncluidoDetallesOrden(long idMovimiento)
        {
            try
            {
                TrasladoInterno resultado = operacionLogica.ObtenerMovimiento(idMovimiento);
                return Json(new MovimientoInternoDeMercaderiaViewModel(resultado.OrdenDeDesplazamiento()));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarMovimientoInternoMercaderia(RegistroTrasladoInternoDeMercaderiaViewModel movimientoInternoMercaderia)
        {
            try
            {
                OperationResult result;
                //CONVERTIMOS LOS DETALLES
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                foreach (var item in movimientoInternoMercaderia.Detalles)
                {
                    detalles.Add(new Detalle_transaccion(item.Cantidad, item.Producto.Id, item.Observacion, 1, 0, 1, 0, null, null, 0, 0, 0, null, null, null));
                }
                if (movimientoInternoMercaderia.TipoDeComprobante.Series == null || movimientoInternoMercaderia.TipoDeComprobante.Series.Count < 1)
                {
                    throw new LogicaException("EL CENTRO DE ATENCIÓN NO CUENTA CON SERIES PARA EL TIPO DE COMPROBANTE SELECCIONADO.");
                }
                result = operacionLogica.ConfirmarMovimientoInternoMercaderiaIntegrado(ProfileData().Empleado.Id,
                    ProfileData().IdCentroDeAtencionSeleccionado,
                    movimientoInternoMercaderia.AlmacenDestino.Id,
                    movimientoInternoMercaderia.ResponsableDestino.Id,
                    movimientoInternoMercaderia.TipoDeComprobante.EsPropio == true ? 0 : movimientoInternoMercaderia.TipoDeComprobante.TipoComprobante.Id,
                    movimientoInternoMercaderia.TipoDeComprobante.EsPropio == true && movimientoInternoMercaderia.TipoDeComprobante.SerieSeleccionada == 0 ? movimientoInternoMercaderia.TipoDeComprobante.Series.First().Id : movimientoInternoMercaderia.TipoDeComprobante.SerieSeleccionada,
                    movimientoInternoMercaderia.TipoDeComprobante.EsPropio,
                    movimientoInternoMercaderia.TipoDeComprobante.SerieIngresada,
                    movimientoInternoMercaderia.TipoDeComprobante.NumeroIngresado,
                    movimientoInternoMercaderia.Observacion,
                    detalles, ProfileData());
                Util.VerificarError(result);
                return new JsonHttpStatusResult(new { result.code_result, data = result.information, result_description = result.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR EL MOVIMIENTO DE ALMACEN", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region OBTENCION DE TIPOS DE COMPROBANTES
        public async Task<JsonResult> ObtenerTiposDeComprobanteMovimientoDeAlmacen()
        {
            try
            {
                var resultados = await operacionLogica.ObtenerTiposDeComprobanteParaAlmacen(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(comprobantes);
            }
            catch (Exception e) { throw e; }
        }

        public JsonResult ObtenerTipoDeComprobanteGuiaDeRemision()
        {
            try
            {
                var resultados = operacionLogica.ObtenerTipoDeComprobanteGuiaDeRemision(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(comprobantes);
            }
            catch (Exception e) { throw e; }
        }

        public JsonResult ObtenerTipoDeComprobanteNotaDeAlmacen()
        {
            try
            {
                var resultados = operacionLogica.ObtenerTipoDeComprobanteNotaDeAlmacen(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(comprobantes);
            }
            catch (Exception e) { throw e; }
        }

        public JsonResult ObtenerTiposDeComprobanteOrdenDeAlmacen()
        {
            try
            {
                var resultados = operacionLogica.ObtenerTipoDeComprobanteOrdenDeAlmacen(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(comprobantes);
            }
            catch (Exception e) { throw e; }
        }
        #endregion



        #region INVENTARIO LOGICO

        public JsonResult GenerarInventarioHistorico()
        {
            try
            {
                //var resultado = operacionLogica.GenerarInventarioLogicoPorLote(ProfileData().Empleado.Id);
                var resultado = inventarioHistoricoLogica.CrearInventarioHistoricoClonandoInventarioFisico(ProfileData().Empleado.Id);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al generar el inventario logico");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al generar inventario lógico", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GenerarInventarioHistoricoAutomaticamente()
        {
            try
            {
                var resultado = inventarioHistoricoLogica.CrearInventarioHistoricoClonandoInventarioFisico(7);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al generar el inventario logico");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al generar inventario lógico", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerFechaDelUltimoInventarioHistorico()
        {
            try
            {
                var resultado = inventarioHistoricoLogica.ObtenerFechaDelUltimoInventarioHistorico(ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(resultado.ToString("dd/MM/yyyy hh:mm:ss"));
            }

            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Errir al obtener fecha del último inventario lógico", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion


        #region GUIA DE REMISISON
        public ActionResult ConsultarGuiasRemision()
        {
            List<string> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteAlmacen();
            ViewBag.fechaInicio = fechas[0];
            ViewBag.fechaFin = fechas[1];
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;

            ViewBag.WCPScript = WebClientPrint.CreateScript(
            Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
            Url.Action("PrintFile", "Almacen", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }
        public async Task<JsonResult> ObtenerGuiasRemision(int[] idsCentrosDeAtencion, string desde, string hasta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            try
            {
                var guiasRemision = operacionLogica.ObtenerGuiasRemision(idsCentrosDeAtencion, fechaDesde, fechaHasta);
                var motivosDeTransporte = await maestroLogica.ObtenerMotivosTrasladoAsync();
                List<BandejaMovimientoMercaderiaViewModel> respuesta = BandejaMovimientoMercaderiaViewModel.Convert(guiasRemision, motivosDeTransporte);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerGuiaRemision(long idGuiaRemision)
        {
            try
            {
                MovimientoDeAlmacen guiaRemision = operacionLogica.ObtenerGuiaRemision(idGuiaRemision);
                int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                var sede = ObtenerSede();
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTransporte = await maestroLogica.ObtenerMotivosTrasladoAsync();
                byte[] byteQr = barCodeUtil.ObtenerCodigoQR(guiaRemision.UrlDocumentoSunat);
                var documento = new DocumentoDeOperacionViewModel()
                {
                    Id = idGuiaRemision,
                    SerieNumeroDocumento = guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante,
                    //AccionInvalidar = (guiaRemision.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado),
                    CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(guiaRemision, FormatoImpresion._80mm, byteQr, sede, proveedores, modalidadesDeTransporte, motivosDeTransporte, this),
                    CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(guiaRemision, FormatoImpresion.A4, byteQr, sede, proveedores, modalidadesDeTransporte, motivosDeTransporte, this)
                };
                return Json(documento);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaGuiaRemision()
        {
            try
            {
                var resultados = await operacionLogica.ObtenerTiposDeComprobanteParaGuiaRemision(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerTiposDeComprobanteParaGuiaRemisionNotaAlmacen()
        {
            try
            {
                var resultados = operacionLogica.ObtenerTiposDeComprobanteGuiaRemisionNotaAlmacen(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> GuardarGuiaRemision(RegistroMovimientoDeAlmacenViewModel guiaRemision)
        {
            try
            {
                OperationResult result;
                //Convertimos los detalles
                List<Detalle_transaccion> detalles = ConstruirDetalleMovimientoMercaderia(guiaRemision);
                //Guardamos la guia de remision
                result = operacionLogica.GuardarGuiaRemision(guiaRemision.Tercero.Id, guiaRemision.TipoDeComprobante.EsPropio, guiaRemision.TipoDeComprobante.TipoComprobante == null ? 0 : guiaRemision.TipoDeComprobante.TipoComprobante.Id, guiaRemision.TipoDeComprobante.SerieSeleccionada, guiaRemision.TipoDeComprobante.SerieIngresada, guiaRemision.TipoDeComprobante.NumeroIngresado, guiaRemision.FechaInicioTraslado, guiaRemision.Transportista.Transportista == null ? 0 : guiaRemision.Transportista.Transportista.Id, guiaRemision.Transportista.Placa, guiaRemision.Conductor.Conductor == null ? 0 : guiaRemision.Conductor.Conductor.Id, guiaRemision.Conductor.NumeroLicencia, guiaRemision.ModalidadTransporte == null ? 0 : guiaRemision.ModalidadTransporte.Id, guiaRemision.MotivoTraslado == null ? 0 : guiaRemision.MotivoTraslado.Id, guiaRemision.DescripcionMotivo, guiaRemision.PesoBrutoTotal, guiaRemision.NumeroBultos, guiaRemision.DireccionOrigen, guiaRemision.UbigeoOrigen == null ? 0 : guiaRemision.UbigeoOrigen.Id, guiaRemision.DireccionDestino, guiaRemision.UbigeoDestino == null ? 0 : guiaRemision.UbigeoDestino.Id, guiaRemision.DocumentoReferencia, guiaRemision.Observacion, detalles, ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar guardar la guia de remision");
                string path = HostingEnvironment.ApplicationPhysicalPath;
                await facturacionElectronicaLogica.TransmitirEnviarGuiaDeRemision(result.data, ProfileData().Sede, ProfileData().Empleado.Id, path);
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR EL MOVIMIENTO", e)), HttpStatusCode.InternalServerError);
            }
        }
        public async Task<JsonResult> EnviarCorreoElectronicoConDocumento(long idGuiaRemision, int formato, List<string> correosElectronicos)
        {
            try
            {
                MovimientoDeAlmacen guiaRemision = operacionLogica.ObtenerGuiaRemision(idGuiaRemision);
                int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                var sede = ObtenerSede();
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTransporte = await maestroLogica.ObtenerMotivosTrasladoAsync();
                byte[] byteQr = barCodeUtil.ObtenerCodigoQR(guiaRemision.UrlDocumentoSunat);
                PdfDocument pdfGuia = PdfBuilder.ObtenerPdfMovimientoDeMercaderia(guiaRemision, sede, byteQr, (FormatoImpresion)formato, proveedores, modalidadesDeTransporte, motivosDeTransporte, this);
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, guiaRemision);
                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, guiaRemision);
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, new List<Attachment>() { new Attachment(new MemoryStream(pdfGuia.Save()), guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".pdf", "application/pdf") });
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public async Task<ActionResult> DescargarDocumento(long idGuiaRemision, int formato)
        {
            try
            {
                var sede = ObtenerSede();
                MovimientoDeAlmacen guiaRemision = operacionLogica.ObtenerGuiaRemision(idGuiaRemision);
                int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(guiaRemision, sede);
                byte[] QrBytes = (guiaRemision.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(guiaRemision.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent); ;
                var pdfGuia = PdfBuilder.ObtenerPdfMovimientoDeMercaderia(guiaRemision, sede, QrBytes, (FormatoImpresion)formato, proveedores, modalidadesDeTraslado, motivosDeTraslado, this);

                string fileNameZip = guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".zip";
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry fileInArchive = null;
                        fileInArchive = archive.CreateEntry(guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".pdf", CompressionLevel.Optimal);
                        fileBytes = pdfGuia.Save();
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }

                        fileInArchive = archive.CreateEntry(guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".xml", CompressionLevel.Optimal);
                        fileBytes = XmlBuilder.ObtenerXmlComprobante(guiaRemision, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }

                    }
                    compressedBytes = outStream.ToArray();
                }
                return File(compressedBytes, "application/zip", fileNameZip);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActionResult> DescargarDocumentoPdf(long idGuiaRemision, int formato)
        {
            try
            {
                var sede = ObtenerSede();
                MovimientoDeAlmacen guiaRemision = operacionLogica.ObtenerGuiaRemision(idGuiaRemision);
                int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(guiaRemision, sede);
                byte[] QrBytes = (guiaRemision.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente) ? barCodeUtil.ObtenerCodigoQR(guiaRemision.Informacion) : barCodeUtil.ObtenerCodigoQR(QrContent); ;
                var pdfGuia = PdfBuilder.ObtenerPdfMovimientoDeMercaderia(guiaRemision, sede, QrBytes, (FormatoImpresion)formato, proveedores, modalidadesDeTraslado, motivosDeTraslado, this);
                byte[] fileBytes = pdfGuia.Save();
                string fileName = guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActionResult> DescargarDocumentoXml(long idGuiaRemision)
        {
            try
            {
                var sede = ObtenerSede();
                MovimientoDeAlmacen guiaRemision = operacionLogica.ObtenerGuiaRemision(idGuiaRemision);
                int[] idsUbigeos = { Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado()) };
                var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                guiaRemision.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                guiaRemision.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(guiaRemision.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                var motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                byte[] fileBytes = XmlBuilder.ObtenerXmlComprobante(guiaRemision, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                string fileName = guiaRemision.Comprobante().NumeroDeSerie + " - " + guiaRemision.Comprobante().NumeroDeComprobante + " - " + guiaRemision.Tercero().RazonSocial + ".xml";
                return File(fileBytes, "application/xml", fileName);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento pdf", e)), HttpStatusCode.InternalServerError);
            }
        }


        public JsonResult InvalidarGuiaRemision(long idGuiaRemision, string observacion)
        {
            try
            {
                OperationResult result = operacionLogica.InvalidarGuiaRemision(idGuiaRemision, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, observacion);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar invalidar la guia de remision");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        public JsonResult ObtenerParametrosParaRegistradorGuiaRemision()
        {
            try
            {
                var configuracion = new ConfiguracionRegistradorGuiaRemision
                {
                    DireccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : "",
                    IdUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto,
                    NumeroDocumentoSede = ObtenerSede().DocumentoIdentidad
                };
                return Json(new { data = configuracion });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "AdministradorTI")]
        public async Task<JsonResult> EnviarGuiasRemision(string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
                string path = HostingEnvironment.ApplicationPhysicalPath;
                OperationResult result = await facturacionElectronicaLogica.EnviarGuiaDeRemisionManual(fechaDesde, fechaHasta, ProfileData().Sede, ProfileData().Empleado.Id, path);
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR ENVIAR LA GUIA REMISION", e)), HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "AdministradorTI")]
        public JsonResult ConsultarEnvioGuiasRemision()
        {
            try
            {
                OperationResult result = facturacionElectronicaLogica.ConsultarGuiasRemisionManual();
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR CONSULTAR LA GUIA REMISION", e)), HttpStatusCode.InternalServerError);
            }
        }
    }
}
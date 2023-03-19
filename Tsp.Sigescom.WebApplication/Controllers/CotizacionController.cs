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

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CotizacionController : BaseController
    {
        private readonly ICotizacionLogica cotizacionLogica;
        private readonly IConceptoLogica conceptoLogica;
        protected readonly IMailer mailer;
        protected readonly IVentaUtilitarioLogica ventaUtil;
        protected readonly IPdfUtil pdfUtil;


        public CotizacionController()
        {
            cotizacionLogica = Dependencia.Resolve<ICotizacionLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            ventaUtil = Dependencia.Resolve<IVentaUtilitarioLogica>();
            pdfUtil = Dependencia.Resolve<IPdfUtil>();


        }

        // GET: Cotizacion
        public ActionResult Index()
        {
            string mascaraDeIngreso = VentasSettings.Default.MascaraDeCamposAIngresarEnVentas;
            string mascaraDeCalculo = VentasSettings.Default.MascaraFormasDeCalculoEnVentas;
            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.idMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.precioContieneIgv = TransaccionSettings.Default.PrecioContieneIGV;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.idTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.mostrarAliasDeClienteGenerico = TransaccionSettings.Default.MostrarAliasDeClienteGenerico;
            ViewBag.precioUnitarioCalculadoVenta = AplicacionSettings.Default.PrecioUnitarioCalculadoVenta;
            ViewBag.mostrarDetalleUnificado = AplicacionSettings.Default.MostrarDetalleUnificado;
            ViewBag.checketDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
            ViewBag.valorDetalleUnificado = AplicacionSettings.Default.ValorDetalleUnificado;
            ViewBag.aplicarCantidadPorDefectoEnVentas = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
            ViewBag.cantidadPorDefectoEnVentas = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.precioUnitarioIngresadoVenta = AplicacionSettings.Default.PrecioUnitarioIngresadoVenta;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.idDetalleMaestroCatalogoDocumentoBoleta = MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta;
            ViewBag.idDetalleMaestroCatalogoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.montoMaximoAVenderCuandoClienteNoEstaIdenticicado = FacturacionElectronicaSettings.Default.MontoMaximoAVenderCuandoClienteNoEstaIdenticicado;
            ViewBag.permitirVentaAlCredito = AplicacionSettings.Default.PermitirVentaAlCredito;
            ViewBag.permitirVentaConFechaPasada = AplicacionSettings.Default.PermitirVentaConFechaPasada;
            ViewBag.fechaActual =DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.VenderConBoletaCada = VentasSettings.Default.VenderConBoletaCada;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnCliente;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.modoIngresoCodigoBarraEnVenta = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta;
            ViewBag.cursorPorDefectoCodigoBarraEnVenta = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta;
            ViewBag.permitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : " ";
            ViewBag.idUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : 0;
            ViewBag.idModalidadTrasladoPorDefecto = AplicacionSettings.Default.IdModalidadDeTrasladoPorDefectoEnSalidaDeMercaderia;
            ViewBag.idMotivoTrasladoPorDefecto = AplicacionSettings.Default.IdMotivoDeTrasladoPorDefectoEnSalidaDeMercaderia;
            ViewBag.idTransportistaPorDefecto = AplicacionSettings.Default.IdTransportistaPorDefectoEnSalidaDeMercaderia;
            ViewBag.idTipoDeComprobantePorDefecto = AplicacionSettings.Default.IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia;
            ViewBag.costoUnitarioPorBolsaDePlastico = ProfileData().CostoUnitarioDelIcbper;
            ViewBag.permitirRegistroNumeroBolsaDePlastico = VentasSettings.Default.PermitirRegistroDeIcbperEnVenta;
            ViewBag.permitirRegistroDeLoteEnDetalleDeVenta = VentasSettings.Default.PermitirRegistroDeLoteEnDetalleDeVenta;
            ViewBag.idConceptoBasicoBolsaPlastica = MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica;
            ViewBag.idTarifaSeleccionadoPorDefecto = VentasSettings.Default.IdTarifaSeleccionadoPorDefecto;
            ViewBag.modoDeRegistroDeDetalleDeVenta = VentasSettings.Default.ModoDeRegistroDeDetalleDeVentaYCobroMasivo;
            ViewBag.permitirIngresarCantidad = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.permitirIngresarPrecioUnitario = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.permitirIngresarImporte = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarCantidadCalcularPrecioUnitario = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.ingresarPrecioUnitarioCalcularImporte = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarImporteCalcularCantidad = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.modoDeSeleccionDeConceptoDeNegocio = VentasSettings.Default.ModoDeSeleccionDeConceptoDeNegocio;
            ViewBag.idComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
            ViewBag.mascaraDeCalculoPorDefecto = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas;
            ViewBag.mascaraDeCalculoPrecioUnitarioCalculado = VentasSettings.Default.MascaraDeCalculoPrecioUnitarioCalculado;
            ViewBag.flujoDespuesDeCodigoBarraEnVenta = VentasSettings.Default.FlujoDespuesDeCodigoBarraEnVenta;
            ViewBag.esPregeneracionVentaDesdeCotizacion = false;
            ViewBag.idOrdenCotizacionAPregenerar = 0;
            ViewBag.permitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.mostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnCotizacion;
            ViewBag.modoDeSeleccionDeConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnCotizacion;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnCotizacion;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            ViewBag.informacionSelectorConcepto = CotizacionSettings.Default.InformacionSelectorConceptoEnCotizacion;
            ViewBag.permitirSeleccionarGrupoCliente = Diccionario.MapeoOperacionesGruposVsPermitirGrupos.Single(m => m.Key == (int)OperacionesGruposActoresComerciales.Cotizacion).Value;
            ViewBag.permitirConvertirCotizacionAVenta = CotizacionSettings.Default.MascaraParaPermitirConvertirCotizacionAVentaPedido[0] == '1';
            ViewBag.permitirConvertirCotizacionAPedido = CotizacionSettings.Default.MascaraParaPermitirConvertirCotizacionAVentaPedido[1] == '1';

            ViewBag.WCPScript = WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("ImprimirCotizacion", "Cotizacion", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        #region COTIZACION

        public JsonResult ObtenerCotizaciones(string desde, string hasta)
        {
            DateTime fechaDesde = DateTime.Parse(desde);
            DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
            try
            {
                List<OrdenDeCotizacion> ordenesDeCotizacion = cotizacionLogica.ObtenerOrdenesDeCotizacion(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                List<BandejaCotizacionViewModel> respuesta = BandejaCotizacionViewModel.Convert(ordenesDeCotizacion);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerOrdenDeCotizacion(long idOrdenVenta)
        {
            try
            {
                OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrdenVenta);
                var respuesta = new CotizacionViewModel(ordenDeCotizacion);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerCotizacionParaEditar(long idOrden)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                int idCentroAtencionQueTieneStockIntegrado = ProfileData().IdCentroAtencionQueTieneElStockIntegrada;
                long idTransaccionInventarioFisico = ProfileData().ObtenerIdInventarioActual(idCentroAtencionQueTieneStockIntegrado);

                OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrden);
                var registro = new RegistroCotizacionViewModel(ordenDeCotizacion, SelectorTipoDeComprobante.Convert(await cotizacionLogica.ObtenerTiposDeComprobanteParaCotizacion(ProfileData().IdCentroDeAtencionSeleccionado)));
                foreach (var item in registro.Detalles)
                {
                    item.ConceptoCompleto = conceptoLogica.ObtenerConceptoDeNegocioComercialPorIdConcepto(ProfileData(), item.Producto.Id, true, true);
                }
                return Json(registro);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarCotizacion(RegistroCotizacionViewModel cotizacion)
        {
            try
            {
                OperationResult result;
                List<DetalleDeOperacion> detalles = ConstruirDetalleTransaccion(cotizacion.Detalles.ToList());
                if (cotizacion.Id > 0)
                {
                    result = cotizacionLogica.EditarCotizacion(cotizacion.Id, cotizacion.IdOrden, cotizacion.IdComprobante, cotizacion.IdEstado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, cotizacion.Cliente.Id, cotizacion.Alias, cotizacion.TipoDeComprobante.TipoComprobante.Id, cotizacion.TipoDeComprobante.SerieSeleccionada == 0 ? cotizacion.TipoDeComprobante.Series.First().Id : cotizacion.TipoDeComprobante.SerieSeleccionada,
                    cotizacion.GrabaIgv, cotizacion.FechaVencimiento, cotizacion.Observacion, detalles, cotizacion.Flete);
                }
                else
                {
                    result = cotizacionLogica.AgregarCotizacion(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, cotizacion.Cliente.Id, cotizacion.Alias, cotizacion.TipoDeComprobante.TipoComprobante.Id, cotizacion.TipoDeComprobante.SerieSeleccionada == 0 ? cotizacion.TipoDeComprobante.Series.First().Id : cotizacion.TipoDeComprobante.SerieSeleccionada, cotizacion.GrabaIgv, cotizacion.FechaVencimiento, cotizacion.Observacion, detalles, cotizacion.Flete);
                }


                Util.ManageIfResultIsNotSuccess(result, "ERROR AL REGISTRAR LA COTIZACION");

                return Json(new { result.code_result, data = result.data, result_description = result.title, IdOrden = result.information });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR LA COTIZACION", e)), HttpStatusCode.InternalServerError);
            }
        }



        public List<DetalleDeOperacion> ConstruirDetalleTransaccion(List<RegistroDetalleVentaViewModel> detalles)
        {
            List<DetalleDeOperacion> detallesConstruidos = new List<DetalleDeOperacion>();
            foreach (var item in detalles)
            {
                detallesConstruidos.Add(new DetalleDeOperacion(item.IdDetalle,
                    item.Producto.Id,
                    item.Cantidad,
                    item.PrecioUnitario,
                    item.Importe,
                    0, 0,
                    item.Descuento,
                    null, null, null, null, true, item.MascaraDeCalculo, null));
            }
            return detallesConstruidos;
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteConSeriesAutonumericasParaCotizacion()
        {
            try
            {
                var resultados = await cotizacionLogica.ObtenerTiposDeComprobanteParaCotizacion(ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado, true);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region VISUALIZACION Y ENVIO DE COMPROBANTES

        public JsonResult ObtenerDocumentoDeCotizacion(long idOrdenDeCotizacion)
        {
            try
            {
                OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrdenDeCotizacion);
                var sede = ObtenerSede();
                var htmlString = ObtenerCadenaHtmlDeCotizacion(ordenDeCotizacion, (FormatoImpresion)1, null, sede);
                var respuesta = new CotizacionViewModel(ordenDeCotizacion, htmlString);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        string ObtenerCadenaHtmlDeCotizacion(OrdenDeCotizacion ordenDeCotizacion, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendidoConLogo sede)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";

            nombreVista = formato == FormatoImpresion._80mm ? "DocumentoDeCotizacion80" : "DocumentoDeCotizacionA4";
            result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new DocumentoDeCotizacion(ordenDeCotizacion, sede,  new EstablecimientoComercialExtendido( ordenDeCotizacion.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), this);

            return result;
        }

        public JsonResult ObtenerHtmlDocumentoDeCotizacion(long idOrden, int formato)
        {
            OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrden);
            var sede = ObtenerSede();
            var htmlString = ObtenerCadenaHtmlDeCotizacion(ordenDeCotizacion, (FormatoImpresion)formato, null, sede);
            return Json(htmlString);
        }

        public JsonResult EnviarCorreoElectronicoConDocumentoDeCotizacion(long idOrden, int formato, List<string> correosElectronicos)
        {
            try
            {
                OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrden);
                var sede = ObtenerSede();
                PdfDocument pdfVenta = ObtenerPdfCotizacion(ordenDeCotizacion, sede, null, (FormatoImpresion)formato);
                string asunto = cotizacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, ordenDeCotizacion);
                string cuerpo = cotizacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, ordenDeCotizacion);
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, new List<Attachment>() { new Attachment(new MemoryStream(pdfVenta.Save()), ordenDeCotizacion.Comprobante().NumeroDeSerie + " - " + ordenDeCotizacion.Comprobante().NumeroDeComprobante + " - " + ordenDeCotizacion.Cliente().RazonSocial + ".pdf", "application/pdf") });
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public ActionResult DescargarDocumentoDeCotizacion(long idOrden, int formato)
        {
            try
            {
                OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrden);
                var sede = ObtenerSede();
                PdfDocument pdfVenta = ObtenerPdfCotizacion(ordenDeCotizacion, sede, null, (FormatoImpresion)formato);
                byte[] fileBytes = pdfVenta.Save();
                string fileName = ordenDeCotizacion.Comprobante().NumeroDeSerie + " - " + ordenDeCotizacion.Comprobante().NumeroDeComprobante + " - " + ordenDeCotizacion.Cliente().RazonSocial + ".pdf";
                return File(fileBytes, "application/pdf", fileName);
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

        public PdfDocument ObtenerPdfCotizacion(OrdenDeCotizacion ordenDeCotizacion, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {
            string htmlString = ObtenerCadenaHtmlDeCotizacion(ordenDeCotizacion, formato, qrBytes, sede);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }

        public void ImprimirCotizacion(long idOrdenCotizacion)
        {
            OrdenDeCotizacion ordenDeCotizacion = cotizacionLogica.ObtenerOrdenDeCotizacion(idOrdenCotizacion);
            var sede = ObtenerSede();
            var imageVenta = ObtenerImageCotizacion(ordenDeCotizacion, sede, null, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto);
            MemoryStream ms = new MemoryStream();

            imageVenta.Save(ms, ImageFormat.Jpeg);

            var imageBytes = ms.ToArray();

            PrintFile file = null;
            file = new PrintFile(imageBytes, idOrdenCotizacion + ".jpg");
            //file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        public System.Drawing.Image ObtenerImageCotizacion(OrdenDeCotizacion ordenDeCotizacion, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {
            string result = ObtenerCadenaHtmlDeCotizacion(ordenDeCotizacion, formato, qrBytes, sede);
            HtmlToImage imgConverter = new HtmlToImage();
            imgConverter.WebPageFixedSize = false;

            if (formato == FormatoImpresion._80mm)
            {
                imgConverter.WebPageWidth = 302;
            }
            else if (formato == FormatoImpresion.A4)
            {
                imgConverter.WebPageWidth = 793;
            }
            else if (formato == FormatoImpresion._56mm)
            {
                imgConverter.WebPageWidth = 211;//in Pixels
            }
            // create a new image converting an url
            System.Drawing.Image image = imgConverter.ConvertHtmlString(result);
            return image;
        }
        #endregion

    }
}
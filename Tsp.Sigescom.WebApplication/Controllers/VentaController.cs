using Microsoft.Reporting.WebForms;
using Neodynamic.SDK.Web;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
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
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Utilitarios.RestHelper;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.Venta;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class VentaController : BaseController
    {
        public string RutaArchivo { get; set; }
        public string IdDocumento { get; set; }
        private readonly IConceptoLogica conceptoLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IConfiguracionLogica configuracionLogica;
        protected readonly IGeneracionArchivosLogica generacionArchivosLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IMailer mailer;
        protected readonly IBarCodeUtil barCodeUtil;
        protected readonly IPdfUtil pdfUtil;

        protected readonly IVentaUtilitarioLogica ventaUtil;

        private readonly IConceptoRepositorio  conceptoDatos;
        private readonly ICentroDeAtencion_Repositorio centroDeAtencionDatos;

        private readonly ISede_Logica sedeLogica;
        protected readonly IOrdenAlmacen_Logica ordenAlmacenLogica;


        public byte[] respuestaCDR;


        public VentaController() : base()
        {
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            conceptoDatos = Dependencia.Resolve<IConceptoRepositorio>();

            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            generacionArchivosLogica = Dependencia.Resolve<IGeneracionArchivosLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            ventaUtil = Dependencia.Resolve<IVentaUtilitarioLogica>();
            pdfUtil = Dependencia.Resolve<IPdfUtil>();
            centroDeAtencionDatos=Dependencia.Resolve<ICentroDeAtencion_Repositorio>();

            sedeLogica = Dependencia.Resolve<ISede_Logica>();
            ordenAlmacenLogica = Dependencia.Resolve<IOrdenAlmacen_Logica>();

        }
        public OrdenDeVenta ObtenerOrdenDeVentaImprimir()
        {
            return (OrdenDeVenta)System.Web.HttpContext.Current.Application["VentaAImprimir"];
        }
        public JsonResult ObtenerClientePorDefecto()
        {
            try
            {
                return Json(ProfileData().ClientePorDefecto);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #region INICIALIZACION DE LAS VISTAS

        [Authorize(Roles = "SoloVendedor,Vendedor")]
        public ActionResult Ventas()
        {
            var modoVenta = (int)ModoVenta.VentaNormal;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos; 
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto; 
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.PermitirEnvioPorWhatsApp = VentasSettings.Default.PermitirEnvioPorWhatsApp;
            ViewBag.EnvioComprobantePostVenta = VentasSettings.Default.EnvioComprobantePostVenta;

            ViewBag.EsPregeneracionVenta = false;
            ViewBag.IdOrdenAPregenerar = 0;
            ViewBag.TipoOperacionAPregenerar = 0;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }
        [Authorize(Roles = "Vendedor")]
        [ActionName("PregeneracionVenta")]
        public ActionResult Ventas(long idOrden, int tipoOperacion)//tipoOperacion: Tipo de operacion a pregenerar, 1: cotizacion, 2: venta
        {
            var modoVenta = (int)ModoVenta.VentaNormal;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto; 
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.PermitirEnvioPorWhatsApp = VentasSettings.Default.PermitirEnvioPorWhatsApp;
            ViewBag.EnvioComprobantePostVenta = VentasSettings.Default.EnvioComprobantePostVenta;

            ViewBag.esPregeneracionVenta = true;
            ViewBag.idOrdenAPregenerar = idOrden;
            ViewBag.tipoOperacionAPregenerar = tipoOperacion;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View("Ventas");
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult VentasPorContingencia()
        {
            var modoVenta = (int)ModoVenta.VentaPorContingencia;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto; 
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.PermitirEnvioPorWhatsApp = VentasSettings.Default.PermitirEnvioPorWhatsApp;
            ViewBag.EnvioComprobantePostVenta = VentasSettings.Default.EnvioComprobantePostVenta;

            ViewBag.EsPregeneracionVenta = false;
            ViewBag.IdOrdenAPregenerar = 0;
            ViewBag.TipoOperacionAPregenerar = 0;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View("Ventas");
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult VentaPorMostradorIntegradoModoCaja()
        {
            var modoVenta = (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.MostrarCodigoBarraBalanza = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeBalanza || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
            ViewBag.CursorInicialEnCodigoBarra = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta == (int)CursorInicialCodigoBarraEnVenta.CodigoBarraDeProducto; 
            ViewBag.EsVentaPorContingencia = modoVenta == (int)ModoVenta.VentaPorContingencia;
            ViewBag.EsVentaModoCajaAlmacen = modoVenta == (int)ModoVenta.VentaModoCajaAlmacen;
            ViewBag.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = VentasSettings.Default.PermitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
            ViewBag.PermitirRegistroDeGuiasDeRemision = VentasSettings.Default.PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada;
            ViewBag.PermitirRegistroConceptoServicio = VentasSettings.Default.PermitirRegistroConceptoServicio;
            ViewBag.PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.PermitirRegistroPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta;
            ViewBag.IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.IdTipoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.PermitirEnvioPorWhatsApp = VentasSettings.Default.PermitirEnvioPorWhatsApp;
            ViewBag.EnvioComprobantePostVenta = VentasSettings.Default.EnvioComprobantePostVenta;

            ViewBag.EsPregeneracionVenta = false;
            ViewBag.IdOrdenAPregenerar = 0;
            ViewBag.TipoOperacionAPregenerar = 0;
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View("Ventas");
        }
        [Authorize(Roles = "Vendedor")]
        public ActionResult ConsultarVentas()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            //ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.idMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.idTipoTransaccionOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
            ViewBag.idTipoTransaccionAnulacionOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta;
            ViewBag.idTipoTransaccionDescuentoSobreOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionDescuentoSobreOrdenDeVenta;
            ViewBag.idTipoTransaccionDebitoSobreOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionDebitoOrdenDeVenta;
            ViewBag.idComprobanteNotaDeCredito = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito;
            ViewBag.idComprobanteNotaDeDebito = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito;
            ViewBag.idDetalleMaestroAnulacionDeLaOperacion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion;
            ViewBag.idDetalleMaestroAnulacionPorErrorEnElRuc = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc;
            ViewBag.idDetalleMaestroCorreccionPorErrorEnLaDescripcion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion;
            ViewBag.idDetalleMaestroDescuentoGlobal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal;
            ViewBag.idDetalleMaestroDescuentoPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem;
            ViewBag.idDetalleMaestroDevolucionTotal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal;
            ViewBag.idDetalleMaestroDevolucionPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem;
            ViewBag.idDetalleMaestroBonificacion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaBonificacion;
            ViewBag.idDetalleMaestroDisminucionEnElValor = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDisminucionEnElValor;
            ViewBag.idDetalleMaestroOtrosConceptos = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaOtrosConceptos;
            ViewBag.idDetalleMaestroInteresesPorMora = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora;
            ViewBag.idDetalleMaestroAumentoEnElValor = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor;
            ViewBag.idDetalleMaestroPenalidadesYOtrosConceptos = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaPenalidadesYOtrosConceptos;
            ViewBag.fechaInicio = fechas[0];
            ViewBag.fechaFin = fechas[1];
            ViewBag.permitirCanjeDeComprobanteEnVentas = VentasSettings.Default.PermitirCanjeDeComprobanteEnVentas;
            ViewBag.idTipoComprobanteNotaDeVenta = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
            ViewBag.idTipoComprobanteFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.permitirClonarVenta = VentasSettings.Default.PermitirClonarVenta;
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mostrarSelectorClienteEnVerVentas = VentasSettings.Default.MostrarSelectorClienteEnVerVentas;
            ViewBag.mostrarBuscadorComprobanteEnVerVentas = VentasSettings.Default.MostrarBuscadorComprobanteEnVerVentas;
            ViewBag.permitirVentaAlCredito = AplicacionSettings.Default.PermitirVentaAlCredito;
            ViewBag.PermitirEnvioPorWhatsApp = VentasSettings.Default.PermitirEnvioPorWhatsApp;
            ViewBag.EnvioComprobantePostVenta = VentasSettings.Default.EnvioComprobantePostVenta;

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult VentasCorporativas()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.idMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.precioContieneIgv = TransaccionSettings.Default.PrecioContieneIGV;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.idTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.mostrarAliasDeClienteGenerico = TransaccionSettings.Default.MostrarAliasDeClienteGenerico;
            ViewBag.precioUnitarioCalculadoVenta = AplicacionSettings.Default.PrecioUnitarioCalculadoVenta;
            ViewBag.mostrarDetalleUnificado = AplicacionSettings.Default.MostrarDetalleUnificado;
            ViewBag.checketDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
            ViewBag.valorDetalleUnificado = AplicacionSettings.Default.ValorDetalleUnificado;
            ViewBag.aplicarCantidadPorDefectoEnVentas = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
            ViewBag.cantidadPorDefectoEnVentas = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
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
            ViewBag.fechaActual = DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto;
            ViewBag.modoIngresoCodigoBarraEnVenta = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta;
            ViewBag.cursorPorDefectoCodigoBarraEnVenta = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta;
            ViewBag.idEstablecimientoComercialPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroAtencionPorDefecto = ProfileData().IdCentroDeAtencionSeleccionado;
            ViewBag.idEmpleadoPorDefecto = ProfileData().Empleado.Id;
            ViewBag.idDetalleMaestroMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.idDetalleMaestroEntidadBancariaNinguna = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
            ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle + " , " + ObtenerSede().DomicilioFiscal.Ubigeo.Nombre : " ";
            ViewBag.idModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
            ViewBag.idMotivoTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnCliente;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.idConceptoBasicoBolsaPlastica = MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica;
            ViewBag.idComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();

        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult ConsultarVentasCorporativas()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.idMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.precioContieneIgv = TransaccionSettings.Default.PrecioContieneIGV;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.idTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.mostrarAliasDeClienteGenerico = TransaccionSettings.Default.MostrarAliasDeClienteGenerico;
            ViewBag.precioUnitarioCalculadoVenta = AplicacionSettings.Default.PrecioUnitarioCalculadoVenta;
            ViewBag.mostrarDetalleUnificado = AplicacionSettings.Default.MostrarDetalleUnificado;
            ViewBag.checketDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
            ViewBag.valorDetalleUnificado = AplicacionSettings.Default.ValorDetalleUnificado;
            ViewBag.aplicarCantidadPorDefectoEnVentas = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
            ViewBag.cantidadPorDefectoEnVentas = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.precioUnitarioIngresadoVenta = AplicacionSettings.Default.PrecioUnitarioIngresadoVenta;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.idDetalleMaestroCatalogoDocumentoBoleta = MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta;
            ViewBag.montoMaximoAVenderCuandoClienteNoEstaIdenticicado = FacturacionElectronicaSettings.Default.MontoMaximoAVenderCuandoClienteNoEstaIdenticicado;
            ViewBag.permitirVentaAlCredito = AplicacionSettings.Default.PermitirVentaAlCredito;
            ViewBag.permitirVentaConFechaPasada = AplicacionSettings.Default.PermitirVentaConFechaPasada;
            ViewBag.fechaActual = DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.fechaInicio = DateTimeUtil.FechaActual().AddMonths(-1).ToString("dd/MM/yyyy");
            ViewBag.fechaFin = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnCliente;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.idComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult PuntoDeVenta()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.idMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.precioContieneIgv = TransaccionSettings.Default.PrecioContieneIGV;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
            ViewBag.idTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
            ViewBag.mostrarAliasDeClienteGenerico = TransaccionSettings.Default.MostrarAliasDeClienteGenerico;
            ViewBag.precioUnitarioCalculadoVenta = AplicacionSettings.Default.PrecioUnitarioCalculadoVenta;
            ViewBag.mostrarDetalleUnificado = AplicacionSettings.Default.MostrarDetalleUnificado;
            ViewBag.checketDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
            ViewBag.valorDetalleUnificado = AplicacionSettings.Default.ValorDetalleUnificado;
            ViewBag.aplicarCantidadPorDefectoEnVentas = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
            ViewBag.cantidadPorDefectoEnVentas = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
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
            ViewBag.fechaActual = DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
            ViewBag.VenderConBoletaCada = VentasSettings.Default.VenderConBoletaCada;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnCliente;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.modoIngresoCodigoBarraEnVenta = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta;
            ViewBag.cursorPorDefectoCodigoBarraEnVenta = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta;
            ViewBag.idComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("PrintFile", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        [Authorize(Roles = "Cajero")]
        public ActionResult CajaDeVenta()
        {
            ViewBag.fechaDesde = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.fechaHasta = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            return View();
        }

        

        [Authorize(Roles = "Vendedor")]
        public ActionResult VentasYCobrosPorVendedor()
        {
            string mascaraDeIngreso = VentasSettings.Default.MascaraDeCamposAIngresarEnVentasYCobrosPorVendedor;
            string mascaraDeCalculo = VentasSettings.Default.MascaraFormasDeCalculoEnVentasYCobrosPorVendedor;
            ViewBag.idConcepto = ConceptoSettings.Default.IdConceptoEnRegistroUnificadosdeVentasyCobros;
            ViewBag.fechaActual = DateTimeUtil.FechaActual().Date;
            ViewBag.permitirIngresarCantidad = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.permitirIngresarPrecioUnitario = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.permitirIngresarImporte = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarCantidadCalcularPrecioUnitario = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.ingresarPrecioUnitarioCalcularImporte = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarImporteCalcularCantidad = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe, ElementoDeCalculoEnVentasEnum.Cantidad);
            //ViewBag.tieneRolDigitador = this.User.IsInRole("Digitador");
            ViewBag.tieneRolCajero = this.User.IsInRole("Cajero");
            ViewBag.idEmpleado = ProfileData().Empleado.Id;
            ViewBag.idCentroAtencion = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.mascaraDeCalculoPorDefecto = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas;
            ViewBag.mascaraDeCalculoPrecioUnitarioCalculado = VentasSettings.Default.MascaraDeCalculoPrecioUnitarioCalculado;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("ImprimirComprobantesVentasMasivas", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult ConsultarVentasCobrosPorVendedor()
        {
            ViewBag.fechaInicio = DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(
                Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
                Url.Action("ImprimirComprobantesVentasMasivas", "Venta", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult VentasMasivas()
        {
            string mascaraDeIngreso = VentasSettings.Default.MascaraDeCamposAIngresarEnVentasMasivas;
            string mascaraDeCalculo = VentasSettings.Default.MascaraFormasDeCalculoEnVentasMasivas;
            ViewBag.fechaActual = DateTimeUtil.FechaActual().Date;
            ViewBag.idComprobantePorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta;
            ViewBag.permitirIngresarCantidad = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.permitirIngresarPrecioUnitario = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.permitirIngresarImporte = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarCantidadCalcularPrecioUnitario = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad, ElementoDeCalculoEnVentasEnum.PrecioUnitario);
            ViewBag.ingresarPrecioUnitarioCalcularImporte = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario, ElementoDeCalculoEnVentasEnum.Importe);
            ViewBag.ingresarImporteCalcularCantidad = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.mascaraDeCalculoPorDefecto = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;

            return View();
        }


        #endregion

        #region VENTA POR MOSTRADOR - INTEGRADA A 

        public JsonResult ObtenerVentas(string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTime.Parse(desde);
                DateTime fechaHasta = DateTime.Parse(hasta);
                List<OperacionDeVenta> operacionesDeVenta = operacionLogica.ObtenerOperacionesDeVenta(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                List<BandejaVentaViewModel> respuesta = BandejaVentaViewModel.Convert_(operacionesDeVenta);
                var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER LAS VENTAS", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerResumenesVentas_(string desde, string hasta)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(desde);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(hasta);
                List<Resumen_Venta> resumenesVentas = operacionLogica.ObtenerResumenesVentas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, 1, "");
                List<BandejaVentaViewModel> resumenes = BandejaVentaViewModel.Convert_(resumenesVentas);
                var jsonResult = Json(resumenes, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER RESUMENES DE VENTAS", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerResumenesVentas(string desde, string hasta, int? idCliente, string comprobante)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(desde);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(hasta);
                List<Resumen_Venta> resumenesVentas = operacionLogica.ObtenerResumenesVentas(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta, idCliente, comprobante);
                var jsonResult = Json(resumenesVentas, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER RESUMENES DE VENTAS", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> GuardarVentaPorMostrador(DatosVentaIntegrada venta)
        {
            try
            {
                OperationResult result = operacionLogica.ConfirmarVentaIntegrada(ModoOperacionEnum.PorMostrador, ProfileData(), venta);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INTENTAR GUARDAR UNA VENTA");
                //Armar orden de venta para guardar para la impresion
                var ordenVenta = ArmarOrdenVentaParaImprimir((OrdenDeVenta)result.objeto, venta);
                //Guardar la orden de venta en una variable de aplicacion
                GuardarOrdenDeVentaParaImprimirEnAplicacion(ordenVenta);
                //Control de tipo de comprobante a emitir en proxima venta
                var totalVenta = venta.Orden.Detalles.Sum(d => d.Importe);
                int[] idsTiposComprobanteTributarios = { MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta, MaestroSettings.Default.IdDetalleMaestroComprobanteFactura };
                this.Session["ContadorVentas"] = (int)((this.Session["ContadorVentas"]) ?? 0) + 1;
                this.Session["VentaAcumulada"] = (decimal)((this.Session["VentaAcumulada"]) ?? 0m) + totalVenta;
                this.Session["VentaFacturada"] = (decimal)((this.Session["VentaFacturada"]) ?? 0m) + (idsTiposComprobanteTributarios.Contains(venta.Orden.Comprobante.Tipo.Id) ? totalVenta : 0);
                var porcentajeFacturado = this.Session["VentaFacturada"] != null && (decimal)((this.Session["VentaFacturada"]) ?? 0) > 0 ? ((decimal)((this.Session["VentaFacturada"]) ?? 0) / (decimal)((this.Session["VentaAcumulada"]) ?? 0)) * 100 : 0;
                var venderConBoleta = (porcentajeFacturado < VentasSettings.Default.PorcentajeFacturacion);
                if (venta.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia)
                {
                    string path = HostingEnvironment.ApplicationPhysicalPath;
                    await facturacionElectronicaLogica.TransmitirEnviarGuiaDeRemision(((OrdenDeVenta)result.objeto).Transaccion().Transaccion11.First().id, ProfileData().Sede, ProfileData().Empleado.Id, path);
                }
                return Json(new { result.code_result, data = result.information, result_description = result.title, VenderConBoleta = venderConBoleta, IdOrden = result.information, SerieNumeroDocumento = ordenVenta.Transaccion().Comprobante.numero_serie + "-" + ordenVenta.Transaccion().Comprobante.numero.ToString(), IdEncriptado = Encriptar(((long)result.information).ToString()) });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR LA VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }

        private OrdenDeVenta ArmarOrdenVentaParaImprimir(OrdenDeVenta ordenDeVenta, DatosVentaIntegrada venta)
        {
            ordenDeVenta.Transaccion().Detalle_maestro1 = ProfileData().MaestrosFrecuentes.Moneda.Convert();
            ordenDeVenta.Transaccion().Comprobante.Detalle_maestro = venta.Orden.Comprobante.Tipo.Convert();
            ordenDeVenta.Transaccion().Actor_negocio = ProfileData().Empleado.Convert();
            ordenDeVenta.Transaccion().Actor_negocio1 = venta.Orden.Cliente.Convert();
            //ordenDeVenta.Transaccion().Actor_negocio2 = ProfileData().CentroDeAtencionSeleccionado;
            foreach (var item in ordenDeVenta.Transaccion().Detalle_transaccion)
            {
                item.registro = venta.Orden.Placa;
                item.Concepto_negocio = new Concepto_negocio()
                {
                    id = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Id,
                    codigo = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Codigo,
                    nombre = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.NombreConcepto,
                    Detalle_maestro4 = new Detalle_maestro() { valor = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.EsBien ? "1" : "0" }
                };
            }
            return ordenDeVenta;
        }

        private OrdenDeVenta ArmarOrdenDeNotaParaImprimir(OrdenDeVenta ordenDeNota, RegistroDeNotaViewModel nota)
        {
            ordenDeNota.Transaccion().Detalle_maestro1 = ProfileData().MaestrosFrecuentes.Moneda.Convert();
            ordenDeNota.Transaccion().Comprobante.Detalle_maestro = nota.Comprobante.TipoComprobante.Convert();
            ordenDeNota.Transaccion().Actor_negocio = ProfileData().Empleado.Convert();
            ordenDeNota.Transaccion().Actor_negocio1 = actorNegocioLogica.ObtenerActorComercialPorId(ActorSettings.Default.IdRolCliente, ordenDeNota.IdCliente).Convert();
            ////ordenDeVenta.Transaccion().Actor_negocio2 = ProfileData().CentroDeAtencionSeleccionado;
            var detallesOrdenNota=ordenDeNota.Transaccion().Detalle_transaccion.ToList();
            var idsConceptosEnOrden = detallesOrdenNota.Select(d => d.id_concepto_negocio).ToArray();
            //conseguir conceptos de negocio incluido su detalle_maestro4
            if (idsConceptosEnOrden.Count() > 0)
            {
            var conceptos = conceptoDatos.ObtenerConceptosDeNegocioIncluyendoDetalleMaestro4(idsConceptosEnOrden).ToList();
            detallesOrdenNota.ForEach(dt => dt.Concepto_negocio = conceptos.Single(c => c.id == dt.id_concepto_negocio));
            }

            return ordenDeNota;
        }
        #endregion
 

        #region VENTA POR MOSTRADOR - 2 PASOS (ORDEN - COBRO , ALMACEN)

        public JsonResult ObtenerOrdenesDeVentas(string desde, string hasta)
        {
            DateTime fechaDesde = DateTime.Parse(desde);
            DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
            try
            {
                List<OrdenDeVenta> ordenesDeVenta = operacionLogica.ObtenerOrdenesDeVenta(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                List<BandejaOrdenesCajaVentaViewModel> respuesta = BandejaOrdenesCajaVentaViewModel.Convert(ordenesDeVenta);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerOrdenVenta(long idOrdenVenta)
        {
            try
            {
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, idOrdenVenta);
                var respuesta = new OrdenVentaCompraViewModel(ordenDeVenta);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER LA ORDEN DE VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> ObtenerOrdenVentaParaClonar(long idOrden)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                int idCentroAtencionQueTieneStockIntegrado = ProfileData().IdCentroAtencionQueTieneElStockIntegrada;
                long idTransaccionInventarioFisico = ProfileData().ObtenerIdInventarioActual(idCentroAtencionQueTieneStockIntegrado);

                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, idOrden);
                List<SelectorTipoDeComprobante> comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.Venta, ProfileData());
                var registro = new RegistroVentaViewModel(ordenDeVenta, comprobantes);
                foreach (var item in registro.Detalles)
                {
                    item.ConceptoCompleto = conceptoLogica.ObtenerConceptoDeNegocioComercialPorIdConcepto(ProfileData(), item.Producto.Id, true, true);
                }
                return Json(registro);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER LA ORDEN DE VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region VENTA CORPORATIVA



        public List<Detalle_transaccion> ConstruirDetalleMovimientoMercaderia(RegistroMovimientoDeAlmacenViewModel salidaMercaderia)
        {
            List<Detalle_transaccion> detallesConstruidos = new List<Detalle_transaccion>();
            foreach (var item in salidaMercaderia.Detalles)
            {
                detallesConstruidos.Add(new Detalle_transaccion(item.IngresoSalidaActual, item.IdProducto, null, 1, 1, null, 0, null, null, 0, 0, 0, item.Lote, null, null));
            }
            return detallesConstruidos;
        }

        public JsonResult GuardarVentaCorporativa(RegistroVentaViewModel venta)
        {
            try
            {
                OperationResult result;
                if (venta.IdVenta != 0)
                {
                    result = EditarVenta(venta);
                }
                else
                {
                    result = RegistrarVenta(venta);
                }
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL REGISTRAR LA VENTA");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR REGISTRAR LA VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }

        public OperationResult RegistrarVenta(RegistroVentaViewModel venta)
        {
            try
            {
                OperationResult result = null;
                List<DetalleDeOperacion> detalles = RegistroDetalleVentaViewModel.Convert(venta.Detalles.ToList());
                if (venta.EsVentaACredito)
                {
                    if (venta.EsCreditoRapido)
                    {
                        result = null;//operacionLogica.RegistrarVentaAlCreditoRapido(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                      // venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                      // venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                      //  venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete);
                    }
                    else
                    {//VENTA A CREDITO CONFIGURADO

                        result = null;//operacionLogica.RegistrarVentaAlCredito(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                      //    venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                      //    venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                      //    venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete, ConstruirCuotas(venta.Cuotas.ToList()), venta.Inicial);
                    }
                }
                else
                {
                    result = null; //operacionLogica.RegistrarVentaAlContado(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                   //venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                   //venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                   // venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult EditarVenta(RegistroVentaViewModel venta)
        {
            try
            {
                OperationResult result = null;
                List<DetalleDeOperacion> detalles = RegistroDetalleVentaViewModel.Convert(venta.Detalles.ToList());
                if (venta.EsVentaACredito)
                {
                    if (venta.EsCreditoRapido)
                    {
                        result = null;//operacionLogica.EditarVentaAlCreditoRapido(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                      //    venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                      //     venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                      //    venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete);
                    }
                    else
                    {//VENTA A CREDITO CONFIGURADO
                        result = null;// operacionLogica.EditarVentaAlCredito(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias, venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada, venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago, venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete, ConstruirCuotas(venta.Cuotas.ToList()), venta.Inicial);
                        result = null;//operacionLogica.EditarVentaAlCredito(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                      //venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                      //venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                      //venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete, ConstruirCuotas(venta.Cuotas.ToList()), venta.Inicial);
                    }
                }
                else
                {
                    result = null;// operacionLogica.EditarVentaAlContado(ProfileData().Empleado.Id, ModoOperacionEnum.PorMostrador, ProfileData().IdCentroDeAtencionSeleccionado, venta.Cliente.Id, venta.Alias,
                                  //venta.TipoDeComprobante.TipoComprobante.Id, venta.TipoDeComprobante.SerieSeleccionada == 0 ? venta.TipoDeComprobante.Series.First().Id : venta.TipoDeComprobante.SerieSeleccionada,
                                  //venta.TipoDeComprobante.NumeroIngresado, venta.GrabaIgv, venta.Observacion, venta.FechaRegistro, venta.EsVentaPasada, venta.IdMedioDePago,
                                  //venta.EntidadFinanciera != null ? venta.EntidadFinanciera.Id : 0, venta.InformacionDeMedioPago ?? "", detalles, venta.DetalleUnificado, venta.Flete);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Cuota> ConstruirCuotas(List<RegistroDetalleFinanciamientoViewModel> cuotas)
        {
            List<Cuota> cuotasConstruidas = new List<Cuota>();
            foreach (var item in cuotas)
            {
                cuotasConstruidas.Add(new Cuota()
                {
                    codigo = "",
                    fecha_emision = item.FechaVencimiento,
                    fecha_vencimiento = item.FechaVencimiento,
                    capital = item.CapitalCuota,
                    interes = item.InteresCuota,
                    total = item.ImporteCuota,
                    por_cobrar = false,
                    cuota_inicial = item.EsCuotaInicial
                });
            }
            return cuotasConstruidas;
        }

        public List<MovimientoDeAlmacen> ConstruirSalidasDeMercaderia(List<RegistroMovimientoDeAlmacenViewModel> registros)
        {
            List<MovimientoDeAlmacen> salidasDeMercaderia = new List<MovimientoDeAlmacen>();
            foreach (var registro in registros)
            {
                salidasDeMercaderia.Add(new MovimientoDeAlmacen(new Transaccion()
                {
                    Detalle_transaccion = ConstruirDetalleMovimientoMercaderia(registro),
                    comentario = registro.Observacion,
                })
                {
                    IdComprobanteSeleccionado = registro.TipoDeComprobante.TipoComprobante.Id,
                    IdSerieSeleccionada = registro.TipoDeComprobante.SerieSeleccionada,
                    EsPropio = registro.TipoDeComprobante.EsPropio,
                    SerieIngresada = registro.TipoDeComprobante.SerieIngresada,
                    NumeroIngresado = registro.TipoDeComprobante.NumeroIngresado,
                    FechaInicioTraslado = registro.FechaInicioTraslado,
                    IdTransportista = registro.Transportista.Transportista.Id,
                    Placa = registro.Transportista.Placa,
                    IdConductor = registro.Conductor.Conductor.Id,
                    NumeroLicencia = registro.Conductor.NumeroLicencia,
                    IdModalidadTransporte = registro.ModalidadTransporte.Id,
                    IdMotivoTraslado = registro.MotivoTraslado.Id,
                    PesoBrutoTotal = registro.PesoBrutoTotal,
                    NumeroBultos = registro.NumeroBultos,
                    DireccionOrigen = registro.DireccionOrigen + " - " + registro.UbigeoOrigen.Nombre,
                    DireccionDestino = registro.DireccionDestino + " - " + registro.UbigeoDestino.Nombre,
                    IdUbigeoOrigen = registro.UbigeoOrigen.Id,
                    IdUbigeoDestino = registro.UbigeoDestino.Id
                });
            }
            return salidasDeMercaderia;
        }



        public JsonResult ContabilizarVentaCorporativa(long idOrdenVenta)
        {
            try
            {
                //OrdenDeVenta resultado = operacionLogica.obtenerOrdenDeCompra(idOrdenCompra);
                return null;/*Json(new VentaConDetallesViewModel(resultado));*/
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ConfirmarVentaCorporativa(long idVenta, RegistroCobroYPagoCuotaViewModel trazaPagoInicial)
        {
            try
            {
                OperationResult result = null;
                //result = operacionLogica.ConfirmarVentaCorporativa(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, idVenta, trazaPagoInicial.MedioDePago.Id, trazaPagoInicial.EntidadFinanciera.Id, trazaPagoInicial.InformacionDePago);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL CONFIRMAR LA COMPRA");
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult RegistrarComprobanteCompraCorporativa(long idCompra, SelectorTipoDeComprobante comprobante)
        {
            try
            {
                OperationResult result = null;
                //result = operacionLogica.RegistrarComprobanteCompraCorporativa(ProfileData().Empleado.Id, ProfileData().IdEntidadInternaSeleccionada, idCompra, comprobante.TipoComprobante.Id, comprobante.SerieSeleccionada, comprobante.EsPropio, comprobante.SerieIngresada, comprobante.NumeroIngresado);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL REGISTRAR EL COMPROBANTE LA COMPRA");
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

     

        #region VENTAS Y COBROS MASIVOS

        public JsonResult GuardarVentasYCobrosPorVendedor(RegistroDeVentasYCobrosViewModel ventasYCobrosPorVendedor)
        {
            try
            {
                VentaMasiva venta = RegistroDeVentasYCobrosViewModel.ConvertVenta(ventasYCobrosPorVendedor);
                CobranzaMasiva cobranza = RegistroDeVentasYCobrosViewModel.ConvertCobranza(ventasYCobrosPorVendedor);
                VentaYCobranzaCarteraDeClientes ventaCobranzaMasiva = new VentaYCobranzaCarteraDeClientes(ventasYCobrosPorVendedor.PuntoDeVenta.Id, ventasYCobrosPorVendedor.PuntoDeVenta.Nombre, ventasYCobrosPorVendedor.Fecha, venta, cobranza);
                OperationResult result = operacionLogica.ConfirmarVentasAlCreditoYCobranzasCarteraCliente(ProfileData(), ventaCobranzaMasiva);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar guardar las ventas y cobros");
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

        public JsonResult ObtenerDeudasMasivas(int idPuntoDeVenta, DateTime fecha)
        {
            try
            {
                //var resultados = _actorNegocioLogica.ObtenerClientesSegunElCentroDeAtencionYElTipoDeVinculo(idCentroDeAtencion, tipoDeVinculo);
                var resultados = operacionLogica.ObtenerDeudasMasivasDeCarteraDeClientes(idPuntoDeVenta, fecha);
                List<RegistroClienteVentaYCobroViewModel> ventasYCobrosViewModel = RegistroClienteVentaYCobroViewModel.Convert(resultados);
                ventasYCobrosViewModel = ventasYCobrosViewModel.OrderBy(vcv => vcv.Cliente.Nombre).ToList();
                return Json(ventasYCobrosViewModel);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// METODO QUE SE UTILIZAN EN LA BANDEJA DE VENTAS Y COBROS POR VENDEDOR
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public JsonResult ObtenerVentasYCobrosMasivos(string desde, string hasta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            try
            {
                List<BandejaVentasYCobrosPorVendedorViewModel> respuesta = BandejaVentasYCobrosPorVendedorViewModel.Convert(operacionLogica.ObtenerVentasYCobranzasMasivas(fechaDesde, fechaHasta));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerVentasYCobrosMasivo(int idCentroDeAtencion, string fecha, int idTransaccion)
        {
            DateTime fechaDeVentaOCobranza = DateTime.Parse(fecha);
            try
            {
                VentaYCobroPorVendedorViewModel respuesta = new VentaYCobroPorVendedorViewModel(operacionLogica.ObtenerVentasYCobranzasMasiva(idTransaccion),
                    operacionLogica.ObtenerDeudasMasivasDeCarteraDeClientes(idCentroDeAtencion, fechaDeVentaOCobranza));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region VENTA MASIVA

        public JsonResult GuardarVentaMasiva(RegistroVentaMasivaViewModel ventaMasiva)
        {
            try
            {
                VentaMasiva venta = RegistroVentaMasivaViewModel.ConvertVenta(ventaMasiva);
                OperationResult result = operacionLogica.ConfirmarVentaMasiva(ProfileData(), venta);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar guardar las ventas y cobros");
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

        #region IMPRESION DE COMPROBANTES

        [AllowAnonymous]
        public ActionResult Imprimir()
        {
            ViewBag.Message = "Your application description page.";
            var imageArray = barCodeUtil.ObtenerCodigoQR("atentos QR codigo");

            //BoletaDeVenta comprobanteBoleta = new BoletaDeVenta
            //{
            //    FechaEmision = DateTime.Now,
            //    Emisor = new Emisor { LogoBytes = imageArray, RazonSocial = "COMERCIAL LOQUITO sac", NombreComercial = "COMERCIAL LOCO", RUC = "20154548741", Direccion = "JR. HUALLAGA 123" },
            //    Receptor = new Receptor { RazonSocial = "Juan Perez", TipoDocumentoIdentidad = "DNI", DocumentoIdentidad = "41302565", Direccion = "JR. el cliente 567" },
            //    Detalles = new List<Detalle> { new Detalle { Cantidad = 2, ImporteTotal = 20, ImporteUnitario = 10, Concepto = "Clavo" },
            //        new Detalle { Cantidad = 3, ImporteTotal = 2, ImporteUnitario = 6, Concepto = "Chinche" } },
            //    MostrarMensajeAmazonia = true, ResolucionAutorizacionSunat="1920050000097SUNAT",  MensajeNegocio = "¡¡¡Gracias por su preferencia, vuelva pronto!!!", MostrarLogo = true, CodigoQR= imageArray, MostrarTestigo=true, Serie="B001", Numero="1152" };

            //Factura comprobanteFactura = new Factura
            //{
            //    FechaEmision = DateTime.Now,
            //    Emisor = new Emisor { OtrosDatosContacto="TF 51-062-568954, 948758769", LogoBytes = imageArray, RazonSocial = "COMERCIAL LOQUITO sac", NombreComercial = "COMERCIAL LOCO", RUC = "20154548741", Direccion = "JR. HUALLAGA 123" },
            //    Receptor = new Receptor { RazonSocial = "Juan Perez", TipoDocumentoIdentidad = "DNI", DocumentoIdentidad = "41302565", Direccion = "JR. el cliente 567" },
            //    Detalles = new List<Detalle> { new Detalle { Cantidad = 2, ImporteTotal = 20, ImporteUnitario = 10, Concepto = "Clavo" },
            //        new Detalle { Cantidad = 3, ImporteTotal = 2, ImporteUnitario = 6, Concepto = "Chinche" } },
            //    MostrarMensajeAmazonia = true,
            //    ResolucionAutorizacionSunat = "1920050000097SUNAT",
            //    MensajeNegocio = "¡¡¡Gracias por su preferencia, vuelva pronto!!!",
            //    Observacion = "Mercaderia verificada por personal antes de la entrega",
            //    MostrarLogo = true,
            //    CodigoQR = imageArray,
            //    Serie = "B001",
            //    Numero = "1152",
            //    ImporteTotalEnLetras = Utilitarios.NumerosALetras.Obtener(50.19d)                
            //};
            var Renderer = new SelectPdf.HtmlToPdf();

            Renderer.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
            Renderer.Options.PdfPageCustomSize = new SizeF(217, 290);

            //Renderer.Options.PdfPageSize = SelectPdf.PdfPageSize.Custom;
            //Renderer.Options.PdfPageCustomSize = new SizeF(81, 210);
            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 1;

            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;

            //var pdf = Renderer.ConvertHtmlString(RenderRazorViewToString("BoletaDeVenta80", comprobanteBoleta));
            var pdf = Renderer.ConvertHtmlString(HtmlStringBuilder.RenderRazorViewToString("FacturaA4", null, this));

            return base.File(pdf.Save(), "application/pdf");
        }

        public void PrintFileTmp(long idVenta)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVenta(idVenta);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var PDFventa = ObtenerPdfVenta(orden, sede, QrBytes, FormatoImpresion._80mm);
            PrintFilePDF file = null;
            file = new PrintFilePDF(PDFventa.Save(), idVenta + ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// SE RECIBE EL ID DE LA ORDEN DE VENTA
        /// </summary>
        /// <param name="idVenta"></param>
        public void PrintFile__(long idVenta)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVenta(idVenta);
            PrintFile_(orden);
        }

        public void PrintFile(long idVenta)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVentaParaImprimir(ObtenerOrdenDeVentaParaImprimirEnAplicacion(idVenta));
            EliminarOrdenDeVentaParaImprimirEnAplicacion(idVenta);
            PrintFile_(orden);
        }

        public void PrintFile_(OrdenDeVenta orden)
        {

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var PDFventa = ObtenerPdfVenta(orden, sede, QrBytes, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto);

            //Si tiene asignado guias de remision
            if (orden.TieneGuiaDeRemision())
            {
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = maestroLogica.ObtenerModalidadesTraslado();
                var motivosDeTransporte = maestroLogica.ObtenerMotivosTraslado();
                var salidaDeMercaderia = operacionLogica.ObtenerSalidaDeMercaderiaDeVenta(orden.IdVenta);
                foreach (var salida in salidaDeMercaderia)
                {
                    int[] idsUbigeos = { Convert.ToInt32(salida.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(salida.IdUbigeoDestinoDeTraslado()) };
                    var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                    salida.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(salida.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                    salida.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(salida.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                    byte[] byteQr = barCodeUtil.ObtenerCodigoQR(salida.UrlDocumentoSunat);
                    PDFventa.Append(PdfBuilder.ObtenerPdfMovimientoDeMercaderia(salida, sede, byteQr, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto, proveedores, modalidadesDeTransporte, motivosDeTransporte, this));
                }
            }
            PrintFilePDF file = null;
            file = new PrintFilePDF(PDFventa.Save(), orden.Id + ".pdf");
            file.PrintRotation = PrintRotation.None;

            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();

            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();

        }

        public void ImprimirComprobantesVentasMasivas(DateTime fecha)
        {

            List<OrdenDeVenta> ordenes = operacionLogica.ObtenerVentasMasivasPorFecha(fecha);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            SelectPdf.PdfDocument pdfGeneral = new SelectPdf.PdfDocument();
            foreach (var orden in ordenes)
            {
                string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
                byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                var PDFventa = ObtenerPdfVenta(orden, sede, QrBytes, FormatoImpresion._80mm);
                pdfGeneral.Append(PDFventa);
            }

            PrintFilePDF file = null;
            file = new PrintFilePDF(pdfGeneral.Save(), ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        public System.Drawing.Image ObtenerImageVenta(OrdenDeVenta ordenDeVenta, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {

            string result = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, formato, qrBytes, sede, this, maestroLogica);

            HtmlToImage imgConverter = new HtmlToImage();
            imgConverter.WebPageFixedSize = false;

            if (formato == FormatoImpresion._80mm)
            {
                imgConverter.WebPageFixedSize = false;
                imgConverter.WebPageWidth = 284;
                //imgConverter.WebPageHeight = 3508;

            }
            else if (formato == FormatoImpresion.A4)
            {
                imgConverter.WebPageWidth = 2480;
                //imgConverter.WebPageHeight = 3508;
                //imgConverter.WebPageHeight = 270;
                //imgConverter.WebPageFixedSize = true;
            }
            else if (formato == FormatoImpresion._56mm)
            {
                imgConverter.WebPageFixedSize = true;
                imgConverter.WebPageWidth = 661;//in Pixels
                                                //imgConverter.WebPageHeight = 3508;

            }
            // create a new image converting an url
            System.Drawing.Image image = imgConverter.ConvertHtmlString(result);


            return image;
        }

        public string CabeceraVoucher(string idDocumento)
        {

            string cabecera = "";
            //cabecera += new string(' ', 20);
            cabecera += "\n " + idDocumento;
            //cabecera += "\n" + new string(' ', 20);
            //cabecera += "\n" + new string(' ', 20);

            return cabecera;
        }

        /// <summary>
        /// OBTIENE EL COMPROBANTE DE LA VENTA, INGRESANDO LA ORDEN DE LA VENTA
        /// </summary>
        /// <param name="idOrdenVenta"></param>
        /// <returns></returns>
        public ActionResult ObtenerComprobanteOrdenVentaPDF(long idOrdenVenta)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVenta(idOrdenVenta);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();

            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var pdf = ObtenerPdfVenta(orden, sede, QrBytes, FormatoImpresion._80mm);
            pdf.ViewerPreferences.FitWindow = false;

            return base.File(pdf.Save(), "application/pdf");

        }

        public ActionResult ObtenerComprobanteVentaHtml(long idVenta, FormatoImpresion formato)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVenta(operacionLogica.obtenerIdOrdenDeVenta(idVenta));
            //Sede sede = ProfileData().Sede;
            var sede = ObtenerSede();

            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);

            return null;// ObtenerHtmlString(orden,formato,QrBytes,sede);

        }

        public ActionResult ObtenerComprobanteVentaPDF(long idVenta)
        {
            OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVenta(operacionLogica.obtenerIdOrdenDeVenta(idVenta));
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var PDFventa = ObtenerPdfVenta(orden, sede, QrBytes, FormatoImpresion._80mm);

            PDFventa.ViewerPreferences.FitWindow = false;

            return base.File(PDFventa.Save(), "application/pdf");
        }

        #endregion

        #region INVALIDACIUON Y ANULACION DE VENTA

        public JsonResult InvalidarVenta(InvalidarVenta invalidarVenta)
        {
            try
            {
                OperationResult result = operacionLogica.InvalidarVenta(invalidarVenta, ProfileData());
                Util.VerificarError(result);
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

        public JsonResult AnularVenta(RegistroAnulacionVentaViewModel anulacion)
        {
            try
            {
                if (anulacion.TipoDeComprobante.Series == null || anulacion.TipoDeComprobante.Series.Count < 1)
                {
                    throw new LogicaException("El centro de atención no cuenta con series para el tipo de comprobante seleccionado.");
                }

                OperationResult result = operacionLogica.AnularVenta(anulacion.Id, ProfileData().Empleado.Id,
                        ProfileData().IdCentroDeAtencionSeleccionado, anulacion.TipoDeComprobante.SerieSeleccionada == 0 ? anulacion.TipoDeComprobante.Series.First().Id : anulacion.TipoDeComprobante.SerieSeleccionada,
                        anulacion.Observacion);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar anular Venta");
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
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

        public JsonResult InvalidarAnulacionDeVenta(long idOrden, string observacion)
        {
            try
            {
                OperationResult result = operacionLogica.InvalidarAnulacionDeVenta(idOrden, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, observacion);

                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
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

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaAnulacionVenta(string serieComprobanteVenta)
        {
            try
            {
                var resultados = await configuracionLogica.ObtenerTiposDeComprobanteParaAnulacionVenta(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados, ProfileData().IdCentroDeAtencionSeleccionado, serieComprobanteVenta.Substring(0, 1));

                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerOrdenVentaYDetallesParaAnulacionDeVenta(long idOrdenVenta)
        {
            try
            {
                OrdenDeVenta resultado = operacionLogica.ObtenerOrdenDeVentaConIdOrden(idOrdenVenta);
                return Json(new RegistroAnulacionVentaViewModel(resultado));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        #region REPORTE DE VENTAS
        [Authorize(Roles = "Vendedor")]
        public ActionResult ReportesVendedor()
        {
            //List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            ViewBag.diasAntesDisponibles = BaseController.FormatearParametroDiasAntesDisponiblesParaReporte(ReporteSettings.Default.DiasAntesDisponiblesReporteVendedor);
            ViewBag.parametrosReportesVendedor = ConfiguracionReportesVendedor.Default;


            return View();
        }
        [Authorize(Roles = "Gerente,Contador,AdministradorNegocio")]
        public ActionResult ReportesAdministrador()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            //List<string> fechas = operacionLogica.ObtenerFechaIncioyFinConPrecisionDeMilisegundosParaReporteVentaPuntoDeVenta();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            ViewBag.parametrosReportesAdministrador = ConfiguracionReportesAdministrador.Default;
            return View();
        }

        public ActionResult ReportesCajero()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaHoraInicio = fechas[0];
            ViewBag.fechaHoraFin = fechas[1];
            ViewBag.diasAntesDisponibles = BaseController.FormatearParametroDiasAntesDisponiblesParaReporte(ReporteSettings.Default.DiasAntesDisponiblesReporteVendedor);
            return View();
        }



        #endregion

        #region OBTENCION DE DATOS
        public JsonResult ObtenerParametrosDeFacturacion()
        {
            try
            {
                var data = new ConfiguracionVenta
                {
                    FechaActual = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy")
                };
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }


        public JsonResult ObtenerParametrosParaPago()
        {
            try
            {
                var data = new ConfiguracionPago
                {
                    FechaActual = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy hh:mm:ss")
                };
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }


        public JsonResult ObtenerParametrosParaTrazaDePago()
        {
            try
            {
                var data = ConfiguracionTrazaDePago.Default;
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerParametrosParaNota()
        {
            try
            {
                var data = new ConfiguracionNota();
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }


        public JsonResult ObtenerParametrosParaRegistroDetalles()
        {
            try
            { 
                string mascaraDeIngreso = VentasSettings.Default.MascaraDeCamposAIngresarEnVentas;
                string mascaraDeCalculo = VentasSettings.Default.MascaraFormasDeCalculoEnVentas;
                var data = new ConfiguracionRegistroDetalle
                {
                    SalidaBienesSujetasADisponibilidadStock = !ProfileData().CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock,
                    PermitirIngresarCantidad = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Cantidad),
                    PermitirIngresarPrecioUnitario = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.PrecioUnitario),
                    PermitirIngresarImporte = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Importe),
                    IngresarCantidadCalcularPrecioUnitario = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Cantidad, ElementoDeCalculoEnVentasEnum.PrecioUnitario),
                    IngresarPrecioUnitarioCalcularImporte = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.PrecioUnitario, ElementoDeCalculoEnVentasEnum.Importe),
                    IngresarImporteCalcularCantidad = ventaUtil.ObtenerFormasDeCalculosEnVentas(mascaraDeCalculo, ElementoDeCalculoEnVentasEnum.Importe, ElementoDeCalculoEnVentasEnum.Cantidad),
                    CostoUnitarioDelIcbper = ProfileData().CostoUnitarioDelIcbper
                };
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }



        public JsonResult CancelarOrdenDeVenta(RegistroVentaViewModel facturacion)
        {
            try
            {
                OperationResult result = operacionLogica.cancelarOrdenDeVenta(facturacion.IdVenta, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult CargarOperacionesDeVenta(long[] ids)
        {
            try
            {
                List<OrdenDeVenta> ordenesDeVentas = operacionLogica.ObtenerOrdenesDeVenta(ids);
                List<OrdenVentaCompraViewModel> documentos = OrdenVentaCompraViewModel.convert(ordenesDeVentas);
                return Json(documentos);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerSeriesConTipoComprobanteParaVenta()
        {
            try
            {
                var seriesConTipoComprobante = await configuracionLogica.ObtenerSeriesConTipoComprobante(TipoComprobantePara.VentasYSusNotas, ProfileData());
                return Json(seriesConTipoComprobante);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaVenta()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.Venta, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteConSeriesAutonumericasParaVenta()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.SeriesAutonumericasParaVenta, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteConSeriesAutonumericasParaVentaExcluidoFactura()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.SeriesAutonumericasParaVentaExcluidoFactura, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e) { return Json(Util.ErrorJson(e)); }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaVentasPorContingencia()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.VentasPorContingencia, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e) { return Json(Util.ErrorJson(e)); }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaReporteDeVenta()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.ReporteDeVenta, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e) { return Json(Util.ErrorJson(e)); }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaVentasYSusNotas()
        {
            try
            {
                var comprobantes = await configuracionLogica.ObtenerSelectorDeComprobante(TipoComprobantePara.VentasYSusNotas, ProfileData());
                return Json(comprobantes);
            }
            catch (Exception e) { return Json(Util.ErrorJson(e)); }
        }



        public async Task<JsonResult> ObtenerTiposDeComprobanteParaClientes()
        {
            try
            {
                var resultados = await configuracionLogica.ObtenerTiposDeComprobanteParaVenta();
                List<ComboGenericoViewModel> comprobantes = ComboGenericoViewModel.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaAnularVenta()
        {
            try
            {
                var resultados = await configuracionLogica.ObtenerTiposDeComprobanteParaAnularVenta();
                List<TipoComprobanteViewModel> comprobantes = TipoComprobanteViewModel.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaDescuentoSobreVenta()
        {
            try
            {
                var resultados = await configuracionLogica.ObtenerTiposDeComprobanteParaDescuentoSobreVenta();
                List<TipoComprobanteViewModel> comprobantes = TipoComprobanteViewModel.Convert(resultados);

                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaRecargoSobreVenta()
        {
            try
            {
                var resultados = await configuracionLogica.ObtenerTiposDeComprobanteParaRecargoSobreVenta();
                List<TipoComprobanteViewModel> comprobantes = TipoComprobanteViewModel.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerMediosDePagoVenta()
        {
            try
            {

                var resultados = await operacionLogica.ObtenerMediosDePagoVenta();
                List<ComboGenericoViewModel> mediosDePago = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    if(item.id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos)
                    {
                        if(VentasSettings.Default.UsarPuntosComoMedioDePago)
                            mediosDePago.Add(new ComboGenericoViewModel(item.id, item.valor));
                    }
                    else
                    {
                        mediosDePago.Add(new ComboGenericoViewModel(item.id, item.valor));
                    }
                }
                return Json(mediosDePago);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerProductosVenta()
        {
            try
            {
                List<ConceptoDeNegocio> resultados = conceptoLogica.ObtenerMercaderiasPorCentroAtencionIncluyendoStockYPrecios(ProfileData().IdCentroAtencionQueTieneLosPrecios);
                List<ProductoParaVentaViewModel> productos = new List<ProductoParaVentaViewModel>();
                foreach (var item in resultados)
                {
                    productos.Add(new ProductoParaVentaViewModel(item, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().IdCentroDeAtencionSeleccionado));
                }
                return Json(productos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        [Obsolete("Metodo cambiado por una clase personalizada")]
        public JsonResult ObtenerMercaderiaPorCodigoBarra(string codigoBarra)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                int idCentroAtencionQueTieneLaExistencia = ProfileData().IdCentroAtencionQueTieneElStockIntegrada;
                ConceptoDeNegocio resultado = conceptoLogica.ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPreciosConUnPrecioComoMinimo(ProfileData().IdCentroAtencionQueTieneLosPrecios, codigoBarra);
                if (resultado != null)
                {
                    ProductoParaVentaViewModel producto = new ProductoParaVentaViewModel(resultado, idCentroAtencionQueTieneLosPrecios, idCentroAtencionQueTieneLaExistencia);
                    return Json(producto);
                }
                else
                {
                    throw new AdvertenciaException("Producto no registrado o no tiene precio");
                }
            }
            catch (AdvertenciaException e)
            {
                return new JsonHttpStatusResult(new { error = e.Message, warning = true }, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        [Obsolete("Metodo cambiado por una clase personalizada")]
        public JsonResult ObtenerProductosVentaPorConceptos(int idConceptoBasico)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios =/* TransaccionSettings.Default.PreciosCentralizados ? ActorSettings.Default.IdActorNegocioSede :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios;

                int idCentroAtencionQueTieneLaExistencia = /*AplicacionSettings.Default.StockCentralizado ? ActorSettings.Default.IdActorNegocioSede :*/ ProfileData().IdCentroAtencionQueTieneElStockIntegrada;

                List<ConceptoDeNegocio> resultados = conceptoLogica.ObtenerMercaderiasIncluyendoStockYPrecios(idConceptoBasico, ProfileData().IdCentroAtencionQueTieneLosPrecios);

                List<ProductoParaVentaViewModel> productos = new List<ProductoParaVentaViewModel>();
                foreach (var item in resultados)
                {
                    productos.Add(new ProductoParaVentaViewModel(item, idCentroAtencionQueTieneLosPrecios, idCentroAtencionQueTieneLaExistencia));
                }
                return Json(productos);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerModalidadesGenerico()
        {
            try
            {
                List<ComboGenericoViewModel> modalidadesViewModel = new List<ComboGenericoViewModel>();

                foreach (ModoOperacionEnum operacion in Enum.GetValues(typeof(ModoOperacionEnum)))
                {
                    modalidadesViewModel.Add(new ComboGenericoViewModel((int)operacion, Enumerado.GetDescription((operacion))));
                }
                return Json(modalidadesViewModel);
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener las modalidades de venta", e);
            }
        }

        #endregion

        #region  COMPROBANTES DE COBRO MASIVO

        public PdfDocument ObtenerPdfCobro(MovimientoEconomico cobro, EstablecimientoComercialExtendidoConLogo sede, FormatoImpresion formato)
        {
            string result = CoreHtmlStringBuilder.ObtenerHtmlString(cobro, formato, sede, this);
            var Renderer = new SelectPdf.HtmlToPdf();
            if (formato == FormatoImpresion._80mm)
            {
                Renderer.Options.PdfPageSize = SelectPdf.PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(81, 320);
            }
            else
            {
                Renderer.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
            }

            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 1;
            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;
            return Renderer.ConvertHtmlString(result);
        }

        public void ImprimirComprobrobantesCobrosMasivos(DateTime fecha)
        {

            List<MovimientoEconomico> cobros = operacionLogica.ObtenerCobrosMasivosPorFecha(fecha);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            SelectPdf.PdfDocument pdfGeneral = new SelectPdf.PdfDocument();
            foreach (var cobro in cobros)
            {
                var PDFcobro = ObtenerPdfCobro(cobro, sede, FormatoImpresion._80mm);
                pdfGeneral.Append(PDFcobro);
            }
            PrintFilePDF file = null;
            file = new PrintFilePDF(pdfGeneral.Save(), ".pdf");
            file.PrintRotation = PrintRotation.None;
            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.ClientPrinter = new DefaultPrinter();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        #endregion

        #region VISUALIZACION Y ENVIO DE COMPROBANTES

        public JsonResult ObtenerDocumentoParaOperacionesEnVenta(long idOrdenDeVenta)
        {
            try
            {
                DocumentoDeOperacionViewModel respuesta = new DocumentoDeOperacionViewModel();
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
                string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                var htmlString80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytes, sede, this, maestroLogica);
                var htmlStringA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                if (ordenDeVenta.TieneGuiaDeRemision())
                {
                    List<string> cadenasHtmlDeGuiaDeRemision80 = new List<string>();
                    List<string> cadenasHtmlDeGuiaDeRemisionA4 = new List<string>();
                    var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                    var modalidadesDeTransporte = maestroLogica.ObtenerModalidadesTraslado();
                    var motivosDeTransporte = maestroLogica.ObtenerMotivosTraslado();
                    var salidaDeMercaderia = operacionLogica.ObtenerSalidaDeMercaderiaDeVenta(ordenDeVenta.IdVenta);
                    foreach (var salida in salidaDeMercaderia)
                    {
                        byte[] byteQr = barCodeUtil.ObtenerCodigoQR(salida.UrlDocumentoSunat);
                        cadenasHtmlDeGuiaDeRemision80.Add(CoreHtmlStringBuilder.ObtenerHtmlString(salida, FormatoImpresion._80mm, byteQr, sede, proveedores, modalidadesDeTransporte, motivosDeTransporte, this));
                        cadenasHtmlDeGuiaDeRemisionA4.Add(CoreHtmlStringBuilder.ObtenerHtmlString(salida, FormatoImpresion.A4, byteQr, sede, proveedores, modalidadesDeTransporte, motivosDeTransporte, this));
                    }
                    respuesta = new DocumentoDeOperacionViewModel(ordenDeVenta, htmlString80, htmlStringA4, ordenDeVenta.TieneGuiaDeRemision(), cadenasHtmlDeGuiaDeRemision80, cadenasHtmlDeGuiaDeRemisionA4, Encriptar(ordenDeVenta.Id.ToString()));
                }
                else
                {
                    respuesta = new DocumentoDeOperacionViewModel(ordenDeVenta, htmlString80, htmlStringA4, false, null, null, Encriptar(ordenDeVenta.Id.ToString()));
                }
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
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

        public JsonResult ObtenerFormatosDeImpresionSolo80mm()
        {
            var valoresEnum = Enum.GetValues(typeof(FormatoImpresion));
            List<ComboGenericoViewModel> formatos = new List<ComboGenericoViewModel>();
            foreach (var item in valoresEnum)
            {
                if ((int)item == (int)FormatoImpresion._80mm)
                    formatos.Add(new ComboGenericoViewModel((int)item, ((FormatoImpresion)Convert.ToInt32(item)).ToString()));
            }
            return Json(formatos);
        }
        public JsonResult ObtenerHtmlVenta(long idOrdenDeVenta, int formato)
        {
            OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, (FormatoImpresion)formato, QrBytes, sede, this, maestroLogica);
            return Json(htmlString);
        }

        public JsonResult ObtenerHtmlVentaYGuiaDeRemision(long idOrdenDeVenta, int formato)
        {
            CadenasHtmlVentaYGuia respuesta = new CadenasHtmlVentaYGuia();

            OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var cadenaHtmlVenta = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, (FormatoImpresion)formato, QrBytes, sede, this, maestroLogica);

            List<string> cadenasHtmlDeGuiaDeRemision = new List<string>();
            var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
            var modalidadesDeTransporte = maestroLogica.ObtenerModalidadesTraslado();
            var motivosDeTransporte = maestroLogica.ObtenerMotivosTraslado();
            var salidaDeMercaderia = operacionLogica.ObtenerSalidaDeMercaderiaDeVenta(ordenDeVenta.IdVenta);
            foreach (var salida in salidaDeMercaderia)
            {
                byte[] byteQr = barCodeUtil.ObtenerCodigoQR(salida.Informacion);
                cadenasHtmlDeGuiaDeRemision.Add(CoreHtmlStringBuilder.ObtenerHtmlString(salida, (FormatoImpresion)formato, byteQr, sede, proveedores, modalidadesDeTransporte, motivosDeTransporte, this));
            }
            respuesta = new CadenasHtmlVentaYGuia(cadenaHtmlVenta, cadenasHtmlDeGuiaDeRemision);

            return Json(respuesta);
        }

        public JsonResult EnviarCorreoElectronicoConDocumento(long idOrdenDeVenta, int formato, List<string> correosElectronicos)
        {
            try
            {
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
                string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                List<LinkedResource> resources = new List<LinkedResource>();
                PdfDocument pdfVenta = ObtenerPdfVenta(ordenDeVenta, sede, QrBytes, (FormatoImpresion)formato);
                //Preparamos el correo electronico para enviarse
                string asunto = operacionLogica.ObtenerAsuntoDeCorreoElectronico(sede, ordenDeVenta);

                string cuerpo = operacionLogica.ObtenerCuerpoDeCorreoElectronico(sede, ordenDeVenta, Request.MapPath(Request.ApplicationPath), resources);
                var documentosAdjuntos = new List<Attachment>() { new Attachment(new MemoryStream(pdfVenta.Save()), ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".pdf", "application/pdf") };
                if (ordenDeVenta.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna)
                {
                    DocumentoXml documentoXml = XmlBuilder.ObtenerXmlComprobante(ordenDeVenta, sede, maestroLogica, generacionArchivosLogica, facturacionElectronicaLogica);
                    documentosAdjuntos.Add(new Attachment(new MemoryStream(documentoXml.Documento), documentoXml.NombreDocumento + ".xml", "application/xml"));
                }
                OperationResult result = mailer.Send(asunto, cuerpo, correosElectronicos, AplicacionSettings.Default.ToMailDefault, documentosAdjuntos, resources);
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }

        public ActionResult DescargarDocumento(long idOrdenDeVenta, int formato)
        {
            try
            {

                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
                return ObtenerDocumento(ordenDeVenta, (int)formato, ObtenerSede());
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento", e)), HttpStatusCode.InternalServerError);
            }
        }


        public ActionResult DescargarDocumentoConsultaComprobante(string Comprobante, string Serie, string Numero, DateTime FechaEmision, decimal Importe)
        {
            try
            {
                ConsultaComprobanteParameter consultaComprobante = new ConsultaComprobanteParameter { 
                 Comprobante=Comprobante,
                 FechaEmision=FechaEmision,
                 Importe=Importe,
                 Numero=Numero,                
                 Serie=Serie,
                };
                var formato = FormatoImpresion.A4;
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVentaConsultaComprobante(consultaComprobante);
                EstablecimientoComercialExtendidoConLogo sede = sedeLogica.ObtenerSedeConLogo();
                return ObtenerDocumento(ordenDeVenta, (int)formato, sede);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar descargar el documento", e)), HttpStatusCode.InternalServerError);
            }
                
            
        }


        public ActionResult ObtenerDocumento(OrdenDeVenta ordenDeVenta, int formato, EstablecimientoComercialExtendidoConLogo sede)
        {
            try
            {            
                string qrString = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                byte[] qrBytes = barCodeUtil.ObtenerCodigoQR(qrString);
                var pdfVenta = ObtenerPdfVenta(ordenDeVenta, sede, qrBytes, (FormatoImpresion)formato);

                string fileNameZip = ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".zip";
                byte[] fileBytes = null;
                byte[] compressedBytes;
                using (var outStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry fileInArchive = null;
                        fileInArchive = archive.CreateEntry(ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".pdf", CompressionLevel.Optimal);
                        fileBytes = pdfVenta.Save();
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(fileBytes))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                            fileToCompressStream.Close();
                        }
                        if (ordenDeVenta.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna)
                        {
                            fileInArchive = archive.CreateEntry(ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".xml", CompressionLevel.Optimal);
                            fileBytes = XmlBuilder.ObtenerXmlComprobante(ordenDeVenta, sede, maestroLogica, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                            using (var entryStream = fileInArchive.Open())
                            using (var fileToCompressStream = new MemoryStream(fileBytes))
                            {
                                fileToCompressStream.CopyTo(entryStream);
                                fileToCompressStream.Close();
                            }
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
        public ActionResult DescargarDocumentoPdf(long idOrdenDeVenta, int formato)
        {
            try
            {
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
                string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                PdfDocument pdfVenta = ObtenerPdfVenta(ordenDeVenta, sede, QrBytes, (FormatoImpresion)formato);
                byte[] fileBytes = pdfVenta.Save();
                string fileName = ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".pdf";
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

        public ActionResult DescargarDocumentoXml(long idOrdenDeVenta)
        {
            try
            {
                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
                OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(idOrdenDeVenta);
                byte[] fileBytes = XmlBuilder.ObtenerXmlComprobante(ordenDeVenta, sede, maestroLogica, generacionArchivosLogica, facturacionElectronicaLogica).Documento;
                string fileName = ordenDeVenta.Comprobante().NumeroDeSerie + " - " + ordenDeVenta.Comprobante().NumeroDeComprobante + " - " + ordenDeVenta.Cliente().RazonSocial + ".xml";
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

        #endregion

        #region OBTENCION DE DOCUMENTO PARA OPERACIONES
        public JsonResult ObtenerDetalleParaNotaDebitoCredito(int idOrdenVenta)
        {
            try
            {
                var ordenAlmacen = ordenAlmacenLogica.ObtenerOrdenAlmacen(idOrdenVenta);
                List<DetalleCuotaPago> detallesCuotas = operacionLogica.ObtenerDetallesCuotaPagoOperacion(idOrdenVenta); 
                return Json(new { detallesOrdenAlmacen = ordenAlmacen.Detalles, detallesTesoreria = detallesCuotas });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeVenta(bool esParaNotaDeDebito, string serieComprobanteVenta)
        {
            try
            {
                var resultados = esParaNotaDeDebito ? operacionLogica.ObtenerTiposDeComprobanteParaNotaDeDebito(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado) :
                    operacionLogica.ObtenerTiposDeComprobanteParaNotaDeCredito(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.ConvertirPropios(resultados, ProfileData().IdCentroDeAtencionSeleccionado, serieComprobanteVenta.Substring(0, 1));
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarNotaDeDebitoCreditoDeVenta(RegistroDeNotaViewModel registroDeNota)
        {
            try
            {
                List<DetalleOrdenDeNota> detalles = ConstruirDetalleTransaccionParaNotaDeVenta(registroDeNota);
                OperationResult resultado = operacionLogica.GuardarNotaVenta(registroDeNota.IdOrdenDeOperacion, registroDeNota.TipoNota.Id,  registroDeNota.Comprobante.EsPropio? 0 : registroDeNota.Comprobante.TipoComprobante.Id, registroDeNota.Comprobante.SerieSeleccionada, registroDeNota.Comprobante.EsPropio, registroDeNota.Comprobante.SerieIngresada, registroDeNota.Comprobante.NumeroIngresado, registroDeNota.MontoNota, 0, detalles, registroDeNota.EsDebito, registroDeNota.EsDiferida, registroDeNota.Observacion, registroDeNota.Pago, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL REGISTRAR LA NOTA");
                //Armar orden de venta para guardar para la impresion
                var ordenVenta = ArmarOrdenDeNotaParaImprimir((OrdenDeVenta)resultado.objeto, registroDeNota);
                //Guardar la orden de venta en una variable de aplicacion
                GuardarOrdenDeVentaParaImprimirEnAplicacion(ordenVenta);
                return Json(new { resultado.code_result, data = resultado.information, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL REGISTRAR LA NOTA", e)), HttpStatusCode.InternalServerError);
            }
        }

        public List<DetalleOrdenDeNota> ConstruirDetalleTransaccionParaNotaDeVenta(RegistroDeNotaViewModel registroDeNota)
        {
            List<DetalleOrdenDeNota> detallesConstruidos = new List<DetalleOrdenDeNota>();
            foreach (var item in registroDeNota.Detalles)
            {
                detallesConstruidos.Add(new DetalleOrdenDeNota()
                {
                    Cantidad = item.Cantidad,
                    Producto = new Concepto_Negocio_Comercial()
                    {
                        Id = item.IdConcepto,
                    },
                    Detalle = item.Observacion,
                    PrecioUnitario = item.Precio,
                    Importe = item.Importe,
                    Descuento = item.Descuento,
                    MontoDetalle = item.MontoDetalle,
                    MontoRevocado = item.MontoRevocado,
                    MontoDevuelto = item.MontoDevuelto,
                });
            }
            return detallesConstruidos;
        }

        #endregion

        #region REPORTES DEL VENDEDOR

        //DEL PUNTO DE VENTA POR COMPROBANTE    -- hecho
        public ActionResult ReporteDeVentaPorComprobanteDetalladoDeUnSoloPuntoDeVenta(string fechaInicio, string fechaFin)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);


            List<OrdenDeVenta> Confirmadas = operacionLogica.ObtenerOrdenesDeVentaConfirmadasYTransmitidas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
            List<OrdenDeVenta> Invalidadas = operacionLogica.ObtenerOrdenesDeVentaInvalidadas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);


            List<ResumenDeTransaccionVenta> rptVentaViewModelDetallesConfirmada = ResumenDeTransaccionVenta.Convert(Confirmadas);
            List<ResumenDeTransaccionVenta> rptVentaViewModelDetallesInvalidada = ResumenDeTransaccionVenta.Convert(Invalidadas);

            List<ResumenDeTransaccionVenta> reporteVentaViewModelResumen = ResumenDeTransaccionVenta.Resumen(rptVentaViewModelDetallesConfirmada);

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentaPorSerieYNumeroDeComprobante.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceResumen = new ReportDataSource("ResumenDataSet", reporteVentaViewModelResumen);

            ReportDataSource rptdatasourceDetalleConfirmada = new ReportDataSource("DetallesDataSet", rptVentaViewModelDetallesConfirmada);
            ReportDataSource rptdatasourceDetalleInvalidada = new ReportDataSource("InvalidadaDetallesDataSet", rptVentaViewModelDetallesInvalidada);


            rptviewer.LocalReport.DataSources.Add(rptdatasourceResumen);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleInvalidada);


            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DEL PUNTO DE VENTA POR CONCEPTO -- copia del admi
        public ActionResult ReporteDeVentaPorConceptoDeUnSoloPuntoDeVenta(string fechaInicio, string fechaFin) //XY4
        {
            try
            {

           
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);



            List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ventasConfirmadasYTransmididas = operacionLogica.ObtenerOrdenesDeVentaConfirmadasYTransmitidasPorConceptoBasicoYDeNegocio(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
            List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ventasInvalidadas = operacionLogica.ObtenerOrdenesDeVentaInvalidadasPorConceptoBasicoYDeNegocio(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);

                EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();

                var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreDeSede = new ReportParameter("NombreSede", sede.Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorConcepto.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreDeSede, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptVentasConfirmadasTransmitida = new ReportDataSource("dataSetVentaConfirmadas", ventasConfirmadasYTransmididas);
            ReportDataSource rptVentasInvalidadas = new ReportDataSource("dataSetVentaInvalidadas", ventasInvalidadas);
            //ReportDataSource rptVentasAnuladas = new ReportDataSource("dataSetVentaAnuladas", ventasAnuladas);

            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.DataSources.Add(rptVentasConfirmadasTransmitida);
            rptviewer.LocalReport.DataSources.Add(rptVentasInvalidadas);
            //rptviewer.LocalReport.DataSources.Add(rptVentasAnuladas);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //REPORTE DEL PUNTO DE VENTA POR SERIE Y CATEGORIA 
        public ActionResult ReporteDeVentaPorSerieYConceptoBasicoResumidoDeUnSoloPuntoDeVenta(string fechaInicio, string fechaFin)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoTransmitidasYConfirmadas = operacionLogica.ObtenerReporteVentasConSerieYConceptoBasicoConfirmadasYTransmitidas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta); //XY6.1
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoInvalidadas = operacionLogica.ObtenerReporteVentasConSerieYConceptoBasicoInvalidadas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta); 
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoAnuladas = operacionLogica.ObtenerReporteVentasConSerieYConceptoBasicoAnuladas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
                                                                                                                                        
            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());


            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentaPorCategoriaYSerie.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceTransmitidasYConfirmadas = new ReportDataSource("DataSetTransmitidasYConfirmadas", ResumenPorSerieYConeptoBasicoTransmitidasYConfirmadas);
            ReportDataSource rptdatasourceInvalidadas = new ReportDataSource("DataSetInvalidadas", ResumenPorSerieYConeptoBasicoInvalidadas);


            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceTransmitidasYConfirmadas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidadas);

            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        //REPORTE DEL PUNTO DE VENTA POR SERIE Y CONCEPTO
        public ActionResult ReportePorSerieYConceptoDeUnSoloPuntoDeVenta(string fechaInicio, string fechaFin) //cod:XY5 usa punto de venta
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);



            List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ResumenPorSerieYConceptoTransmitidoYConfirmado = operacionLogica.ObtenerResumenVentasConSerieYConceptoNegocioConfirmada(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoTransmitidoYConfirmado = operacionLogica.ObtenerResumenVentasConSerieYConceptoBasicoConfirmadaYTransmitida(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
            List<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ResumenPorSerieYConceptoInvalidos = operacionLogica.ObtenerResumenVentasConSerieYConceptoNegocioInvalidadas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);
            List<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ResumenPorSerieYConeptoBasicoInvalidos = operacionLogica.ObtenerResumenVentasConSerieYConceptoBasicoInvalidadas(ProfileData().IdCentroDeAtencionSeleccionado, fechaDesde, fechaHasta);


            var rptviewer = new ReportViewer();
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorConceptoYSerie.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { parametroFechaDesde, parametroFechaHasta, nombreEmpresa, nombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceSerieConceptoTransmitidoYConfirmado = new ReportDataSource("DataSetPorSerieYConceptoNegocioTransmitidaYConfirmada", ResumenPorSerieYConceptoTransmitidoYConfirmado);
            ReportDataSource rptdatasourceSerieConceptoBasicoTransmitidoYConfirmado = new ReportDataSource("DataSetPorSerieYConceptoBasicoTransmitidaYConfirmada", ResumenPorSerieYConeptoBasicoTransmitidoYConfirmado);//
            ReportDataSource rptdatasourceSerieConceptoInvalidado = new ReportDataSource("DataSetPorSerieYConceptoNegocioInvalidado", ResumenPorSerieYConceptoInvalidos);
            ReportDataSource rptdatasourceSerieConceptoBasicoInvalidado = new ReportDataSource("DataSetPorSerieYConceptoBasicoInvalidado", ResumenPorSerieYConeptoBasicoInvalidos);//


            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoTransmitidoYConfirmado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasicoTransmitidoYConfirmado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoInvalidado);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceSerieConceptoBasicoInvalidado);


            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }


        //VENTAS POR INTERVALO DE TIEMPO O DIARIO

        public ActionResult ReporteDeVentaDeLosPuntosDeVentaPorIntervaloDiario(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsPuntosDeVentas)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);


            List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ventasConfirmada = operacionLogica.ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(idsPuntosDeVentas, fechaDesde, fechaHasta);
            List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ventasInvalidada = operacionLogica.ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSeriePorIntervaloDiario(idsPuntosDeVentas, fechaDesde, fechaHasta);

            string _nombresCentrosAtencion = "";

            foreach (var idPuntoDeVenta in idsPuntosDeVentas)
            {
                string _nombreCentroAtencion = centroDeAtencionDatos.ObtenerNombreDeCentroDeAtencion(idPuntoDeVenta);
                _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
            }

            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);

            EstablecimientoComercialExtendidoConLogo sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Ventas/VentasPorIntervaloDeTiempo.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreEmpresa, nombreCentroAtencion, parametroFechaDesde, parametroFechaHasta, logoSede, fechaActualSistema, empleadoSede });

            ReportDataSource rptdatasourceConfirmada = new ReportDataSource("DataSetConfirmada", ventasConfirmada);
            ReportDataSource rptdatasourceInvalidada = new ReportDataSource("DataSetInvalidada", ventasInvalidada);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceConfirmada);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceInvalidada);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.SizeToReportContent = true;

            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }





        #endregion


        #region CANJE DE COMPROBANTES
        public JsonResult RegistrarCanjeDeComprobante(List<long> idsOrdenes, SelectorTipoDeComprobante tipoDeComprobante)
        {
            try
            {
                OperationResult result = operacionLogica.RegistrarCanjeDeComprobante(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, idsOrdenes, tipoDeComprobante.TipoComprobante.Id, tipoDeComprobante.SerieSeleccionada, tipoDeComprobante.NumeroIngresado);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INTENTAR GUARDAR UNA VENTA");

                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR LA VENTA", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region MANEJO DE ORDEN DE VENTA EN APLICACION PARA IMPRIMIR
        public void GuardarOrdenDeVentaParaImprimirEnAplicacion(OrdenDeVenta ordenDeVenta)
        {
            //MEJORAR EL ARMADO DE LA ORDEN DE VENTA, CON LOS DATOS DEL REGISTRO DE VENTA

            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)System.Web.HttpContext.Current.Application["VentasAImprimir"] ?? new List<OrdenDeVenta>();
            //Agregar a las ordenes de venta de la variable de alicacion la nueva orden de venta
            ordenesDeVenta.Add(ordenDeVenta);
            //Guardar en la variable de aplicacion a las ordenes de venta
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["VentasAImprimir"] = ordenesDeVenta;
            System.Web.HttpContext.Current.Application.UnLock();
        }

        public OrdenDeVenta ObtenerOrdenDeVentaParaImprimirEnAplicacion(long idOrdenDeVenta)
        {
            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)System.Web.HttpContext.Current.Application["VentasAImprimir"];
            //Buscar en la lista de las ordenes de venta el idOrdenDeVenta 
            OrdenDeVenta ordenDeVenta = ordenesDeVenta.Single(ov => ov.Id == idOrdenDeVenta);
            //Retornar la orden de venta encontrada
            return ordenDeVenta;
        }

        public void EliminarOrdenDeVentaParaImprimirEnAplicacion(long idOrdenDeVenta)
        {
            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)System.Web.HttpContext.Current.Application["VentasAImprimir"];
            //Buscar en la lista de las ordenes de venta el idOrdenDeVenta 
            ordenesDeVenta.Remove(ordenesDeVenta.Single(ov => ov.Id == idOrdenDeVenta));
            //Guardar en la variable de aplicacion a las ordenes de venta
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["VentasAImprimir"] = ordenesDeVenta;
            System.Web.HttpContext.Current.Application.UnLock();
        }
        #endregion

        public ActionResult ObtenerPuntosDeCliente(int idCliente)
        {
            try
            {
                PuntosDeCliente resultado = operacionLogica.ObtenerPuntosDeCliente(idCliente);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR OBTENER PUNTOS DE CLIENTE", e)), HttpStatusCode.InternalServerError);
            }
        }


        public PdfDocument ObtenerPdfVenta(OrdenDeVenta ordenDeVenta, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato)
        {
            string htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, formato, qrBytes, sede, this, maestroLogica);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }
    }
}

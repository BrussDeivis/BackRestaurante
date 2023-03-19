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
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class OrdenAlmacenController : CommonAlmacenController
    {
        protected readonly IOrdenAlmacen_Logica ordenAlmacenLogica;

        public OrdenAlmacenController() : base()
        {
            ordenAlmacenLogica = Dependencia.Resolve<IOrdenAlmacen_Logica>();
        }

        [Authorize(Roles = "Almacenero,AdministradorNegocio")]
        public ActionResult Principal()
        {
            ViewBag.Data = ordenAlmacenLogica.ObtenerDatosParaOrdenAlmacenPrincipal(ProfileData());

            //ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            //ViewBag.IdTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            //ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            //ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            //ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            //ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            //ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            //ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            //ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            //ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            //ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            //ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : " ";
            //ViewBag.idUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : 0;
            //ViewBag.idModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
            //ViewBag.idMotivoTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
            //ViewBag.idTransportistaPorDefecto = AplicacionSettings.Default.IdTransportistaPorDefectoEnSalidaDeMercaderia;
            //ViewBag.idTipoDeComprobantePorDefecto = AplicacionSettings.Default.IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia;
            //ViewBag.WCPScript = WebClientPrint.CreateScript(
            //Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme),
            //Url.Action("PrintFile", "Almacen", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            return View();
        }

        public JsonResult ObtenerOrdenesAlmacen(string desde, string hasta, bool porIngresar, bool entregaInmediata, bool entregaDiferida, bool estadoPendiente, bool estadoParcial, bool estadoCompletada, int[] idsAlmacenes)
        {
            try
            {
                DateTime fechaDesde = DateTime.Parse(desde);
                DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
                List<OrdenAlmacenResumen> ordenesAlmacen = ordenAlmacenLogica.ObtenerOrdenesAlmacen(fechaDesde, fechaHasta, porIngresar, entregaInmediata, entregaDiferida, estadoPendiente, estadoParcial, estadoCompletada, idsAlmacenes, ProfileData());
                return Json(ordenesAlmacen);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> ObtenerOrdenAlmacen(long idOrdenAlmacen, bool porIngresar)
        {
            try
            {
                OrdenAlmacen ordenAlmacen = ordenAlmacenLogica.ObtenerOrdenAlmacen(idOrdenAlmacen, porIngresar);
                var sede = ObtenerSede();
                var proveedores = new List<Proveedor>();
                var modalidadesDeTraslado = new List<Detalle_maestro>();
                var motivosDeTraslado = new List<Detalle_maestro>();
                var idsComprobantesMovimiento = new List<int> { MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna, MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente };
                if (ordenAlmacen.Movimientos.Select(m => m.IdTipoComprobante).ToList().Intersect(idsComprobantesMovimiento).Any())
                {
                    proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                    modalidadesDeTraslado = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                    motivosDeTraslado = await maestroLogica.ObtenerMotivosTrasladoAsync();
                }
                foreach (var orden in ordenAlmacen.Ordenes)
                {
                    orden.Comprobante = ObtenerOrdenMovimientoAlmacen(sede, orden.Comprobante, orden.IdTipoTransaccion);
                }
                foreach (var movimiento in ordenAlmacen.Movimientos)
                {
                    movimiento.Comprobante = ObtenerMovimientoAlmacen(sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, movimiento.Comprobante);
                }
                return Json(ordenAlmacen);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerRegistroMovimientoOrdenAlmacen(long idOrdenAlmacen, bool porIngresar)
        {
            try
            {
                RegistroMovimientoAlmacen registroMovimientoOrdenAlmacen = ordenAlmacenLogica.ObtenerRegistroMovimientoOrdenAlmacen(idOrdenAlmacen, porIngresar, ProfileData());
                return Json(registroMovimientoOrdenAlmacen);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<JsonResult> GuardarMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen)
        {
            try
            {
                OperationResult result = ordenAlmacenLogica.GuardarMovimientoOrdenAlmacen(movimientoOrdenAlmacen, ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar guardar el registro de movimiento de orden de almacen.");
                if (movimientoOrdenAlmacen.TipoDeComprobante.TipoComprobante.Id == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
                {
                    string path = HostingEnvironment.ApplicationPhysicalPath;
                    await facturacionElectronicaLogica.TransmitirEnviarGuiaDeRemision(((OrdenDeVenta)result.objeto).Transaccion().Transaccion11.First().id, ProfileData().Sede, ProfileData().Empleado.Id, path);
                }
                return Json(new { result.code_result, data = result.information, result_description = result.title});
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult InvalidarMovimientoOrdenAlmacen(long idMovimientoOrdenAlmacen, string observacion)
        {
            try
            {
                OperationResult result = ordenAlmacenLogica.InvalidarMovimientoOrdenAlmacen(idMovimientoOrdenAlmacen, observacion, ProfileData());
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar invalidar el movimiento de orden de almacen.");
                return Json(new { result.code_result, data = result.information, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
    }
}
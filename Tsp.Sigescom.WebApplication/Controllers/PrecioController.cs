using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PrecioController : BaseController
    {
        private readonly IPrecioLogica _precioLogica;
        private readonly IMaestroLogica _maestroLogica;
        private readonly IConceptoLogica conceptoLogica;
        private readonly IActorNegocioLogica actorNegocioLogica;

        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;

        public PrecioController()
        {
            _maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            _precioLogica = Dependencia.Resolve<IPrecioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();

        }
        [Authorize(Roles = "JefeVenta")]
        public ActionResult Precio()
        {
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            return View();
        }
        [Authorize(Roles = "JefeVenta")]
        public ActionResult Descuento()
        {
            return View();
        }
        [Authorize(Roles = "JefeVenta")]
        public ActionResult Bonificacion()
        {
            return View();
        }

        public JsonResult ObtenerPrecios()
        {
            try
            {
                var precios = _precioLogica.ObtenerPrecios();
                var jsonResult = Json(precios, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

                //JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                //jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                //var precios = _precioLogica.ObtenerPrecios();

                //string json = JsonConvert.SerializeObject(precios, Formatting.None, jsSettings);
                //var jsonResult = Json(precios, JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerPreciosDeConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                var puntosDePrecio = ComboGenericoViewModel.Convert(centroDeAtencion_Logica.ObtenerPuntosDePrecio());
                var tarifasDePrecio = ComboGenericoViewModel.Convert(_maestroLogica.obtenerTarifas());
                var preciosCompraVenta = conceptoLogica.ObtenerPreciosCompraVentaDeConceptoNegocio(idConceptoNegocio);
                var registroPrecio = new RegistroPrecioViewModel(preciosCompraVenta, idConceptoNegocio, puntosDePrecio, tarifasDePrecio,DateTimeUtil.FechaActual());
                return Json(registroPrecio);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }



        public JsonResult GuardarPrecio(RegistroPrecioViewModel registroPrecio)
        {
            try
            {
                OperationResult resultado = new OperationResult();
                var result = RegistroPrecioViewModel.Convert(registroPrecio);
                resultado = _precioLogica.EstablecerPrecio(result, registroPrecio.IdConcepto, ProfileData().Empleado.Id);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR PRECIO");
                return Json(new { resultado.code_result, resultado.data, result_description = "El precio se guardo con exito" });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar precio", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerDescuentos()
        {
            try
            {
                var descuentos = _precioLogica.obtenerDescuentos();
                List<BandejaBonificacionDescuentoViewModel> respuesta = BandejaBonificacionDescuentoViewModel.Convert(descuentos);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerBonificaciones()
        {
            try
            {
                var bonificaciones = _precioLogica.obtenerBonificaciones();
                List<BandejaBonificacionDescuentoViewModel> respuesta = BandejaBonificacionDescuentoViewModel.Convert(bonificaciones);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarDescuento(BonificacionDescuentoViewModel descuento)
        {
            try
            {
                OperationResult resultado = _precioLogica.crearDescuento(ProfileData().IdCentroDeAtencionSeleccionado, descuento.Mercaderia.Id, descuento.Porcentaje, descuento.Valor,
                    descuento.CantidadMinima, descuento.CantidadMaxima, descuento.FechaDesde, descuento.FechaHasta,
                    descuento.Descripcion, ProfileData().Empleado.Id);
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.message });

            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult GuardarBonificacion(BonificacionDescuentoViewModel bonificacion)
        {
            try
            {
                OperationResult resultado = _precioLogica.crearBonificacion(ProfileData().IdCentroDeAtencionSeleccionado, bonificacion.Mercaderia.Id, bonificacion.Porcentaje, bonificacion.Valor,
                    bonificacion.CantidadMinima, bonificacion.CantidadMaxima, bonificacion.FechaDesde, bonificacion.FechaHasta,
                    bonificacion.MercaderiaReferencia.Id, bonificacion.Descripcion, ProfileData().Empleado.Id);
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.message });

            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CaducarPrecio(int idPrecio)
        {
            try
            {
                OperationResult result = _precioLogica.caducarPrecio(idPrecio);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult CargarPrecio(int idPrecio)
        {
            try
            {
                PrecioViewModel precio = new PrecioViewModel(_precioLogica.obtenerPrecio(idPrecio));
                return Json(precio);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerTarifasConPrecioMercaderiaUnica(int idMercaderia)
        {
            try
            {

                List<Detalle_maestro> tarifas = _maestroLogica.obtenerTarifas();
                List<Precio> precios = _precioLogica.ObtenerPreciosMercaderiaUnica(idMercaderia);
                List<PrecioParaRegistroMercaderiaUnicaViewModel> tarifasConPrecio = PrecioParaRegistroMercaderiaUnicaViewModel.Match(tarifas, precios);
                return Json(tarifasConPrecio);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerPrecioConceptoUnico(int idConcepto)
        {
            try
            {

                List<Precio> precios = _precioLogica.ObtenerPreciosMercaderiaUnica(idConcepto);
                List<PrecioParaRegistroDeVentaViewModel> preciosVenta = PrecioParaRegistroDeVentaViewModel.Convert(precios);
                return Json(preciosVenta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarTarifasConPrecioMercaderiaUnica(List<PrecioParaRegistroMercaderiaUnicaViewModel> tarifasConPrecio, int idMercaderia)
        {

            try
            {
                OperationResult resultado;

                resultado = _precioLogica.establecerPrecios(ProfileData().IdCentroAtencionQueTieneLosPrecios, ActorSettings.Default.IdActorNegocioSede, ProfileData().Empleado.Id, PrecioParaRegistroMercaderiaUnicaViewModel.Convert(tarifasConPrecio), idMercaderia);

                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = "El precio se guardo con exito" });

            }
            catch (Exception e)
            {                                                                                                                                                                                                                                                                     
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }



    }
}
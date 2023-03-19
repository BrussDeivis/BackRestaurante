using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.TipoTransaccion;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class TipoDeTransaccionController : BaseController
    {
        private readonly IConfiguracionLogica _configuracionLogica;
        public TipoDeTransaccionController()
        {
            _configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
        }
        #region GETS
        public ActionResult TiposDeTransaccion()
        {
            return View();
        }
        #endregion

        #region GENERICO
        public JsonResult ObtenerTiposDeTransaccionGenerico()
        {
            try
            {
                var resultados = _configuracionLogica.ObtenerTiposDeTransaccion();
                List<ComboGenericoViewModel> tiposDeTransaccion = new List<ComboGenericoViewModel>();
                tiposDeTransaccion = ComboGenericoViewModel.Convert(resultados);
                return Json(tiposDeTransaccion);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region CRUD: Obtener, Crear Actualizar (Tipo De Transaccion)
        public JsonResult ObtenerTiposDeTransaccionBandeja()
        {
            try
            {
                var resultados = _configuracionLogica.ObtenerTiposDeTransaccion();
                List<BandejaTipoDeTransaccionViewModel> bandejaTiposDeTransaccion = BandejaTipoDeTransaccionViewModel.Convert(resultados);
                return Json(bandejaTiposDeTransaccion);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CrearTipoDeTransaccion(RegistroTipoDeTransaccionViewModel tipoDeTransaccion)
        {
            try
            {
                OperationResult resultado;
                resultado = null;
                if (tipoDeTransaccion.Id != 0)
                {

                    resultado = _configuracionLogica.ActualizarTipoDeTransaccion(tipoDeTransaccion.Id, tipoDeTransaccion.Nombre, tipoDeTransaccion.Descripcion, tipoDeTransaccion.TransaccionMaestro.Id, AccionDeNegocioPorTipoTransaccionViewModel.Convert(tipoDeTransaccion.AccionesDeNegocioPorTipoDeTransaccion));
                }
                else
                {
                    resultado = _configuracionLogica.CrearTipoDeTransaccion(tipoDeTransaccion.Nombre, tipoDeTransaccion.Descripcion, tipoDeTransaccion.TransaccionMaestro.Id, AccionDeNegocioPorTipoTransaccionViewModel.Convert(tipoDeTransaccion.AccionesDeNegocioPorTipoDeTransaccion));
                }

                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerTipoDeTransaccion(int idTipoDeTransaccion)
        {
            try
            {
                TipoDeTransaccion tipoDeTransaccion = _configuracionLogica.ObtenerTipoDeTransaccion(idTipoDeTransaccion);
                RegistroTipoDeTransaccionViewModel tipoDeTransaccionViewModel = new RegistroTipoDeTransaccionViewModel(tipoDeTransaccion);
                return Json(tipoDeTransaccionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        #region DATA: Acciones De Negocio
        public JsonResult ObtenerAccionesDeNegocioPorTipoDeTransaccion()
        {
            try
            {
                var resultados = _configuracionLogica.ObtenerAccionesDeNegocio();
                List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccion  = AccionDeNegocioPorTipoTransaccionViewModel.Convert(resultados);
                return Json(accionesDeNegocioPorTipoDeTransaccion);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerAccionesDeNegocioPorTipoDeTransaccionEditar(List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccion)
        {
            try
            {
                var resultados = _configuracionLogica.ObtenerAccionesDeNegocio();
                List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccionEditar = AccionDeNegocioPorTipoTransaccionViewModel.SetearAccionesDeNegocioPorTipoDeTransaccionParaEditar(resultados, accionesDeNegocioPorTipoDeTransaccion);
                return Json(accionesDeNegocioPorTipoDeTransaccionEditar);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion
    }
}
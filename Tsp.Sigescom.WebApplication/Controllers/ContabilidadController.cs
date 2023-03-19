using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ContabilidadController : Controller
    {
        private readonly IContabilidadLogica _logicaContabilidad;
        private readonly IActorNegocioLogica _logicaActorNegocio;
        private readonly IEstablecimiento_Repositorio _establecimientoDatos;


        public ContabilidadController()
        {
            _logicaContabilidad = Dependencia.Resolve<IContabilidadLogica>();
            _logicaActorNegocio = Dependencia.Resolve<IActorNegocioLogica>();
            _establecimientoDatos = Dependencia.Resolve<IEstablecimiento_Repositorio>();

        }
        public ActionResult Series()
        {
            return View();
        }
        public JsonResult ObtenerSeries()
        {

            List<SerieDeComprobante> series = _logicaContabilidad.obtenerSeriesDeComprobante();
            List<SerieViewModel> lista = new List<SerieViewModel>();
            lista = SerieViewModel.Convert(series);
            return Json(lista);
        }
        public JsonResult Guardar(SerieViewModel serieViewModel)
        {
            try
            {
                OperationResult result = null;
                if (serieViewModel.Id > 0)
                {
                    result = _logicaContabilidad.actualizarSerieDeComprobante(serieViewModel.Id, Convert.ToInt32(serieViewModel.IdTipoDeComprobante), serieViewModel.NumeroSerie, Convert.ToInt32(serieViewModel.IdSede), serieViewModel.Autonumerico, serieViewModel.NumeroSiguiente, serieViewModel.Vigente);
                }
                else
                {
                    result = _logicaContabilidad.guardarSerieDeComprobante(Convert.ToInt32(serieViewModel.IdTipoDeComprobante), serieViewModel.NumeroSerie, Convert.ToInt32(serieViewModel.IdSede), serieViewModel.Autonumerico, serieViewModel.NumeroSiguiente, serieViewModel.Vigente);
                }
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarTiposDeComprobante()
        {
            List<Detalle_maestro> lista = _logicaContabilidad.obtenerComprobantes();

            List<ComboGenerico> respuesta = new List<ComboGenerico>();
            for (int i = 0; i < lista.Count; i++)
            {
                Detalle_maestro t = lista[i];
                ComboGenerico c = new ComboGenerico(t.id, t.nombre);
                respuesta.Add(c);
            }
            return Json(respuesta);
        }
        public JsonResult ListarSedes()
        {
            List<EstablecimientoComercial> lista = _establecimientoDatos.ObtenerEstablecimientosComercialesVigentes().ToList();
            List<ComboGenerico> respuesta = new List<ComboGenerico>();
            for (int i = 0; i < lista.Count; i++)
            {
                EstablecimientoComercial t = lista[i];
                ComboGenerico c = new ComboGenerico(t.Id, t.Nombre, t.DocumentoIdentidad);

                respuesta.Add(c);
            }
            return Json(respuesta);
        }

       

    }
}
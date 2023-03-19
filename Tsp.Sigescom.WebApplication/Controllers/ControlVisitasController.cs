using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsp.ApiIdentificacion;
using Tsp.ApiIdentificacion.Logica;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ControlVisitasController : BaseController
    {
        private IdentificacionLogica _identificacionLogica = new IdentificacionLogica();

        // GET: ControlVisitas
        public ActionResult Ingresos()
        {
            return View();
        }

        // GET: Reporte dni
        public ActionResult Reporte_dni()
        {
            return View();
        }

        // GET: Reporte fecha
        public ActionResult Reporte_fecha()
        {
            return View();
        }

        // GET: Registro ruc
        public ActionResult Registro_ruc()
        {
            return View();
        }

        // GET: Reporte dni
        public ActionResult Reporte_ruc()
        {
            return View();
        }

        public JsonResult ObtenerDatosDniYGuardar(string dni)
        {
            try
            {
                var Dni = new DniConsult();
                var response = Dni.PadronElectoral(dni);
                var fecha = FechaActual();

                if (response != null)
                {
                    OperationResult resultado = _identificacionLogica.crearDatosDni(response[0], response[1],
                    response[3], response[4], response[5], fecha);

                    Util.VerificarError(resultado);
                    return Json(new { code_result = resultado.code_result, data = resultado.data,es_menor=false, result_description = resultado.title });
                }
                else
                {
                    OperationResult resultado = _identificacionLogica.crearDatosDni("", dni,
                    "", "", "", fecha);

                    Util.VerificarError(resultado);
                    return Json(new { code_result = resultado.code_result, data = resultado.data, es_menor = false, result_description = resultado.title });
                }

                
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ConsultarListaPorFecha(string fechaDesde, string fechaHasta)
        {
            DateTime FechaDesde = DateTime.Parse(fechaDesde);
            DateTime FechaHasta = DateTime.Parse(fechaHasta + " 23:59:59");
            try
            {
                List<DniConsultViewModel> respuesta = DniConsultViewModel.Convert(_identificacionLogica.obtenerListaPorFecha(FechaDesde,FechaHasta));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
            
        }
        public JsonResult ConsultarListaPorDni(string dni, string fechaDesde, string fechaHasta)
        {
            DateTime FechaDesde = DateTime.Parse(fechaDesde);
            DateTime FechaHasta = DateTime.Parse(fechaHasta + " 23:59:59");
            try
            {
                List<DniConsultViewModel> respuesta = DniConsultViewModel.Convert(_identificacionLogica.obtenerListaPorDniYFecha(dni,FechaDesde, FechaHasta));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }

        }
        public JsonResult ObtenerDatosRucYGuardar(string ruc)
        {
            try
            {
                var Ruc = new RucConsult();
                var response = Ruc.ObtenerInformacionDeRuc(ruc).ToArray();
                var fecha = FechaActual();
                if (response[2].Key == "Tipo de Documento")
                {
                    OperationResult resultado = _identificacionLogica.crearDatosRuc(response[0].Value, response[1].Value,
                    response[3].Value, response[4].Value, response[5].Value, response[6].Value,response[7].Value,
                    response[9].Value, response[10].Value, response[11].Value, response[12].Value, response[13].Value,
                    response[14].Value, response[15].Value, response[16].Value, response[17].Value);

                    Util.VerificarError(resultado);
                    return Json(new { code_result = resultado.code_result, data = resultado.data, es_menor = false, result_description = resultado.title });
                }
                else
                {
                    OperationResult resultado = _identificacionLogica.crearDatosRuc(response[0].Value, response[1].Value,
                    response[2].Value, response[3].Value, response[4].Value, response[5].Value, response[6].Value,
                    response[7].Value, response[8].Value, response[9].Value, response[10].Value, response[11].Value,
                    response[12].Value, response[13].Value, response[14].Value, response[15].Value);

                    Util.VerificarError(resultado);
                    return Json(new { code_result = resultado.code_result, data = resultado.data, es_menor = false, result_description = resultado.title });
                }
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ConsultarListaRuc()
        {
            try
            {
                List<RucConsultViewModel> respuesta = RucConsultViewModel.Convert(_identificacionLogica.obtenerListaRuc());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }

        }
    }
}
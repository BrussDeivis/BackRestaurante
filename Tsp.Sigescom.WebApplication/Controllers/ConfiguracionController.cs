using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models.Configuracion;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ConfiguracionController : Controller
    {

        private readonly IConfiguracionLogica _logicaConfiguracion;

        public ConfiguracionController()
        {
            _logicaConfiguracion = Dependencia.Resolve<IConfiguracionLogica>();

        } 

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GuardarConfiguracion(ConfiguracionViewModel configuracion)
        {
            try
            {
                OperationResult resultado;
                if (configuracion.Id != 0)
                {
                    resultado = _logicaConfiguracion.actualizarConfiguracion(configuracion.Id, configuracion.Nombre, configuracion.Descripcion);
                }
                else
                {
                    resultado = _logicaConfiguracion.guardarConfiguracion(configuracion.Nombre, configuracion.Descripcion);
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
        public JsonResult GuardarParametroConfiguracion(ParametroConfiguracionViewModel parametro)
        {
            try
            {
                OperationResult resultado;
                if (parametro.Id != 0)
                {
                    resultado = _logicaConfiguracion.actualizarParametroConfiguracion(parametro.Id, parametro.IdConfiguracion, parametro.Nombre, parametro.Tipo, parametro.Descripcion, parametro.Valor);
                    switch(parametro.IdConfiguracion)
                    {
                        case 1: TransaccionSettings.Reset();break;
                        case 2: OperationResultSettings.Reset();break;
                        case 3: MaestroSettings.Reset();break;
                        case 4: FacturacionElectronicaSettings.Reset(); break;
                        case 5: ContabilidadSettings.Reset(); break;
                        case 6: ConceptoSettings.Reset(); break;
                        case 7: AplicacionSettings.Reset(); break;
                        case 8: ActorSettings.Reset(); break;
                        case 9: ReporteSettings.Reset(); break;
                        case 10: LibrosElectronicosSettings.Reset(); break;
                        case 11: HotelSettings.Reset(); break;
                        case 12: VentasSettings.Reset(); break;
                        case 13: RestauranteSettings.Reset(); break;
                        case 14: CocheraSettings.Reset(); break;
                        case 15: CotizacionSettings.Reset(); break;
                        case 16: CodigoFESunatSettings.Reset(); break;
                        case 17: ConcarSettings.Reset(); break;
                    }
                }
                else
                {
                    resultado = _logicaConfiguracion.guardarParametroConfiguracion(parametro.IdConfiguracion, parametro.Nombre, parametro.Tipo, parametro.Descripcion, parametro.Valor);
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
        public JsonResult ListarConfiguraciones()
        {
            try
            {
                var resultados = _logicaConfiguracion.obtenerConfiguraciones();
                List<ConfiguracionViewModel> configuraciones = new List<ConfiguracionViewModel>();
                foreach (var item in resultados)
                {
                    configuraciones.Add(new ConfiguracionViewModel(item));
                }
                return Json(configuraciones);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarParametrosConfiguracion(int idConfiguracion)
        {
            try
            {
                List<Parametro_de_configuracion> resultados = _logicaConfiguracion.obtenerParametrosConfiguracion(idConfiguracion);
                List<ParametroConfiguracionViewModel> parametros = new List<ParametroConfiguracionViewModel>();
                foreach (var item in resultados)
                {
                    parametros.Add(new ParametroConfiguracionViewModel(item));
                }
                return Json(parametros);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }
}

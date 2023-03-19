using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.Turno;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class RolController : BaseController
    {
        private readonly IConfiguracionLogica _configuracionLogica;
       
       
        public RolController()
        {
            _configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
        }

        #region GETS

        public ActionResult Roles()
        {
            return View();
        }


        #endregion



        #region Crear, Obtener ( Turno)
        public JsonResult CrearRol(RegistroRolViewModel rol)
        {
            try
            {
                OperationResult resultado;
                if (rol.Id != 0)
                {
                    resultado = null;
                    //resultado = _configuracionLogica.ActualizarTurno(rol.Id, rol.CentroDeAtencion.Id, rol.Empleado.Id, rol.Desde, rol.Hasta);
                }
                else
                {
                    resultado = _configuracionLogica.CrearRol(rol.Nombre, rol.Descripcion, rol.RolPadre.Id, rol.Aplica);
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




        //public JsonResult ObtenerTurno(int idTurno)
        //{
        //    try
        //    {
        //        TurnoDeEmpleado turno = _actorNegocioLogica.ObtenerTurno(idTurno);
        //        RegistroTurnoViewModel turnoViewModel = new RegistroTurnoViewModel(turno);
        //        return Json(turnoViewModel);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(Util.errorJson(e));
        //    }
        //}
        public JsonResult ObtenerRolesDeNegocioGenerico()
        {
            try
            {
                var resultados = _configuracionLogica.ObtenerRolesDeNegocio();
                List<ComboGenericoViewModel> roles = ComboGenericoViewModel.Convert(resultados);
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

    }
}
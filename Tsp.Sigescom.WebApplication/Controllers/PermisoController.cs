using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PermisoController : Controller
    {
        private readonly IPermisos_Logica _logica;

        public PermisoController()
        {
            _logica = Dependencia.Resolve<IPermisos_Logica>();
        }
        // GET: Permiso
        [Authorize(Roles = "AdministradorTI")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeConfig()
        {
            //AplicacionSettings.Default.TipoVenta = 3;
            //Properties.Settings.Default.Save();
            return View();
        }


        public JsonResult ObtenerEstadoAccion(int idTT, int idEA)
        {
            try
            {
                List<Accion_por_estado> lista = _logica.ObtenerAccionesPosiblesPorTipoTransaccionYEstadoActual(idTT, idEA);
                List<PermisoViewModel> respuesta = new List<PermisoViewModel>();
                foreach (var item in lista)
                {
                    respuesta.Add(new PermisoViewModel(item.id, item.id_tipo_transaccion,item.TipoTransaccion, item.id_estado_actual,item.Estado, item.id_accion_posible,item.Accion));
                }
                return Json(respuesta);
            }
            catch(Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
            
        }

        public JsonResult ObtenerRolAccion(int idTT, int idR)
        {
            try
            {
                List<Accion_por_rol> lista = _logica.ObtenerAccionesPosiblesPorTipoTransaccionYRolPersonal(idTT, idR);
                List<PermisoViewModel> respuesta = new List<PermisoViewModel>();
                foreach (var item in lista)
                {
                    respuesta.Add(new PermisoViewModel(item.id, item.id_tipo_transaccion, item.TipoTransaccion, item.id_rol, item.RolPersonal, item.id_accion_posible,
                        item.Accion, item.id_unidad_negocio, item.UnidadNegocio));
                }
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult GuardarAcciones(List<PermisoViewModel> rolAcciones, List<PermisoViewModel> estadoAcciones, int idTT, int? idR, int? idEA)
        {
            try
            {
                List<Accion_por_rol> listaRolAcciones = null;//= new List<Accion_por_rol>();
                List<Accion_por_estado> listaEstadoAcciones = null;// = new List<Accion_por_estado>();
                if (rolAcciones != null && idR != null)
                {
                    listaRolAcciones = new List<Accion_por_rol>();
                    foreach (var av in rolAcciones)
                    {
                        if (av.Id == 0)
                        {
                            listaRolAcciones.Add(new Accion_por_rol(av.TipoTransaccion.Id, av.RolPersonal.Id, av.Accion.Id, av.UnidadNegocio.Id));
                        }
                        else
                        {
                            listaRolAcciones.Add(new Accion_por_rol(av.Id, av.TipoTransaccion.Id, av.RolPersonal.Id, av.Accion.Id, av.UnidadNegocio.Id));
                        }
                    }
                }
                if (estadoAcciones != null && idEA != null)
                {
                    listaEstadoAcciones = new List<Accion_por_estado>();
                    foreach (var av in estadoAcciones)
                    {
                        if (av.Id == 0)
                        {
                            listaEstadoAcciones.Add(new Accion_por_estado(av.TipoTransaccion.Id, av.Estado.Id, av.Accion.Id));
                        }
                        else
                        {
                            listaEstadoAcciones.Add(new Accion_por_estado(av.Id, av.TipoTransaccion.Id, av.Estado.Id, av.Accion.Id));
                        }
                    }
                }

                OperationResult op = _logica.ActualizarPermisosPorRolYEstado(listaRolAcciones, listaEstadoAcciones, idTT, Convert.ToInt32(idR), Convert.ToInt32(idEA));

                List<string> excepciones = new List<string>();
                for (int i = 0; i < op.exceptions.Count; i++)
                {
                    excepciones.Add(op.exceptions[i].Message);
                }
                return Json(new { codigo = op.code_result, descripcion = op.title, excepciones = excepciones });
            }
            catch(Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
    }
}
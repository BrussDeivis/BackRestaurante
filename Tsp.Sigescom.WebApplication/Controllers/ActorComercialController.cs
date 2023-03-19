using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.WebApplication.Controllers;

namespace Tsp.Sigescom.Areas.Administracion.Controllers
{
    public class ActorComercialController : ActorBaseController
    {

        private new readonly IActorNegocioLogica actorNegocioLogica;
        private readonly IValidacionActorNegocio_Logica _validacionActorNegocioLogica;

        public ActorComercialController()
        {
            _validacionActorNegocioLogica = Dependencia.Resolve<IValidacionActorNegocio_Logica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
        }

        /// <summary>
        /// Si el actor comercial está registrado lo devuelve, si esta registrado como actor lo crea y lo devuelve,
        /// En caso que no está registrado, lo busca con algun API, lo registra y lo devuelve.
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        public JsonResult ResolverActorComercialPorDocumentoDeIdentidad(int idRol, string numeroDocumento)
        {
            try
            {
                OperationResult result = new OperationResult(OperationResultEnum.Success);
                ActorComercial_ actorComercial = ResolverActorComercial(idRol, numeroDocumento);
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title, information = actorComercial }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public ActorComercial_ ResolverActorComercial(int idRol, string numeroDocumento)
        {
            ActorComercial_ actorComercial = actorNegocioLogica.ObtenerActorComercialCreandoloSiExisteSoloComoActor(idRol, numeroDocumento);
            if (actorComercial == null)//Si no está registrado en la base de datos, lo conseguimos mediante apis, scrapping, otros.
            {
                RegistroActorComercial registroActorComercial = null;
                ItemGenerico tipoDocumentoIdentidad = actorNegocioLogica.DeterminarTipoDeDocumentoDeIdentidad(numeroDocumento);
                if (tipoDocumentoIdentidad == null)
                {
                    throw new ControllerException("No se ha podido determinar el tipo de documento de identidad.");
                }
                if (tipoDocumentoIdentidad.Id == ActorSettings.Default.IdTipoDocumentoIdentidadDni)
                {
                    registroActorComercial = ObtenerActorDniApi(numeroDocumento).ConvertirConDni(numeroDocumento);
                }
                else if (tipoDocumentoIdentidad.Id == ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
                {
                    registroActorComercial = ObtenerActorRucApi(numeroDocumento).ConvertirConRuc(numeroDocumento);
                }
                var result = actorNegocioLogica.GuardarActorComercial(idRol, registroActorComercial);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL INTENTAR OBTENER O REGISTRAR PERSONA, REVISE EL NUMERO DE DOCUMENTO DE IDENTIDAD.");
                actorComercial = (ActorComercial_)result.information;
            }
            return actorComercial;
        }

        /// <summary>
        /// Verifica si el documento esta registrado como actor comercial o si esta regsitrado como actor lo devuelve, y si no esta registrado lo obtine del api y lo devuelve 
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="idTipoDocumento"></param>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        public JsonResult ResolverObtenerActorComercial(int idRol, int idTipoDocumento, string numeroDocumento)
        {
            ActorComercial_ actorComercial = new ActorComercial_();
            try
            {
                _validacionActorNegocioLogica.ValidarDocumentoIdentidad(numeroDocumento, new ItemGenerico(idTipoDocumento));
                RespuestaVerificacionActorNegocio respuesta = actorNegocioLogica.VerificarActor(idTipoDocumento, numeroDocumento, idRol);
                if (respuesta.respuesta == RespuestaVerificacionEnum.ExisteSoloActor)
                {
                    actorComercial = new ActorComercial_(respuesta.actor);
                }
                if (respuesta.respuesta == RespuestaVerificacionEnum.ExisteActorNegocio)
                {
                    actorComercial = new ActorComercial_(idRol, respuesta.actorNegocio);
                }
                if (respuesta.respuesta == RespuestaVerificacionEnum.NoExisteActor)
                {
                    if (idTipoDocumento == ActorSettings.Default.IdTipoDocumentoIdentidadDni)
                    {
                        actorComercial = ObtenerActorDniApi(numeroDocumento).ConvertirConDni(numeroDocumento);
                    }
                    else if (idTipoDocumento == ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
                    {
                        actorComercial = ObtenerActorRucApi(numeroDocumento).ConvertirConRuc(numeroDocumento);
                    }
                    else
                    {
                        throw new ControllerException("No se pudo obtener informacion, ingresar todos los datos de la persona.");
                    }
                }
                return Json(new { respuesta.respuesta, actorComercial });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult GuardarActorComercial(int idRol, RegistroActorComercial actorComercial)
        {
            try
            {
                OperationResult result;
                result = actorNegocioLogica.GuardarActorComercial(idRol, actorComercial);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar guardar el actor comercial");
                return Json(new { result.code_result, result.data, result_description = result.title, result.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerParametrosParaSelectorDeActorComercial()
        {
            try
            {
                var data = new ConfiguracionSelectorActorComercial
                {
                    IdEmpleadoPorDefecto = ProfileData().Empleado.Id
                };
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerParametrosParaRegistroDeActorComercial()
        {
            try
            {
                var data = new ConfiguracionRegistroActorComercial
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

        public JsonResult ObtenerActorComercialPorId(int idRol, int id)
        {
            try
            {
                OperationResult result = new OperationResult(OperationResultEnum.Success);
                ActorComercial_ actorComercial = actorNegocioLogica.ObtenerActorComercialPorId(idRol, id);
                return new JsonHttpStatusResult(new { result.code_result, result.data, result_description = result.title, information = actorComercial }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerActoresComercialesPorRolYBusqueda(int idRol, string cadenaBusqueda)
        {
            try
            {
                List<SelectorActorComercial> selectorActoresComerciales = actorNegocioLogica.ObtenerActoresComercialesVigentesPorRolYBusqueda(idRol, cadenaBusqueda);
                var jsonResult = Json(selectorActoresComerciales, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener los actores comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerActoresComercialesPorCentroDeAtencion(int idCentroDeAtencion)
        {
            try
            {
                List<ItemGenerico> empleados = actorNegocioLogica.ObtenerActoresComercialesPorCentroDeAtencion(idCentroDeAtencion);
                return Json(empleados);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerGruposActoresComerciales()
        {
            try
            {
                var respuesta = actorNegocioLogica.ObtenerGruposActoresComerciales();
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerGruposActoresComercialesPorRol(int idRol)
        {
            try
            {
                var respuesta = actorNegocioLogica.ObtenerGruposActoresComercialesPorRol(idRol);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(int idRol, int idGrupoActoresComerciales)
        {
            try
            {
                var respuesta = actorNegocioLogica.ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(idRol, idGrupoActoresComerciales);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerGruposActoresComercialesPorRolDeActorComercial(int idRol, int idActorComercial)
        {
            try
            {
                var respuesta = actorNegocioLogica.ObtenerGruposActoresComercialesPorRolDeActorComercial(idRol, idActorComercial);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }
}
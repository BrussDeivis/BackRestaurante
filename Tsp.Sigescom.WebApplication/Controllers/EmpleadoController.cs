using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.Turno;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class EmpleadoController : AdminController
    {
        private readonly IActorNegocioLogica _logica;
        private ApplicationUserManager _userManager;

        public EmpleadoController()
        {
            _logica = Dependencia.Resolve<IActorNegocioLogica>();
        }

        public JsonResult ListarEmpleados()
        {
            try
            {

                var result = UserManager.Users.ToList();
                List<BandejaEmpleadoViewModel> respuesta = BandejaEmpleadoViewModel.Convert(_logica.ObtenerEmpleadosVigentes(), result);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerEmpleadosGenerico()
        {
            try
            {
                List<ComboGenericoViewModel> respuesta = ComboGenericoViewModel.Convert(_logica.ObtenerEmpleadosVigentes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        internal void ResolverDirecciones(EmpleadoViewModel empleado, List<Direccion> direcciones)
        {
            if (empleado.IdActor > 0)
            {


                foreach (var item in empleado.Direcciones)
                {
                    if (item.Id > 0)
                    {
                        direcciones.Add(new Direccion(item.Id, empleado.IdActor, MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
                            null, null, item.EsPrincipal, item.EsVigente));
                    }
                    else
                    {
                        direcciones.Add(new Direccion(empleado.IdActor, MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id,
                            item.Detalle, null, null, item.EsPrincipal, item.EsVigente));
                    }
                }
            }
            else
            {
                foreach (var item in empleado.Direcciones)
                {
                    direcciones.Add(new Direccion(MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id, item.Detalle,
                        null, null, item.EsPrincipal, item.EsVigente));
                }
            }
        }

        public JsonResult GuardarEmpleado(EmpleadoViewModel empleado, ExpandedUserDTO usuario)
        {
            try
            {
                List<Direccion> direcciones = new List<Direccion>();
                List<int> roles = new List<int>();
                roles = empleado.Roles.Select(r => r.Id).ToList();
                if (empleado.Id > 0)
                {
                    //Pasamos las direcciones de la vista a la clase direcciones
                    ResolverDirecciones(empleado, direcciones);
                    //Actualizamos el empleado
                    var result = _logica.ActualizarEmpleado(empleado.Id, empleado.IdActor, empleado.Codigo, empleado.TipoDocumentoIdentidad.Id, empleado.NumeroDocumentoIdentidad, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Nombres, empleado.Correo, empleado.ClaseActor.Id, empleado.EstadoLegalActor.Id, empleado.FechaNacimiento, empleado.Telefono, roles, direcciones);
                    Util.ManageIfResultIsNotSuccess(result, "No se pudo actualizar los datos del empleado");
                    if (empleado.GuardarUsuario)
                    {
                        if (empleado.ExisteUsuario)
                        {
                            var resultadoEditarUsuario = EditarUsuario(usuario);
                            Util.ManageIfResultIsNotSuccess(resultadoEditarUsuario, "No se pudo editar el usuario"); 
                            return new JsonHttpStatusResult(new { resultadoEditarUsuario.code_result, resultadoEditarUsuario.data, result_description = "Se actualizo el empleado y su usuario" }, HttpStatusCode.OK);
                        }
                        else
                        {
                            var resultadoGuardarUsuario = GuardarUsuario_(usuario);
                            Util.ManageIfResultIsNotSuccess(resultadoGuardarUsuario, "Se actualizo el empleado pero no se pudo guardar el usuario");
                            var resultadoEstablecerUsuario = _logica.EstablecerUsuario(resultadoGuardarUsuario.idUser, (int)result.data);
                            Util.ManageIfResultIsNotSuccess(resultadoEstablecerUsuario, "No se pudo asociar el usuario con el empleado empleado");
                            return new JsonHttpStatusResult(new { code_result = resultadoEstablecerUsuario.code_result, data = resultadoEstablecerUsuario.data, result_description = "Se creo el empleado y se creo su usuario" }, HttpStatusCode.OK);
                        }
                    }
                    else
                    {
                        return Json(new { result.code_result, result.data, result_description = "Se actualizo el empleado" });
                    }
                }
                else
                {
                    if (empleado.IdActor > 0)
                    {
                        //Pasamos las direcciones de la vista a la clase direcciones
                        ResolverDirecciones(empleado, direcciones);
                        //Creamos el empleado y actualizamos el actor
                        var resultadoCrearEmpleado = _logica.CrearEmpleadoActualizandoActor(empleado.IdActor, empleado.Codigo, empleado.TipoDocumentoIdentidad.Id, empleado.NumeroDocumentoIdentidad, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Nombres, empleado.Correo, empleado.ClaseActor.Id, empleado.EstadoLegalActor.Id, empleado.FechaNacimiento, empleado.Telefono, roles, direcciones);
                        Util.ManageIfResultIsNotSuccess(resultadoCrearEmpleado, "No se pudo crear al empleado");
                        if (empleado.GuardarUsuario)
                        {
                            var resultadoGuardarUsuario = GuardarUsuario_(usuario);
                            Util.ManageIfResultIsNotSuccess(resultadoGuardarUsuario, "No se pudo guardar el usuario");
                            var resultadoEstablecerUsuario = _logica.EstablecerUsuario(resultadoGuardarUsuario.idUser, (int)resultadoCrearEmpleado.data);
                            Util.ManageIfResultIsNotSuccess(resultadoEstablecerUsuario, "No se pudo asociar el usuario con el empleado empleado");
                            return new JsonHttpStatusResult(new { code_result = resultadoEstablecerUsuario.code_result, data = resultadoEstablecerUsuario.data, result_description = "Se creo el empleado y se creo su usuario" }, HttpStatusCode.OK);
                        }
                        else
                        {
                            return new JsonHttpStatusResult(new { code_result = resultadoCrearEmpleado.code_result, data = resultadoCrearEmpleado.data, result_description = "Se creo el empleado" }, HttpStatusCode.OK);
                        }
                    }
                    else
                    {
                        //Pasamos las direcciones de la vista a la clase direcciones
                        foreach (var item in empleado.Direcciones)
                        {
                            direcciones.Add(new Direccion(MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioPersonal, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id, item.Detalle,
                                null, null, item.EsPrincipal, item.EsVigente));
                        }
                        var resultadoCrearEmpleado = _logica.CrearEmpleado(empleado.Codigo, empleado.TipoDocumentoIdentidad.Id, empleado.NumeroDocumentoIdentidad, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.Nombres, empleado.Correo, empleado.ClaseActor.Id, empleado.EstadoLegalActor.Id, empleado.FechaNacimiento, empleado.Telefono, roles, direcciones);
                        Util.ManageIfResultIsNotSuccess(resultadoCrearEmpleado, "No se pudo crear al empleado");
                        if (empleado.GuardarUsuario)
                        {
                            var resultadoGuardarUsuario = GuardarUsuario_(usuario);
                            Util.ManageIfResultIsNotSuccess(resultadoGuardarUsuario, "No se pudo guardar el usuario");
                            var resultadoEstablecerUsuario = _logica.EstablecerUsuario(resultadoGuardarUsuario.idUser, (int)resultadoCrearEmpleado.data);
                            Util.ManageIfResultIsNotSuccess(resultadoEstablecerUsuario, "No se pudo asociar el usuario con el empleado");
                            return new JsonHttpStatusResult(new { code_result = resultadoEstablecerUsuario.code_result, data = resultadoEstablecerUsuario.data, result_description = "Se creo el empleado y se creo su usuario" }, HttpStatusCode.OK);
                        }
                        else
                        {
                            return new JsonHttpStatusResult(new { code_result = resultadoCrearEmpleado.code_result, data = resultadoCrearEmpleado.data, result_description = "Se creo el empleado" }, HttpStatusCode.OK);
                        }
                    }
                }
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

        public JsonResult Validar(int idTipoDocumento, string numeroDocumento)
        {
            EmpleadoViewModel actor = null;
            EmpleadoViewModel empleado = null;
            try
            {
                RespuestaVerificacionActorNegocio result = _logica.VerificarActor(idTipoDocumento, numeroDocumento, ActorSettings.Default.IdRolEmpleado);

                if (result.respuesta == RespuestaVerificacionEnum.ExisteSoloActor)
                {
                    actor = new EmpleadoViewModel(result.actor);
                }
                if (result.respuesta == RespuestaVerificacionEnum.ExisteActorNegocio)
                {
                    Empleado e = new Empleado(result.actorNegocio);
                    empleado = new EmpleadoViewModel(e);
                }
                if (result.respuesta == RespuestaVerificacionEnum.NoExisteActor)
                {
                    if (idTipoDocumento == ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
                    {
                        var datos = ObtenerActorRucApi(numeroDocumento);
                        return Json(new { result.respuesta, dataApi = datos });
                    }
                    if (idTipoDocumento == ActorSettings.Default.IdTipoDocumentoIdentidadDni)
                    {
                        var datos = ObtenerActorDniApi(numeroDocumento);
                        return Json(new { result.respuesta, dataApi = datos });
                    }
                }
                return Json(new { result.respuesta, actor, empleado });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { respuesta = RespuestaVerificacionEnum.NoSePudoVerificar, actor, empleado, mensaje = e.Message });
            }
        }
        public JsonResult EliminarEmpleado(int IdEmpleado)
        {
            try
            {
                //todo: al caducar empleado bloquear al usuario para que no pueda ingresar al sistema
                OperationResult result = _logica.DarDeBajaEmpleado(IdEmpleado);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CargarEmpleado(int idEmpleado)
        {
            try
            {
                Empleado e = _logica.ObtenerEmpleado(idEmpleado);
                EmpleadoViewModel ev = new EmpleadoViewModel(e);
                return Json(ev);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }

        public JsonResult AsignarUsuarioEmpleado(string idUsuario, RegistroUsuarioEmpleadoViewModel registro)
        {
            try
            {


                foreach (var item in registro.UsuariosEmpleadoInicial)
                {
                    _logica.EstablecerUsuario(null, item.Id);
                }

                foreach (var item in registro.UsuariosEmpleado)
                {
                    _logica.EstablecerUsuario(idUsuario, item.Id);

                }
                //OperationResult result = _logica.establecerUsuario(usuariosEmpleadosIinicial, usuariosEmpleados, idUsuario);
                OperationResult result = new OperationResult();


                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = "Se actualizo la cuenta de usuario del empleado" });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerEmpleadosConIdUsuario()
        {
            try
            {
                List<UsuarioEmpleadoViewModel> respuesta = UsuarioEmpleadoViewModel.Convert(_logica.ObtenerEmpleadosVigentes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerVendedoresConIdUsuario()
        {
            try
            {
                List<ComboGenericoViewModel> respuesta = ComboGenericoViewModel.Convert(_logica.ObtenerVendedoresVigentes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerEmpleadosConRolVendedor()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoDeEmpleadosConRolVendedor = ComboGenericoViewModel.Convert(_logica.ObtenerVendedoresVigentes());

                return Json(comboGenericoDeEmpleadosConRolVendedor);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol vendedor", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolComprador()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoDeEmpleadosConRolComprador = ComboGenericoViewModel.Convert(_logica.ObtenerCompradoresVigentes());

                return Json(comboGenericoDeEmpleadosConRolComprador);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol comprador", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolCajero()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoDeEmpleadosConRolCajero = ComboGenericoViewModel.Convert(_logica.ObtenerCajerosVigentes());

                return Json(comboGenericoDeEmpleadosConRolCajero);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol cajero", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolAlmacenero()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoDeEmpleadosConRolAlmacenero = ComboGenericoViewModel.Convert(_logica.ObtenerAlmacenerosVigentes());

                return Json(comboGenericoDeEmpleadosConRolAlmacenero);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol almacenero", e);
            }
        }

        public JsonResult ObtenerEmpleados()
        {
            try
            {
                var resultados = _logica.ObtenerEmpleadosVigentes();
                List<ComboActorComercialViewModel> empleados = ComboActorComercialViewModel.Convert(resultados);
                return Json(empleados);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }

        public JsonResult ObtenerEmpleadosConRolVendedorVigentesConCodigo()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoDeEmpleadosConRolVendedor = ComboCentroAtencionViewModel.Convert(_logica.ObtenerVendedoresVigentes());

                return Json(comboGenericoDeEmpleadosConRolVendedor);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol vendedor", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolCompradorVigentesConCodigo()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoDeEmpleadosConRolComprador = ComboCentroAtencionViewModel.Convert(_logica.ObtenerCompradoresVigentes());

                return Json(comboGenericoDeEmpleadosConRolComprador);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol comprador", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolCajeroVigentesConCodigo()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoDeEmpleadosConRolCajero = ComboCentroAtencionViewModel.Convert(_logica.ObtenerCajerosVigentes());

                return Json(comboGenericoDeEmpleadosConRolCajero);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol cajero", e);
            }
        }

        public JsonResult ObtenerEmpleadosConRolAlmaceneroVigentesConCodigo()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoDeEmpleadosConRolAlmacenero = ComboCentroAtencionViewModel.Convert(_logica.ObtenerAlmacenerosVigentes());

                return Json(comboGenericoDeEmpleadosConRolAlmacenero);

            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener empleados con rol almacenero", e);
            }
        }

        #region GESTION DE TURNO
        [Authorize(Roles = "AdministradorTI,AdministradorNegocio")]
        public ActionResult Turnos()
        {
            return View();
        }

        #region Metodos crear, obtener, actualizar Turno

        public JsonResult GuardarTurno(RegistroTurnoViewModel turno)
        {
            try
            {
                OperationResult resultado;
                if (turno.Id != 0)
                {
                    resultado = _logica.ActualizarTurno(turno.Id, turno.CentroDeAtencion.Id, turno.Empleado.Id, turno.Desde, turno.Hasta);
                }
                else
                {
                    resultado = _logica.CrearTurno(turno.CentroDeAtencion.Id, turno.Empleado.Id, turno.Desde, turno.Hasta);
                }

                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR EL TURNO");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerTurno(int idTurno)
        {
            try
            {
                TurnoDeEmpleado turno = _logica.ObtenerTurno(idTurno);
                RegistroTurnoViewModel turnoViewModel = new RegistroTurnoViewModel(turno);
                return Json(turnoViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerTurnosBandeja()
        {
            try
            {
                var resultados = _logica.ObtenerTurnos();
                List<BandejaTurnoViewModel> turnos = BandejaTurnoViewModel.Convert(resultados);
                return Json(turnos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        #endregion

    }
}
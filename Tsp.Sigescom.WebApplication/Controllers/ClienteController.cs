using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.WebApplication.Controllers;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.Turno;

namespace Tsp.Sigescom.Areas.Administracion.Controllers
{
    public class ClienteController : ActorBaseController
    {
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly IValidacionActorNegocio_Logica _validacionActorNegocioLogica;
        private readonly IGrupoClientes_Logica _grupoClientes_Logica;




        public ClienteController()
        {
            _actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            _validacionActorNegocioLogica = Dependencia.Resolve<IValidacionActorNegocio_Logica>();
            _grupoClientes_Logica = Dependencia.Resolve<IGrupoClientes_Logica>();
        }

        public JsonResult ListarClientes()
        {
            try
            {
                List<ResumenCliente> respuesta = _actorNegocioLogica.ObtenerClientesVigentes();
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }

        public JsonResult ListarClientes_()//Saul modificar tu metodo //Exceptuar el cliente varios este tiene muchas transacciones y sobre carga el metodo
        {
            try
            {
                List<BandejaClienteViewModel> respuesta = BandejaClienteViewModel.Convert(_actorNegocioLogica.ObtenerClientesConCuotasVigentes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }

        public JsonResult ObtenerClientes()
        {
            try
            {
                var resultados = _actorNegocioLogica.ObtenerClientesVigentesParaVenta();
                List<ComboActorComercialViewModel> clientes = ComboActorComercialViewModel.Convert(resultados);
                return Json(clientes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarCliente(ClienteViewModel cliente)
        {
            try
            {
                List<Direccion> direcciones = new List<Direccion>();
                OperationResult result;
                if (cliente.Id > 0)
                {
                    if (cliente.Direcciones != null)
                    {
                        foreach (var item in cliente.Direcciones)
                        {
                            if (item.Id > 0)
                            {
                                direcciones.Add(new Direccion(item.Id, cliente.IdActor, item.Tipo.Id, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
                                    null, null, item.EsPrincipal, item.EsVigente));
                            }
                            else
                            {
                                direcciones.Add(new Direccion(cliente.IdActor, item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id,
                                    item.Detalle, null, null, item.EsPrincipal, item.EsVigente));
                            }
                        }
                    }
                    result = _actorNegocioLogica.ActualizarCliente(cliente.Id, cliente.IdActor, cliente.Codigo, cliente.TipoPersona.Id, cliente.RazonSocial, cliente.ApellidoPaterno, cliente.ApellidoMaterno, cliente.Nombres,
                         cliente.NombreComercial, cliente.NombreCorto, cliente.TipoDocumentoIdentidad.Id, cliente.NumeroDocumentoIdentidad,
                         cliente.ClaseActor != null ? cliente.ClaseActor.Id : (int?)null, cliente.EstadoLegalActor != null ? cliente.EstadoLegalActor.Id : (int?)null,
                         cliente.Correo, cliente.Telefono, direcciones, cliente.IdComprobantePredeterminado);
                }
                else
                {
                    if (cliente.IdActor > 0)
                    {
                        if (cliente.Direcciones != null)
                        {
                            foreach (var item in cliente.Direcciones)
                            {
                                if (item.Id > 0)
                                {
                                    direcciones.Add(new Direccion(item.Id, cliente.IdActor, item.Tipo.Id, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
                                        null, null, item.EsPrincipal, item.EsVigente));
                                }
                                else
                                {
                                    direcciones.Add(new Direccion(cliente.IdActor, item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru,
                                        item.Ubigeo.Id, item.Detalle, null, null, item.EsPrincipal, item.EsVigente));
                                }
                            }
                        }
                        result = _actorNegocioLogica.CrearClienteActualizandoActor(cliente.IdActor, cliente.TipoPersona.Id, cliente.RazonSocial, cliente.ApellidoPaterno, cliente.ApellidoMaterno, cliente.Nombres, cliente.NombreComercial,
                            cliente.NombreCorto, cliente.TipoDocumentoIdentidad.Id, cliente.NumeroDocumentoIdentidad, cliente.ClaseActor != null ? cliente.ClaseActor.Id : (int?)null,
                            cliente.EstadoLegalActor != null ? cliente.EstadoLegalActor.Id : (int?)null, cliente.Correo, cliente.Telefono, direcciones, cliente.IdComprobantePredeterminado);
                    }
                    else
                    {
                        if (cliente.Direcciones != null)
                        {
                            foreach (var item in cliente.Direcciones)
                            {
                                direcciones.Add(new Direccion(item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id, item.Detalle,
                                    null, null, item.EsPrincipal, item.EsVigente));
                            }
                        }

                        result = _actorNegocioLogica.CrearCliente(cliente.TipoPersona.Id, cliente.RazonSocial, cliente.ApellidoPaterno, cliente.ApellidoMaterno, cliente.Nombres, cliente.NombreComercial, cliente.NombreCorto,
                                                cliente.TipoDocumentoIdentidad.Id, cliente.NumeroDocumentoIdentidad, cliente.ClaseActor != null ? cliente.ClaseActor.Id : (int?)null,
                                                cliente.EstadoLegalActor != null ? cliente.EstadoLegalActor.Id : (int?)null, cliente.Correo, cliente.Telefono, direcciones, cliente.IdComprobantePredeterminado);
                    }
                }
                Util.VerificarError(result);
                return Json(new { result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult Validar(int idTipoDocumento, string numeroDocumento)
        {
            ClienteViewModel actor = null;
            ClienteViewModel cliente = null;
            try
            {
                if (idTipoDocumento == ActorSettings.Default.IdTipoDocumentoIdentidadRuc)
                {
                    if (!_validacionActorNegocioLogica.ValidarRUC(numeroDocumento))
                        return Json(new { respuesta = RespuestaVerificacionEnum.RucNoValido, actor, cliente, mensaje = "Ruc ingresado no valido" });
                }

                RespuestaVerificacionActorNegocio result = _actorNegocioLogica.VerificarActor(idTipoDocumento, numeroDocumento, ActorSettings.Default.IdRolCliente);
                if (result.respuesta == RespuestaVerificacionEnum.ExisteSoloActor)
                {
                    actor = new ClienteViewModel(result.actor);
                }
                if (result.respuesta == RespuestaVerificacionEnum.ExisteActorNegocio)
                {
                    Cliente c = new Cliente(result.actorNegocio);
                    cliente = new ClienteViewModel(c);
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
                return Json(new { result.respuesta, actor, cliente });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { respuesta = RespuestaVerificacionEnum.NoSePudoVerificar, actor, cliente, mensaje = e.Message });
            }
        }

        /// <summary>
        /// si el cliente está registrado lo devuelve.
        /// En caso que no está registrado, lo busca con algun API, lo registra y lo devuelve.
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        public JsonResult ResolverClientePorDni(string dni)
        {
            try
            {
                OperationResult resultado = new OperationResult(OperationResultEnum.Success);
                var cliente = _actorNegocioLogica.ObtenerClientePorDni(dni);
                if (cliente == null)
                {
                    var persona = ObtenerActorDniApi(dni);
                    resultado = _actorNegocioLogica.CrearCliente(persona.ConvertirConDni(dni));
                    cliente = (ActorComercial_)resultado.information;
                    Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL INTENTAR OBTENER O REGISTRAR EL CLIENTE, revise el numero de documento de identidad.");
                }
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title, information = cliente }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ValidarClienteYRuc(int idCliente, int idTipoDocumento)
        {
            try
            {
                if (idTipoDocumento == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
                {
                    if (_validacionActorNegocioLogica.ValidarClienteYRUC(idCliente))
                    {
                        return Json(new { respuesta = OperationResultEnum.Success, mensaje = "Cliente si cuenta con numero de RUC" });
                    }
                    else
                    {
                        return Json(new { respuesta = OperationResultEnum.Warning, mensaje = "Cliente no cuenta con numero de RUC" });
                    }
                }
                else
                {
                    return Json(new { respuesta = OperationResultEnum.Success, mensaje = "Cliente si cuenta con numero de RUC" });
                }
            }
            catch (Exception e)
            {
                return Json(new { respuesta = OperationResultEnum.Error, mensaje = e.Message });
            }
        }

        public JsonResult EliminarCliente(int IdCliente)
        {
            try
            {
                OperationResult result = _actorNegocioLogica.DarDeBajaCliente(IdCliente);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CargarCliente(int idCliente)
        {
            try
            {
                Cliente cliente = _actorNegocioLogica.ObtenerCliente(idCliente);
                ClienteViewModel clienteViewModel = new ClienteViewModel(cliente);
                return Json(clienteViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerClientesGenerico()
        {
            try
            {
                var clientesVigentes = await _actorNegocioLogica.ObtenerClientesVigentesComoItemsGenericos();
                return Json(clientesVigentes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerClientesGenericoSinParentRol()
        {
            try
            {
                var clientesVigentes = await _actorNegocioLogica.ObtenerClientesVigentesComoItemsGenericosPorIdRol();
                return Json(clientesVigentes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #region Cartera de clientes
        public ActionResult CarteraDeClientes()
        {
            return View();
        }

        public JsonResult GuardarCarteraDeClientes(RegistroCarteraDeClientesViewModel cartera)
        {
            try
            {
                OperationResult resultado;
                if (cartera.Detalles != null)
                {
                    List<int> idClientes = new List<int>();
                    idClientes = cartera.Detalles.Select(c => c.Id).ToList();
                    if (cartera.Id != 0)
                    {
                        resultado = null;
                        resultado = _actorNegocioLogica.ActualizarCarteraDeClientes(cartera.CentroDeAtencion.Id, idClientes);
                    }
                    else
                    {
                        resultado = _actorNegocioLogica.CrearCarteraDeClientes(cartera.CentroDeAtencion.Id, idClientes);
                    }
                    Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR LA CARTERA DE CLIENTES");
                    return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
                }
                throw new LogicaException("NO HAY NINGÚN CLIENTE.");
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar la cartera de clientes", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCarterasDeClientes()
        {
            try
            {
                List<BandejaCarteraDeClientesViewModel> respuesta = BandejaCarteraDeClientesViewModel.Convert(_actorNegocioLogica.ObtenerCarterasDeClientes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCarteraDeClientes(int idCarteraDeCliente)
        {
            try
            {
                DetalleCarteraDeClientesViewModel respuesta = new DetalleCarteraDeClientesViewModel(_actorNegocioLogica.ObtenerCarteraDeClientesSegunCentroDeAtencion(idCarteraDeCliente));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCarteraDeClientesAEditar(int idCarteraDeCliente)
        {
            try
            {
                RegistroCarteraDeClientesViewModel registro = new RegistroCarteraDeClientesViewModel(_actorNegocioLogica.ObtenerCarteraDeClientesSegunCentroDeAtencion(idCarteraDeCliente));
                return Json(registro);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerClientesDeCartera(int idCarteraDeCliente)
        {
            try
            {
                var respuesta = _actorNegocioLogica.ObtenerCarteraDeClientesSegunCentroDeAtencion(idCarteraDeCliente);
                List<ComboGenericoViewModel> clientesViewModel = ComboGenericoViewModel.Convert(respuesta.clientes);
                return Json(clientesViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion


        public JsonResult ObtenerDireccionCliente(int idCliente)
        {
            try
            {
                return Json(_actorNegocioLogica.ObtenerDireccionActorComercial(idCliente));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerUbigeoDireccionCliente(int idCliente)
        {
            try
            {
                return Json(_actorNegocioLogica.ObtenerIdUbigeoDireccionActorComercial(idCliente));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerDetalleDireccionCliente(int idCliente)
        {
            try
            {
                return Json(_actorNegocioLogica.ObtenerDetalleDireccionActorComercial(idCliente));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerUbigeoDireccionTercero(int idTercero)
        {
            try
            {
                return Json(_actorNegocioLogica.ObtenerIdUbigeoDireccionActorComercial(idTercero));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerDetalleDireccionTercero(int idTercero)
        {
            try
            {
                return Json(_actorNegocioLogica.ObtenerDetalleDireccionActorComercial(idTercero));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerUltimaPlacaDeCliente(int idCliente)
        {
            try
            {
                var placaCliente = _actorNegocioLogica.ObtenerUltimaPlacaDeCliente(idCliente);
                return Json(placaCliente);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #region Grupo de clientes
        public ActionResult GrupoClientes()
        {
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            return View();
        }

        public JsonResult GuardarGrupoClientes(GrupoClientes grupoClientes)
        {
            try
            {
                OperationResult resultado;
                if (grupoClientes.Id != 0)
                {
                    resultado = _grupoClientes_Logica.ActualizarGrupoClientes(grupoClientes);
                }
                else
                {
                    resultado = _grupoClientes_Logica.CrearGrupoClientes(grupoClientes);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "Error al guardar el grupo de clientes");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar el grupo de clientes", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerGruposClientes()
        {
            try
            {
                var resultado = _grupoClientes_Logica.ObtenerGruposClientes();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerGrupoClientes(int idGrupoCliente)
        {
            try
            {
                var resultado = _grupoClientes_Logica.ObtenerGrupoClientes(idGrupoCliente);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
         
        public JsonResult DarBajaGrupoClientes(int idGrupoCliente)
        {
            try
            {
                var respuesta = _grupoClientes_Logica.DarBajaGrupoClientes(idGrupoCliente);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar dar de baja a un grupo de clientes", e)), HttpStatusCode.InternalServerError);
            }
        }

        
        #endregion

    }
}
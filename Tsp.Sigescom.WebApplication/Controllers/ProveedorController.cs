using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tsp.Sigescom.Areas.Administracion.Controllers;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ProveedorController : ActorBaseController
    {
        private readonly IActorNegocioLogica _logica;
        private readonly IValidacionActorNegocio_Logica _validacionActorNegocioLogica;

        public ProveedorController()
        {
            _logica = Dependencia.Resolve<IActorNegocioLogica>();
            _validacionActorNegocioLogica = Dependencia.Resolve<IValidacionActorNegocio_Logica>();

        }

        public JsonResult ListarProveedores()
        {
            try
            {
                List<BandejaProveedorViewModel> respuesta = BandejaProveedorViewModel.Convert(_logica.ObtenerProveedoresVigentes());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }

        public JsonResult GuardarProveedor(ProveedorViewModel proveedor)
        {
            try
            {
                List<Direccion> direcciones = new List<Direccion>();
                OperationResult result;
                if (proveedor.Id > 0)
                {
                    if (proveedor.Direcciones != null)
                    {
                        foreach (var item in proveedor.Direcciones)
                        {
                            if (item.Id > 0)
                            {
                                direcciones.Add(new Direccion(item.Id, proveedor.IdActor, item.Tipo.Id, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
                                    null, null, item.EsPrincipal, item.EsVigente));
                            }
                            else
                            {
                                direcciones.Add(new Direccion(proveedor.IdActor, item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id,
                                    item.Detalle, null, null, item.EsPrincipal, item.EsVigente));
                            }
                        }
                    }
                    result = _logica.ActualizarProveedor(proveedor.Id, proveedor.IdActor, proveedor.Codigo, proveedor.TipoPersona.Id, proveedor.RazonSocial, proveedor.ApellidoPaterno, proveedor.ApellidoMaterno, proveedor.Nombres,
                         proveedor.NombreComercial, proveedor.NombreCorto, proveedor.TipoDocumentoIdentidad.Id, proveedor.NumeroDocumentoIdentidad,
                         proveedor.ClaseActor != null ? proveedor.ClaseActor.Id : (int?)null, proveedor.EstadoLegalActor != null ? proveedor.EstadoLegalActor.Id : (int?)null,
                         proveedor.Correo, proveedor.Telefono, direcciones);
                }
                else
                {
                    if (proveedor.IdActor > 0)
                    {
                        if (proveedor.Direcciones != null)
                        {
                            foreach (var item in proveedor.Direcciones)
                            {
                                if (item.Id > 0)
                                {
                                    direcciones.Add(new Direccion(item.Id, proveedor.IdActor, item.Tipo.Id, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
                                        null, null, item.EsPrincipal, item.EsVigente));
                                }
                                else
                                {
                                    direcciones.Add(new Direccion(proveedor.IdActor, item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru,
                                        item.Ubigeo.Id, item.Detalle, null, null, item.EsPrincipal, item.EsVigente));
                                }
                            }
                        }
                        result = _logica.CrearProveedorActualizandoActor(proveedor.IdActor, proveedor.TipoPersona.Id, proveedor.RazonSocial, proveedor.ApellidoPaterno, proveedor.ApellidoMaterno, proveedor.Nombres, proveedor.NombreComercial,
                            proveedor.NombreCorto, proveedor.TipoDocumentoIdentidad.Id, proveedor.NumeroDocumentoIdentidad, proveedor.ClaseActor != null ? proveedor.ClaseActor.Id : (int?)null,
                            proveedor.EstadoLegalActor != null ? proveedor.EstadoLegalActor.Id : (int?)null, proveedor.Correo, proveedor.Telefono, direcciones);
                    }
                    else
                    {
                        if (proveedor.Direcciones != null)
                        {
                            foreach (var item in proveedor.Direcciones)
                            {
                                direcciones.Add(new Direccion(item.Tipo.Id, MaestroSettings.Default.IdDetalleMaestroNacionPeru, item.Ubigeo.Id, item.Detalle,
                                    null, null, item.EsPrincipal, item.EsVigente));
                            }
                        }

                        result = _logica.CrearProveedor(proveedor.TipoPersona.Id, proveedor.RazonSocial, proveedor.ApellidoPaterno, proveedor.ApellidoMaterno, proveedor.Nombres, proveedor.NombreComercial, proveedor.NombreCorto,
                                                proveedor.TipoDocumentoIdentidad.Id, proveedor.NumeroDocumentoIdentidad, proveedor.ClaseActor != null ? proveedor.ClaseActor.Id : (int?)null,
                                                proveedor.EstadoLegalActor != null ? proveedor.EstadoLegalActor.Id : (int?)null, proveedor.Correo, proveedor.Telefono, direcciones);
                    }
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
        public JsonResult Validar(int idTipoDocumento, string numeroDocumento)
        {
            ProveedorViewModel actor = null;
            ProveedorViewModel proveedor = null;
            try
            {
                RespuestaVerificacionActorNegocio result = _logica.VerificarActor(idTipoDocumento, numeroDocumento, ActorSettings.Default.IdRolProveedor);

                if (result.respuesta == RespuestaVerificacionEnum.ExisteSoloActor)
                {
                    actor = new ProveedorViewModel(result.actor);
                }
                if (result.respuesta == RespuestaVerificacionEnum.ExisteActorNegocio)
                {
                    Proveedor p = new Proveedor(result.actorNegocio);
                    proveedor = new ProveedorViewModel(p);
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
                return Json(new { result.respuesta, actor, proveedor });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { respuesta = RespuestaVerificacionEnum.NoSePudoVerificar, actor, proveedor, mensaje = e.Message });
            }
        }
        public JsonResult EliminarProveedor(int IdProveedor)
        {
            try
            {
                OperationResult result = _logica.DarDeBajaProveedor(IdProveedor);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult CargarProveedor(int idProveedor)
        {
            try
            {
                Proveedor p = _logica.ObtenerProveedor(idProveedor);
                ProveedorViewModel pv = new ProveedorViewModel(p);
                return Json(pv);
            }
            catch (Exception e)
            {

                return Json(Util.ErrorJson(e));
            }
        }


        public JsonResult ObtenerProveedores()
        {
            try
            {
                var resultados = _logica.ObtenerProveedores();
                List<ComboActorComercialViewModel> proveedores = ComboActorComercialViewModel.Convert(resultados);
                return Json(proveedores);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }
        public async Task<JsonResult> ObtenerProveedoresGenerico()
        {
            try
            {
                var proveedores = await _logica.ObtenerProveedoresVigentesComoItemsGenericos();
                return Json(proveedores);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }

        }
        public JsonResult ObtenerDireccionProveedor(int idProveedor)
        {
            try
            {
                return Json(_logica.ObtenerDireccionActorComercial(idProveedor));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ValidarProveedorYRuc(int idProveedor, int idTipoDocumento)
        {
            try
            {
                if (idTipoDocumento == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
                {
                    if (_validacionActorNegocioLogica.ValidarProveedorYRUC(idProveedor))
                    {
                        return Json(new { respuesta = OperationResultEnum.Success, mensaje = "Proveedor si cuenta con numero de RUC" });
                    }
                    else
                    {
                        return Json(new { respuesta = OperationResultEnum.Warning, mensaje = "Proveedor no cuenta con numero de RUC" });
                    }
                }
                else
                {
                    return Json(new { respuesta = OperationResultEnum.Success, mensaje = "Proveedor si cuenta con numero de RUC" });
                }
            }
            catch (Exception e)
            {
                return Json(new { respuesta = OperationResultEnum.Error, mensaje = e.Message });
            }
        }

        public JsonResult ObtenerUbigeoDireccionProveedor(int idProveedor)
        {
            try
            {
                return Json(_logica.ObtenerIdUbigeoDireccionActorComercial(idProveedor));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerDetalleDireccionProveedor(int idProveedor)
        {
            try
            {
                return Json(_logica.ObtenerDetalleDireccionActorComercial(idProveedor));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }

}
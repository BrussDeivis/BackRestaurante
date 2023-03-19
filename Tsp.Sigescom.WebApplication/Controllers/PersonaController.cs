using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IActorNegocioLogica _logicaActorNegocio;

        public PersonaController()
        {
            _logicaActorNegocio = Dependencia.Resolve<IActorNegocioLogica>();
        }

        [Authorize(Roles = "Vendedor")]
        public ActionResult Cliente()
        {
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.mascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            ViewBag.mascaraDatosAdicionalesEnBandejaClientes = ActorSettings.Default.MascaraDatosAdicionalesEnBandejaClientes;
            return View();
        }

        [Authorize(Roles = "Comprador")]
        public ActionResult Proveedor()
        {
            ViewBag.idRolProveedor = ActorSettings.Default.IdRolProveedor;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.mascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
            //ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            //ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            //ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            //ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            //ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            //ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            //ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            //ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            //ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            //ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            return View();
        }

        [Authorize(Roles = "AdministradorTI,AdministradorNegocio")]
        public ActionResult Empleado()
        {
            ViewBag.idRolEmpleado = ActorSettings.Default.IdRolEmpleado;
            ViewBag.mascaraDeVisualizacionValidacionRegistroEmpleado = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroEmpleado;
            return View();
        }

        public JsonResult obtenerDenominacionClaseActor(int IdTipoDeActor)
        {
            try
            {
                string denominacion = _logicaActorNegocio.ObtenerDenominacionClase(IdTipoDeActor);
                return Json(denominacion);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarTiposDeActor()
        {
            try
            {
                List<Tipo_actor> resultados = _logicaActorNegocio.ObtenerTiposDeActor();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ListarTiposDeClaseActor(int IdTipoDeActor)
        {
            try
            {
                List<Clase_actor> resultados = _logicaActorNegocio.ObtenerTiposDeClaseActor(IdTipoDeActor);
                List<ComboGenericoViewModel> combo = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    combo.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(combo);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ListarTiposDeEstadoLegalActor(int IdTipoDeActor)
        {
            try
            {
                List<Estado_legal> resultados = _logicaActorNegocio.ObtenerTiposDeEstadoLegal(IdTipoDeActor);
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerListaSexos()
        {
            try
            {
                List<Clase_actor> resultados = _logicaActorNegocio.ObtenerListaSexos();
                List<ItemGenerico> combo = new List<ItemGenerico>();
                foreach (var item in resultados)
                {
                    combo.Add(new ItemGenerico(item.id, item.nombre));
                }
                return Json(combo);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerListaTiposDeSociedad()
        {
            try
            {
                List<Clase_actor> resultados = _logicaActorNegocio.ObtenerListaTiposDeSociedad();
                List<ItemGenerico> roles = new List<ItemGenerico>();
                foreach (var item in resultados)
                {
                    roles.Add(new ItemGenerico(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

         public JsonResult ObtenerListaEstadosCiviles()
        {
            try
            {
                List<Estado_legal> resultados = _logicaActorNegocio.ObtenerListaEstadosCiviles();
                List<ItemGenerico> roles = new List<ItemGenerico>();
                foreach (var item in resultados)
                {
                    roles.Add(new ItemGenerico(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }
}
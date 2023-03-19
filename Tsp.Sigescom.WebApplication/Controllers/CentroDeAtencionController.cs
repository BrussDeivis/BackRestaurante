using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Sesion;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.WebApplication.Models.Comprobante;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CentroDeAtencionController : BaseController
    {
        private readonly IConceptoLogica conceptoLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IOperacionLogica _operacionLogica;
        protected readonly ISession_Logica _sesionLogica;
        protected readonly ISede_Logica _sedeLogica;
        protected readonly ISucursal_Logica _sucursalLogica;
        protected readonly IEstablecimiento_Repositorio _establecimientoDatos;
        protected readonly ICentroDeAtencion_Logica _centroAtencionLogica;
        protected readonly IEstablecimiento_Logica _establecimientoLogica;
        protected readonly IInventarioActual_Logica _inventarioActualLogica;
        protected readonly ITipoDeCambio_Logica _tipoDeCambioLogica;








        public CentroDeAtencionController()
        {
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();

            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            _operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            _sesionLogica = Dependencia.Resolve<ISession_Logica>();
            _sedeLogica= Dependencia.Resolve<ISede_Logica>();
            _establecimientoDatos = Dependencia.Resolve<IEstablecimiento_Repositorio>();

            _sucursalLogica = Dependencia.Resolve<ISucursal_Logica>();
            _centroAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            _establecimientoLogica = Dependencia.Resolve<IEstablecimiento_Logica>();

            _inventarioActualLogica = Dependencia.Resolve<IInventarioActual_Logica>();
            _tipoDeCambioLogica = Dependencia.Resolve<ITipoDeCambio_Logica>();



        }

        #region INICIO DE SESION


        public ActionResult SeleccionarCentroDeAtencion()
        {
            UserProfileSessionData profile = _sesionLogica.GenerarSesionUsuario(User.Identity.GetUserId(), User.Identity.GetUserName());
            this.Session["UserProfile"] = profile;
            return View(profile);
        }


        [HttpPost]
        public ActionResult SeleccionarCentroDeAtencion(UserProfileSessionData profile)
        {
            var profileData = EstablecerDatosSesionUsuario(profile);

            if(profileData.MensajeError == "")
            {
                this.Session["VentaAcumulada"] = 0m;
                this.Session["VentaFacturada"] = 0m;

                Configuraciones.Reset();
                ConfiguracionesLogica.Reset();

                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Sede"] = profileData.Sede;
                System.Web.HttpContext.Current.Application.UnLock();

                return RedirectToLocal();
            }
            else
            {
                return View(profileData);
            }
        }



        private ActionResult RedirectToLocal()
        {
            return RedirectToAction("Estadistica", "Escritorio");
        }

        public JsonResult ObtenerRolesCentrosDeAtencion()
        {
            try
            {
                var resultados = _centroAtencionLogica.ObtenerRolesDeCentroDeAtencion();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                roles = ComboGenericoViewModel.Convert(resultados);
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
         


        public UserProfileSessionData EstablecerDatosSesionUsuario(UserProfileSessionData profile)
        {
            var profileData = this.ProfileData();
            profileData.MensajeError = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var sede = _sedeLogica.ObtenerSedeConLogo();
                    profileData.Sede = sede;
                    profileData.NombreSede = sede.Nombre;

                    var clientePorDefecto = actorNegocioLogica.ObtenerClienteGenerico();
                    profileData.ClientePorDefecto = clientePorDefecto;

                    if (profileData.CentrosDeAtencionProgramados.Count > 1)
                    {
                        profileData.CentroDeAtencionSeleccionado = profileData.CentrosDeAtencionProgramados.SingleOrDefault(cap => cap.Id == profile.IdCentroDeAtencionInicioSesion);
                    }

                    if (profileData.CentroDeAtencionSeleccionado != null)
                    {
                         
                        profileData.EstablecimientoComercialSeleccionado = profileData.CentroDeAtencionSeleccionado.EstablecimientoComercial.Id == sede.Id? sede:_establecimientoDatos.ObtenerEstablecimientoComercialExtendidoConLogo(profileData.CentroDeAtencionSeleccionado.EstablecimientoComercial.Id); 
                        
                        profileData.IdCentroAtencionQueTieneLosPrecios = _centroAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDePrecios(profileData.CentroDeAtencionSeleccionado, profileData.EstablecimientoComercialSeleccionado);

                        profileData.IdCentroAtencionQueTieneElStockIntegrada = _centroAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.PorMostrador, profileData.CentroDeAtencionSeleccionado, profileData.EstablecimientoComercialSeleccionado);

                        profileData.CentroAtencionQueTieneElStockIntegrada = _centroAtencionLogica.ObtenerCentroDeAtencion_(profileData.IdCentroAtencionQueTieneElStockIntegrada);

                        profileData.IdCentroAtencionQueTieneElStockDosPasos = _centroAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.PorMostradorEnDosPasos, profileData.CentroDeAtencionSeleccionado, profileData.EstablecimientoComercialSeleccionado);

                        profileData.IdCentroAtencionQueTieneElStockCorporativa = _centroAtencionLogica.ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum.Corporativa, profileData.CentroDeAtencionSeleccionado, profileData.EstablecimientoComercialSeleccionado);
                    }
                    profileData.CostoUnitarioDelIcbper = conceptoLogica.ObtenerCostoUnitarioDelIcbperALaFecha();

                    profileData.TipoDeCambio = _tipoDeCambioLogica.ObtenerTipoCambioDolarActual();

                    profileData.SetIdAlmacenIdInventarioFisico(_inventarioActualLogica.ObtenerIdsAlmacenIdsInventarioActual());

                    profileData.MaestrosFrecuentes = new MaestroSesion
                    {
                        Moneda = ItemGenerico.Convert(maestroLogica.ObtenerMonedaPorDefecto())
                    };

                    //conseguirá la operacion generica donde estarán incluidas todas las operaciones de la sesion actual.
                    profileData.OperacionSesionContenedora = _operacionLogica.ObtenerOperacionSesionContenedora(profileData.IdCentroDeAtencionSeleccionado);

                    var stringJson = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Scripts\text_file\mensaje-transaccion.json"));
                    profileData.MensajesTransaccion = JsonConvert.DeserializeObject<MensajesTransaccion>(stringJson);
                    return profileData;
                }
                else
                {
                    ModelState.AddModelError("", "Es necesario estar asignado o seleccionar una entidad interna");
                }
            }
            catch (Exception e)
            {
                profileData.MensajeError = "Error en el inicio de sesión: " + e.Message + e.StackTrace;
            }
            return profileData;
        }
        #endregion

        #region GETS
        public ActionResult Sede()
        {
            ViewBag.permitirRegistroCodigoDigemidEnEstableciemientoComercial = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial;
            ViewBag.idRolAlmacen = ActorSettings.Default.IdRolAlmacen;
            return View();
        }

        public ActionResult Sucursal()
        {
            ViewBag.permitirRegistroCodigoDigemidEnEstableciemientoComercial = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial;
            return View();
        }

        public ActionResult Sucursal_(int idSucursal)
        {
            EstablecimientoComercial sucursal = _sucursalLogica.ObtenerSucursalComoEstablecimiento(idSucursal);
            ViewBag.IdSucursal = sucursal.Id;
            ViewBag.nombre = sucursal.Nombre;
            ViewBag.idRolAlmacen = ActorSettings.Default.IdRolAlmacen;
            return View();
        }
        #endregion

        #region SEDE 
        public JsonResult CrearSede(RegistroSedeViewModel sede)
        {
            try
            {
                OperationResult resultado;
                if (sede.Id > 0)
                {
                    resultado = _sedeLogica.ActualizarSede(sede.IdActor, sede.Id, sede.NumeroDocumentoIdentidad, sede.CodigoEstablecimiento, sede.CodigoEstablecimientoDigemid, sede.InformacionPublicitaria, sede.TipoPersona.Id, sede.ClaseActor.Id, sede.RazonSocial, sede.NombreComercial, sede.NombreInterno, sede.Telefono, sede.Correo, CrearYResolverDireccion(sede.Direccion, sede.IdActor), sede.Foto.HayFoto ? Convert.FromBase64String(sede.Foto.Foto) : null);
                }
                else
                {
                    if (sede.IdActor > 0)
                    {
                        //ResolverDireccionCrearClienteActualizandoActor
                        resultado = new OperationResult();
                    }
                    else
                    {
                        resultado = _sedeLogica.CrearSede(sede.NumeroDocumentoIdentidad, sede.CodigoEstablecimiento, sede.CodigoEstablecimientoDigemid, sede.InformacionPublicitaria, sede.TipoPersona.Id, sede.ClaseActor.Id, sede.RazonSocial, sede.NombreComercial, sede.NombreInterno, sede.Telefono, sede.Correo, CrearDireccion(sede.Direccion), sede.Foto.HayFoto ? Convert.FromBase64String(sede.Foto.Foto) : null);
                    }
                }
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR SEDE");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar la sede", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerSedePrincipal()
        {
            try
            {
                //Le puse nombre ObtenerSedes porque hay un metodo obtenerSede()
                EstablecimientoComercialExtendidoConLogo sede = _sedeLogica.ObtenerSedeConLogo();
                RegistroSedeViewModel sedeViewModel = new RegistroSedeViewModel(sede);
                return Json(sedeViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerEstablecimientosComerciales()
        {
            try
            {
                List<EstablecimientoComercialExtendido> establecimientosComerciales = _establecimientoLogica.ObtenerEstablecimientosComercialesExtendidosVigentes();
                List<ComboGenericoViewModel> establecimientoComercialViewModel = ComboGenericoViewModel.Convert(establecimientosComerciales);
                return Json(establecimientoComercialViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionParaPrecios()
        {
            try
            {
                List<CentroDeAtencion> centrosDeAtencion = _centroAtencionLogica.ObtenerCentrosDeAtencionVigentes();
                List<ComboGenericoViewModel> centrosDeAtencionCombo = new List<ComboGenericoViewModel>();
                foreach (var centroDeAtencion in centrosDeAtencion)
                {
                    centrosDeAtencionCombo.Add(new ComboGenericoViewModel(centroDeAtencion.Id, centroDeAtencion.EstablecimientoComercial.NombreInterno + " - " + centroDeAtencion.Nombre));
                }
                return Json(centrosDeAtencionCombo);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public Direccion CrearYResolverDireccion(DireccionViewModel direccionViewModel, int idActor)
        {
            Direccion direccion = new Direccion();

            if (direccionViewModel != null)
            {
                if (direccionViewModel.Id > 0)
                {
                    direccion = new Direccion(direccionViewModel.Id, idActor, direccionViewModel.Tipo.Id, direccionViewModel.Nacion.Id, direccionViewModel.Ubigeo.Id, direccionViewModel.Detalle,
                        null, null, direccionViewModel.EsPrincipal, direccionViewModel.EsVigente);
                }
                else
                {
                    direccion = new Direccion(idActor, MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal, MaestroSettings.Default.IdDetalleMaestroNacionPeru, direccionViewModel.Ubigeo.Id,
                        direccionViewModel.Detalle, null, null, direccionViewModel.EsPrincipal, direccionViewModel.EsVigente);
                }
            }
            return direccion;
        }

        public Direccion CrearDireccion(DireccionViewModel direccionSede)
        {
            Direccion direccion = new Direccion();

            if (direccionSede != null)
            {
                direccion = new Direccion(MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal, MaestroSettings.Default.IdDetalleMaestroNacionPeru, direccionSede.Ubigeo.Id, direccionSede.Detalle,
                    null, null, direccionSede.EsPrincipal, direccionSede.EsVigente);
            }

            return direccion;
        }
        #endregion

        #region SUCURSAL
        public JsonResult CrearSucursal(RegistroSucursalViewModel sucursal)
        {
            try
            {
                OperationResult resultado;
                if (sucursal.Id > 0)
                {
                    resultado = _sucursalLogica.ActualizarSucursal(sucursal.IdActor, sucursal.Id, sucursal.CodigoEstablecimiento, sucursal.CodigoEstablecimientoDigemid, sucursal.InformacionPublicitaria, sucursal.Nombre, sucursal.NombreInterno, sucursal.Telefono, sucursal.Correo, CrearYResolverDireccion(sucursal.Direccion, sucursal.IdActor), sucursal.Foto.HayFoto ? Convert.FromBase64String(sucursal.Foto.Foto) : null);
                }
                else
                {
                    if (sucursal.IdActor > 0)
                    {
                        resultado = new OperationResult();
                        //ResolverDireccionCrearClienteActualizandoActor
                    }
                    else
                    {
                        resultado = _sucursalLogica.CrearSucursal(sucursal.CodigoEstablecimiento, sucursal.CodigoEstablecimientoDigemid, sucursal.InformacionPublicitaria, sucursal.Nombre, sucursal.NombreInterno, sucursal.Telefono, sucursal.Correo, CrearDireccion(sucursal.Direccion), sucursal.Foto.HayFoto ? Convert.FromBase64String(sucursal.Foto.Foto) : null);
                    }
                }

                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR LA SUCURSAL");
                return new JsonHttpStatusResult(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar la sucursal", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerSucursalesBandeja()
        {
            try
            {
                List<Sucursal> sucursales = _sucursalLogica.ObtenerSucursalesVigentes();
                List<BandejaSucursalViewModel> sucursalesViewModel = BandejaSucursalViewModel.Convert(sucursales);
                return Json(sucursalesViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerSucursales()
        {
            try
            {
                List<Sucursal> sucursales = _sucursalLogica.ObtenerSucursalesVigentes();
                List<ComboGenericoViewModel> sucursalesViewModel = ComboGenericoViewModel.Convert(sucursales);
                return Json(sucursalesViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerSucursal(int idSucursal)
        {
            try
            {
                EstablecimientoComercialExtendidoConLogo sucursal = _establecimientoDatos.ObtenerEstablecimientoComercialExtendidoConLogo(idSucursal);
                RegistroSucursalViewModel sucursalViewModel = new RegistroSucursalViewModel(sucursal);
                return Json(sucursalViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult EliminarSucursal(int idSucursal)
        {
            try
            {

                OperationResult resultado = _sucursalLogica.DarDeBajaSucursal(idSucursal);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL ELLIMINAR LA SUCURSAL");
                return new JsonHttpStatusResult(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar eliminar la sucursal", e)), HttpStatusCode.InternalServerError);
            }

        }
        #endregion

        #region CENTROS DE ATENCION
        public JsonResult CrearCentroDeAtencion(RegistroCentroDeAtencionViewModel centroDeAtencion, int idEstablecimientoComercial)
        {
            try
            {
                OperationResult resultado;
                List<int> roles = new List<int>();
                roles = centroDeAtencion.Roles.Select(r => r.Id).ToList();
                if (centroDeAtencion.Id > 0)
                {
                    resultado = _centroAtencionLogica.ActualizarCentroDeAtencion(ProfileData().Empleado.Id, centroDeAtencion.IdActor, centroDeAtencion.Id, centroDeAtencion.Codigo, centroDeAtencion.Nombre, centroDeAtencion.SalidaBienesSinStock, roles, idEstablecimientoComercial);
                }
                else
                {
                    if (centroDeAtencion.IdActor > 0)
                    {
                        resultado = new OperationResult();
                    }
                    else
                    {
                        resultado = _centroAtencionLogica.CrearCentroDeAtencion(ProfileData().Empleado.Id, centroDeAtencion.Codigo, centroDeAtencion.Nombre, centroDeAtencion.SalidaBienesSinStock, roles, idEstablecimientoComercial);
                    }
                }
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR CENTRO DE ATENCIÓN");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar centro de atención", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCentrosDeAtencionBandeja(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = new List<CentroDeAtencionExtendido>();
                centrosDeAtencion = _centroAtencionLogica.ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<BandejaCentroDeAtencionViewModel> centrosDeAtencionViewModel = BandejaCentroDeAtencionViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencion()
        {
            try
            {
                List<CentroDeAtencion> centrosDeAtencion = _centroAtencionLogica.ObtenerCentrosDeAtencionVigentes();
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentroDeAtencion(int idCentroDeAtencion, int idEstablecimientoComercial)
        {
            try
            {
                CentroDeAtencionExtendido centrosDeAtencion = _centroAtencionLogica.ObtenerCentroDeAtencionSucursalOSede(idCentroDeAtencion, idEstablecimientoComercial);
                RegistroCentroDeAtencionViewModel centroDeAtencionViewModel = new RegistroCentroDeAtencionViewModel(centrosDeAtencion);
                return Json(centroDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult EliminarCentroDeAtencion(int idCentroDeAtencion)
        {
            try
            {

                OperationResult resultado = _centroAtencionLogica.DarDeBajaCentroDeAtencion(idCentroDeAtencion);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL ELLIMINAR EL CENTRO DE ATENCION");
                return new JsonHttpStatusResult(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar eliminar el centro de atencion", e)), HttpStatusCode.InternalServerError);
            }

        }

        public JsonResult EstablecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial(int idEstablecimientoComercial, int idCentroDeAtencionPrecios, int idCentroDeAtencionStock)
        {
            try
            {
                OperationResult resultado = _centroAtencionLogica.EstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial(idEstablecimientoComercial, idCentroDeAtencionPrecios, idCentroDeAtencionStock);

                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL ESTABLECER EL CENTRO DE ATENCIÓN COMO PUNTO DE OBTENCION DE PRECIOS O STOCK");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL ESTABLECER EL CENTRO DE ATENCIÓN COMO PUNTO DE OBTENCION DE PRECIOS O STOCK", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region OBTENCION DE DATOS
        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVentaNoVigentes()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeVenta = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeVentaNoVigentes());

                return Json(comboGenericoPuntosDeVenta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompraNoVigentes()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeCompra = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeCompraNoVigentes());

                return Json(comboGenericoPuntosDeCompra);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerCentrosDeAtencionConRolAlmacenNoVigentes()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoAlmacenes = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerAlmacenesNoVigentes());

                return Json(comboGenericoAlmacenes);
            }
            catch (Exception e) { throw e; }
        }
        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVenta()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeVenta = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeVentaVigentes());

                return Json(comboGenericoPuntosDeVenta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompra()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeCompra = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeCompraVigentes());

                return Json(comboGenericoPuntosDeCompra);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolCaja()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoDeEntidadesInternasConRolCaja = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerCajasVigentes());

                return Json(comboGenericoDeEntidadesInternasConRolCaja);
            }
            catch (Exception e) { throw e; }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolAlmacen()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoAlmacenes = ComboGenericoViewModel.Convert(_centroAtencionLogica.ObtenerAlmacenesVigentes());

                return Json(comboGenericoAlmacenes);
            }
            catch (Exception e) { throw e; }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeVenta = ComboGenericoViewModel.ConvertirCentroDeAtencionConEstablecimientoComercial(_centroAtencionLogica.ObtenerPuntosDeVentaVigentes());
                return Json(comboGenericoPuntosDeVenta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConEstablecimientoComercial()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoPuntosDeCompra = ComboGenericoViewModel.ConvertirCentroDeAtencionConEstablecimientoComercial(_centroAtencionLogica.ObtenerPuntosDeCompraVigentes());
                return Json(comboGenericoPuntosDeCompra);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolCajaVigentesConEstablecimientoComercial()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoCajas = ComboGenericoViewModel.ConvertirCentroDeAtencionConEstablecimientoComercial(_centroAtencionLogica.ObtenerCajasVigentes());
                return Json(comboGenericoCajas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial()
        {
            try
            {
                List<ComboGenericoViewModel> comboGenericoAlmacenes = ComboGenericoViewModel.ConvertirCentroDeAtencionConEstablecimientoComercial(_centroAtencionLogica.ObtenerAlmacenesVigentes());
                return Json(comboGenericoAlmacenes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoPuntosDeVenta = ComboCentroAtencionViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeVentaVigentes());
                return Json(comboGenericoPuntosDeVenta);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConCodigoYEstablecimientoComercial()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoPuntosDeCompra = ComboCentroAtencionViewModel.Convert(_centroAtencionLogica.ObtenerPuntosDeCompraVigentes());
                return Json(comboGenericoPuntosDeCompra);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolCajaVigentesConCodigoYEstablecimientoComercial()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoCajas = ComboCentroAtencionViewModel.Convert(_centroAtencionLogica.ObtenerCajasVigentes());
                return Json(comboGenericoCajas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial()
        {
            try
            {
                List<ComboCentroAtencionViewModel> comboGenericoAlmacenes = ComboCentroAtencionViewModel.Convert(_centroAtencionLogica.ObtenerAlmacenesVigentes());
                return Json(comboGenericoAlmacenes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerPuntosDeCompraVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerCajasVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerAlmacenesVigentesPorEstablecimientoComercial(idEstablecimientoComercial);
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientosComerciales(int[] idsEstablecimientosComerciales)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerPuntosDeVentaVigentesPorEstablecimientosComerciales(idsEstablecimientosComerciales.ToList());
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientosComerciales(int[] idsEstablecimientosComerciales)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerPuntosDeCompraVigentesPorEstablecimientosComerciales(idsEstablecimientosComerciales.ToList());
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientosComerciales(int[] idsEstablecimientosComerciales)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerCajasVigentesPorEstablecimientosComerciales(idsEstablecimientosComerciales.ToList());
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales(int[] idsEstablecimientosComerciales)
        {
            try
            {
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerAlmacenesVigentesPorEstablecimientosComerciales(idsEstablecimientosComerciales.ToList());
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCentrosDeAtencionPorEstablecimientoComercialParaCarteraDeClientes(int idEstablecimientoComercial)
        {
            try
            {
                List<int> excludeIdsCentrosDeAtencion = actorNegocioLogica.ObtenerIdsCentrosDeAtencionDeLaCarteraDeClientes();
                List<CentroDeAtencionExtendido> centrosDeAtencion = _centroAtencionLogica.ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(idEstablecimientoComercial).Where(ca => !excludeIdsCentrosDeAtencion.Contains(ca.Id)).ToList();
                List<ComboGenericoViewModel> centrosDeAtencionViewModel = ComboGenericoViewModel.Convert(centrosDeAtencion);
                return Json(centrosDeAtencionViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.Configuraciones;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelLogica hotelLogica;
        private new readonly IActorNegocioLogica actorNegocioLogica;
        private readonly IEstablecimiento_Repositorio _establecimientoDatos;
        private readonly ICentroDeAtencion_Logica _centroDeAtencionLogica;
        private new readonly IOperacionLogica operacionLogica;
        private readonly IMaestroLogica maestroLogica;
        private readonly IConceptoLogica logicaConcepto;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IPdfUtil pdfUtil;
        protected readonly IBarCodeUtil barCodeUtil;
        protected readonly IVentaUtilitarioLogica ventaUtil;

        public HotelController()
        {
            hotelLogica = Dependencia.Resolve<IHotelLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            _establecimientoDatos = Dependencia.Resolve<IEstablecimiento_Repositorio>();
            _centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            logicaConcepto = Dependencia.Resolve<IConceptoLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            ventaUtil = Dependencia.Resolve<IVentaUtilitarioLogica>();
        }

        #region GETS
        [Authorize(Roles = "AdministradorNegocio,RecepcionistaHotel")]
        public ActionResult Index()
        {
            var fechaActual = DateTimeUtil.FechaActual().Date;
            ViewBag.fechaDesde = fechaActual.AddDays(-HotelSettings.Default.DiasAntesALaFechaActualPorDefectoEnPlanificador).ToString("dd/MM/yyyy");
            ViewBag.fechaHasta = fechaActual.AddDays(HotelSettings.Default.DiasDespuesALaFechaActualPorDefectoEnPlanificador).ToString("dd/MM/yyyy");
            ViewBag.fechaActual = fechaActual.ToString("dd/MM/yyyy");
            ViewBag.accionesEstadoDisponible = DiccionarioHotel.AccionesDeEstadoHabitacionDisponible();
            ViewBag.accionesEstadoOcupada = DiccionarioHotel.AccionesDeEstadoHabitacionOcupada();
            ViewBag.accionesEstadoReservada = DiccionarioHotel.AccionesDeEstadoHabitacionReservada();
            ViewBag.EstablecimientoSesion = ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico();
            ViewBag.UsuarioTieneRolAdministradorDeNegocio = ProfileData().Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            ViewBag.Establecimientos = ViewBag.UsuarioTieneRolAdministradorDeNegocio ? _establecimientoDatos.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos() : new List<ItemGenerico>() { ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico() };
            ViewBag.MaximoDiasMostrarEnPlanificador = HotelSettings.Default.MaximoDiasMostrarEnPlanificador;
            ViewBag.configuracionEstadoHabitacion = ConfiguracionEstadoHabitacion.Default;
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio,RecepcionistaHotel")]
        public ActionResult Reservas()
        {
            var fechaIniciales = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();
            ViewBag.fechaDesde = fechaIniciales[0].ToString("dd/MM/yyyy");
            ViewBag.fechaHasta = fechaIniciales[1].ToString("dd/MM/yyyy");
            ViewBag.EstablecimientoSesion = ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico();
            ViewBag.UsuarioTieneRolAdministradorDeNegocio = ProfileData().Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            ViewBag.Establecimientos = ViewBag.UsuarioTieneRolAdministradorDeNegocio ? _establecimientoDatos.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos() : new List<ItemGenerico>() { ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico() };
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio,RecepcionistaHotel")]
        public ActionResult Consumos()
        {
            string mascaraDeIngreso = VentasSettings.Default.MascaraDeCamposAIngresarEnVentas;
            var fechaActual = DateTimeUtil.FechaActual().Date;
            ViewBag.fechaActual = fechaActual.ToString("dd/MM/yyyy");
            ViewBag.EstablecimientoSesion = ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico();
            ViewBag.UsuarioTieneRolAdministradorDeNegocio = ProfileData().Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            ViewBag.Establecimientos = ViewBag.UsuarioTieneRolAdministradorDeNegocio ? _establecimientoDatos.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos() : new List<ItemGenerico>() { ProfileData().EstablecimientoComercialSeleccionado.ToItemGenerico() };
            //componente selectoCconceptoComercial
            ViewBag.permitirIngresarCantidad = ventaUtil.ObtenerCamposEditablesEnVentas(mascaraDeIngreso, ElementoDeCalculoEnVentasEnum.Cantidad);
            ViewBag.cursorPorDefectoCodigoBarraEnVenta = VentasSettings.Default.CursorPorDefectoEnCodigoBarraEnVenta;
            ViewBag.flujoDespuesDeCodigoBarraEnVenta = VentasSettings.Default.FlujoDespuesDeCodigoBarraEnVenta;
            ViewBag.checketDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.mascaraDeCalculoPorDefecto = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas;
            ViewBag.idConceptoBasicoBolsaPlastica = MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica;
            ViewBag.ventasSujetasADisponibilidadStock = !ProfileData().CentroDeAtencionSeleccionado.SalidaBienesSinStock;
            ViewBag.aplicarCantidadPorDefectoEnVentas = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
            ViewBag.cantidadPorDefectoEnVentas = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnVentas;
            ViewBag.modoIngresoCodigoBarraEnVenta = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta;
            ViewBag.modoDeSeleccionDeConcepto = VentasSettings.Default.ModoDeSeleccionDeConceptoDeNegocio;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Habitaciones()
        {
            ViewBag.idEstablecimientoPorDefecto = ProfileData().EstablecimientoComercialSeleccionado.Id;
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult TipoHabitacion()
        {
            ViewBag.idFamiliaHabitacion = HotelSettings.Default.IdDetalleMaestroFamiliaHabitacion;
            ViewBag.idCaracteristicaAforoAdultos = HotelSettings.Default.IdCaracteristicaAforoAdultos;
            ViewBag.idCaracteristicaAforoNinos = HotelSettings.Default.IdCaracteristicaAforoNinos;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.tamanioMaximoFotoTipoHabitacion = HotelSettings.Default.TamanioMaximoFotoTipoHabitacion;
            ViewBag.maximaCantidadFotoTipoHabitacion = HotelSettings.Default.MaximaCantidadFotoTipoHabitacion;
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Ambientes()
        {
            ViewBag.idEstablecimientoPorDefecto = ProfileData().EstablecimientoComercialSeleccionado.Id;
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio,RecepcionistaHotel")]
        public ActionResult DetalleReserva(long idEstablecimiento, long idAtencionMacro, long idAtencion)
        {
            ViewBag.accionesProcesoHabitacion = DiccionarioHotel.AccionesProcesoDeHabitacion.ToList();
            ViewBag.idAtencionMacro = idAtencionMacro;
            ViewBag.idAtencion = idAtencion;
            ViewBag.idRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.idClienteGenerico = ActorSettings.Default.IdClienteGenerico;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            ViewBag.idEstadoRegistrado = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            ViewBag.idEstadoConfirmado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            ViewBag.idEstadoCheckedIn = MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn;
            ViewBag.idEstadoCheckedOut = MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut;
            ViewBag.idEstadoFacturado = MaestroSettings.Default.IdDetalleMaestroEstadoFacturado;
            ViewBag.idEstadoAnulado = MaestroSettings.Default.IdDetalleMaestroEstadoAnulado;
            ViewBag.idEstadoEntradaCambiado = MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado;
            ViewBag.idEstadoSalidaCambiado = MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado;
            ViewBag.fechaActual = DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.idEstablecimiento = idEstablecimiento;
            return View();
        }
        public ActionResult Complementos()
        {
            return View();
        }
        #endregion

        #region PLANIFICADOR
        public JsonResult ObtenerReportePlanificador(int idEstablecimiento)
        {
            try
            {
                ReportePlanificador planificador = hotelLogica.ObtenerReportePlanificador(idEstablecimiento, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(planificador);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerPlanificadorHabitaciones(int idEstablecimiento, string fechaDesde, string fechaHasta, int idAmbiente, int idTipoHabitacion)
        {
            try
            {
                DateTime fechaInicio = DateTime.Parse(fechaDesde);
                DateTime fechaFin = DateTime.Parse(fechaHasta);
                Planificador planificadorHabitaciones = hotelLogica.ObtenerPlanificadorHabitaciones(idEstablecimiento, fechaInicio, fechaFin, idAmbiente, idTipoHabitacion, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(planificadorHabitaciones);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarEnLimpiezaHabitacion(int idHabitacion)
        {
            try
            {
                OperationResult resultado;
                resultado = hotelLogica.CambiarEnLimpiezaDeHabitacion(idHabitacion);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region HABITACIONES
        public JsonResult ObtenerHabitacion(int id)
        {
            try
            {
                Habitacion habitacion = hotelLogica.ObtenerHabitacion(id);
                return Json(habitacion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerHabitacionesBandeja(int idEstablecimiento)
        {

            try
            {
                List<HabitacionBandeja> habitaciones = hotelLogica.ObtenerHabitacionesBandeja(idEstablecimiento);

                return Json(habitaciones);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult GuardarHabitacion(Habitacion habitacion)
        {
            try
            {
                OperationResult resultado;
                if (habitacion.Id != 0)
                {
                    resultado = hotelLogica.EditarHabitacion(habitacion);
                }
                else
                {
                    resultado = hotelLogica.CrearHabitacion(habitacion);
                }

                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarEsVigenteHabitacion(int id)
        {
            try
            {
                OperationResult resultado;
                resultado = hotelLogica.CambiarEsVigenteHabitacion(id);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {

                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }

        }
        public JsonResult ObtenerHabitacionesDisponibles(int idTipoHabitacion, string fechaDesde, string fechaHasta, int idEstablecimiento, int idAmbiente)
        {
            try
            {
                DateTime fechaInicio = DateTime.Parse(fechaDesde);
                DateTime fechaFin = DateTime.Parse(fechaHasta);
                List<Habitacion> habitacionesDisponibles = hotelLogica.ObtenerHabitacionesDisponibles(idTipoHabitacion, fechaInicio, fechaFin, idEstablecimiento, idAmbiente, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(habitacionesDisponibles);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerHabitacionDisponible(int idHabitacion)
        {
            try
            {
                Habitacion habitacionDisponible = hotelLogica.ObtenerHabitacionDisponible(idHabitacion, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(habitacionDisponible);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region AMBIENTES
        public JsonResult CambiarVigenciaDelAmbienteHotel(int id)
        {
            try
            {
                //guardando eltipoHabitacion
                OperationResult resultado;

                resultado = hotelLogica.CambiarVigenciaDelAmbienteHotel(id);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAmbientesHotelPorEstablecimiento(int idEstablecimiento)
        {
            try
            {
                List<AmbienteHotel> ambientesDeHotel = hotelLogica.ObtenerAmbientesHotelPorEstablecimiento(idEstablecimiento);
                return Json(ambientesDeHotel);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAmbientesVigentesPorEstablecimientoSimplificado(int idEstablecimiento)
        {
            try
            {
                List<ItemGenerico> ambientesDeHotel = hotelLogica.ObtenerAmbientesVigentesPorEstablecimientoSimplificado(idEstablecimiento);
                return Json(ambientesDeHotel);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult GuardarAmbiente(AmbienteHotel ambiente)
        {
            try
            {
                //guardando eltipoHabitacion
                OperationResult resultado;

                if (ambiente.Id != 0)
                {
                    resultado = hotelLogica.EditarAmbiente(ambiente);
                }
                else
                {
                    resultado = hotelLogica.CrearAmbiente(ambiente);
                }
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region TIPO HABITACION 
        public JsonResult ObtenerTiposHabitacionVigentesSimplificado()
        {
            try
            {
                List<ItemGenerico> tipoHabitacionesDeHotel = hotelLogica.ObtenerTiposHabitacionVigentesSimplificado();
                return Json(tipoHabitacionesDeHotel);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarVigenciaDelTipoHabitacion(int id)
        {
            try
            {
                OperationResult resultado;
                resultado = hotelLogica.CambiarVigenciaDelTipoHabitacion(id);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerTipoHabitacion(int id)
        {
            try
            {
                TipoHabitacion tipoHabitacion = hotelLogica.ObtenerTipoHabitacion(id, ProfileData());
                List<ComboGenericoViewModel> puntosDePrecio = ComboGenericoViewModel.Convert(_centroDeAtencionLogica.ObtenerPuntosDePrecio());
                var tarifasDePrecio = ComboGenericoViewModel.Convert(maestroLogica.obtenerTarifas());
                var preciosCompraVenta = logicaConcepto.ObtenerPreciosCompraVentaDeConceptoNegocio(tipoHabitacion.Id);
                RegistroPrecioViewModel precios = new RegistroPrecioViewModel(tipoHabitacion.Precios, tipoHabitacion.Id, puntosDePrecio, tarifasDePrecio, DateTimeUtil.FechaActual());
                return Json(new { tipoHabitacion, precios });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerTipoHabitacionesBandeja()
        {
            try
            {
                List<TipoHabitacionesBandeja> tipoHabitacionesDeHotel = hotelLogica.ObtenerTipoHabitaciones();
                return Json(tipoHabitacionesDeHotel);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult GuardarTipoHabitacion(TipoHabitacion tipoHabitacion, RegistroPrecioViewModel precios)
        {
            try
            {
                OperationResult resultado;
                tipoHabitacion.Precios = RegistroPrecioViewModel.Convert(precios);
                if (tipoHabitacion.Id != 0)
                {
                    resultado = hotelLogica.EditarTipoHabitacion(tipoHabitacion, ProfileData());
                }
                else
                {
                    resultado = hotelLogica.CrearTipoHabitacion(tipoHabitacion, ProfileData());
                    tipoHabitacion.IdsValoresCaracteristicas = null;
                }
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region TIPO CAMAS 
        public JsonResult ObtenerTipoCamas()
        {
            try
            {
                List<ItemGenerico> tipoCamas = hotelLogica.ObtenerTipoCamas();
                return Json(tipoCamas);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region CARACTERISTICAS
        public JsonResult ObtenerComplementos()
        {
            try
            {
                List<ItemGenerico> tipoCamas = hotelLogica.ObtenerTipoCamas();
                return Json(tipoCamas);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult GuardarComplemento(Complemento complemento)
        {
            try
            {
                OperationResult resultado;
                if (complemento.Id != 0)
                {
                    resultado = hotelLogica.ActualizarComplemento(complemento);
                }
                else
                {
                    resultado = hotelLogica.GuardarComplemento(complemento);
                }
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region RESERVAS 
        public JsonResult ObtenerReservasBandeja(int idEstablecimiento, string fechaDesde, string fechaHasta)
        {
            try
            {
                DateTime fechaInicio = DateTime.Parse(fechaDesde);
                DateTime fechaFin = DateTime.Parse(fechaHasta + " 23:59:59");
                List<ReservaBandeja> reservaBandejas = hotelLogica.ObtenerReservaBandeja(idEstablecimiento, fechaInicio, fechaFin);
                return Json(reservaBandejas);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerParametrosParaRegistradorReserva()
        {
            try
            {
                var fechaActual = DateTimeUtil.FechaActual();
                var data = new ConfiguracionReserva
                {
                    FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                    AgregarDiaAFechaDesde = ((fechaActual.Date.AddHours(12) - fechaActual).TotalMinutes - HotelSettings.Default.ToleranciaEnMinutosParaChecking) > 0
                };
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerParametrosParaRegistradorHuesped()
        {
            try
            {
                var data = ConfiguracionHuesped.Default;
                return Json(new { data });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConfiguracionParaFacturar()
        {
            try
            {
                return Json(new { data = ConfiguracionFacturar.Default });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult GuardarReserva(AtencionMacroHotel reserva)
        {
            try
            {
                OperationResult resultado = hotelLogica.ConfirmarReserva(reserva, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CheckInReserva(AtencionMacroHotel reserva)
        {
            try
            {
                OperationResult resultado = hotelLogica.CheckInReserva(reserva, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region HUESPED
        public JsonResult ObtenerUltimoMotivoViajeHuesped(int idHuesped)
        {
            try
            {
                ItemGenerico motivoViaje = hotelLogica.ObtenerUltimoMotivoViajeHuesped(idHuesped);
                return Json(motivoViaje);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AgregarHuesped(long idAtencion, int idActorComercial, int idMotivoViaje, bool esTitular)
        {
            try
            {
                OperationResult resultado = hotelLogica.AgregarHuesped(idAtencion, idActorComercial, idMotivoViaje, esTitular);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarTitularHuesped(int idHuespedCambiado, int idHuespedNuevoTitular)
        {
            try
            {
                OperationResult resultado = hotelLogica.CambiarTitularHuesped(idHuespedCambiado, idHuespedNuevoTitular);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult EliminarHuesped(int idHuesped)
        {
            try
            {
                OperationResult resultado = hotelLogica.EliminarHuesped(idHuesped);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region OBTENCIONS DE ATENCION
        public JsonResult ObtenerAtencionMacroHotel(long idAtencion)
        {
            try
            {
                AtencionMacroHotel atencionMacro = hotelLogica.ObtenerAtencionMacro(idAtencion, ProfileData());
                return Json(atencionMacro);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionDesdeAtencionMacro(long idAtencionMacro)
        {
            try
            {
                Atencion atencion = hotelLogica.ObtenerAtencionDesdeAtencionMacro(idAtencionMacro);
                if (atencion.TieneFacturacion)
                {
                    var sede = ObtenerSede();
                    foreach (var comprobante in atencion.ComprobantesFacturados)
                    {
                        OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(comprobante.IdOrden);
                        string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                        byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                        comprobante.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytes, sede, this, maestroLogica);
                        comprobante.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                    }
                }
                return Json(atencion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionDesdeAtencion(long idAtencion)
        {
            try
            {
                Atencion atencion = hotelLogica.ObtenerAtencionDesdeAtencion(idAtencion);
                if (atencion.TieneFacturacion)
                {
                    var sede = ObtenerSede();
                    foreach (var comprobante in atencion.ComprobantesFacturados)
                    {
                        OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(comprobante.IdOrden);
                        string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                        byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                        comprobante.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytes, sede, this, maestroLogica);
                        comprobante.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                    }
                }
                return Json(atencion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region GUARDAR ANOTACION 
        public JsonResult GuardarAnotacion(AtencionHotel atencion)
        {
            try
            {
                OperationResult resultado = hotelLogica.GuardarAnotacion(atencion);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region ACCIONES DE ATENCION
        public JsonResult EditarResponsableAtencionMacro(long idAtencionMacro, int idResponsable)
        {
            try
            {
                OperationResult resultado = hotelLogica.EditarResponsableAtencionMacro(idAtencionMacro, idResponsable);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult EditarFechaAtencion(AtencionHotel atencion)
        {
            try
            {
                OperationResult resultado = hotelLogica.EditarFechaAtencion(atencion);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CambiarHabitacionAtencion(AtencionHotel atencionCambioHabitacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.CambiarHabitacionAtencion(atencionCambioHabitacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ConfirmarAtencionMacro(long idAtencionMacro, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.ConfirmarAtencionMacro(idAtencionMacro, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ConfirmarAtencion(long idAtencion, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.ConfirmarAtencion(idAtencion, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CheckInAtencionMacro(long idAtencionMacro, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.CheckInAtencionMacro(idAtencionMacro, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CheckInAtencion(long idAtencion, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.CheckInAtencion(idAtencion, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CheckOutAtencionMacro(long idAtencionMacro, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.CheckOutAtencionMacro(idAtencionMacro, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult CheckOutAtencion(long idAtencion, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.CheckOutAtencion(idAtencion, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerComprobantesAtencionMacro(long idAtencionMacro)
        {
            try
            {
                List<ComprobanteAtencion> comprobantes = hotelLogica.ObtenerComprobantesDeAtencionMacro(idAtencionMacro);
                return Json(comprobantes);  
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerComprobantesAtencion(long idAtencionMacro, long idAtencion)
        {
            try
            {
                List<ComprobanteAtencion> comprobantes = hotelLogica.ObtenerComprobantesDeAtencion(idAtencionMacro, idAtencion);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AnularAtencionMacro(long idAtencionMacro, List<ComprobanteAtencion> comprobantes, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.AnularAtencionMacro(idAtencionMacro, comprobantes, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult AnularAtencion(long idAtencion, long idAtencionMacro, List<ComprobanteAtencion> comprobantes, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.AnularAtencion(idAtencion, idAtencionMacro, comprobantes, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult RegistrarIncidenteAtencionMacro(long idAtencionMacro, bool esDevolucion, List<ComprobanteAtencion> comprobantes, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.RegistrarIncidenteAtencionMacro(idAtencionMacro, esDevolucion, comprobantes, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult RegistrarIncidenteAtencion(long idAtencion, long idAtencionMacro, bool esDevolucion, List<ComprobanteAtencion> comprobantes, string observacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.RegistrarIncidenteAtencion(idAtencion, idAtencionMacro, esDevolucion, comprobantes, observacion, ProfileData());
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title, resultado.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult FacturarAtencionMacro(Atencion atencion)
        {
            try
            {
                OperationResult resultadoFacturacion = hotelLogica.FacturarAtencionMacro(atencion, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultadoFacturacion, "Error al intentar facturar la atencion macro");
                OperationResult resultadoComprobantes = GenerarDocumentosParaVisualizar((Dictionary<long, long>)resultadoFacturacion.objeto);
                return Json(new { resultadoFacturacion.code_result, resultadoFacturacion.information, result_description = resultadoComprobantes.title, documentos = resultadoComprobantes.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult FacturarAtencion(Atencion atencion)
        {
            try
            {
                OperationResult resultadoFacturacion = hotelLogica.FacturarAtencion(atencion, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultadoFacturacion, "Error al intentar facturar la atencion macro");
                OperationResult resultadoComprobantes = GenerarDocumentosParaVisualizar((Dictionary<long, long>)resultadoFacturacion.objeto);
                return Json(new { resultadoFacturacion.code_result, resultadoFacturacion.information, result_description = resultadoComprobantes.title, documentos = resultadoComprobantes.information });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public OperationResult GenerarDocumentosParaVisualizar(Dictionary<long, long> diccionarioIdsOrdenVenta)
        {
            try
            {
                OperationResult resultado = new OperationResult();
                List<DocumentoComprobanteOrdenVenta> documentos = new List<DocumentoComprobanteOrdenVenta>();
                var sede = ObtenerSede();
                foreach (var item in diccionarioIdsOrdenVenta)
                {
                    OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(item.Value);
                    string QrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                    byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                    var htmlString80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytes, sede, this, maestroLogica);
                    var htmlStringA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytes, sede, this, maestroLogica);
                    documentos.Add(new DocumentoComprobanteOrdenVenta()
                    {
                        Id = item.Key,
                        IdOrden = item.Value,
                        CadenaHtmlDeComprobante80 = htmlString80,
                        CadenaHtmlDeComprobanteA4 = htmlStringA4
                    });
                }
                resultado.information = documentos;
                return resultado;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar generar los documentos de atencion", e);
            }
        }
        #endregion

        #region CONSUMOS
        public JsonResult ObtenerConsumos(int idEstablecimiento, string fechaDesde, string fechaHasta)
        {
            try
            {
                DateTime fechaInicio = DateTime.Parse(fechaDesde);
                DateTime fechaFin = DateTime.Parse(fechaHasta + " 23:59:59");
                List<Consumo> consumos = hotelLogica.ObtenerConsumos(idEstablecimiento, fechaInicio, fechaFin);
                return Json(consumos);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConsumoHabitacionAtencion(int idAtencion)
        {
            try
            {
                ConsumoHabitacion consumoHabitacion = hotelLogica.ObtenerConsumoHabitacion(idAtencion);
                return Json(consumoHabitacion);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ConfirmarConsumo(ConsumoHabitacion consumoHabitacion)
        {
            try
            {
                OperationResult resultado = hotelLogica.ConfirmarConsumo(consumoHabitacion, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultado, "Error al intentar confirmar los consumos");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult InvalidarConsumo(int idConsumo)
        {
            try
            {
                OperationResult resultado = hotelLogica.InvalidarConsumo(idConsumo, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultado, "Error al invalidar el consumo");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerAtencionesEnCheckedInComoHabitaciones(int idEstablecimiento)
        {
            try
            {
                List<ItemGenerico> respuesta = hotelLogica.ObtenerAtencionesEnCheckedInComoHabitaciones(idEstablecimiento);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
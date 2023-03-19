using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using System.Threading.Tasks;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class GastoController : BaseController
    {
        private readonly IOperacionLogica operacionLogica;
        private readonly IConceptoLogica conceptoLogica;

        public GastoController()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
        }

        public ActionResult Index()
        {
            ViewBag.fechaInicio = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            ViewBag.fechaFin = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.fechaActual =DateTimeUtil.FechaActual().Date;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.idDetalleMaestroCatalogoDocumentoFactura= MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.idRolProveedor = ActorSettings.Default.IdRolProveedor;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.mascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
            ViewBag.permitirSeleccionarGrupoProveedor = Diccionario.MapeoOperacionesGruposVsPermitirGrupos.Single(m => m.Key == (int)OperacionesGruposActoresComerciales.Compra).Value;
            return View();
        }

        public ActionResult Concepto()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaGasto()
        {
            try
            {
                var resultados = await operacionLogica.ObtenerTiposDeComprobanteParaGasto(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerGastos(string desde, string hasta)
        {
            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(hasta);
            //DateTime fechaHasta = operacionLogica.ObtenerFechaHastaConPrecisionDeMilisegundos(hasta + " 23:59:59");
            try
            {
                List<OrdenDeGasto> ordenDeGastos = operacionLogica.ObtenerOrdenesDeGasto(ProfileData().Empleado.Id, fechaDesde, fechaHasta);
                List<BandejaGastoViewModel> respuesta = BandejaGastoViewModel.Convert(ordenDeGastos);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerGasto(long idOrdenGasto)
        {
            try
            {
                OrdenDeGasto resultado = operacionLogica.ObtenerGasto(idOrdenGasto);
                return Json(new GastoConDetallesViewModel(resultado));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult GuardarGasto(RegistroGastoViewModel_ gasto)
        {
            try
            {
                OperationResult result = operacionLogica.GuardarGasto(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, gasto.Proveedor.Id, gasto.TipoDeComprobante.EsPropio == true ? 0 : gasto.TipoDeComprobante.TipoComprobante.Id, gasto.TipoDeComprobante.EsPropio == true && gasto.TipoDeComprobante.SerieSeleccionada == 0 ? gasto.TipoDeComprobante.Series.First().Id : gasto.TipoDeComprobante.SerieSeleccionada, gasto.TipoDeComprobante.EsPropio, gasto.TipoDeComprobante.SerieIngresada, gasto.TipoDeComprobante.NumeroIngresado, gasto.Concepto.Id, gasto.Concepto.Nombre, gasto.Detalle, gasto.Observacion, gasto.FechaEmision, gasto.Igv, gasto.Total, !gasto.EsGastoACredito, gasto.EsCreditoRapido, gasto.Cuotas != null ? ConstruirCuotas(gasto.Cuotas.ToList()) : null);
                Util.ManageIfResultIsNotSuccess(result, "Error al guardar el gasto");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al intentar guardar el gasto", e)), HttpStatusCode.InternalServerError);
            }
        }

        public List<Cuota> ConstruirCuotas(List<RegistroDetalleFinanciamientoViewModel> cuotas)
        {
            List<Cuota> cuotasConstruidas = new List<Cuota>();
            foreach (var item in cuotas)
            {
                cuotasConstruidas.Add(new Cuota()
                {
                    codigo = "",
                    fecha_emision = item.FechaVencimiento,
                    fecha_vencimiento = item.FechaVencimiento,
                    capital = item.CapitalCuota,
                    interes = item.InteresCuota,
                    total = item.ImporteCuota,
                    por_cobrar = false,
                    cuota_inicial = item.EsCuotaInicial
                });
            }
            return cuotasConstruidas;
        }

        public JsonResult InvalidarGasto(long idOrden, string observacion)
        {
            try
            {
                OperationResult result = operacionLogica.InvalidarGasto(idOrden, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, observacion);
                Util.ManageIfResultIsNotSuccess(result, "Error al intentar invalidar el gasto");
                return Json(new { result.code_result, result.data, result_description = result.title });
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

    }
}

using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class AlmacenReportesController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IAlmacenReporte_Logica almacenReportingLogica;
        protected readonly IInventarioActual_Logica inventarioActual_Logica;

        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;



        public AlmacenReportesController():base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            almacenReportingLogica = Dependencia.Resolve<IAlmacenReporte_Logica>();
            inventarioActual_Logica = Dependencia.Resolve<IInventarioActual_Logica>();

            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();

        }

        [Authorize(Roles = "Almacenero,AdministradorNegocio,Gerente")]
        public ActionResult Principal()
        {
                ViewBag.Data = almacenReportingLogica.ObtenerDatosParaReportePrincipal(ProfileData());
                return View();
        }

        [Authorize(Roles = "Almacenero,AdministradorNegocio,Gerente")]
        public ActionResult ReporteKardex()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaInicio = fechas[0].AddDays(-6);
            ViewBag.fechaFin = fechas[1];
            return View();
        }

        public ActionResult ReporteConceptoBasico()
        {
            List<DateTime> fechas = operacionLogica.ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta();
            ViewBag.fechaInicio = fechas[0].AddDays(-6);
            ViewBag.fechaFin = fechas[1];
            return View();
        }

        [Authorize(Roles = "Almacenero,AdministradorNegocio,Gerente")]
        public ActionResult ReporteInventarioValorizado()
        {
            ViewBag.fechaActual= DateTimeUtil.FechaActual();

            return View();
        }



        #region REPORTE INVENTARIO VALORIZADO
        public JsonResult ObtenerInventarioValorizado(int idAlmacen, int[] idsConceptosBasicos, int[] idsValoresDeCaracteristicas, bool conLote)
        {
            try
            {
                List<string> cadena = new List<string>();
                List<Detalle_maestro> resultados = conceptoLogica.ObtenerCaracteristicas();
                List<ComboGenericoViewModel> caracteristicas = ComboGenericoViewModel.Convert(resultados);
                List<InventarioValorizadoViewModel> inventarioViewModel;
                var idCentroAtencionPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                if (idsValoresDeCaracteristicas == null)
                {
                    idsValoresDeCaracteristicas = new int[] { };
                }

                if (idsConceptosBasicos == null)
                {
                    idsConceptosBasicos = new int[] { };
                }

                inventarioViewModel = InventarioValorizadoViewModel.Convert(inventarioActual_Logica.ObtenerInventarioValorizadoActual(idAlmacen, idCentroAtencionPrecios, idsConceptosBasicos, idsValoresDeCaracteristicas), caracteristicas, conLote);
                //Agregar el primer item total con la sumatoria general 
                decimal? cantidadTotal = null;
                decimal? costoUnitarioTotal = null;
                decimal? precioVentaTotal = null;
                decimal costoTotal = inventarioViewModel.Sum(ivm => ivm.CostoTotal);
                decimal importeTotal = inventarioViewModel.Sum(ivm => ivm.ImporteTotal);
                decimal utilidadTotal = inventarioViewModel.Sum(ivm => ivm.Utilidad);
                var totales = new InventarioValorizadoViewModel(" TOTAL ", cantidadTotal, "", costoUnitarioTotal, costoTotal, precioVentaTotal, importeTotal, utilidadTotal, new List<Valor_caracteristica_concepto_negocio>(), caracteristicas);

                cadena = ConvertInventarioValorizadoViewModel(inventarioViewModel);

               
                return Json(new { cadena, totales });
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener inventario valorizado", e);
            }
        }

        public List<String> ConvertInventarioValorizadoViewModel(List<InventarioValorizadoViewModel> inventarioViewModel)
        {
            var listDynamic = new List<dynamic>();
            List<JObject> jobject = new List<JObject>();
            List<string> cadena = new List<string>();


            foreach (var item in inventarioViewModel)
            {
                dynamic itemDynamic = new System.Dynamic.ExpandoObject();
                itemDynamic._______Nombre_De_Concepto_______ = item.Producto;
                foreach (var itemValor in item.ValoresCaracteristicas)
                {
                    ((IDictionary<string, object>)itemDynamic).Add(itemValor.NombreCaracteristica, itemValor.NombreValor);
                }
                itemDynamic.Lote = item.Lote;
                itemDynamic.Cant = item.Cantidad != null ? ((decimal)item.Cantidad).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad) : null;
                itemDynamic.CU = item.CostoUnitario != null ? ((decimal)item.CostoUnitario).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) : null;
                itemDynamic.Costo = item.CostoTotal.ToString("N2");
                itemDynamic.PU = item.PrecioVenta != null ? ((decimal)item.PrecioVenta).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) : null;
                itemDynamic.Val_Vta = item.ImporteTotal.ToString("N2");
                itemDynamic.Utilidad = item.Utilidad.ToString("N2");
                listDynamic.Add(itemDynamic);
                string json = JsonConvert.SerializeObject(itemDynamic);
                cadena.Add(json);
                jobject.Add(JObject.Parse(json));
            }

            return cadena;

        }
        #endregion



        #region REPORTE POR CONCEPTO BASICO

        public ActionResult ObtenerReporteDeSalidasDeAlcohol(string fechaInicio, string fechaFin, [System.Web.Http.FromUri] int[] idsEntidadInterna)
        {
            try
            {
                DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(fechaInicio);
                DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(fechaFin);

                List<Reporte_Concepto_Basico> detalladoConceptosBasicos = operacionLogica.ObtenerReporteDeSalidasDeAlcohol(fechaDesde, fechaHasta, idsEntidadInterna);

                List<ReporteDeConceptoBasicoAlcoholViewModel> reporteDetalladoDeConceptoBasicoAlcoholViewModel = ReporteDeConceptoBasicoAlcoholViewModel.ConvertirConceptosBasicosALitros(detalladoConceptosBasicos);


                string _nombresCentrosAtencion = "";

                foreach (var item in idsEntidadInterna)
                {
                    string _nombreCentroAtencion = centroDeAtencion_Logica.ObtenerNombreDeCentroDeAtencion(item);
                    _nombresCentrosAtencion += "  - " + _nombreCentroAtencion;
                }


                ReportParameter parametroNombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
                ReportParameter parametroNombreCentroAtencion = new ReportParameter("NombreCentroAtencion", _nombresCentrosAtencion);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                var sede = ObtenerSede();
                string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
                ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

                ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

                DateTime fechaActual =DateTimeUtil.FechaActual();
                ReportParameter fechaActualSistema = new ReportParameter("FechaActualConHora", fechaActual.ToString());

                var rptviewer = new ReportViewer();
                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Almacen/SalidasDeAlcohol.rdlc";
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametroNombreEmpresa ,parametroFechaDesde, parametroFechaHasta, parametroNombreCentroAtencion, logoSede, empleadoSede, fechaActualSistema
                 });


                ReportDataSource rptdatasourceDetalladoConceptosBasicos = new ReportDataSource("DataSetDetallado", reporteDetalladoDeConceptoBasicoAlcoholViewModel);


                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalladoConceptosBasicos);

                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);
                ViewBag.ReportViewer = rptviewer;

                return View("VisualizadorReporte");
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener reporte por concepto básico", e);
            }
        }

        #endregion

    }
}
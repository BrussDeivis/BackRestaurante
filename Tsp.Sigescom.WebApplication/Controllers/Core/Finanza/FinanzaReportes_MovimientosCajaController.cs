using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Finanza;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class FinanzaReportes_MovimientosCajaController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IFinanzaReporte_Logica finanzaReportingLogica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencionLogica;


        public FinanzaReportes_MovimientosCajaController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            finanzaReportingLogica = Dependencia.Resolve<IFinanzaReporte_Logica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();

        }

        public ActionResult MovimientosCaja_Ingresos(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago, bool todasLasOperaciones, [System.Web.Http.FromUri] int[] idsOperaciones, string nombresOperaciones)
        {
            var rptviewer = GenerarMovimientosCaja_Ingresos(esCuenta, idCaja, nombreCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago, nombresMediosPago, todasLasOperaciones, idsOperaciones, nombresOperaciones, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarMovimientosCaja_Ingresos(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago, bool todasLasOperaciones, [System.Web.Http.FromUri] int[] idsOperaciones, string nombresOperaciones, bool fromRequest)
        {
            List<IngresoEgreso> ingresos = finanzaReportingLogica.Ingresos(esCuenta, idCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago, todasLasOperaciones, idsOperaciones);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreCaja = new ReportParameter("NombreCaja", nombreCaja);
            ReportParameter parametroMediosPago = new ReportParameter("MediosPago", nombresMediosPago ?? "Todos");
            ReportParameter parametroOperaciones = new ReportParameter("Operaciones", nombresOperaciones ?? "Todas");
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Finanzas/MovimientosCaja_Ingresos.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroMediosPago,
                parametroOperaciones,
                parametroNombreCaja,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceIngresos = new ReportDataSource("DataSetIngresos", ingresos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceIngresos);
            return rptviewer;
        }
        
        public ActionResult MovimientosCaja_Egresos(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago, bool todasLasOperaciones, [System.Web.Http.FromUri] int[] idsOperaciones, string nombresOperaciones)
        {
            var rptviewer = GenerarMovimientosCaja_Egresos(esCuenta, idCaja, nombreCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago, nombresMediosPago, todasLasOperaciones, idsOperaciones, nombresOperaciones, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarMovimientosCaja_Egresos(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago, bool todasLasOperaciones, [System.Web.Http.FromUri] int[] idsOperaciones, string nombresOperaciones, bool fromRequest)
        {
            List<IngresoEgreso> egresos = finanzaReportingLogica.Egresos(esCuenta, idCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago, todasLasOperaciones, idsOperaciones);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreCaja = new ReportParameter("NombreCaja", nombreCaja);
            ReportParameter parametroMediosPago = new ReportParameter("MediosPago", nombresMediosPago ?? "Todos");
            ReportParameter parametroOperaciones = new ReportParameter("Operaciones", nombresOperaciones ?? "Todas");
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Finanzas/MovimientosCaja_Egresos.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreCaja,
                parametroMediosPago,
                parametroOperaciones,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceEgresos = new ReportDataSource("DataSetEgresos", egresos);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceEgresos);
            return rptviewer;
        }
        
        public ActionResult MovimientosCaja_Flujo(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago)
        {
            var rptviewer = GenerarMovimientosCaja_Flujo(esCuenta, idCaja, nombreCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago, nombresMediosPago, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarMovimientosCaja_Flujo(bool esCuenta, int idCaja, string nombreCaja, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, [System.Web.Http.FromUri] int[] idsMediosPago, string nombresMediosPago, bool fromRequest)
        {
            var flujo = finanzaReportingLogica.Flujo(esCuenta, idCaja, fechaDesde, fechaHasta, todosLosMediosPago, idsMediosPago);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreCaja = new ReportParameter("NombreCaja", nombreCaja);
            ReportParameter parametroMediosPago = new ReportParameter("MediosPago", nombresMediosPago ?? "Todos");
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Core/Finanzas/MovimientosCaja_Flujo.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreCaja,
                parametroMediosPago,
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceResumenFlujo = new ReportDataSource("DataSetResumenFlujo", new List<ResumenFlujo> { flujo.Resumen });
            ReportDataSource rptdatasourceDetalleFlujo = new ReportDataSource("DataSetDetalleFlujo", flujo.Detalles);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceResumenFlujo);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceDetalleFlujo);
            return rptviewer;
        }
        
    }
}
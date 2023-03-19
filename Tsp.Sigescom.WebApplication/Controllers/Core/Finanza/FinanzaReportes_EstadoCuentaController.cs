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
    public partial class FinanzaReportes_EstadoCuentaController : BaseController
    {
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IConceptoLogica conceptoLogica;
        protected readonly IFinanzaReporte_Logica finanzaReportingLogica;

        public FinanzaReportes_EstadoCuentaController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            finanzaReportingLogica = Dependencia.Resolve<IFinanzaReporte_Logica>();
        }

        public ActionResult EstadoCuenta_EstadoCuentaPorCliente(DateTime fechaDesde, DateTime fechaHasta, int idCliente, string nombreCliente)
        {
            fechaDesde = DateTime.Parse(fechaDesde.ToString("dd/MM/yyyy") + " 00:00:00");
            fechaHasta = DateTime.Parse(fechaHasta.ToString("dd/MM/yyyy") + " 23:59:59");

            EstadoDeCuenta estadoDeCuenta = operacionLogica.ObtenerEstadoDeCuentaCliente(idCliente, fechaDesde, fechaHasta);

            ReportParameter parametroNombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);
            ReportParameter parametroCliente = new ReportParameter("Cliente", nombreCliente);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroSaldoAnterior = new ReportParameter("SaldoAnterior", estadoDeCuenta.SaldoAnterior.ToString());
            ReportParameter parametroEntregas = new ReportParameter("Entregas", estadoDeCuenta.Entregas.ToString());
            ReportParameter parametroPagos = new ReportParameter("Pagos", estadoDeCuenta.Pagos.ToString());
            ReportParameter parametroSaldoFinal = new ReportParameter("SaldoFinal", estadoDeCuenta.SaldoFinal.ToString());

            var sede = ObtenerSede(); //trae de la variable de seccion
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            DateTime fechaActual = DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString());

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            var rptviewer = new ReportViewer();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Finanzas/EstadoDeCuenta.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[]
            { parametroNombreEmpresa,parametroCliente ,parametroFechaDesde, parametroFechaHasta, parametroSaldoAnterior,parametroEntregas,parametroPagos, parametroSaldoFinal, empleadoSede, fechaActualSistema, logoSede});

            ReportDataSource rptdatasourceDetallado = new ReportDataSource("DataSetDetalleEstadoDeCuenta", estadoDeCuenta.Detalle);

            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.DataSources.Add(rptdatasourceDetallado);
            rptviewer.SizeToReportContent = true;
            rptviewer.Width = Unit.Percentage(100);
            rptviewer.Height = Unit.Percentage(100);
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }
    }
}
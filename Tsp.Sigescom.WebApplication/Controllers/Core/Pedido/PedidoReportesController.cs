using Microsoft.Reporting.WebForms;
using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Venta;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PedidoReportesController : BaseController
    {
        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IPedido_Logica pedidoLogica;
        protected readonly IPedidoReporte_Logica pedidoReportingLogica;

   
        public PedidoReportesController() : base()
        {
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            pedidoLogica = Dependencia.Resolve<IPedido_Logica>();
            pedidoReportingLogica = Dependencia.Resolve<IPedidoReporte_Logica>();
        }


        public ActionResult Index()
        {
            ViewBag.Data = pedidoReportingLogica.ObtenerDatosParaReportePrincipal(ProfileData());
            return View();
        }

        public ActionResult PedidosInvalidados(int idEstablecimientoComercial, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsPuntosVenta, bool todosLosPuntosVenta, string nombresPuntosVenta)
        {
            var rptviewer = GenerarReportePedidosInvalidados(idEstablecimientoComercial, nombreEstablecimiento, fechaDesde, fechaHasta, idsPuntosVenta, todosLosPuntosVenta, nombresPuntosVenta, true) ;
            ViewBag.ReportViewer=rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReportePedidosInvalidados(int idEstablecimientoComercial,string nombreEstablecimiento,DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsPuntosVenta,bool todosLosPuntosVenta,string nombresPuntosVenta, bool fromRequest)
        {
            try
            {
                var entradas = pedidoLogica.ObtenerReportePedidosInvalidados(fechaDesde, fechaHasta, idsPuntosVenta, todosLosPuntosVenta, idEstablecimientoComercial);
                var parametrosBasicos = ObtenerParametrosBasicos();
                ReportParameter parametroPuntosVenta = new ReportParameter("PuntosVenta", nombresPuntosVenta ?? "Todas");
                ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
                ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
                ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());

                var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
                string path = @"/Content/reports/Core/Pedido/Pedidos_Invalidados.rdlc";

                rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
                rptviewer.LocalReport.SetParameters(new ReportParameter[]
                 {
                    parametrosBasicos.FechaActualSistema,
                    parametrosBasicos.LogoSede,
                    parametrosBasicos.Usuario,
                    parametroPuntosVenta,
                    parametroNombreEstablecimiento,
                    parametroFechaDesde,
                    parametroFechaHasta
                 });
                ReportDataSource rptdatasourceEntradas = new ReportDataSource("PedidosInvalidados", entradas);
                rptviewer.LocalReport.DataSources.Add(rptdatasourceEntradas);
                return rptviewer;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al obtener Pedidos Invalidados", e);

            }
        }
    }
}
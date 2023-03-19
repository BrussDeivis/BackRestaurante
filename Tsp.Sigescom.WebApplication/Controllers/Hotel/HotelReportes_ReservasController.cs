using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Logica.SigesHotel;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class HotelReportes_ReservasController : BaseController
    {
        private readonly IHotelReporte_Logica hotelReporte_Logica;

        public HotelReportes_ReservasController() : base()
        {
            hotelReporte_Logica = Dependencia.Resolve<IHotelReporte_Logica>();
        }
        public ActionResult Reservas_Confirmadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, [System.Web.Http.FromUri] int[] idsTiposHabitacion, string nombresTiposHabitacion, bool todosTiposHabitacion)
        {
            var rptviewer = GenerarReservas_Confirmadas(idEstablecimiento, nombreEstablecimiento, fechaDesde, fechaHasta, idsTiposHabitacion, nombresTiposHabitacion, todosTiposHabitacion, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }
        public ReportViewer GenerarReservas_Confirmadas(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposHabitacion, string nombresTiposHabitacion, bool todosTiposHabitacion, bool fromRequest)
        {
            List<Reserva> reservas = hotelReporte_Logica.ObtenerReservasConfirmadas(idEstablecimiento, fechaDesde, fechaHasta, todosTiposHabitacion, idsTiposHabitacion);
            var parametrosBasicos = ObtenerParametrosBasicos();
            ReportParameter parametroNombreEstablecimiento = new ReportParameter("NombreEstablecimiento", nombreEstablecimiento);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            ReportParameter parametroNombreTiposHabitacion = new ReportParameter("NombreTiposHabitacion", nombresTiposHabitacion ?? "Todos");
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Reservas_Confirmadas.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametrosBasicos.NombreSede,
                parametrosBasicos.FechaActualSistema,
                parametrosBasicos.LogoSede,
                parametrosBasicos.Usuario,
                parametroNombreEstablecimiento,
                parametroFechaDesde,
                parametroFechaHasta,
                parametroNombreTiposHabitacion
            });
            ReportDataSource rptdatasourceFacturadas = new ReportDataSource("DataSetConfirmadas", reservas);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceFacturadas);
            return rptviewer;
        }
    }
}
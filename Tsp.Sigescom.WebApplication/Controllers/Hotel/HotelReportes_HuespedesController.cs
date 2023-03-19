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
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class HotelReportes_HuespedesController : BaseController
    {

        private readonly IHotelReporte_Logica hotelReporte_Logica;

        public HotelReportes_HuespedesController() : base()
        {
            hotelReporte_Logica = Dependencia.Resolve<IHotelReporte_Logica>();
        }

        public JsonResult Huespedes_RegistroHuespedes(int idEstablecimiento, string nombreEstablecimiento, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var rptviewer = GenerarHuespedes_RegistroHuespedes(idEstablecimiento, fechaDesde, fechaHasta, true);

                string filenameContent = nombreEstablecimiento + "_FormularioT1_" + fechaDesde.ToString("dd/MM/yyyy") + "-" + fechaHasta.ToString("dd/MM/yyyy");
                string filename = string.Format("{0}.{1}", filenameContent, "xls");
                filename = filename.Replace(" ", "");
                byte[] bytes = rptviewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);
                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                return Json("Operacion realizada con exito");
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al generar el registro de huespedes", e);
            }
        }

        public ReportViewer GenerarHuespedes_RegistroHuespedes(int idEstablecimiento, DateTime fechaDesde, DateTime fechaHasta, bool fromRequest)
        {
            List<RegistroHuesped> registroHuespedes = hotelReporte_Logica.ObtenerRegistroHuespedes(idEstablecimiento, fechaDesde, fechaHasta);
            ReportParameter parametroFechaDesde = new ReportParameter("FechaDesde", fechaDesde.ToString());
            ReportParameter parametroFechaHasta = new ReportParameter("FechaHasta", fechaHasta.ToString());
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            string path = @"/Content/reports/Hotel/Huespedes_RegistroHuespedes.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {
                parametroFechaDesde,
                parametroFechaHasta
            });
            ReportDataSource rptdatasourceRegistroHuespedes = new ReportDataSource("DataSetRegistroHuespedes", registroHuespedes);
            rptviewer.LocalReport.DataSources.Add(rptdatasourceRegistroHuespedes);
            return rptviewer;
        }
    }
}
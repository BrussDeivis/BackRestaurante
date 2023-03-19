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
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class HotelReportesController : BaseController
    {
        private readonly IHotelLogica hotelLogica;
        private readonly IHotelReporte_Logica hotelReporte_Logica;
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IEstablecimiento_Logica establecimientoLogica;


        public HotelReportesController() : base()
        {
            hotelLogica = Dependencia.Resolve<IHotelLogica>();
            hotelReporte_Logica = Dependencia.Resolve<IHotelReporte_Logica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            establecimientoLogica = Dependencia.Resolve<IEstablecimiento_Logica>();

        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Reportes()
        {
            ViewBag.Data = hotelReporte_Logica.ObtenerDatosParaReportePrincipal(ProfileData());
            return View();
        }

    }
}
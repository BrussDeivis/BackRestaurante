using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;

namespace Tsp.Sigescom.WebApplication.Controllers.ApiControllers
{
    [Route("api/ReservaOnline")]
    public class ReservaOnlineController : ApiController
    {
        ObjectCache cache = MemoryCache.Default; 

        private readonly IHotelLogica hotelLogica;
        private readonly IEmpleado_Logica empleadoLogica;
        private readonly ISession_Logica sessionLogica;
        private readonly ITipoDeCambio_Logica tipoCambioLogica;

        private readonly ICentroDeAtencion_Logica centroDeAtencionLogica;

        UserProfileSessionData sessionProfile;
        public ReservaOnlineController()
        {
            hotelLogica = Dependencia.Resolve<IHotelLogica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            sessionLogica= Dependencia.Resolve<ISession_Logica>();
            tipoCambioLogica = Dependencia.Resolve<ITipoDeCambio_Logica>();
            sessionProfile = sessionLogica.GenerarSesionUsuario();

        }


       

        [HttpPost]
        public IHttpActionResult PostBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                var tipoCambio = tipoCambioLogica.ObtenerTipoCambioDolarActual();
                var completeSession = sessionLogica.ResolverSession(sessionProfile, tipoCambio, booking.IdFilial);
                hotelLogica.RegistrarBooking(booking, completeSession);
            }
            else
            {
            }

            Console.WriteLine(booking.DateBooking.EntryDate);

            return Ok();
        }


        //[HttpPost]
        //[Route("availableRooms")]
        //public IHttpActionResult PostAvailableRooms(DateBooking dateBooking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Console.WriteLine(dateBooking.EntryDate.ToString(), dateBooking.DepartureDate.ToString());
        //    }
        //    else
        //    {

        //    }


        //    return Ok(availableRooms);
        //}
        // PUT api/<controller>/5
        //[HttpPut]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //}


    }
}
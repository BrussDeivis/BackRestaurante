using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;

namespace Tsp.Sigescom.WebApplication.Controllers.HotelApiControllers
{
    public class CatalogoHabitacionesController : ApiController
    {
        private readonly IHotelLogica hotelLogica;
        List<RoomType> RoomCatalog;
        public CatalogoHabitacionesController() {
            hotelLogica = Dependencia.Resolve<IHotelLogica>();
        }

        // GET api/<controller>
        [HttpGet]
        public List<RoomType> CatologoRoomType()
        {
            return RoomCatalog;
        }
    }
}

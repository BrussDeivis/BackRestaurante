using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;

namespace Tsp.Sigescom.WebApplication.Controllers.ApiControllers
{
    [Route("api/Reserva")]
    public class HabitacionesDisponiblesController : ApiController
    {
        private readonly IHotelLogica hotelLogica;
        List<RoomType> roomsTypes;

        List<RoomType> availableRooms;

        public HabitacionesDisponiblesController()
        {
            hotelLogica = Dependencia.Resolve<IHotelLogica>();
            //RoomCatalog = new List<RoomType>()
            //{
            //    new RoomType(){Id=1,Name="Matrimonial",Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ullam natus iusto vitae, reprehenderit architecto ducimus! Molestias eos nostrum eveniet est dignissimos, quia quisquam hic obcaecati at minus distinctio natus asperiores",AdultsCapacity=2,ChildrenCapacity=2,PriceValue=80.23M,UrlImagen="https://media-cdn.tripadvisor.com/media/photo-s/14/7b/83/b0/room-mate-gorka.jpg"},
            //     new RoomType(){Id=2,Name="Ejecutivo",Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ullam natus iusto vitae, reprehenderit architecto ducimus! Molestias eos nostrum eveniet est dignissimos, quia quisquam hic obcaecati at minus distinctio natus asperiores",AdultsCapacity=2,ChildrenCapacity=2,PriceValue=100,UrlImagen="https://i.pinimg.com/originals/b6/aa/91/b6aa915a8af1139561f0b9ec24a2e5af.jpg"},
            //     new RoomType(){Id=3,Name="Simple",Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ullam natus iusto vitae, reprehenderit architecto ducimus! Molestias eos nostrum eveniet est dignissimos, quia quisquam hic obcaecati at minus distinctio natus asperiores",AdultsCapacity=2,ChildrenCapacity=2,PriceValue=100,UrlImagen="https://www.parkinternationalhotel.com/d/fraserparkinternational/media/Images/__thumbs_1050_567_crop/SingleRoom_Park_International.jpg"},
            //     new RoomType(){Id=4,Name="Premium",Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ullam natus iusto vitae, reprehenderit architecto ducimus! Molestias eos nostrum eveniet est dignissimos, quia quisquam hic obcaecati at minus distinctio natus asperiores",AdultsCapacity=2,ChildrenCapacity=2,PriceValue=250.00M,UrlImagen="https://i.pinimg.com/originals/b6/aa/91/b6aa915a8af1139561f0b9ec24a2e5af.jpg"}
            //};
            availableRooms = new List<RoomType>()
            {
               new RoomType(){Id=1,Name="Matrimonial",PriceValue=80.23M, AvailabilityAmount=4},
                 new RoomType(){Id=2,Name="Ejecutivo",PriceValue=100, AvailabilityAmount=6},
                 new RoomType(){Id=3,Name="Simple",PriceValue=100, AvailabilityAmount=5},
                 new RoomType(){Id=4,Name="Premium",PriceValue=250.00M, AvailabilityAmount=3}
            };
        }
        //Elija IHttpActionResult como el tipo de retorno de respuesta para sus métodos de controlador WebAPI para que sus métodos de acción sean más fáciles de leer, probar y mantener y también abstraer la forma en que las respuestas HTTP se construyen realmente



        // GET api/<controller>/5
        [HttpGet]
        [Route("GetAvailableRoomss")]
        public string GetAvailableRoomss(int numero)
        {
            return ("hola");
        }

        //// Post: HabitacionesDisponibles
        [HttpPost]
        public IHttpActionResult AvailableRooms(DateBooking dateBooking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    roomsTypes = hotelLogica.ObtenerRoomTypesDisponibles(dateBooking);
                    Console.WriteLine(dateBooking.EntryDate.ToString(), dateBooking.DepartureDate.ToString());
                    return Ok(roomsTypes);
                }
                else
                {
                    return BadRequest("Modelo Invalido");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las habitaciones disponibles");
            }
        }

        //[HttpPost]
        //public IHttpActionResult PostBooking(Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        hotelLogica.RegistrarBooking(booking);
        //    }
        //    else
        //    {

        //    }
        //    Console.WriteLine(booking.DateBooking.EntryDate);

        //    return Ok();
        //}

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
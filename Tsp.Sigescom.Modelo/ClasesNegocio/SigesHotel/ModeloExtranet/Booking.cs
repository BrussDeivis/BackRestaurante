using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet
{
    public class Booking
    {
        public int Id { get; set; }
        public int IdFilial { get; set; }
        public decimal TotalPrice { get; set; }
        public int QuantityRoom { get; set; }
        public PersonalData PersonalData { get; set; }
        public string ConfirmationCode { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public DateBooking DateBooking { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int NumberOfNights { get; set; }
        public PaymentTrace PaymentTrace { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet
{
    public class PaymentTrace
    {
        public int IdPaymentMethod { get; set; }
        public string ImageVoucher { get; set; }
        public string JsonPaymentInformation { get; set; }
        public PaymentInfo PaymentInformation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet
{
    public class PersonalData
    {
        
        public int IdTypeDocument { get; set; }
        public string DocumentNumber { get; set; }
        public string NameComplete { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int IdCountry { get; set; }
        public int IdDepartament { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public string HomeAddress { get; set; }
    }
}

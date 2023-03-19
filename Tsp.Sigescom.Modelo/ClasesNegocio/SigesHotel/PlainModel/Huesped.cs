using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class Huesped
    {
        public string Nombre { get; set; }
        public string ExtensionJson { get; set; }
        public bool EsTitular{ get => string.IsNullOrEmpty(ExtensionJson) ? false : JsonConvert.DeserializeObject<JsonHuesped>(ExtensionJson).estitular; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class HabitacionBandeja
    {
        public int Id { get; set; }       
        public string CodigoHabitacion { get; set; }
        public string Ambiente { get; set; }
        public string TipoHabitacion { get; set; }
        public string Camas { get; set; }
        public string CamasInformacion
        {
            get
            {
                var camas = JsonConvert.DeserializeObject<List<ItemGenerico>>(Camas);
                string cadenaCamas = "";

                foreach (var item in camas)
                {
                    cadenaCamas = $" {cadenaCamas}{item.Valor}x{item.Nombre} + ";
                }
                cadenaCamas = cadenaCamas.Remove(cadenaCamas.Length - 3);
                return cadenaCamas;
            }
            set
            {
            }
        }
        public string Aforo { get; set; }

        public string AnexoTelefono { get; set; }
        public bool EsVigente { get; set; }

    }
}

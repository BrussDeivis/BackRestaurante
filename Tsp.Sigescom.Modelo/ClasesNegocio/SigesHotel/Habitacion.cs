using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Habitacion
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string CodigoHabitacion { get; set; }
        public AmbienteHotel Ambiente { get; set; }
        public Concepto_Negocio_Comercial_ TipoHabitacion { get; set; }
        public string Anexo { get; set; }
        public bool EsVigente { get; set; }
        public List<ItemGenerico> Camas { get; set; }
        public string InformacionCamas { get; set; }
        public string InformacionAforo { get; set; }
        //public decimal Cantidad { get; set; }
        //public decimal PrecioUnitario { get; set; }
        //public decimal Importe { get; set; }

        public Habitacion() { }
    }
}

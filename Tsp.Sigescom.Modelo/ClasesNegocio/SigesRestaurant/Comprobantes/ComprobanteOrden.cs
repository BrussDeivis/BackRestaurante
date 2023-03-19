using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes
{
    public class ComprobanteOrden
    {
        public ComprobanteOrden(Orden_Atencion orden, string nombreEstablecimiento)
        {
            this.orden = orden;
            this.nombreRestaurant =nombreEstablecimiento;
        }

        private Orden_Atencion orden;
        private string nombreRestaurant;

        public Orden_Atencion Orden { get => orden; set => orden = value; }
        public string NombreRestaurant { get => nombreRestaurant; set => nombreRestaurant = value; }
    }
}

using System;
using System.Linq;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes
{
    public class ComprobanteCuentaAtencion
    {
        public ComprobanteCuentaAtencion(AtencionRestaurante atencion,string nombreEstablecimiento, DateTime fechaHora)
        {
            this.atencion = atencion;
            this.mozo = atencion.Ordenes.Last().Mozo.Nombre;
            this.nombreRestaurant = nombreEstablecimiento;
            this.fechaHora = fechaHora;
        }

        private AtencionRestaurante atencion;
        private string nombreRestaurant;
        private string mozo;
        private DateTime fechaHora;

        public AtencionRestaurante Atencion { get => atencion; set => atencion = value; }
        public string NombreRestaurant { get => nombreRestaurant; set => nombreRestaurant = value; }
        public string Mozo { get => mozo; set => mozo = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
    }
}

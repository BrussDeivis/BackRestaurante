using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    [Serializable]
    public class OrdenPorModoAtencion_Consulta
    {
        public DateTime FechaHoraAtencion { get; set; }
        public long IdAtencion { get; set; }
        public string CodigoOrden { get; set; }
        public string Mesa { get; set; }
        public string Mozo { get; set; }
        public decimal Importe { get; set; }
        public bool Facturado { get; set; }
        public string Estado { get; set; }
        public bool EsAtencionEnSalon { get; set; }
        public bool EsAtencionPorDelivery { get; set; }
        public string ModoAtencion { get => EsAtencionEnSalon ? EnumeradosRestaurant.GetDescription(ModoAtencionEnum.Salon) : (EsAtencionPorDelivery ? EnumeradosRestaurant.GetDescription(ModoAtencionEnum.Delivery) : EnumeradosRestaurant.GetDescription(ModoAtencionEnum.AlPaso)); }
        public string Ambiente { get; set; }


        public OrdenPorModoAtencion_Consulta(){}

        public static List<OrdenPorModoAtencion_Consulta> Convert()
        {
            return new List<OrdenPorModoAtencion_Consulta>();
        }
    }
}

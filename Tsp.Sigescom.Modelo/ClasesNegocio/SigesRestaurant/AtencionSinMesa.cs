using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class AtencionSinMesa
    {
        public long Id { get; set; }
        public int IdMesa { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraAtencion { get => Fecha.ToString("hh:mm tt"); }
        public bool EsDelivery { get; set; }
        public string ModoAtencion { get => EsDelivery ? "DELIVERY" : "AL PASO"; }
    }
}

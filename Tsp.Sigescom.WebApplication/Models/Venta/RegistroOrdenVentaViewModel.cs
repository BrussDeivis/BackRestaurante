using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models.Venta
{
    public class RegistroOrdenVentaViewModel
    {
        public long Id { get; set; }
        public ComboActorComercialViewModel Cliente { get; set; }
        public ComboGenericoViewModel TipoDeComprobante { get; set; }
        public ComboGenericoViewModel SerieDeComprobante { get; set; }
        public DateTime FechaRegistro { get; set; }
        public IEnumerable<RegistroDetalleVentaViewModel> Detalles { get; set; }
        public string Observacion { get; set; }
    }

    public class ConfirmadoYPagoOrdenVentaViewModel
    {
        public int IdMedioDePago { get; set; }
        public ComboGenericoViewModel EntidadFinanciera { get; set; }
        public ComboGenericoViewModel TipoTarjeta { get; set; }
        public string InformacionDeMedioPago { get; set; }
        public string Observacion { get; set; }
    }
}
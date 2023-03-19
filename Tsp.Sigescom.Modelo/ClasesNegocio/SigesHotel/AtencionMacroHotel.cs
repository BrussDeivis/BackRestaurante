using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class AtencionMacroHotel
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public ActorComercial_ Responsable { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroString { get => FechaRegistro.ToString("dd/MM/yyyy"); }
        public string FechaHoraRegistroString { get => FechaRegistro.ToString("dd/MM/yyyy hh:mm tt"); }
        /// <summary>
        /// Atributo que nos dice si es que la atencion ya tiene alguna Facturacion realizada
        /// </summary>
        public bool TieneFacturacion { get; set; }
        /// <summary>
        /// Atributo que muestra si el facturado de manera global (sino seria individual)
        /// </summary>
        public bool FacturadoGlobal { get; set; }
        public DatosVentaIntegrada Comprobante { get; set; }
        public IEnumerable<AtencionHotel> Atenciones { get; set; }
        public decimal Total { get; set; }
        public int IdMedioPagoExtranet { get; set; }
        public string ImagenVoucherExtranet { get; set; }
        public bool HayImagenVoucherExtranet { get; set; }
        public List<ItemEstado> Eventos { get; set; }

        public AtencionMacroHotel() { }
    }
}

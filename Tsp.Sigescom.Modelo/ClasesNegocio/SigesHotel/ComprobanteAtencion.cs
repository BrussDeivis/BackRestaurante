using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class ComprobanteAtencion
    {
        public long IdAtencion { get; set; }
        public long IdOrdenVenta { get; set; }
        public bool PuedeDarDeBaja { get; set; }
        public bool DarDeBaja { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public decimal Importe { get; set; }
        public decimal MontoHospedaje { get; set; }
        public decimal Descuento { get; set; }
        public decimal Diferencia { get => MontoHospedaje - Descuento; }
        public decimal MontoSoles { get; set; }
        public int IdTipoComprobante { get; set; }
        public bool EsComprobanteInterno { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna; }

        public ComprobanteAtencion() { }
    }

}

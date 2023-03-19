using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ReporteDigemid
    {
        public string CodigoEstablecimiento { get; set; }
        public string CodigoConcepto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioUnitarioPaquete
        {
            get
            { return PrecioUnitario - (PrecioUnitario * (Convert.ToDecimal(ConceptoSettings.Default.PorcentajeDescuentoParaPrecioUnitarioPaqueteEnReporteDigemid) / 100)); }
        }
        public ReporteDigemid()
        { }

        //public static List<ReporteDigemid> Convert()
        //{
        //    return new List<ReporteDigemid>();
        //}

    }
}

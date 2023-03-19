using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.ReportData
{
    public class PrincipalReportData
    {
        public List<Establecimiento> Establecimientos { get; set; }
        public Establecimiento EstablecimientoSesion { get; set; }
        public ItemGenerico PuntoVentaSesion { get; set; }

        public DateTime FechaActual_ { get; set; }

        public long FechaActual { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaHastaDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaDesdeDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }

        public bool EsAdministrador { get; set; }

    }
}

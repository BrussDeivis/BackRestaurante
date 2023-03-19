using System;

using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.Report
{
    public class PrincipalReportData
    {
        public List<Establecimiento> Establecimientos { get; set; }
        public Establecimiento EstablecimientoSesion { get; set; }
        public DateTime FechaActual_ { get; set; }
        public long FechaActual { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaDesdeDefault { get { return FechaActual_.Date.ToJavaScriptMilliseconds(); } }
        public long FechaHastaDefault { get { return FechaActual_.Date.AddDays(1).AddSeconds(-1).ToJavaScriptMilliseconds(); } }
        public int MaximoDiasReporteHotel { get { return HotelSettings.Default.MaximoDiasReporteHotel; } }

        public List<ItemGenerico> TiposHabitacion { get; set; }


        public bool EsAdministrador { get; set; }



    }
}


using System;

using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.Negocio.Venta.Report
{
    public class PrincipalReportData
    {
        public List<Establecimiento> Establecimientos { get; set; }
        public Establecimiento EstablecimientoSesion { get; set; }
        public ItemGenerico PuntoVentaSesion { get; set; }
        public List<ItemGenerico> PuntosVentas { get; set; }
        public List<Familia_Concepto_Comercial> Familias { get; set; }

        public DateTime FechaActual_ { get; set; }

        public long FechaActual { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaHastaDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaDesdeDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }

        public bool EsAdministrador { get; set; }



    }
}


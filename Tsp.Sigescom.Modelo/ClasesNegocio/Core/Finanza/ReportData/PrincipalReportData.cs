using System;

using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.Negocio.Finanza.Report
{
    public class PrincipalReportData
    {
        public List<Establecimiento> Establecimientos { get; set; }
        public Establecimiento EstablecimientoSesion { get; set; }
        public List<ItemGenerico> Cajas { get; set; }
        public ItemGenerico CajaSesion { get; set; }

        public List<ItemGenerico> MediosPago { get; set; }
        public List<ItemGenerico> MediosPagoCuenta { get; set; }
        public List<ItemGenerico> OperacionesIngresos { get; set; }
        public List<ItemGenerico> OperacionesEgresos { get; set; }

        public DateTime FechaActual_ { get; set; }
        public long FechaActual { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaHastaDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public long FechaDesdeDefault { get { return FechaActual_.ToJavaScriptMilliseconds(); } }
        public string FechaDesdeString { get { return FechaActual_.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public string FechaHastaString { get { return FechaActual_.ToString("dd/MM/yyyy hh:mm:ss tt"); } }

        public bool EsAdministrador { get; set; }

    }
}


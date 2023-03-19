using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteCaja
    {
        public bool UsuarioTieneRolCajero { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public long FechaDesdeDefault { get { return FechaDesde.ToJavaScriptMilliseconds(); } }
        public long FechaHastaDefault { get { return FechaHasta.ToJavaScriptMilliseconds(); } }
        public string FechaDesdeString { get { return FechaDesde.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public string FechaHastaString { get { return FechaHasta.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
        public ConfiguracionRestauranteCaja()
        {

        }
    }
}


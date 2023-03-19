using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    /// <summary>
    /// Configuraciones de logica en la cual esta todas las configuraciones que permiten en las funcionalidades 
    /// </summary>
    public sealed class ConfiguracionesLogica
    {
        public static void Reset()
        {
            //ConfiguracionVenta.Reset();
            //ConfiguracionRegistradorGuiaRemision.Reset();
            //ConfiguracionPago.Reset();
            //ConfiguracionRegistroActorComercial.Reset();
            //ConfiguracionRegistroDetalle.Reset();
            ConfiguracionTrazaDePago.Reset();
        }                           
    }
}

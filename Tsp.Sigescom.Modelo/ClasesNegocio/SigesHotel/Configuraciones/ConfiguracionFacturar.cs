using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public sealed class ConfiguracionFacturar
    {
        private static readonly ConfiguracionFacturar defaultInstance = new ConfiguracionFacturar();
        public static ConfiguracionFacturar Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public readonly int TipoDePagoGeneral = (int)TipoPagoAtencion.General;
        public readonly int TipoDePagoDiferenciado = (int)TipoPagoAtencion.Diferenciado;
        public readonly int IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int Formato80 = (int)FormatoImpresion._80mm;
        public readonly int FormatoA4 = (int)FormatoImpresion.A4;
    }
}


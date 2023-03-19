using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteFacturacion
    {
        private static readonly ConfiguracionRestauranteFacturacion defaultInstance = new ConfiguracionRestauranteFacturacion();
        public static ConfiguracionRestauranteFacturacion Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public readonly int TipoDePagoSimple = (int)TipoPago.Simple;
        public readonly int TipoDePagoCuentaDividida = (int)TipoPago.DivididoPorMonto;
        public readonly int TipoDePagoCuentaDiferenciadaDetallada = (int)TipoPago.DivididoPorItem;
        public readonly int IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int Formato80 = (int)FormatoImpresion._80mm;
        public readonly int FormatoA4 = (int)FormatoImpresion.A4;
        public readonly ConfiguracionRestauranteDetalleOrden ConfiguracionDetallesDeOrden = ConfiguracionRestauranteDetalleOrden.Default;
    }
}

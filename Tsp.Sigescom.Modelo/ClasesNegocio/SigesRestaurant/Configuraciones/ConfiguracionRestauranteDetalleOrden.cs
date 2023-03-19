using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;


namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteDetalleOrden
    {
        private static readonly ConfiguracionRestauranteDetalleOrden defaultInstance = new ConfiguracionRestauranteDetalleOrden();
        public static ConfiguracionRestauranteDetalleOrden Default
        {
            get
            {
                return defaultInstance;
            }
        }

        public readonly int EstadoRegistrado = (int)EstadoDeDetalleDeOrden.Registrado;
        public readonly int EstadoPreparando = (int)EstadoDeDetalleDeOrden.Preparacion;
        public readonly int EstadoServido = (int)EstadoDeDetalleDeOrden.Servido;
        public readonly int EstadoAtendido = (int)EstadoDeDetalleDeOrden.Atendido;
        public readonly int EstadoAnulado = (int)EstadoDeDetalleDeOrden.Anulado;
        public readonly int EstadoDevuelto = (int)EstadoDeDetalleDeOrden.Devuelto;
        public readonly int EstadoObservado = (int)EstadoDeDetalleDeOrden.Observado;


    }
}

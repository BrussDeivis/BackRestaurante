using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public sealed class ConfiguracionTrazaDePago
    {
        private static ConfiguracionTrazaDePago defaultInstance = new ConfiguracionTrazaDePago();
        public static ConfiguracionTrazaDePago Default
        {
            get
            {
                return defaultInstance;
            }
        }
        public static void Reset()
        {
            defaultInstance = new ConfiguracionTrazaDePago();
        }

        public readonly int IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
        public readonly int IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int IdMedioDePagoTarjetaCredito = MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito;
        public readonly int IdMedioDePagoTarjetaDebito = MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaDebito;
        public readonly int IdMedioDePagoDepositoEnCuenta = MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta;
        public readonly int IdMedioDePagoTransferencia = MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos;
        public readonly int IdMedioDePagoNotaCredito = MaestroSettings.Default.IdDetalleMaestroMedioDePagoNotaDeCredito;
        public readonly int IdMedioDePagoPuntos = MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos;
    }

    
}

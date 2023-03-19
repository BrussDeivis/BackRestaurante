using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ConfiguracionPago
    {
        public readonly int ModoPagoContado = (int)ModoPago.Contado;
        public readonly int ModoPagoCreditoRapido = (int)ModoPago.CreditoRapido;
        public readonly int ModoPagoCreditoConfigurado = (int)ModoPago.CreditoConfigurado;
        public string FechaActual;
    }
}

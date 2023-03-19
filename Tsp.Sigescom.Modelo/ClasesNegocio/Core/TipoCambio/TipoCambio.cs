using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio
{
    public class TipoCambio
    {
        public int IdMoneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public bool Estado { get; set; }
        public string FechaString { get => Fecha.ToString("dd/MM/yyyy"); }

        public TipoCambio()
        {
            IdMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaDolares;
            Fecha = DateTimeUtil.FechaActual().Date;
            ValorCompra = 0;
            ValorVenta = 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class EstadoDeCuentaClienteViewModel
    {
        public DateTime Fecha { get; set; }
        public decimal  Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public decimal Saldo { get; set; }


        public EstadoDeCuentaClienteViewModel()
        { }

        public EstadoDeCuentaClienteViewModel(Cuota deuda)
        {

        }

        public EstadoDeCuentaClienteViewModel(string cliente, decimal total, decimal acuenta, decimal deuda, string nombreCortoComprobante, string numeroSerie, string numeroComprobante)
        {

        }


    }
}
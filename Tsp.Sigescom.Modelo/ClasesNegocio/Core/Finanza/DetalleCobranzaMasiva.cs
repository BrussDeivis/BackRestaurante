using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleCobranzaMasiva
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }


        public DetalleCobranzaMasiva(int idCliente, decimal importe)
        {
            this.IdCliente = idCliente;
            this.Importe = importe;
        }

        public DetalleCobranzaMasiva(int idCliente, string nombreCliente, decimal importe)
        {
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCliente;
            this.Importe = importe;
        }


    }
}

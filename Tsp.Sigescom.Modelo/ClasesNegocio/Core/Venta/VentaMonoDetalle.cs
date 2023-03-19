using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class VentaMonoDetalle
    {
        private int idCliente;
        private string nombreCliente;
        private int idConcepto;
        private string nombreConcepto;
        private decimal cantidad;
        private decimal precioUnitario;
        private decimal importe;
        private int idComprobantePredeterminado;
        private bool esBien;
        private string mascaraDeCalculo;


        public VentaMonoDetalle()
        {
        }

        public VentaMonoDetalle(int idCliente, decimal cantidad, decimal precioUnitario)
        {
            this.IdCliente = idCliente;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
        }

        public VentaMonoDetalle(decimal cantidad, decimal precioUnitario, int idConcepto)
        {
            this.IdConcepto = idConcepto;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
        }

        //public VentaMonoDetalle(int idCliente,int idConcepto, decimal cantidad, decimal precioUnitario)
        //{
        //    this.IdCliente = idCliente;
        //    this.IdConcepto = IdConcepto;
        //    this.Cantidad = cantidad;
        //    this.PrecioUnitario = precioUnitario;
        //}

        public VentaMonoDetalle(int idCliente, string nombreCliente, string nombreConcepto ,decimal cantidad, decimal precioUnitario)
        {
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCliente;
            this.NombreConcepto = nombreConcepto;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
        }

        public VentaMonoDetalle(int idCliente, string nombreCliente, string nombreConcepto, decimal cantidad, decimal precioUnitario, decimal importe)
        {
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCliente;
            this.NombreConcepto = nombreConcepto;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
            this.importe = importe;
        }

        public VentaMonoDetalle(int idCliente, string nombreCliente, int idConcepto, string nombreConcepto, decimal cantidad, decimal precioUnitario)
        {
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCliente;
            this.IdConcepto = idConcepto;
            this.NombreConcepto = nombreConcepto;
            this.Cantidad = cantidad;
            this.PrecioUnitario = precioUnitario;
        }

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public int IdConcepto { get => idConcepto; set => idConcepto = value; }
        public string NombreConcepto { get => nombreConcepto; set => nombreConcepto = value; }
        public decimal Cantidad { get => cantidad; set => cantidad = value; }
        public decimal PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public decimal Importe { get => importe; set => importe = value; }
        public bool EsBien { get => esBien; set => esBien = value; }
        public string MascaraDeCalculo { get => mascaraDeCalculo; set => mascaraDeCalculo = value; }
        public int IdComprobantePredeterminado { get => idComprobantePredeterminado; set => idComprobantePredeterminado = value; }
    }

}

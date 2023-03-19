using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaCotizacionViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public long IdCotizacion { get; set; }
        public string Fecha { get; set; }
        public string FechaVencimiento { get; set; }
        public string CodigoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Cajero { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public bool Convertido { get; set; }

        public BandejaCotizacionViewModel(OrdenDeCotizacion orden)
        {
            this.IdOrden = orden.Id;
            this.IdCotizacion = orden.IdVenta;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.FechaVencimiento = orden.FechaVencimiento.ToString("dd/MM/yyyy");
            this.CodigoDocumento = orden.Comprobante().Tipo().Codigo;
            this.TipoDocumento = orden.Comprobante().NombreTipo;
            this.Numero = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante;
            this.IdCliente = orden.Cliente().Id;
            this.Cliente = (orden.Cliente().Id == ActorSettings.Default.IdClienteGenerico ? "-" : orden.Cliente().NumeroDocumentoIdentidadCliente) + " | " + (TransaccionSettings.Default.MostrarAliasDeClienteGenerico && !String.IsNullOrEmpty(orden.AliasCliente()) ? orden.Cliente().RazonSocial + " | " + orden.AliasCliente() : orden.Cliente().RazonSocial);
            this.Cajero = orden.Empleado().NombreCompleto;
            this.Total = orden.Total.ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.Convertido = orden.ConvertidoAVenta;
        }

        public static List<BandejaCotizacionViewModel> Convert(List<OrdenDeCotizacion> ordenesDeCotizacion)
        {
            List<BandejaCotizacionViewModel> ordenes = new List<BandejaCotizacionViewModel>();
            foreach (var ordenDeCotizacion in ordenesDeCotizacion)
            {
                ordenes.Add(new BandejaCotizacionViewModel(ordenDeCotizacion));
            }
            return ordenes;
        }

    }
}
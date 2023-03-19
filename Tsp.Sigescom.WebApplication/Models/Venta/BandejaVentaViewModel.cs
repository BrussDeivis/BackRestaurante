using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaVentaViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public string Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string CodigoDocumento { get; set; }
        public string Numero { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Cajero { get; set; }
        public string Total { get; set; }
        public string TipoDeVenta { get; set; }
        public string ModoDePago { get; set; }
        public string Estado { get; set; }
        public string EstaTransmitido { get; set; }
        public bool EsAnulableConNotaInterna { get; set; }
        public bool EsAnulableConNotaDeCredito { get; set; }
        public bool EsOrdenDeVenta { get; set; }
        
        public BandejaVentaViewModel(OperacionDeVenta orden)
        {
            this.Id = orden.Id;
            this.IdVenta = orden.IdVenta;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.TipoDocumento = orden.Comprobante().NombreTipo;
            this.CodigoDocumento = orden.Comprobante().Tipo().Codigo;
            this.Numero = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante;
            this.IdCliente = orden.Cliente().Id;
            this.Cliente = TransaccionSettings.Default.MostrarAliasDeClienteGenerico && !String.IsNullOrEmpty(orden.AliasCliente()) ? orden.Cliente().RazonSocial + " | " + orden.AliasCliente() : orden.Cliente().RazonSocial;
            this.Cajero = orden.Empleado().NombreCompleto;
            this.Total = orden.Total.ToString("N2");
            this.TipoDeVenta = Enumerado.GetDescription(orden.TipoDeVenta());
            this.Estado = orden.EstadoActual().nombre;
            this.EstaTransmitido = orden.EstaTransmitido() ? "SI" : "NO";
            this.EsAnulableConNotaInterna = orden.EsAnulableConNotaInterna();
            this.EsAnulableConNotaDeCredito = orden.EsAnulableConNotaDeCredito();
            this.EsOrdenDeVenta = orden.EsOrdenDeVenta();
        }

        public static List<BandejaVentaViewModel> Convert_(List<OperacionDeVenta> operacionesDeVenta)
        {
            List<BandejaVentaViewModel> ordenes = new List<BandejaVentaViewModel>();
            foreach (var operacionDeVenta in operacionesDeVenta)
            {
                ordenes.Add(new BandejaVentaViewModel(operacionDeVenta));
            }
            return ordenes;
        }

        public BandejaVentaViewModel(Resumen_Venta venta)
        {
            //this.Id = venta.Id;
            //this.Fecha = venta.FechaEmision.ToString("dd/MM/yyyy");
            //this.TipoDocumento = venta.TipoComprobante;
            //this.CodigoDocumento = venta.CodigoComprobante;
            //this.Numero = venta.NumeroSerie + " - " + venta.NumeroComprobante;
            //this.Cliente = (venta.IdCliente == ActorSettings.Default.IdClienteGenerico ? "-" : venta.Cliente.numero_documento_identidad) + " | " + (TransaccionSettings.Default.MostrarAliasDeClienteGenerico && !String.IsNullOrEmpty(AliasCliente(venta.AliasDeCliente)) ? RazonSocial(venta.Cliente) + " | " + AliasCliente(venta.AliasDeCliente) : RazonSocial(venta.Cliente));
            //this.Cajero = venta.Cajero.primer_nombre + " " + venta.Cajero.segundo_nombre + " " + venta.Cajero.tercer_nombre;
            //this.Total = venta.ImporteTotal.ToString("0.00");
            //this.TipoDeVenta = venta.TipoDeVenta != null ? Enumerado.GetDescription((ModoOperacionEnum)Convert.ToInt32(venta.TipoDeVenta.valor)) : Enumerado.GetDescription(ModoOperacionEnum.PorMostrador);
            //this.ModoDePago = venta.ModoDePago != null ? Enumerado.GetDescription((ModoPago)Convert.ToInt32(venta.ModoDePago.valor)) : Enumerado.GetDescription(ModoPago.Contado);
            //this.Estado = venta.Estado.nombre;
            //this.EstaTransmitido = venta.Transmitido ? "SI" : "NO";
        }

        public static List<BandejaVentaViewModel> Convert_(List<Resumen_Venta> resumesDeVenta)
        {
            List<BandejaVentaViewModel> ventas = new List<BandejaVentaViewModel>();
            foreach (var resumen in resumesDeVenta)
            {
                ventas.Add(new BandejaVentaViewModel(resumen));
            }
            return ventas;
        }

        public string AliasCliente(Parametro_transaccion parametroTransaccion)
        {
            return parametroTransaccion != null ? "(" + Convert.ToString(parametroTransaccion.valor) + ")" : "";
        }

        public string RazonSocial(Actor actor)
        {
            return actor.primer_nombre.Replace("|", " ");
        }
    }
}
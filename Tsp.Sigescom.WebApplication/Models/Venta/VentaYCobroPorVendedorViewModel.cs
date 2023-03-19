using Microsoft.Ajax.Utilities;
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
    public class VentaYCobroPorVendedorViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string PuntoDeVenta { get; set; }
        public string Almacen { get; set; }
        public string Caja { get; set; }
        public string Cajero { get; set; }
        public string Almacenero { get; set; }
        public string Vendedor { get; set; }
        public List<ClienteVentasYCobrosViewModel> Clientes { get; set; }
        public string SaldoAnteriorTotal { get; set; }
        public string CobroTotal { get; set; }
        public string SaldoTotal { get; set; }
        public string ImporteTotal { get; set; }
        public string PrecioUnitarioTotal { get; set; }
        public string CantidadTotal { get; set; }

        public VentaYCobroPorVendedorViewModel()
        {
        }

        public VentaYCobroPorVendedorViewModel(VentaYCobranzaCarteraDeClientes ventaYCobranzaMasiva, List<Deuda_Actor_Negocio> saldosAnterios)
        {
            this.Fecha = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.FechaEmision.ToString("dd/MM/yyyy HH:mm:ss") : ventaYCobranzaMasiva.Cobranza.FechaEmision.ToString("dd/MM/yyyy HH:mm:ss");

            this.PuntoDeVenta = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.NombrePuntoDeVenta : "";
            this.Almacen = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.NombreAlmacen : "";
            this.Almacenero = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.NombreAlmacenero : "";
            this.Vendedor = ventaYCobranzaMasiva.Venta != null ? ventaYCobranzaMasiva.Venta.NombreVendedor : "";

            this.Caja = ventaYCobranzaMasiva.Cobranza != null ? ventaYCobranzaMasiva.Cobranza.NombreCaja : "";
            this.Cajero = ventaYCobranzaMasiva.Cobranza != null ? ventaYCobranzaMasiva.Cobranza.NombreCajero : "";

            this.Clientes = ClienteVentasYCobrosViewModel.Convert(ventaYCobranzaMasiva, saldosAnterios);

            this.SaldoAnteriorTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.SaldoAnterior)).ToString("N2");
            this.CobroTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.Cobro)).ToString("N2");
            this.SaldoTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.SaldoActual)).ToString("N2");

            this.CantidadTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.Cantidad)).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad);
            this.PrecioUnitarioTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.PrecioUnitario)).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
            this.ImporteTotal = Clientes.Sum(d => System.Convert.ToDecimal(d.ImporteTotal)).ToString("N2");

        }

    }

    public class ClienteVentasYCobrosViewModel
    {
        [DataMember]
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string SaldoAnterior { get; set; }
        public string Cobro { get; set; }
        public string SaldoActual { get; set; }
        public string ImporteTotal { get; set; }
        public List<DetalleVentasViewModel> Detalles { get; set; }
        public string Cantidad { get; set; }
        public string PrecioUnitario { get; set; }

        public ClienteVentasYCobrosViewModel()
        {
            this.IdCliente = 0;
            this.Cliente = "";
            this.SaldoAnterior = "0.00";
            this.Cobro = "0.00";
            this.SaldoActual = "0.00";
            this.ImporteTotal = "0.00";
            this.Detalles = new List<DetalleVentasViewModel>();
        }

        public ClienteVentasYCobrosViewModel(ComboGenericoViewModel cliente, List<VentaMonoDetalle> detallesVentas, DetalleCobranzaMasiva cobranza, decimal saldoAnterior)
        {
            this.IdCliente = cliente.Id;
            this.Cliente = cliente.Nombre;
            this.Cobro = (cobranza != null ? cobranza.Importe : 0).ToString("0.00");
            if (detallesVentas != null && detallesVentas.Count() > 0)
            {
                this.ImporteTotal = (detallesVentas.Sum(d => d.Cantidad * d.PrecioUnitario)).ToString("0.00");
                this.Detalles = DetalleVentasViewModel.Convert(detallesVentas);
            }
        }

        public static List<ClienteVentasYCobrosViewModel> Convert(VentaYCobranzaCarteraDeClientes ventaYCobranzaMasiva, List<Deuda_Actor_Negocio> saldosAnterios)
        {
            var clientesVentaYCobroViewModels = new List<ClienteVentasYCobrosViewModel>();
            //Obtener lista unificada de os clientes que participan en venta y/o cobranza
            List<int> idsClientesUnificados = ventaYCobranzaMasiva.ListaClientesUnificado();
            foreach (var item in idsClientesUnificados)
            {
                List<VentaMonoDetalle> detallesVentaCliente = ventaYCobranzaMasiva.Venta?.Detalles.Where(d => d.IdCliente == item).ToList();
                DetalleCobranzaMasiva cobroCliente = ventaYCobranzaMasiva.Cobranza?.Detalles.SingleOrDefault(d => d.IdCliente == item);
                int idCliente = detallesVentaCliente != null && detallesVentaCliente.Count() > 0 ? detallesVentaCliente.FirstOrDefault().IdCliente : cobroCliente.IdCliente;
                string nombreCliente = detallesVentaCliente != null && detallesVentaCliente.Count() > 0 ? detallesVentaCliente.FirstOrDefault().NombreCliente : cobroCliente.NombreCliente;
                ComboGenericoViewModel cliente = new ComboGenericoViewModel(idCliente, nombreCliente);
                clientesVentaYCobroViewModels.Add(new ClienteVentasYCobrosViewModel(cliente, detallesVentaCliente, cobroCliente, saldosAnterios.SingleOrDefault(sa => sa.ActorNegocio.id == item).Deuda()));
            }

            clientesVentaYCobroViewModels = clientesVentaYCobroViewModels.OrderBy(dvc => dvc.Cliente).ToList();
            return clientesVentaYCobroViewModels;
        }

    }

    public class DetalleVentasViewModel
    {
        public string NombreConcepto { get; set; }
        public string Cantidad { get; set; }
        public string PrecioUnitario { get; set; }
        public string Importe { get; set; }

        public DetalleVentasViewModel()
        {
            this.NombreConcepto = "";
            this.Cantidad = 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad);
            this.PrecioUnitario = 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
            this.Importe = 0.ToString("N2");
        }
         
             
        public DetalleVentasViewModel(VentaMonoDetalle detalleVenta)
        {
            this.NombreConcepto = detalleVenta != null ? detalleVenta.NombreConcepto : "";
            this.PrecioUnitario = detalleVenta != null ? detalleVenta.PrecioUnitario.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) : 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
            this.Cantidad = detalleVenta != null ? detalleVenta.Cantidad.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad) : 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad);
            this.Importe = detalleVenta != null ? (detalleVenta.Cantidad * detalleVenta.PrecioUnitario).ToString("N2") : 0.ToString("N2");
        }

        public static List<DetalleVentasViewModel> Convert(List<VentaMonoDetalle> detallesVenta)
        {
            List<DetalleVentasViewModel> detallesVentaViewModel = new List<DetalleVentasViewModel>();
            foreach (var item in detallesVenta)
            {
                detallesVentaViewModel.Add(new DetalleVentasViewModel(item));
            }
            return detallesVentaViewModel;
        }
    }
}
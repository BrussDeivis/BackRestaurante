using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroDeVentasYCobrosViewModel
    {
        public DateTime Fecha { get; set; }
        public ComboGenericoViewModel PuntoDeVenta { get; set; }
        public ComboGenericoViewModel Almacen { get; set; }
        public ComboGenericoViewModel Caja { get; set; }
        public ComboGenericoViewModel Cajero { get; set; }
        public ComboGenericoViewModel Almacenero { get; set; }
        public ComboGenericoViewModel Vendedor { get; set; }
        public IEnumerable<RegistroClienteVentaYCobroViewModel> Clientes { get; set; }

        public RegistroDeVentasYCobrosViewModel()
        {

        }

        public static VentaMasiva ConvertVenta(RegistroDeVentasYCobrosViewModel ventasYCobros)
        {
            List<VentaMonoDetalle> detalles = new List<VentaMonoDetalle>();
            foreach (var cliente in ventasYCobros.Clientes)
            {
                var c = cliente;
                foreach (var detalle in c.Detalles)
                {
                    detalles.Add(new VentaMonoDetalle()
                    {
                        IdCliente = c.Cliente.Id,
                        IdConcepto = detalle.IdConcepto,
                        NombreConcepto = detalle.NombreConceto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        Importe = detalle.Importe,
                        IdComprobantePredeterminado = c.IdComprobantePredeterminado,
                        EsBien = detalle.EsBien,
                        MascaraDeCalculo = detalle.MascaraDeCalculo
                    });
                }
            }
            return new VentaMasiva(ventasYCobros.PuntoDeVenta.Id,ventasYCobros.Almacen.Id,ventasYCobros.Almacenero.Id ,ventasYCobros.Vendedor.Id,ventasYCobros.Caja.Id  ,ventasYCobros.Cajero.Id,ventasYCobros.Fecha, detalles);
        }

        public static CobranzaMasiva ConvertCobranza(RegistroDeVentasYCobrosViewModel ventasYCobros)
        {
            List<DetalleCobranzaMasiva> detalles = new List<DetalleCobranzaMasiva>();
            foreach (var item in ventasYCobros.Clientes)
            {
                detalles.Add(new DetalleCobranzaMasiva(item.Cliente.Id, item.Cobro) { }); 

            }
            return new CobranzaMasiva(ventasYCobros.Caja.Id, ventasYCobros.Cajero.Id, ventasYCobros.Fecha, detalles);
        }

    }

}

    public class RegistroClienteVentaYCobroViewModel
    {

        public ComboGenericoViewModel Cliente { get; set; }
        public decimal? SaldoAnterior { get; set; }
        public decimal Cobro { get; set; }
        public decimal Descuento { get; set; }
        public bool RealizaCanje { get; set; }
        public int IdComprobanteCanje { get; set; }
    public decimal SaldoActual { get; set; }
        public decimal ImporteTotal { get; set; }
        public IEnumerable<RegistroDetalleVentasViewModel> Detalles { get; set; }
        public int IdComprobantePredeterminado { get; set; }

        public RegistroClienteVentaYCobroViewModel()
        {
            this.Detalles = new List<RegistroDetalleVentasViewModel>();
        }

        public RegistroClienteVentaYCobroViewModel(Deuda_Actor_Negocio deuda)
        {
            this.Cliente = new ComboGenericoViewModel(deuda.ActorNegocio.id, deuda.ActorNegocio.PrimerNombre.Replace("|", " "));
            this.SaldoAnterior = deuda.Deuda();
            this.Cobro = 0;
            this.SaldoActual = 0;
            this.ImporteTotal = 0;
            this.Detalles = new  List<RegistroDetalleVentasViewModel>();
            this.IdComprobantePredeterminado = deuda.IdTipoComprobantePredeterminadoEntero;
        }

        public static List<RegistroClienteVentaYCobroViewModel> Convert(List<Deuda_Actor_Negocio> clientes)
        {
            List<RegistroClienteVentaYCobroViewModel> registroDeDetalleVentasYCobrosViewModel = new List<RegistroClienteVentaYCobroViewModel>();
            foreach (var item in clientes)
            {
                registroDeDetalleVentasYCobrosViewModel.Add(new RegistroClienteVentaYCobroViewModel(item));
            }
            return registroDeDetalleVentasYCobrosViewModel;
        }
    }

    public class RegistroDetalleVentasViewModel
    {
        public int IdConcepto { get; set; }
        public string NombreConceto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public bool EsBien { get; set; }
        public string MascaraDeCalculo { get; set; }
        public RegistroDetalleVentasViewModel()
        {

        }
    }




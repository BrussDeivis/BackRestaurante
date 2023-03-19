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
    public class RegistroVentaMasivaViewModel
    {
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public ComboGenericoViewModel PuntoDeVenta { get; set; }
        public ComboGenericoViewModel Almacen { get; set; }
        public ComboGenericoViewModel Caja { get; set; }
        public ComboGenericoViewModel Vendedor { get; set; }
        public ComboGenericoViewModel Cajero { get; set; }
        public ComboGenericoViewModel Almacenero { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public IEnumerable<RegistroDetalleVentasViewModel> Detalles { get; set; }

        public RegistroVentaMasivaViewModel()
        {

        }

        public static VentaMasiva ConvertVenta(RegistroVentaMasivaViewModel venta)
        {
            List<VentaMonoDetalle> detalles = new List<VentaMonoDetalle>();
            foreach (var item in venta.Detalles)
            {
                detalles.Add(new VentaMonoDetalle()
                {
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    IdConcepto = item.IdConcepto,
                    Importe = item.Importe,
                    MascaraDeCalculo = item.MascaraDeCalculo,
                    EsBien = item.EsBien
                });
            }
            return new VentaMasiva(venta.TipoDeComprobante.SerieSeleccionada,venta.PuntoDeVenta.Id, venta.Caja.Id, venta.Almacen.Id, venta.Vendedor.Id, venta.Cajero.Id, venta.Almacenero.Id,venta.FechaDeEmision, detalles);
        }


        public class RegistroDetalleVentasViewModel
        {
            public int IdConcepto { get; set; }
            public decimal Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Importe { get; set; }
            public string MascaraDeCalculo { get; set; }
            public bool EsBien { get; set; }


            public RegistroDetalleVentasViewModel()
            {

            }

            //public RegistroDetalleVentasViewModel(Deuda_Actor_Negocio deuda)
            //{
            //    this.Cliente = new ComboGenericoViewModel(deuda.ActorNegocio.id, deuda.ActorNegocio.Actor.primer_nombre);
            //    this.SaldoAnterior = deuda.Deuda();
            //    this.Cantidad = 0;
            //    this.PrecioUnitario = 0;
            //    this.Importe = 0;
            //    this.Cobro = 0;
            //    this.SaldoActual = 0;
            //}


            //public static List<RegistroDetalleventaViewModel> Convert(List<Deuda_Actor_Negocio> clientes)
            //{
            //    List<RegistroDetalleventaViewModel> registroDeDetalleventaViewModel = new List<RegistroDetalleventaViewModel>();
            //    foreach (var item in clientes)
            //    {
            //        registroDeDetalleventaViewModel.Add(new RegistroDetalleventaViewModel(item));
            //    }
            //    return registroDeDetalleventaViewModel;
            //}


        }
    }
}
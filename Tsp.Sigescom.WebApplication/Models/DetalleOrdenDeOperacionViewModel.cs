using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class DetalleOrdenDeOperacionViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public string Detalle { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public decimal Total { get; set; }
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }
        public decimal ValorIcbper { get; set; }
        public decimal Descuento { get; set; }
        public string Lote { get; set; }
        public string Vencimiento { get; set; }
        public string Registro { get; set; }
        public bool EsBien { get; set; }

        public DetalleOrdenDeOperacionViewModel()
        {
        }

        public DetalleOrdenDeOperacionViewModel(DetalleDeOperacion detalle)
        {
            this.Id = detalle.Id;
            this.IdConcepto = detalle.Producto.Id;
            this.Codigo = detalle.Producto.Codigo;
            this.Concepto = detalle.Producto.Nombre;
            this.EsBien = detalle.Producto.EsBien;
            this.Detalle = detalle.Detalle;
            this.Cantidad = detalle.Cantidad;
            this.Precio = detalle.PrecioUnitario;
            this.Descuento = detalle.Descuento;
            this.Igv = detalle.Igv;
            this.Importe = detalle.Importe;
            this.Lote = detalle.Lote;
            this.Vencimiento = detalle.Vencimiento != null ? ((DateTime)detalle.Vencimiento).ToString("dd/MM/yyyy") : null;
            this.Registro = detalle.Registro;
        }

        public DetalleOrdenDeOperacionViewModel(DetalleDeOperacion detalle, decimal valorIcbper)
        {
            this.Id = detalle.Id;
            this.IdConcepto = detalle.Producto.Id;
            this.Codigo = detalle.Producto.Codigo;
            this.Concepto = detalle.Producto.Nombre;
            this.EsBien = detalle.Producto.EsBien;
            this.Detalle = detalle.Detalle;
            this.Cantidad = detalle.Cantidad;
            this.Precio = detalle.PrecioUnitario;
            this.Descuento = detalle.Descuento;
            this.Igv = detalle.Igv;
            this.Importe = detalle.Importe;
            this.Icbper = detalle.Producto.IdConceptoBasico == MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica ? detalle.Cantidad * valorIcbper : 0;
            this.ValorIcbper = detalle.Producto.IdConceptoBasico == MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica ? valorIcbper : 0;
            this.Total = this.Importe + this.Icbper;
            this.Lote = detalle.Lote;
            this.Vencimiento = detalle.Vencimiento != null ? ((DateTime)detalle.Vencimiento).ToString("dd/MM/yyyy") : null;
            this.Registro = detalle.Registro;
        }

        public DetalleOrdenDeOperacionViewModel(DetalleDeOperacion detalle, bool esBien)
        {
            this.Id = detalle.Id;
            this.IdConcepto = detalle.Producto.Id;
            this.Concepto = detalle.Producto.Nombre;
            this.Detalle = detalle.Detalle;
            this.Cantidad = detalle.Cantidad;
            this.Precio = detalle.PrecioUnitario;
            this.Descuento = detalle.Descuento;
            this.Igv = detalle.Igv;
            this.Importe = detalle.Importe;
            this.Lote = detalle.Lote;
            this.Vencimiento = detalle.Vencimiento != null ? ((DateTime)detalle.Vencimiento).ToString("dd/MM/yyyy") : null;
            this.Registro = detalle.Registro;
            this.EsBien = esBien;
        }

        public static List<DetalleOrdenDeOperacionViewModel> Convert(List<DetalleDeOperacion> detalles)
        {
            var detalleDeOperacion = new List<DetalleOrdenDeOperacionViewModel>();
            foreach (var detalle in detalles)
            {
                detalleDeOperacion.Add(new DetalleOrdenDeOperacionViewModel(detalle));
            }
            return detalleDeOperacion;
        }

        public static List<DetalleOrdenDeOperacionViewModel> ConvertirABienes(List<DetalleDeOperacion> detalles)
        {
            var detalleDeOperacion = new List<DetalleOrdenDeOperacionViewModel>();
            foreach (var detalle in detalles)
            {
                detalleDeOperacion.Add(new DetalleOrdenDeOperacionViewModel(detalle,true));
            }
            return detalleDeOperacion;
        }

        public static List<DetalleOrdenDeOperacionViewModel> Convert(List<DetalleDeOperacion> detalles, decimal valorIcbper)
        {
            var detalleDeOperacion = new List<DetalleOrdenDeOperacionViewModel>();
            foreach (var detalle in detalles)
            {
                detalleDeOperacion.Add(new DetalleOrdenDeOperacionViewModel(detalle, valorIcbper));
            }
            return detalleDeOperacion;
        }

        //public DetalleOrdenDeOperacionViewModel(DetalleOrdenDesplazamiento detalle)
        //{
        //    this.Id = detalle.Id;
        //    this.IdConcepto = detalle.Concepto().Id;
        //    this.Concepto = detalle.Concepto().Nombre;
        //    this.Cantidad = detalle.Cantidad;
        //    this.Precio = detalle.Precio;
        //    this.Descuento = detalle.Descuento;
        //    this.Igv = detalle.Igv;
        //    this.Importe = detalle.ImporteTotal;
        //    this.Lote = detalle.Lote;
        //    this.Vencimiento = detalle.Vencimiento != null ? ((DateTime)detalle.Vencimiento).ToString("dd/MM/yyyy") : "01/01/0001";
        //    this.Registro = detalle.Registro;
        //}

        //public static List<DetalleOrdenDeOperacionViewModel> Convert(List<DetalleOrdenDesplazamiento> detalles)
        //{
        //    var detalleFacturacion = new List<DetalleOrdenDeOperacionViewModel>();

        //    foreach (var detalle in detalles)
        //    {
        //        detalleFacturacion.Add(new DetalleOrdenDeOperacionViewModel(detalle));
        //    }
        //    return detalleFacturacion;
        //}

    }
}
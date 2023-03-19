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
    public class ProductoConPreciosVentaCompraViewModel
    {
        public ComboGenericoViewModel Producto { get; set; }
        public List<PrecioYFechaViewModel> PreciosCompra { get; set; }
        public List<PrecioYFechaViewModel> PreciosVenta { get; set; }
        public string PromedioPrecioCompra { get; set; }
        public string PromedioPrecioVenta { get; set; }

        public ProductoConPreciosVentaCompraViewModel()
        {

        }
        public ProductoConPreciosVentaCompraViewModel(ConceptoDeNegocio p)
        {
            this.Producto = new ComboGenericoViewModel(p.Id, p.Nombre);

            List<PrecioDeCompra> PreciosCompra = p.Ultimos5PreciosCompra();
            List<PrecioDeVenta> PreciosVenta = p.Ultimos5PreciosDeVenta();
            
            this.PreciosCompra = new List<PrecioYFechaViewModel>();
            this.PreciosVenta = new List<PrecioYFechaViewModel>();



            foreach (var item in PreciosCompra)
            {
                this.PreciosCompra.Add(new PrecioYFechaViewModel(item.Precio.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), item.Fecha.ToString("dd/MM/yy"),item.Tarifa));
            }

            foreach (var item in PreciosVenta)
            {
                this.PreciosVenta.Add(new PrecioYFechaViewModel(item.Precio.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), item.Fecha.ToString("dd/MM/yy"), item.Tarifa));
            }
            this.PromedioPrecioCompra = PreciosCompra.Count > 0 ? PreciosCompra.Average(up => up.Precio).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) : 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio); ;
            this.PromedioPrecioVenta = PreciosVenta.Count > 0 ? PreciosVenta.Average(up => up.Precio).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) : 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio); ;
        }

        public static List<ProductoConPreciosVentaCompraViewModel> Convert(List<ConceptoDeNegocio> productos)
        {
            var ListaProductos = new List<ProductoConPreciosVentaCompraViewModel>();

            foreach (var producto in productos)
            {
                ListaProductos.Add(new ProductoConPreciosVentaCompraViewModel(producto));
            }
            return ListaProductos;
        }
    }

    [Serializable]
    [DataContract]
    public class PrecioYFechaViewModel
    {
        public string Precio { get; set; }
        public string Fecha { get; set; }
        public string Tarifa { get; set; }
        public PrecioYFechaViewModel(string precio, string fecha, string tarifa)
        {
            this.Precio = precio;
            this.Fecha = fecha;
            this.Tarifa = tarifa;
        }
    }
}
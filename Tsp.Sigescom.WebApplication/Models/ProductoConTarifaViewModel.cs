using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ProductoConTarifaViewModel
    {
        public ComboGenericoViewModel Producto { get; set; }
        public List<TarifaViewModel> Tarifas { get; set; }
        public decimal Stock { get; set; }

        public ProductoConTarifaViewModel()
        {

        }
        public ProductoConTarifaViewModel(ConceptoDeNegocio p)
        {
            this.Producto = new ComboGenericoViewModel(p.Id, p.Nombre);
            this.Tarifas = new List<TarifaViewModel>();
            //foreach (var item in p.Precios())
            //{
            //    this.Tarifas.Add(new TarifaViewModel(item.Detalle_maestro3.id, item.Detalle_maestro3.nombre,item.valor));
            //}
            this.Stock = p.Stock();
        }

        public static List<ProductoConTarifaViewModel> Convert(List<ConceptoDeNegocio> productos)
        {
            var ListaProductos = new List<ProductoConTarifaViewModel>();

            foreach (var producto in productos)
            {
                ListaProductos.Add(new ProductoConTarifaViewModel(producto));
            }
            return ListaProductos;
        }
    }
}
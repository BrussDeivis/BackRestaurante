using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaProductoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public string StockMinimo { get; set; }
        public string Stock { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
        public string UnidadMedidaComercial { get; set; }


        public BandejaProductoViewModel(ConceptoDeNegocio producto, int idCentroAtencion)
        {
            this.Id = producto.Id;
            this.CodigoBarra = producto.CodigoBarra;
            this.Nombre = producto.Nombre;
            this.Presentacion = producto.Presentacion().Nombre+" "+ Convert.ToInt32(producto.CantidadPresentacion).ToString()+" "+producto.UnidadMedidaPresentacion().Codigo;
            this.StockMinimo = producto.StockMinimo.ToString();
            this.Stock = Convert.ToInt32(producto.Stock()).ToString();
            //this.PrecioCompra = producto.PrecioCompra().ToString();
            this.PrecioVenta = producto.PrecioVentaNormal(idCentroAtencion).ToString();
            this.UnidadMedidaComercial = producto.UnidadMedidaComercial().Codigo;
        }

        public static List<BandejaProductoViewModel> Convert_(List<ConceptoDeNegocio> productos, int idCentroAtencion)
        {
            var productos_ = new List<BandejaProductoViewModel>();

            foreach (var producto in productos)
            {
                productos_.Add(new BandejaProductoViewModel(producto, idCentroAtencion));
            }
            return productos_;
        }
    }
    

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{

    [Serializable]
    [DataContract]
    public class ProductoParaCompraViewModel
    {

        //Atributos utilizados en la vista
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string SoloNombre { get; set; }
        public string NombreParaDetalle { get; set; }
        public bool EsBien { get; set; }
        public ConceptoBasicoViewModel ConceptoBasico { get; set; }



        //Se dejaron de usar
        public int IdActorVersion { get; set; }
        public int IdVersion { get; set; }
        public string VersionFila { get; set; }
        public decimal Stock { get; set; }

        public ProductoParaCompraViewModel(){
        }

        public ProductoParaCompraViewModel(Concepto_Negocio_Comercial cnComercial)
        {
            this.Id = cnComercial.Id;
            this.Nombre = cnComercial.Nombre;
            this.Nombre = cnComercial.Nombre;
            this.SoloNombre = cnComercial.SoloNombre;
            this.NombreParaDetalle = cnComercial.NombreParaDetalle;
            this.EsBien = cnComercial.EsBien;
            this.ConceptoBasico = new ConceptoBasicoViewModel();
        }

        public static List<ProductoParaCompraViewModel> ConvertProductoParaCompraViewModel(List<Concepto_Negocio_Comercial> conceptosNegociosComerciales)
        {
            List<ProductoParaCompraViewModel> productos = new List<ProductoParaCompraViewModel>();
            foreach (var item in conceptosNegociosComerciales)
            {
                productos.Add(new ProductoParaCompraViewModel(item));
            }
            return productos;
        }





        public ProductoParaCompraViewModel(ConceptoDeNegocio producto, int idEntidadInterna)
        {
            this.Id = producto.Id;
            this.Nombre = producto.CodigoBarra + " | " + producto.Nombre;
            this.SoloNombre = producto.Nombre;
            this.NombreParaDetalle = producto.Nombre;
            this.Stock = producto.Stock(idEntidadInterna);
            this.IdActorVersion = producto.IdActorExistencia();
            this.IdVersion = producto.IdExistencia();
            this.VersionFila = producto.VersionFila() != null ? Convert.ToBase64String(producto.VersionFila()) : null;
        }

        public static List<ProductoParaCompraViewModel> ConvertProductoParaCompra(List<ConceptoDeNegocio> conceptosDeNegocio, int idCentroDeAtencionSeleccionado)
        {
            List<ProductoParaCompraViewModel> productos = new List<ProductoParaCompraViewModel>();
            foreach (var item in conceptosDeNegocio)
            {
                productos.Add(new ProductoParaCompraViewModel(item, idCentroDeAtencionSeleccionado));
            }
            return productos;
        }

    }
}
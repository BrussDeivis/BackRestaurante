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
    public class ProductoParaVentaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string SoloNombre { get; set; }
        public string NombreParaDetalle { get; set; }
        public bool EsBien { get; set; }
        public int IdConceptoBasico { get; set; }
        public ConceptoBasicoViewModel ConceptoBasico { get; set; }
        public List<Complemento_Concepto_Negocio_Comercial> Complementos { get; set; }

        //Atributos que no se utiliza
        public string CodigoBarra { get; set; }
        public List<PrecioParaRegistroDeVentaViewModel> Precios { get; set; }
        public string NombreYPrecios { get; set; }
        public decimal Stock { get; set; }
        public int IdActorVersion { get; set; }
        public int IdVersion { get; set; }
        public string VersionFila { get; set; }

        public decimal Precio { get; set; }
        public decimal PrecioUnitario { get; set; }

        public bool HayDescuento { get; set; }
        public decimal Descuento { get; set; }
        public bool DescuentoPorcentaje { get; set; }
        public int CantidadMinimaDescuento { get; set; }
        public int CantidadMaximaDescuento { get; set; }

        public ProductoParaVentaViewModel()
        {
        }

        public ProductoParaVentaViewModel(Concepto_Negocio_Comercial cnComercial)
        {
            this.Id = cnComercial.Id;
            this.CodigoBarra = cnComercial.CodigoBarra;
            this.Nombre = cnComercial.Nombre;
            this.SoloNombre = cnComercial.SoloNombre;
            this.NombreParaDetalle = cnComercial.NombreParaDetalle;
            this.EsBien = cnComercial.EsBien;
            this.IdConceptoBasico = cnComercial.IdConceptoBasico;
            this.Precios = PrecioParaRegistroDeVentaViewModel.Convert(cnComercial.Precios.ToList());
            this.ConceptoBasico = new ConceptoBasicoViewModel();
        }

        public static List<ProductoParaVentaViewModel> ConvertProductoParaVentaViewModel(List<Concepto_Negocio_Comercial> conceptosNegociosComerciales)
        {

            List<ProductoParaVentaViewModel> productos = new List<ProductoParaVentaViewModel>();
            foreach (var item in conceptosNegociosComerciales)
            {
                productos.Add(new ProductoParaVentaViewModel(item));
            }
            return productos;
        }






        public ProductoParaVentaViewModel(ConceptoDeNegocio producto, int idCentroAtencionPrecio,int idCentroAtencionStock)
        {
            this.Id = producto.Id;
            this.CodigoBarra = producto.CodigoBarra;
            this.Nombre = producto.Nombre;
            this.EsBien = producto.EsBien;
            this.Stock = producto.Stock(idCentroAtencionStock);
            this.IdActorVersion = producto.IdActorExistencia();
            this.IdVersion = producto.IdExistencia();
            this.VersionFila = producto.VersionFila() != null ? Convert.ToBase64String(producto.VersionFila()) : null;
            this.Precio = producto.PrecioVentaNormal(idCentroAtencionPrecio);
            this.Precios= PrecioParaRegistroDeVentaViewModel.Convert(producto.Precios(idCentroAtencionPrecio));
            this.NombreYPrecios = CodigoBarra + " | " + Nombre + " | " ;
            this.PrecioUnitario = producto.PrecioVentaNormal(idCentroAtencionPrecio);

            foreach (var item in Precios)
            {
                this.NombreYPrecios += item.Codigo + " ";
                this.NombreYPrecios += item.Valor + " | ";
            }

            //Conservar este codigo
            if (producto.Descuento() != null)
            {
                this.HayDescuento = true;
                this.Descuento = producto.Descuento().valor;
                this.DescuentoPorcentaje = (bool)producto.Descuento().porcentaje;
                this.CantidadMinimaDescuento = (int)producto.Descuento().cantidad_minima;
                this.CantidadMaximaDescuento = (int)producto.Descuento().cantidad_maxima;
            }
            else
            {
                this.HayDescuento = false;
            }
        }

        public static List<ProductoParaVentaViewModel> ConvertProductoParaVentaViewModel(List<ConceptoDeNegocio> conceptosNegocio, int idCentroAtencionQueTieneLosPrecios, int idCentroAtencionQueTieneLaExistencia)
        {
            List<ProductoParaVentaViewModel> productos = new List<ProductoParaVentaViewModel>();
            foreach (var item in conceptosNegocio)
            {
                productos.Add(new ProductoParaVentaViewModel(item, idCentroAtencionQueTieneLosPrecios, idCentroAtencionQueTieneLaExistencia));
            }
            return productos;
        }


    }

}
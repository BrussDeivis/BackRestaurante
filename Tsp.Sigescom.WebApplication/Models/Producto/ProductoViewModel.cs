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
    public class ProductoViewModel
    {

        [DataMember]
        public int Id { get; set; }
        public ConceptoBasicoViewModel Concepto { get; set; }
        public string Sufijo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string CodigoDigemid { get; set; }
        public ComboGenericoViewModel UnidadDeMedidaCom { get; set; }
        public ComboGenericoViewModel UnidadDeMedidaRef { get; set; }
        public List<int> IdsCaracteristicas { get; set; }
        public ComboGenericoViewModel Presentacion { get; set; }
        public string Caracteristica { get; set; }
        public decimal CantidadPresentacion { get; set; }
        public ComboGenericoViewModel UnidadDeMedidaPres { get; set; }
        public ComboGenericoViewModel PresentacionSubContenido { get; set; }
        public FotoViewModel Foto { get; set; }
        public RegistroPrecioViewModel PreciosVenta { get; set; }
        public decimal StockMinimo { get; set; }
        public List<int> ModulosAdicionales { get; set; }

        private List<int> ConvertirRolesAModulos(List<int> idsRol)
        {
            List<int> idModulosAdicionales = new List<int>();
            foreach (var idRol in idsRol)
            {
                if (idRol != ConceptoSettings.Default.IdRolMercaderia)
                    idModulosAdicionales.Add(Diccionario.MapeoModuloVsRolNegocio.Single(m => m.Value == idRol).Key);
            }
            return idModulosAdicionales;
        }

        public ProductoViewModel()
        {

        }

        public ProductoViewModel(ConceptoDeNegocio producto, List<Precio_Compra_Venta_Concepto> precios, List<ComboGenericoViewModel> totalPuntos, List<ComboGenericoViewModel> totalTarifas, DateTime fechaActual)
        {
            this.Id = producto.Id;
            this.Nombre = producto.Nombre;
            this.Sufijo = producto.Sufijo;
            this.Codigo = producto.Codigo;
            this.CodigoBarra = producto.CodigoBarra;
            this.CodigoDigemid = producto.CodigoDigemid;
            this.Concepto = new ConceptoBasicoViewModel(producto.ConceptoBasico());
            this.UnidadDeMedidaCom = new ComboGenericoViewModel(producto.UnidadMedidaComercial().Id, producto.UnidadMedidaComercial().Nombre);
            this.UnidadDeMedidaRef = new ComboGenericoViewModel(producto.UnidadMedidaReferencial().Id, producto.UnidadMedidaReferencial().Nombre);
            this.UnidadDeMedidaPres = new ComboGenericoViewModel(producto.UnidadMedidaPresentacion().Id, producto.UnidadMedidaPresentacion().Nombre);
            this.Presentacion = new ComboGenericoViewModel(producto.Presentacion().Id, producto.Presentacion().Nombre);
            this.CantidadPresentacion = producto.CantidadPresentacion;
            this.Foto = new FotoViewModel(producto.HayFoto(), producto.Foto());
            this.IdsCaracteristicas = producto.IdsValorCaracteristicas();
            this.PreciosVenta = new RegistroPrecioViewModel(precios, producto.Id, totalPuntos, totalTarifas, fechaActual);
            this.StockMinimo = producto.StockMinimo;
            this.ModulosAdicionales = ConvertirRolesAModulos(producto.IdsRolesAdicionales());
        }
    }
}

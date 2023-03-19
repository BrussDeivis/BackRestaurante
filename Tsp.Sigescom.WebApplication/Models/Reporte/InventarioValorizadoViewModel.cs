using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class InventarioValorizadoViewModel
    {
        public string Producto { get; set; }
        public string Lote { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? CostoUnitario { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Utilidad { get; set; }
        public List<DetalleMaestroValorCaracteristicaViewModel> ValoresCaracteristicas { get; set; }


        public InventarioValorizadoViewModel()
        { }

        public InventarioValorizadoViewModel(Reporte_Inventario_Valorizado valorizado, List<ComboGenericoViewModel> caracteristicas)
        {

            this.Producto = valorizado.Producto;
            this.Cantidad = valorizado.Cantidad;
            this.Lote = valorizado.Lote;
            this.CostoUnitario = valorizado.CostoUnitario;
            this.CostoTotal = valorizado.CostoTotal;
            this.PrecioVenta = valorizado.PrecioVenta;
            this.ImporteTotal = valorizado.ImporteTotal;
            this.Utilidad = valorizado.Utilidad;
            this.ValoresCaracteristicas = DetalleMaestroValorCaracteristicaViewModel.Match(ValorDeCaracteristicaConceptoNegocioViewModel.Convert(valorizado.ValorCaracteristicasConceptoNegocio.ToList()), caracteristicas);
        }

        public InventarioValorizadoViewModel(string producto,decimal? cantidadTotal, string lote, decimal? costoUnitario, decimal costoTotal, decimal? precioVenta, decimal importeTotal, decimal utilidad, List<Valor_caracteristica_concepto_negocio> valoresCaracteristicas,List<ComboGenericoViewModel> caracteristicas)
        {

            this.Producto = producto;
            this.Lote = lote;
            this.Cantidad = cantidadTotal;
            this.CostoUnitario = costoUnitario;
            this.CostoTotal = costoTotal;
            this.PrecioVenta = precioVenta;
            this.ImporteTotal = importeTotal;
            this.Utilidad = utilidad;
            this.ValoresCaracteristicas = DetalleMaestroValorCaracteristicaViewModel.Match(ValorDeCaracteristicaConceptoNegocioViewModel.Convert(valoresCaracteristicas), caracteristicas);
        }


        public InventarioValorizadoViewModel(decimal cantidad, decimal costoUnitario, decimal precioVenta)
        {
            this.Cantidad = cantidad;
            this.CostoUnitario = costoUnitario;
            this.CostoTotal = cantidad * costoUnitario;
            this.PrecioVenta = precioVenta;
            this.ImporteTotal = cantidad * precioVenta;
            this.Utilidad = ImporteTotal - CostoTotal;
        }



        public static List<InventarioValorizadoViewModel> Convert(List<Reporte_Inventario_Valorizado> inventarios, List<ComboGenericoViewModel> caracteristicas, bool conLote)
        {
            var inventariosViewModel = new List<InventarioValorizadoViewModel>();

            if (conLote)
            {
                foreach (var i in inventarios)
                {
                    inventariosViewModel.Add(new InventarioValorizadoViewModel(i, caracteristicas));
                }
            }
            else
            {
                var inventariosAgrupados = Reporte_Inventario_Valorizado.AgruparPorIdConcpetoNegocio(inventarios);
                foreach (var i in inventariosAgrupados)
                {
                    inventariosViewModel.Add(new InventarioValorizadoViewModel(i, caracteristicas));
                }
            }

            

            
            return inventariosViewModel;
        }

        public static List<InventarioValorizadoViewModel> Convert(decimal cantidad, decimal costoUnitario, Ultimo_Precio_Compra_Venta ultimosPrecios)
        {
            List<InventarioValorizadoViewModel> inventarioValorizadoViewModel = new List<InventarioValorizadoViewModel>();
            //decimal costoUnitario = ultimosPrecios.UltimoPrecioCompra != null ? (decimal)ultimosPrecios.UltimoPrecioCompra : 0;
            decimal precioVenta = ultimosPrecios.UltimoPrecioVenta != null ? (decimal)ultimosPrecios.UltimoPrecioVenta : 0;
            inventarioValorizadoViewModel.Add(new InventarioValorizadoViewModel(cantidad, costoUnitario, precioVenta));
            return inventarioValorizadoViewModel;
        }
        
        public static List<InventarioValorizadoViewModel> Convert(List<KardexViewModel> kardex, Ultimo_Precio_Compra_Venta ultimosPrecios)
        {
            List<InventarioValorizadoViewModel> inventarioValorizadoViewModel = new List<InventarioValorizadoViewModel>();
            //Obtener el saldo final
            decimal cantidad = kardex.Count() > 0 ?  System.Convert.ToDecimal(kardex.OrderByDescending(k => k.Index).First().Saldo) : 0;
            decimal costoUnitario = ultimosPrecios.UltimoPrecioCompra != null ? (decimal)ultimosPrecios.UltimoPrecioCompra : 0;
            decimal precioVenta = ultimosPrecios.UltimoPrecioVenta != null ? (decimal)ultimosPrecios.UltimoPrecioVenta : 0;
            inventarioValorizadoViewModel.Add(new InventarioValorizadoViewModel(cantidad, costoUnitario, precioVenta));
            return inventarioValorizadoViewModel;
        }

        public static List<InventarioValorizadoViewModel> Convert()
        {
            return new List<InventarioValorizadoViewModel>();
        }

    }

}
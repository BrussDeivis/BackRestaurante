using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models//Tsp.Sigescom.WebApplication.Models.concepto
  {

    [Serializable]
  
    public class StockViewModel
    {
        public string CodigoConcepto { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedidad{ get; set; }
        public decimal Stock { get; set; }
        public string NombreBasico { get; set; }
        public bool TieneStock { get; set; }
        public StockViewModel()
        {
            
        }
        public StockViewModel(string codigoConcepto, string nombre, string descripcion, string unidadMedidad, decimal stock , string nombreBasico)
        {
            CodigoConcepto = codigoConcepto;
            Nombre = nombre;
            UnidadMedidad = unidadMedidad;
            Stock = stock;
            NombreBasico = nombreBasico;
            TieneStock =stock> 0;
    }

        public StockViewModel(Stock stock)
        {
            CodigoConcepto = stock.CodigoBarraProducto;
            Nombre = stock.NombreProducto;
            UnidadMedidad = stock.producto().UnidadMedidaComercial().Codigo;
            Stock = stock.Valor;
            NombreBasico = stock.producto().ConceptoBasico().Nombre;
            TieneStock = Stock > 0;
        }
      

        public static List<StockViewModel> Convert(List<Stock> stocks)
        {
            var stockViewModels = new List<StockViewModel>();

            foreach (var stock in stocks)
            {   stockViewModels.Add(new StockViewModel(stock)); }

            return stockViewModels;
        }

    }
}
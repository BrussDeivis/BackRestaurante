using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
namespace Tsp.Sigescom.WebApplication.Models
  {

    [Serializable]
  
    public class StockGeneralViewModel
    {
        public string CodigoConcepto { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedidad{ get; set; }
        public decimal Stock { get; set; }
        public string NombreBasico { get; set; }
        public string NombreEntidadInterna { get; set; }
        public int IdEntidadInterna{ get; set; }
        public bool TieneStock { get; set; }

        public StockGeneralViewModel()
        {
            
        }

        public StockGeneralViewModel(ConceptoDeNegocio producto,int idEntidadInterna)
        {
            CodigoConcepto = producto.CodigoBarra;
            Nombre = producto.Nombre;
            UnidadMedidad = producto.UnidadMedidaComercial().Codigo;
            Stock = producto.Stock(idEntidadInterna);
            NombreBasico = producto.ConceptoBasico().Nombre;
            IdEntidadInterna = idEntidadInterna;
            NombreEntidadInterna =  producto.NombreEntidadInterna(idEntidadInterna);
            TieneStock = Stock > 0;
        }
         
        public static List<StockGeneralViewModel> Convert(List<ConceptoDeNegocio> productos, int [] idsEntidadInterna)
        {
            var stockViewModels = new List<StockGeneralViewModel>();

            for(int idEntidadInterna=0; idEntidadInterna< idsEntidadInterna.Length;idEntidadInterna++)
            {
                foreach (var producto in productos)
                {
                    
                    stockViewModels.Add(new StockGeneralViewModel(producto, idsEntidadInterna[idEntidadInterna]));                     
                }
            }
            

            return stockViewModels;
        }

    }
}
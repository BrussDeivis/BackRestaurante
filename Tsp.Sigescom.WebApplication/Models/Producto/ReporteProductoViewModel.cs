using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
namespace Tsp.Sigescom.WebApplication.Models//Tsp.Sigescom.WebApplication.Models.concepto
  {

    [Serializable]
  
    public class ReporteProductoViewModel
    {
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedidad{ get; set; }
        public string NombreBasico { get; set; }
       
        public ReporteProductoViewModel()
        {
            
        }
        public ReporteProductoViewModel(string codigoConcepto, string nombre, string unidadMedidad, int stock , string nombreBasico,string nombreEntidadInterna,int idEntidadInterna)
        {
            CodigoBarra = codigoConcepto;
            Nombre = nombre;
            NombreBasico = nombreBasico;
            UnidadMedidad = unidadMedidad;
        }

        public ReporteProductoViewModel(ConceptoDeNegocio producto)
        {
            CodigoBarra= producto.CodigoBarra;
            Nombre = producto.Nombre;
            UnidadMedidad = producto.UnidadMedidaComercial().Codigo;
            NombreBasico = producto.ConceptoBasico().Nombre;
        }
         
        public static List<ReporteProductoViewModel> Convert(List<ConceptoDeNegocio> productos)
        {
            var reporteProductoViewModel = new List<ReporteProductoViewModel>();      
                foreach (var producto in productos)
                {
                reporteProductoViewModel.Add(new ReporteProductoViewModel(producto));                     
                }            
                 return reporteProductoViewModel;
        }

    }
}
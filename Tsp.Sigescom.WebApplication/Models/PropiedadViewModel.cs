using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class PropiedadViewModel
    {
        public string nombre { get; set; }
        public string formato { get; set; }
        public string descripcion { get; set; }      
  

        public PropiedadViewModel()
        {
            
        }

        //public PropiedadViewModel(Caracteristica caracteristica)
        //{

        //}
    }
}
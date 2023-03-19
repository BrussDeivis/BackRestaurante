using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ModeloBasicoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ModeloBasicoViewModel()
        {

        }

        public ModeloBasicoViewModel(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
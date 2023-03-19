using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class TarifaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public TarifaViewModel() { }
        
        public TarifaViewModel(int id, string nombre, decimal precio)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Precio = precio;
        }
    }
}
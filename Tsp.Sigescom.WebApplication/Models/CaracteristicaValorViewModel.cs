using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class CaracteristicaValorViewModel
    {
        public string Nombre { get; set; }
        public List<ComboGenericoViewModel> Valores { get; set; }

        public CaracteristicaValorViewModel()
        {

        }

        public CaracteristicaValorViewModel(string nombre,List<Valor_caracteristica> valores)
        {
            this.Nombre = nombre;
            this.Valores = new List<ComboGenericoViewModel>();
            foreach (var item in valores)
            {
                Valores.Add(new ComboGenericoViewModel(item.id, item.valor));
            }
        }
    }
}
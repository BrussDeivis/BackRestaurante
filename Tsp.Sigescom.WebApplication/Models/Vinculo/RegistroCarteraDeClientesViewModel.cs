using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Turno
{
    public class RegistroCarteraDeClientesViewModel
    {
        public int  Id { get; set; }
        public ComboGenericoViewModel EstablecimientoComercial { get; set; }
        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public List<ComboGenericoViewModel> Detalles { get; set; }



        public RegistroCarteraDeClientesViewModel()
        {

        }

        public RegistroCarteraDeClientesViewModel(CarteraDeClientes cartera)
        {
            this.Id = cartera.centroDeAtencion.Id;
            this.EstablecimientoComercial = new ComboGenericoViewModel(cartera.establecimientoComercial.Id, cartera.establecimientoComercial.NombreInterno);
            this.CentroDeAtencion = new ComboGenericoViewModel(cartera.centroDeAtencion.Id, cartera.centroDeAtencion.Nombre);
            this.Detalles = new List<ComboGenericoViewModel>();
            foreach (var item in cartera.clientes)
            {
                this.Detalles.Add(new ComboGenericoViewModel(item.Id, item.RazonSocial));
            }
        }

    }

}
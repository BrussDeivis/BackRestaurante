using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaCarteraDeClientesViewModel
    {

        [DataMember]
        public int IdCartera { get; set; }
        public string EstablecimientoComercial { get; set; }
        public string CentroDeAtencion { get; set; }
        public string CantidadClientes { get; set; }

        public BandejaCarteraDeClientesViewModel()
        {
        }

        public BandejaCarteraDeClientesViewModel(CarteraDeClientes cartera)
        {
            this.IdCartera = cartera.centroDeAtencion.Id;
            this.EstablecimientoComercial = cartera.establecimientoComercial.NombreInterno;
            this.CentroDeAtencion = cartera.centroDeAtencion.Nombre;
            this.CantidadClientes = System.Convert.ToString(cartera.clientes.Count()) + " CLIENTE(S)";
        }

        public static List<BandejaCarteraDeClientesViewModel> Convert(List<CarteraDeClientes> carteras)
        {
            List<BandejaCarteraDeClientesViewModel> carterasViewModel = new List<BandejaCarteraDeClientesViewModel>();

            foreach (var item in carteras)
            {
                carterasViewModel.Add(new BandejaCarteraDeClientesViewModel(item));
            }
            return carterasViewModel;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class DetalleCarteraDeClientesViewModel
    {
        [DataMember]
        public int IdCartera { get; set; }
        public string EstablecimientoComercial { get; set; }
        public string CentroDeAtencion { get; set; }
        public List<string> Clientes { get; set; }

        public DetalleCarteraDeClientesViewModel()
        {
        }

        public DetalleCarteraDeClientesViewModel(CarteraDeClientes cartera)
        {
            this.IdCartera = cartera.centroDeAtencion.Id;
            this.EstablecimientoComercial = cartera.establecimientoComercial.NombreInterno;
            this.CentroDeAtencion = cartera.centroDeAtencion.Nombre;
            this.Clientes = cartera.clientes.Select(c => c.RazonSocial).ToList();
        }

    }
}
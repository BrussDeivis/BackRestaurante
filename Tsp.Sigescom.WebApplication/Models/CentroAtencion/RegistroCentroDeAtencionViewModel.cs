using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{

    public class RegistroCentroDeAtencionViewModel
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool SalidaBienesSinStock { get; set; }

        public List<ComboGenericoViewModel> Roles { get; set; }


        public RegistroCentroDeAtencionViewModel()
        {

        }

        public RegistroCentroDeAtencionViewModel(CentroDeAtencionExtendido centroDeAtencion)
        {
            this.Id = centroDeAtencion.Id;
            this.IdActor = centroDeAtencion.IdActor;
            this.Codigo = centroDeAtencion.Codigo;
            this.Nombre = centroDeAtencion.Nombre;
            this.SalidaBienesSinStock = centroDeAtencion.SalidaBienesSinStock;
            this.Roles = new List<ComboGenericoViewModel>();
            foreach (var item in centroDeAtencion.RolesHijosVigentes)
            {
                this.Roles.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
            }
        }

    }

    public class BandejaCentroDeAtencionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Roles { get; set; }
        public bool EsCentroAtencionParaObtencioDePrecios { get; set; }
        public bool EsCentroAtencionParaObtencioDeStock { get; set; }
          
        public BandejaCentroDeAtencionViewModel()
        {

        }

        public BandejaCentroDeAtencionViewModel(CentroDeAtencionExtendido centroDeAtencion)
        {
            this.Id = centroDeAtencion.Id;
            this.Codigo = centroDeAtencion.Codigo;
            this.Nombre = centroDeAtencion.Nombre;
            this.Roles = CadenaRoles(centroDeAtencion.RolesHijosVigentes.ToList());
            this.EsCentroAtencionParaObtencioDePrecios = centroDeAtencion.EsCentroAtencionParaObtencioDePrecios;
            this.EsCentroAtencionParaObtencioDeStock = centroDeAtencion.EsCentroAtencionParaObtencioDeStock;
        }

        public static List<BandejaCentroDeAtencionViewModel> Convert(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<BandejaCentroDeAtencionViewModel> centrosDeAtencionViewModel = new List<BandejaCentroDeAtencionViewModel>();

            foreach (var item in centrosDeAtencion)
            {
                centrosDeAtencionViewModel.Add(new BandejaCentroDeAtencionViewModel(item));
            }
            return centrosDeAtencionViewModel;
        }

        public string CadenaRoles(List<ItemGenerico> roles)
        {
            string rolesViewModel = "";

            foreach (var item in roles)
            {
                rolesViewModel += item.Nombre + ", ";
            }
            rolesViewModel = rolesViewModel == "" ? rolesViewModel : rolesViewModel.Substring(0, rolesViewModel.Length - 2);
            return rolesViewModel;
        }

    }

}
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
    public class ComboActorComercialViewModel
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }

        public ComboActorComercialViewModel()
        {

        }
        public ComboActorComercialViewModel(int id, string razonSocial, string numeroDocuemntoIdentidad)
        {
            this.Id = id;
            this.RazonSocial = razonSocial;
            this.NumeroDocumentoIdentidad = numeroDocuemntoIdentidad;
        }

        public static List<ComboActorComercialViewModel> Convert(List<Cliente> clientes)
        {
            List<ComboActorComercialViewModel> comboGenericoActores = new List<ComboActorComercialViewModel>();
            foreach (var cleinte in clientes)
            {
                comboGenericoActores.Add(new ComboActorComercialViewModel(cleinte.Id, cleinte.RazonSocial, cleinte.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }

        public static List<ComboActorComercialViewModel> Convert(List<Proveedor> proveedores)
        {
            List<ComboActorComercialViewModel> comboGenericoActores = new List<ComboActorComercialViewModel>();
            foreach (var prooveedor in proveedores)
            {
                comboGenericoActores.Add(new ComboActorComercialViewModel(prooveedor.Id, prooveedor.RazonSocial, prooveedor.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }

        public static List<ComboActorComercialViewModel> Convert(List<Empleado> empleados)
        {
            List<ComboActorComercialViewModel> comboGenericoActores = new List<ComboActorComercialViewModel>();
            foreach (var empleado in empleados)
            {
                comboGenericoActores.Add(new ComboActorComercialViewModel(empleado.Id, empleado.NombreCompleto, empleado.DocumentoIdentidad));
            }
            return comboGenericoActores;
        }
    }
    
    public class ComboCentroAtencionViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public ComboCentroAtencionViewModel()
        {

        }
        public ComboCentroAtencionViewModel(int id, string codigo, string nombre)
        {
            this.Id = id;
            this.Codigo = codigo;
            this.Nombre = nombre;
        }

        public static List<ComboCentroAtencionViewModel> Convert(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ComboCentroAtencionViewModel> comboGenericoActores = new List<ComboCentroAtencionViewModel>();
            foreach (var item in centrosDeAtencion)
            {
                comboGenericoActores.Add(new ComboCentroAtencionViewModel(item.Id, item.Codigo, item.Codigo + " | " + item.EstablecimientoComercial.NombreInterno + " | " + item.Nombre ));
            }
            return comboGenericoActores;
        }

        public static List<ComboCentroAtencionViewModel> Convert(List<Empleado> empleados)
        {
            List<ComboCentroAtencionViewModel> comboGenericoActores = new List<ComboCentroAtencionViewModel>();
            foreach (var item in empleados)
            {
                comboGenericoActores.Add(new ComboCentroAtencionViewModel(item.Id, item.Codigo, item.Codigo + " | " + item.NombreCompleto));
            }
            return comboGenericoActores;
        }
    }
}
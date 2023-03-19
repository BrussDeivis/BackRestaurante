using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaEmpleadoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombresYApellidos { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Roles { get; set; }
        public string Direccion { get; set; }
        public bool ExisteUsuario { get; set; }
        public string NombreUsuario { get; set; }


        public BandejaEmpleadoViewModel(Empleado empleado, ApplicationUser usuario)
        {
            Inicializar(empleado);
            this.NombreUsuario =(usuario!=null && this.ExisteUsuario) ? usuario.UserName : "Usuario no asignado";
        }



        public BandejaEmpleadoViewModel(Empleado empleado)
        {
            Inicializar(empleado);
        }
        private void Inicializar(Empleado empleado)
        {
            this.Id = empleado.Id;
            this.Codigo = empleado.Codigo;
            this.NombresYApellidos = empleado.NombreCompleto;
            this.NumeroDocumentoIdentidad = empleado.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = empleado.TipoDocumento;
            this.TipoPersona = empleado.TipoPersona;
            if (empleado.DomicilioFiscal() != null)
            {
                this.Direccion = (empleado.DomicilioFiscal().detalle + " , " + empleado.DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper();
            }
            if (empleado.ObtenerRolesHijosVigentes() != null)
            {
                var roles = "";
                foreach (var item in empleado.ObtenerRolesHijosVigentes())
                {
                    roles += item.nombre + ", ";
                }
                this.Roles = roles == "" ? roles : roles.Substring(0, roles.Length - 2);
            }
            this.ExisteUsuario = empleado.HayUsuario();
            this.NombreUsuario = "";
        }
        public static List<BandejaEmpleadoViewModel> Convert(List<Empleado> empleados, List<ApplicationUser> usuarios)
       {
            var empleados_ = new List<BandejaEmpleadoViewModel>();

            foreach (var empleado in empleados)
            {
                empleados_.Add(new BandejaEmpleadoViewModel(empleado, usuarios.SingleOrDefault(u=>u.Id== empleado.IdUsuario)));
            }
            return empleados_;
        }
    }
    

}
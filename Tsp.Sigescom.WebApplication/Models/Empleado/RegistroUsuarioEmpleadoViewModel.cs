using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;


namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroUsuarioEmpleadoViewModel
    {
        public List<UsuarioEmpleadoViewModel> UsuariosEmpleadoInicial { get; set; }
        public List<UsuarioEmpleadoViewModel> UsuariosEmpleado { get; set; }

        public RegistroUsuarioEmpleadoViewModel(List<UsuarioEmpleadoViewModel> usuariosEmpleadoInicial, List<UsuarioEmpleadoViewModel> usuariosEmpleado)
        {
            this.UsuariosEmpleadoInicial = usuariosEmpleadoInicial;
            this.UsuariosEmpleado = usuariosEmpleado;

        }

        public RegistroUsuarioEmpleadoViewModel()
        {
            this.UsuariosEmpleadoInicial = new List<UsuarioEmpleadoViewModel>();
            this.UsuariosEmpleado = new List<UsuarioEmpleadoViewModel>();

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;


namespace Tsp.Sigescom.WebApplication.Models
{
    public class UsuarioEmpleadoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdUsuario { get; set; }

        public UsuarioEmpleadoViewModel()
        {

        }

        public UsuarioEmpleadoViewModel(int id, string nombre, string idUsuario)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.IdUsuario = idUsuario;

        }

        public static List<UsuarioEmpleadoViewModel> Convert(List<Empleado> empleados)
        {
            List<UsuarioEmpleadoViewModel> usuariosEmpleado = new List<UsuarioEmpleadoViewModel>();

            foreach (var item in empleados)
            {
                usuariosEmpleado.Add(new UsuarioEmpleadoViewModel(item.Id,item.NombreCompleto,item.IdUsuario));
            }
            return usuariosEmpleado;
        }

    }
}
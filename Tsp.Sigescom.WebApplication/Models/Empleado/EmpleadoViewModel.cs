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
    public class EmpleadoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public ComboGenericoViewModel ClaseActor { get; set; }
        public int IdTipoActor { get; set; }
        public ComboGenericoViewModel EstadoLegalActor { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public ComboDocumentoIdentidadViewModel TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public List<ComboGenericoViewModel> Roles { get; set; }
        public List<DireccionViewModel> Direcciones { get; set; }
        public bool ExisteUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public bool GuardarUsuario { get; set; }

        public EmpleadoViewModel()
        {
            this.Roles = new List<ComboGenericoViewModel>();
            this.Direcciones = new List<DireccionViewModel>();
            //Telefono es campo requerido en la BD
            this.Telefono = "999999999";
        }
        public EmpleadoViewModel(Actor actor)
        {
            this.IdActor = actor.id;
            this.ApellidoPaterno = actor.segundo_nombre;
            this.ApellidoMaterno = actor.tercer_nombre;
            this.Nombres = actor.primer_nombre;
            this.Correo = actor.correo;
            this.TipoDocumentoIdentidad = new ComboDocumentoIdentidadViewModel(actor.id_documento_identidad, actor.Detalle_maestro.codigo);
            this.NumeroDocumentoIdentidad = actor.numero_documento_identidad;
            if (actor.Direccion.Any())
            {
                this.Direcciones = DireccionViewModel.Convert(actor.Direccion.ToList());
            }
            this.NombreUsuario = "";
        }

        public EmpleadoViewModel(Empleado empleado)
        {
            this.Id = empleado.Id;
            this.IdActor = empleado.ActorNegocio.Actor.id;
            this.Codigo = empleado.Codigo;
            this.ApellidoPaterno = empleado.ApellidoPaterno;
            this.ApellidoMaterno = empleado.ApellidoMaterno;
            this.Nombres = empleado.Nombres;
            this.Correo = empleado.Correo();
            this.FechaNacimiento = empleado.FechaNacimiento();
            this.Telefono = empleado.Telefono();
            this.ClaseActor = new ComboGenericoViewModel(empleado.IdClaseActor,empleado.Sexo().nombre);
            this.EstadoLegalActor =new ComboGenericoViewModel(empleado.IdEstadoLegal,empleado.EstadoLegal().nombre);
            this.TipoDocumentoIdentidad = new ComboDocumentoIdentidadViewModel(empleado.IdTipoDocumentoIdentidad,empleado.TipoDocumento);
            this.NumeroDocumentoIdentidad = empleado.DocumentoIdentidad;

            if (empleado.Direcciones() != null)
            {
                this.Direcciones = DireccionViewModel.Convert(empleado.Direcciones());
            }

            //this.Direcciones = DireccionViewModel.Convert(empleado.Direcciones());


            this.Roles = new List<ComboGenericoViewModel>();
            foreach(var item in empleado.ObtenerRolesHijosVigentes())
            {
                this.Roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
            }
            this.ExisteUsuario = empleado.HayUsuario();
            this.NombreUsuario = "";

        }

        public EmpleadoViewModel(Empleado empleado, AspNetUsers usuario):this(empleado)
        {
            this.NombreUsuario = this.ExisteUsuario ? usuario.UserName: "Usuario no asignado";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Turno
{
    public class RegistroRolViewModel
    {
        public int  Id { get; set; }
        public string Nombre { get;set; }
        public string Descripcion { get; set; }
        public ComboGenericoViewModel RolPadre { get; set; }
        public int Aplica { get; set; }
 


        public RegistroRolViewModel()
        {
            this.RolPadre = new ComboGenericoViewModel();
        }

        public RegistroRolViewModel(TurnoDeEmpleado turno)
        {
            this.Id = turno.Id;
          //  this.CentroDeAtencion = new ComboGenericoViewModel(turno.CentroDeAtencion().Id, turno.CentroDeAtencion().RazonSocial);
            //this.Empleado = new ComboGenericoViewModel(turno.Empleado().Id, turno.Empleado().NombreCompleto);
       //     this.Desde = turno.Desde();
         //   this.Hasta = turno.Hasta();
   
        }

    }

}
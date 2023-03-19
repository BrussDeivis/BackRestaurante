using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Turno
{
    public class RegistroTurnoViewModel
    {
        public int  Id { get; set; }
        public ComboGenericoViewModel EstablecimientoComercial { get; set; }
        public ComboGenericoViewModel CentroDeAtencion { get; set; }
        public ComboGenericoViewModel Empleado { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }


        public RegistroTurnoViewModel()
        {

        }

        public RegistroTurnoViewModel(TurnoDeEmpleado turno)
        {
            this.Id = turno.Id;
            this.EstablecimientoComercial = new ComboGenericoViewModel(turno.EstablecimientoComercial().Id, turno.EstablecimientoComercial().NombreInterno);
            this.CentroDeAtencion = new ComboGenericoViewModel(turno.CentroDeAtencion().Id, turno.CentroDeAtencion().Nombre);
            this.Empleado = new ComboGenericoViewModel(turno.Empleado().Id, turno.Empleado().NombreCompleto);
            this.Desde = turno.Desde();
            this.Hasta = turno.Hasta();
   
        }

    }

}
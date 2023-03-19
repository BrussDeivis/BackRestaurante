using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Turno
{
    [Serializable]
    [DataContract]
    public class BandejaTurnoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string EstablecimientoComercial { get; set; }
        public string CentroDeAtencion { get; set; }
        public string Empleado { get; set; }
        public string Usuario { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }


        public BandejaTurnoViewModel()
        {

        }

        public BandejaTurnoViewModel(TurnoDeEmpleado turno)
        {
            this.Id = turno.Id;
            this.EstablecimientoComercial = turno.EstablecimientoComercial().NombreInterno;
            this.CentroDeAtencion = turno.CentroDeAtencion().Nombre;
            this.Empleado = turno.Empleado().NombreCompleto;
            this.Usuario = turno.Empleado().Correo();
            this.Desde = turno.Desde().ToString("dd/MM/yyyy");
            this.Hasta = turno.Hasta().ToString("dd/MM/yyyy");
        }

        public static List<BandejaTurnoViewModel> Convert(List<TurnoDeEmpleado> turnos)
        {
            List<BandejaTurnoViewModel> turnosViewModel = new List<BandejaTurnoViewModel>();

            foreach (var item in turnos)
            {
                turnosViewModel.Add(new BandejaTurnoViewModel(item));
            }
            return turnosViewModel;
        }

    }
}
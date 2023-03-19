using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.ApiIdentificacion.AccesoDatos;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class DniConsultViewModel
    {
        [DataMember]
        public string ApellidosNombre { get; set; }
        public string Dni { get; set; }
        public string Distrito { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }


        public DniConsultViewModel(dni dni)
        {
            this.ApellidosNombre = dni.apellidos_nombre;
            this.Dni = dni.numero;
            this.Distrito = dni.distrito;
            this.Provincia = dni.provincia;
            this.Departamento = dni.departamento;
            this.Fecha = dni.fecha.ToString("dd/MM/yyyy");
            this.Hora = dni.fecha.ToString("h:mm tt");
        }

        public static List<DniConsultViewModel> Convert(List<dni> dni)
        {
            var dni_ = new List<DniConsultViewModel>();

            foreach (var item in dni)
            {
                dni_.Add(new DniConsultViewModel(item));
            }
            return dni_;
        }
    }
    

}
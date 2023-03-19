using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class MaestroViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public MaestroViewModel()
        {

        }
        public MaestroViewModel(Maestro maestro)
        {
            this.Id = maestro.id;
            this.Codigo = maestro.codigo;
            this.Nombre = maestro.nombre;

        }
    }
}
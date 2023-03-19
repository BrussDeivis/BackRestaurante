using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models.Configuracion
{
    [Serializable]
    [DataContract]
    public class ConfiguracionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ConfiguracionViewModel()
        {

        }
        public ConfiguracionViewModel(Modelo.Entidades.Configuracion configuracion)
        {
            this.Id = configuracion.id;
            this.Nombre = configuracion.nombre;
            this.Descripcion = configuracion.descripcion;

        }
    }
}
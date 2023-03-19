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
    public class ParametroConfiguracionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdConfiguracion { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }

        public ParametroConfiguracionViewModel()
        {

        }
        public ParametroConfiguracionViewModel(Parametro_de_configuracion parametro)
        {
            this.Id = parametro.id;
            this.IdConfiguracion = parametro.id_configuracion;
            this.Nombre = parametro.nombre;
            this.Tipo = parametro.tipo;
            this.Descripcion = parametro.descripcion;
            this.Valor = parametro.valor;

        }
    }
}
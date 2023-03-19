using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ActorViewModels
    {
        public int IdActor { get; set; }
        public ComboGenericoViewModel TipoPersona{ get; set; }
        public string RazonSocial { get; set; }
        public ComboGenericoViewModel TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreComercial { get; set; }
        public string NombreCorto { get; set; }
        public int Direccion { get; set; }
             
    }
}
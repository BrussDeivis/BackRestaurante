using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class RegistroConceptoGastoViewModel
    {

        [DataMember]
        public int Id { get; set; }
        public ComboGenericoViewModel ConceptoBasico { get; set; }
        public string Sufijo { get; set; }
        public string NombreConceptoBasico { get; set; }
        public string Valor { get; set; }
        public bool ConceptoBasicoSeleccionado  { get; set; }
        
        public RegistroConceptoGastoViewModel()
        {

        }

        public RegistroConceptoGastoViewModel(ConceptoDeNegocio concepto)
        {
            //this.Id = concepto.Id;
            //this.ConceptoBasico = new ComboGenericoViewModel(concepto.ConceptoBasico().Id, concepto.ConceptoBasico().Nombre);
            //this.Nombre = concepto.ConceptoBasico().Nombre + " | " + concepto.Sufijo;
            //this.Sufijo = concepto.Sufijo ;
            //this.IdClasificacion = concepto.RolDeProducto().id;
           
        }
    }

    public class RegistroConceptoServicioViewModel
    {

        [DataMember]
        public int Id { get; set; }
        public ComboGenericoViewModel ConceptoBasico { get; set; }
        public string Sufijo { get; set; }
        public string NombreConceptoBasico { get; set; }
        public string Valor { get; set; }
        public bool ConceptoBasicoSeleccionado { get; set; }
        public string NombreCompleto { get; set; }

        public RegistroConceptoServicioViewModel()
        {

        }
    }
}
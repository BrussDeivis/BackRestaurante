using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System.Runtime.Serialization;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ProductoParaAlmacenViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string VersionFila { get; set; }
        public decimal Stock { get; set; }
        public int IdVersion { get; set; }
        public int IdActorVersion { get; set; }

        public ProductoParaAlmacenViewModel()
        {

        }

        public ProductoParaAlmacenViewModel(ConceptoDeNegocio p, int idCentroAtencion)
        {
            this.Id = p.Id;
            this.Nombre = p.Nombre;
            this.Stock = p.Stock(idCentroAtencion);
            this.VersionFila = p.VersionFila() != null ? Convert.ToBase64String(p.VersionFila()) : null;
            this.IdActorVersion = p.IdActorExistencia();
            this.IdVersion = p.IdExistencia();
        }

    }
}
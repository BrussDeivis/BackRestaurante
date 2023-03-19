using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaConceptoGastoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sufijo { get; set; }
        public string Clasificacion { get; set; }
   

        public BandejaConceptoGastoViewModel(ConceptoDeNegocio producto)
        {
            this.Id = producto.Id;
            this.Nombre = producto.Nombre;
            this.Sufijo = producto.Sufijo;
            this.Clasificacion = producto.RolDeProducto().nombre;
           
        }

        public static List<BandejaProductoViewModel> Convert_(List<ConceptoDeNegocio> productos, int idCentroAtencion)
        {
            var productos_ = new List<BandejaProductoViewModel>();

            foreach (var producto in productos)
            {
                productos_.Add(new BandejaProductoViewModel(producto, idCentroAtencion));
            }
            return productos_;
        }
    }


}
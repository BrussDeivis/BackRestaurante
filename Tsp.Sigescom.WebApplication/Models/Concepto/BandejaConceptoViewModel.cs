using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaConceptoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public string Categorias { get; set; }
        public bool EsVigente { get; set; }
        public BandejaConceptoViewModel(Detalle_maestro detalle)
        {
            this.Id = detalle.id;
            this.Nombre = detalle.nombre;
            this.Valor = detalle.valor;
            //this.Descripcion = detalle.valor;
            this.Categorias = detalle.Categoria_concepto.Count <= 0 ? "" : detalle.Categoria_concepto.Select(cc => cc.Detalle_maestro1.nombre).Aggregate((cc, ccNext) => cc + " | " + ccNext);
            this.EsVigente = detalle.es_vigente;
        }

        public static List<BandejaConceptoViewModel> Convert(List<Detalle_maestro> detalle)
        {
            List<BandejaConceptoViewModel> ordenes = new List<BandejaConceptoViewModel>();
            foreach (var ordenDeVenta in detalle)
            {
                ordenes.Add(new BandejaConceptoViewModel(ordenDeVenta));
            }
            return ordenes;
        }
    }
}
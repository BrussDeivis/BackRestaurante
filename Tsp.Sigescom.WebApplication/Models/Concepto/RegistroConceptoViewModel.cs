using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroConceptoViewModel
    {
        public int IdConcepto { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public List<ModeloBasicoViewModel> Categorias { get; set; }
        public List<ModeloBasicoViewModel> Caracteristicas { get; set; }

        public RegistroConceptoViewModel()
        {

        }

        public RegistroConceptoViewModel(Detalle_maestro concepto)
        {
            IdConcepto = concepto.id;
            Nombre = concepto.nombre;
            Valor = concepto.valor;
            Categorias = new List<ModeloBasicoViewModel>();
            Caracteristicas = new List<ModeloBasicoViewModel>();
            foreach (var c in concepto.Categoria_concepto)
            {
                Categorias.Add(new ModeloBasicoViewModel() { Id = c.Detalle_maestro1.id, Nombre = c.Detalle_maestro1.nombre });
            }
            foreach (var c in concepto.Caracteristica_concepto)
            {
                Caracteristicas.Add(new ModeloBasicoViewModel() { Id = c.Detalle_maestro1.id, Nombre = c.Detalle_maestro1.nombre });
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ConceptoBasicoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public bool EsBien { get; set; }
        public bool TieneCaracteristicasPropias { get; set; }
        public List<CaracteristicaPropiaViewModel> CaracteristicasPropias { get; set; }

        public ConceptoBasicoViewModel()
        {
            CaracteristicasPropias = new List<CaracteristicaPropiaViewModel>();
        }

        public ConceptoBasicoViewModel(Detalle_maestro detalle)
        {
            Id = detalle.id;
            Nombre = detalle.nombre;
            Valor = detalle.valor;
            EsBien = detalle.valor == "1";
        }

        public ConceptoBasicoViewModel(DetalleGenerico detalle)
        {
            Id = detalle.Id;
            Nombre = detalle.Nombre;
            Valor = detalle.Valor;
            EsBien = detalle.Valor == "1";
        }

        public static List<ConceptoBasicoViewModel> Convert(List<Detalle_maestro> detalles)
        {
            List<ConceptoBasicoViewModel> conceptos = new List<ConceptoBasicoViewModel>();
            foreach (var detalle in detalles)
            {
                conceptos.Add(new ConceptoBasicoViewModel(detalle));
            }
            return conceptos;
        }


    }
}
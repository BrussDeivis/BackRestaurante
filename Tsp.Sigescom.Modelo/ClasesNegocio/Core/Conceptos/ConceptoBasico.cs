using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConceptoBasico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public bool EsBien { get; set; }
        public bool TieneCaracteristicasPropias { get; set; }
        public List<CaracteristicaPropia> CaracteristicasPropias { get; set; }

        public ConceptoBasico()
        {
            CaracteristicasPropias = new List<CaracteristicaPropia>();
        }

        public ConceptoBasico(Detalle_maestro detalle)
        {
            Id = detalle.id;
            Nombre = detalle.nombre;
            Valor = detalle.valor;
            EsBien = detalle.valor == "1";
        }

        public ConceptoBasico(DetalleGenerico detalle)
        {
            Id = detalle.Id;
            Nombre = detalle.Nombre;
            Valor = detalle.Valor;
            EsBien = detalle.Valor == "1";
        }

        public static List<ConceptoBasico> Convert(List<Detalle_maestro> detalles)
        {
            List<ConceptoBasico> conceptos = new List<ConceptoBasico>();
            foreach (var detalle in detalles)
            {
                conceptos.Add(new ConceptoBasico(detalle));
            }
            return conceptos;
        }


    }
}
using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico
{
    public class SerieComprobante_ : ItemGenerico
    {
        public bool EsAutonumerica { get; set; }

        public SerieComprobante_()
        {

        }

        public SerieComprobante_(int id, string nombre, bool esAutonumerica) : base(id, nombre)
        {
            this.EsAutonumerica = esAutonumerica;
        }
    }

}

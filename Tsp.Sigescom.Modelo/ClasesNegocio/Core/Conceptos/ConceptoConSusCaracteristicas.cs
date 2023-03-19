using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConceptoConSusCaracteristicas
    {
        public int IdConcepto { get; set; }
        public string NombreConceptoSinCaracteristicas { get; set; }
        public List<ItemGenerico> Caracteristicas { get; set; }

        public ConceptoConSusCaracteristicas()
        {
        }
    }
}
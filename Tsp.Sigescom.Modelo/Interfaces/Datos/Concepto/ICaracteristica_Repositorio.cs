using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos
{
    public interface ICaracteristica_Repositorio
    {
        ConceptoConSusCaracteristicas ObtenerConceptoNegocioConSusCaracteristicasYSusValores(int idConceptoNegocio);

    }

}

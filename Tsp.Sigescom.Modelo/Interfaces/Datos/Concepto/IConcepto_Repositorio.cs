using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos
{
    public interface IConcepto_Repositorio
    {
       
        IEnumerable<Concepto> ObtenerConceptos(int[] idsConceptos);
        IEnumerable<Concepto_negocio> ObtenerConceptosComercialesPorFamilia(int idFamilia);

    }

}

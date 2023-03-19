using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;


namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ICaracteristica_Logica
    {
        ConceptoConSusCaracteristicas ObtenerConceptoNegocioConSusCaracteristicasYSusValores(int idConceptoNegocio);
    }
}

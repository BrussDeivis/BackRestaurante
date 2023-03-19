using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Compras

{
    public interface IConsultaCompras_Repositorio
    {
        IEnumerable<PrecioComercial> ObtenerValorUnitarioDePrimeraOrdenDeCompraConPrecioMayorACero(int[] idConceptos);
        decimal ObtenerValorUnitarioDePrimeraOrdenDeCompra(int idConcepto);


    }
}

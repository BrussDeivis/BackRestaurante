using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica
{
    public partial class Caracteristica_Logica : ICaracteristica_Logica
    {
        private readonly ICaracteristica_Repositorio _caracteristicaRepositorio;


        public Caracteristica_Logica(ICaracteristica_Repositorio caracteristicaRepositorio)
        {
            _caracteristicaRepositorio = caracteristicaRepositorio;
        }

        public ConceptoConSusCaracteristicas ObtenerConceptoNegocioConSusCaracteristicasYSusValores(int idConceptoNegocio)
        {
            try
            {
                var concepto = _caracteristicaRepositorio.ObtenerConceptoNegocioConSusCaracteristicasYSusValores(idConceptoNegocio);
                concepto.Caracteristicas.ForEach(c => c.Nombre = c.Nombre.ToLower());
                concepto.Caracteristicas.ForEach(c => c.Valor = c.Valor.ToLower());
                return concepto;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener caracteristicas y sus valores de un concepto de negocio", e);
            }
        }
    }
}

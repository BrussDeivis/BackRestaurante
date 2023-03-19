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

namespace Tsp.Sigescom.Logica
{
    public partial class ConceptoLogica : IConceptoLogica
    {
        public bool ExisteCodigoDeBarraDeProducto(string codigoBarra)
        {
            try
            {
                bool existe = _conceptoRepositorio.ExisteCodigoDeBarraConceptoNegocio(codigoBarra);
                return existe;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe código de barra", e);
            }
        }

        public bool ExisteNombreConceptoComercial(string nombre)
        {
            try
            {
                bool existe = _conceptoRepositorio.ExisteNombreConceptoNegocio(nombre);
                return existe;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe nombre", e);
            }
        }

        public bool ExisteNombreDeValorCaracteristica(int idCaracteristica, string valor)
        {
            try
            {
                bool existe = _conceptoRepositorio.ExisteNombreDeValorCaracteristica(idCaracteristica, valor);
                return existe;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe nombre de valor característica", e);
            }
        }
    }
}

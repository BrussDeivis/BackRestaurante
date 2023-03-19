using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos.Tests
{
    [TestClass]
    public class ConceptoDatosTests
    {
        private readonly IConceptoRepositorio _repositorio;
        public ConceptoDatosTests()
        {
            _repositorio = Dependencia.Resolve<IConceptoRepositorio>();
        }
        [TestMethod]
        public void ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestroTest()
        {
            int[] caracteristicas = { 102 };
            var resultado = _repositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(9, caracteristicas);
            int elementos = resultado.Count();
            Assert.IsTrue(elementos > 0);
        }

        [TestMethod]
        public void ConceptosNegocioVigentesConCaracteristicaTest()
        {
            int caracteristica = 11969;
            var resultado = _repositorio.ConceptosNegocioVigentesConCaracteristica(caracteristica);
            int elementos = resultado.Count();
            Assert.IsTrue(elementos > 0);

        }

        [TestMethod]
        public void ObtenerConceptosDeNegociosComercialesPorRolesTest()
        {
            var resultado = _repositorio.ObtenerConceptosDeNegociosComercialesPorRol(9).ToList();
            int elementos = resultado.Count();
            Assert.IsTrue(elementos > 0);

        }

        //[TestMethod]
        //public void ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesTest()
        //{
        //    var resultado = _repositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(9,3,3,7).ToList();
        //    resultado.ForEach(r => r.Nombre = r.Nombre.Substring(0, r.Nombre.Length - 8));
        //    int elementos = resultado.Count();
        //    Assert.IsTrue(elementos > 0);

        //}


    }
}


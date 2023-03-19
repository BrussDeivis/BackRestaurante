using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    class ConfiguracionLogicaUnitTest
    {

        private readonly IMaestroLogica _logica;
        public ConfiguracionLogicaUnitTest()
        {
            _logica = Dependencia.Resolve<IMaestroLogica>();
        }


        [TestMethod]
        public void guardarMaestroTestMethod()
        {
            string codigo = "prueba", nombre = "prueba";
            var lista = _logica.guardarMaestro(codigo, nombre);

            Assert.AreEqual(lista.code_result, OperationResultEnum.Success);
        }

    }
}

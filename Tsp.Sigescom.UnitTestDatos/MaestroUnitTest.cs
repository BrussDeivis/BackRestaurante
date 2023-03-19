using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;
using Tsp.Sigescom.AccesoDatos;

namespace Tsp.Sigescom.UnitTestDatos
{
    [TestClass]
    public class MaestroUnitTest
    {
        [TestMethod]
        public void ObtenerMaestroTestMethod()
        {
            IMaestroRepositorio repositorio = new MaestroDatos();
            Maestro maestro = repositorio.obtenerMaestro(1);

            Assert.IsNotNull(maestro);

        }

        [TestMethod]
        public void obtenerMaestrosTestMethod()
        {

            IMaestroRepositorio repositorio = new MaestroDatos();
            IEnumerable<Maestro> _maestro = repositorio.obtenerMaestros();

            Assert.IsNotNull(_maestro);
        }

        [TestMethod]
        public void obtenerDetallesTestMethod()
        {
            IMaestroRepositorio repositorio = new MaestroDatos();
            IEnumerable<Detalle_maestro> _detalleMaestro = repositorio.obtenerDetalles(42, null);

            Assert.IsNotNull(_detalleMaestro);
        }

        [TestMethod]
        public void obtenerCategorias() {
        }
    }
}

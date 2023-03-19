using System;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class GastoCostoIndirectoUnitTest
    {
        private readonly IGastoLogica logica;

        public GastoCostoIndirectoUnitTest()
        {
            logica = Dependencia.Resolve<IGastoLogica>();
        }

        /// <summary>
        /// Metodo listar los gastos y costos indirectos
        /// </summary>
        [TestMethod]
        public void ListarGastoCostoIndirecto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio,fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void BuscarGastoCostoIndirecto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void UltimoGastoCostoIndirecto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
    }
}




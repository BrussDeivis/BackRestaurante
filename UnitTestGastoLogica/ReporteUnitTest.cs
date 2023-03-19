using System;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.UnitTestGastoLogica
{
    [TestClass]
    public class ReporteUnitTest
    {
        private readonly IGastoLogica logica;
        public ReporteUnitTest()
        {   logica = Dependencia.Resolve<IGastoLogica>();
        }

             
        [TestMethod]
        public void ListarIngresoVentas()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ListarCostoVentas()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ListarGastosIndirectos()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ListarDepreciacion()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        
       
    }
}




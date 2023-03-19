using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class AlmacenLogicaUnitTest
    {
        private readonly IOperacionLogica logica;


        public AlmacenLogicaUnitTest()
        {
            logica = Dependencia.Resolve<IOperacionLogica>();
        }



        #region MANEJO DE FECHA
        [TestMethod()]
        public void RestarFechas()
        {

            var fecha = new DateTime(2019, 06, 18, 00, 00, 00);

            var fechaSumada = fecha.AddMilliseconds(1);
            var fechaRestada = fecha.AddMilliseconds((-1));
            var fechaResultadoRestada = fechaRestada;
            var fechaResultadoSumada = fechaSumada;


            Assert.Fail();
        }


        #endregion

        [TestMethod()]
        public void ObtenerReportePorConceptoBasico()
        {
            var fechaDesde = new DateTime(2019,10, 8, 00, 00, 00);
            var fechaHasta = new DateTime(2019, 10,10, 00, 00, 00);

            var resultado = logica.ObtenerReporteDeSalidasDeAlcohol(fechaDesde,fechaHasta , new int[] { 11});
            Assert.Fail();
        }
    }
}
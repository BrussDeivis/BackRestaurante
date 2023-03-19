using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class FacturacionLogicaUnitTests
    {
        private readonly IOperacionLogica facturacion;

        public FacturacionLogicaUnitTests()
        {
            facturacion = Dependencia.Resolve<IOperacionLogica>();
        }

        [TestMethod]
        public void GuardarVentaTestMethod()
        {
            //var tt = facturacion.obtenerTiposDeComprobanteParaVenta(2);
            //var cantidadSeriesFacturasEsperadas = 1;
            //var cantidadSeriesFacturasActual = tt.SingleOrDefault(t => t.Id == 26).Series().Count;

            //Assert.AreEqual(cantidadSeriesFacturasEsperadas, cantidadSeriesFacturasActual);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class MaestroLogicaUnitTest
    {

        private readonly IMaestroLogica _logica;
        public MaestroLogicaUnitTest()
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

        //[TestMethod]
        //public void ObtenerTipoDeCambio()
        //{
        //    DateTime fecha = DateTime.Now;
        //    try
        //    {
        //        decimal tipoCambio = _logica.obtenerTipoDeCambio(fecha).valorVenta;
        //        Assert.IsNotNull(tipoCambio);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("no", e);
        //    }
        //}

        //[TestMethod]
        //public void ObtenerproductoTestMethod()
        //{
        //    List<DetalleGenerico> _producto = _logica.obtenerProductos("manda");

        //    Assert.IsNotNull(_producto);
        //}

        //[TestMethod]
        //public void ObtenerVariedadProductoTestMethod()
        //{
        //    List<Detalle_detalle_maestro> _variedadProducto = _logica.obtenerVariedadProductos(102, null);

        //    Assert.IsNotNull(_variedadProducto);
        //}
        [TestMethod]
        public void ObtenerMenus()
        {
            List<Menu_aplicacion> menu = _logica.obtenerMenus();

            Assert.IsNotNull(menu);
        }
    }
}

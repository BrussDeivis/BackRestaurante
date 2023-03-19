using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public class PrecioLogicaUnitTest
    {
        private readonly IPrecioLogica logica;
        int idCentroAtencion=4;

        public PrecioLogicaUnitTest()
        {
            logica = Dependencia.Resolve<IPrecioLogica>();
        }
       

        #region PRECIOS

        /// <summary>
        /// En este método estoy probando el registro de los precios, donde se ingresará
        /// un precio negatio, cosa que no debe pasar
        /// </summary>
         [TestMethod]
        public void CrearPrecioNegativoTestMethod1()
        {
            // arrange
            int idProducto = 8;
            int idEmpleado = 4;
            int idTarifa = 1;
            Decimal importe = -15;
            DateTime fechaDesde = new DateTime(2018, 5, 1);
            DateTime fechaHasta = new DateTime(2018, 5, 28); 

            // act  
            OperationResult result = logica.establecerPrecio(idCentroAtencion, idProducto, idTarifa, importe, fechaDesde, fechaHasta, "", idEmpleado);

            // assert 
            Assert.AreEqual(OperationResultEnum.Error, result.code_result);
        }

        /// <summary>
        /// En este método estoy probando las fechas, donde la "Fechadesde" debe ser
        /// una fecha menor a la "FechaHasta", pero en este método probaremos lo contrario
        /// haciendo que la "FechaDesde" sea mayor que la "FechaHasta", cosa que no debe pasar
        /// </summary>
        [TestMethod]        
        public void CrearPrecioFechaTest()
        {
            // arrange            
            int idProducto = 8;
            int idEmpleado = 4;
            int idTarifa = 1;
            Decimal importe = 25;
            DateTime fechaDesde = new DateTime(2018, 5, 1);
            DateTime fechaHasta = new DateTime(2018, 4, 28);

            // act  
            OperationResult result = logica.establecerPrecio(idCentroAtencion, idProducto, idTarifa, importe, fechaDesde, fechaHasta, "", idEmpleado);

            // assert 
            Assert.AreEqual(OperationResultEnum.Error, result.code_result);
        }

        #endregion
    }
}

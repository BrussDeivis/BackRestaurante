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
    public class GastoPersonalUnitTest
    {
        private readonly IGastoLogica logica;
        public GastoPersonalUnitTest()
        {
            logica = Dependencia.Resolve<IGastoLogica>();
        }

        [TestMethod]
        public void GuardarGastoPersonalTestMethod()
        {
            int idEmpleado = 42042, idProveedor = 42024, idTipoComprobante = 2;
            string Observacion = "ninguno", numeroComprobante = "0001000023";
            decimal importeTotal = 500;
            DateTime fechaCompra = new DateTime(2016, 03, 30);
            List<Detalle_transaccion> detalles = obtenerDetalles();
            OperationResult result = logica.guardarRegistroCompraEME(idEmpleado, idTipoComprobante, numeroComprobante, importeTotal, idProveedor, fechaCompra, Observacion, detalles);
            Assert.AreEqual(OperationResultEnum.Success, result.code_result);
        }
       
        private List<Detalle_transaccion> obtenerDetalles()
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            int idProducto = 82, idprecio = 85;
            decimal cantidad = 5, precio = 100, total = 500;
            string detalle = "ninguno";
            Detalle_transaccion midetalle = new Detalle_transaccion(cantidad, idProducto, detalle, precio, total, idprecio, 0, 0, 1, 0, 0, 0);
            detalles.Add(midetalle);
            return detalles;
        }
        [TestMethod]
        public void ActualizarGastoPersonalTestMethod()
        {
            int idEmpleado = 42042, idProveedor = 42024, idTipoComprobante = 2;
            string Observacion = "ninguno", numeroComprobante = "0001000023";
            decimal importeTotal = 500;
            DateTime fechaCompra = new DateTime(2016, 03, 30);
            List<Detalle_transaccion> detalles = obtenerDetalles();
            OperationResult result = logica.guardarRegistroCompraEME(idEmpleado, idTipoComprobante, numeroComprobante, importeTotal, idProveedor, fechaCompra, Observacion, detalles);
            Assert.AreEqual(OperationResultEnum.Success, result.code_result);
        }
        [TestMethod]
        public void ListarGastoPersonalTestMethod()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void BuscarGastoPersonalTestMethod()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ObtenerSiguienteGastoPersonalTestMethod()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ObtenerOrdenGastoPersonalTestMethod()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void PagoCuotaGastoPersonalTestMethod()
        {
            int idEmpleado = 42042, idProveedor = 42024, idTipoComprobante = 2;
            string Observacion = "ninguno", numeroComprobante = "0001000023";
            decimal importeTotal = 500;
            DateTime fechaCompra = new DateTime(2016, 03, 30);
            List<Detalle_transaccion> detalles = obtenerDetalles();
            OperationResult result = logica.guardarRegistroCompraEME(idEmpleado, idTipoComprobante, numeroComprobante, importeTotal, idProveedor, fechaCompra, Observacion, detalles);
            Assert.AreEqual(OperationResultEnum.Success, result.code_result);
        }
        [TestMethod]
        public void ObtenerPagosGastoPersonalTestMethod()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
    }
}




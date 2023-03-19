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
    public class ServicioImpuestoUnitTest
    {
        private readonly IGastoLogica logica;
        public ServicioImpuestoUnitTest()
        {
            logica = Dependencia.Resolve<IGastoLogica>();
        }

        /// <summary>
        /// Metodo para guardar una compra de equipos muebles y enseres
        /// </summary>
        [TestMethod]
        public void GuardarServicioImpuestoTestMethod()
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
        public void GuardarServicioImpuestoFinanciadoTestMethod()
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
        public void ActualizarServicioImpuestoTestMethod()
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
        public void ListarServicioImpuesto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void BuscarServicioImpuesto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ObtenerSiguienteServicioImpuesto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void ObtenerOrdenServicioImpuesto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void PagoCuotaServicioImpuestoTestMethod()
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
        public void ObtenerPagosServicioImpuesto()
        {
            DateTime fechaInicio = new DateTime(2016, 03, 30);
            DateTime fechaFin = new DateTime(2017, 03, 30);
            List<Gasto> result = logica.verGastoCosto(fechaInicio, fechaFin);
            Assert.AreEqual(0, result.Count);
        }
    }
}



using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class CotizacionLogicaTests
    {
        /*
        private readonly ICotizacionLogica logica;

        public CotizacionLogicaTests()
        {
            logica = Dependencia.Resolve<ICotizacionLogica>();
        }

        [TestMethod()]
        public void ConfirmarCotizacionTest()
        {
            int idEmpleado = 13;
            int idCentroDeAtencion = 1;
            int idCliente = 30;
            string alias = "Juan Perez";
            //int idTipoComprobante = 407;//comprobante nota interna
            int idTipoComprobante = 1650;//comprobante cotizacion
            int idSerieComprobante = 17;//Serie de cotizacion 
            bool gravaIgv = false;
            string observacion = "NINGUNA";
            decimal flete = 15;
            DateTime fechaVencimiento = new DateTime(2019, 04, 03, 23, 59, 59);

            List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();

            //detalles.Add(new DetalleDeOperacion(new Detalle_transaccion(
            //    5, //item.Cantidad,
            //    6, //item.Producto.Id,
            //    "",
            //    20, //item.PrecioUnitario,
            //    100, //item.Importe,
            //    null, 0, null, null, 0, 0,
            //    0, //item.Descuento,
            //    null, null, null), false));

            OperationResult resultado = new OperationResult();
            resultado = null;// logica.ConfirmarCotizacion(idEmpleado, idCentroDeAtencion, idCliente, alias, idTipoComprobante, idSerieComprobante, 0, gravaIgv, fechaVencimiento, observacion, detalles, flete);

            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);

        }

        [TestMethod()]
        public void ObtenerOrdenesDeCotizacionTest()
        {
            DateTime fechaInicio = new DateTime(2019, 04, 03, 00, 00, 00);
            DateTime fechaFin = new DateTime(2019, 04, 03, 23, 59, 59);
            var ordenes = logica.ObtenerOrdenesDeCotizacion(1, 1, fechaInicio, fechaFin);
            Assert.AreEqual(ordenes.Count() > 0, true);
        }

        [TestMethod()]
        public void ObtenerOrdenDeCotizacionTest()
        {
            var orden = logica.ObtenerOrdenDeCotizacion(1);
            Assert.AreEqual(orden != null, true);
        }*/
    }
}
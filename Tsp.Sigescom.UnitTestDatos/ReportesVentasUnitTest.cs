using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;
using System.Linq;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Reportes.UnitTestDatos
{
    [TestClass]
    public class ReportesVentasUnitTest
    {
        private readonly ITransaccionRepositorio _repositorio;

        public ReportesVentasUnitTest()
        {
            _repositorio = Dependencia.Resolve<ITransaccionRepositorio>();
        }

        /// <summary>
        /// Metodo para guardar Ventas
        /// </summary>
        [TestMethod]
        public void VentasPorCaracteristicaTestMethod()
        {

            int idTipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
            int idEstadoConfirmado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            int idCaracteristicaMarca = 11969;
            int[] idsActorNegocio = { 3};
            DateTime fechaDesde = DateTimeUtil.FechaActual().AddDays(-120);
            DateTime fechaHasta = DateTimeUtil.FechaActual().AddDays(0);

            var resultado  = _repositorio.ObtenerItemOperacionPorCaracteristica(idTipoTransaccion,idEstadoConfirmado,fechaDesde, fechaHasta, idsActorNegocio,idCaracteristicaMarca).ToList();
            Assert.IsTrue(resultado.Count>0);
        }

        [TestMethod]
        public void ObtenerItemsDetalladoDeVentaConMedioPagoTestMethod()
        {

            int idTipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
            int idEstadoConfirmado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            int[] idsCaracteristicas  = { 11969,11968,11967,11966,119 };
            int idPuntoVenta =  3 ;
            DateTime fechaDesde = DateTimeUtil.FechaActual().AddDays(-5);
            DateTime fechaHasta = DateTimeUtil.FechaActual().AddDays(0);

            var resultado = _repositorio.ObtenerItemsDetalladoDeVentaConMedioPago(idTipoTransaccion, idEstadoConfirmado, fechaDesde, fechaHasta, idsCaracteristicas, idPuntoVenta).ToList();
            Assert.IsTrue(resultado.Count > 0);
        }
         
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class VentaLogicaUnitTest
    {
        private readonly IConfiguracionLogica _logica;
        private readonly IOperacionLogica operacionLogica;
        private readonly ILibrosElectronicosLogica _librosElectronicosLogica;



        public VentaLogicaUnitTest()
        {
            _logica = Dependencia.Resolve<IConfiguracionLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            _librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
        }

        //[TestMethod()]
        //public void ObtenerCuotasDeCarteraDeClientesTest()
        //{
        //    var cuotas = logica.ObtenerCuotasDeCarteraDeClientes(3,409);
        //    Assert.IsFalse(1==1);
        //}


        //[TestMethod()]
        //public void ObtenerVentasMasivasPorFechaTest()
        //{
        //    DateTime fecha = new DateTime(2018, 12, 03, 00, 00, 00);
        //    //var cuotas = logica.ObtenerVentasMasivasPorFecha(4,409,1, TipoVentaEnum.VentaMasiva.ToString(), fecha);
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ObtenerDeudasMasivaTest()
        //{
        //    DateTime fecha = new DateTime(2018, 12, 03, 00, 00, 00);
        //    var deudas = logica.ObtenerDeudasMasivas(3 , fecha);
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void actualizarSerieDeComprobanteTest()
        {

            int id = 15;
            int IdCentroDeAtencion = 1;
            int IdTipoDeComprobante = 26;
            string NumeroDeSerie = "O002";
            int NumeroDeComprobanteSiguiente = 1;
            bool Autonumerica = false;
            bool Vigente = false;
            // ejecucion del metodo
            OperationResult result = _logica.ActualizarSerieDeComprobante(id, IdCentroDeAtencion, IdTipoDeComprobante, NumeroDeSerie, NumeroDeComprobanteSiguiente, Autonumerica, Vigente);
            /// comprobacion del asert
            Assert.AreEqual(OperationResultEnum.Success, result.code_result);

        }

        [TestMethod()]
        public void ObtenerEstadoDeCuentaClienteTest()
        {
            var datos = operacionLogica.ObtenerCobranzasPorCliente(86, new DateTime(2019, 10, 30, 00, 00, 00), new DateTime(2019, 10, 30, 23, 59, 59));

            Assert.Fail();
        }

        [TestMethod()]
        public void ObtnerFecha()
        {
            var datos = operacionLogica.ObtenerFechaIncioyFinBasadoEnFechaActual();

            Assert.Fail();
        }

        #region REPORTES

        [TestMethod()]
        public void ObtenerResumenDeComprobantePorSerieInvalidadaTest()
        {

            var ventasInvalidada = operacionLogica.ObtenerResumenDeComprobantesInvalidadosDeOperacionesDeVentasPorSerie(new int[] { 11 }, new DateTime(2019, 04, 1, 00, 00, 00), new DateTime(2019, 04, 12, 18, 00, 00)); //a11
            Assert.Fail();
        }

        #endregion

        [TestMethod()]
        public void ObtenerOrdenesDeVentaTransmitidasYConfirmadasTest()
        {
            int idSerie = 2;
            DateTime fechaDesde = new DateTime(2019, 03, 01);
            DateTime fechaHasta = new DateTime(2019, 04, 30);
            List<ResumenDeTransaccionVenta> ordenes = operacionLogica.ObtenerResumenDeOperacionesDeVenta(fechaDesde, fechaHasta, idSerie);

            Assert.IsNotNull(ordenes);

        }

        [TestMethod()]
        public void ObtenerResumenesVentasTest()
        {
            Assert.Fail();
        }

        #region REPORTE DE VENTAS FORMATO ADSOFT Y AFEX

        [TestMethod()]
        public void ConsolidarRegistroDeVentasTest()
        {

            var periodo = _librosElectronicosLogica.ObtenerPeriodo(12);
            var ventas = operacionLogica.ConsolidarRegistroDeVentas(periodo, 1);
            Assert.Fail();

        }


        [TestMethod()]
        public void ObtenerVentasClienteTest()
        {

            var periodo = _librosElectronicosLogica.ObtenerPeriodo(8);
            var ventas = operacionLogica.ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito(1, periodo.FechaDesde, periodo.FechaHasta);

            Assert.Fail();

        }

        #endregion
        //[TestMethod()]
        //public void ObtenerCuotasConSaldoTest()
        //{
        //    var cuotas = operacionLogica.ObtenerCuotasConSaldo(new int[] { 82 });
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSerieTest()
        {
            DateTime fechaDesde = new DateTime(2019, 04, 01);
            DateTime fechaHasta = new DateTime(2019, 08, 09);
            var resultado = operacionLogica.ObtenerResumenDeComprobantesConfirmadosDeOperacionesDeVentasPorSerie(new int[] { 1, 324, 322 }, fechaDesde, fechaHasta);
            Assert.Fail();
        }

        #region REPORTE DEUDAS Y PAGOS
        [TestMethod()]
        public void ObtenerPagosAProveedoresTest()
        {
            DateTime fechaDesde = new DateTime(2019, 08, 01);
            DateTime fechaHasta = new DateTime(2019, 08, 09);

            //var resultado = operacionLogica.ObtenerPagosAProveedores(fechaDesde, fechaHasta);
            Assert.Fail();
        }
        #endregion

        #region FECHAS REPORTES
        [TestMethod()]
        public void ObtenerFechaIncioyFinConPrecisionDeMilisegundosParaReporteVentaPuntoDeVentaTest()
        {
            var resultado = operacionLogica.ObtenerFechaIncioyFinConPrecisionDeMilisegundosParaReporteVentaPuntoDeVenta();
            Assert.Fail();
        }

        [TestMethod()]
        public void ConvertirStringADateTimeTest()
        {
            DateTime fechaDesde = new DateTime(2019, 02, 05, 11, 45, 50, 300);
            //DateTime fechaHasta = fechaDesde.Date.AddDays(1);
            //fechaHasta.AddMilliseconds(999);

            fechaDesde = fechaDesde.AddMilliseconds(-fechaDesde.Millisecond);

            var fechas = new List<string> { fechaDesde.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            DateTime fechaInicio = DateTime.Parse(fechas[0]);
            DateTime fechaFin = DateTime.Parse(fechas[1]);


            Assert.Fail();
        }

        #endregion

        [TestMethod()]
        public void ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributablesTest()
        {
            DateTime fechaDesde = new DateTime(2019, 08, 01);
            DateTime fechaHasta = new DateTime(2019, 08, 31);
            var resultado = operacionLogica.ObtenerOrdenesConBoletaYFacturaYNotasDeCreditoYDebitoDeVentasTributables(1, fechaDesde, fechaHasta);

            Assert.Fail();
        }
        /// <summary>
        /// Obtener ventas y cobros masivos segun el idtransaccion (wrapper)
        /// </summary>
        [TestMethod()]
        public void ObtenerVentasYCobranzasMasivaTest()
        {
            var resultado = operacionLogica.ObtenerVentasYCobranzasMasiva(11242);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerVentasClientesConfirmadasTest()
        {
            DateTime fechaDesde = new DateTime(2019, 08, 01);
            DateTime fechaHasta = new DateTime(2019, 08, 31);

            var resultado = operacionLogica.ObtenerVentasClientesQueSeanConComprobantesTributablesConfirmadas(new int[] { 1, 3, 9 }, fechaDesde, fechaHasta);

            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPagosAProveedoresTest1()
        {

            DateTime fechaDesde = new DateTime(2019, 09, 18);
            DateTime fechaHasta = new DateTime(2019, 09, 20);

            //int[] idsPuntosDeVenta = new int[]{ 1194 };
            //int[] idsClientes = new int[]{ 72 };

            //var resultadoClientes = operacionLogica.ObtenerPagosDeClientes(fechaDesde, fechaHasta, idsPuntosDeCompra, idsProveedores);


            int[] idsPuntosDeCompra = new int[] { 11 };
            int[] idsProveedores = new int[] { 1737 };


            var resultadoProveedores = operacionLogica.ObtenerPagosAProveedores(fechaDesde, fechaHasta,true, idsPuntosDeCompra,true, idsProveedores);

            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadasTest()
        {
        //    var watch = new System.Diagnostics.Stopwatch();

        //    watch.Start();

        //    var resumenOrdenesEnLAsQueIngresaDineroYSalenBienes = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();

        //    watch.Stop();

        //    Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


        //    var resumenOrdenesEnLAsQueSaleDineroEIngresanBienes = transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, new int[]  {
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta
        //}, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();


            DateTime fechaDesde = new DateTime(2019, 9, 1);
            DateTime fechaHasta = new DateTime(2019, 9, 30);
            var resultado = operacionLogica.ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadas(new int[] {8,12,16} ,fechaDesde, fechaHasta);

            Assert.Fail();
        }
    }
}
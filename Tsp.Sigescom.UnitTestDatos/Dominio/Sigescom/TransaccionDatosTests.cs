using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos.Tests
{
    [TestClass()]
    public class TransaccionDatosTests
    {
        private readonly ITransaccionRepositorio datos;

        public TransaccionDatosTests()
        {
            datos = Dependencia.Resolve<ITransaccionRepositorio>();
        }

        [TestMethod()]
        public void ObtenerDeudasActorNegocioTest()
        {
            int a = 2;

            DateTime fecha = new DateTime(2018, 12, 03, 00, 00, 00);

            Assert.IsFalse(1 == 1);


        }

        [TestMethod()]
        public void ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion()
        {
            long idTransaccionPadre = 1911115;
            int idTipoTransaccion = TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta;

            var transacciones = datos.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(idTransaccionPadre, idTipoTransaccion);

            Assert.IsNotNull(transacciones);


        }
        




        [TestMethod()]
        public void ObtenerCuotasConSaldoTest()
        {
            int idSerie = 2;
            DateTime fechaDesde = new DateTime(2019, 03, 01);
            DateTime fechaHasta = new DateTime(2019, 04, 30);
           var ordenes = datos.ObtenerCuota(idSerie);
        }
    }
}
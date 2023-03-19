using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.AccesoDatos.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.AccesoDatos.Transacciones.Tests
{
    [TestClass()]
    public class ConsultaTransaccion_DatosTests
    {
        private readonly IConsultaTransaccion_Repositorio _repositorio;

        public ConsultaTransaccion_DatosTests()
        {
            _repositorio = Dependencia.Resolve<IConsultaTransaccion_Repositorio>();
        }
        [TestMethod()]
        public void ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasicoTest()
        {
            int[] idsPuntosDeVentas = new int[] { 3 };
            int[] idsTransacciones = new int[] { 4,61,176,180,182,185,192,188,202,205 };
            DateTime fechaDesde = new DateTime(2022, 04, 01);
            DateTime fechaHasta = new DateTime(2022, 04, 18);

            var resultado = _repositorio.ObtenerTransaccionesPorSerieDeComprobanteYCategoria(idsPuntosDeVentas, idsTransacciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta);
            Assert.Fail();
            }
        }
    }
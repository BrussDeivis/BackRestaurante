using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Logica.SigesHotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Inyeccion;

namespace Tsp.Sigescom.Logica.SigesHotel.Tests
{
    [TestClass()]
    public class HotelLogicaTests
    {
        private readonly IHotelLogica _hotelLogica;



        public HotelLogicaTests()
        {
            _hotelLogica = Dependencia.Resolve<IHotelLogica>();
        }

        [TestMethod()]
        public void ObtenerHabitacionesDisponiblesTest()
        {
            var resultado = _hotelLogica.ObtenerHabitacionesDisponibles(1617, new DateTime(2021, 10, 21, 12, 00, 00), new DateTime(2021, 10, 23, 11, 59, 59), 109, 3,0);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerAtencionMacroTest()
        {
            var resultado = _hotelLogica.ObtenerAtencionMacro(63474,null);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerAtencionDesdeAtencionMacroTest()
        {
            var resultado = _hotelLogica.ObtenerAtencionDesdeAtencionMacro(63474);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerAtencionDesdeAtencionTest()
        {
            var resultado = _hotelLogica.ObtenerAtencionDesdeAtencion(63475);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPlanificadorTest()
        {
            //var resultado = _hotelLogica.ObtenerPlanificador(23, new DateTime(2021, 11, 15, 00, 00, 00), new DateTime(2021, 11, 18, 00, 00, 00),0,0,3);
            Assert.Fail();
        }
    }
}
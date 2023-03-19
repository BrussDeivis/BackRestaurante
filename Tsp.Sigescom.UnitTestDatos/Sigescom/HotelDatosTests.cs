using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.AccesoDatos.Sigescom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Inyeccion;

namespace Tsp.Sigescom.AccesoDatos.Sigescom.Tests
{
    [TestClass()]
    public class HotelDatosTests
    {
        private readonly IHotelRepositorio _repositorio;

        public HotelDatosTests()
        {
            _repositorio = Dependencia.Resolve<IHotelRepositorio>();
        }
        [TestMethod()]
        public void ObtenerEstadoHabitacionPlanificadorTest()
        {
            var rr = _repositorio.ObtenerEstadoHabitacionPlanificador(new DateTime(2022, 05, 27), 1077, new DateTime(2022, 05, 27));
        }
    }
}